namespace ImSharp;

/// <summary> Flags that govern the behaviour of a draw list. </summary>
[Flags]
public enum ImDrawListFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Enable anti-aliased lines and borders. </summary>
    /// <remarks> This at least doubles the number of triangles. </remarks>
    AntiAliasedLines = 1,

    /// <summary> Enable anti-aliased lines and borders using textures when possible.  </summary>
    /// <remarks> Requires the backend to render with bilinear filtering. </remarks>
    AntiAliasedLinesUseTex = 2,

    /// <summary> Enable anti-aliased edges around filled shapes. </summary>
    AntiAliasedFill = 4,

    /// <summary> Allows emitting VertexOffset > 0 for large meshes. </summary>
    AllowVertexOffset = 8,
}
