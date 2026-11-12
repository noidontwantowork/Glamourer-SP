namespace ImSharp;

/// <summary> A wrapper around 2 points that span a rectangle. </summary>
/// <param name="Minimum"> One corner of the rectangle. </param>
/// <param name="Maximum"> The diagonally opposite corner of the rectangle. </param>
public readonly record struct ImRect(ImVec2 Minimum, ImVec2 Maximum)
{
    /// <summary> Create a rectangle from one point and a size. </summary>
    /// <param name="minimum"> The fixed point. </param>
    /// <param name="size"> The size to add to the fixed point. </param>
    /// <returns> A rectangle of the given <paramref name="size"/> based at the given point <paramref name="minimum"/>. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static ImRect FromSize(ImVec2 minimum, ImVec2 size)
        => new(minimum, new ImVec2(minimum.X + size.X, minimum.Y + size.Y));

    /// <summary> Create a rectangle from 2 points. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public ImRect(float xMin, float yMin, float xMax, float yMax)
        : this(new ImVec2(xMin, yMin), new ImVec2(xMax, yMax))
    { }

    /// <summary> Get the size of the rectangle. </summary>
    public ImVec2 Size
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => new(Maximum.X - Minimum.X, Maximum.Y - Minimum.Y);
    }

    public override string ToString()
        => $"[{Minimum.X},  {Minimum.Y}] x [{Maximum.X},  {Maximum.Y}]";

    /// <summary> Add a border of certain pixel width to all sides of the rectangle. </summary>
    /// <param name="pixel"> The size to be added to the maximum and subtracted from the minimum. </param>
    /// <returns> The increased rectangle. </returns>
    public ImRect Increase(float pixel)
        => new(new ImVec2(Minimum.X - pixel, Minimum.Y - pixel), new ImVec2(Maximum.X + pixel, Maximum.Y + pixel));
}
