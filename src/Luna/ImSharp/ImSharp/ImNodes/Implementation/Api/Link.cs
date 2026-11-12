namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    public static unsafe partial class Api
    {
        public static void CreateLink(LinkId id, AttributeId startAttributeId, AttributeId endAttributeId)

        {
            ref var context = ref *Context;
            Debug.Assert(context.CurrentScope is Internal.Scope.Editor);
            ref var editor    = ref *Editor;
            var     linkIndex = editor.Links.FindOrCreateIndex(id);
            ref var link      = ref editor.Links.GetWrite(linkIndex);
            link.Id            = id;
            link.StartPinIndex = editor.Pins.FindOrCreateIndex(startAttributeId.Id);
            link.EndPinIndex   = editor.Pins.FindOrCreateIndex(endAttributeId.Id);
            link.SaveStyle(Style);

            var currentLink = editor.ClickInteraction.Type is Internal.ClickInteractionType.LinkCreation
             && editor.Pins.Get(link.EndPinIndex).Flags.HasFlag(AttributeFlags.EnableLinkCreationOnSnap)
             && editor.ClickInteraction.LinkCreation.StartPinIndex == link.StartPinIndex
             && editor.ClickInteraction.LinkCreation.EndPinIndex == link.EndPinIndex;
            var inverse = editor.ClickInteraction.LinkCreation.StartPinIndex == link.EndPinIndex
             && editor.ClickInteraction.LinkCreation.EndPinIndex == link.StartPinIndex;

            // I feel like those bools are wrongly bracketed, but it is the current implementation.
            if (currentLink || inverse)
                context.SnapLinkIndex = linkIndex;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static ImBool IsLinkHovered(LinkId* hovered)
        {
            ref var context = ref *Context;
            Debug.Assert(context.CurrentScope is Internal.Scope.None);
            var anyHovered = context.HoveredLinkIndex.IsValid;
            if (anyHovered && hovered is not null)
                *hovered = Editor->Links.Get(context.HoveredLinkIndex).Id;

            return anyHovered;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static int NumSelectedLinks()
        {
            Debug.Assert(Context->CurrentScope is Internal.Scope.None);
            return Editor->SelectedLinkIndices.Count;
        }

        public static void GetSelectedLinks(LinkId* ids)
        {
            Debug.Assert(ids is not null);
            ref var editor = ref *Editor;
            foreach (var linkIndex in editor.SelectedLinkIndices)
                *ids++ = editor.Links.Get(linkIndex).Id;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void ClearLinkSelection()
            => Editor->SelectedLinkIndices.Clear<Internal.LinkIndex>();

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void ClearLinkSelection(LinkId link)
            => Internal.ClearObjectSelection(Editor->Links, ref Editor->SelectedLinkIndices, link);

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SelectLink(LinkId link)
            => Internal.Select(Editor->Links, ref Editor->SelectedLinkIndices, link);

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static ImBool IsLinkSelected(LinkId link)
            => Internal.IsObjectSelected(Editor->Links, Editor->SelectedLinkIndices, link);

        public static ImBool IsLinkStarted(AttributeId* startedAtId)
        {
            ref readonly var context = ref *Context;
            Debug.Assert(context.CurrentScope is Internal.Scope.None);
            var started = context.ImNodesUiState.HasFlag(Internal.UiState.LinkStarted);
            if (started && startedAtId is not null)
                *startedAtId = Editor->Pins.Get(Editor->ClickInteraction.LinkCreation.StartPinIndex).Id;
            return started;
        }

        public static ImBool IsLinkDropped(AttributeId* startedAtId, ImBool includingDetachedLinks)
        {
            ref readonly var context = ref *Context;
            Debug.Assert(context.CurrentScope is Internal.Scope.None);
            var dropped = context.ImNodesUiState.HasFlag(Internal.UiState.LinkDropped)
             && (includingDetachedLinks || Editor->ClickInteraction.LinkCreation.Type is not Internal.LinkCreationType.FromDetach);
            if (dropped && startedAtId is not null)
                *startedAtId = Editor->Pins.Get(Editor->ClickInteraction.LinkCreation.StartPinIndex).Id;
            return dropped;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static ImBool IsLinkCreated(AttributeId* startedAt, AttributeId* endedAt, ImBool* createdFromSnap)
            => IsLinkCreated(null, startedAt, null, endedAt, createdFromSnap);

        public static bool IsLinkCreated(NodeId* startedAtNode, AttributeId* startedAtPin, NodeId* endedAtNode, AttributeId* endedAtPin,
            ImBool* createdFromSnap)
        {
            ref readonly var context = ref *Context;
            Debug.Assert(context.CurrentScope is Internal.Scope.None);
            if (!context.ImNodesUiState.HasFlag(Internal.UiState.LinkCreated))
                return false;

            ref var          editor   = ref *Editor;
            ref readonly var startPin = ref editor.Pins.Get(editor.ClickInteraction.LinkCreation.StartPinIndex);
            ref readonly var endPin   = ref editor.Pins.Get(editor.ClickInteraction.LinkCreation.EndPinIndex);
            if (startPin.Type is Internal.AttributeType.Output)
            {
                if (startedAtPin is not null)
                    *startedAtPin = startPin.Id;
                if (endedAtPin is not null)
                    *endedAtPin = endPin.Id;
            }
            else
            {
                if (startedAtPin is not null)
                    *startedAtPin = endPin.Id;
                if (endedAtPin is not null)
                    *endedAtPin = startPin.Id;
            }

            if (startedAtNode is not null && endedAtNode is not null)
            {
                ref readonly var startNode = ref editor.Nodes.Get(startPin.ParentNodeIndex);
                ref readonly var endNode   = ref editor.Nodes.Get(endPin.ParentNodeIndex);
                if (startPin.Type is Internal.AttributeType.Output)
                {
                    *startedAtNode = startNode.Id;
                    *endedAtNode   = endNode.Id;
                }
                else
                {
                    *startedAtNode = endNode.Id;
                    *endedAtNode   = startNode.Id;
                }
            }

            if (createdFromSnap is not null)
                *createdFromSnap = editor.ClickInteraction.Type is Internal.ClickInteractionType.LinkCreation;
            return true;
        }

        public static ImBool IsLinkDestroyed(LinkId* destroyed)
        {
            ref readonly var context = ref *Context;
            Debug.Assert(context.CurrentScope is Internal.Scope.None);
            if (!context.DeletedLinkIndex.IsValid)
                return false;

            if (destroyed is not null)
                *destroyed = Editor->Links.Get(context.DeletedLinkIndex).Id;
            return true;
        }
    }
}
