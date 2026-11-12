namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Draw a selectable that copies a given text on click and displays the given tooltip. </summary>
    /// <param name="text"> The displayed text. If this is a UTF8 string, it HAS to be null-terminated.</param>
    /// <param name="copiedText"> The text to copy when clicked. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="tooltip"> The text to display on hover. Does not have to be null-terminated, but will be evaluated regardless of hovering. </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void CopyOnClickSelectable(Utf8LabelHandler text, Utf8HintHandler copiedText, Utf8TextHandler tooltip)
    {
        if (Im.Selectable(ref text))
        {
            try
            {
                Im.Clipboard.Set(ref copiedText);
            }
            catch
            {
                // ignored
            }
        }

        Im.Tooltip.OnHover(tooltip.Span());
    }

    /// <inheritdoc cref="CopyOnClickSelectable(Utf8LabelHandler,Utf8HintHandler,Utf8TextHandler)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void CopyOnClickSelectable(Utf8LabelHandler text, Utf8HintHandler copiedText)
        => CopyOnClickSelectable(text, copiedText, "点击复制到剪贴板。"u8);

    /// <inheritdoc cref="CopyOnClickSelectable(Utf8LabelHandler,Utf8HintHandler,Utf8TextHandler)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void CopyOnClickSelectable(Utf8LabelHandler text)
        => CopyOnClickSelectable(text, text.Span(), "点击复制到剪贴板。"u8);
}
