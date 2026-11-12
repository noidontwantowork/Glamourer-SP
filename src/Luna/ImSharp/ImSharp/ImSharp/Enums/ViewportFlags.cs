namespace ImSharp;

/// <summary> Flags that govern the behavior of viewports. </summary>
[Flags]
public enum ViewportFlags : uint
{
    /// <summary> No specific behavior. </summary>
    None = 0,

    /// <summary> This viewport is a platform window. </summary>
    IsPlatformWindow = 1 << 0,

    /// <summary> This viewport is a platform monitor. </summary>
    /// <remarks> Currently unused. </remarks>
    IsPlatformMonitor = 1 << 1,

    /// <summary> The platform window is created and managed by the application, not ImGui. </summary>
    OwnedByApp = 1 << 2,

    /// <summary> Disable any platform decorations like the title bar, borders etc. </summary>
    NoDecoration = 1 << 3,

    /// <summary> Disable the platform task bar icon. </summary>
    NoTaskBarIcon = 1 << 4,

    /// <summary> Do not take focus when created. </summary>
    NoFocusOnAppearing = 1 << 5,

    /// <summary> Do not take focus when clicked on. </summary>
    NoFocusOnClick = 1 << 6,

    /// <summary> Make the mouse pass through so this window can be dragged while peaking behind it. </summary>
    NoInputs = 1 << 7,

    /// <summary> The renderer does not need to clear the framebuffer ahead. </summary>
    NoRendererClear = 1 << 8,

    /// <summary> Display this window on top. </summary>
    TopMost = 1 << 9,

    /// <summary> The window is minimized, skip rendering it. </summary>
    Minimized = 1 << 10,

    /// <summary> Avoid merging this window into another host. </summary>
    NoAutoMerge = 1 << 11,

    /// <summary> This main viewport can host multiple ImGui windows. </summary>
    CanHostOtherWindows = 1 << 12,
}
