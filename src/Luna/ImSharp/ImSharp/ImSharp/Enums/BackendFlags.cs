namespace ImSharp;

/// <summary> Flags with information about backend capabilities. </summary>
[Flags]
public enum BackendFlags : uint
{
    /// <summary> No specific information. </summary>
    None = 0,

    /// <summary> The backend supports gamepads and currently has one connected. </summary>
    HasGamepad = 1 << 0,

    /// <summary> The backend supports changing the OS mouse cursor shape, see <seealso cref="Im.GetMouseCursor"/>. </summary>
    HasMouseCursors = 1 << 1,

    /// <summary> The backend supports setting the OS mouse cursor position, see <seealso cref="ImGuiIo.WantSetMousePos"/>. </summary>
    HasSetMousePos = 1 << 2,

    /// <summary> The backend supports vertex offsets, putting out large meshes with 16-bit indices, see <seealso cref="ImDrawCommand.VtxOffset"/>. </summary>
    RendererHasVtxOffset = 1 << 3,

    /// <summary> The backend supports multiple viewports. </summary>
    PlatformHasViewports = 1 << 10,

    /// <summary> The backend supports querying the current viewport the mouse is hovering. </summary>
    HasMouseHoveredViewport = 1 << 11,

    /// <summary> The backend supports multiple viewports. </summary>
    RendererHasViewports = 1 << 12,
}
