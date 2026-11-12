namespace ImSharp;

/// <summary> Flags that control the behavior of sliders and drag widgets. </summary>
[Flags]
public enum SliderFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Always clamp the value to the minimum and maximum bounds even when input manually with Control+Click. </summary>
    AlwaysClamp = 1 << 4,

    /// <summary> Make the widget logarithmic instead of linear. </summary>
    Logarithmic = 1 << 5,

    /// <summary> Disable rounding the underlying value to match the precision of the display format string. </summary>
    NoRoundToFormat = 1 << 6,

    /// <summary> Disable Control+Click and the Enter key to input text directly. </summary>
    NoInput = 1 << 7,
}
