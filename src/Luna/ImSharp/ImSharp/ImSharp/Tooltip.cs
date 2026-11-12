namespace ImSharp;

public static partial class Im
{
    public static class Tooltip
    {
        /// <inheritdoc cref="TooltipDisposable(bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static TooltipDisposable Begin()
            => new(true);

        /// <summary> Add the given text to the tooltip. </summary>
        /// <param name="text"> The tooltip text as text. Does not have to be null-terminated. </param>
        /// <param name="pushDefault"> If this is set, pushes text color, border color, frame padding, window padding and item spacing to their default values if they are changed. </param>
        /// <param name="font"> If this is not null, push the given font before the text. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe void Set(Utf8TextHandler text, bool pushDefault = false, Font font = default)
        {
            using var style = DefaultStyle(pushDefault);
            using var tt    = Begin();
            using var f     = Font.Push(font, font.Pointer is not null);
            Text(text);
        }

        /// <inheritdoc cref="Set(Utf8TextHandler,bool,Font)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe void Set(ref Utf8TextHandler text, bool pushDefault = false, Font font = default)
        {
            using var style = DefaultStyle(pushDefault);
            using var tt    = Begin();
            using var f     = Font.Push(font, font.Pointer is not null);
            Text(text);
        }

        /// <summary> Add the given text to a tooltip when the prior item is hovered. </summary>
        /// <param name="flags"> The flags to check on hovering. </param>
        /// <param name="text"> The tooltip text as UTF8 string. Does not have to be null-terminated. </param>
        /// <param name="pushDefault"> If this is set, pushes text color, border color, frame padding, window padding and item spacing to their default values if they are changed. </param>
        /// <param name="font"> If this is not null, push the given font before the text. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe void OnHover(HoveredFlags flags, ReadOnlySpan<byte> text, bool pushDefault = false, Font font = default)
        {
            if (text.Length is 0 || text[0] is 0 || !Native.Methods.Items.IsItemHovered(flags))
                return;

            using var style = DefaultStyle(pushDefault);
            using var tt    = Begin();
            using var f     = Font.Push(font, font.Pointer is not null);
            Native.Methods.Text.TextUnformatted(text.Start(out var end), end);
        }

        /// <inheritdoc cref="ImageOnHover(ImTextureId,Vector2,HoveredFlags,ref HoverUtf8StringHandler)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ImageOnHover(ImTextureId image, Vector2 size, HoveredFlags flags = HoveredFlags.None)
        {
            if (!Native.Methods.Items.IsItemHovered(flags))
                return;

            using var enabled = Enabled();
            using var tt      = Begin();
            Image.Draw(image, size);
        }

        /// <inheritdoc cref="ImageOnHover(ImTextureId,Vector2,HoveredFlags,ref HoverUtf8StringHandler)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ImageOnHover(ImTextureId image, Vector2 size, ReadOnlySpan<byte> text, HoveredFlags flags = HoveredFlags.None)
        {
            if (!Native.Methods.Items.IsItemHovered(flags))
                return;

            using var enabled = Enabled();
            using var tt      = Begin();
            Image.Draw(image, size);
            if (text.Length > 0)
                Text(text);
        }

        /// <summary> Draw an image of a given size when hovering the last item. </summary>
        /// <param name="image"> The ID of the image. </param>
        /// <param name="size"> The size to scale the image to in pixels. </param>
        /// <param name="text"> The tooltip text as interpolated string. This will only get evaluated if the item is hovered. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe void ImageOnHover(ImTextureId image, Vector2 size, ref HoverUtf8StringHandler text)
        {
            if (!text.IsHovered)
                return;

            using var enabled = Enabled();
            using var tt      = Begin();
            Image.Draw(image, size);
            if (!text.GetEnd(out var end) || *text.Begin is 0)
                return;

            Native.Methods.Text.TextUnformatted(text.Begin, end);
        }

        /// <summary> Draw an image of a given size when hovering the last item. </summary>
        /// <param name="image"> The ID of the image. </param>
        /// <param name="size"> The size to scale the image to in pixels. </param>
        /// <param name="flags"> The flags to check on hovering. </param>
        /// <param name="text"> The tooltip text as interpolated string. This will only get evaluated if the item is hovered. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        // ReSharper disable once EntityNameCapturedOnly.Global
        public static unsafe void ImageOnHover(ImTextureId image, Vector2 size, HoveredFlags flags,
            [InterpolatedStringHandlerArgument(nameof(flags))]
            ref HoverUtf8StringHandler text)
        {
            if (!text.IsHovered)
                return;

            using var enabled = Enabled();
            using var tt      = Begin();
            Image.Draw(image, size);
            if (!text.GetEnd(out var end) || *text.Begin is 0)
                return;

            Native.Methods.Text.TextUnformatted(text.Begin, end);
        }


