namespace ImSharp;

/// <summary> Flags that control the behaviour of text inputs. </summary>
[Flags]
public enum InputTextFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Allows decimal number symbols (0123456789.+-*). </summary>
    CharsDecimal = 1 << 0,

    /// <summary> Allows hexadecimal number symbols (0123456789, ABCDEF, abcdef). </summary>
    CharsHexadecimal = 1 << 1,

    /// <summary> Turns all lower-case default alphabet ASCII characters to upper-case automatically. </summary>
    CharsUppercase = 1 << 2,

    /// <summary> Filter out all spaces and tabs. </summary>
    CharsNoBlank = 1 << 3,

    /// <summary> Select the entire text in the input when taking focus with the mouse. </summary>
    AutoSelectAll = 1 << 4,

    /// <summary> Return true only when Enter is pressed, not on every modification of the value. </summary>
    /// <remarks> Most use-cases can be handled better with <seealso cref="Im.Item.Deactivated"/> and <seealso cref="Im.Item.DeactivatedAfterEdit"/>. </remarks>
    EnterReturnsTrue = 1 << 5,

    /// <summary> The callback is invoked on pressing TAB (e.g. for completion handling). </summary>
    CallbackCompletion = 1 << 6,

    /// <summary> The callback is invoked on pressing Up/Down (e.g. for history handling). </summary>
    CallbackHistory = 1 << 7,

    /// <summary> The callback is invoked on each iteration. </summary>
    CallbackAlways = 1 << 8,

    /// <summary> The callback is invoked on character inputs to replace or discard them. </summary>
    CallbackCharFilter = 1 << 9,

    /// <summary> Pressing TAB inputs a '\t' character into the text field. </summary>
    AllowTabInput = 1 << 10,

    /// <summary> Validate with Enter, add a new line with Ctrl + Enter (default is the opposite). </summary>
    CtrlEnterForNewLine = 1 << 11,

    /// <summary> Disable following the cursor horizontally. </summary>
    NoHorizontalScroll = 1 << 12,

    /// <summary> Overwrite mode. </summary>
    AlwaysOverwrite = 1 << 13,

    /// <summary> The input is read-only and can only be used to select text. </summary>
    ReadOnly = 1 << 14,

    /// <summary> Password mode displays all characters as '*' and disables outgoing copies. </summary>
    Password = 1 << 15,

    /// <summary> Disable undo/redo functions.  </summary>
    NoUndoRedo = 1 << 16,

    /// <summary> Allow scientific number symbols (0123456789.+-*/eE). </summary>
    CharsScientific = 1 << 17,

    /// <summary> The callback is invoked on buffer capacity change requests, allowing the string to grow. </summary>
    CallbackResize = 1 << 18,

    /// <summary> The callback is invoked on any edit. </summary>
    /// <remarks> Inputs return true on any edit, too. So only use callbacks if you need to manipulate the underlying owned buffer during focus. </remarks>
    CallbackEdit = 1 << 19,

    /// <summary> Used internally to denote <seealso cref="Im.Input.MultiLine(Utf8LabelHandler,ref string,Vector2,InputTextFlags)"/>. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Multiline = 1 << 26,

    /// <summary> Used internally before reformatting data. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoMarkEdited = 1 << 27,

    /// <summary> Used internally to skip adding items. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    MergedItem = 1 << 28,
}
