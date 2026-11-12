using ImSharp.Internal;

namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Create a bordered child that can be resized in positive X- or Y-direction. </summary>
    /// <param name="label"> The ID of the child as text. HAS to be null-terminated. </param>
    /// <param name="size"> The desired current size of the child. </param>
    /// <param name="currentSize"> The returned current size of the child. </param>
    /// <param name="setSize"> The function to invoke when the size of the child finalizes a change. </param>
    /// <param name="minSize"> The minimum size of the child. </param>
    /// <param name="maxSize"> The maximum size of the child. </param>
    /// <param name="resizeX"> Whether to allow resizing in X-direction. </param>
    /// <param name="resizeY"> Whether to allow resizing in Y-direction. </param>
    /// <param name="flags"> Additional flags for the child. </param>
    /// <returns> A disposable object that evaluates to true if any part of the begun child is currently visible. Use with using. </returns>
    public static Im.ChildDisposable ResizableChild(Utf8LabelHandler label, Vector2 size, out Vector2 currentSize, Action<Vector2> setSize,
        Vector2 minSize, Vector2 maxSize,
        WindowFlags flags = WindowFlags.None, bool resizeX = true, bool resizeY = false)
    {
        // Work in the child ID.
        using var idStack = Im.Id.Push(ref label);

        // Use two IDs to store state and current resizing value if any.
        var     stateId = Im.Id.Get("####state"u8);
        var     valueId = Im.Id.Get("####value"u8);
        ref var state   = ref Im.State.Storage.GetIntReference(stateId);
        ref var value   = ref Im.State.Storage.GetFloatReference(valueId);
        currentSize = state switch
        {
            1 => size with { X = value },
            2 => size with { Y = value },
            _ => size,
        };
        if (minSize.X <= maxSize.X)
            currentSize.X = Math.Clamp(currentSize.X, minSize.X, maxSize.X);
        if (minSize.Y <= maxSize.Y)
            currentSize.Y = Math.Clamp(currentSize.Y, minSize.Y, maxSize.Y);

        // Fix border width, use regular color and rounding style.
        const float borderWidth     = 1f;
        const float halfBorderWidth = borderWidth / 2f;
        var         borderColor     = Im.Style[ImGuiColor.Border];
        var         rounding        = Im.Style.ChildRounding;
        var         onlyInner       = rounding is 0 ? borderWidth : rounding;
        var         hoverExtend     = 5f * Im.Style.GlobalScale;
        const float delay           = 0.1f;

        var rectMin = Im.Cursor.ScreenPosition + new Vector2(halfBorderWidth);
        var rectMax = Im.Cursor.ScreenPosition + currentSize - new Vector2(halfBorderWidth);

        // If resizing in X direction is allowed, handle it.
        if (resizeX)
        {
            var id = Im.Id.Get("####x"u8);
            // Behaves as a splitter, so second size is the remainder.
            var sizeInc      = currentSize.X;
            var sizeDec      = Im.ContentRegion.Available.X - currentSize.X;
            var remainderMin = Im.ContentRegion.Available.X - maxSize.X;

            using var color = Im.Color.Push(ImGuiColor.Separator, borderColor);
            var rect = new ImRect(new Vector2(rectMax.X - halfBorderWidth, MathF.Floor(rectMin.Y + onlyInner)),
                new Vector2(rectMax.X + halfBorderWidth,                   MathF.Ceiling(rectMax.Y - onlyInner)));
            if (Im.Behavior.Splitter(rect, id, Axis.X, ref sizeInc, ref sizeDec, minSize.X, remainderMin, hoverExtend, delay, 0))
            {
                // Update internal state.
                value       = sizeInc;
                currentSize = currentSize with { X = sizeInc };
                rectMax     = Im.Cursor.ScreenPosition + currentSize;
                state       = 1;
            }

            if (Im.Item.Deactivated)
            {
                // Handle updating on deactivation only.
                state = 0;
                if (Im.Item.DeactivatedAfterEdit)
                {
                    currentSize.X  = value;
                    rectMax = Im.Cursor.ScreenPosition + currentSize;
                    setSize(currentSize);
                }
            }
        }

        if (resizeY)
        {
            // Same as X just for the other direction. Y takes priority in length.
            var id = Im.Id.Get("####y"u8);

            var sizeInc      = currentSize.Y;
            var sizeDec      = Im.ContentRegion.Available.Y - currentSize.Y;
            var remainderMin = Im.ContentRegion.Available.Y - maxSize.Y;

            using var color = Im.Color.Push(ImGuiColor.Separator, borderColor);
            var rect = new ImRect(new Vector2(rectMin.X + onlyInner, rectMax.Y - halfBorderWidth),
                new Vector2(rectMax.X - onlyInner,                   rectMax.Y + halfBorderWidth));
            if (Im.Behavior.Splitter(rect, id, Axis.Y, ref sizeInc, ref sizeDec, minSize.X, remainderMin, hoverExtend, delay, 0))
            {
                value       = sizeInc;
                currentSize = currentSize with { Y = sizeInc };
                rectMax     = Im.Cursor.ScreenPosition + currentSize;
                state       = 2;
            }

            if (Im.Item.Deactivated)
            {
                state = 0;
                if (Im.Item.DeactivatedAfterEdit)
                {
                    currentSize.Y  = value;
                    rectMax = Im.Cursor.ScreenPosition + currentSize;
                    setSize(currentSize);
                }
            }
        }

        var path = Im.Window.DrawList.Path;
        if (rounding is 0)
        {
            // With no rounding, simply draw the lines not dealt with by the resizable lines.
            if (!resizeY)
                path.LineTo(rectMax);
            path.LineTo(new Vector2(rectMin.X, rectMax.Y));
            path.LineTo(rectMin);
            path.LineTo(new Vector2(rectMax.X, rectMin.Y));
            if (!resizeX)
                path.LineTo(new Vector2(rectMax.X, rectMax.Y));
            path.FinishStroke(borderColor, ImDrawFlagsPath.None, borderWidth);
            if (resizeX && resizeY)
                Im.Window.DrawList.Shape.RectangleFilled(rectMax - new Vector2(halfBorderWidth), rectMax + new Vector2(halfBorderWidth),
                    borderColor);
        }
        else
        {
            // Otherwise, draw all required arcs and lines.
            var centerTopRight    = new Vector2(rectMax.X - rounding, rectMin.Y + rounding);
            var centerTopLeft     = new Vector2(rectMin.X + rounding, centerTopRight.Y);
            var centerBottomRight = new Vector2(centerTopRight.X,     rectMax.Y - rounding);
            var centerBottomLeft  = new Vector2(centerTopLeft.X,      centerBottomRight.Y);
            if (!resizeX)
                path.ArcToFast(centerBottomRight, rounding, 3, 0);
            path.ArcToFast(centerTopRight,   rounding, 12, 9);
            path.ArcToFast(centerTopLeft,    rounding, 9,  6);
            path.ArcToFast(centerBottomLeft, rounding, 6,  3);
            if (resizeY)
            {
                path.FinishStroke(borderColor, ImDrawFlagsPath.None, borderWidth);
                if (resizeX)
                {
                    path.ArcToFast(centerBottomRight, rounding, 3, 0);
                    path.FinishStroke(borderColor, ImDrawFlagsPath.None, borderWidth);
                }
            }
            else if (!resizeX)
            {
                path.FinishStroke(borderColor, ImDrawFlagsPath.Closed, borderWidth);
            }
            else
            {
                path.ArcToFast(centerBottomRight, rounding, 3, 0);
                path.FinishStroke(borderColor, ImDrawFlagsPath.None, borderWidth);
            }
        }

        idStack.Pop();
        using var c = Im.Color.Push(ImGuiColor.Border, Rgba32.Transparent);
        return Im.Child.Begin(label, currentSize, true, flags);
    }
}
