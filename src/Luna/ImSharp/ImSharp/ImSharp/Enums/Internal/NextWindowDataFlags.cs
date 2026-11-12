namespace ImSharp.Internal;

/// <summary> Flags that describe settings to be applied to the next window. </summary>
[Flags]
public enum NextWindowDataFlags : uint
{
    /// <summary> No specific settings to be applied. </summary>
    None = 0,

    /// <summary> A new position is to be applied. </summary>
    HasPosition = 1 << 0,

    /// <summary> A new size is to be applied. </summary>
    HasSize = 1 << 1,

    /// <summary> A new content size is to be applied. </summary>
    HasContentSize = 1 << 2,

    /// <summary> The window is to change its collapsed state. </summary>
    HasCollapsed = 1 << 3,

    /// <summary> New size constraints are to be applied. </summary>
    HasSizeConstraint = 1 << 4,

    /// <summary> The window is to change its focus state. </summary>
    HasFocus = 1 << 5,

    /// <summary> A new background alpha is to be applied. </summary>
    HasBgAlpha = 1 << 6,

    /// <summary> A new scroll position is to be applied. </summary>
    HasScroll = 1 << 7,

    /// <summary> The windows viewport is to be changed. </summary>
    HasViewport = 1 << 8,

    /// <summary> The windows docking position is to be changed. </summary>
    HasDock = 1 << 9,

    /// <summary> A new window class is to be applied. </summary>
    HasWindowClass = 1 << 10,
}
