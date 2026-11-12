namespace ImSharp.Internal;

/// <summary> Flags for navigation requests. </summary>
[Flags]
public enum NavigationMoveFlags : uint
{
    /// <summary> No navigation is requested. </summary>
    None = 0,

    /// <summary> On a failed request, start from the opposite horizontal side. </summary>
    LoopX = 1 << 0,

    /// <summary> On a failed request, start from the opposite vertical side. </summary>
    LoopY = 1 << 1,

    /// <summary> On a failed request, start from the opposite horizontal side but move one vertical line in the corresponding direction. </summary>
    WrapX = 1 << 2,

    /// <summary> On a failed request, start from the opposite vertical side but move one horizontal column in the corresponding direction. </summary>
    /// <remarks> This is not really used. </remarks>
    WrapY = 1 << 3,

    /// <summary> Allow scoring and consider the current navigation ID as a target candidate. </summary>
    AllowCurrentNavigationId = 1 << 4,

    /// <summary> Store an alternate scoring result that only comprises fully visible elements. </summary>
    /// <remarks> Used by PageUp and PageDown. </remarks>
    AlsoScoreVisibleSet = 1 << 5,

    /// <summary> Force scrolling to the minimum or maximum vertical position. </summary>
    /// <remarks> Used by Home and End, respectively. </remarks>
    ScrollToEdgeY = 1 << 6,

    /// <summary> The request was forwarded by another navigation item. </summary>
    Forwarded = 1 << 7,

    /// <summary> Do not apply the scoring result for debug purposes. </summary>
    DebugNoResult = 1 << 8,

    /// <summary> An item is focused by the API instead of an interaction. </summary>
    FocusApi = 1 << 9,

    /// <summary> Focus an item and activate it if the item is <seealso cref="ItemFlags.Inputable"/>. </summary>
    Tabbing = 1 << 10,

    /// <summary> Activate an item. </summary>
    Activate = 1 << 11,

    /// <summary> Do not alter the visible state of keyboard vs. mouse navigation highlights. </summary>
    DontSetNavHighlight = 1 << 12,
}
