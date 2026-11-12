namespace ImSharp;

/// <summary> Control the behaviour and appearance of an ImGui window. </summary>
[Flags]
public enum WindowFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Disable the title bar of the window. </summary>
    NoTitleBar = 1 << 0,

    /// <summary> Prevent the user being able to resize the window with the corner grips. </summary>
    NoResize = 1 << 1,

    /// <summary> Prevent the user from moving the window by dragging it. </summary>
    NoMove = 1 << 2,

    /// <summary> Disable scroll bars in the window. It can still be scrolled via other means. </summary>
    NoScrollbar = 1 << 3,

    /// <summary> Disable the user scrolling the window vertically with the mouse wheel. </summary>
    NoScrollWithMouse = 1 << 4,

    /// <summary> Disable collapsing the window by double-clicking it. </summary>
    NoCollapse = 1 << 5,

    /// <summary> Resize the window to its content every frame. </summary>
    AlwaysAutoResize = 1 << 6,

    /// <summary> Disable drawing the background colors and outer borders for this window. </summary>
    NoBackground = 1 << 7,

    /// <summary> Never load or save the settings for this window in an .ini file. </summary>
    NoSavedSettings = 1 << 8,

    /// <summary> Disable catching the mouse state for this window. </summary>
    NoMouseInputs = 1 << 9,

    /// <summary> Add a menu bar to this window. </summary>
    MenuBar = 1 << 10,

    /// <summary> Allow a horizontal scroll bar to appear (this is disabled by default). </summary>
    HorizontalScrollbar = 1 << 11,

    /// <summary> This window does not take focus when it switches from a hidden to a visible state. </summary>
    NoFocusOnAppearing = 1 << 12,

    /// <summary> This window is not put in front of other windows when it is focused. </summary>
    NoBringToFrontOnFocus = 1 << 13,

    /// <summary> Always show a vertical scrollbar, even if not necessary. </summary>
    AlwaysVerticalScrollbar = 1 << 14,

    /// <summary> Always show a horizontal scrollbar, even if not necessary. </summary>
    AlwaysHorizontalScrollbar = 1 << 15,

    /// <summary> Always use window padding. </summary>
    AlwaysUseWindowPadding = 1 << 16,

    /// <summary> Disable gamepad and keyboard navigation within this window. </summary>
    NoNavInputs = 1 << 18,

    /// <summary> Disable focusing toward this window with gamepad and keyboard navigation. </summary>
    NoNavFocus = 1 << 19,

    /// <summary> Display a dot next to the title. </summary>
    UnsavedDocument = 1 << 20,

    /// <summary> Disable docking for this window. </summary>
    NoDocking = 1 << 21,

    /// <summary> Disable all gamepad and keyboard navigation for this window. </summary>
    NoNav = NoNavInputs | NoNavFocus,

    /// <summary> Disable titlebar, collapsing, resizing and scrollbars for this window. </summary>
    NoDecoration = NoTitleBar | NoResize | NoScrollbar | NoCollapse,

    /// <summary> Disable all inputs for this window. </summary>
    NoInputs = NoMouseInputs | NoNavInputs | NoNavFocus,

    /// <summary> On child windows, allow gamepad and keyboard navigation to cross over the parent border or between siblings. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NavFlattened = 1 << 23,

    /// <summary> Used by <seealso cref="Im.Child.Begin(Utf8LabelHandler,Vector2,bool,WindowFlags)"/>. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    ChildWindow = 1 << 24,

    /// <summary> Used by <seealso cref="Im.Tooltip.Begin"/>. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Tooltip = 1 << 25,

    /// <summary> Used by <seealso cref="Im.Popup.Begin(Utf8LabelHandler,WindowFlags)"/>. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Popup = 1 << 26,

    /// <summary> Used by <seealso cref="Im.Popup.BeginModal(Utf8LabelHandler,WindowFlags)"/>. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Modal = 1 << 27,

    /// <summary> Used by <seealso cref="Im.Menu.Begin"/>. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    ChildMenu = 1 << 28,

    /// <summary> Used by <seealso cref="Im.Window.Begin(Utf8LabelHandler,WindowFlags)"/> and <seealso cref="NewFrame"/>. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    DockNodeHost = 1 << 29,
}
