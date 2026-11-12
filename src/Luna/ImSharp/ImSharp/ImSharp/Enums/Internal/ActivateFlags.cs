namespace ImSharp.Internal;

/// <summary> Flags for navigation and focus handling. </summary>
[Flags]
public enum ActivateFlags : uint
{
    /// <summary> No specific behavior. </summary>
    None = 0,

    /// <summary> Favor activation that requires keyboard text input. This is default if a keyboard is available. </summary>
    PreferInput = 1 << 0,

    /// <summary> Favor activation for tweaking with arrow buttons or the gamepad. This is default if a keyboard is not available. </summary>
    PreferTweak = 1 << 1,

    /// <summary> Request a widget to preserve its state if able to. </summary>
    TryToPreserveState = 1 << 2,
}
