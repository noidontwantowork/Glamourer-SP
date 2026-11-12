namespace ImSharp;

/// <summary> Style Colors used internally by ImGui. </summary>
public enum ImGuiColor : uint
{
    /// <summary> Default text color. </summary>
    Text = 0,

    /// <summary> Default text color for disabled text. </summary>
    TextDisabled,

    /// <summary> Background color of normal windows. </summary>
    WindowBackground,

    /// <summary> Background color of child windows. </summary>
    ChildBackground,

    /// <summary> Background color of popups, menus and tooltip windows. </summary>
    PopupBackground,

    /// <summary> Color of borders around frames. </summary>
    Border,

    /// <summary> Color of border shadows. </summary>
    BorderShadow,

    /// <summary> Background color of checkboxes, radio buttons, plots, sliders and text inputs. </summary>
    FrameBackground,

    /// <summary> Background color of checkboxes, radio buttons, plots, sliders and text inputs while they are hovered. </summary>
    FrameBackgroundHovered,

    /// <summary> Background color of checkboxes, radio buttons, plots, sliders and text inputs while they are active. </summary>
    FrameBackgroundActive,

    /// <summary> Background color of unfocused but expanded title bars. </summary>
    TitleBackground,

    /// <summary> Background color of focused title bars. </summary>
    TitleBackgroundActive,

    /// <summary> Background color of unfocused and collapsed title bars. </summary>
    TitleBackgroundCollapsed,

    /// <summary> Background color of the menu bar. </summary>
    MenuBarBg,

    /// <summary> Background color of the scroll bar. </summary>
    ScrollbarBg,

    /// <summary> Color of the draggable object of a scroll bar. </summary>
    ScrollbarGrab,

    /// <summary> Color of the draggable object of a scroll bar when it is hovered. </summary>
    ScrollbarGrabHovered,

    /// <summary> Color of the draggable object of a scroll bar when it is being dragged. </summary>
    ScrollbarGrabActive,

    /// <summary> Color of the tick or circle used in checkboxes. </summary>
    CheckMark,

    /// <summary> Color of the draggable object of a slider. </summary>
    SliderGrab,

    /// <summary> Color of the draggable object of a slider when it is being dragged. </summary>
    SliderGrabActive,

    /// <summary> Background color of a button. </summary>
    Button,

    /// <summary> Background color of a button when it is hovered. </summary>
    ButtonHovered,

    /// <summary> Background color of a button when it is being pressed or held down. </summary>
    ButtonActive,

    /// <summary> Background color of a collapsing header, Selectable, framed TreeNodes or MenuItems. </summary>
    Header,

    /// <summary> Background color of a collapsing header, Selectable, framed TreeNodes or MenuItems when it is hovered. </summary>
    HeaderHovered,

    /// <summary> Background color of a collapsing header, Selectable, framed TreeNodes or MenuItems when it is being active (pressed or selected). </summary>
    HeaderActive,

    /// <summary> Color of horizontal separator lines. </summary>
    Separator,

    /// <summary> Color of horizontal separator lines when they are being hovered. </summary>
    SeparatorHovered,

    /// <summary> Color of horizontal separator lines when they are being dragged. </summary>
    SeparatorActive,

    /// <summary> Color of the grip bottom-left or bottom-right corner used for resizing windows. </summary>
    ResizeGrip,

    /// <summary> Color of the grip bottom-left or bottom-right corner used for resizing windows when it is hovered. </summary>
    ResizeGripHovered,

    /// <summary> Color of the grip bottom-left or bottom-right corner used for resizing windows when it is being dragged. </summary>
    ResizeGripActive,

    /// <summary> Background color of a tab item when the tab bar is focused and the tab is not currently selected. </summary>
    Tab,

    /// <summary> Background color of a tab item when it is hovered. </summary>
    TabHovered,

    /// <summary> Background color of a tab item when the tab bar is focused and the tab is currently selected. </summary>
    TabSelected,

    /// <summary> Background color of a tab item when the tab bar is unfocused and the tab is not currently selected. </summary>
    TabDimmed,

    /// <summary> Background color of a tab item when the tab bar is unfocused and the tab is currently selected. </summary>
    TabDimmedSelected,

    /// <summary> Color of the highlight preview when dragging a window into docking spaces. </summary>
    DockingPreview,

    /// <summary> Background color of empty docking spaces. </summary>
    DockingEmptyBackground,

    /// <summary> Color of plot lines. </summary>
    PlotLines,

    /// <summary> Color of plot lines when they are hovered. </summary>
    PlotLinesHovered,

    /// <summary> Color of histogram data. </summary>
    PlotHistogram,

    /// <summary> Color of histogram data when it is hovered. </summary>
    PlotHistogramHovered,

    /// <summary> Background color for table headers. </summary>
    TableHeaderBackground,

    /// <summary> Color for outer and header borders of tables. </summary>
    TableBorderStrong,

    /// <summary> Color for inner borders of tables. </summary>
    TableBorderLight,

    /// <summary> Default background color for table rows. If alternating row background colors are enabled, this is used for even rows. </summary>
    TableRowBackground,

    /// <summary> Default background color for odd table rows only. If alternating row background colors are enabled, this is used for odd rows. </summary>
    TableRowBackgroundAlt,

    /// <summary> Background color for selected text. </summary>
    TextSelectedBackground,

    /// <summary> Color for the rectangle highlighting drop targets for drag & drop. </summary>
    DragDropTarget,

    /// <summary> Color for the current highlighted item for gamepad or keyboard navigation. </summary>
    NavHighlight,

    /// <summary> Color used by the highlighted window when using Ctrl + Tab. </summary>
    NavWindowingHighlight,

    /// <summary> Tint for the entire screen behind the Ctrl + Tab Window list. </summary>
    NavWindowingDimBackground,

    /// <summary> Tint for the entire screen behind a modal window. </summary>
    ModalWindowDimBackground,
}

public static class ImGuiColorExtensions
{
    public const int ColorCount = (int)ImGuiColor.ModalWindowDimBackground + 1;

    extension(ImGuiColor type)
    {
        /// <inheritdoc cref="Im.ColorDisposable.Push(ImGuiColor,Rgba32,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Im.ColorDisposable Push(Rgba32 color, bool condition)
            => new Im.ColorDisposable().Push(type, color, condition);

        /// <inheritdoc cref="Im.ColorDisposable.Push(ImGuiColor,ColorParameter)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Im.ColorDisposable Push(ColorParameter color)
            => new Im.ColorDisposable().Push(type, color);

        /// <inheritdoc cref="Im.ColorDisposable.Push(ImGuiColor,Vector4,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Im.ColorDisposable Push(Vector4 color, bool condition)
            => new Im.ColorDisposable().Push(type, color, condition);

        /// <inheritdoc cref="Im.ColorDisposable.Push(ImGuiColor,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Im.ColorDisposable Push(Rgba32 color)
            => new Im.ColorDisposable().Push(type, color);

        /// <inheritdoc cref="Im.ColorDisposable.Push(ImGuiColor,Vector4)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Im.ColorDisposable Push(Vector4 color)
            => new Im.ColorDisposable().Push(type, color);

        /// <inheritdoc cref="Im.ColorDisposable.PushDefault(ImGuiColor)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Im.ColorDisposable PushDefault()
            => new Im.ColorDisposable().PushDefault(type);

        /// <inheritdoc cref="Im.Color.Get"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Rgba32 Get(float alphaMultiplier = 1.0f)
            => Im.Color.Get(type, alphaMultiplier);

        /// <summary> Get the number of available ImGui colors. </summary>
        public static int Count
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => ColorCount;
        }
    }
}
