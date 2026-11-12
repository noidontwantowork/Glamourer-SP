namespace ImSharp;

/// <summary> Style Variables used internally by ImGui. </summary>
public enum ImStyle : uint
{
    /// <summary> The overall alpha transparency in [0,1]. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    Alpha,

    /// <summary> The padding between a window and its first or last items in pixels. </summary>
    /// <remarks> <seealso cref="Vector2"/> </remarks>
    WindowPadding,

    /// <summary> The rounding on the corners of a window in pixels. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    WindowRounding,

    /// <summary> The thickness of the borders of a window in pixels. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    WindowBorderThickness,

    /// <summary> The minimum size of a window in pixels. </summary>
    /// <remarks> <seealso cref="Vector2"/> </remarks>
    MinimumWindowSize,

    /// <summary> The alignment of the title in window headers in [0,1]. </summary>
    /// <remarks> <seealso cref="Vector2"/> </remarks>
    WindowTitleAlignment,

    /// <summary> The rounding on the corners of a child window in pixels. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    ChildRounding,

    /// <summary> The thickness of the borders of a child window in pixels. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    ChildBorderThickness,

    /// <summary> The rounding on the corners of a popup window in pixels. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    PopupRounding,

    /// <summary> The thickness of the borders of a popup window in pixels. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    PopupBorderThickness,

    /// <summary> The additional size of a frame around an item in pixels. </summary>
    /// <remarks> <seealso cref="Vector2"/> </remarks>
    FramePadding,

    /// <summary> The rounding on the corners of a frame in pixels. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    FrameRounding,

    /// <summary> The thickness of the borders of a frame in pixels. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    FrameBorderThickness,

    /// <summary> The spacing kept between subsequent items on the same line for X and the next line for Y. </summary>
    /// <remarks> <seealso cref="Vector2"/> </remarks>
    ItemSpacing,

    /// <summary> The spacing kept between subsequent items that belong together (like labels) on the same line for X and the next line for Y. </summary>
    /// <remarks> <seealso cref="Vector2"/> </remarks>
    ItemInnerSpacing,

    /// <summary> The spacing a single indent causes. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    IndentSpacing,

    /// <summary> The additional size of a cell in a table in pixels. </summary>
    /// <remarks> <seealso cref="Vector2"/> </remarks>
    CellPadding,

    /// <summary> The width of vertical and height of horizontal scroll bars in pixels. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    ScrollbarThickness,

    /// <summary> The rounding of the corners of a scroll bar in pixels. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    ScrollbarRounding,

    /// <summary> The minimum size of the draggable object in a scrollbar in pixels. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    MinimumGrabSize,

    /// <summary> The rounding of the corners of the draggable object in a scrollbar in pixels. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    GrabRounding,

    /// <summary> The rounding of the upper corners of a tab item in pixels. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    TabRounding,

    /// <summary> The alignment of text in buttons in [0,1]. </summary>
    /// <remarks> <seealso cref="Vector2"/> </remarks>
    ButtonTextAlign,

    /// <summary> The alignment of text in selectables in [0,1]. </summary>
    /// <remarks> <seealso cref="Vector2"/> </remarks>
    SelectableTextAlign,

    /// <summary> The overall alpha transparency of disabled items in [0,1]. </summary>
    /// <remarks> <seealso cref="float"/> </remarks>
    DisabledAlpha,
}

/// <summary> Style Variables that use float values used internally by ImGui. </summary>
public enum ImStyleSingle : uint
{
    /// <inheritdoc cref="ImStyle.Alpha"/>
    Alpha = ImStyle.Alpha,

    /// <inheritdoc cref="ImStyle.DisabledAlpha"/>
    DisabledAlpha = ImStyle.DisabledAlpha,

    /// <inheritdoc cref="ImStyle.WindowRounding"/>
    WindowRounding = ImStyle.WindowRounding,

    /// <inheritdoc cref="ImStyle.WindowBorderThickness"/>
    WindowBorderThickness = ImStyle.WindowBorderThickness,

    /// <inheritdoc cref="ImStyle.ChildRounding"/>
    ChildRounding = ImStyle.ChildRounding,

