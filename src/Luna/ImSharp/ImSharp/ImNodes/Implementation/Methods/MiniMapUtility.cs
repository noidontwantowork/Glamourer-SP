namespace ImSharp.ImNodes;

public static unsafe partial class Internal
{
    public static bool MiniMapActive
        => ImNodes.Editor->MiniMap.Enabled && ImNodes.Editor->MiniMap.SizeFraction > 0;

    public static bool MiniMapHovered
        => MiniMapActive && Im.Mouse.IsHoveringRectangle(ImNodes.Editor->MiniMap.RectangleScreenSpace);

    public static void CalculateLayout()
    {
        ref var editor     = ref *ImNodes.Editor;
        var     editorRect = ImNodes.Context->CanvasRectangleScreenSpace;
        var     border     = ImNodes.Style.MiniMapPadding;

        // Compute the size of the mini-map area
        var maxSize         = (editorRect.Size * editor.MiniMap.SizeFraction - 2 * border).Floor();
        var maxAspectRatio  = maxSize.X / maxSize.Y;
        var gridContentSize = editor.GridContentBounds.IsInverted ? maxSize : editor.GridContentBounds.Size.Floor();
        var gridAspectRatio = gridContentSize.X / gridContentSize.Y;
        var miniMapSize = gridAspectRatio > maxAspectRatio
            ? new Vector2(maxSize.X,                   maxSize.X / gridAspectRatio).Floor()
            : new Vector2(maxSize.Y * gridAspectRatio, maxSize.Y).Floor();

        // Compute location of the mini-map
        var offset = ImNodes.Style.MiniMapOffset;
        var align = editor.MiniMap.Location switch
        {
            MiniMapLocation.BottomRight => Vector2.One,
            MiniMapLocation.BottomLeft  => Vector2.UnitY,
            MiniMapLocation.TopRight    => Vector2.UnitX,
            _                           => Vector2.Zero,
        };
        var topLeft     = editorRect.Minimum + offset + border;
        var bottomRight = editorRect.Maximum - offset - border - miniMapSize;
        var miniMapPos  = Vector2.Lerp(topLeft, bottomRight, align).Floor();

        editor.MiniMap.ContentScreenSpace   = Rectangle.FromSize(miniMapPos, miniMapSize);
        editor.MiniMap.RectangleScreenSpace = editor.MiniMap.ContentScreenSpace.Expand(border);
        editor.MiniMap.Scaling              = miniMapSize.X / gridContentSize.X;
    }

    public static void DrawMiniMapNode(ref EditorContext editor, NodeIndex nodeIndex)
    {
        ref readonly var node     = ref editor.Nodes.Get(nodeIndex);
        var              nodeRect = editor.ScreenToMiniMap(node.Rectangle);
        // Round to near whole pixel value for corner-rounding to prevent visual glitches
        var    rounding = MathF.Floor(node.Layout.CornerRounding * editor.MiniMap.Scaling);
        Rgba32 nodeBackground;
        if (editor.ClickInteraction.Type is ClickInteractionType.None && Im.Mouse.IsHoveringRectangle(nodeRect))
        {
            if (editor.MiniMap.NodeHoveringCallback is not null)
                editor.MiniMap.NodeHoveringCallback(node.Id, editor.MiniMap.NodeHoveringCallbackUserData);
            nodeBackground = ImNodes.Style[ImNodesColor.MiniMapNodeBackgroundHovered];
        }
        else if (editor.SelectedNodeIndices.Contains(nodeIndex))
        {
            nodeBackground = ImNodes.Style[ImNodesColor.MiniMapNodeBackgroundSelected];
        }
        else
        {
            nodeBackground = ImNodes.Style[ImNodesColor.MiniMapNodeBackground];
        }

        var outline  = ImNodes.Style[ImNodesColor.MiniMapNodeOutline];
        var drawList = ImNodes.Context->CanvasDrawList.Shape;
        drawList.RectangleFilled(nodeRect, nodeBackground, rounding);
        drawList.Rectangle(nodeRect, outline, rounding);
    }

