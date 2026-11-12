namespace ImSharp.Internal;

/// <summary> Flags that control the behavior of tooltips. Used by <seealso cref="Im.Tooltip.Begin"/> </summary>
[Flags]
public enum TooltipFlags : uint
{
    /// <summary> No specific behavior. </summary>
    None = 0,

    /// <summary> Remove any prior tooltip instead of appending to it. </summary>
    OverridePreviousTooltip = 1 << 0,
}
