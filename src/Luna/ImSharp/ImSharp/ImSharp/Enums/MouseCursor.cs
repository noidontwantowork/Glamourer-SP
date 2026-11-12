namespace ImSharp;

/// <summary> Types of mouse cursors potentially supported by the backend.  </summary>
public enum MouseCursor : int
{
    /// <summary> No known cursor. </summary>
    None = -1,

    /// <summary> The default arrow cursor.  </summary>
    Arrow = 0,

    /// <summary> The text input cursor. </summary>
    TextInput = 1,

    /// <summary> An omnidirectional resize cursor. </summary>
    /// <remarks> Not used by ImGui. </remarks>
    ResizeAll = 2,

    /// <summary> A vertical resize cursor.  </summary>
    ResizeVertical = 3,

    /// <summary> A horizontal resize cursor.  </summary>
    ResizeHorizontal = 4,

    /// <summary> A diagonal resize cursor in inclining direction (i.e. bottom-left to top-right).  </summary>
    ResizeInclining = 5,

    /// <summary> A diagonal resize cursor in declining direction (i.e. top-left to bottom-right).  </summary>
    ResizeDeclining = 6,

    /// <summary> A hand cursor. </summary>
    /// <remarks> Unused by ImGui. </remarks>
    Hand = 7,

    /// <summary> A cursor signifying that an interaction is disallowed. </summary>
    NotAllowed = 8,
}
