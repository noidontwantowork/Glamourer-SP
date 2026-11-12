namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Draw a spinner indicating indeterminate loading. </summary>
    /// <param name="id"> The ID of the spinner. </param>
    /// <param name="radius"> The radius of the spinner. </param>
    /// <param name="thickness"> The thickness of the spinner. </param>
    /// <param name="color"> The color of the spinner. </param>
    public static void Spinner(Utf8LabelHandler id, float radius, int thickness, Rgba32 color)
    {
        var window = Im.Window.Current;
        if (window.SkipItems)
            return;

        var itemId      = Im.Id.Get(ref id);
        var pos         = window.CursorPosition;
        var size        = new Vector2(radius * 2, (radius + Im.Style.FramePadding.Y) * 2);
        var boundingBox = new ImRect(pos, pos + size);
        Im.Item.SetSize(size, Im.Style.FramePadding.Y);
        if (!Im.Item.Add(boundingBox, itemId))
            return;

        // Render
        var         drawList    = Im.Window.DrawList;
        const float numSegments = 30;
        var         time        = (float)Im.State.Time;
        var         start       = MathF.Abs(MathF.Sin(time * 1.8f) * (numSegments - 5));

        const float max    = MathF.PI * 2 * (numSegments - 3) / numSegments;
        var         min    = MathF.PI * 2 * start / numSegments;
        var         diff   = max - min;
        var         center = new Vector2(pos.X + radius, pos.Y + radius + Im.Style.FramePadding.Y);
        for (var i = 0; i < numSegments; ++i)
        {
            var segment = min + i / numSegments * diff + time * 8;
            drawList.Path.LineTo(new Vector2(center.X + MathF.Cos(segment) * radius, center.Y + MathF.Sin(segment) * radius));
        }

        drawList.Path.FinishStroke(color, ImDrawFlagsPath.None, thickness);
    }
}
