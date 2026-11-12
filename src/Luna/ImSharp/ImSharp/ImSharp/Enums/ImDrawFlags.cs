namespace ImSharp;

/// <summary> Flags for draw lists drawing paths. </summary>
/// <remarks> Compatible with <seealso cref="ImDrawFlagsRectangle"/>. </remarks>
[Flags]
public enum ImDrawFlagsPath : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Automatically close the path going from the last path node back to the first. </summary>
    Closed = 1 << 0,
}

/// <summary> Flags for draw lists drawing rectangles. </summary>
/// <remarks> Compatible with <seealso cref="ImDrawFlagsPath"/>. </remarks>
[Flags]
public enum ImDrawFlagsRectangle : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Round only the top left corner. The default is to round all corners when rounding is > 0. </summary>
    RoundCornersTopLeft = 1 << 4,

    /// <summary> Round only the top right corner. The default is to round all corners when rounding is > 0. </summary>
    RoundCornersTopRight = 1 << 5,

    /// <summary> Round only the bottom left corner. The default is to round all corners when rounding is > 0.</summary>
    RoundCornersBottomLeft = 1 << 6,

    /// <summary> Round only the bottom right corner. The default is to round all corners when rounding is > 0. </summary>
    RoundCornersBottomRight = 1 << 7,

    /// <summary> Disable rounding on all corners even if rounding is > 0. </summary>
    /// <remarks> This is not the same as <seealso cref="None"/> or implicit. </remarks>
    RoundCornersNone = 1 << 8,

    /// <summary> Round only the top corners. The default is to round all corners when rounding is > 0. </summary>
    RoundCornersTop = RoundCornersTopLeft | RoundCornersTopRight,

    /// <summary> Round only the bottom corners. The default is to round all corners when rounding is > 0. </summary>
    RoundCornersBottom = RoundCornersBottomLeft | RoundCornersBottomRight,

    /// <summary> Round only the left corners. The default is to round all corners when rounding is > 0. </summary>
    RoundCornersLeft = RoundCornersTopLeft | RoundCornersBottomLeft,

    /// <summary> Round only the right corners. The default is to round all corners when rounding is > 0. </summary>
    RoundCornersRight = RoundCornersTopRight | RoundCornersBottomRight,

    /// <summary> Round all corners. The default is to round all corners when rounding is > 0. </summary>
    RoundCornersAll = RoundCornersLeft | RoundCornersRight,

    /// <summary> Default to rounding all corners if none of the more specific flags are specified. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    RoundCornersDefault = RoundCornersAll,

    /// <summary> The mask for rounding of corners. Only useful when combining with <seealso cref="ImDrawFlagsPath"/>. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    RoundCornersMask = RoundCornersAll | RoundCornersNone,
}

/// <summary> Flags to signify sets of corners of a rectangle. </summary>
/// <remarks> Compatible with <seealso cref="ImDrawFlagsRectangle"/>, just more user-friendly. </remarks>
[Flags]
public enum Corners : uint
{
    /// <summary> Default set of corners. </summary>
    Default = ImDrawFlagsRectangle.None,

    /// <summary> The top left corner only. </summary>
    TopLeft = ImDrawFlagsRectangle.RoundCornersTopLeft,

    /// <summary> The top right corner only. </summary>
    TopRight = ImDrawFlagsRectangle.RoundCornersTopRight,

    /// <summary> The bottom left corner only. </summary>
    BottomLeft = ImDrawFlagsRectangle.RoundCornersBottomLeft,

    /// <summary> The bottom right corner only. </summary>
    BottomRight = ImDrawFlagsRectangle.RoundCornersBottomRight,

    /// <summary> No corner. </summary>
    /// <remarks> This is not the same as <seealso cref="Default"/> or implicit. </remarks>
    None = ImDrawFlagsRectangle.RoundCornersNone,

    /// <summary> The top pair of corners. </summary>
    Top = ImDrawFlagsRectangle.RoundCornersTop,

    /// <summary> The bottom pair of corners. </summary>
    Bottom = ImDrawFlagsRectangle.RoundCornersBottom,

    /// <summary> The left pair of corners. </summary>
    Left = ImDrawFlagsRectangle.RoundCornersLeft,

    /// <summary> The right pair of corners. </summary>
    Right = ImDrawFlagsRectangle.RoundCornersRight,

    /// <summary> All corners. </summary>
    All = ImDrawFlagsRectangle.RoundCornersAll,
}
