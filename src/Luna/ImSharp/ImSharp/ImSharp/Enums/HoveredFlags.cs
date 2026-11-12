namespace ImSharp;

/// <summary> Flags that govern the behavior of IsHovered checks. </summary>
[Flags]
public enum HoveredFlags : uint
{
    /// <summary> Returns true if hovering directly over the unobstructed item and window. </summary>
    None = 0,

    /// <summary> Returns true if any child of the window is hovered. </summary>
    /// <remarks> Only used by <seealso cref="Im.IsWindowHovered"/>. </remarks>
    ChildWindows = 1 << 0,

    /// <summary> Hit-test from the root window (the top-most parent of the current hierarchy.) </summary>
    /// <remarks> Only used by <seealso cref="Im.IsWindowHovered"/>. </remarks>
    RootWindow = 1 << 1,

    /// <summary> Returns true if any window is hovered. </summary>
    /// <remarks> Only used by <seealso cref="Im.IsWindowHovered"/>. </remarks>
    AnyWindow = 1 << 2,

    /// <summary> Returns true even if a popup window is blocking access to this item. </summary>
    AllowWhenBlockedByPopup = 1 << 3,

    /// <summary> Returns true even if an active item is blocking access to this item. </summary>
    AllowWhenBlockedByActiveItem = 1 << 5,

    /// <summary> Returns true even if the item is overlapped by another hoverable item. </summary>
    /// <remarks> Only used by <seealso cref="Im.IsItemHovered"/>. </remarks>
    AllowWhenOverlapped = 1 << 6,

    /// <summary> Returns true even if the item is disabled. </summary>
    /// <remarks> Only used by <seealso cref="Im.IsItemHovered"/>. </remarks>
    AllowWhenDisabled = 1 << 7,

    /// <summary> Disables using the gamepad or keyboard navigation state when active and always queries the mouse. </summary>
    /// <remarks> Only used by <seealso cref="Im.IsItemHovered"/>. </remarks>
    NoNavOverride = 1 << 8,

    /// <summary> Do not consider the popup hierarchy, i.e. do not treat the popup emitter as a parent. </summary>
    /// <remarks> Only used by <seealso cref="Im.IsWindowHovered"/>. </remarks>
    NoPopupHierarchy = 1 << 9,

    /// <summary> Consider the docking hierarchy, i.e. treat the dockspace host as a parent. </summary>
    /// <remarks> Only used by <seealso cref="Im.IsWindowHovered"/>. </remarks>
    DockHierarchy = 1 << 10,

    /// <summary> Hit-test from the root and any child windows. </summary>
    RootAndChildWindows = RootWindow | ChildWindows,

    /// <summary> Hit-test based on the bounding rectangle of the item regardless of other state. </summary>
    RectOnly = AllowWhenOverlapped | AllowWhenBlockedByActiveItem | AllowWhenBlockedByPopup,
}
