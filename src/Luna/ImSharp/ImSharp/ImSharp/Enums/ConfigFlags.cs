namespace ImSharp;

/// <summary> Flags to configure ImGui. </summary>
[Flags]
public enum ConfigFlags : uint
{
    /// <summary> No specific configuration. </summary>
    None = 0,

    /// <summary> Enable keyboard navigation with tabbing, directional arrows and space or enter. </summary>
    NavEnableKeyboard = 1 << 0,

    /// <summary> Enable gamepad navigation. </summary>
    /// <remarks> Requires the backend to set <seealso cref="BackendFlags.HasGamepad"/>. </remarks>
    NavEnableGamepad = 1 << 1,

    /// <summary> The navigation can also move the mouse cursor. </summary>
    /// <remarks> If enabled, <seealso cref="Native.Io.WantSetMousePos"/> must be honored by the backend. </remarks>
    NavEnableSetMousePosition = 1 << 2,

    /// <summary> Navigation does not capture the keyboard. </summary>
    NavNoCaptureKeyboard = 1 << 3,

    /// <summary> Disable mouse inputs and interactions. </summary>
    NoMouse = 1 << 4,

    /// <summary> Disable altering the mouse cursors shape and visibility. </summary>
    NoMouseCursorChange = 1 << 5,

    /// <summary> Enable docking of windows. </summary>
    DockingEnable = 1 << 6,

    /// <summary> Ignore kerning when drawing all text. </summary>
    NoKerning = 1 << 7,

    /// <summary> Enable Viewports. </summary>
    /// <remarks> Requires the backend to support viewports via renderer and platform. </remarks>
    ViewportsEnable = 1 << 10,

    /// <summary> Reposition and resize windows when the DPI scale of a viewport changes. </summary>
    /// <remarks> BETA: Do not use. </remarks>
    DpiEnableScaleViewports = 1 << 14,

    /// <summary> Request bitmap-scaled fonts when the DPI scale of a viewport changes. </summary>
    /// <remarks> BETA: Do not use. </remarks>
    DpiEnableScaleFonts = 1 << 15,

    /// <summary> The application is SRGB-aware. </summary>
    /// <remarks> Not used by Core ImGui. </remarks>
    IsSrgb = 1 << 20,

    /// <summary> The application is using a touch screen instead of a mouse. </summary>
    /// <remarks> Not used by Core ImGui. </remarks>
    IsTouchScreen = 1 << 21,
}
