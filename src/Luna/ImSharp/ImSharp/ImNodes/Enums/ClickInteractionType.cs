namespace ImSharp.ImNodes;

public static partial class Internal
{
    /// <summary> Different click interaction types for interactivity. </summary>
    public enum ClickInteractionType : uint
    {
        /// <summary> A node was clicked. </summary>
        Node,

        /// <summary> A link was clicked. </summary>
        Link,

        /// <summary> A link is being created. </summary>
        LinkCreation,

        /// <summary> The user is panning the editor. </summary>
        Panning,

        /// <summary> The user is dragging a selection box. </summary>
        BoxSelection,

        /// <summary> An independent ImGui item was clicked. </summary>
        ImGuiItem,

        /// <summary> Nothing is happening. </summary>
        None,
    }
}
