namespace ImSharp;

/// <summary> Available modifier keys. </summary>
[Flags]
public enum ModFlags : uint
{
    /// <summary> Unmodified. </summary>
    None = 0,

    /// <summary> Control is held. </summary>
    Ctrl = 1 << 0,

    /// <summary> Shift is held. </summary>
    Shift = 1 << 1,

    /// <summary> Alt is held. </summary>
    Alt = 1 << 2,

    /// <summary> Super is held (Command on macOS). </summary>
    Super = 1 << 3,
}