        /// <inheritdoc cref="OnHover(HoveredFlags,ReadOnlySpan{byte},bool,Font)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public static void OnHover(ReadOnlySpan<byte> text, bool pushDefault = false, Font font = default)
            => OnHover(HoveredFlags.None, text, pushDefault, font);

        /// <summary> Add the given text to a tooltip when the prior item is hovered. </summary>
        /// <param name="flags"> The flags to check on hovering. </param>
        /// <param name="text"> The tooltip text as UTF16 string. </param>
        /// <param name="pushDefault"> If this is set, pushes text color, border color, frame padding, window padding and item spacing to their default values if they are changed. </param>
        /// <param name="font"> If this is not null, push the given font before the text. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe void OnHover(HoveredFlags flags, ReadOnlySpan<char> text, bool pushDefault = false, Font font = default)
        {
            if (text.Length is 0 || text[0] is '\0' || !Native.Methods.Items.IsItemHovered(flags))
                return;

            text.CopyInto<TextStringHandlerBuffer>(out var length);
            using var style = DefaultStyle(pushDefault);
            using var tt    = Begin();
            using var f     = Font.Push(font, font.Pointer is not null);
            Native.Methods.Text.TextUnformatted(TextStringHandlerBuffer.Buffer, TextStringHandlerBuffer.Buffer + length);
        }

        /// <inheritdoc cref="OnHover(HoveredFlags,ReadOnlySpan{byte},bool,Font)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void OnHover(ReadOnlySpan<char> text, bool pushDefault = false, Font font = default)
            => OnHover(HoveredFlags.None, text, pushDefault, font);

        /// <inheritdoc cref="OnHover(HoveredFlags,ReadOnlySpan{byte},bool,Font)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void OnHover(Utf8TextHandler text, HoveredFlags flags = HoveredFlags.None, bool pushDefault = false, Font font = default)
            => OnHover(ref text, flags, pushDefault, font);

        /// <summary> Add the given text to a tooltip when the prior item is hovered. </summary>
        /// <param name="flags"> The flags to check on hovering. </param>
        /// <param name="text"> The tooltip text as interpolated string. This will only get evaluated if the item is hovered. </param>
        /// <param name="pushDefault"> If this is set, pushes text color, border color, frame padding, window padding and item spacing to their default values if they are changed. </param>
        /// <param name="font"> If this is not null, push the given font before the text. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(50)]
        // ReSharper disable once EntityNameCapturedOnly.Global
        public static unsafe void OnHover(HoveredFlags flags,
            [InterpolatedStringHandlerArgument(nameof(flags))]
            ref HoverUtf8StringHandler text, bool pushDefault = false, Font font = default)
        {
            if (!text.GetEnd(out var end) || *text.Begin is 0)
                return;

            using var style = DefaultStyle(pushDefault);
            using var tt    = Begin();
            using var f     = Font.Push(font, font.Pointer is not null);
            Native.Methods.Text.TextUnformatted(text.Begin, end);
        }

        /// <inheritdoc cref="OnHover(HoveredFlags,ref HoverUtf8StringHandler,bool,Font)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe void OnHover<T>(ref Utf8StringHandler<T> text, HoveredFlags flags = HoveredFlags.None, bool pushDefault = false,
            Font font = default)
            where T : IStringHandlerBuffer
        {
            if (!Native.Methods.Items.IsItemHovered(flags))
                return;

            var start = text.Start(out var end);
            if (*text.Begin is 0 || start == end)
                return;

            using var style = DefaultStyle(pushDefault);
            using var tt    = Begin();
            using var f     = Font.Push(font, font.Pointer is not null);
            Native.Methods.Text.TextUnformatted(start, end);
        }

        [MethodImpl(ImSharpConfiguration.OptInl)]
        private static ColorStyleDisposable? DefaultStyle(bool pushDefault)
        {
            if (!pushDefault)
                return null;

            return new ColorStyleDisposable()
                .PushDefault(ImGuiColor.Text)
                .PushDefault(ImGuiColor.Border)
                .PushDefault(ImStyleDouble.FramePadding)
                .PushDefault(ImStyleDouble.WindowPadding)
                .PushDefault(ImStyleDouble.ItemSpacing);
        }
    }
}
