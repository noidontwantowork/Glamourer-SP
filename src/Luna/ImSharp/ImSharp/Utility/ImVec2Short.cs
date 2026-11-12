namespace ImSharp;

/// <summary> A wrapper around 2 shorts for coordinates and sizes. </summary>
public record struct ImVec2Short(short X, short Y)
{
    public override string ToString()
        => $"({X}, {Y})";
}
