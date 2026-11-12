namespace ImSharp;

/// <summary> Keys for input handling. </summary>
public enum Key : uint
{
    /// <summary> No key. </summary>
    None = 0,

    /// <summary> Tab key. </summary>
    Tab = 512,

    /// <summary> Left arrow key. </summary>
    LeftArrow = 513,

    /// <summary> Right arrow key. </summary>
    RightArrow = 514,

    /// <summary> Up arrow key. </summary>
    UpArrow = 515,

    /// <summary> Down arrow key. </summary>
    DownArrow = 516,

    /// <summary> Page up key. </summary>
    PageUp = 517,

    /// <summary> Page down key. </summary>
    PageDown = 518,

    /// <summary> Home key. </summary>
    Home = 519,

    /// <summary> End key. </summary>
    End = 520,

    /// <summary> Insert key. </summary>
    Insert = 521,

    /// <summary> Delete key. </summary>
    Delete = 522,

    /// <summary> Backspace key. </summary>
    Backspace = 523,

    /// <summary> Spacebar key. </summary>
    Space = 524,

    /// <summary> Enter/Return key. </summary>
    Enter = 525,

    /// <summary> Escape key. </summary>
    Escape = 526,

    /// <summary> Left Control key. </summary>
    LeftCtrl = 527,

    /// <summary> Left Shift key. </summary>
    LeftShift = 528,

    /// <summary> Left Alt key. </summary>
    LeftAlt = 529,

    /// <summary> Left Super key (Command key on macOS). </summary>
    LeftSuper = 530,

    /// <summary> Right Control key. </summary>
    RightCtrl = 531,

    /// <summary> Right Shift key. </summary>
    RightShift = 532,

    /// <summary> Right Alt key. </summary>
    RightAlt = 533,

    /// <summary> Right Super key (Command key on macOS). </summary>
    RightSuper = 534,

    /// <summary> Menu key. </summary>
    Menu = 535,

    /// <summary> Number key 0. </summary>
    Key0 = 536,

    /// <summary> Number key 1. </summary>
    Key1 = 537,

    /// <summary> Number key 2. </summary>
    Key2 = 538,

    /// <summary> Number key 3. </summary>
    Key3 = 539,

    /// <summary> Number key 4. </summary>
    Key4 = 540,

    /// <summary> Number key 5. </summary>
    Key5 = 541,

    /// <summary> Number key 6. </summary>
    Key6 = 542,

    /// <summary> Number key 7. </summary>
    Key7 = 543,

    /// <summary> Number key 8. </summary>
    Key8 = 544,

    /// <summary> Number key 9. </summary>
    Key9 = 545,

    /// <summary> Letter A key. </summary>
    A = 546,

    /// <summary> Letter B key. </summary>
    B = 547,

    /// <summary> Letter C key. </summary>
    C = 548,

    /// <summary> Letter D key. </summary>
    D = 549,

    /// <summary> Letter E key. </summary>
    E = 550,

    /// <summary> Letter F key. </summary>
    F = 551,

    /// <summary> Letter G key. </summary>
    G = 552,

    /// <summary> Letter H key. </summary>
    H = 553,

    /// <summary> Letter I key. </summary>
    I = 554,

    /// <summary> Letter J key. </summary>
    J = 555,

    /// <summary> Letter K key. </summary>
    K = 556,

    /// <summary> Letter L key. </summary>
    L = 557,

    /// <summary> Letter M key. </summary>
    M = 558,

    /// <summary> Letter N key. </summary>
    N = 559,

    /// <summary> Letter O key. </summary>
    O = 560,

    /// <summary> Letter P key. </summary>
    P = 561,

    /// <summary> Letter Q key. </summary>
    Q = 562,

    /// <summary> Letter R key. </summary>
    R = 563,

    /// <summary> Letter S key. </summary>
    S = 564,

    /// <summary> Letter T key. </summary>
    T = 565,

    /// <summary> Letter U key. </summary>
    U = 566,

    /// <summary> Letter V key. </summary>
    V = 567,

    /// <summary> Letter W key. </summary>
    W = 568,

    /// <summary> Letter X key. </summary>
    X = 569,

    /// <summary> Letter Y key. </summary>
    Y = 570,

    /// <summary> Letter Z key. </summary>
    Z = 571,

    /// <summary> Function key F1. </summary>
    F1 = 572,

    /// <summary> Function key F2. </summary>
    F2 = 573,

    /// <summary> Function key F3. </summary>
    F3 = 574,

    /// <summary> Function key F4. </summary>
    F4 = 575,

    /// <summary> Function key F5. </summary>
    F5 = 576,

    /// <summary> Function key F6. </summary>
    F6 = 577,

    /// <summary> Function key F7. </summary>
    F7 = 578,

    /// <summary> Function key F8. </summary>
    F8 = 579,

    /// <summary> Function key F9. </summary>
    F9 = 580,

    /// <summary> Function key F10. </summary>
    F10 = 581,

    /// <summary> Function key F11. </summary>
    F11 = 582,

    /// <summary> Function key F12. </summary>
    F12 = 583,

    /// <summary> Apostrophe key. </summary>
    Apostrophe = 584,

    /// <summary> Comma key. </summary>
    Comma = 585,

    /// <summary> Minus key. </summary>
    Minus = 586,

    /// <summary> Period key. </summary>
    Period = 587,

    /// <summary> Slash key. </summary>
    Slash = 588,

    /// <summary> Semicolon key. </summary>
    Semicolon = 589,

