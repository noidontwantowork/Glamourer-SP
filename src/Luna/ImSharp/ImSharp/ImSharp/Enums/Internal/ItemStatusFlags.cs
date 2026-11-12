namespace ImSharp.Internal;

/// <summary> Flags containing the state of an item. </summary>
[Flags]
public enum ItemStatusFlags : uint
{
    /// <summary> No specific status. </summary>
    None = 0,

    /// <summary> The current mouse cursor position is within the item rectangle. </summary>
    /// <remarks> This does not necessarily mean that the item is hovered. </remarks>
    HoveredRect = 1 << 0,

    /// <summary> Has a valid DisplayRect. </summary>
    HasDisplayRect = 1 << 1,

    /// <summary> The underlying value of the item was edited in the current frame. </summary>
    Edited = 1 << 2,

    /// <summary> Set when selectables or tree nodes report toggling a selection. </summary>
    ToggledSelection = 1 << 3,

    /// <summary> Set when tree nodes report toggling their open state. </summary>
    ToggledOpen = 1 << 4,

    /// <summary> Set if the widget or group provide data for <seealso cref="Deactivated"/>. </summary>
    HasDeactivated = 1 << 5,

    /// <summary> Only valid if <seealso cref="HasDeactivated"/> is set. </summary>
    Deactivated = 1 << 6,

    /// <summary> Override the hovered window test to allow cross-window hover testing. </summary>
    HoveredWindow = 1 << 7,

    /// <summary> Set when a focusable item was focused by tabbing in this frame. </summary>
    FocusedByTabbing = 1 << 8,
}