    public static void DrawMiniMapLink(ref EditorContext editor, LinkIndex linkIndex)
    {
        // It's possible for a link to be deleted in begin_link_interaction. A user
        // may detach a link, resulting in the link wire snapping to the mouse
        // position.
        // In other words, skip rendering the link if it was deleted.
        if (ImNodes.Context->DeletedLinkIndex == linkIndex)
            return;

        ref readonly var link     = ref editor.Links.Get(linkIndex);
        ref readonly var startPin = ref editor.Pins.Get(link.StartPinIndex);
        ref readonly var endPin   = ref editor.Pins.Get(link.EndPinIndex);

        var bezier = CubicBezier.Generate(editor.ScreenToMiniMap(startPin.Position),
            editor.ScreenToMiniMap(endPin.Position), ImNodes.Style.LinkLineSegmentsPerLength / editor.MiniMap.Scaling,
            startPin.Type is AttributeType.Input);

        var color = ImNodes.Style[
            editor.SelectedLinkIndices.Contains(linkIndex) ? ImNodesColor.MiniMapLinkSelected : ImNodesColor.MiniMapLink];
        ImNodes.Context->CanvasDrawList.Shape.BezierCubic(bezier.Point0, bezier.Point1, bezier.Point2, bezier.Point3, color,
            ImNodes.Style.LinkThickness * editor.MiniMap.Scaling, bezier.NumSegments);
    }

    public static void UpdateMiniMap()
    {
        ref var          editor = ref *ImNodes.Editor;
        ref readonly var rect   = ref editor.MiniMap.RectangleScreenSpace;

        // Create a child window below mini-map, so it blocks all mouse interaction on canvas.
        Im.Cursor.Position = rect.Minimum;
        bool hovered;
        using (Im.Child.Begin("minimap"u8, rect.Size, false, WindowFlags.NoBackground))
        {
            var drawList   = ImNodes.Context->CanvasDrawList;
            var background = ImNodes.Style[MiniMapHovered ? ImNodesColor.MiniMapBackgroundHovered : ImNodesColor.MiniMapBackground];
            drawList.Shape.RectangleFilled(rect, background);
            drawList.Shape.Rectangle(rect, ImNodes.Style[ImNodesColor.MiniMapOutline]);

            // Clip draw list items to mini-map rect (after drawing background/outline)
            using (drawList.PushClipRect(rect, true))
            {
                // Draw links first so they appear under nodes, and we can use the same draw channel
                for (var linkIndex = 0; linkIndex < editor.Links.FullCount; ++linkIndex)
                {
                    if (editor.Links.Used(linkIndex))
                        DrawLink(ref editor, linkIndex);
                }

                for (var nodeIndex = 0; nodeIndex < editor.Nodes.FullCount; ++nodeIndex)
                {
                    if (editor.Nodes.Used(nodeIndex))
                        DrawNode(ref editor, nodeIndex);
                }

                var canvasRect = editor.ScreenToMiniMap(ImNodes.Context->CanvasRectangleScreenSpace);
                drawList.Shape.RectangleFilled(canvasRect, ImNodes.Style[ImNodesColor.MiniMapCanvas]);
                drawList.Shape.Rectangle(canvasRect, ImNodes.Style[ImNodesColor.MiniMapCanvasOutline]);
            }

            hovered = Im.Window.Hovered();
        }

        if (hovered
         && Im.Mouse.IsDown(MouseButton.Left)
         && editor.ClickInteraction.Type is ClickInteractionType.None
         && ImNodes.Context->NodeIndexSubmissionOrder.Count > 0)
        {
            var target = editor.MiniMapToGrid(Im.Mouse.Position);
            var center = ImNodes.Context->CanvasRectangleScreenSpace.Size * 0.5f;
            editor.Panning = (center - target).Floor();
        }

        // Reset callback info after use
        editor.MiniMap.NodeHoveringCallback         = null;
        editor.MiniMap.NodeHoveringCallbackUserData = null;
    }
}
