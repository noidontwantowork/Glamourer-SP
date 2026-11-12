#if IMPLOT
namespace ImSharp.ImPlot;

/// <summary> Flags to control the behavior of a plots axis. </summary>
[Flags]
public enum AxisFlags : uint
{
    /// <summary> No specific behavior. </summary>
    None = 0,

    /// <summary>  </summary>
    NoLabel = 1 << 0,

    /// <summary>  </summary>
    NoGridLines = 1 << 1,

    /// <summary>  </summary>
    NoTickMarks = 1 << 2,

    /// <summary>  </summary>
    NoTickLabels = 1 << 3,

    /// <summary>  </summary>
    NoInitialFit = 1 << 4,

    /// <summary>  </summary>
    NoMenus = 1 << 5,

    /// <summary>  </summary>
    NoSideSwitch = 1 << 6,

    /// <summary>  </summary>
    NoHighlight = 1 << 7,

    /// <summary>  </summary>
    Opposite = 1 << 8,

    /// <summary>  </summary>
    Foreground = 1 << 9,

    /// <summary>  </summary>
    Invert = 1 << 10,

    /// <summary>  </summary>
    AutoFit = 1 << 11,

    /// <summary>  </summary>
    RangeFit = 1 << 12,

    /// <summary>  </summary>
    PanStretch = 1 << 13,

    /// <summary>  </summary>
    LockMinimum = 1 << 14,

    /// <summary>  </summary>
    LockMaximum = 1 << 15,

    /// <summary>  </summary>
    Lock = LockMinimum | LockMaximum,

    /// <summary>  </summary>
    NoDecorations = NoLabel | NoGridLines | NoTickMarks | NoTickLabels,

    /// <summary>  </summary>
    AuxDefault = NoGridLines | Opposite,
}
#endif
