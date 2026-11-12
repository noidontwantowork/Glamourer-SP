namespace ImSharp.Internal;

/// <summary> Flags controlling the behavior of navigation highlights. </summary>
[Flags]
public enum NavigationHighlightFlags
{
    /// <summary> No specific behavior. </summary>
    None = 0,

    /// <summary> Draw a default navigation highlight box. </summary>
    DefaultType = 1,

    /// <summary> Draw a thin navigation highlight box. </summary>
    ThinType = 2,

    /// <summary> Draw the navigation highlight box even when using the mouse. </summary>
    AlwaysDraw = 4,

    /// <summary> Do not round the corners of the navigation highlight box regardless of style. </summary>
    NoRounding = 3,
}
