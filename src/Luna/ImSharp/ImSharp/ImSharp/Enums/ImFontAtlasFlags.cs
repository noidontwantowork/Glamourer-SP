namespace ImSharp;

/// <summary> Flags for building a <seealso cref="Native.ImFontAtlas"/>. </summary>
[Flags]
public enum ImFontAtlasFlags : uint
{
    /// <summary> No specific behavior. </summary>
    None = 0,

    /// <summary> Do not round the height to the next power of two. </summary>
    NoPowerOfTwoHeight = 1 << 0,

    /// <summary> Do not build software mouse cursors into the atlas. </summary>
    NoMouseCursors = 1 << 1,

    /// <summary> Do not build thick line textures into the atlas. </summary>
    /// <remarks> Saves some texture memory, but is more expensive for the CPU and GPU by using polygons during rendering of lines. </remarks>
    NoBakedLines = 1 << 2,
}
