namespace ImSharp;

/// <summary> A wrapper around 2 floats for coordinates and sizes. </summary>
public record struct ImVec2(float X, float Y)
{
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Vector2(ImVec2 value)
        => new(value.X, value.Y);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ImVec2(Vector2 value)
        => new(value.X, value.Y);

    public override string ToString()
        => $"({X}, {Y})";
}
