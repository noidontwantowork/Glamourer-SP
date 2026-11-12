// ReSharper disable MethodOverloadWithOptionalParameter

namespace ImSharp;

public static unsafe partial class Im
{
    /// <summary> Draw text. </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void Text(Utf8TextHandler text)
        => Native.Methods.Text.TextUnformatted(text.Start(out var end), end);

    /// <summary> Draw text in a given color. </summary>
    /// <param name="text"> The given text as a UTF8 string. Does not have to be null-terminated. </param>
    /// <param name="color"> The desired color as RGBA32. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void Text(Utf8TextHandler text, Rgba32 color)
    {
        var style = Style.AsWritable();
        var old   = style[ImGuiColor.Text];
        style[ImGuiColor.Text] = color.ToVector();
        Native.Methods.Text.TextUnformatted(text.Start(out var end), end);
        style[ImGuiColor.Text] = old;
    }

    /// <summary> Draw text in a given color. </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    /// <param name="color"> The desired color. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void Text(Utf8TextHandler text, in Vector4 color)
    {
        var style = Style.AsWritable();
        var old   = style[ImGuiColor.Text];
        style[ImGuiColor.Text] = color;
        Native.Methods.Text.TextUnformatted(text.Start(out var end), end);
        style[ImGuiColor.Text] = old;
    }

    /// <summary> Draw text in the <seealso cref="ImGuiColor.TextDisabled"/> color. </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void TextDisabled(Utf8TextHandler text)
    {
        var style = Style.AsWritable();
        var old   = style[ImGuiColor.Text];
        style[ImGuiColor.Text] = style[ImGuiColor.TextDisabled];
        Native.Methods.Text.TextUnformatted(text.Start(out var end), end);
        style[ImGuiColor.Text] = old;
    }

    /// <summary> Can be used to avoid copying an already established <seealso cref="Utf8TextHandler"/>. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    [OverloadResolutionPriority(100)]
    public static void Text<T>(ref Utf8StringHandler<T> text) where T : IStringHandlerBuffer
        => Native.Methods.Text.TextUnformatted(text.Start(out var end), end);

    /// <inheritdoc cref="TextWrapDisposable.Push(float)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    [OverloadResolutionPriority(100)]
    public static TextWrapDisposable PushTextWrapPosition(float localX = 0)
        => new TextWrapDisposable().Push(localX);

    /// <inheritdoc cref="TextWrapDisposable.Push(float,bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    [OverloadResolutionPriority(50)]
    public static TextWrapDisposable PushTextWrapPosition(float localX = 0, bool condition = true)
        => condition ? new TextWrapDisposable().Push(localX) : new TextWrapDisposable();

    /// <summary> Pop a number of text wrap positions. </summary>
    /// <param name="num"> The number of text wrap positions to pop. The number is not checked against the text wrap stack. </param>
    /// <remarks> Avoid using this function, and text wrap positions across scopes, as much as possible. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void PopTextWrapPositionUnsafe(int num = 1)
    {
        while (num-- > 0)
            Native.Methods.Stacks.PopTextWrapPos();
    }

    /// <summary> Draw text wrapping at the end of the available content region to the current cursor location. </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    /// <param name="wrapPosition"> <inheritdoc cref="PushTextWrapPosition(float)"/> </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void TextWrapped(Utf8TextHandler text, float wrapPosition = 0)
    {
        using var wrap = PushTextWrapPosition(wrapPosition);
        Text(ref text);
    }

    /// <inheritdoc cref="TextWrapped(Utf8TextHandler,float)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void TextWrapped<T>(ref Utf8StringHandler<T> text, float wrapPosition = 0)
        where T : IStringHandlerBuffer
    {
        using var wrap = PushTextWrapPosition(wrapPosition);
        Text(ref text);
    }

    /// <summary> Draw a bullet point for enumerations followed by the given text. </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void BulletText(Utf8TextHandler text)
        => BulletText(ref text);

    /// <summary> Draw a bullet point for enumerations followed by the given text. </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    /// <param name="color"> The color for the text. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    [OverloadResolutionPriority(50)]
    public static void BulletText(Utf8TextHandler text, Rgba32 color)
    {
        var c = ImGuiColor.Text.Push(color);
        BulletText(ref text);
        c.Pop();
    }

    /// <inheritdoc cref="BulletText(Utf8TextHandler,Rgba32)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    [OverloadResolutionPriority(100)]
    public static void BulletText(Utf8TextHandler text, Vector4 color)
    {
        var c = ImGuiColor.Text.Push(color);
        BulletText(ref text);
        c.Pop();
    }

    /// <inheritdoc cref="BulletText(Utf8TextHandler)"/>
    /// <remarks> Custom implementation based on ImGui's because ImGui uses printf formatting and varargs. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void BulletText<T>(ref Utf8StringHandler<T> text)
        where T : IStringHandlerBuffer
    {
        if (Window.Current.SkipItems)
            return;

        var size      = Font.CalculateSize(ref text);
        var totalSize = size with { X = Context.FontSize + (size.X > 0 ? size.X + Style.FramePadding.X : 0f) };
        var pos       = Window.Current.CursorPosition;
        pos.Y += Window.Current.CurrentLineTextBaseOffset;
        Item.SetSize(totalSize, 0);
        var boundingBox = Rectangle.FromSize(pos, totalSize);
        if (!Item.Add(boundingBox, 0))
            return;

        var color = ImGuiColor.Text.Get();
        // Ceil the Y-offset because it generally looks better and is more likely to be actually centered for the text.
        Window.DrawList.Render.Bullet(pos + new Vector2(Context.FontSize / 2,           (Context.FontSize + 1) / 2), color);
        Window.DrawList.Text(pos + new Vector2(Context.FontSize + Style.FramePadding.X, 0), color, ref text);
    }
}
