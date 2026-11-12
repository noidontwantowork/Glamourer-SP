namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Draw a button with the given label and size. </summary>
    /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="tooltip"> A tooltip shown when hovering the button regardless of whether it is disabled or not as text. Does not have to be null-terminated. </param>
    /// <param name="config"> Additional parameters to configure the design and behavior of the button. </param>
    /// <returns> True if the button has been clicked in this frame. </returns>
    /// <remarks> The tooltip is always evaluated. If this is expensive, prefer leaving it empty and using <seealso cref="Im.Tooltip.OnHover(HoveredFlags,ref HoverUtf8StringHandler)"/> manually. </remarks>
    [OverloadResolutionPriority(20)]
    public static bool Button(Utf8LabelHandler label, Utf8TextHandler tooltip = default, in ButtonConfiguration config = default)
    {
        using var style = config.PushColorStyle();

        bool ret;
        using (Im.Disabled(config.Disabled))
        {
            ret = Im.Button(label, config.Size, config.Flags);
        }

        if (tooltip.GetSpan(out var span))
            Im.Tooltip.OnHover(HoveredFlags.AllowWhenDisabled, span);
        return ret;
    }

    /// <summary> Draw a button with the given label and size. </summary>
    /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="config"> Additional parameters to configure the design and behavior of the button. </param>
    /// <returns> True if the button has been clicked in this frame. </returns>
    [OverloadResolutionPriority(25)]
    public static bool Button(Utf8LabelHandler label, in ButtonConfiguration config = default)
    {
        using var style = config.PushColorStyle();

        using var _ = Im.Disabled(config.Disabled);
        return Im.Button(label, config.Size, config.Flags);
    }

    /// <summary> Draw a button with the given label and size. </summary>
    /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="size">
    /// The desired size for the button. If (0, 0), it will fit to the label.<br/>
    /// You can pass <paramref name="size"/>.x = float.MinValue to span the available width.
    /// </param>
    /// <param name="tooltip"> A tooltip shown when hovering the button regardless of whether it is disabled or not as text. Does not have to be null-terminated. </param>
    /// <param name="disabled"> Whether the button should be disabled or not. </param>
    /// <param name="buttonColor"> The color of the button's background. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Button"/> is used. </param>
    /// <param name="textColor"> The color of the button's label. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
    /// <param name="flags"> Additional flags to control the button's behaviour. </param>
    /// <returns> True if the button has been clicked in this frame. </returns>
    /// <remarks> The tooltip is always evaluated. If this is expensive, prefer leaving it empty and using <seealso cref="Im.Tooltip.OnHover(HoveredFlags,ref HoverUtf8StringHandler)"/> manually. </remarks>
    [OverloadResolutionPriority(50)]
    public static bool Button(Utf8LabelHandler label, ColorParameter buttonColor = default, ColorParameter textColor = default,
        Vector2 size = default, Utf8TextHandler tooltip = default, bool disabled = false, ButtonFlags flags = ButtonFlags.None)
    {
        using var color = Im.Color.Push(ImGuiColor.Button, buttonColor).Push(ImGuiColor.Text, textColor);

        bool ret;
        using (Im.Disabled(disabled))
        {
            ret = Im.Button(label, size, flags);
        }

        if (tooltip.GetSpan(out var span))
            Im.Tooltip.OnHover(HoveredFlags.AllowWhenDisabled, span);
        return ret;
    }

    /// <inheritdoc cref="Button(Utf8LabelHandler,ColorParameter,ColorParameter,Vector2,Utf8TextHandler,bool,ButtonFlags)"/>
    [OverloadResolutionPriority(100)]
    public static bool Button(Utf8LabelHandler label, Vector2 size = default, Utf8TextHandler tooltip = default, bool disabled = false,
        ButtonFlags flags = ButtonFlags.None)
    {
        bool ret;
        using (Im.Disabled(disabled))
        {
            ret = Im.Button(label, size, flags);
        }

        if (tooltip.GetSpan(out var span))
            Im.Tooltip.OnHover(HoveredFlags.AllowWhenDisabled, span);
        return ret;
    }

    /// <inheritdoc cref="Button(Utf8LabelHandler,ColorParameter,ColorParameter,Vector2,Utf8TextHandler,bool,ButtonFlags)"/>
    [OverloadResolutionPriority(200)]
    public static bool Button(Utf8LabelHandler label, Vector2 size = default, Utf8TextHandler tooltip = default,
        ButtonFlags flags = ButtonFlags.None)
    {
        var ret = Im.Button(label, size, flags);
        if (tooltip.GetSpan(out var span))
            Im.Tooltip.OnHover(HoveredFlags.AllowWhenDisabled, span);
        return ret;
    }

    /// <inheritdoc cref="Button(Utf8LabelHandler,ColorParameter,ColorParameter,Vector2,Utf8TextHandler,bool,ButtonFlags)"/>
    [OverloadResolutionPriority(300)]
    public static bool Button(Utf8LabelHandler label, Vector2 size = default, bool disabled = false, ButtonFlags flags = ButtonFlags.None)
    {
        using var _ = Im.Disabled(disabled);
        return Im.Button(label, size, flags);
    }

    /// <inheritdoc cref="Button(Utf8LabelHandler,ColorParameter,ColorParameter,Vector2,Utf8TextHandler,bool,ButtonFlags)"/>
    [OverloadResolutionPriority(400)]
    public static bool Button(Utf8LabelHandler label, Vector2 size = default, ButtonFlags flags = ButtonFlags.None)
        => Im.Button(label, size, flags);


    /// <summary> Which button half was pressed. </summary>
    public enum SplitButtonHalf : byte
    {
        /// <summary> The button was not pressed. </summary>
        None,

        /// <summary> The upper left half of the button was pressed. </summary>
        UpperLeft,

        /// <summary> The lower right half of the button was pressed. </summary>
        LowerRight,
    }

    /// <summary> Draw a diagonally split button with the upper left half representing one button, and the lower right half representing the other. </summary>
    /// <param name="id"> The ID for the full button. </param>
    /// <param name="left"> The data for the upper left button half. </param>
    /// <param name="right"> The data for the lower right button half. </param>
    /// <param name="size"> The total size of the button. If Y is 0, the current frame height will be used. If X is 0, the required width for both labels with padding will be used. </param>
    /// <param name="sharedBorder"> The color of the inner border between the two button halves. If this is transparent, no border will be drawn. </param>
    /// <returns> Which part, if any, of the button was pressed. </returns>
    public static SplitButtonHalf SplitButton(ImGuiId id, SplitButtonData left, SplitButtonData right, Vector2 size,
        Rgba32 sharedBorder = default)
    {
        // Copied from ImGui proper.
        var window = Im.Window.Current;
        if (window.SkipItems)
            return SplitButtonHalf.None;

        var     screenPos = window.CursorPosition;
        Vector2 rightStart;
        if (size.Y is 0)
            size.Y = Im.Style.FrameHeight;
        if (size.X is 0)
        {
            var sizeLeft  = Im.Font.CalculateSize(left.Label).X;
            var sizeRight = Im.Font.CalculateSize(right.Label).X;
            size.X = Math.Max(sizeLeft, sizeRight) + size.Y + 2 * Im.Style.FramePadding.X;
            rightStart = new Vector2(screenPos.X + size.X - sizeRight - Im.Style.FramePadding.X,
                screenPos.Y + size.Y - 1 - Im.Style.TextHeight);
        }
        else
        {
            var sizeRight = Im.Font.CalculateSize(right.Label).X;
            rightStart = new Vector2(screenPos.X + size.X - sizeRight - Im.Style.FramePadding.X,
                screenPos.Y + size.Y - 1 - Im.Style.TextHeight);
        }

        size = size.Round();
        var boundingBox = new ImRect(screenPos, screenPos + size);
        Im.Item.SetSize(size, Im.Style.FramePadding.Y);
        if (!Im.Item.Add(boundingBox, id))
            return 0;

        var ret = Im.Behavior.Button(boundingBox, id, out var hovered, out var held);
        Im.Render.NavigationHighlight(boundingBox, id);

        var (isHoveredLeft, isHoveredRight) = DiagonalCheck(hovered, boundingBox);
        var colorLeft        = left.GetColor(isHoveredLeft, held);
        var colorRight       = right.GetColor(isHoveredRight, held);
        var leftBorderColor  = left.Border.CheckDefault(ImGuiColor.Border);
        var rightBorderColor = right.Border.CheckDefault(ImGuiColor.Border);
        var rounding         = MathF.Min(Im.Style.FrameRounding, MathF.Floor(size.Y / 2));
        var borderThickness  = MathF.Max(Im.Style.FrameBorderThickness, 1);
        var drawList         = Im.Window.DrawList;
        var path             = drawList.Path;
        if (rounding is 0)
        {
            var upperRight = new Vector2(boundingBox.Maximum.X, boundingBox.Minimum.Y);
            var lowerLeft  = new Vector2(boundingBox.Minimum.X, boundingBox.Maximum.Y);
            path.LineTo(boundingBox.Minimum);
            path.LineTo(upperRight);
            path.LineTo(lowerLeft);
            path.FinishFillConvex(colorLeft);
            if (!leftBorderColor.IsTransparent)
            {
                path.LineTo(lowerLeft);
                path.LineTo(boundingBox.Minimum);
                path.LineTo(upperRight);
                path.FinishStroke(leftBorderColor, ImDrawFlagsPath.None, borderThickness);
            }

            path.LineTo(upperRight);
            path.LineTo(boundingBox.Maximum);
            path.LineTo(lowerLeft);
            path.FinishFillConvex(colorRight);
            if (!rightBorderColor.IsTransparent)
            {
                path.LineTo(upperRight);
                path.LineTo(boundingBox.Maximum);
                path.LineTo(lowerLeft);
                path.FinishStroke(rightBorderColor, ImDrawFlagsPath.None, borderThickness);
            }

            if (!sharedBorder.IsTransparent)
            {
                path.LineTo(lowerLeft);
                path.LineTo(upperRight);
                path.FinishStroke(sharedBorder, ImDrawFlagsPath.None, Math.Max(Im.Style.FrameBorderThickness, 2));
            }
        }
        else
        {
            var upperLeftCenter  = new Vector2(boundingBox.Minimum.X + rounding, boundingBox.Minimum.Y + rounding);
            var lowerLeftCenter  = new Vector2(upperLeftCenter.X,                boundingBox.Maximum.Y - rounding);
            var upperRightCenter = new Vector2(boundingBox.Maximum.X - rounding, upperLeftCenter.Y);
            var lowerRightCenter = new Vector2(upperRightCenter.X,               lowerLeftCenter.Y);
            path.ArcTo(lowerLeftCenter, rounding, 135f / 180f * MathF.PI, 180 / 180f * MathF.PI);
            path.ArcToFast(upperLeftCenter, rounding, 6, 9);
            path.ArcTo(upperRightCenter, rounding, 270 / 180f * MathF.PI, 315 / 180f * MathF.PI);
            path.FinishFillConvex(colorLeft);
            if (!leftBorderColor.IsTransparent)
            {
                path.ArcTo(lowerLeftCenter, rounding, 135f / 180f * MathF.PI, 180 / 180f * MathF.PI);
                path.ArcToFast(upperLeftCenter, rounding, 6, 9);
                path.ArcTo(upperRightCenter, rounding, 270 / 180f * MathF.PI, 315 / 180f * MathF.PI);
                path.FinishStroke(leftBorderColor, ImDrawFlagsPath.None, borderThickness);
            }

            path.ArcTo(upperRightCenter, rounding, 315 / 180f * MathF.PI, 360 / 180f * MathF.PI);
            path.ArcToFast(lowerRightCenter, rounding, 0, 3);
            path.ArcTo(lowerLeftCenter, rounding, 90 / 180f * MathF.PI, 135 / 180f * MathF.PI);
            path.FinishFillConvex(colorRight);

            if (!rightBorderColor.IsTransparent)
            {
                path.ArcTo(upperRightCenter, rounding, 315 / 180f * MathF.PI, 360 / 180f * MathF.PI);
                path.ArcToFast(lowerRightCenter, rounding, 0, 3);
                path.ArcTo(lowerLeftCenter, rounding, 90 / 180f * MathF.PI, 135 / 180f * MathF.PI);
                path.FinishStroke(rightBorderColor, ImDrawFlagsPath.None, borderThickness);
            }

            if (!sharedBorder.IsTransparent)
            {
                var         width  = Math.Max(Im.Style.FrameBorderThickness, 2);
                const float sqrt2  = 1.4142135623731f;
                var         offset = rounding / sqrt2;
                var         start  = lowerLeftCenter + new Vector2(-offset, offset);
                var         end    = upperRightCenter + new Vector2(offset, -offset);
                start.X = MathF.Ceiling(start.X);
                start.Y = MathF.Floor(start.Y);
                end.X   = MathF.Floor(end.X);
                end.Y   = MathF.Ceiling(end.Y);
                path.LineTo(start);
                path.LineTo(end);
                path.FinishStroke(sharedBorder, ImDrawFlagsPath.None, width);
            }
        }

        var textColorLeft  = left.Text.CheckDefault(ImGuiColor.Text);
        var textColorRight = right.Text.CheckDefault(ImGuiColor.Text);
        drawList.Text(boundingBox.Minimum + Im.Style.FramePadding with { Y = 0 }, textColorLeft,  left.Label);
        drawList.Text(rightStart,                                                 textColorRight, right.Label);

        if (isHoveredLeft && !left.Tooltip.IsEmpty && left.Tooltip[0] is not 0)
        {
            using var tt = Im.Tooltip.Begin();
            Im.Text(left.Tooltip);
        }

        if (isHoveredRight && !right.Tooltip.IsEmpty && right.Tooltip[0] is not 0)
        {
            using var tt = Im.Tooltip.Begin();
            Im.Text(right.Tooltip);
        }

        if (ret)
            return isHoveredLeft ? SplitButtonHalf.UpperLeft : isHoveredRight ? SplitButtonHalf.LowerRight : SplitButtonHalf.None;

        return SplitButtonHalf.None;
    }

    /// <summary> Check the current mouse position against the diagonal incline of the split button. </summary>
    /// <param name="isHovered"> Whether the button's bounding box is hovered at all, otherwise we can skip the check. </param>
    /// <param name="boundingBox"> The button's bounding box to compute the incline. </param>
    /// <returns> Whether the button is hovered on the upper left or lower right part. </returns>
    private static (bool IsHoveredLeft, bool IsHoveredRight) DiagonalCheck(bool isHovered, in ImRect boundingBox)
    {
        if (!isHovered)
            return (false, false);

        var lowerLeft  = new Vector2(boundingBox.Minimum.X, boundingBox.Maximum.Y);
        var upperRight = new Vector2(boundingBox.Maximum.X, boundingBox.Minimum.Y);
        var incline    = (upperRight.Y - lowerLeft.Y) / (upperRight.X - lowerLeft.X);
        var mousePos   = Im.Mouse.Position;
        var x          = mousePos.X - lowerLeft.X;
        var y          = mousePos.Y - lowerLeft.Y;
        return x * incline >= y ? (true, false) : (false, true);
    }

    /// <summary> Copy ImGui ButtonEx functionality but with optional corner rounding. </summary>
    /// <param name="label"> The button's label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="size"> The minimum size of the button. </param>
    /// <param name="flags"> Flags to control the button's behavior. </param>
    /// <param name="corners"> Flags to control which corners of the button should follow the frame rounding style. </param>
    /// <returns> Whether the button was clicked this frame. </returns>
    public static bool ButtonCorners(Utf8LabelHandler label, Vector2 size, ButtonFlags flags, Corners corners)
    {
        // Copied from ImGui proper.
        var window = Im.Window.Current;
        if (window.SkipItems)
            return false;

        if (!SplitLabel(ref label, out var visibleText, out var id))
            return false;

        var screenPos = window.CursorPosition;
        // Try to vertically align buttons that are smaller/have no padding so that text baseline matches (bit hacky, since it shouldn't be a flag)
        if (flags.HasFlag(ButtonFlags.AlignTextBaseLine) && Im.Style.FramePadding.Y < window.CurrentLineTextBaseOffset)
            screenPos.Y += window.CurrentLineTextBaseOffset - Im.Style.FramePadding.Y;

        var textSize = Im.Font.CalculateSize(visibleText, false);
        size = Im.Item.CalculateSize(size, textSize + 2 * Im.Style.FramePadding);
        var boundingBox = new ImRect(screenPos, screenPos + size);
        Im.Item.SetSize(size, Im.Style.FramePadding.Y);
        if (!Im.Item.Add(boundingBox, id))
            return false;

        // Custom.
        var clicked = Im.Behavior.Button(boundingBox, id, out var hovered, out var held, flags);
        var color   = GetButtonColor(hovered, held);
        Im.Render.NavigationHighlight(boundingBox, id);
        var drawList = Im.Window.DrawList;
        drawList.Shape.RectangleFilled(boundingBox, color, Im.Style.FrameRounding, (ImDrawFlagsRectangle)corners);
        drawList.TextClipped(boundingBox.Minimum + Im.Style.FramePadding, boundingBox.Maximum - Im.Style.FramePadding, visibleText, textSize,
            Im.Style.ButtonTextAlignment);
        return clicked;
    }
}
