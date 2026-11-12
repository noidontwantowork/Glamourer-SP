namespace ImSharp;


[InterpolatedStringHandler]
public ref struct Utf8InterpolatedStringHandler
{
    private readonly byte[]                                 _array;
    private          Utf8.TryWriteInterpolatedStringHandler _handler;

    public int Length
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Utf8.TryWrite([], ref _handler, out var written)
            ? written
            : -1;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public Utf8InterpolatedStringHandler(int literalLength, int formattedCount, out bool shouldAppend)
    {
        _array   = ImSharpConfiguration.ArrayPool.Rent(ImSharpConfiguration.ArrayPoolRequestSizeLarge);
        _handler = new Utf8.TryWriteInterpolatedStringHandler(literalLength, formattedCount, _array, CultureInfo.InvariantCulture, out shouldAppend);
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public Utf8InterpolatedStringHandler(int literalLength, int formattedCount, IFormatProvider? formatProvider, out bool shouldAppend)
    {
        _array   = ImSharpConfiguration.ArrayPool.Rent(ImSharpConfiguration.ArrayPoolRequestSizeLarge);
        _handler = new Utf8.TryWriteInterpolatedStringHandler(literalLength, formattedCount, _array, formatProvider, out shouldAppend);
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public byte[] WriteAndClear()
    {
        if (Utf8.TryWrite([], ref _handler, out var written))
        {
            var ret = new byte[written + 1];
            ret[written] = 0;
            _array.AsSpan(0, written).CopyTo(ret);
            ImSharpConfiguration.ArrayPool.Return(_array);
            return ret;
        }

        ImSharpConfiguration.ArrayPool.Return(_array);
        throw new InvalidOperationException(
            $"Interpolating a Utf8String using more than {ImSharpConfiguration.ArrayPoolRequestSizeLarge} bytes is not supported.");
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public int WriteAndClear(scoped Span<byte> destination)
    {
        if (!Utf8.TryWrite([], ref _handler, out var written))
        {
            ImSharpConfiguration.ArrayPool.Return(_array);
            throw new InvalidOperationException(
                $"Interpolating a Utf8String using more than {ImSharpConfiguration.ArrayPoolRequestSizeLarge} bytes is not supported.");
        }

        if (destination.Length < written)
        {
            ImSharpConfiguration.ArrayPool.Return(_array);
            throw new ArgumentException(
                $"Interpolating a Utf8String using more than {ImSharpConfiguration.ArrayPoolRequestSizeLarge} bytes is not supported.");
        }

        if (destination.Length > written)
            destination[written] = 0;
        _array.AsSpan(0, written).CopyTo(destination);
        ImSharpConfiguration.ArrayPool.Return(_array);
        return written;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool AppendLiteral(string value)
        => _handler.AppendLiteral(value);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool AppendFormatted<TValue>(TValue value)
        => _handler.AppendFormatted(value);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool AppendFormatted<TValue>(TValue value, string? format)
        => _handler.AppendFormatted(value, format);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool AppendFormatted<TValue>(TValue value, int alignment)
        => _handler.AppendFormatted(value, alignment);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool AppendFormatted<TValue>(TValue value, int alignment, string? format)
        => _handler.AppendFormatted(value, alignment, format);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool AppendFormatted(scoped ReadOnlySpan<char> value)
        => _handler.AppendFormatted(value);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    // ReSharper disable once MethodOverloadWithOptionalParameter
    public bool AppendFormatted(scoped ReadOnlySpan<char> value, int alignment = 0, string? format = null)
        => _handler.AppendFormatted(value, alignment, format);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool AppendFormatted(scoped ReadOnlySpan<byte> utf8Value)
        => _handler.AppendFormatted(utf8Value);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    // ReSharper disable once MethodOverloadWithOptionalParameter
    public bool AppendFormatted(scoped ReadOnlySpan<byte> utf8Value, int alignment = 0, string? format = null)
        => _handler.AppendFormatted(utf8Value, alignment, format);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool AppendFormatted(string? value)
        => _handler.AppendFormatted(value);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    // ReSharper disable once MethodOverloadWithOptionalParameter
    public bool AppendFormatted(string? value, int alignment = 0, string? format = null)
        => _handler.AppendFormatted(value, alignment, format);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool AppendFormatted(object? value, int alignment = 0, string? format = null)
        => _handler.AppendFormatted(value, alignment, format);
}
