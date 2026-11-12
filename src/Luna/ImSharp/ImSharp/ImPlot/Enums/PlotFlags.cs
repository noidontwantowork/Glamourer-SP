#if IMPLOT
namespace ImSharp.ImPlot;

/// <summary> Flags to control the behavior of plots. </summary>
[Flags]
public enum PlotFlags : uint
{
    /// <summary> No specific behavior. </summary>
    None = 0,

    /// <summary> The plot title will not be displayed. </summary>
    NoTitle = 1 << 0,

    /// <summary> The legend will not be displayed. </summary>
    NoLegend = 1 << 1,

    /// <summary> The mouse position in plot coordinates will not be displayed while hovering the plot. </summary>
    NoMouseText = 1 << 2,

    /// <summary> The user will not be able to interact with the plot. </summary>
    NoInputs = 1 << 3,

    /// <summary> The user will not be able to open context menus. </summary>
    NoMenus = 1 << 4,

    /// <summary> The user will not be able to box-select. </summary>
    NoBoxSelect = 1 << 5,

    /// <summary> Do not use a child window region to capture mouse scrolling etc. </summary>
    NoChild = 1 << 6,

    /// <summary> No frame will be rendered around the plot. </summary>
    NoFrame = 1 << 7,

    /// <summary> Axes will be constrained to the same units per pixel. </summary>
    EqualProportions = 1 << 8,

    /// <summary> The default mouse cursor will be replaced with cross-hairs when hovering the plot. </summary>
    Crosshairs = 1 << 9,

    /// <summary> Only draw a plot canvas. </summary>
    CanvasOnly = NoTitle | NoLegend | NoMenus | NoBoxSelect | NoMouseText,
}
#endif
