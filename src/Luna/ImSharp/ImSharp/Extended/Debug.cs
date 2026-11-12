namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Draw some checker-boarded help lines across the whole window along the dimensions of the last item when hovering it. </summary>
    /// <param name="color1"> The color of odd pixels. If this is <see cref="ColorParameter.Default"/>, <see cref="Rgba32.White"/> is used. </param>
    /// <param name="color2"> The color of even pixels. If this is <see cref="ColorParameter.Default"/>, <see cref="Rgba32.Black"/> is used.</param>
    /// <param name="always"> Draw the lines even when not hovering the item. </param>
    public static void HoverHelpLines(ColorParameter color1 = default, ColorParameter color2 = default, bool always = false)
    {
        if (!always && !Im.Item.Hovered(HoveredFlags.AllowWhenDisabled))
            return;

        var min        = Im.Item.UpperLeftCorner;
        var max        = Im.Item.LowerRightCorner;
        var drawList   = Im.DrawList.Foreground;
        var windowPos  = Im.Window.Position;
        var windowSize = Im.Window.Size;
        var lineXStart = (int)MathF.Round(windowPos.X);
        var lineXEnd   = (int)MathF.Round(lineXStart + windowSize.X) + 1;

        var lineYStart = (int)MathF.Round(windowPos.Y);
        var lineYEnd   = (int)MathF.Round(lineYStart + windowSize.Y) + 1;

        var minX = (int)MathF.Round(min.X);
        var minY = (int)MathF.Round(min.Y);
        var maxX = (int)MathF.Round(max.X);
        var maxY = (int)MathF.Round(max.Y);

        const int thickness = 1;

        var c1 = color1.CheckDefault(Rgba32.White);
        var c2 = color1.CheckDefault(Rgba32.Black);
        drawList.Shape.Line(new Vector2(lineXStart, minY),       new Vector2(lineXEnd, minY),     c1, thickness);
        drawList.Shape.Line(new Vector2(lineXStart, maxY),       new Vector2(lineXEnd, maxY),     c1, thickness);
        drawList.Shape.Line(new Vector2(minX,       lineYStart), new Vector2(minX,     lineYEnd), c1, thickness);
        drawList.Shape.Line(new Vector2(maxX,       lineYStart), new Vector2(maxX,     lineYEnd), c1, thickness);

        for (var x = (lineXStart + minY) % 2 is 0 ? lineXStart + 1 : lineXStart; x < lineXEnd; x += 2)
            drawList.Shape.Line(new Vector2(x, minY), new Vector2(x + 1, minY), c2, thickness);

        for (var x = (lineXStart + maxY) % 2 is 0 ? lineXStart + 1 : lineXStart; x < lineXEnd; x += 2)
            drawList.Shape.Line(new Vector2(x, maxY), new Vector2(x + 1, maxY), c2, thickness);

        for (var y = (lineYStart + minX) % 2 is 0 ? lineYStart + 1 : lineYStart; y < lineYEnd; y += 2)
            drawList.Shape.Line(new Vector2(minX, y), new Vector2(minX, y + 1), c2, thickness);

        for (var y = (lineYStart + maxX) % 2 is 0 ? lineYStart + 1 : lineYStart; y < lineYEnd; y += 2)
            drawList.Shape.Line(new Vector2(maxX, y), new Vector2(maxX, y + 1), c2, thickness);
    }
}
