namespace ImSharp;

public static partial class Im
{
    /// <summary> A reference to runtime data for fonts and their rendering. </summary>
    /// <param name="pointer"> The native pointer to the font. </param>
    public readonly unsafe ref struct Font(Native.ImFont* pointer)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Native.ImFont* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator Font(Native.ImFont* pointer)
            => new(pointer);

        /// <summary> The character to use if a glyph is not found in the font. </summary>
        public ImWchar FallbackCharacter
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->FallbackChar;
        }

        /// <summary> The character to use for ellipsis rendering. </summary>
        public ImWchar EllipsisCharacter
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->EllipsisChar;
        }

        /// <summary> The single dot character to use for ellipsis rendering if a single <seealso cref="EllipsisCharacter"/> is not found. </summary>
        public ImWchar DotCharacter
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->DotChar;
        }

        /// <summary> The base font scale, multiplied with the per-window font scale. </summary>
        public float Scale
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->Scale;
        }

        /// <summary> The font size. </summary>
        public float Size
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->FontSize;
        }

        /// <inheritdoc cref="FontDisposable.Push(Im.Font)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public FontDisposable Push()
            => new FontDisposable().Push(this);

        /// <inheritdoc cref="FontDisposable.Push(Im.Font,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public FontDisposable Push(bool condition)
            => new FontDisposable().Push(this, condition);

        /// <inheritdoc cref="FontDisposable.Push(ImSharp.Im.Font,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static FontDisposable Push(Font font, bool condition)
            => new FontDisposable().Push(font, condition);

        /// <inheritdoc cref="FontDisposable.Push(ImSharp.Im.Font)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static FontDisposable Push(Font font)
            => new FontDisposable().Push(font);

        /// <summary> Get the current font. </summary>
        public static Font Current
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Style.GetFont();
        }

        /// <summary> Get the configured monospaced font, see <see cref="ImSharpContext.MonoFont"/>. </summary>
        public static Font Mono
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => (Native.ImFont*)ImSharpConfiguration.Context->MonoFont;
        }

        /// <summary> Get the configured default font, see <see cref="ImSharpContext.DefaultFont"/>. </summary>
        public static Font Default
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => (Native.ImFont*)ImSharpConfiguration.Context->DefaultFont;
        }

        /// <summary> Push the monospaced font configured in the <seealso cref="ImSharpContext"/> if it is available. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static FontDisposable PushMono()
        {
            if (ImSharpConfiguration.Context->MonoFont is null)
                return new FontDisposable();

            return Push((Font)ImSharpConfiguration.Context->MonoFont);
        }

        /// <summary> Push the default font configured in the <seealso cref="ImSharpContext"/> if it is available. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static FontDisposable PushDefault()
        {
            if (ImSharpConfiguration.Context->DefaultFont is null)
                return new FontDisposable();

            return Push((Font)ImSharpConfiguration.Context->DefaultFont);
        }

        /// <summary> Get the current font size. </summary>
        /// <remarks> The font size is the height in pixels with the current scale applied. </remarks>
        public static float CurrentSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Style.GetFontSize();
        }

        /// <summary> Get a texture ID for a white pixel in the current font. </summary>
        public static ImTextureId WhitePixelId
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Style.GetFontTexIdWhitePixel();
        }

        /// <summary> Get te UV coordinates for a white pixel in the current font. </summary>
        public static Vector2 WhitePixelUv
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImVec2 ret;
                Native.Methods.Style.GetFontTexUvWhitePixel(&ret);
                return ret;
            }
        }

        /// <summary> Calculate the required size to display the given text. </summary>
        /// <param name="text"> The given text as text. Does not have to be null-terminated. </param>
        /// <param name="hideTextAfterDashes"> Whether everything after the first ## is to be included or not. </param>
        /// <param name="wrapWidth"> The text wrap width to use for wrapping. 0 uses the current wrapping position, if any. </param>
        /// <returns> The required size to display the text. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static Vector2 CalculateSize(Utf8TextHandler text, bool hideTextAfterDashes = true, float wrapWidth = 0)
        {
            ImVec2 ret;
            Native.Methods.Text.CalcTextSize(&ret, text.Start(out var end), end, hideTextAfterDashes, wrapWidth);
            return ret;
        }

        /// <summary> Calculate the required size to display the given text as a button. </summary>
        /// <param name="text"> The given text as text. Does not have to be null-terminated. </param>
        /// <param name="wrapWidth"> The text wrap width to use for wrapping. 0 uses the current wrapping position, if any. </param>
        /// <returns> The required size to display the button. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static Vector2 CalculateButtonSize(Utf8TextHandler text, float wrapWidth = 0)
        {
            ImVec2 ret;
            Native.Methods.Text.CalcTextSize(&ret, text.Start(out var end), end, true, wrapWidth);
            ret += 2 * Style.FramePadding;
            return ret;
        }

        /// <inheritdoc cref="CalculateSize(Utf8TextHandler,bool,float)"/>
        /// <typeparam name="T"> The buffer type. </typeparam>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static Vector2 CalculateSize<T>(ref Utf8StringHandler<T> text, bool hideTextAfterDashes = true, float wrapWidth = 0)
            where T : IStringHandlerBuffer
        {
            ImVec2 ret;
            Native.Methods.Text.CalcTextSize(&ret, text.Start(out var end), end, hideTextAfterDashes, wrapWidth);
            return ret;
        }

        /// <summary> Calculate the required size to display the given text and return the cloned transcoded text. </summary>
        /// <param name="text"> The given text as text. Does not have to be null-terminated. </param>
        /// <param name="formatted"> The transcoded text as null-terminated UTF8. </param>
        /// <param name="hideTextAfterDashes"> Whether everything after the first ## is to be included or not. </param>
        /// <param name="wrapWidth"> The text wrap width to use for wrapping. 0 uses the current wrapping position, if any. </param>
        /// <returns> The required size to display the text. </returns>
        /// <remarks> Only use this if the transcoding is expected to be more expensive than the allocation of a clone. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static Vector2 CalculateSize(Utf8TextHandler text, out StringU8 formatted, bool hideTextAfterDashes = true,
            float wrapWidth = 0)
        {
            ImVec2 ret;
            formatted = text.Span().CloneNullTerminated();
            Native.Methods.Text.CalcTextSize(&ret, text.Start(out var end), end, hideTextAfterDashes, wrapWidth);
            return ret;
        }

        /// <summary> Calculate the required size to display the given text using this font. If this is a null-reference, use the current font. </summary>
        /// <param name="text"> The given text as text. Does not have to be null-terminated. </param>
        /// <param name="hideTextAfterDashes"> Whether everything after the first ## is to be included or not. </param>
        /// <param name="wrapWidth"> The text wrap width to use for wrapping. 0 uses the current wrapping position, if any. </param>
        /// <returns> The required size to display the text. </returns>
        public Vector2 CalculateTextSize(Utf8TextHandler text, bool hideTextAfterDashes = true, float wrapWidth = 0)
        {
            byte* start, end;
            if (hideTextAfterDashes && ImEx.VisibleLabel(ref text, out var visible))
                start = visible.Start(out end);
            else
                start = text.Start(out end);

            if (start == end)
                return new Vector2(0, Size);

            ImVec2 size;
            // fontSize computed as in ImGui.
            var window       = Window.Current.Pointer;
            var fontBaseSize = Math.Max(1, Io.Pointer->FontGlobalScale * Size * Scale);
            var fontSize     = fontBaseSize * window->FontWindowScale;
            if (window->ParentWindow is not null)
                fontSize *= window->ParentWindow->FontWindowScale;

            Native.ImFont.CalcTextSize(&size, Pointer, fontSize, float.MaxValue, wrapWidth, start, end, null);
            return size;
        }

        /// <summary> Get the cursor advance, or width, of a single character. </summary>
        /// <param name="character"> The character. </param>
        /// <returns> The cursor advance. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public float GetCharacterAdvance(char character)
            => Native.ImFont.GetCharAdvance(Pointer, new ImWchar(character));
    }
}
