namespace ImSharp.Internal;

/// <summary> Types of supported logs. </summary>
public enum LogType
{
    /// <summary> Do not log. </summary>
    None = 0,

    /// <summary> Log to TTY. </summary>
    Tty = 1,

    /// <summary> Log to file. </summary>
    File = 2,

    /// <summary> Log to buffer. </summary>
    Buffer = 3,

    /// <summary> Log to clipboard. </summary>
    Clipboard = 4,
}
