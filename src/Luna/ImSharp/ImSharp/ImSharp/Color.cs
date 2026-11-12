namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper class for color-related queries and actions. </summary>
    public static class Color
    {
        /// <summary> Create a new, empty <see cref="ColorDisposable"/> to push colors to. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ColorDisposable Empty()
            => new();

        /// <summary> Draw an editing panel for a given RGB color. </summary>
        /// <param name="label"> The color editor label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="color"> The input / output color value. </param>
        /// <param name="flags"> Additional flags controlling the editing panel. </param>
        /// <returns> True if the color has been changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public static unsafe bool Editor(Utf8LabelHandler label, ref Vector3 color, ColorEditorFlags flags = ColorEditorFlags.None)
            => Native.Methods.Color.ColorEdit3(label.Start(), (float*)Unsafe.AsPointer(ref color), flags);

        /// <summary> Draw an editing panel for a given RGBA color. </summary>
        /// <param name="label"> The color edit label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="color"> The input / output color value. </param>
        /// <param name="flags"> Additional flags controlling the editing panel. </param>
        /// <returns> True if the color has been changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public static unsafe bool Editor(Utf8LabelHandler label, ref Vector4 color, ColorEditorFlags flags = ColorEditorFlags.None)
            => Native.Methods.Color.ColorEdit4(label.Start(), (float*)Unsafe.AsPointer(ref color), flags);

        /// <summary> Draw a color button opening a color edit popup for a given RGB color. </summary>
        /// <param name="label"> The color picker label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="color"> The input / output color value . </param>
        /// <param name="flags"> Additional flags controlling the button display and editing popup. </param>
        /// <returns> True if the color has been changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public static unsafe bool Picker(Utf8LabelHandler label, ref Vector3 color, ColorPickerFlags flags = ColorPickerFlags.None)
            => Native.Methods.Color.ColorPicker3(label.Start(), (float*)Unsafe.AsPointer(ref color), flags);

        /// <summary> Draw a color button opening a color edit popup for a given RGBA color. </summary>
        /// <param name="label"> The color picker label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="color"> The input / output color value . </param>
        /// <param name="flags"> Additional flags controlling the button display and editing popup. </param>
        /// <returns> True if the color has been changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public static unsafe bool Picker(Utf8LabelHandler label, ref Vector4 color, ColorPickerFlags flags = ColorPickerFlags.None)
            => Native.Methods.Color.ColorPicker4(label.Start(), (float*)Unsafe.AsPointer(ref color), flags, null);

        /// <summary> Draw a color button. </summary>
        /// <param name="description"> The tooltip description and ID as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="color"> The color the button should have. </param>
        /// <param name="size"> The desired size for the button. If (0, 0), it will be a square of frame-height. </param>
        /// <param name="flags"> Additional flags for the color information displayed on hover. </param>
        /// <returns> True if the button has been clicked in this frame. </returns>
        /// <remarks> The color button is a rectangle of the given size and color. On hover, it will display color information and the given description. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public static unsafe bool Button(Utf8LabelHandler description, in Vector4 color, ColorButtonFlags flags = ColorButtonFlags.None,
            Vector2 size = default)
            => Native.Methods.Color.ColorButton(description.Start(), color, flags, size);

        /// <inheritdoc cref="Editor(Utf8LabelHandler,ref Vector4,ColorEditorFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool Editor(Utf8LabelHandler label, ref Rgba32 color, ColorEditorFlags flags = ColorEditorFlags.None)
        {
            var vector = color.ToVector();
            if (!Native.Methods.Color.ColorEdit4(label.Start(), (float*)&vector, flags))
                return false;

            color = new Rgba32(vector);
            return true;
        }

        /// <inheritdoc cref="Picker(Utf8LabelHandler,ref Vector4,ColorPickerFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool Picker(Utf8LabelHandler label, ref Rgba32 color, ColorPickerFlags flags = ColorPickerFlags.None)
        {
            var vector = color.ToVector();
            if (!Native.Methods.Color.ColorPicker4(label.Start(), (float*)&vector, flags, null))
                return false;

            color = new Rgba32(vector);
            return true;
        }

        /// <inheritdoc cref="Button(Utf8LabelHandler,in Vector4,ColorButtonFlags,Vector2)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool Button(Utf8LabelHandler description, Rgba32 color, ColorButtonFlags flags = ColorButtonFlags.None,
            Vector2 size = default)
            => Native.Methods.Color.ColorButton(description.Start(), color.ToVector(), flags, size);

        /// <summary> Draw a tooltip for a given color as if hovering a color button or edit. </summary>
        /// <param name="name"> The label for the tooltip. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="color"> The color to draw. </param>
        /// <param name="flags"> Additional flags to control the behavior. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public static unsafe void Tooltip(Utf8LabelHandler name, in Vector4 color, ColorEditorFlags flags = ColorEditorFlags.None)
        {
            fixed (Vector4* ptr = &color)
            {
                Native.Methods.Internal.ColorTooltip(name.Start(), (float*)ptr, flags);
            }
        }

        /// <inheritdoc cref="Tooltip(Utf8LabelHandler,in Vector4,ColorEditorFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(50)]
        public static unsafe void Tooltip(Utf8LabelHandler name, in Vector3 color, ColorEditorFlags flags = ColorEditorFlags.None)
        {
            fixed (Vector3* ptr = &color)
            {
                Native.Methods.Internal.ColorTooltip(name.Start(), (float*)ptr, flags | ColorEditorFlags.NoAlpha);
            }
        }

        /// <inheritdoc cref="Tooltip(Utf8LabelHandler,in Vector4,ColorEditorFlags)"/>
        [OverloadResolutionPriority(0)]
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe void Tooltip(Utf8LabelHandler name, Rgba32 color, ColorEditorFlags flags = ColorEditorFlags.None)
            => Tooltip(name, color.ToVector(), flags);

        /// <inheritdoc cref="ColorDisposable.Push(ImGuiColor,Rgba32,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(50)]
        public static ColorDisposable Push(ImGuiColor type, Rgba32 color, bool condition)
            => new ColorDisposable().Push(type, color, condition);

        /// <inheritdoc cref="ColorDisposable.Push(ImGuiColor,Rgba32?)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ColorDisposable Push(ImGuiColor type, ColorParameter color)
            => new ColorDisposable().Push(type, color);

        /// <inheritdoc cref="ColorDisposable.Push(ImGuiColor,Vector4,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public static ColorDisposable Push(ImGuiColor type, Vector4 color, bool condition)
            => new ColorDisposable().Push(type, color, condition);

        /// <inheritdoc cref="ColorDisposable.Push(ImGuiColor,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(50)]
        public static ColorDisposable Push(ImGuiColor type, Rgba32 color)
            => new ColorDisposable().Push(type, color);

        /// <inheritdoc cref="ColorDisposable.Push(ImGuiColor,Vector4)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public static ColorDisposable Push(ImGuiColor type, Vector4 color)
            => new ColorDisposable().Push(type, color);

        /// <inheritdoc cref="ColorDisposable.PushDefault(ImGuiColor)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ColorDisposable PushDefault(ImGuiColor type)
            => new ColorDisposable().PushDefault(type);

        /// <summary> Get the current style color as a RGBA32 uint. </summary>
        /// <param name="color"> The requested color. </param>
        /// <param name="alphaMultiplier"> An optional multiplier for the alpha channel. </param>
        /// <returns> The requested color as RGBA32, possibly with a multiplied alpha channel. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static Rgba32 Get(ImGuiColor color, float alphaMultiplier = 1f)
            => Native.Methods.Style.GetColorU32(color, alphaMultiplier);

        /// <summary> Convert a color from RGB to HSV without touching alpha. </summary>
        /// <param name="rgba"> The input color in RGB encoding. </param>
        /// <returns> The color converted to HSV encoding. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe Vector4 ToHsv(Vector4 rgba)
        {
            Native.Methods.Color.ColorConvertRgbToHsv(rgba.X, rgba.Y, rgba.Z, &rgba.X, &rgba.Y, &rgba.Z);
            return rgba;
        }

        /// <summary> Convert a color from HSV to RGB without touching alpha. </summary>
        /// <param name="hsv"> The input color in HSV encoding. </param>
        /// <returns> The color converted to RGB encoding. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe Vector4 ToRgb(Vector4 hsv)
        {
            Native.Methods.Color.ColorConvertRgbToHsv(hsv.X, hsv.Y, hsv.Z, &hsv.X, &hsv.Y, &hsv.Z);
            return hsv;
        }

        /// <summary> Convert a color from RGB to HSV. </summary>
        /// <param name="rgba"> The input color in RGB encoding. </param>
        /// <returns> The color converted to HSV encoding. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe Vector3 ToHsv(Vector3 rgba)
        {
            Native.Methods.Color.ColorConvertRgbToHsv(rgba.X, rgba.Y, rgba.Z, &rgba.X, &rgba.Y, &rgba.Z);
            return rgba;
        }

        /// <summary> Convert a color from HSV to RGB. </summary>
        /// <param name="hsv"> The input color in HSV encoding. </param>
        /// <returns> The color converted to RGB encoding. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe Vector3 ToRgb(Vector3 hsv)
        {
            Native.Methods.Color.ColorConvertHsvToRgb(hsv.X, hsv.Y, hsv.Z, &hsv.X, &hsv.Y, &hsv.Z);
            return hsv;
        }

        /// <summary> Obtain the name of a pre-defined ImGui color. </summary>
        /// <param name="color"> The queried color. </param>
        /// <returns> A reference to the UTF8-encoded name owned by ImGui. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe ReadOnlySpan<byte> GetName(ImGuiColor color)
            => NullTerminationHelpers.GetSpan(Native.Methods.Color.GetStyleColorName(color));

        /// <summary> Obtain the name of a pre-defined ImGui color. </summary>
        /// <param name="color"> The queried color. </param>
        /// <returns> An owned copy of the UTF8-encoded name. </returns>
        public static unsafe StringU8 GetNameOwned(ImGuiColor color)
            => NullTerminationHelpers.GetClone(Native.Methods.Color.GetStyleColorName(color));

        /// <summary> Obtain the name of a pre-defined ImGui color. </summary>
        /// <param name="color"> The queried color. </param>
        /// <returns> A UTF16-encoded name. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe string GetNameUtf16(ImGuiColor color)
            => NullTerminationHelpers.GetString(Native.Methods.Color.GetStyleColorName(color));

        /// <summary> Set the default options for color editor widgets. </summary>
        /// <param name="editorFlags"> The flags for color editors. </param>
        /// <param name="pickerFlags"> The flags for color pickers. </param>
        /// <param name="buttonFlags"> The flags for color buttons. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetEditorOptions(ColorEditorFlags editorFlags = ColorEditorFlags.None,
            ColorPickerFlags pickerFlags = ColorPickerFlags.None, ColorButtonFlags buttonFlags = ColorButtonFlags.None)
            => Native.Methods.Color.SetColorEditOptions(editorFlags | (ColorEditorFlags)pickerFlags | (ColorEditorFlags)buttonFlags);

        /// <summary> Set the default options for color editor widgets. </summary>
        /// <param name="flags"> The flags to set. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetEditorOptions(uint flags)
            => Native.Methods.Color.SetColorEditOptions((ColorEditorFlags)flags);
    }
}
