namespace ImSharp.Internal;

/// <summary> Internal flags extending <seealso cref="SliderFlags"/>. </summary>
[Flags]
public enum SliderFlagsPrivate : uint
{
    /// <summary> The slider is oriented vertically instead of horizontally. </summary>
    Vertical = 1 << 20,

    /// <summary> The slider is read-only and may not change its underlying value. </summary>
    ReadOnly = 1 << 21,
}