    /// <inheritdoc cref="ImStyle.ChildBorderThickness"/>
    ChildBorderThickness = ImStyle.ChildBorderThickness,

    /// <inheritdoc cref="ImStyle.PopupRounding"/>
    PopupRounding = ImStyle.PopupRounding,

    /// <inheritdoc cref="ImStyle.PopupBorderThickness"/>
    PopupBorderThickness = ImStyle.PopupBorderThickness,

    /// <inheritdoc cref="ImStyle.FrameRounding"/>
    FrameRounding = ImStyle.FrameRounding,

    /// <inheritdoc cref="ImStyle.FrameBorderThickness"/>
    FrameBorderThickness = ImStyle.FrameBorderThickness,

    /// <inheritdoc cref="ImStyle.IndentSpacing"/>
    IndentSpacing = ImStyle.IndentSpacing,

    /// <inheritdoc cref="ImStyle.ScrollbarThickness"/>
    ScrollbarSize = ImStyle.ScrollbarThickness,

    /// <inheritdoc cref="ImStyle.ScrollbarRounding"/>
    ScrollbarRounding = ImStyle.ScrollbarRounding,

    /// <inheritdoc cref="ImStyle.MinimumGrabSize"/>
    MinimumGrabSize = ImStyle.MinimumGrabSize,

    /// <inheritdoc cref="ImStyle.GrabRounding"/>
    GrabRounding = ImStyle.GrabRounding,

    /// <inheritdoc cref="ImStyle.TabRounding"/>
    TabRounding = ImStyle.TabRounding,
}

/// <summary> Style Variables that use pairs of float values used internally by ImGui. </summary>
public enum ImStyleDouble : uint
{
    /// <inheritdoc cref="ImStyle.WindowPadding"/>
    WindowPadding = ImStyle.WindowPadding,

    /// <inheritdoc cref="ImStyle.MinimumWindowSize"/>
    MinimumWindowSize = ImStyle.MinimumWindowSize,

    /// <inheritdoc cref="ImStyle.WindowTitleAlignment"/>
    WindowTitleAlignment = ImStyle.WindowTitleAlignment,

    /// <inheritdoc cref="ImStyle.FramePadding"/>
    FramePadding = ImStyle.FramePadding,

    /// <inheritdoc cref="ImStyle.ItemSpacing"/>
    ItemSpacing = ImStyle.ItemSpacing,

    /// <inheritdoc cref="ImStyle.ItemInnerSpacing"/>
    ItemInnerSpacing = ImStyle.ItemInnerSpacing,

    /// <inheritdoc cref="ImStyle.CellPadding"/>
    CellPadding = ImStyle.CellPadding,

    /// <inheritdoc cref="ImStyle.ButtonTextAlign"/>
    ButtonTextAlign = ImStyle.ButtonTextAlign,

    /// <inheritdoc cref="ImStyle.SelectableTextAlign"/>
    SelectableTextAlign = ImStyle.SelectableTextAlign,
}

/// <summary> Different types of widget borders to push thickness to. </summary>
public enum ImStyleBorder : uint
{
    /// <inheritdoc cref="ImStyle.WindowBorderThickness"/>
    Window = ImStyle.WindowBorderThickness,

    /// <inheritdoc cref="ImStyle.ChildBorderThickness"/>
    Child = ImStyle.ChildBorderThickness,

    /// <inheritdoc cref="ImStyle.PopupBorderThickness"/>
    Popup = ImStyle.PopupBorderThickness,

    /// <inheritdoc cref="ImStyle.FrameBorderThickness"/>
    Frame = ImStyle.FrameBorderThickness,
}

public static class ImGuiStyleExtensions
{
    /// <summary> A set of bools whether a style flag is for a single float or not. </summary>
    private static readonly bool[] ImStyleSingle =
        ImStyle.Values.Select(v => Enum.IsDefined((ImStyleSingle)v)).ToArray();

    /// <summary> Get whether this style variable uses a single float (true) or a ImVec2 (false). </summary>
    public static bool Single(this ImStyle style)
        => ImStyleSingle[(int)style];

    /// <inheritdoc cref="Im.StyleDisposable.Push(ImStyleSingle,float,bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.StyleDisposable Push(this ImStyleSingle type, float value, bool condition)
        => new Im.StyleDisposable().Push(type, value, condition);

