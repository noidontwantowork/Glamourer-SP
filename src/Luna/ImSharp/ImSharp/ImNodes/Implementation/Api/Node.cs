namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    public static unsafe partial class Api
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void BeginNode(NodeId nodeId)
            => BeginNodeInternal(nodeId);

        internal static ref Internal.NodeData BeginNodeInternal(NodeId nodeId)
        {
            ref var context   = ref Internal.Scope.Editor.Check(Internal.Scope.Node);
            ref var editor    = ref *Editor;
            var     nodeIndex = editor.Nodes.FindOrCreateIndex(nodeId.Id);
            context.CurrentNodeIndex = nodeIndex;
            ref var data = ref Editor->Nodes.GetWrite(nodeIndex);
            data.SaveStyle(Style);

            // ImGui::SetCursorPos sets the cursor position, local to the current widget
            // (in this case, the child object started in BeginNodeEditor). Use
            // ImGui::SetCursorScreenPos to set the screen space coordinates directly.
            Im.Cursor.Position = editor.GridToEditor(data.TitleBarOrigin);

            Internal.AddNode(nodeIndex);
            Internal.ActivateCurrentNodeForeground();

            Im.Id.Push(data.Id);
            Im.Group();
            return ref data;
        }

        public static void EndNode()
        {
            ref var context = ref Internal.Scope.Node.Check(Internal.Scope.Editor);
            ref var editor  = ref *Editor;
            Im.GroupDisposable.EndUnsafe();
            Im.IdDisposable.PopUnsafe();
            ref var data = ref editor.Nodes.GetWrite(context.CurrentNodeIndex);
            data.Rectangle = Im.Item.Bounds.Expand(data.Layout.Padding);
            editor.GridContentBounds.Add(data.Origin);
            editor.GridContentBounds.Add(data.Origin + data.Rectangle.Size);
            if (data.Rectangle.Contains(context.MousePosition))
                context.NodeIndicesOverlappingWithMouse.Add<Internal.NodeIndex>(context.CurrentNodeIndex);
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void GetNodeDimensions(ImVec2* ret, NodeId nodeId)
        {
            Debug.Assert(ret is not null);
            ref readonly var editor = ref *Editor;
            var              index  = editor.Nodes.FindIndex(nodeId.Id);
            *ret = editor.Nodes.Get(index).Rectangle.Size;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void BeginNodeTitleBar()
        {
            Internal.Scope.Node.Check();
            Im.Group();
        }

        public static void EndNodeTitleBar()
        {
            Internal.Scope.Node.Check();
            Im.GroupDisposable.EndUnsafe();
            ref var editor = ref *Editor;
            ref var node   = ref editor.Nodes.GetWrite(Context->CurrentNodeIndex);
            node.TitleBarContent = Im.Item.Bounds;
            Im.Item.Add(node.TitleRectangle, Im.Id.Get("title_bar"u8));
            Im.Cursor.Position = editor.GridToEditor(node.ContentOrigin);
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SetNodeDraggable(NodeId id, ImBool draggable)
            => Editor->Nodes.FindOrCreateObject(id.Id).Draggable = draggable;

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SetNodeScreenSpacePos(NodeId id, ImVec2 position)
            => Editor->Nodes.FindOrCreateObject(id).Origin = Editor->ScreenToGrid(position);

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SetNodeEditorSpacePos(NodeId id, ImVec2 position)
            => Editor->Nodes.FindOrCreateObject(id.Id).Origin = Editor->EditorToGrid(position);

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SetNodeGridSpacePos(NodeId id, ImVec2 position)
            => Editor->Nodes.FindOrCreateObject(id.Id).Origin = position;

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void GetNodeScreenSpacePos(ImVec2* ret, NodeId id)
        {
            ref var editor = ref *Editor;
            var     exists = editor.Nodes.TryGetObject(id.Id, out var node);
            Debug.Assert(exists);
            Debug.Assert(ret is not null);
            *ret = editor.GridToScreen(node->Origin);
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void GetNodeEditorSpacePos(ImVec2* ret, NodeId id)
        {
            ref var editor = ref *Editor;
            var     exists = editor.Nodes.TryGetObject(id.Id, out var node);
            Debug.Assert(exists);
            Debug.Assert(ret is not null);
            *ret = editor.GridToEditor(node->Origin);
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void GetNodeGridSpacePos(ImVec2* ret, NodeId id)
        {
            ref var editor = ref *Editor;
            var     exists = editor.Nodes.TryGetObject(id.Id, out var node);
            Debug.Assert(exists);
            Debug.Assert(ret is not null);
            *ret = node->Origin;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SnapNodeToGrid(NodeId id)
        {
            ref var origin = ref Editor->Nodes.FindOrCreateObject(id.Id).Origin;
            origin = Internal.SnapOriginToGrid(origin);
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static ImBool IsNodeHovered(NodeId* hovered)
        {
            ref var context    = ref Internal.Scope.None.Check();
            var     anyHovered = context.HoveredNodeIndex.IsValid;
            if (!anyHovered)
                return false;

            if (hovered is not null)
                *hovered = Editor->Nodes.Get(context.HoveredNodeIndex).Id.Id;

            return true;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static int NumSelectedNodes()
        {
            Internal.Scope.None.Check();
            return Editor->SelectedNodeIndices.Count;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void GetSelectedNodes(NodeId* ids)
        {
            Debug.Assert(ids is not null);
            ref var editor = ref *Editor;
            foreach (var nodeIndex in editor.SelectedNodeIndices)
                *ids++ = editor.Nodes.Get(nodeIndex).Id;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void ClearNodeSelection()
            => Editor->SelectedNodeIndices.Clear<Internal.NodeIndex>();

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void ClearNodeSelection(NodeId id)
            => Internal.ClearObjectSelection(Editor->Nodes, ref Editor->SelectedNodeIndices, id.Id);

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SelectNode(NodeId id)
            => Internal.Select(Editor->Nodes, ref Editor->SelectedNodeIndices, id.Id);

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static ImBool IsNodeSelected(NodeId id)
            => Internal.IsObjectSelected(Editor->Nodes, Editor->SelectedNodeIndices, id.Id);
    }
}
