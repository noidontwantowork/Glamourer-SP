using ImSharp.Internal;

namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Draw the given text framed as if it were a button but without interactivity. </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    /// <param name="frameColor"> The background color of the frame. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.FrameBackground"/> is used. </param>
    /// <param name="textColor"> The color of the text. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
    /// <param name="borderColor"> The color of the frame border. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
    /// <param name="size"> The size of the frame. If 0, the text size is used. Otherwise, Text is aligned according to ButtonTextAlign and corners are rounded according to style. </param>
    public static void TextFramed(Utf8TextHandler text, Vector2 size = default, ColorParameter frameColor = default,
        ColorParameter textColor = default, ColorParameter borderColor = default)
    {
        var       textSize = CalcAndUpdateSize(ref text, ref size);
        var       rect     = Frame(size, frameColor, borderColor);
        using var color    = Im.Color.Push(ImGuiColor.Text, textColor);
        var       textRect = new Rectangle(rect.Minimum + Im.Style.FramePadding, rect.Maximum - Im.Style.FramePadding);
        Im.DrawList.Window.TextClipped(textRect, ref text, textSize, Im.Style.ButtonTextAlignment);
        Im.Item.SetSize(rect.Size, Im.Style.FramePadding.Y);
        Im.Item.Add(rect, 0, ItemFlags.ReadOnly | ItemFlags.NoNavigation);
    }

    /// <summary> Draw the given text framed as if it were a button but without interactivity. </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    /// <param name="frameColor"> The background color of the frame. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.FrameBackground"/> is used. </param>
    /// <param name="textColor"> The color of the text. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
    /// <param name="borderColor"> The color of the frame border. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
    /// <param name="size"> The size of the frame. If 0, the text size is used. Otherwise, Text is aligned according to ButtonTextAlign and corners are rounded according to style. </param>
    /// <param name="alignmentSpace"> The rectangle the text is aligned in. This should be a subset of the rectangle spanned by (0, 0) and <paramref name="size"/>. </param>
    public static void TextFramed(Utf8TextHandler text, Vector2 size, in Rectangle alignmentSpace, ColorParameter frameColor = default,
        ColorParameter textColor = default, ColorParameter borderColor = default)
    {
        var       textSize = CalcAndUpdateSize(ref text, ref size);
        var       rect     = Frame(size, frameColor, borderColor);
        using var color    = Im.Color.Push(ImGuiColor.Text, textColor);
        var       textRect = new Rectangle(alignmentSpace.Minimum + rect.Minimum, rect.Minimum + alignmentSpace.Maximum);
        Im.DrawList.Window.TextClipped(textRect, ref text, textSize, Im.Style.ButtonTextAlignment);
        Im.Item.SetSize(rect.Size, Im.Style.FramePadding.Y);
        Im.Item.Add(rect, 0, ItemFlags.ReadOnly | ItemFlags.NoNavigation);
    }

    /// <summary> Draw the given text bordered as if it were a button with a border and no background but without interactivity. </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    /// <param name="textColor"> The color of the text. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
    /// <param name="borderColor"> The color of the frame border. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Border"/> is used. </param>
    /// <param name="size"> The size of the frame. If 0, the text size is used. Otherwise, Text is aligned according to ButtonTextAlign and corners are rounded according to style. </param>
    public static void TextBordered(Utf8TextHandler text, Vector2 size = default, ColorParameter borderColor = default,
        ColorParameter textColor = default)
    {
        var textSize = CalcAndUpdateSize(ref text, ref size);
        var rect     = Im.Cursor.ScreenRectangle(size);
        Im.Render.FrameBorder(rect, borderColor.CheckDefault(ImGuiColor.Border), Im.Style.FrameRounding);
        using var color    = Im.Color.Push(ImGuiColor.Text, textColor);
        var       textRect = new Rectangle(rect.Minimum + Im.Style.FramePadding, rect.Maximum - Im.Style.FramePadding);
        Im.DrawList.Window.TextClipped(textRect, ref text, textSize, Im.Style.ButtonTextAlignment);
        Im.Item.SetSize(rect.Size, Im.Style.FramePadding.Y);
        Im.Item.Add(rect, 0, ItemFlags.ReadOnly | ItemFlags.NoNavigation);
    }

    /// <summary> Draw a frame of the given size using the default frame rounding and the given colors. </summary>
    /// <param name="size"> The desired size of the frame. </param>
    /// <param name="frameColor"> The color of the frame. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.FrameBackground"/> is used. </param>
    /// <param name="borderColor"> The color of the border. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.FrameBackground"/> is used. </param>
    /// <param name="borderThickness"> The thickness of the border. If this is null, <see cref="ImStyleSingle.FrameBorderThickness"/> is used. </param>
    /// <returns> The drawn rectangle. </returns>
    public static Rectangle Frame(Vector2 size, ColorParameter frameColor = default, ColorParameter borderColor = default,
        float? borderThickness = null)
    {
        if (size.X <= 0 || size.Y <= 0)
            return Rectangle.Zero;

        var rect      = Im.Cursor.ScreenRectangle(size);
        var style     = Im.Style.AsWritable();
        var oldBorder = style.FrameBorderThickness;
        style.FrameBorderThickness = borderThickness ?? oldBorder;
        Im.Render.Frame(rect, frameColor.CheckDefault(ImGuiColor.FrameBackground), Im.Style.FrameRounding,
            borderColor.CheckDefault(ImGuiColor.Border));
        style.FrameBorderThickness = oldBorder;
        return rect;
    }

    /// <summary> Draw text aligned to the frame, i.e. offset by <seealso cref="Im.ImGuiStyle.FramePadding"/>.Y </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static void TextFrameAligned<T>(ref Utf8StringHandler<T> text) where T : IStringHandlerBuffer
    {
        Im.Cursor.FrameAlign();
        Im.Text(ref text);
    }

    /// <summary> Draw a label for the prior item, aligned to frame and offset by inner item spacing. </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. Will stop at the first occurence of '##'. </param>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static void TextLabel(Utf8LabelHandler text)
    {
        Im.Line.SameInner();
        Im.Cursor.FrameAlign();
        if (VisibleLabel(ref text, out var visible))
            Im.Text(visible);
        else
            Im.Text(""u8);
    }

    /// <inheritdoc cref="TextLabel"/>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static void TextLabel<T>(ref Utf8StringHandler<T> text) where T : IStringHandlerBuffer
    {
        Im.Line.SameInner();
        Im.Cursor.FrameAlign();
        if (VisibleLabel(ref text, out var visible))
            Im.Text(visible);
        else
            Im.Text(""u8);
    }

    /// <inheritdoc cref="TextFrameAligned{T}"/>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static void TextFrameAligned(Utf8TextHandler text)
        => TextFrameAligned(ref text);

    /// <summary> Draw text using the monospaced font configured in the <seealso cref="ImSharpContext"/> if it is available  </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static void MonoText<T>(ref Utf8StringHandler<T> text) where T : IStringHandlerBuffer
    {
        using var mono = Im.Font.PushMono();
        Im.Text(ref text);
    }

    /// <inheritdoc cref="MonoText{T}"/>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static void MonoText(Utf8TextHandler text)
        => MonoText(ref text);

    /// <summary> Draw text aligned to the right of the current content region. </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    /// <param name="offset"> Optional additional offset from the right of the available region. </param>
    /// <param name="knownWidth"> If the width of the text is already known, you can pass it here. If this is non-positive, the width will be calculated. </param>
    /// <returns> The offset added to the horizontal cursor position before drawing. If this is negative, the text was clipped. </returns>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static float TextRightAligned(Utf8TextHandler text, float offset = 0, float knownWidth = 0)
        => TextRightAligned(ref text, offset, knownWidth);

    /// <inheritdoc cref="TextRightAligned(Utf8TextHandler,float,float)"/>
    public static float TextRightAligned<T>(ref Utf8StringHandler<T> text, float offset = 0, float knownWidth = 0)
        where T : IStringHandlerBuffer
    {
        var size      = knownWidth <= 0 ? Im.Font.CalculateSize(ref text, false).X : knownWidth;
        var available = Im.ContentRegion.Available;
        offset = available.X - size - offset;
        using var rect = offset <= 0 ? Im.Drawing.PushClipRect(Rectangle.FromSize(Im.Cursor.ScreenPosition, Im.ContentRegion.Available)) : null;
        Im.Cursor.X += offset;
        Im.Text(ref text);
        return offset;
    }

    /// <summary> Draw the given text horizontally centered in the currently remaining available content region. </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    /// <param name="knownWidth"> If the width of the text is already known, you can pass it here. If this is non-positive, the width will be calculated. </param>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static void TextCentered(Utf8TextHandler text, float knownWidth = 0)
        => TextCentered(ref text, knownWidth);

    /// <inheritdoc cref="TextCentered(Utf8TextHandler,float)"/>
    public static void TextCentered<T>(ref Utf8StringHandler<T> text, float knownWidth = 0) where T : IStringHandlerBuffer
    {
        var size      = knownWidth is 0 ? Im.Font.CalculateSize(ref text, false).X : knownWidth;
        var available = Im.ContentRegion.Available.X;
        Im.Cursor.X += (available - size) / 2;
        Im.Text(ref text);
    }

    /// <summary> Draw the same text multiple times at the cursor position to simulate a shadowed text. </summary>
    /// <param name="text"> The given text. Does not need to be null-terminated. </param>
    /// <param name="foregroundColor"> The center text color. If <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
    /// <param name="shadowColor"> The shadow color. If <see cref="ColorParameter.Default"/>, <see cref="Rgba32.Black"/> is used. </param>
    /// <param name="shadowWidth"> The width of the shadow in pixels. Should usually be 1. </param>
    public static void TextShadowed(Utf8TextHandler text, ColorParameter foregroundColor, ColorParameter shadowColor, byte shadowWidth = 1)
    {
        var       shadow   = shadowColor.CheckDefault(Rgba32.Black);
        var       position = Im.Cursor.Position;
        using var color    = ImGuiColor.Text.Push(shadow);
        for (var i = -shadowWidth; i <= shadowWidth; i++)
        {
            for (var j = -shadowWidth; j <= shadowWidth; j++)
            {
                if (i is 0 && j is 0)
                    continue;

                Im.Cursor.Position = new Vector2(position.X + i, position.Y + j);
                Im.Text(ref text);
            }
        }

        color.Pop();
        color.Push(ImGuiColor.Text, foregroundColor);
        Im.Cursor.Position = position;
        Im.Text(ref text);
    }

    /// <summary> Draw the same text multiple times at the given position in a draw list to simulate a shadowed text. </summary>
    /// <param name="drawList"> The draw list. </param>
    /// <param name="position"> The position to draw the text at in screen coordinates. </param>
    /// <param name="text"> The text to draw. </param>
    /// <param name="foregroundColor"> The center text color. If <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
    /// <param name="shadowColor"> The shadow color. If <see cref="ColorParameter.Default"/>, <see cref="Rgba32.Black"/> is used. </param>
    /// <param name="shadowWidth"> The width of the shadow in pixels. Should usually be 1. </param>
    public static void TextShadowed(Im.DrawList drawList, Vector2 position, Utf8TextHandler text,
        ColorParameter foregroundColor, ColorParameter shadowColor, byte shadowWidth = 1)
    {
        var shadow = shadowColor.CheckDefault(Rgba32.Black);
        for (var i = -shadowWidth; i <= shadowWidth; i++)
        {
            for (var j = -shadowWidth; j <= shadowWidth; j++)
            {
                if (i is 0 && j is 0)
                    continue;

                drawList.Text(position, shadow, ref text);
            }
        }

        drawList.Text(position, foregroundColor.CheckDefault(ImGuiColor.Text), ref text);
    }

    /// <summary>
    ///   Create a centered, modal help popup with the given content for the given label.
    ///   It has a centered 'Understood' button to close the window.
    /// </summary>
    /// <param name="id"> The popup ID. </param>
    /// <param name="size"> The size of the popup. </param>
    /// <param name="content"> The action to draw the content of the popup. </param>
    public static void HelpPopup(Utf8LabelHandler id, Vector2 size, Action content)
    {
        Im.Window.SetNextPosition(Im.Viewport.Main.Center, Condition.Always, new Vector2(0.5f));
        Im.Window.SetNextSize(size);
        using var pop = Im.Popup.Begin(id, WindowFlags.Modal | WindowFlags.NoResize | WindowFlags.NoMove);
        if (!pop)
            return;

        content();
        var buttonSize   = Math.Max(size.X / 5, Im.Font.CalculateButtonSize("已了解"u8).X);
        var buttonCenter = (size.X - buttonSize) / 2 - Im.Style.WindowPadding.X;
        Im.Cursor.Position = new Vector2(buttonCenter, size.Y - 1.75f * Im.Style.FrameHeight);
        if (Im.Button("已了解"u8, new Vector2(buttonSize, 0)))
            Im.Popup.CloseCurrent();
    }
}
