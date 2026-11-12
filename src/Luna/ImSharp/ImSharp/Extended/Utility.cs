namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Get a vector scaled with the current <see cref="Im.ImGuiStyle.GlobalScale"/>. </summary>
    /// <param name="x"> The unscaled X-parameter. </param>
    /// <param name="y"> The unscaled Y-parameter. </param>
    /// <returns> The scaled vector. </returns>
    public static Vector2 ScaledVector(float x, float y)
        => new(x * Im.Style.GlobalScale, y * Im.Style.GlobalScale);

    /// <summary> Get a vector scaled with the current <see cref="Im.ImGuiStyle.GlobalScale"/> in X-direction and unscaled in Y. </summary>
    /// <param name="x"> The unscaled X-parameter. </param>
    /// <param name="y"> The Y-parameter. </param>
    /// <returns> The scaled vector. </returns>
    public static Vector2 ScaledVectorX(float x, float y = 0)
        => new(x * Im.Style.GlobalScale, y);

    /// <summary> Get a vector scaled with the current <see cref="Im.ImGuiStyle.GlobalScale"/> in Y-direction and unscaled in X. </summary>
    /// <param name="y"> The unscaled Y-parameter. </param>
    /// <param name="x"> The X-parameter. </param>
    /// <returns> The scaled vector. </returns>
    public static Vector2 ScaledVectorY(float y, float x = 0)
        => new(x, y * Im.Style.GlobalScale);

    /// <summary> Get a vector whose elements have the same value scaled with the current <see cref="Im.ImGuiStyle.GlobalScale"/>. </summary>
    /// <param name="x"> The unscaled X- and Y-parameter. </param>
    /// <returns> The scaled vector. </returns>
    public static Vector2 ScaledVector(float x)
        => new(x * Im.Style.GlobalScale);

    /// <summary> Calculate and return the size of the given text and update the given size if it is non-positive. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    internal static ImVec2 CalcAndUpdateSize<T>(ref Utf8StringHandler<T> text, ref Vector2 size) where T : IStringHandlerBuffer
    {
        var textSize = Im.Font.CalculateSize(ref text, false);
        if (size.X <= 0)
            size.X = textSize.X + 2 * Im.Style.FramePadding.X;
        if (size.Y <= 0)
            size.Y = textSize.Y + 2 * Im.Style.FramePadding.Y;
        return textSize;
    }

    /// <summary> Obtain the visible text of a label and return the full ID. </summary>
    /// <param name="text"> The text. Does not have to be null-terminated. </param>
    /// <param name="visibleText"> The visible part of the text before any occurence of '##'. </param>
    /// <param name="id"> The ID computed from the full text, or the part after a '###' (including).</param>
    /// <returns> True if the text could be obtained. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool SplitLabel<T>(ref Utf8StringHandler<T> text, out ReadOnlySpan<byte> visibleText, out ImGuiId id)
        where T : IStringHandlerBuffer
    {
        if (!text.GetSpan(out visibleText))
        {
            id = 0;
            return false;
        }

        var pounds = visibleText.IndexOf("##"u8);
        if (pounds < 0)
        {
            id = Im.Id.Get(visibleText);
            return true;
        }

        var label = visibleText.Length > pounds + 2 && visibleText[pounds + 2] is (byte)'#'
            ? visibleText[pounds..]
            : visibleText;

        visibleText = visibleText[..pounds];
        id          = Im.Id.Get(label);
        return true;
    }

    /// <summary> Obtain the visible text of a label. </summary>
    /// <param name="text"> The text. Does not have to be null-terminated. </param>
    /// <param name="visibleText"> The visible part of the text before any occurence of '##'. </param>
    /// <returns> True if the text could be obtained. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool VisibleLabel<T>(ref Utf8StringHandler<T> text, out ReadOnlySpan<byte> visibleText)
        where T : IStringHandlerBuffer
    {
        if (!text.GetSpan(out visibleText))
            return false;

        var pounds = visibleText.IndexOf("##"u8);
        if (pounds > 0)
            visibleText = visibleText[..pounds];
        return true;
    }

    /// <summary> Obtain the correct color for a frame depending on mouse state. </summary>
    /// <param name="hovered"> Whether the object is hovered. </param>
    /// <param name="held"> Whether a mouse button is held down on the object. </param>
    /// <returns> The frame color. </returns>
    public static Rgba32 GetFrameBackgroundColor(bool hovered, bool held)
        => Im.Color.Get((hovered, held) switch
        {
            (true, true)  => ImGuiColor.FrameBackgroundActive,
            (true, false) => ImGuiColor.FrameBackgroundHovered,
            _             => ImGuiColor.FrameBackground,
        });

    /// <summary> Obtain the correct color for a button depending on mouse state. </summary>
    /// <param name="hovered"> Whether the button is hovered. </param>
    /// <param name="held"> Whether a mouse button is held down on the button. </param>
    /// <returns> The frame color. </returns>
    public static Rgba32 GetButtonColor(bool hovered, bool held)
        => Im.Color.Get((hovered, held) switch
        {
            (true, true)  => ImGuiColor.ButtonActive,
            (true, false) => ImGuiColor.ButtonHovered,
            _             => ImGuiColor.Button,
        });
}
