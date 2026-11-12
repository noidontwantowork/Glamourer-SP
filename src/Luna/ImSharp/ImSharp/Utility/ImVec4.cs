namespace ImSharp;

/// <summary> A wrapper around 4 floats for 4-channel colors, matrices or rectangles. </summary>
public readonly record struct ImVec4(float R, float G, float B, float A)
{
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Vector4(ImVec4 value)
        => new(value.R, value.G, value.B, value.A);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ImVec4(Vector4 value)
        => new(value.X, value.Y, value.Z, value.W);

    public float X
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => R;
    }

    public float Y
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => G;
    }

    public float Z
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => B;
    }

    public float W
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => A;
    }

    public override string ToString()
        => $"({R}, {G}, {B}, {A})";
}
