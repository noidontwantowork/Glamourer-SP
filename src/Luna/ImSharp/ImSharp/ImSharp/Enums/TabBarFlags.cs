namespace ImSharp;

/// <summary> Flags controlling the behaviour of a tab bar. </summary>
[Flags]
public enum TabBarFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Tabs can be dragged manually to re-order them, new tabs are appended at the end of the list. </summary>
    Reorderable = 1 << 0,

    /// <summary> New tabs are automatically selected when they appear. </summary>
    AutoSelectNewTabs = 1 << 1,

    /// <summary> Disable the buttons to open the tab list popup. </summary>
    TabListPopupButton = 1 << 2,

    /// <summary> Disable the option of closing tabs (that can be closed) with the middle mouse button. </summary>
    NoCloseWithMiddleMouseButton = 1 << 3,

    /// <summary> Disable scrolling buttons available when <seealso cref="FittingPolicyScroll"/> is set. </summary>
    NoTabListScrollingButtons = 1 << 4,

    /// <summary> Disable tooltips when hovering a tab. </summary>
    NoTooltip = 1 << 5,

    /// <summary> Resize tabs if they do not fit on the line. </summary>
    FittingPolicyResizeDown = 1 << 6,

    /// <summary> Add scroll buttons when tabs do not fit on the line. </summary>
    FittingPolicyScroll = 1 << 7,

    /// <summary> The mask for different fitting policies. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    FittingPolicyMask = FittingPolicyResizeDown | FittingPolicyScroll,

    /// <summary> The default fitting policy. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    FittingPolicyDefault = FittingPolicyResizeDown,
}
