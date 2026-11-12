namespace ImSharp;

/// <summary> Flags controlling the behaviour of focus checks. </summary>
[Flags]
public enum FocusedFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Return true if any child of the window is focused. </summary>
    ChildWindows = 1 << 0,

    /// <summary> Test the focus check from the root window. </summary>
    RootWindow = 1 << 1,

    /// <summary> Return true if any window is focused. </summary>
    /// <remarks> Do not use this to dispatch low-level inputs. </remarks>
    AnyWindow = 1 << 2,

    /// <summary> Do not consider the popup hierarchy when checking for child windows or from the root window. </summary>
    NoPopupHierarchy = 1 << 3,

    /// <summary> Do consider the docking hierarchy when checking for child windows or from the root window. </summary>
    DockHierarchy = 1 << 4,

    /// <summary> Return true if any child of the current root window is focused. </summary>
    RootAndChildWindows = RootWindow | ChildWindows,
}
