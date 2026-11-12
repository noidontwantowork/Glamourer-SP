namespace ImSharp;

/// <summary> Flags that control the behaviour of a tab item. </summary>
[Flags]
public enum TabItemFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Display a dot next to the title of this tab. </summary>
    UnsavedDocument = 1 << 0,

    /// <summary> Make this tab selected when it is drawn. </summary>
    SetSelected = 1 << 1,

    /// <summary> Disable the option of closing this tab with the middle mouse button. </summary>
    /// <remarks> Requires this tab to be opened with an open in-out parameter. </remarks>
    NoCloseWithMiddleMouseButton = 1 << 2,

    /// <summary> This tab item does not push its label as an ID. </summary>
    NoPushId = 1 << 3,

    /// <summary> Do not display a tooltip when hovering this tab item. </summary>
    NoTooltip = 1 << 4,

    /// <summary> Disable reordering this tab manually or have another tab cross over this tab (which would reorder it). </summary>
    NoReorder = 1 << 5,

    /// <summary> Enforce placing this tab item on the left of the tab bar. </summary>
    Leading = 1 << 6,

    /// <summary> Enforce placing this tab item on the right of the tab bar. </summary>
    Trailing = 1 << 7,
}
