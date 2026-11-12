namespace ImSharp.Internal;

/// <summary> State flags  for windows. Reset every frame. Inherited by child windows. </summary>
[Flags]
public enum ItemFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Disable keyboard tabbing. </summary>
    NoTabStop = 1 << 0,

    /// <summary> Buttons will return true multiple times based on <seealso cref="Native.Io.KeyRepeatDelay"/> and <seealso cref="Native.Io.KeyRepeatRate"/>. </summary>
    ButtonRepeat = 1 << 1,

    /// <summary> Disable interactions without affecting visuals, see <seealso cref="Im.Disabled()"/> </summary>
    Disabled = 1 << 2,

    /// <summary> Disable keyboard and gamepad directional navigation. </summary>
    NoNavigation = 1 << 3,

    /// <summary> Disable the item being a candidate for default focus. </summary>
    NoNavigationDefaultFocus = 1 << 4,

    /// <summary> Disable selectables or MenuItems automatically closing their popup window. </summary>
    SelectableDontClosePopup = 1 << 5,

    /// <summary> Represent a mixed or indeterminate value, e.g. for <seealso cref="Im.Checkbox(Utf8LabelHandler,ref ulong,ulong)"/>. </summary>
    MixedValue = 1 << 6,

    /// <summary> Allow hovering interactions but not changing underlying values. </summary>
    /// <remarks> This is not yet fully supported. </remarks>
    ReadOnly = 1 << 7,

    /// <summary> Auto-activate input mode when focused via navigation. </summary>
    /// <remarks> This is not yet fully supported. </remarks>
    Inputable = 1 << 8,
}
