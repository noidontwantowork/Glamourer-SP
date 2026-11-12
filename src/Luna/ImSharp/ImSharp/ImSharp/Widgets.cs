#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace ImSharp;

public static partial class Im
{
    /// <summary> Create an empty disposable that can be used for both color and style pushing. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static ColorStyleDisposable ColorStyle()
        => new();

    /// <summary> A one pixel separator line. Generally horizontal, except when in menu bars or other horizontal layouts, where it is vertical. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void Separator()
        => Native.Methods.Layout.Separator();

    /// <summary> Draw a button with the given label and size. </summary>
    /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="size">
    /// The desired size for the button. If (0, 0), it will fit to the label.<br/>
    /// You can pass <paramref name="size"/>.x = float.MinValue to span the available width.
    /// </param>
    /// <param name="flags"> Additional flags to control the button's behaviour. </param>
    /// <returns> True if the button has been clicked in this frame. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool Button(Utf8LabelHandler label, Vector2 size = default, ButtonFlags flags = ButtonFlags.None)
        => Native.Methods.Internal.ButtonEx(label.Start(), size, flags);

    /// <summary> Draw a small button without frame padding. </summary>
    /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <returns> True if the button has been clicked in this frame. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool SmallButton(Utf8LabelHandler label)
        => Native.Methods.Widgets.SmallButton(label.Start());

    /// <summary> Draw a widget behaving like a button but without visuals. </summary>
    /// <param name="id"> The id as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="size"> The desired size for the button. You can pass <paramref name="size"/>.x = float.MinValue to span the available width. </param>
    /// <param name="flags"> Additional flags to control the button's behaviour. </param>
    /// <returns> True if the button has been clicked in this frame. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool InvisibleButton(Utf8LabelHandler id, Vector2 size, ButtonFlags flags = ButtonFlags.None)
        => Native.Methods.Widgets.InvisibleButton(id.Start(), size, flags);

    /// <summary> Draw a widget behaving like a button but without visuals. </summary>
    /// <param name="id"> The id as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="bounds"> The desired bounds for the button. </param>
    /// <param name="flags"> Additional flags to control the button's behaviour. </param>
    /// <returns> True if the button has been clicked in this frame. </returns>
    public static bool InvisibleButton(Utf8LabelHandler id, Rectangle bounds, ButtonFlags flags = ButtonFlags.None)
    {
        var savedCursor = Cursor.ScreenPosition;
        try
        {
            Cursor.ScreenPosition = bounds.Minimum;
            return InvisibleButton(id, bounds.Size, flags);
        }
        finally
        {
            Cursor.ScreenPosition = savedCursor;
        }
    }

    /// <summary> Draw a square button with side-length <seealso cref="ImGuiStyle.FrameHeight"/> and an arrow shape. </summary>
    /// <param name="id"> The id as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="direction"> The direction of the arrow. </param>
    /// <returns> True if the button has been clicked in this frame. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool ArrowButton(Utf8LabelHandler id, Direction direction)
        => Native.Methods.Widgets.ArrowButton(id.Start(), direction);

    /// <summary> Draw a radio button with the given label. </summary>
    /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="active"> Whether the radio button is currently active (i.e. filled). </param>
    /// <returns> True if the button has been clicked in this frame. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool RadioButton(Utf8LabelHandler label, bool active)
        => Native.Methods.Widgets.RadioButton(label.Start(), active);

    /// <summary> Draw a radio button representing integral values with the given label. </summary>
    /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="selected"> The currently selected value. </param>
    /// <param name="value"> The value for this button. </param>
    /// <returns> True if the button has been clicked in this frame. </returns>
    /// <remarks> Shortcut to handle a set of radio buttons with an integer index/value. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool RadioButton(Utf8LabelHandler label, ref int selected, int value)
        => Native.Methods.Widgets.RadioButton(label.Start(), (int*)Unsafe.AsPointer(ref selected), value);

