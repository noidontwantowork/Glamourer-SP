namespace ImSharp;

public static partial class Im
{
    /// <summary> Draw a slider for numerical values. </summary>
    /// <typeparam name="T"> The type of the numeric value to change. </typeparam>
    /// <param name="label"> The slider label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="value"> The numeric input/output value. </param>
    /// <param name="format"> The printf format-string to display the number in as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="minValue"> The minimum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
    /// <param name="maxValue"> The maximum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
    /// <param name="flags"> Additional flags controlling the sliders behavior. </param>
    /// <returns> Whether the value changed in this frame. </returns>
    /// <remarks> Sliders can be turned to input boxes with a Control-Click on them unless disabled by flags. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool Slider<T>(Utf8LabelHandler label, ref T value, Utf8HintHandler format, T minValue, T maxValue,
        SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
        => Native.Methods.Sliders.SliderScalar(label.Start(), DataTypeExtensions.From<T>(), Unsafe.AsPointer(ref value), &minValue, &maxValue,
            format.Start(), flags);

    /// <summary> Draw a slider for numerical values using the default format for the type. </summary>
    /// <inheritdoc cref="Slider{T}(Utf8LabelHandler,ref T,Utf8HintHandler,T,T,SliderFlags)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool Slider<T>(Utf8LabelHandler label, ref T value, T minValue, T maxValue,
        SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
        => Native.Methods.Sliders.SliderScalar(label.Start(), DataTypeExtensions.From<T>(), Unsafe.AsPointer(ref value), &minValue, &maxValue,
            DefaultSliderFormat<T>().Start(), flags);

    /// <summary> Draw a group of sliders for multiple numerical values. </summary>
    /// <typeparam name="T"> The type of the numeric value to change. </typeparam>
    /// <param name="label"> The slider label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="values"> The numeric input/output values. </param>
    /// <param name="format"> The printf format-string to display the number in as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="minValue"> The minimum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
    /// <param name="maxValue"> The maximum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
    /// <param name="flags"> Additional flags controlling the sliders behavior. </param>
    /// <returns> Whether the value changed in this frame. </returns>
    /// <remarks> Sliders can be turned to input boxes with a Control-Click on them unless disabled by flags. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool SliderN<T>(Utf8LabelHandler label, Span<T> values, Utf8HintHandler format, T minValue, T maxValue,
        SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
        => Native.Methods.Sliders.SliderScalarN(label.Start(), DataTypeExtensions.From<T>(),
            (T*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(values)), values.Length, &minValue, &maxValue,
            format.Start(), flags);

    /// <summary> Draw a group of sliders for multiple numerical values using the default format for the type. </summary>
    /// <inheritdoc cref="SliderN{T}(Utf8LabelHandler,Span{T},Utf8HintHandler,T,T,SliderFlags)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool SliderN<T>(Utf8LabelHandler label, Span<T> values, T minValue, T maxValue,
        SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
        => Native.Methods.Sliders.SliderScalarN(label.Start(), DataTypeExtensions.From<T>(),
            (T*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(values)), values.Length, &minValue, &maxValue,
            DefaultSliderFormat<T>().Start(), flags);

    /// <summary> Draw a vertical slider for numerical values using the default format for the type </summary>
    /// <typeparam name="T"> The type of the numeric value to change. </typeparam>
    /// <param name="label"> The slider label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="size"> The size of the vertical slider. </param>
    /// <param name="value"> The numeric input/output value. </param>
    /// <param name="format"> The printf format-string to display the number in as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="minValue"> The minimum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
    /// <param name="maxValue"> The maximum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
    /// <param name="flags"> Additional flags controlling the sliders behavior. </param>
    /// <returns> Whether the value changed in this frame. </returns>
    /// <remarks> Sliders can be turned to input boxes with a Control-Click on them unless disabled by flags. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool SliderVertical<T>(Utf8LabelHandler label, Vector2 size, ref T value, Utf8HintHandler format, T minValue,
        T maxValue,
        SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
        => Native.Methods.Sliders.VSliderScalar(label.Start(), size, DataTypeExtensions.From<T>(), Unsafe.AsPointer(ref value), &minValue,
            &maxValue, format.Start(), flags);

