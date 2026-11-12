namespace ImSharp.ImNodes;

public static partial class Internal
{
    /// <summary> Descriptors of the current UI state. </summary>
    [Flags]
    public enum UiState : uint
    {
        /// <summary> Nothing specific is happening. </summary>
        None = 1 << 0,

        /// <summary> Dragging of a link was started. </summary>
        LinkStarted = 1 << 1,

        /// <summary> A dragged link was let go of. </summary>
        LinkDropped = 1 << 2,

        /// <summary> A dragged link was finalized. </summary>
        LinkCreated = 1 << 3,

        // CUSTOM
        /// <summary> Disable selection in the editor. </summary>
        NoSelection = 1 << 30,

        /// <summary> Disable hover-interactions in the editor. </summary>
        NoHovering = 1u << 31,
    }
}
