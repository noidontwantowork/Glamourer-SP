namespace ImSharp.Internal;

/// <summary> Different sources of input events. </summary>
public enum InputSource
{
    /// <summary> No source specified. </summary>
    None = 0,

    /// <summary> The input event stems from a mouse. </summary>
    Mouse = 1,

    /// <summary> The input event stems from a keyboard. </summary>
    Keyboard = 2,

    /// <summary> The input event stems from a gamepad. </summary>
    Gamepad = 3,

    /// <summary> The input event stems from the clipboard. </summary>
    /// <remarks> Only used by <seealso cref="Im.Input.Text(Utf8LabelHandler,Span{byte},out StringU8,Utf8HintHandler,InputTextFlags)"/>. </remarks>
    Clipboard = 4,

    /// <summary> The input event stems from navigation. </summary>
    Nav = 5,
}
