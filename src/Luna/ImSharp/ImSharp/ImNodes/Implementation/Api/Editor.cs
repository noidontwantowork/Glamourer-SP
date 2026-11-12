namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    public static unsafe partial class Api
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static Internal.EditorContext* EditorContextCreate()
        {
            var ret = Im.Main.Alloc<Internal.EditorContext>();
            *ret = new Internal.EditorContext();
            return ret;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void EditorContextFree(Internal.EditorContext* editor)
        {
            editor->Dispose();
            Im.Main.Free(editor);
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void EditorContextSet(Internal.EditorContext* editor)
            => Context->EditorContext = editor;

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void GetPanning(ImVec2* ret)
        {
            Debug.Assert(ret is not null);
            *ret = Editor->Panning;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void ResetPanning(ImVec2 position)
            => Editor->Panning = position;

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void MoveToNode(NodeId nodeId)
        {
            ref var editor = ref *Editor;
            ref var node   = ref Editor->Nodes.FindOrCreateObject(nodeId.Id);
            editor.Panning -= node.Origin;
        }

        public static void BeginNodeEditor()
        {
            ref var context = ref Internal.Scope.None.Check(Internal.Scope.Editor);
            ref var editor  = ref *Editor;
            editor.AutoPanningDelta  = Vector2.Zero;
            editor.GridContentBounds = new Rectangle(new Vector2(float.MaxValue), new Vector2(float.MinValue));
            editor.MiniMap.Enabled   = false;
            editor.Nodes.Reset();
            editor.Pins.Reset();
            editor.Links.Reset();

            context.HoveredNodeIndex = Internal.NodeIndex.Invalid;
            context.HoveredLinkIndex = Internal.LinkIndex.Invalid;
            context.HoveredPinIndex  = Internal.AttributeIndex.Invalid;
            context.DeletedLinkIndex = Internal.LinkIndex.Invalid;
            context.SnapLinkIndex    = Internal.LinkIndex.Invalid;

            context.NodeIndicesOverlappingWithMouse.Clear<Internal.NodeIndex>();
            context.ImNodesUiState         &= ~(Internal.UiState.LinkStarted | Internal.UiState.LinkCreated | Internal.UiState.LinkDropped);
            context.MousePosition          =  Im.Mouse.Position;
            context.LeftMouseClicked       =  Im.Mouse.IsClicked(MouseButton.Left);
            context.LeftMouseReleased      =  Im.Mouse.IsReleased(MouseButton.Left);
            context.LeftMouseDragging      =  Im.Mouse.IsDragging(MouseButton.Left, 0);
            context.AltMouseClicked        =  context.Io.AltMouseClicked;
            context.AltMouseDragging       =  context.Io.AltMouseDragging;
            context.AltMouseScrollDelta    =  Im.Io.MouseWheel;
            context.MultipleSelectModifier =  context.Io.MultipleSelectActive;

            context.ActiveAttribute = false;

            var flags = WindowFlags.NoScrollbar | WindowFlags.NoScrollWithMouse;
            // CUSTOM
            if (!context.ImNodesUiState.HasFlag(Internal.UiState.NoSelection))
                flags |= WindowFlags.NoMove;
            Im.Group();
            Im.ColorStyle().Push(ImStyleDouble.FramePadding, Vector2.One)
                .Push(ImStyleDouble.WindowPadding, Vector2.Zero)
                .Push(ImGuiColor.ChildBackground,  Style[ImNodesColor.GridBackground]);
            Im.Child.Begin("scrolling_region"u8, Vector2.Zero, true, flags);
            context.CanvasOriginScreenSpace = Im.Cursor.ScreenPosition;

            // NOTE: we have to fetch the canvas draw list *after* we call
            // BeginChild(), otherwise the ImGui UI elements are going to be
            // rendered into the parent window draw list.
            Internal.SetDrawList(Im.Window.DrawList);

            var canvasSize = Im.Window.Size;
            context.CanvasRectangleScreenSpace =
                new Rectangle(editor.EditorToScreen(Vector2.Zero), editor.EditorToScreen(canvasSize));
            if (Style.Flags.HasFlag(ImNodesStyleFlags.GridLines))
                Internal.DrawGrid(ref editor, canvasSize);
        }

        public static void EndNodeEditor()
        {
            ref var context       = ref Internal.Scope.Editor.Check(Internal.Scope.None);
            ref var editor        = ref *Editor;
            var     noGridContent = editor.GridContentBounds.IsInverted;
            if (noGridContent)
                editor.GridContentBounds = editor.ScreenToGrid(context.CanvasRectangleScreenSpace);

            // Detect ImGui interaction first, because it blocks interaction with the rest of the UI
            if (context.LeftMouseClicked && Im.Item.AnyActive)
                editor.ClickInteraction.Type = Internal.ClickInteractionType.ImGuiItem;

            // Detect which UI element is being hovered over. Detection is done in a hierarchical fashion,
            // because a UI element being hovered excludes any other as being hovered over.

            // Don't do hovering detection for nodes/links/pins when interacting with the mini-map, since
            // its an *overlay* with its own interaction behavior and must have precedence during mouse
            // interaction.
            if (editor.ClickInteraction.Type is Internal.ClickInteractionType.None or Internal.ClickInteractionType.LinkCreation
             && Internal.MouseInCanvas()
             && !Internal.MiniMapHovered)
                // CUSTOM
                if (!context.ImNodesUiState.HasFlag(Internal.UiState.NoHovering))
                {
                    // Pins needs some special care. We need to check the depth stack to see which pins are
                    // being occluded by other nodes.
                    Internal.ResolveOccludedPins(editor, ref context.OccludedPinIndices);

                    context.HoveredPinIndex = Internal.ResolveHoveredPin(editor.Pins, context.OccludedPinIndices);
                    // Resolve which node is actually on top and being hovered using the depth stack.
                    if (!context.HoveredPinIndex.IsValid)
                        context.HoveredNodeIndex = Internal.ResolveHoveredNode(editor.NodeDepthOrder);

                    // We don't check for hovered pins here, because if we want to detach a link by clicking and
                    // dragging, we need to have both a link and pin hovered.
                    if (!context.HoveredNodeIndex.IsValid)
                        context.HoveredLinkIndex = Internal.ResolveHoveredLink(editor.Links, editor.Pins);
                }

            for (var nodeIndex = 0; nodeIndex < editor.Nodes.FullCount; ++nodeIndex)
            {
                if (!editor.Nodes.Used(nodeIndex))
                    continue;

                Internal.ActivateNodeBackground(nodeIndex);
                Internal.DrawNode(ref editor, nodeIndex);
            }

            // In order to render the links underneath the nodes, we want to first select the bottom draw
            // channel.
            context.CanvasDrawList.Splitter.SetChannel(0);

            for (var linkIndex = 0; linkIndex < editor.Links.FullCount; ++linkIndex)
            {
                if (editor.Links.Used(linkIndex))
                    Internal.DrawLink(ref editor, linkIndex);
            }

            // Render the click interaction UI elements (partial links, box selector) on top of everything
            // else.
            Internal.AppendClickInteractionChannel();
            Internal.ActivateClickInteractionChannel();

            if (Internal.MiniMapActive)
            {
                Internal.CalculateLayout();
                Internal.UpdateMiniMap();
            }

            // Handle node graph interaction
            if (!Internal.MiniMapHovered)
            {
                if (context.LeftMouseClicked)
                {
                    if (context.HoveredLinkIndex.IsValid)
                        Internal.BeginLinkInteraction(ref editor, context.HoveredLinkIndex, context.HoveredPinIndex);
                    else if (context.HoveredPinIndex.IsValid)
                        Internal.BeginLinkCreation(ref editor, context.HoveredPinIndex);
                    else if (context.HoveredNodeIndex.IsValid)
                        Internal.BeginNodeSelection(ref editor, context.HoveredNodeIndex);
                    else
                        Internal.BeginCanvasInteraction(ref editor);
                }
                else if (context.LeftMouseReleased || context.AltMouseClicked || context.AltMouseScrollDelta is not 0)
                {
                    Internal.BeginCanvasInteraction(ref editor);
                }

                var shouldAutoPan = editor.ClickInteraction.Type is Internal.ClickInteractionType.BoxSelection
                    or Internal.ClickInteractionType.LinkCreation
                    or Internal.ClickInteractionType.Node;
                if (shouldAutoPan && !Internal.MouseInCanvas())
                {
                    var mouse     = Im.Mouse.Position;
                    var center    = context.CanvasRectangleScreenSpace.Center;
                    var direction = Vector2.Normalize(center - mouse);
                    editor.AutoPanningDelta =  direction * Im.Io.DeltaTime * Io.AutoPanningSpeed;
                    editor.Panning          += editor.AutoPanningDelta;
                }
            }

            Internal.ClickInteractionUpdate(ref editor);

            // At this point, draw commands have been issued for all nodes (and pins). Update the node pool
            // to detect unused node slots and remove those indices from the depth stack before sorting the
            // node draw commands by depth.
            editor.Nodes.Update();
            editor.Pins.Update();
            Internal.SortChannelsByDepth(editor.NodeDepthOrder);

            // After the links have been rendered, the link pool can be updated as well.
            editor.Links.Update();

            // Finally, merge the draw channels
            context.CanvasDrawList.Splitter.Merge();

            Im.ChildDisposable.EndUnsafe();
            Im.ColorDisposable.PopUnsafe();
            Im.StyleDisposable.PopUnsafe(2);
            Im.GroupDisposable.EndUnsafe();
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void MiniMap(float sizeFraction, MiniMapLocation location, delegate* unmanaged<NodeId, nint, void> callback,
            nint userData)
        {
            Debug.Assert(sizeFraction is > 0 and <= 1);
            Internal.Scope.Editor.Check();

            ref var map = ref Editor->MiniMap;
            map.Enabled                      = true;
            map.SizeFraction                 = sizeFraction;
            map.Location                     = location;
            map.NodeHoveringCallback         = (delegate* unmanaged<NodeId, void*, void>)callback;
            map.NodeHoveringCallbackUserData = (void*)userData;

            // Actual drawing/updating of the MiniMap is done in EndNodeEditor so that
            // mini map is draw over everything and all pin/link positions are updated
            // correctly relative to their respective nodes. Hence, we must store some 
            // of the state for the mini map in GImNodes for the actual drawing/updating
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static ImBool IsEditorHovered()
            => Internal.MouseInCanvas();

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static byte* SaveCurrentEditorStateToIniString(ulong* dataSize)
            => SaveEditorStateToIniString(Editor, dataSize);

        public static byte* SaveEditorStateToIniString(Internal.EditorContext* editor, ulong* dataSize)
        {
            Debug.Assert(editor is not null);
            ref var buffer = ref Context->TextBuffer.Buffer;
            buffer.Clear<TrivialTypeInformation<byte>>();
            buffer.Reserve<TrivialTypeInformation<byte>>(64 * editor->Nodes.FullCount);
            var span = new Span<byte>(buffer.Data, buffer.Capacity);
            if (Utf8.TryWrite(span, $"[editor]\npanning={(int)editor->Panning.X},{(int)editor->Panning.Y}\n", out var bytesWritten))
                for (var nodeIndex = 0; nodeIndex < editor->Nodes.FullCount; ++nodeIndex)
                {
                    if (!editor->Nodes.Used(nodeIndex))
                        continue;

                    ref readonly var node = ref editor->Nodes.Get(nodeIndex);
                    span = span[bytesWritten..];
                    if (!Utf8.TryWrite(span, $"\n[node.{node.Id.Id}]\norigin={(int)node.Origin.X},{(int)node.Origin.Y}\n",
                            out var subBytesWritten))
                        break;

                    span         =  span[subBytesWritten..];
                    bytesWritten += subBytesWritten;
                }

            span[bytesWritten] = 0;
            if (dataSize is not null)
                *dataSize = (ulong)bytesWritten;
            return buffer.Data;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void LoadCurrentEditorStateFromIniString(byte* data, ulong dataSize)
            => LoadEditorStateFromIniString(Editor, data, dataSize);

        public static void LoadEditorStateFromIniString(Internal.EditorContext* editorPtr, byte* data, ulong dataSize)
        {
            if (dataSize is 0)
                return;

            ref var            editor       = ref *(editorPtr is null ? Editor : editorPtr);
            var                span         = new ReadOnlySpan<byte>(data, (int)dataSize);
            var                inEditorPart = false;
            Internal.NodeData* inNodePart   = null;
            foreach (var lineRange in span.SplitAny("\r\n"u8))
            {
                var line = span[lineRange];
                if (line.IsEmpty || line[0] is (byte)';')
                    continue;

                switch (line)
                {
                    case [(byte)'[', (byte)'e', (byte)'d', (byte)'i', (byte)'t', (byte)'o', (byte)'r', (byte)']']:
                        inEditorPart = true;
                        inNodePart   = null;
                        break;
                    case [(byte)'[', (byte)'n', (byte)'o', (byte)'d', (byte)'e', (byte)'.', .. var idSpan, (byte)']']
                        when int.TryParse(idSpan, out var id):
                        inEditorPart   = false;
                        inNodePart     = editor.Nodes.FindOrCreateIndex(id) + editor.Nodes.Pool.Data;
                        inNodePart->Id = id;
                        break;
                    case [(byte)'p', (byte)'a', (byte)'n', (byte)'n', (byte)'i', (byte)'n', (byte)'g', (byte)'=', .. var panningSpan]
                        when inEditorPart && TryParseVector(panningSpan, out var panning):
                        editor.Panning = panning;
                        break;
                    case [(byte)'o', (byte)'r', (byte)'i', (byte)'g', (byte)'i', (byte)'n', (byte)'=', .. var originSpan]
                        when inNodePart is not null && TryParseVector(originSpan, out var origin):
                        inNodePart->Origin = Internal.SnapOriginToGrid(origin);
                        break;
                }
            }
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SaveCurrentEditorStateToIniFile(byte* fileName)
            => SaveEditorStateToIniFile(Editor, fileName);

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SaveEditorStateToIniFile(Internal.EditorContext* editor, byte* fileName)
        {
            ulong size;
            var   data = SaveEditorStateToIniString(editor, &size);
            var   span = new ReadOnlySpan<byte>(data, (int)size);
            var   name = Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(fileName));
            File.WriteAllBytes(name, span);
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void LoadCurrentEditorStateFromIniFile(byte* fileName)
            => LoadEditorStateFromIniFile(Editor, fileName);

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void LoadEditorStateFromIniFile(Internal.EditorContext* editor, byte* fileName)

        {
            var name  = Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(fileName));
            var bytes = File.ReadAllBytes(name);
            fixed (byte* ptr = bytes)
            {
                LoadEditorStateFromIniString(editor, ptr, (ulong)bytes.Length);
            }
        }

        private static bool TryParseVector(ReadOnlySpan<byte> vectorSpan, out Vector2 value)
        {
            var comma = vectorSpan.IndexOf((byte)',');
            if (comma < 0 || !float.TryParse(vectorSpan[..comma], out var x) || !float.TryParse(vectorSpan[(comma + 1)..], out var y))
            {
                value = Vector2.NaN;
                return false;
            }

            value = new Vector2(x, y);
            return true;
        }
    }
}