    /// <summary> Draw a vertical slider for numerical values. </summary>
    /// <inheritdoc cref="SliderVertical{T}(Utf8LabelHandler,Vector2,ref T,Utf8HintHandler,T,T,SliderFlags)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool SliderVertical<T>(ReadOnlySpan<byte> label, Vector2 size, ref T value, T minValue, T maxValue,
        SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
        => Native.Methods.Sliders.VSliderScalar(label.Start(), size, DataTypeExtensions.From<T>(), Unsafe.AsPointer(ref value), &minValue,
            &maxValue, DefaultSliderFormat<T>().Start(), flags);

    /// <summary> Draw a slider specifically for angles in degrees radians. </summary>
    /// <param name="label"> The slider label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="valueRadian"> The angle in radian, i.e. usually in [0, 2pi). </param>
    /// <param name="format"> The printf format-string to display the degrees in as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="minDegrees"> The minimum value for the degrees, i.e. usually in [-360°, 360°]. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
    /// <param name="maxDegrees"> The maximum value for the degrees, usually in [-360°, 360°]. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
    /// <param name="flags"> Additional flags controlling the sliders behavior. </param>
    /// <returns> Whether the value changed in this frame. </returns>
    /// <remarks> Sliders can be turned to input boxes with a Control-Click on them unless disabled by flags. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool SliderAngle(Utf8LabelHandler label, ref float valueRadian, Utf8HintHandler format,
        float minDegrees = -360f, float maxDegrees = 360f, SliderFlags flags = SliderFlags.None)
        => Native.Methods.Sliders.SliderAngle(label.Start(), (float*)Unsafe.AsPointer(ref valueRadian), minDegrees, maxDegrees,
            format.Start(), flags);

    /// <summary> Draw a slider specifically for angles in degrees radians with the default format '%.0f deg'. </summary>
    /// <inheritdoc cref="SliderAngle(Utf8LabelHandler,ref float,Utf8HintHandler,float,float,SliderFlags)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool SliderAngle(Utf8LabelHandler label, ref float valueRadian, float minDegrees = -360f, float maxDegrees = 360f,
        SliderFlags flags = SliderFlags.None)
        => Native.Methods.Sliders.SliderAngle(label.Start(), (float*)Unsafe.AsPointer(ref valueRadian), minDegrees, maxDegrees,
            DefaultAngleFormat.Start(), flags);

    /// <summary> Draw a drag slider, i.e. a button you can hold and drag to the side to change the value. </summary>
    /// <typeparam name="T"> The type of the numeric value to change. </typeparam>
    /// <param name="label"> The slider label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="value"> The numeric input/output value. </param>
    /// <param name="format"> The printf format-string to display the number in as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="min"> The optional minimum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
    /// <param name="max"> The optional maximum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
    /// <param name="speed"> The dragging speed as the value increase per pixel of mouse movement. </param>
    /// <param name="flags"> Additional flags controlling the sliders behavior. </param>
    /// <returns> Whether the value changed in this frame. </returns>
    /// <remarks> Sliders can be turned to input boxes with a Control-Click on them unless disabled by flags. </remarks>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static unsafe bool Drag<T>(Utf8LabelHandler label, ref T value, Utf8HintHandler format, T? min = null, T? max = null,
        float speed = 1, SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
    {
        var actualMin = min.GetValueOrDefault();
        var actualMax = max.GetValueOrDefault();
        return Native.Methods.DragSliders.DragScalar(label.Start(), DataTypeExtensions.From<T>(), Unsafe.AsPointer(ref value), speed,
            min.HasValue ? &actualMin : null, max.HasValue ? &actualMax : null, format.Start(), flags);
    }

    /// <summary> Draw a drag slider, i.e. a button you can hold and drag to the side to change the value, using the default format for the type. </summary>
    /// <inheritdoc cref="Drag{T}(Utf8LabelHandler,ref T,Utf8HintHandler,Nullable{T},Nullable{T},float,SliderFlags)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool Drag<T>(Utf8LabelHandler label, ref T value, T? min = null, T? max = null, float speed = 1,
        SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
    {
        var actualMin = min.GetValueOrDefault();
        var actualMax = max.GetValueOrDefault();
        return Native.Methods.DragSliders.DragScalar(label.Start(), DataTypeExtensions.From<T>(), Unsafe.AsPointer(ref value), speed,
            min.HasValue ? &actualMin : null, max.HasValue ? &actualMax : null, DefaultSliderFormat<T>().Start(), flags);
    }

    /// <summary> Draw a group of drag sliders, i.e. buttons you can hold and drag to the side to change the values. </summary>
    /// <typeparam name="T"> The type of the numeric value to change. </typeparam>
    /// <param name="label"> The slider label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="values"> The numeric input/output values. </param>
    /// <param name="format"> The printf format-string to display the number in as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="min"> The optional minimum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
    /// <param name="max"> The optional maximum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
    /// <param name="speed"> The dragging speed as the value increase per pixel of mouse movement. </param>
    /// <param name="flags"> Additional flags controlling the sliders behavior. </param>
    /// <returns> Whether the value changed in this frame. </returns>
    /// <remarks> Sliders can be turned to input boxes with a Control-Click on them unless disabled by flags. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool DragN<T>(Utf8LabelHandler label, Span<T> values, Utf8HintHandler format, T? min = null, T? max = null,
        float speed = 1, SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
    {
        var actualMin = min.GetValueOrDefault();
        var actualMax = max.GetValueOrDefault();
        return Native.Methods.DragSliders.DragScalarN(label.Start(), DataTypeExtensions.From<T>(),
            (T*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(values)), values.Length, speed,
            min.HasValue ? &actualMin : null, max.HasValue ? &actualMax : null, format.Start(), flags);
    }

    /// <summary> Draw a group of drag sliders, i.e. buttons you can hold and drag to the side to change the values, using the default format for the type. </summary>
    /// <inheritdoc cref="DragN{T}(Utf8LabelHandler,Span{T},Utf8HintHandler,Nullable{T},Nullable{T},float,SliderFlags)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool DragN<T>(Utf8LabelHandler label, Span<T> values, T? min = null, T? max = null, float speed = 1,
        SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
    {
        var actualMin = min.GetValueOrDefault();
        var actualMax = max.GetValueOrDefault();
        return Native.Methods.DragSliders.DragScalarN(label.Start(), DataTypeExtensions.From<T>(),
            (T*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(values)), values.Length, speed,
            min.HasValue ? &actualMin : null, max.HasValue ? &actualMax : null, DefaultSliderFormat<T>().Start(), flags);
    }

    private static ReadOnlySpan<byte> DefaultAngleFormat
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        get => "%.0f deg"u8;
    }

    /// <summary> Obtain the default slider format for a number type. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static ReadOnlySpan<byte> DefaultSliderFormat<T>()
    {
        if (typeof(T) == typeof(byte))
            return "%u"u8;

        if (typeof(T) == typeof(ushort))
            return "%u"u8;

        if (typeof(T) == typeof(uint))
            return "%u"u8;

        if (typeof(T) == typeof(ulong))
            return "%llu"u8;

        if (typeof(T) == typeof(sbyte))
            return "%d"u8;

        if (typeof(T) == typeof(short))
            return "%d"u8;

        if (typeof(T) == typeof(int))
            return "%d"u8;

        if (typeof(T) == typeof(long))
            return "%lld"u8;

        if (typeof(T) == typeof(float))
            return "%f"u8;

        if (typeof(T) == typeof(double))
            return "%f"u8;

        if (typeof(T) == typeof(nint))
            return "0x%llx"u8;

        if (typeof(T) == typeof(nuint))
            return "0x%llx"u8;

        return ""u8;
    }
}
