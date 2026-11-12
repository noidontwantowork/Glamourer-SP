namespace ImSharp;

/// <summary> A wrapper around 3 floats for 3-channel colors. </summary>
public record struct ImVec3(float R, float G, float B)
{
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Vector3(ImVec3 value)
        => new(value.R, value.G, value.B);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ImVec3(Vector3 value)
        => new(value.X, value.Y, value.Z);

    public override string ToString()
        => $"({R}, {G}, {B})";
}
