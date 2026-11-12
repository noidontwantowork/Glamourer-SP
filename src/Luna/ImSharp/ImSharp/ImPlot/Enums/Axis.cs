#if IMPLOT
namespace ImSharp.ImPlot;

/// <summary> The indices for different axes. </summary>
public enum PlotAxis : uint
{
    /// <summary> The first horizontal axis. This is enabled by default. </summary>
    Horizontal1 = 0,

    /// <summary> The second horizontal axis. This is disabled by default. </summary>
    Horizontal2 = 1,

    /// <summary> The third horizontal axis. This is disabled by default. </summary>
    Horizontal3 = 2,

    /// <summary> The first vertical axis. This is enabled by default. </summary>
    Vertical1 = 3,

    /// <summary> The second vertical axis. This is disabled by default. </summary>
    Vertical2 = 4,

    /// <summary> The third vertical axis. This is disabled by default. </summary>
    Vertical3 = 5,
}
#endif
