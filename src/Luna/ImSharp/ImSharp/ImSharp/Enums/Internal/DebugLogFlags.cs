namespace ImSharp.Internal;

/// <summary> Flags used for debug logging in the internal ImGui debug log window. </summary>
[Flags]
public enum DebugLogFlags : uint
{
    /// <summary> No specific flag. </summary>
    None = 0,

    /// <summary> The active ID changed. </summary>
    EventActiveId = 1 << 0,

    /// <summary> The focus event occured. </summary>
    EventFocus = 1 << 1,

    /// <summary> A popup event occured. </summary>
    EventPopup = 1 << 2,

    /// <summary> A navigation event occured. </summary>
    EventNav = 1 << 3,

    /// <summary> An input/output event occured. </summary>
    EventIo = 1 << 4,

    /// <summary> A docking event occured. </summary>
    EventDocking = 1 << 5,

    /// <summary> A viewport event occured. </summary>
    EventViewport = 1 << 6,

    /// <summary> Also send output to TTY. </summary>
    OutputToTty = 1 << 10,

    /// <summary> A mask of event types. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    EventMask = EventActiveId | EventFocus | EventPopup | EventNav | EventIo | EventDocking | EventViewport,
}