    /// <summary> Draw a selectable, i.e. text that highlights on being hovered or selected. </summary>
    /// <param name="label"> The selectable label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="isSelected"> Whether the selectable is currently selected. </param>
    /// <param name="flags"> Additional flags that control the selectable's behaviour. </param>
    /// <param name="size"> The desired size of the selectable. </param>
    /// <returns> True if the selectable has been clicked in this frame. </returns>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static unsafe bool Selectable(Utf8LabelHandler label, bool isSelected = false, SelectableFlags flags = SelectableFlags.None,
        Vector2 size = default)
        => Native.Methods.Widgets.Selectable(label.Start(), isSelected, flags, size);

    /// <inheritdoc cref="Selectable(Utf8LabelHandler,bool,SelectableFlags,Vector2)"/>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static unsafe bool Selectable<T>(ref Utf8StringHandler<T> label, bool isSelected = false,
        SelectableFlags flags = SelectableFlags.None, Vector2 size = default) where T : IStringHandlerBuffer
        => Native.Methods.Widgets.Selectable(label.Start(), isSelected, flags, size);

    /// <summary> Draw a checkbox. </summary>
    /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="value"> The input and output value of the checkbox. </param>
    /// <returns> True if the checkbox has been clicked in this frame, in which case <paramref name="value"/> will be flipped. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool Checkbox(Utf8LabelHandler label, ref bool value)
        => Native.Methods.Widgets.Checkbox(label.Start(), (ImBool*)Unsafe.AsPointer(ref value));

    /// <summary> Draw a checkbox. </summary>
    /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="value"> The input value of the checkbox. </param>
    /// <returns> True if the checkbox has been clicked in this frame, in which case <paramref name="value"/> will be flipped. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool Checkbox(Utf8LabelHandler label, bool value)
        => Native.Methods.Widgets.Checkbox(label.Start(), (ImBool*)Unsafe.AsPointer(ref value));

    /// <summary> Draw a tri-state checkbox. </summary>
    /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="value"> The bit-flag based input and output value. </param>
    /// <param name="flags"> The bit-flags this checkbox is representing. </param>
    /// <returns> True if the checkbox has been clicked in this frame. </returns>
    /// <remarks>
    ///     This checkbox will display empty if <paramref name="value"/> and <paramref name="flags"/> have no bits in common,
    ///     a square if some but not all <paramref name="flags"/> are set in <paramref name="value"/>,
    ///     and a checkmark if all are set. <br/>
    ///     If the current display is empty or square, clicking it results in setting all <paramref name="flags"/>,
    ///     if it is a checkmark, clicking unsets all <paramref name="flags"/>.
    /// </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool Checkbox(Utf8LabelHandler label, ref ulong value, ulong flags)
        => Native.Methods.Internal.CheckboxFlags(label.Start(), (ulong*)Unsafe.AsPointer(ref value), flags);

    /// <inheritdoc cref="Checkbox(Utf8LabelHandler,ref ulong,ulong)"/>
    /// <typeparam name="T"> A basic enumeration type with backing type using at most 4 bytes. </typeparam>
    /// <exception cref="ArgumentException"> If sizeof(T) > 4. </exception>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool Checkbox<T>(Utf8LabelHandler label, ref T value, T flags) where T : unmanaged, Enum
    {
        var (val, f) = ConvertEnum(value, flags);
        if (!Native.Methods.Internal.CheckboxFlags(label.Start(), &val, f))
            return false;

        value = *(T*)&val;
        return true;
    }

    /// <summary> Draw a progress bar. </summary>
    /// <param name="fraction"> The current percentage of progress. If less than 0, displays indeterminate progress bar animation instead. </param>
    /// <param name="size"> The desired size for the progress bar. If the size is less than 0, aligns the progress bar to the end. If it is 0, automatic size is used. </param>
    /// <param name="format"> The printf format-string to display the number in as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe void ProgressBar(float fraction, Vector2 size, Utf8LabelHandler format)
        => Native.Methods.Widgets.ProgressBar(fraction, size, format.Start());

    /// <summary> Draw a progress bar displaying the fraction as an integral percentage. </summary>
    /// <inheritdoc cref="ProgressBar(float,Vector2,Utf8LabelHandler)"/>>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe void ProgressBar(float fraction, Vector2 size)
        => Native.Methods.Widgets.ProgressBar(fraction, size, null);

    /// <summary> Draw a progress bar of standard height over the available content width. </summary>
    /// <inheritdoc cref="ProgressBar(float,Vector2,Utf8LabelHandler)"/>>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe void ProgressBar(float fraction, Utf8LabelHandler format)
        => Native.Methods.Widgets.ProgressBar(fraction, new Vector2(-float.MinValue, 0), format.Start());

    /// <summary> Draw a progress bar displaying the fraction as an integral percentage and of standard height over the available content width. </summary>
    /// <inheritdoc cref="ProgressBar(float,Vector2,Utf8LabelHandler)"/>>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe void ProgressBar(float fraction)
        => Native.Methods.Widgets.ProgressBar(fraction, new Vector2(-float.MinValue, 0), null);

    /// <summary> Draw a bullet point, i.e. a small circle that keeps the cursor on the same line. </summary>
    /// <remarks> Advances the cursor X position by <seealso cref="ImGuiStyle.TreeNodeToLabelSpacing"/>. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void Bullet()
        => Native.Methods.Widgets.Bullet();

    /// <summary> Convert two enum-based values to uint. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static unsafe (ulong, ulong) ConvertEnum<T>(T value, T flags) where T : unmanaged, Enum
        => sizeof(T) switch
        {
            1 => (*(byte*)&value, *(byte*)&flags),
            2 => (*(ushort*)&value, *(ushort*)&flags),
            4 => (*(uint*)&value, *(uint*)&flags),
            8 => (*(ulong*)&value, *(ulong*)&flags),
            _ => throw new ArgumentException($"Enum type {typeof(T)} has size {sizeof(T)} > 8, which is not supported for flag checkboxes."),
        };
}
