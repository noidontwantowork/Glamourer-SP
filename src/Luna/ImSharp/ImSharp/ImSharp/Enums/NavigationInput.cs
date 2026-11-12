namespace ImSharp;

/// <summary> Utility enum to handle gamepad or keyboard navigation. </summary>
/// <remarks> This is mostly handled automatically. </remarks>
public enum NavigationInput : int
{
    /// <summary> Activate, open, toggle or tweak a value. </summary>
    /// <remarks> Cross on PS controllers, A on XBox and Switch controllers, Space on keyboards.</remarks>
    Activate,

    /// <summary> Cancel, close or exit out of something. </summary>
    /// <remarks> Circle on PS controllers, B on XBox and Switch controllers, Escape on keyboards.</remarks>
    Cancel,

    /// <summary> Text input or using the on-screen keyboard. </summary>
    /// <remarks> Triangle on PS controllers, Y on XBox controllers, X on Switch controllers, Return on keyboards.</remarks>
    Input,

    /// <summary> Toggle menu on tap, focus, move or resize on hold. </summary>
    /// <remarks> Square on PS controllers, X on XBox controllers, Y on Switch controllers, Alt on keyboards.</remarks>
    Menu,

    /// <summary> Move cursor to the left, tweak or resize windows. </summary>
    /// <remarks> D-pad left on controllers, arrow left on keyboards.</remarks>
    DPadLeft,

    /// <summary> Move cursor to the right, tweak or resize windows. </summary>
    /// <remarks> D-pad right on controllers, arrow right on keyboards.</remarks>
    DPadRight,

    /// <summary> Move cursor upwards, tweak or resize windows. </summary>
    /// <remarks> D-pad up on controllers, arrow up on keyboards.</remarks>
    DPadUp,

    /// <summary> Move cursor downwards, tweak or resize windows. </summary>
    /// <remarks> D-pad down on controllers, arrow down on keyboards.</remarks>
    DPadDown,

    /// <summary> Scroll the visible area to the left or move windows. </summary>
    /// <remarks> Moving the left analog stick to the left on controllers. </remarks>
    LStickLeft,

    /// <summary> Scroll the visible area to the right or move windows. </summary>
    /// <remarks> Moving the right analog stick to the right on controllers. </remarks>
    LStickRight,

    /// <summary> Scroll the visible area upwards or move windows. </summary>
    /// <remarks> Moving the left analog stick upwards on controllers. </remarks>
    LStickUp,

    /// <summary> Scroll the visible area downwards or move windows. </summary>
    /// <remarks> Moving the left analog stick downwards on controllers. </remarks>
    LStickDown,

    /// <summary> Focus the previous window. </summary>
    /// <remarks> L1 or L2 on PS controllers, LB or LT on XBox controllers, L or ZL on Switch controllers. </remarks>
    FocusPrevious,

    /// <summary> Focus the next window. </summary>
    /// <remarks> R1 or R2 on PS controllers, RB or RT on XBox controllers, R or ZR on Switch controllers. </remarks>
    FocusNext,

    /// <summary> Slower tweaks. </summary>
    /// <remarks> L1 or L2 on PS controllers, LB or LT on XBox controllers, L or ZL on Switch controllers. </remarks>
    TweakSlow,

    /// <summary> Faster tweaks. </summary>
    /// <remarks> R1 or R2 on PS controllers, RB or RT on XBox controllers, R or ZR on Switch controllers. </remarks>
    TweakFast,

    /// <summary> Toggle the Menu </summary>
    /// <remarks> Do not use this directly. </remarks>
    KeyMenu,

    /// <summary> Move left. </summary>
    /// <remarks> Do not use this directly. </remarks>
    KeyLeft,

    /// <summary> Move right. </summary>
    /// <remarks> Do not use this directly. </remarks>
    KeyRight,

    /// <summary> Move up. </summary>
    /// <remarks> Do not use this directly. </remarks>
    KeyUp,

    /// <summary> Move down. </summary>
    /// <remarks> Do not use this directly. </remarks>
    KeyDown,

    /// <summary> The number of separate NavInput values. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Count,
}
