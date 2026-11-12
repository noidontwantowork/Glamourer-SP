namespace ImSharp;

/// <summary> Flags that control the behaviour of buttons. </summary>
[Flags]
public enum ButtonFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> React to clicks with the left mouse button (Default). </summary>
    MouseButtonLeft = 1 << 0,

    /// <summary> React to clicks with the right mouse button. </summary>
    MouseButtonRight = 1 << 1,

    /// <summary> React to clicks with the middle mouse button. </summary>
    MouseButtonMiddle = 1 << 2,

    /// <summary> Returns true on mouse down events. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    PressedOnClick = 1 << 4,

    /// <summary> Returns true when clicking down and releasing on the same item. Default. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    PressedOnClickRelease = 1 << 5,

    /// <summary> Returns true when clicking down on the item and then releasing anywhere. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    PressedOnClickReleaseAnywhere = 1 << 6,

    /// <summary> Returns true when releasing a click on the item. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    PressedOnRelease = 1 << 7,

    /// <summary> Returns true only on a double click. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    PressedOnDoubleClick = 1 << 8,

    /// <summary> Returns true when hovered for a certain time while drag and dropping another item (e.g. tree nodes and collapsing headers). </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    PressedOnDragDropHold = 1 << 9,

    /// <summary> Repeatedly returns true while holding a click down on it, based on <seealso cref="Native.Io.KeyRepeatDelay"/> and <seealso cref="Native.Io.KeyRepeatRate"/>. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Repeat = 1 << 10,

    /// <summary> Allow interactions even if child windows are overlapping. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    FlattenChildren = 1 << 11,

    /// <summary> Require the HoveredId of the prior frame to match or be null before being usable. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    AllowItemOverlap = 1 << 12,

    /// <summary> Disables automatically closing a parent popup on press. This is unused. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    DontClosePopups = 1 << 13,

    /// <summary> Vertically align the button to match the text baseline. Only used by ButtonEx, but should not be used. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    AlignTextBaseLine = 1 << 15,

    /// <summary> Disable any mouse interaction if a key modifier is held. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoKeyModifiers = 1 << 16,

    /// <summary> Do not update the Active ID while holding the mouse down. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoHoldingActiveId = 1 << 17,

    /// <summary> Do not override navigation focus when activated. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoNavFocus = 1 << 18,

    /// <summary> Do not report as hovered while navigation focus is on this item. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoHoveredOnFocus = 1 << 19,

    /// <summary> The default mouse button to click buttons with. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Default = MouseButtonLeft,

    /// <summary> The mask for all pressed on behaviours for a button. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    PressedOnMask = PressedOnClick
      | PressedOnClickRelease
      | PressedOnClickReleaseAnywhere
      | PressedOnDoubleClick
      | PressedOnDragDropHold
      | PressedOnRelease,

    /// <summary> The default pressed on behavior. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    PressedOnDefault = PressedOnClickRelease,
}
