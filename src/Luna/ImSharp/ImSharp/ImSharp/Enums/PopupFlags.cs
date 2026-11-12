namespace ImSharp;

/// <summary> Flags used when opening popups. </summary>
[Flags]
public enum PopupFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Do not open this popup if there is already a popup open at the same level. </summary>
    NoOpenOverExistingPopup = 1 << 5,
}

/// <summary> Flags used to control the behavior of context popups. </summary>
[Flags]
public enum PopupContextFlags : uint
{
    /// <inheritdoc cref="PopupFlags.None"/>
    None = PopupFlags.None,

    /// <summary> Open the context on release of the left mouse button. </summary>
    MouseButtonLeft = 0,

    /// <summary> Open the context on release of the right mouse button. </summary>
    MouseButtonRight = 1 << 0,

    /// <summary> Open the context on release of the middle mouse button. </summary>
    MouseButtonMiddle = 1 << 1,

    /// <inheritdoc cref="PopupFlags.NoOpenOverExistingPopup"/>
    NoOpenOverExistingPopup = PopupFlags.NoOpenOverExistingPopup,

    /// <summary> Do not return true when hovering items, only on empty space. </summary>
    NoOpenOverItems = 1 << 6,

    /// <summary> Mask for the available mouse buttons. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    MouseButtonMask = 0x1F,

    /// <summary> The default mouse button for opening context menus. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    MouseButtonDefault = MouseButtonRight,
}

/// <summary> Flags used when querying popup state. </summary>
[Flags]
public enum PopupQueryFlags : uint
{
    /// <inheritdoc cref="PopupFlags.None"/>
    None = PopupFlags.None,

    /// <summary> Ignore the passed <seealso cref="ImGuiId"/> parameter and check if any popup is open on this level. </summary>
    AnyPopupId = 1 << 7,

    /// <summary> Search for popups on any level of the popup stack. </summary>
    AnyPopupLevel = 1 << 8,

    /// <summary> Check if any popup is open at all. </summary>
    AnyPopup = AnyPopupId | AnyPopupLevel,
}
