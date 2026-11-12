using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper class around Combo functions. </summary>
    public static unsafe class Combo
    {
        /// <inheritdoc cref="ComboDisposable(ref Utf8LabelHandler,ref Utf8TextHandler,ComboFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ComboDisposable Begin(Utf8LabelHandler label, Utf8TextHandler preview, ComboFlags flags = ComboFlags.None)
            => new(ref label, ref preview, flags);

        /// <summary> Draw a simple combo of items. </summary>
        /// <param name="label"> The combo label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="currentIndex"> The index of the currently selected item used for the preview string and highlighting the item. </param>
        /// <param name="itemsSeparatedByZeros"> The list of item strings separated by '\0' bytes and terminated by a doubled '\0' byte. </param>
        /// <param name="popupMaxHeight"> The maximum height of the combo popup in items. If non-positive, this is automatically calculated. </param>
        /// <returns> True if a new item was selected in this frame, in which case <paramref name="currentIndex"/> will have changed. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Draw(Utf8LabelHandler label, ref int currentIndex, Utf8TextHandler itemsSeparatedByZeros, int popupMaxHeight = -1)
            => Native.Methods.Widgets.Combo(label.Start(), (int*)Unsafe.AsPointer(ref currentIndex), itemsSeparatedByZeros.Start(),
                popupMaxHeight);

        /// <summary> Draw a simple combo of items. </summary>
        /// <param name="label"> The combo label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="currentIndex"> The index of the currently selected item used for the preview string and highlighting the item. </param>
        /// <param name="flags"> Flags controlling the behavior of the combo. </param>
        /// <param name="items"> The list of items to display as UTF8 strings. </param>
        /// <returns> True if a new item was selected in this frame, in which case <paramref name="currentIndex"/> will have changed. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Draw(Utf8LabelHandler label, ref int currentIndex, ComboFlags flags, params IReadOnlyList<StringU8> items)
            => Draw(ref label, ref currentIndex, flags, items);

        /// <inheritdoc cref="Draw(Utf8LabelHandler,ref int,ComboFlags,IReadOnlyList{StringU8})"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Draw(Utf8LabelHandler label, ref int currentIndex, params IReadOnlyList<StringU8> items)
            => Draw(ref label, ref currentIndex, ComboFlags.None, items);

        /// <summary> Draw a simple combo of items. </summary>
        /// <param name="label"> The combo label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="currentIndex"> The currently selected item used for the preview string and highlighting the item. </param>
        /// <param name="flags"> Flags controlling the behavior of the combo. </param>
        /// <param name="items"> The list of items to display as UTF16 strings. </param>
        /// <returns> True if a new item was selected in this frame, in which case <paramref name="currentIndex"/> will have changed. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Draw(Utf8LabelHandler label, ref int currentIndex, ComboFlags flags, params IReadOnlyList<string> items)
            => Draw(ref label, ref currentIndex, flags, items);

        /// <inheritdoc cref="Draw(Utf8LabelHandler,ref int,ComboFlags,IReadOnlyList{string})"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Draw(Utf8LabelHandler label, ref int currentIndex, params IReadOnlyList<string> items)
            => Draw(ref label, ref currentIndex, ComboFlags.None, items);

        /// <summary> Draw only the preview part of a combo. </summary>
        /// <param name="label"> The label of the combo. Does not have to be null-terminated. </param>
        /// <param name="preview"> The preview text displayed. Does not have to be null-terminated. </param>
        /// <param name="popupId"> The ID of the associated popup if the combo was drawn. </param>
        /// <param name="boundingBox"> The bounding box of the drawn widget without the label. </param>
        /// <param name="flags"> Additional flags to control the combos behaviour. </param>
        /// <param name="alignment"> The alignment of the preview text. </param>
        /// <returns> True if the combo was clicked in this frame, regardless of the popup status. </returns>
        /// <remarks>
        ///   Only use this if you need to separate the behavior of the combo preview from the popup itself.<br/>
        ///   Call <see cref="DrawPopup"/> with the returned ID afterward.<br/>
        ///   You do not generally need to care about the return value.<br/>
        ///   This is effectively copied from the original ImGui code for combos, just without opening the popup.
        /// </remarks>
        public static bool DrawPreview(Utf8LabelHandler label, Utf8HintHandler preview, out ImGuiId popupId, out Rectangle boundingBox,
            ComboFlags flags = ComboFlags.None, Vector2 alignment = default)
        {
            var window = Window.Current;
            popupId     = 0;
            boundingBox = default;
            if (window.SkipItems)
                return false;

            var context     = Context;
            var windowFlags = context.Pointer->NextWindowData.Flags;
            context.Pointer->NextWindowData.Flags = NextWindowDataFlags.None;
            ImEx.SplitLabel(ref label, out var textLabel, out var id);
            var hasPreview  = !flags.HasFlag(ComboFlags.NoPreview);
            var hasArrow    = !flags.HasFlag(ComboFlags.NoArrowButton);
            var labelSize   = Font.CalculateSize(textLabel);
            var arrowSize   = hasArrow ? Style.FrameHeight : 0;
            var widgetWidth = hasPreview ? Item.CalculateWidth() : arrowSize;
            var widgetSize  = new Vector2(widgetWidth, labelSize.Y + 2 * Style.FramePadding.Y);
            var totalSize   = widgetSize + new Vector2(labelSize.X > 0 ? labelSize.X + Style.ItemInnerSpacing.X : 0, 0);
            boundingBox = Rectangle.FromSize(window.CursorPosition, widgetSize);
            var totalBoundingBox = Rectangle.FromSize(window.CursorPosition, totalSize);
            Item.SetSize(totalSize, Style.FramePadding.Y);
            if (!Item.Add(totalBoundingBox, id, boundingBox))
                return false;

            var pressed = Behavior.Button(boundingBox, id, out var hovered, out _);
            popupId = Id.Calculate("##ComboPopup"u8, id);
            var popupOpen = Popup.IsOpen(popupId);
            if (pressed && !popupOpen)
            {
                Popup.Open(popupId);
                popupOpen = true;
            }

            var frameColor = hovered ? ImGuiColor.FrameBackgroundHovered.Get() : ImGuiColor.FrameBackground.Get();
            Render.NavigationHighlight(boundingBox, id);
            var drawList  = Window.DrawList;
            var arrowEnd  = Math.Max(boundingBox.Minimum.X, boundingBox.Maximum.X - arrowSize);
            var textColor = ImGuiColor.Text.Get();
            if (hasPreview)
                drawList.Shape.RectangleFilled(boundingBox.Minimum, boundingBox.Maximum with { X = arrowEnd }, frameColor, Style.FrameRounding,
                    widgetWidth <= arrowSize || !hasArrow ? ImDrawFlagsRectangle.RoundCornersAll : ImDrawFlagsRectangle.RoundCornersLeft);
            if (hasArrow)
            {
                var button = popupOpen || hovered ? ImGuiColor.ButtonHovered.Get() : ImGuiColor.Button.Get();
                drawList.Shape.RectangleFilled(boundingBox.Minimum with { X = arrowEnd }, boundingBox.Maximum, button, Style.FrameRounding,
                    widgetWidth <= arrowSize ? ImDrawFlagsRectangle.RoundCornersAll : ImDrawFlagsRectangle.RoundCornersRight);
                if (arrowEnd + arrowSize - Style.FramePadding.X <= boundingBox.Maximum.X)
                    drawList.Render.Arrow(new Vector2(arrowEnd + Style.FramePadding.Y, boundingBox.Minimum.Y + Style.FramePadding.Y), textColor,
                        Direction.Down, 1f);
            }

            Render.FrameBorder(boundingBox, default, Style.FrameRounding);

            if (hasPreview && preview.Start(out var end) - end < 0)
                drawList.TextClipped(boundingBox.Minimum + Style.FramePadding, boundingBox.Maximum with { X = arrowEnd }, ref preview, null,
                    alignment);

            if (labelSize.X > 0)
                drawList.Text(new Vector2(boundingBox.Maximum.X + Style.ItemInnerSpacing.X, boundingBox.Minimum.Y + Style.FramePadding.Y),
                    textColor, textLabel);

            if (!popupOpen)
                return false;

            context.Pointer->NextWindowData.Flags = windowFlags;
            return pressed;
        }

        /// <inheritdoc cref="ComboDisposable(ImGuiId,in Rectangle,ComboFlags)"/>
        public static ComboDisposable DrawPopup(ImGuiId id, in Rectangle boundingBox, ComboFlags flags = ComboFlags.None)
            => new(id, boundingBox, flags);

        /// <summary> Draw a combo over all valid entries for an Enum type using the enums <seealso cref="Enum.ToString()"/> for names. </summary>
        /// <typeparam name="T"> The Enum type. </typeparam>
        /// <param name="label"> The combo label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="currentValue"> The currently selected value used for the preview string and highlighting the item. </param>
        /// <param name="flags"> Flags controlling the behavior of the combo. </param>
        /// <returns> True if a new item was selected in this frame, in which case <paramref name="currentValue"/> will have changed. </returns>
        [MethodImpl(ImSharpConfiguration.Opt)]
        public static bool DrawEnum<T>(Utf8LabelHandler label, ref T currentValue, ComboFlags flags = ComboFlags.None) where T : unmanaged, Enum
        {
            using var combo = new ComboDisposable(ref label, $"{currentValue}", flags);
            if (!combo)
                return false;

            var ret = false;

            foreach (var value in EnumExtensions.get_Values<T>())
            {
                var equal = EqualityComparer<T>.Default.Equals(value, currentValue);
                if (Selectable($"{value}", equal) && !equal)
                {
                    currentValue = value;
                    ret          = true;
                }
            }

            return ret;
        }

        /// <summary> Draw a combo over all valid entries for an Enum type using a custom string function for names. </summary>
        /// <typeparam name="T"> The Enum type. </typeparam>
        /// <param name="label"> The combo label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="currentValue"> The currently selected value used for the preview string and highlighting the item. </param>
        /// <param name="toName"> The method converting the enum value to a UTF8 string which has to be null-terminated. </param>
        /// <param name="flags"> Flags controlling the behavior of the combo. </param>
        /// <returns> True if a new item was selected in this frame, in which case <paramref name="currentValue"/> will have changed. </returns>
        [MethodImpl(ImSharpConfiguration.Opt)]
        public static bool DrawEnum<T>(Utf8LabelHandler label, ref T currentValue, Func<T, ReadOnlySpan<byte>> toName,
            ComboFlags flags = ComboFlags.None) where T : unmanaged, Enum
        {
            var       handler = (Utf8TextHandler)toName(currentValue);
            using var combo   = new ComboDisposable(ref label, ref handler, flags);
            if (!combo)
                return false;

            var ret = false;

            foreach (var value in EnumExtensions.get_Values<T>())
            {
                var equal = EqualityComparer<T>.Default.Equals(value, currentValue);
                if (Selectable(toName(value), equal) && !equal)
                {
                    currentValue = value;
                    ret          = true;
                }
            }

            return ret;
        }

        /// <summary> Draw a combo over all valid entries for an Enum type using a custom string function for names. </summary>
        /// <typeparam name="T"> The Enum type. </typeparam>
        /// <param name="label"> The combo label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="currentValue"> The currently selected value used for the preview string and highlighting the item. </param>
        /// <param name="toName"> The method converting the enum value to a UTF8 string. </param>
        /// <param name="flags"> Flags controlling the behavior of the combo. </param>
        /// <returns> True if a new item was selected in this frame, in which case <paramref name="currentValue"/> will have changed. </returns>
        [MethodImpl(ImSharpConfiguration.Opt)]
        public static bool DrawEnum<T>(Utf8LabelHandler label, ref T currentValue, Func<T, StringU8> toName,
            ComboFlags flags = ComboFlags.None) where T : unmanaged, Enum
        {
            var       handler = (Utf8TextHandler)toName(currentValue);
            using var combo   = new ComboDisposable(ref label, ref handler, flags);
            if (!combo)
                return false;

            var ret = false;

            foreach (var value in EnumExtensions.get_Values<T>())
            {
                var equal = EqualityComparer<T>.Default.Equals(value, currentValue);
                if (Selectable(toName(value), equal) && !equal)
                {
                    currentValue = value;
                    ret          = true;
                }
            }

            return ret;
        }

        /// <summary> Draw a combo over all valid entries for an Enum type using a custom string function for names. </summary>
        /// <typeparam name="T"> The Enum type. </typeparam>
        /// <param name="label"> The combo label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="currentValue"> The currently selected value used for the preview string and highlighting the item. </param>
        /// <param name="toName"> The method converting the enum value to a UTF8 label string. </param>
        /// <param name="toTooltip"> The method converting the enum value to a UTF8 tooltip string. </param>
        /// <param name="flags"> Flags controlling the behavior of the combo. </param>
        /// <returns> True if a new item was selected in this frame, in which case <paramref name="currentValue"/> will have changed. </returns>
        [MethodImpl(ImSharpConfiguration.Opt)]
        public static bool DrawEnum<T>(Utf8LabelHandler label, ref T currentValue, Func<T, StringU8> toName, Func<T, StringU8> toTooltip,
            ComboFlags flags = ComboFlags.None) where T : unmanaged, Enum
        {
            var       handler = (Utf8TextHandler)toName(currentValue);
            using var combo   = new ComboDisposable(ref label, ref handler, flags);
            if (!combo)
                return false;

            var ret = false;

            foreach (var value in EnumExtensions.get_Values<T>())
            {
                var equal = EqualityComparer<T>.Default.Equals(value, currentValue);
                if (Selectable(toName(value), equal) && !equal)
                {
                    currentValue = value;
                    ret          = true;
                }

                Tooltip.OnHover(toTooltip(value));
            }

            return ret;
        }

        /// <summary> Draw a combo over all valid entries for an Enum type using a custom string function for names. </summary>
        /// <typeparam name="T"> The Enum type. </typeparam>
        /// <param name="label"> The combo label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="currentValue"> The currently selected value used for the preview string and highlighting the item. </param>
        /// <param name="toName"> The method converting the enum value to a UTF16 string. </param>
        /// <param name="flags"> Flags controlling the behavior of the combo. </param>
        /// <returns> True if a new item was selected in this frame, in which case <paramref name="currentValue"/> will have changed. </returns>
        [MethodImpl(ImSharpConfiguration.Opt)]
        public static bool DrawEnum<T>(Utf8LabelHandler label, ref T currentValue, Func<T, ReadOnlySpan<char>> toName,
            ComboFlags flags = ComboFlags.None) where T : unmanaged, Enum
        {
            var       handler = (Utf8TextHandler)toName(currentValue);
            using var combo   = new ComboDisposable(ref label, ref handler, flags);
            if (!combo)
                return false;

            var ret = false;

            foreach (var value in EnumExtensions.get_Values<T>())
            {
                var equal = EqualityComparer<T>.Default.Equals(value, currentValue);
                if (Selectable(toName(value), equal) && !equal)
                {
                    currentValue = value;
                    ret          = true;
                }
            }

            return ret;
        }

        /// <inheritdoc cref="DrawEnum{T}(Utf8LabelHandler,ref T,Func{T,ReadOnlySpan{char}},ComboFlags)"/>
        [MethodImpl(ImSharpConfiguration.Opt)]
        public static bool DrawEnum<T>(Utf8LabelHandler label, ref T currentValue, Func<T, string> toName,
            ComboFlags flags = ComboFlags.None) where T : unmanaged, Enum
        {
            var       handler = (Utf8TextHandler)toName(currentValue);
            using var combo   = new ComboDisposable(ref label, ref handler, flags);
            if (!combo)
                return false;

            var ret = false;

            foreach (var value in EnumExtensions.get_Values<T>())
            {
                var equal = EqualityComparer<T>.Default.Equals(value, currentValue);
                if (Selectable(toName(value), equal) && !equal)
                {
                    currentValue = value;
                    ret          = true;
                }
            }

            return ret;
        }

        /// <summary> Draw a combo over the given items using <seealso cref="object.ToString()"/> for names. </summary>
        /// <typeparam name="T"> The item type. </typeparam>
        /// <param name="label"> The combo label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="currentValue"> The currently selected value used for the preview string and highlighting the item. </param>
        /// <param name="flags"> Flags controlling the behavior of the combo. </param>
        /// <param name="items"> The list of items to draw as selectables. </param>
        /// <returns> True if a new item was selected in this frame, in which case <paramref name="currentValue"/> will have changed. </returns>
        [MethodImpl(ImSharpConfiguration.Opt)]
        public static bool DrawItems<T>(Utf8LabelHandler label, ref T currentValue, ComboFlags flags, params IEnumerable<T> items)
            where T : IEquatable<T>
        {
            using var combo = new ComboDisposable(ref label, $"{currentValue}", flags);
            if (!combo)
                return false;

            var ret = false;

            foreach (var value in items)
            {
                var equal = value.Equals(currentValue);
                if (Selectable($"{value}", equal) && !equal)
                {
                    currentValue = value;
                    ret          = true;
                }
            }

            return ret;
        }

        /// <inheritdoc cref="DrawItems{T}(Utf8LabelHandler,ref T,ComboFlags,IEnumerable{T})"/>
        [MethodImpl(ImSharpConfiguration.Opt)]
        public static bool DrawItems<T>(Utf8LabelHandler label, ref T currentValue, params IEnumerable<T> items) where T : IEquatable<T>
        {
            using var combo = new ComboDisposable(ref label, $"{currentValue}", ComboFlags.None);
            if (!combo)
                return false;

            var ret = false;

            foreach (var value in items)
            {
                var equal = value.Equals(currentValue);
                if (Selectable($"{value}", equal) && !equal)
                {
                    currentValue = value;
                    ret          = true;
                }
            }

            return ret;
        }

        /// <summary> Draw a combo over the given items using a custom string function for names. </summary>
        /// <typeparam name="T"> The item type. </typeparam>
        /// <param name="label"> The combo label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="currentValue"> The currently selected value used for the preview string and highlighting the item. </param>
        /// <param name="flags"> Flags controlling the behavior of the combo. </param>
        /// <param name="toName"> The method converting the enum value to a UTF8 string which has to be null-terminated. </param>
        /// <param name="items"> The list of items to draw as selectables. </param>
        /// <returns> True if a new item was selected in this frame, in which case <paramref name="currentValue"/> will have changed. </returns>
        [MethodImpl(ImSharpConfiguration.Opt)]
        public static bool DrawItems<T>(Utf8LabelHandler label, ref T currentValue, ComboFlags flags, Func<T, ReadOnlySpan<byte>> toName,
            params IEnumerable<T> items) where T : IEquatable<T>
        {
            var       handler = (Utf8TextHandler)toName(currentValue);
            using var combo   = new ComboDisposable(ref label, ref handler, flags);
            if (!combo)
                return false;

            var ret = false;

            foreach (var value in items)
            {
                var equal = value.Equals(currentValue);
                if (Selectable(toName(value), equal) && !equal)
                {
                    currentValue = value;
                    ret          = true;
                }
            }

            return ret;
        }

        /// <summary> Draw a combo over the given items using a custom string function for names. </summary>
        /// <typeparam name="T"> The item type. </typeparam>
        /// <param name="label"> The combo label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="currentValue"> The currently selected value used for the preview string and highlighting the item. </param>
        /// <param name="flags"> Flags controlling the behavior of the combo. </param>
        /// <param name="toName"> The method converting the enum value to a UTF8 string. </param>
        /// <param name="items"> The list of items to draw as selectables. </param>
        /// <returns> True if a new item was selected in this frame, in which case <paramref name="currentValue"/> will have changed. </returns>
        [MethodImpl(ImSharpConfiguration.Opt)]
        public static bool DrawItems<T>(Utf8LabelHandler label, ref T currentValue, ComboFlags flags, Func<T, StringU8> toName,
            params IEnumerable<T> items) where T : IEquatable<T>
        {
            var       handler = (Utf8TextHandler)toName(currentValue);
            using var combo   = new ComboDisposable(ref label, ref handler, flags);
            if (!combo)
                return false;

            var ret = false;

            foreach (var value in items)
            {
                var equal = value.Equals(currentValue);
                if (Selectable(toName(value), equal) && !equal)
                {
                    currentValue = value;
                    ret          = true;
                }
            }

            return ret;
        }

        /// <summary> Draw a combo over the given items using a custom string function for names. </summary>
        /// <typeparam name="T"> The item type. </typeparam>
        /// <param name="label"> The combo label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="currentValue"> The currently selected value used for the preview string and highlighting the item. </param>
        /// <param name="flags"> Flags controlling the behavior of the combo. </param>
        /// <param name="toName"> The method converting the enum value to a UTF16 string. </param>
        /// <param name="items"> The list of items to draw as selectables. </param>
        /// <returns> True if a new item was selected in this frame, in which case <paramref name="currentValue"/> will have changed. </returns>
        [MethodImpl(ImSharpConfiguration.Opt)]
        public static bool DrawItems<T>(Utf8LabelHandler label, ref T currentValue, ComboFlags flags, Func<T, ReadOnlySpan<char>> toName,
            params IEnumerable<T> items) where T : IEquatable<T>
        {
            var       handler = (Utf8TextHandler)toName(currentValue);
            using var combo   = new ComboDisposable(ref label, ref handler, flags);
            if (!combo)
                return false;

            var ret = false;

            foreach (var value in items)
            {
                var equal = value.Equals(currentValue);
                if (Selectable(toName(value), equal) && !equal)
                {
                    currentValue = value;
                    ret          = true;
                }
            }

            return ret;
        }

        /// <inheritdoc cref="DrawItems{T}(Utf8LabelHandler,ref T,ComboFlags,Func{T,ReadOnlySpan{char}},IEnumerable{T})"/>
        [MethodImpl(ImSharpConfiguration.Opt)]
        public static bool DrawItems<T>(Utf8LabelHandler label, ref T currentValue, ComboFlags flags, Func<T, string> toName,
            params IEnumerable<T> items) where T : IEquatable<T>
        {
            var       handler = (Utf8TextHandler)toName(currentValue);
            using var combo   = new ComboDisposable(ref label, ref handler, flags);
            if (!combo)
                return false;

            var ret = false;

            foreach (var value in items)
            {
                var equal = value.Equals(currentValue);
                if (Selectable(toName(value), equal) && !equal)
                {
                    currentValue = value;
                    ret          = true;
                }
            }

            return ret;
        }

        [MethodImpl(ImSharpConfiguration.Opt)]
        private static bool Draw(scoped ref Utf8LabelHandler label, ref int currentIndex, ComboFlags flags,
            params IReadOnlyList<StringU8> items)
        {
            var       currentItem = (Utf8TextHandler)items[currentIndex];
            using var combo       = new ComboDisposable(ref label, ref currentItem, flags);
            if (!combo)
                return false;

            var ret = false;
            for (var i = 0; i < items.Count; ++i)
            {
                if (Selectable(items[i], i == currentIndex) && i != currentIndex)
                {
                    currentIndex = i;
                    ret          = true;
                }
            }

            return ret;
        }

        [MethodImpl(ImSharpConfiguration.Opt)]
        private static bool Draw(ref Utf8LabelHandler label, ref int currentIndex, ComboFlags flags, params IReadOnlyList<string> items)
        {
            var       currentItem = (Utf8TextHandler)items[currentIndex];
            using var combo       = new ComboDisposable(ref label, ref currentItem, flags);
            if (!combo)
                return false;

            var ret = false;
            for (var i = 0; i < items.Count; ++i)
            {
                if (Selectable(items[i], i == currentIndex) && i != currentIndex)
                {
                    currentIndex = i;
                    ret          = true;
                }
            }

            return ret;
        }
    }
}
