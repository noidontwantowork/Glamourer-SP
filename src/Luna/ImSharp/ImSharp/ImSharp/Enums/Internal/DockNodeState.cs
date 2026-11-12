namespace ImSharp.Internal;

/// <summary> Flags used internally to denote the state of a dock node. </summary>
public enum DockNodeState
{
    /// <summary> Unknown state. </summary>
    Unknown = 0,

    /// <summary> The dock host window is hidden because it is a single window. </summary>
    HostWindowHiddenBecauseSingleWindow = 1,

    /// <summary> The dock host window is hidden because the window is being resized. </summary>
    HostWindowHiddenBecauseWindowsAreResizing = 2,

    /// <summary> The dock host window is visible. </summary>
    HostWindowVisible = 3,
}
