namespace ImSharp;

/// <summary> The different types of mousewheel scrolling supported for combo previews. </summary>
/// <remarks> The modifiers can be combined as flags. </remarks>
[Flags]
public enum MouseWheelType : byte
{
    /// <summary> Do not interact with the mousewheel. </summary>
    None = 0,

    /// <summary> React to any mousewheel interaction while hovering the preview. </summary>
    Unmodified = 1,

    /// <summary> Only react to mousewheel interaction while hovering the preview if Shift is held. </summary>
    Shift = 2,

    /// <summary> Only react to mousewheel interaction while hovering the preview if Control is held. </summary>
    Control = 4,

    /// <summary> Only react to mousewheel interaction while hovering the preview if Alt is held. </summary>
    Alt = 8,
}

public static class MouseWheelTypeExtensions
{
    /// <summary> Check the modifiers for the mousewheel check. </summary>
    public static bool CheckMouseWheel(this MouseWheelType type)
        => Im.Io.KeyModifiers.HasFlag(type switch
        {
            MouseWheelType.None                                                => (ModFlags)0xFFFFFFFF,
            MouseWheelType.Unmodified                                          => ModFlags.None,
            MouseWheelType.Shift                                               => ModFlags.Shift,
            MouseWheelType.Control                                             => ModFlags.Ctrl,
            MouseWheelType.Alt                                                 => ModFlags.Alt,
            MouseWheelType.Shift | MouseWheelType.Control                      => ModFlags.Shift | ModFlags.Ctrl,
            MouseWheelType.Shift | MouseWheelType.Alt                          => ModFlags.Shift | ModFlags.Alt,
            MouseWheelType.Control | MouseWheelType.Alt                        => ModFlags.Ctrl | ModFlags.Alt,
            MouseWheelType.Shift | MouseWheelType.Control | MouseWheelType.Alt => ModFlags.Shift | ModFlags.Ctrl | ModFlags.Alt,
            _                                                                  => (ModFlags)0xFFFFFFFF,
        });
}
