namespace ImSharp;

/// <summary> Flags that control the behavior of a dock space. </summary>
[Flags]
public enum DockNodeFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Do not display the dock space node, but keep it alive. Windows docked into this will not be undocked. </summary>
    /// <remarks> Shared. </remarks>
    KeepAliveOnly = 1 << 0,

    /// <summary> Disable docking into the central node. </summary>
    /// <remarks> Shared. </remarks>
    NoDockingInCentralNode = 1 << 2,

    /// <summary> Enable a passthru dock space. </summary>
    /// <remarks> Shared. </remarks>
    PassthruCentralNode = 1 << 3,

    /// <summary> Disable splitting the docking node into smaller nodes. </summary>
    /// <remarks> Shared and local. When this is turned on, existing splits will be preserved. </remarks>
    NoSplit = 1 << 4,

    /// <summary> Disable resizing the docking node. </summary>
    /// <remarks> Shared and local. </remarks>
    NoResize = 1 << 5,

    /// <summary> The tab bar will automatically hide when there is only a single window in the node. </summary>
    /// <remarks> Shared and local. </remarks>
    AutoHideTabBar = 1 << 6,

    /// <summary> A mask of all flags that are inherited. </summary>
    SharedFlagsInheritMask = ~0u,

    /// <summary> A mask for all flags that control resizing. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoResizeMask = NoResize | NoResizeX | NoResizeY,

    /// <summary> A mask of all local flags that are moved to inheriting children when splitting a node. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    LocalFlagsTransferMask =
        NoSplit | NoResizeMask | AutoHideTabBar | CentralNode | NoTabBar | HiddenTabBar | NoWindowMenuButton | NoCloseButton | NoDocking,

    /// <summary> A mask of all local flags. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    LocalFlagsMask = LocalFlagsTransferMask | DockSpace,

    /// <summary> A mask of all values that are saved to config. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    SavedFlagsMask = NoResizeMask | DockSpace | CentralNode | NoTabBar | HiddenTabBar | NoWindowMenuButton | NoCloseButton | NoDocking,

    /// <summary> A node that occupies space within an existing user window. Otherwise, the node is floating and its own window. </summary>
    /// <remarks> Local and saved. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    DockSpace = 1 << 10,

    /// <summary> Central nodes stay visible even when empty and only use the remaining spaces from its neighbors. </summary>
    /// <remarks> Local and saved. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    CentralNode = 1 << 11,

    /// <summary> Tab bars are completely unavailable. </summary>
    /// <remarks> Local and saved. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoTabBar = 1 << 12,

    /// <summary> Tab bars are hidden, with a triangle in the corner to show it again. </summary>
    /// <remarks> Local and saved. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    HiddenTabBar = 1 << 13,

    /// <summary> Disable the window/docking menu that appears instead of the collapse button. </summary>
    /// <remarks> Local and saved. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoWindowMenuButton = 1 << 14,

    /// <summary> Do not show a close button. </summary>
    /// <remarks> Local and saved. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoCloseButton = 1 << 15,

    /// <summary> Disable any form of docking in this dock space or node. </summary>
    /// <remarks> Local and saved. Existing docked nodes will be preserved when turning this on. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoDocking = 1 << 16,

    /// <summary> Prevent other nodes from splitting this node. </summary>
    /// <remarks> Experimental. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoDockingSplitMe = 1 << 17,

    /// <summary> Prevent this node from splitting another node. </summary>
    /// <remarks> Experimental. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoDockingSplitOther = 1 << 18,

    /// <summary> Prevent other nodes from being docked over this node. </summary>
    /// <remarks> Experimental. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoDockingOverMe = 1 << 19,

    /// <summary> Prevent this node docking over other non-empty nodes. </summary>
    /// <remarks> Experimental. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoDockingOverOther = 1 << 20,

    /// <summary> Prevent this node docking over other empty nodes. </summary>
    /// <remarks> Experimental. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoDockingOverEmpty = 1 << 21,

    /// <summary> Prevent this node from resizing horizontally. </summary>
    /// <remarks> Experimental. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoResizeX = 1 << 22,

    /// <summary> Prevent this node from resizing vertically. </summary>
    /// <remarks> Experimental. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoResizeY = 1 << 23,
}