    /// <inheritdoc cref="Im.StyleDisposable.Push(ImStyleSingle,float)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.StyleDisposable Push(this ImStyleSingle type, float value)
        => new Im.StyleDisposable().Push(type, value);

    /// <inheritdoc cref="Im.StyleDisposable.Push(ImStyleDouble,Vector2,bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.StyleDisposable Push(this ImStyleDouble type, Vector2 value, bool condition)
        => new Im.StyleDisposable().Push(type, value, condition);

    /// <inheritdoc cref="Im.StyleDisposable.Push(ImStyleDouble,Vector2)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.StyleDisposable Push(this ImStyleDouble type, Vector2 value)
        => new Im.StyleDisposable().Push(type, value);

    /// <inheritdoc cref="Im.StyleDisposable.PushX(ImStyleDouble,float,bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.StyleDisposable PushX(this ImStyleDouble type, float value, bool condition)
        => new Im.StyleDisposable().PushX(type, value, condition);

    /// <inheritdoc cref="Im.StyleDisposable.PushX(ImStyleDouble,float)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.StyleDisposable PushX(this ImStyleDouble type, float value)
        => new Im.StyleDisposable().PushX(type, value);

    /// <inheritdoc cref="Im.StyleDisposable.PushY(ImStyleDouble,float,bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.StyleDisposable PushY(this ImStyleDouble type, float value, bool condition)
        => new Im.StyleDisposable().PushY(type, value, condition);

    /// <inheritdoc cref="Im.StyleDisposable.PushY(ImStyleDouble,float)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.StyleDisposable PushY(this ImStyleDouble type, float value)
        => new Im.StyleDisposable().PushY(type, value);

    /// <inheritdoc cref="Im.ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.ColorParameter,float,bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.ColorStyleDisposable Push(this ImStyleBorder borderType, ColorParameter color, float thickness, bool condition)
        => new Im.ColorStyleDisposable().Push(borderType, color, thickness, condition);

    /// <inheritdoc cref="Im.ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.ColorParameter,float)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.ColorStyleDisposable Push(this ImStyleBorder borderType, ColorParameter color, float thickness)
        => new Im.ColorStyleDisposable().Push(borderType, color, thickness);

    /// <inheritdoc cref="Im.ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.ColorParameter)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.ColorStyleDisposable Push(this ImStyleBorder borderType, ColorParameter color)
        => new Im.ColorStyleDisposable().Push(borderType, color);

    /// <inheritdoc cref="Im.ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.Rgba32,float,bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.ColorStyleDisposable Push(this ImStyleBorder borderType, Rgba32 color, float thickness, bool condition)
        => new Im.ColorStyleDisposable().Push(borderType, color, thickness, condition);

    /// <inheritdoc cref="Im.ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.Rgba32,float)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.ColorStyleDisposable Push(this ImStyleBorder borderType, Rgba32 color, float thickness)
        => new Im.ColorStyleDisposable().Push(borderType, color, thickness);

    /// <inheritdoc cref="Im.ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.Rgba32)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.ColorStyleDisposable Push(this ImStyleBorder borderType, Rgba32 color)
        => new Im.ColorStyleDisposable().Push(borderType, color);

    /// <inheritdoc cref="Im.ColorStyleDisposable.Push(ImSharp.ImStyleBorder,System.Numerics.Vector4,float,bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.ColorStyleDisposable Push(this ImStyleBorder borderType, Vector4 color, float thickness, bool condition)
        => new Im.ColorStyleDisposable().Push(borderType, color, thickness, condition);

    /// <inheritdoc cref="Im.ColorStyleDisposable.Push(ImSharp.ImStyleBorder,System.Numerics.Vector4,float)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.ColorStyleDisposable Push(this ImStyleBorder borderType, Vector4 color, float thickness)
        => new Im.ColorStyleDisposable().Push(borderType, color, thickness);

    /// <inheritdoc cref="Im.ColorStyleDisposable.Push(ImSharp.ImStyleBorder,System.Numerics.Vector4)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Im.ColorStyleDisposable Push(this ImStyleBorder borderType, Vector4 color)
        => new Im.ColorStyleDisposable().Push(borderType, color);
}
