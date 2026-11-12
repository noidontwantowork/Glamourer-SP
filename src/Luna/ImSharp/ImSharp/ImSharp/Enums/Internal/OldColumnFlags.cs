namespace ImSharp.Internal;

/// <summary> Flags used by the outdated <seealso cref="Native.Methods.Column"/> methods. </summary>
[Flags]
public enum OldColumnFlags : uint
{
    /// <summary> No specific state. </summary>
    None = 0,

    /// <summary> Disable column dividers. </summary>
    NoBorder = 1 << 0,

    /// <summary> Disable resizing columns when dragging the dividers. </summary>
    NoResize = 1 << 1,

    /// <summary> Disable preservation of column widths when adjusting them. </summary>
    NoPreserveWidths = 1 << 2,

    /// <summary> Disable forcing columns to fit within the window. </summary>
    NoForceWithinWindow = 1 << 3,

    /// <summary> Restore old behavior of extending the parent windows content size. </summary>
    GrowParentContentsSize = 1 << 4,
}