    /// <summary> Equal key. </summary>
    Equal = 590,

    /// <summary> Left bracket key. </summary>
    LeftBracket = 591,

    /// <summary> Backslash key. </summary>
    Backslash = 592,

    /// <summary> Right bracket key. </summary>
    RightBracket = 593,

    /// <summary> Grave accent key. </summary>
    GraveAccent = 594,

    /// <summary> Caps Lock key. </summary>
    CapsLock = 595,

    /// <summary> Scroll Lock key. </summary>
    ScrollLock = 596,

    /// <summary> Num Lock key. </summary>
    NumLock = 597,

    /// <summary> Print Screen key. </summary>
    PrintScreen = 598,

    /// <summary> Pause key. </summary>
    Pause = 599,

    /// <summary> Keypad 0 key. </summary>
    Keypad0 = 600,

    /// <summary> Keypad 1 key. </summary>
    Keypad1 = 601,

    /// <summary> Keypad 2 key. </summary>
    Keypad2 = 602,

    /// <summary> Keypad 3 key. </summary>
    Keypad3 = 603,

    /// <summary> Keypad 4 key. </summary>
    Keypad4 = 604,

    /// <summary> Keypad 5 key. </summary>
    Keypad5 = 605,

    /// <summary> Keypad 6 key. </summary>
    Keypad6 = 606,

    /// <summary> Keypad 7 key. </summary>
    Keypad7 = 607,

    /// <summary> Keypad 8 key. </summary>
    Keypad8 = 608,

    /// <summary> Keypad 9 key. </summary>
    Keypad9 = 609,

    /// <summary> Keypad decimal key. </summary>
    KeypadDecimal = 610,

    /// <summary> Keypad divide key. </summary>
    KeypadDivide = 611,

    /// <summary> Keypad multiply key. </summary>
    KeypadMultiply = 612,

    /// <summary> Keypad subtract key. </summary>
    KeypadSubtract = 613,

    /// <summary> Keypad add key. </summary>
    KeypadAdd = 614,

    /// <summary> Keypad enter key. </summary>
    KeypadEnter = 615,

    /// <summary> Keypad equal key. </summary>
    KeypadEqual = 616,

    /// <summary> Gamepad start button. </summary>
    GamepadStart = 617,

    /// <summary> Gamepad back button. </summary>
    GamepadBack = 618,

    /// <summary> Gamepad face left button. </summary>
    GamepadFaceLeft = 619,

    /// <summary> Gamepad face right button. </summary>
    GamepadFaceRight = 620,

    /// <summary> Gamepad face up button. </summary>
    GamepadFaceUp = 621,

    /// <summary> Gamepad face down button. </summary>
    GamepadFaceDown = 622,

    /// <summary> Gamepad D-pad left button. </summary>
    GamepadDpadLeft = 623,

    /// <summary> Gamepad D-pad right button. </summary>
    GamepadDpadRight = 624,

    /// <summary> Gamepad D-pad up button. </summary>
    GamepadDpadUp = 625,

    /// <summary> Gamepad D-pad down button. </summary>
    GamepadDpadDown = 626,

    /// <summary> Gamepad left bumper (L1) button. </summary>
    GamepadL1 = 627,

    /// <summary> Gamepad right bumper (R1) button. </summary>
    GamepadR1 = 628,

    /// <summary> Gamepad left trigger (L2) button. </summary>
    GamepadL2 = 629,

    /// <summary> Gamepad right trigger (R2) button. </summary>
    GamepadR2 = 630,

    /// <summary> Gamepad left stick press (L3) button. </summary>
    GamepadL3 = 631,

    /// <summary> Gamepad right stick press (R3) button. </summary>
    GamepadR3 = 632,

    /// <summary> Gamepad left stick left direction. </summary>
    GamepadLStickLeft = 633,

    /// <summary> Gamepad left stick right direction. </summary>
    GamepadLStickRight = 634,

    /// <summary> Gamepad left stick up direction. </summary>
    GamepadLStickUp = 635,

    /// <summary> Gamepad left stick down direction. </summary>
    GamepadLStickDown = 636,

    /// <summary> Gamepad right stick left direction. </summary>
    GamepadRStickLeft = 637,

    /// <summary> Gamepad right stick right direction. </summary>
    GamepadRStickRight = 638,

    /// <summary> Gamepad right stick up direction. </summary>
    GamepadRStickUp = 639,

    /// <summary> Gamepad right stick down direction. </summary>
    GamepadRStickDown = 640,

    /// <summary> Modifier Ctrl. </summary>
    ModCtrl = 641,

    /// <summary> Modifier Shift. </summary>
    ModShift = 642,

    /// <summary> Modifier Alt. </summary>
    ModAlt = 643,

    /// <summary> Modifier Super. </summary>
    ModSuper = 644,
}

public static class KeyExtensions
{
    /// <summary> The offset at which named keys start. </summary>
    public const int NamedKeyBegin = 512;

    /// <summary> The offset at which named keys end. </summary>
    public const int NamedKeyEnd = Count;

    /// <summary> The number of named keys. </summary>
    public const int NamedKeyCount = NamedKeyEnd - NamedKeyBegin;

    /// <summary> The total count of keys. </summary>
    public const int Count = 645;

    /// <summary> The size of the key data array. </summary>
    public const int KeyDataSize = NamedKeyCount;

    /// <summary> The offset to translate keys into the key data array. </summary>
    public const int KeyDataOffset = NamedKeyBegin;
}
