namespace ImSharp;

using ImSharp;

/// <summary> A structure representing a segmented cubic Bézier curve. </summary>
/// <param name="Point0"> The evaluation point at 0. </param>
/// <param name="Point1"> The trend direction from 0. </param>
/// <param name="Point2"> The trend direction to 1. </param>
/// <param name="Point3"> The evaluation point at 1. </param>
/// <param name="NumSegments"> The number of segments to draw the curve. </param>
public readonly record struct CubicBezier(Vector2 Point0, Vector2 Point1, Vector2 Point2, Vector2 Point3, int NumSegments)
{
    /// <summary> Get the closest point on this segmented cubic Bézier curve to the given coordinates. </summary>
    /// <param name="point"> The point to check for. </param>
    /// <returns> The closest point to the given coordinates. </returns>
    public Vector2 GetClosestPoint(Vector2 point)
    {
        var lastPoint       = Point0;
        var closest         = Vector2.Zero;
        var closestDistance = float.MaxValue;
        var step            = 1f / NumSegments;
        for (var i = 1; i <= NumSegments; ++i)
        {
            var currentPoint = Evaluate(step * i);
            var linePoint    = LineEvaluator.ClosestPointOnLine(lastPoint, currentPoint, point);
            var distance     = (point - linePoint).LengthSquared();
            if (distance < closestDistance)
            {
                closest         = linePoint;
                closestDistance = distance;
            }

            lastPoint = currentPoint;
        }

        return closest;
    }

    /// <summary> Get the distance from this segmented cubic Bézier curve to the given coordinates. </summary>
    /// <param name="point"> The point to check for. </param>
    /// <returns> The distance, which is the distance of the point returned by <see cref="GetClosestPoint"/> to <paramref name="point"/>. </returns>
    public float GetDistance(Vector2 point)
    {
        var curvePoint = GetClosestPoint(point);
        return (curvePoint - point).Length();
    }


    /// <summary> Get the smallest rectangular box containing the whole segmented cubic Bézier curve. </summary>
    public Rectangle GetBoundingBox()
    {
        var (minX, maxX) = Point0.X >= Point3.X ? (Point3.X, Point0.X) : (Point0.X, Point3.X);
        var (minY, maxY) = Point0.Y >= Point3.Y ? (Point3.Y, Point0.Y) : (Point0.Y, Point3.Y);
        var rect = new Rectangle(minX, minY, maxX, maxY);
        return rect.Add(Point1).Add(Point2);
    }

    /// <summary> Check whether this segmented cubic Bézier curve shares any points with the given rectangle. </summary>
    /// <param name="rectangle"> The rectangle to check. </param>
    /// <returns> Whether there exist any points lying both on the curve and within the rectangle. </returns>
    public bool Overlaps(in Rectangle rectangle)
    {
        var current     = Point0;
        var segmentDist = 1f / NumSegments;
        for (var segment = 0; segment < NumSegments; ++segment)
        {
            var next = Evaluate((segment + 1) * segmentDist);
            if (rectangle.OverlapsLine(current, next))
                return true;

            current = next;
        }

        return false;
    }

    /// <summary> Evaluate a point in [0, 1] on this cubic Bézier function. </summary>
    /// <param name="point"> The point to evaluate. </param>
    /// <returns> The evaluated point. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public Vector2 Evaluate(float point)
        => Evaluate(point, Point0, Point1, Point2, Point3);

    /// <summary> Evaluate a point in [0, 1] on the cubic Bézier function given by the 4 points passed. </summary>
    /// <param name="point"> A point in [0, 1]. </param>
    /// <param name="point0"> The first point generating the cubic Bézier. </param>
    /// <param name="point1"> The second point generating the cubic Bézier. </param>
    /// <param name="point2"> The third point generating the cubic Bézier. </param>
    /// <param name="point3"> The fourth point generating the cubic Bézier. </param>
    /// <returns> The evaluated point on the cubic Bézier. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static Vector2 Evaluate(float point, Vector2 point0, Vector2 point1, Vector2 point2, Vector2 point3)
    {
        var inverse      = 1f - point;
        var coefficient0 = inverse * inverse;
        var coefficient1 = 3 * coefficient0 * point;
        coefficient0 *= inverse;
        var coefficient3 = point * point;
        var coefficient2 = 3 * inverse * coefficient3;
        coefficient3 *= inverse;
        return coefficient0 * point0 + coefficient1 * point1 + coefficient2 * point2 + coefficient3 * point3;
    }

    /// <summary> Generate a cubic Bézier curve between two points. </summary>
    /// <param name="origin"> The left point. </param>
    /// <param name="target"> The right point. </param>
    /// <param name="lineSegmentsPerLength"> The number of linear segments to draw the curve. </param>
    /// <param name="flip"> Whether to flip origin and target. </param>
    /// <returns> A cubic Bézier curve defined by slight X-axis directional bias. </returns>
    public static CubicBezier Generate(Vector2 origin, Vector2 target, float lineSegmentsPerLength, bool flip = false)
    {
        if (flip)
            (origin, target) = (target, origin);
        var linkLength = (target - origin).Length();
        var offset     = new Vector2(0.25f * linkLength, 0f);
        return new CubicBezier(origin, origin + offset, target - offset, target, Math.Max((int)(linkLength * lineSegmentsPerLength), 1));
    }
}

public static class BezierExtensions
{
    /// <summary> Draw a cubic Bézier curve from the given curve. </summary>
    /// <param name="shape"> The draw list. </param>
    /// <param name="bezier"> The Bézier curve. </param>
    /// <param name="color"><inheritdoc cref="Im.DrawList.DrawListShapes.BezierCubic"/>></param>
    /// <param name="thickness"><inheritdoc cref="Im.DrawList.DrawListShapes.BezierCubic"/>></param>
    public static void BezierCubic(this Im.DrawList.DrawListShapes shape, in CubicBezier bezier, ColorParameter color = default,
        float thickness = 1f)
        => shape.BezierCubic(bezier.Point0, bezier.Point1, bezier.Point2, bezier.Point3, color, thickness, bezier.NumSegments);
}
