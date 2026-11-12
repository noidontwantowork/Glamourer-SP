namespace ImSharp.ImNodes;

public static unsafe partial class Internal
{
    public static bool RectangleOverlapsLink(in Rectangle rectangle, Vector2 linkStart, Vector2 linkEnd, AttributeType startType)
    {
        var oriented = new Rectangle(linkStart, linkEnd).Orient();
        if (rectangle.Overlaps(oriented))
        {
            if (rectangle.Contains(linkStart) || rectangle.Contains(linkEnd))
                return true;

            var bezier = CubicBezier.Generate(linkStart, linkEnd, ImNodes.Style.LinkLineSegmentsPerLength, startType is AttributeType.Input);
            return bezier.Overlaps(rectangle);
        }

        return false;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Vector2 GetScreenSpacePinCoordinates(in Rectangle node, in Rectangle attribute, AttributeType type)
    {
        var x = type switch
        {
            AttributeType.Input  => node.Minimum.X - ImNodes.Context->Style.PinOffset,
            AttributeType.Output => node.Maximum.X + ImNodes.Context->Style.PinOffset,
            _                    => throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(AttributeType)),
        };
        return new Vector2(x, 0.5f * (attribute.Minimum.Y + attribute.Maximum.Y));
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Vector2 GetScreenSpacePinCoordinates(in EditorContext editor, in PinData pin)
        => GetScreenSpacePinCoordinates(editor.Nodes.Get(pin.ParentNodeIndex).Rectangle, pin.AttributeRectangle, pin.Type);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool MouseInCanvas()
    {
        if (!Im.Window.Hovered() && !Im.Window.Focused())
            return false;

        return ImNodes.Context->CanvasRectangleScreenSpace.Contains(Im.Mouse.Position);
    }

    public static void BeginNodeSelection(ref EditorContext editor, NodeIndex nodeIndex)
    {
        // Don't start selecting a node if we are e.g. already creating and dragging
        // a new link! New link creation can happen when the mouse is clicked over
        // a node, but within the hover radius of a pin.
        if (editor.ClickInteraction.Type is not ClickInteractionType.None)
            return;

        // CUSTOM
        if (ImNodes.Context->ImNodesUiState.HasFlag(UiState.NoSelection))
            return;

        editor.ClickInteraction.Type = ClickInteractionType.Node;
        // If the node is not already contained in the selection, then we want only
        // the interaction node to be selected, effective immediately.
        //
        // If the multiple selection modifier is active, we want to add this node
        // to the current list of selected nodes.
        //
        // Otherwise, we want to allow for the possibility of multiple nodes to be
        // moved at once.
        if (!editor.SelectedNodeIndices.Contains(nodeIndex))
        {
            editor.SelectedLinkIndices.Clear<LinkIndex>();
            if (!ImNodes.Context->MultipleSelectModifier)
                editor.SelectedNodeIndices.Clear<NodeIndex>();
            editor.SelectedNodeIndices.Add<NodeIndex>(nodeIndex);
        }
        else if (ImNodes.Context->MultipleSelectModifier)
        {
            var index = editor.SelectedNodeIndices.FindIndex(nodeIndex);
            if (index is not IIndex.InvalidIndex)
                editor.SelectedNodeIndices.RemoveAt<NodeIndex>(index);
            editor.ClickInteraction.Type = ClickInteractionType.None;
        }

        // To support snapping of multiple nodes, we need to store the offset of
        // each node in the selection to the origin of the dragged node.
        var referenceOrigin = editor.Nodes.Get(nodeIndex).Origin;
        editor.PrimaryNodeOffset =
            referenceOrigin + ImNodes.Context->CanvasOriginScreenSpace + editor.Panning - ImNodes.Context->MousePosition;
        editor.SelectedNodeOffsets.Clear<TrivialTypeInformation<Vector2>>();
        foreach (var node in editor.SelectedNodeIndices)
        {
            var origin = editor.Nodes.Get(node).Origin - referenceOrigin;
            editor.SelectedNodeOffsets.Add<TrivialTypeInformation<Vector2>>(origin);
        }
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void BeginLinkSelection(ref EditorContext editor, LinkIndex linkIndex)
    {
        // CUSTOM
        if (ImNodes.Context->ImNodesUiState.HasFlag(UiState.NoSelection))
            return;

        editor.ClickInteraction.Type = ClickInteractionType.Link;
        editor.SelectedNodeIndices.Clear<NodeIndex>();
        editor.SelectedLinkIndices.Clear<LinkIndex>();
        editor.SelectedLinkIndices.Add<LinkIndex>(linkIndex);
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void BeginLinkDetach(ref EditorContext editor, LinkIndex linkIndex, AttributeIndex detachPinIndex)
    {
        ref readonly var link  = ref editor.Links.Get(linkIndex.Index);
        ref var          state = ref editor.ClickInteraction;
        state.Type                        = ClickInteractionType.LinkCreation;
        state.LinkCreation.EndPinIndex    = AttributeIndex.Invalid;
        state.LinkCreation.StartPinIndex  = detachPinIndex == link.StartPinIndex ? link.EndPinIndex : link.StartPinIndex;
        ImNodes.Context->DeletedLinkIndex = linkIndex.Index;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void BeginLinkCreation(ref EditorContext editor, AttributeIndex hoveredPin)
    {
        ref var i = ref editor.ClickInteraction;
        i.Type                          =  ClickInteractionType.LinkCreation;
        i.LinkCreation.StartPinIndex    =  hoveredPin;
        i.LinkCreation.EndPinIndex      =  AttributeIndex.Invalid;
        i.LinkCreation.Type             =  LinkCreationType.Standard;
        ImNodes.Context->ImNodesUiState |= UiState.LinkStarted;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void BeginLinkInteraction(ref EditorContext editor, LinkIndex link)
        => BeginLinkInteraction(ref editor, link, AttributeIndex.Invalid);

    public static void BeginLinkInteraction(ref EditorContext editor, LinkIndex linkIndex, AttributeIndex pinIndex)
    {
        // Check if we are clicking the link with the modifier pressed.
        // This will in a link detach via clicking.
        var modifierPressed = ImNodes.Context->Io.LinkDetachWithModifierClick switch
        {
            null    => false,
            var ptr => (bool)*ptr,
        };

        if (modifierPressed)
        {
            ref readonly var link            = ref editor.Links.Get(linkIndex.Index);
            ref readonly var startPin        = ref editor.Pins.Get(link.StartPinIndex);
            ref readonly var endPin          = ref editor.Pins.Get(link.EndPinIndex);
            var              mousePos        = ImNodes.Context->MousePosition;
            var              distanceToStart = (startPin.Position - mousePos).LengthSquared();
            var              distanceToEnd   = (endPin.Position - mousePos).LengthSquared();
            var              closestPinId    = distanceToStart < distanceToEnd ? link.StartPinIndex : link.EndPinIndex;
            editor.ClickInteraction.Type = ClickInteractionType.LinkCreation;
            BeginLinkDetach(ref editor, linkIndex, closestPinId);
            editor.ClickInteraction.LinkCreation.Type = LinkCreationType.FromDetach;
        }
        else
        {
            if (pinIndex.IsValid)
            {
                var hoveredFlags = editor.Pins.Get(pinIndex).Flags;
                // Check the 'click and drag to detach' case.
                if (hoveredFlags.HasFlag(AttributeFlags.EnableLinkDetachWithDragClick))
                {
                    BeginLinkDetach(ref editor, linkIndex, pinIndex);
                    editor.ClickInteraction.LinkCreation.Type = LinkCreationType.FromDetach;
                }
                else
                {
                    BeginLinkCreation(ref editor, pinIndex);
                }
            }
            else
            {
                BeginLinkSelection(ref editor, linkIndex);
            }
        }
    }

    public static void BeginCanvasInteraction(ref EditorContext editor)
    {
        ref var context = ref *ImNodes.Context;
        var anyHovered = context.HoveredNodeIndex.IsValid
         || context.HoveredLinkIndex.IsValid
         || context.HoveredPinIndex.IsValid
         || Im.Item.AnyHovered;
        var mouseNotInCanvas = !MouseInCanvas();
        if (editor.ClickInteraction.Type is not ClickInteractionType.None || anyHovered || mouseNotInCanvas || !Im.Window.Hovered())
            return;

        if (context.AltMouseClicked)
        {
            editor.ClickInteraction.Type = ClickInteractionType.Panning;
        }
        else if (context.LeftMouseClicked)
        {
            // CUSTOM
            if (context.ImNodesUiState.HasFlag(UiState.NoSelection))
                return;

            editor.ClickInteraction.Type = ClickInteractionType.BoxSelection;
            editor.ClickInteraction.BoxSelector = editor.ClickInteraction.BoxSelector with
            {
                Minimum = editor.ScreenToGrid(context.MousePosition),
            };
        }
    }

    public static void BoxSelectorUpdateSelection(ref EditorContext editor, in Rectangle boxRectangle)
    {
        // Invert box selector coordinates as needed
        var box = boxRectangle.Orient();

        // Update node selection
        editor.SelectedNodeIndices.Clear<NodeIndex>();

        // Test for overlap against node rectangles
        for (var nodeIndex = 0; nodeIndex < editor.Nodes.FullCount; ++nodeIndex)
        {
            if (!editor.Nodes.Used(nodeIndex))
                continue;

            ref readonly var node = ref editor.Nodes.Get(nodeIndex);
            if (box.Overlaps(node.Rectangle))
                editor.SelectedNodeIndices.Add<NodeIndex>(nodeIndex);
        }

        // Update link selection
        editor.SelectedLinkIndices.Clear<LinkIndex>();

        // Test for overlap against links
        for (var linkIndex = 0; linkIndex < editor.Links.FullCount; ++linkIndex)
        {
            if (!editor.Links.Used(linkIndex))
                continue;

            ref readonly var link          = ref editor.Links.Get(linkIndex);
            ref readonly var startPin      = ref editor.Pins.Get(link.StartPinIndex);
            ref readonly var endPin        = ref editor.Pins.Get(link.EndPinIndex);
            ref readonly var nodeStartRect = ref editor.Nodes.Get(startPin.ParentNodeIndex).Rectangle;
            ref readonly var nodeEndRect   = ref editor.Nodes.Get(endPin.ParentNodeIndex).Rectangle;
            var              start         = GetScreenSpacePinCoordinates(nodeStartRect, startPin.AttributeRectangle, startPin.Type);
            var              end           = GetScreenSpacePinCoordinates(nodeEndRect,   endPin.AttributeRectangle,   endPin.Type);

            // Test
            if (RectangleOverlapsLink(box, start, end, startPin.Type))
                editor.SelectedLinkIndices.Add<LinkIndex>(linkIndex);
        }
    }

    public static Vector2 SnapOriginToGrid(Vector2 origin)
    {
        if (!ImNodes.Context->Style.Flags.HasFlag(ImNodesStyleFlags.GridSnapping))
            return origin;

        var spacing = ImNodes.Context->Style.GridSpacing;
        var halved  = spacing * 0.5f;
        var modX    = (MathF.Abs(origin.X) + halved) % spacing - halved;
        var modY    = (MathF.Abs(origin.Y) + halved) % spacing - halved;
        origin.X += origin.X < 0 ? modX : -modX;
        origin.Y += origin.Y < 0 ? modY : -modY;
        return origin;
    }

    public static void TranslateSelectedNodes(ref EditorContext editor)
    {
        ref var context = ref *ImNodes.Context;
        if (!context.LeftMouseDragging)
            return;

        // If we have grid snap enabled, don't start moving nodes until we've moved the mouse slightly
        var shouldTranslate = !context.Style.Flags.HasFlag(ImNodesStyleFlags.GridSnapping) || Im.Io.Pointer->MouseDragMaxDistanceSqr[0] > 5.0;
        if (!shouldTranslate)
            return;

        var origin = SnapOriginToGrid(context.MousePosition - context.CanvasOriginScreenSpace - editor.Panning + editor.PrimaryNodeOffset);
        for (var i = 0; i < editor.SelectedNodeIndices.Count; ++i)
        {
            var     nodeOffset = editor.SelectedNodeOffsets[i];
            var     nodeIndex  = editor.SelectedNodeIndices[i];
            ref var node       = ref editor.Nodes.GetWrite(nodeIndex);
            if (node.Draggable)
                node.Origin = origin + nodeOffset + editor.AutoPanningDelta;
        }
    }

    public static LinkIndex FindDuplicateLink(in EditorContext editor, AttributeIndex startPin, AttributeIndex endPin)
    {
        for (var i = 0; i < editor.Links.FullCount; ++i)
        {
            ref readonly var link = ref editor.Links.Get(i);
            if (CheckLink(link, startPin, endPin) && editor.Links.Used(i))
                return i;
        }

        return LinkIndex.Invalid;

        static bool CheckLink(in LinkData link, AttributeIndex startPin, AttributeIndex endPin)
        {
            // Do a unique compare by sorting the pins' addresses.
            // This catches duplicate links, whether they are in the
            // same direction or not.
            // Sorting by pin index should have the uniqueness guarantees as sorting
            // by id -- each unique id will get one slot in the link pool array.
            var (linkStartPin, linkEndPin) = link.StartPinIndex > link.EndPinIndex
                ? (link.EndPinIndex, link.StartPinIndex)
                : (link.StartPinIndex, link.EndPinIndex);

            var (testStartPin, testEndPin) = startPin > endPin
                ? (endPin, startPin)
                : (startPin, endPin);
            return linkStartPin == testStartPin && linkEndPin == testEndPin;
        }
    }

    public static bool ShouldLinkSnapToPin(in EditorContext editor, in PinData startPin, AttributeIndex hoveredPinIndex,
        LinkIndex duplicateLink)
    {
        ref readonly var endPin = ref editor.Pins.Get(hoveredPinIndex);
        if (startPin.ParentNodeIndex == endPin.ParentNodeIndex)
            return false;

        if (startPin.Type == endPin.Type)
            return false;

        // The link to be created must not be a duplicate, unless it is the link which was created on
        // snap. In that case we want to snap, since we want it to appear visually as if the created
        // link remains snapped to the pin.
        return !duplicateLink.IsValid || duplicateLink == ImNodes.Context->SnapLinkIndex;
    }

    public static void ClickInteractionUpdate(ref EditorContext editor)
    {
        switch (editor.ClickInteraction.Type)
        {
            case ClickInteractionType.Node:         NodeClickInteractionUpdate(ref editor); break;
            case ClickInteractionType.Link:         ReleaseCheckUpdate(ref editor); break;
            case ClickInteractionType.LinkCreation: LinkCreationClickInteractionUpdate(ref editor); break;
            case ClickInteractionType.Panning:      PanningClickInteractionUpdate(ref editor); break;
            case ClickInteractionType.BoxSelection: BoxSelectionClickInteractionUpdate(ref editor); break;
            case ClickInteractionType.ImGuiItem:    ReleaseCheckUpdate(ref editor); break;
        }
    }

    [MethodImpl(ImSharpConfiguration.Inl)]
    private static void NodeClickInteractionUpdate(ref EditorContext editor)
    {
        TranslateSelectedNodes(ref editor);
        ReleaseCheckUpdate(ref editor);
    }

    [MethodImpl(ImSharpConfiguration.Inl)]
    private static void ReleaseCheckUpdate(ref EditorContext editor)
    {
        if (ImNodes.Context->LeftMouseReleased)
            editor.ClickInteraction.Type = ClickInteractionType.None;
    }

    [MethodImpl(ImSharpConfiguration.Inl)]
    private static void LinkCreationClickInteractionUpdate(ref EditorContext editor)
    {
        ref var context = ref *ImNodes.Context;

        ref readonly var startPin = ref editor.Pins.Get(editor.ClickInteraction.LinkCreation.StartPinIndex);
        var maybeDuplicateLinkId = context.HoveredPinIndex.IsValid
            ? FindDuplicateLink(editor, editor.ClickInteraction.LinkCreation.StartPinIndex, context.HoveredPinIndex)
            : LinkIndex.Invalid;

        var shouldSnap = context.HoveredPinIndex.IsValid
         && ShouldLinkSnapToPin(editor, startPin, context.HoveredPinIndex, maybeDuplicateLinkId);

        // If we created on snap and the hovered pin is empty or changed, then we need signal that
        // the link's state has changed.
        var snappingPinChanged = editor.ClickInteraction.LinkCreation.EndPinIndex.IsValid
         && context.HoveredPinIndex != editor.ClickInteraction.LinkCreation.EndPinIndex;

        // Detach the link that was created by this link event if it's no longer in snap range
        if (snappingPinChanged && context.SnapLinkIndex.IsValid)
            BeginLinkDetach(ref editor, context.SnapLinkIndex, editor.ClickInteraction.LinkCreation.EndPinIndex);

        var startPos = GetScreenSpacePinCoordinates(editor, startPin);
        // If we are within the hover radius of a receiving pin, snap the link endpoint to it
        var endPos = shouldSnap
            ? GetScreenSpacePinCoordinates(editor, editor.Pins.Get(context.HoveredPinIndex))
            : context.MousePosition;
        var bezier = CubicBezier.Generate(startPos, endPos, ImNodes.Style.LinkLineSegmentsPerLength, startPin.Type is AttributeType.Input);
        context.CanvasDrawList.Shape.BezierCubic(bezier.Point0, bezier.Point1, bezier.Point2, bezier.Point3, ImNodes.Style[ImNodesColor.Link],
            ImNodes.Style.LinkThickness, bezier.NumSegments);

        var linkCreationOnSnap = context.HoveredPinIndex.IsValid
         && editor.Pins.Get(context.HoveredPinIndex).Flags.HasFlag(AttributeFlags.EnableLinkCreationOnSnap);

        if (!shouldSnap)
            editor.ClickInteraction.LinkCreation.EndPinIndex = AttributeIndex.Invalid;

        var createLink = shouldSnap && (context.LeftMouseReleased || linkCreationOnSnap);

        if (createLink && !maybeDuplicateLinkId.IsValid)
        {
            // Avoid send OnLinkCreated() events every frame if the snap link is not saved
            // (only applies for EnableLinkCreationOnSnap)
            if (!context.LeftMouseReleased && editor.ClickInteraction.LinkCreation.EndPinIndex == context.HoveredPinIndex)
                return;

            context.ImNodesUiState                           |= UiState.LinkCreated;
            editor.ClickInteraction.LinkCreation.EndPinIndex =  context.HoveredPinIndex;
        }

        if (context.LeftMouseReleased)
        {
            editor.ClickInteraction.Type = ClickInteractionType.None;
            if (!createLink)
                context.ImNodesUiState |= UiState.LinkDropped;
        }
    }

    [MethodImpl(ImSharpConfiguration.Inl)]
    private static void PanningClickInteractionUpdate(ref EditorContext editor)
    {
        if (ImNodes.Context->AltMouseDragging)
            editor.Panning += Im.Io.Pointer->MouseDelta;
        else
            editor.ClickInteraction.Type = ClickInteractionType.None;
    }

    [MethodImpl(ImSharpConfiguration.Inl)]
    private static void BoxSelectionClickInteractionUpdate(ref EditorContext editor)
    {
        ref var context = ref *ImNodes.Context;
        editor.ClickInteraction.BoxSelector = editor.ClickInteraction.BoxSelector with
        {
            Maximum = editor.ScreenToGrid(context.MousePosition),
        };

        var rect = editor.ClickInteraction.BoxSelector;
        rect = new Rectangle(editor.GridToScreen(rect.Minimum), editor.GridToScreen(rect.Maximum));
        BoxSelectorUpdateSelection(ref editor, rect);
        context.CanvasDrawList.Shape.RectangleFilled(rect, ImNodes.Style[ImNodesColor.BoxSelector]);
        context.CanvasDrawList.Shape.Rectangle(rect, ImNodes.Style[ImNodesColor.BoxSelectorOutline]);

        if (!context.LeftMouseReleased)
            return;

        editor.ClickInteraction.Type = ClickInteractionType.None;

        ref var          depthStack    = ref editor.NodeDepthOrder;
        ref readonly var selectedNodes = ref editor.SelectedNodeIndices;

        if (selectedNodes.Count is 0 || selectedNodes.Count >= depthStack.Count)
            return;

        // Bump the selected node indices, in order, to the top of the depth stack.
        // NOTE: this algorithm has worst case time complexity of O(N^2), if the node selection
        // is ~ N (due to selected_idxs.contains()).
        var numMoved = 0;
        for (var i = 0; i < depthStack.Count - selectedNodes.Count; ++i)
        {
            for (var nodeIndex = depthStack[i]; selectedNodes.Contains(nodeIndex); nodeIndex = depthStack[i])
            {
                depthStack.RemoveAt<NodeIndex>(i);
                depthStack.Add<NodeIndex>(nodeIndex);
                ++numMoved;
            }

            if (numMoved == selectedNodes.Count)
                break;
        }
    }

    public static void ResolveOccludedPins(in EditorContext editor, ref ImVector<AttributeIndex> occludedPins)
    {
        ref readonly var depthStack = ref editor.NodeDepthOrder;
        occludedPins.Clear<AttributeIndex>();
        if (depthStack.Count < 2)
            return;

        for (var node = 0; node < depthStack.Count - 1; ++node)
        {
            ref readonly var lowerNode = ref editor.Nodes.Get(depthStack[node]);
            for (var nextNode = node + 1; nextNode < depthStack.Count; ++nextNode)
            {
                ref readonly var rect = ref editor.Nodes.Get(depthStack[nextNode]).Rectangle;
                foreach (var pinIndex in lowerNode.PinIndices)
                {
                    var pinPosition = editor.Pins.Get(pinIndex).Position;
                    if (rect.Contains(pinPosition))
                        occludedPins.Add<AttributeIndex>(pinIndex);
                }
            }
        }
    }

    public static AttributeIndex ResolveHoveredPin(in IdObjectPool<PinData> pins, in ImVector<AttributeIndex> occludedPins)
    {
        var smallestDistance   = float.MaxValue;
        var closestPin         = AttributeIndex.Invalid;
        var hoverRadiusSquared = ImNodes.Style.PinHoverRadius * ImNodes.Style.PinHoverRadius;

        for (var pin = 0; pin < pins.FullCount; ++pin)
        {
            if (!pins.Used(pin))
                continue;

            if (occludedPins.Contains(pin))
                continue;

            ref readonly var pinData = ref pins.Get(pin);
            if (pinData.Flags.IsDisabled)
                continue;

            var position        = pinData.Position;
            var distanceSquared = (position - ImNodes.Context->MousePosition).LengthSquared();

            // td: GImNodes->Style.PinHoverRadius needs to be copied into pin data and the pin-local
            // value used here. This is no longer called in BeginAttribute/EndAttribute scope and the
            // detected pin might have a different hover radius than what the user had when calling
            // BeginAttribute/EndAttribute.
            if (distanceSquared < hoverRadiusSquared && distanceSquared < smallestDistance)
            {
                smallestDistance = distanceSquared;
                closestPin       = pin;
            }
        }

        return closestPin;
    }

    public static NodeIndex ResolveHoveredNode(in ImVector<NodeIndex> depthStack)
    {
        ref readonly var context = ref *ImNodes.Context;
        switch (context.NodeIndicesOverlappingWithMouse.Count)
        {
            case 0: return NodeIndex.Invalid;
            case 1: return context.NodeIndicesOverlappingWithMouse[0];
            default:
                var largestDepthIndex = -1;
                var nodeIndexOnTop    = NodeIndex.Invalid;
                foreach (var node in context.NodeIndicesOverlappingWithMouse)
                {
                    for (var depthIndex = 0; depthIndex < depthStack.Count; ++depthIndex)
                    {
                        if (depthStack[depthIndex] != node || depthIndex <= largestDepthIndex)
                            continue;

                        largestDepthIndex = depthIndex;
                        nodeIndexOnTop    = node;
                    }
                }

                Debug.Assert(nodeIndexOnTop.IsValid);
                return nodeIndexOnTop;
        }
    }

    public static LinkIndex ResolveHoveredLink(in IdObjectPool<LinkData> links, in IdObjectPool<PinData> pins)
    {
        ref readonly var context          = ref *ImNodes.Context;
        var              smallestDistance = float.MaxValue;
        var              closestLink      = LinkIndex.Invalid;

        // There are two ways a link can be detected as "hovered".
        // 1. The link is within hover distance to the mouse. The closest such link is selected as being
        // hovered over.
        // 2. If the link is connected to the currently hovered pin.
        //
        // The latter is a requirement for link detaching with drag click to work, as both a link and
        // pin are required to be hovered over for the feature to work.
        for (var linkIndex = 0; linkIndex < links.FullCount; ++linkIndex)
        {
            if (!links.Used(linkIndex))
                continue;

            ref readonly var link = ref links.Get(linkIndex);
            // If there is a hovered pin links can only be considered hovered if they use that pin
            if (context.HoveredPinIndex.IsValid)
            {
                if (context.HoveredPinIndex == link.StartPinIndex || context.HoveredPinIndex == link.EndPinIndex)
                    return linkIndex;

                continue;
            }

            ref readonly var startPin = ref pins.Get(link.StartPinIndex);
            ref readonly var endPin   = ref pins.Get(link.EndPinIndex);

            // td: the calculated CubicBeziers could be cached since we generate them again when rendering the links
            var bezier = CubicBezier.Generate(startPin.Position, endPin.Position, ImNodes.Style.LinkLineSegmentsPerLength,
                startPin.Type is AttributeType.Input);

            var linkRect = bezier.GetBoundingBox().Expand(ImNodes.Style.LinkHoverDistance);
            if (!linkRect.Contains(context.MousePosition))
                continue;

            var distance = bezier.GetDistance(context.MousePosition);
            // td: GImNodes->Style.LinkHoverDistance could be also copied into ImLinkData,
            // since we're not calling this function in the same scope as ImNodes::Link(). The
            // rendered/detected link might have a different hover distance than what the user
            // had specified when calling Link()
            if (distance >= smallestDistance || distance >= ImNodes.Style.LinkHoverDistance)
                continue;

            smallestDistance = distance;
            closestLink      = linkIndex;
        }

        return closestLink;
    }
}
