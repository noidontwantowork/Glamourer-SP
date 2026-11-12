namespace ImSharp;

/// <inheritdoc cref="ImRect"/>
/// <remarks> C# version using <seealso cref="Vector2"/> instead of <seealso cref="ImVec2"/>. </remarks>
public readonly record struct Rectangle(Vector2 Minimum, Vector2 Maximum)
{
    /// <summary> A rectangle that is only the point Zero. </summary>
    public static readonly Rectangle Zero = new(Vector2.Zero, Vector2.Zero);

    /// <summary> One of the corners not stored. If this rectangle is oriented (see <see cref="Orient"/>), this is at the bottom right. </summary>
    public Vector2 BottomRight
        => new(Maximum.X, Minimum.Y);

    /// <summary> One of the corners not stored. If this rectangle is oriented (see <see cref="Orient"/>), this is at the top left. </summary>
    public Vector2 TopLeft
        => new(Minimum.X, Maximum.Y);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Rectangle(ImRect rect)
        => new(rect.Minimum, rect.Maximum);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ImRect(Rectangle rect)
        => new(rect.Minimum, rect.Maximum);

    /// <inheritdoc cref="ImRect.FromSize"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Rectangle FromSize(Vector2 minimum, Vector2 size)
        => new(minimum, minimum + size);

    /// <inheritdoc cref="ImRect.FromSize"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Rectangle FromSize(Vector2 size)
        => new(Vector2.Zero, size);

    /// <inheritdoc cref="ImRect.FromSize"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Rectangle FromSize(float sizeX, float sizeY)
        => new(Vector2.Zero, new Vector2(sizeX, sizeY));

    /// <inheritdoc cref="ImRect.ImRect(float,float,float,float)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public Rectangle(float xMinimum, float yMinimum, float xMaximum, float yMaximum)
        : this(new Vector2(xMinimum, yMinimum), new Vector2(xMaximum, yMaximum))
    { }

    /// <summary> Whether this rectangle contains the given point. </summary>
    /// <param name="point"> The point to check. </param>
    /// <returns> Whether the point is contained in the rectangle. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool Contains(Vector2 point)
        => point.X >= Minimum.X && point.X <= Maximum.X && point.Y >= Minimum.Y && point.Y <= Maximum.Y;

    /// <summary> Add a point to a rectangle, i.e. ensure that the point is within the rectangle. </summary>
    /// <param name="point"> The point to add. </param>
    /// <returns> The minimum rectangle containing the added point and the entire prior rectangle. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public Rectangle Add(Vector2 point)
        => new(MathF.Min(point.X, Minimum.X), MathF.Min(point.Y, Minimum.Y), MathF.Max(point.X, Maximum.X), MathF.Max(point.Y, Maximum.Y));

    /// <summary> Expand the rectangle in all directions. </summary>
    /// <param name="amount"> The amount to expand the rectangle by. </param>
    /// <returns> The expanded rectangle. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public Rectangle Expand(float amount)
        => new(Minimum.X - amount, Minimum.Y - amount, Maximum.X + amount, Maximum.Y + amount);

    /// <summary> Expand the rectangle in all directions. </summary>
    /// <param name="amount"> The amount to expand the rectangle by in X and Y direction. </param>
    /// <returns> The expanded rectangle. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public Rectangle Expand(Vector2 amount)
        => new(Minimum.X - amount.X, Minimum.Y - amount.Y, Maximum.X + amount.X, Maximum.Y + amount.Y);

    /// <inheritdoc cref="ImRect.Size"/>
    public Vector2 Size
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => new(Width, Height);
    }

    /// <summary> Get the width of this rectangle. </summary>
    public float Width
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Maximum.X - Minimum.X;
    }

    /// <summary> Get the height of this rectangle. </summary>
    public float Height
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Maximum.Y - Minimum.Y;
    }

    /// <summary> Get the center of this rectangle. </summary>
    public Vector2 Center
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => (Maximum + Minimum) / 2;
    }

    /// <summary> Check whether this rectangle overlaps the given rectangle. </summary>
    /// <param name="rect"> The other rectangle to check. </param>
    /// <returns> Whether a real overlap with measure greater than 0 exists between the two rectangles. </returns>
    public bool Overlaps(in Rectangle rect)
        => rect.Minimum.Y < Maximum.Y && rect.Maximum.Y > Minimum.Y && rect.Minimum.X < Maximum.X && rect.Maximum.X > Minimum.X;

    /// <summary> Check whether this rectangle overlaps the given line on at least one point. </summary>
    /// <param name="lineStart"> One end of the line. </param>
    /// <param name="lineEnd"> The other end of the line. </param>
    /// <returns> Whether at least one point lies both in the rectangle and on the line. </returns>
    public bool OverlapsLine(Vector2 lineStart, Vector2 lineEnd)
    {
        if (Contains(lineStart) || Contains(lineEnd))
            return true;

        var oriented = Orient();
        // Check the full line being on any side of the rectangle.
        if (lineStart.X < oriented.Minimum.X && lineEnd.X < oriented.Minimum.X
         || lineStart.X > oriented.Maximum.X && lineEnd.X > oriented.Maximum.X
         || lineStart.Y < oriented.Minimum.Y && lineEnd.Y < oriented.Minimum.Y
         || lineStart.Y > oriented.Maximum.Y && lineEnd.Y > oriented.Maximum.Y)
            return false;

        // Evaluate the corners of the rectangle against the line.
        var lineEvaluator = new LineEvaluator(lineStart, lineEnd);
        var (sign0, sign1, sign2, sign3) = (Math.Sign(lineEvaluator.Evaluate(oriented.Minimum)),
            Math.Sign(lineEvaluator.Evaluate(oriented.TopLeft)),
            Math.Sign(lineEvaluator.Evaluate(oriented.BottomRight)),
            Math.Sign(lineEvaluator.Evaluate(oriented.Maximum)));
        return Math.Abs(sign0 + sign1 + sign2 + sign3) != Math.Abs(sign0) + Math.Abs(sign1) + Math.Abs(sign2) + Math.Abs(sign3);
    }

    /// <summary> Orient this rectangle, so that <see cref="Minimum"/> is the lower left corner and <see cref="Maximum"/> is the upper right corner. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public Rectangle Orient()
    {
        var (minX, maxX) = Minimum.X > Maximum.X ? (Maximum.X, Minimum.X) : (Minimum.X, Maximum.X);
        var (minY, maxY) = Minimum.Y > Maximum.Y ? (Maximum.Y, Minimum.Y) : (Minimum.Y, Maximum.Y);
        return new Rectangle(minX, minY, maxX, maxY);
    }

    /// <summary> Whether this rectangle is not correctly <see cref="Orient"/>ed. </summary>
    public bool IsInverted
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Minimum.X > Maximum.X || Minimum.Y > Maximum.Y;
    }
}
