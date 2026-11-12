namespace ImSharp;

public static unsafe partial class Im
{
    public static class Clipboard
    {
        /// <summary> Copy the given text to the clipboard. </summary>
        /// <param name="text"> The text as a UTF8 string. HAS to be null-terminated. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void Set(Utf8TextHandler text)
            => Native.Methods.Clipboard.SetClipboardText(text.Start());

        /// <inheritdoc cref="Set(Utf8TextHandler)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void Set<T>(ref Utf8StringHandler<T> text) where T : IStringHandlerBuffer
            => Native.Methods.Clipboard.SetClipboardText(text.Start());

        /// <summary> Obtain the current text from the clipboard. </summary>
        /// <returns> A non-owned view into the current clipboard text up to the null-terminator on success. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ReadOnlySpan<byte> Get()
            => NullTerminationHelpers.GetSpan(Native.Methods.Clipboard.GetClipboardText());

        /// <summary> Obtain the current text from the clipboard. </summary>
        /// <returns> An owned, null-terminated span of UTF8 text. An empty (still null-terminated) span on failure. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static StringU8 GetCopy()
            => NullTerminationHelpers.GetClone(Native.Methods.Clipboard.GetClipboardText());

        /// <inheritdoc cref="Get"/>
        /// <returns> A UTF16 string of the current clipboard text. An empty string on failure. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static string GetUtf16()
            => NullTerminationHelpers.GetString(Native.Methods.Clipboard.GetClipboardText());
    }
}
