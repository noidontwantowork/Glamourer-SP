// ReSharper disable MethodOverloadWithOptionalParameter

namespace ImSharp;

[SkipLocalsInit]
[InterpolatedStringHandler]
public unsafe ref struct Utf8StringHandler<T> where T : IStringHandlerBuffer
{
    private Data _data;

    [UsedImplicitly]
    internal byte* Begin
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => _data.IsCustom ? _data.CustomBegin : T.Buffer;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public byte* Start()
    {
        if (_data.IsCustom)
            return _data.CustomBegin;

        if (!Utf8.TryWrite([], ref _data.Handler, out var bytes))
        {
            T.Buffer[0] = 0;
            return T.Buffer;
        }

        T.Buffer[bytes] = 0;
        return T.Buffer;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public byte* Start(out byte* end)
    {
        if (!_data.IsCustom)
        {
            if (Utf8.TryWrite([], ref _data.Handler, out var bytes))
            {
                end             = T.Buffer + bytes;
                T.Buffer[bytes] = 0;
                return T.Buffer;
            }

            end         = T.Buffer;
            T.Buffer[0] = 0;
            return T.Buffer;
        }

        end = _data.CustomEnd;
        return _data.CustomBegin;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool GetSpan(out ReadOnlySpan<byte> span)
    {
        if (!_data.IsCustom)
        {
            if (Utf8.TryWrite([], ref _data.Handler, out var bytes))
            {
                span            = new ReadOnlySpan<byte>(T.Buffer, bytes);
                T.Buffer[bytes] = 0;
                return true;
            }

            span = [0];
            return false;
        }

        span = new ReadOnlySpan<byte>(_data.CustomBegin, _data.CustomSize);
        return true;
    }


    [MethodImpl(ImSharpConfiguration.Inl)]
    public Utf8StringHandler(int literalLength, int formattedCount, out bool shouldAppend)
        => _data.Handler = new Utf8.TryWriteInterpolatedStringHandler(literalLength, formattedCount, new Span<byte>(T.Buffer, T.Size),
            out shouldAppend);

    [MethodImpl(ImSharpConfiguration.Inl)]
    public Utf8StringHandler(int literalLength, int formattedCount, IFormatProvider? provider, out bool shouldAppend)
        => _data.Handler = new Utf8.TryWriteInterpolatedStringHandler(literalLength, formattedCount, new Span<byte>(T.Buffer, T.Size),
            provider, out shouldAppend);

    [MethodImpl(ImSharpConfiguration.Inl)]
    [UsedImplicitly]
    public bool AppendLiteral(string value)
        => _data.Handler.AppendLiteral(value);

    [MethodImpl(ImSharpConfiguration.Inl)]
    [UsedImplicitly]
    public bool AppendLiteral(ReadOnlySpan<char> value)
        => _data.Handler.AppendFormatted(value);

    [MethodImpl(ImSharpConfiguration.Inl)]
    [UsedImplicitly]
    public bool AppendFormatted<TValue>(TValue value)
        => _data.Handler.AppendFormatted(value);

    [MethodImpl(ImSharpConfiguration.Inl)]
    [UsedImplicitly]
    public bool AppendFormatted<TValue>(TValue value, string? format)
        => _data.Handler.AppendFormatted(value, format);

    [MethodImpl(ImSharpConfiguration.Inl)]
    [UsedImplicitly]
    public bool AppendFormatted<TValue>(TValue value, int alignment)
        => _data.Handler.AppendFormatted(value, alignment);

    [MethodImpl(ImSharpConfiguration.Inl)]
    [UsedImplicitly]
    public bool AppendFormatted<TValue>(TValue value, int alignment, string? format)
        => _data.Handler.AppendFormatted(value, alignment, format);

    [MethodImpl(ImSharpConfiguration.Inl)]
    [UsedImplicitly]
    public bool AppendFormatted(scoped ReadOnlySpan<char> value)
        => _data.Handler.AppendFormatted(value);

    [MethodImpl(ImSharpConfiguration.Inl)]
    [UsedImplicitly]
    public bool AppendFormatted(scoped ReadOnlySpan<char> value, int alignment = 0, string? format = null)
        => _data.Handler.AppendFormatted(value, alignment, format);

    [MethodImpl(ImSharpConfiguration.Inl)]
    [UsedImplicitly]
    public bool AppendFormatted(scoped ReadOnlySpan<byte> utf8Value)
        => _data.Handler.AppendFormatted(utf8Value);

    [MethodImpl(ImSharpConfiguration.Inl)]
    [UsedImplicitly]
    public bool AppendFormatted(scoped ReadOnlySpan<byte> utf8Value, int alignment = 0, string? format = null)
        => _data.Handler.AppendFormatted(utf8Value, alignment, format);

    [MethodImpl(ImSharpConfiguration.Inl)]
    [UsedImplicitly]
    public bool AppendFormatted(string? value)
        => _data.Handler.AppendFormatted(value);

    [MethodImpl(ImSharpConfiguration.Inl)]
    [UsedImplicitly]
    public bool AppendFormatted(string? value, int alignment = 0, string? format = null)
        => _data.Handler.AppendFormatted(value, alignment, format);

    [MethodImpl(ImSharpConfiguration.Inl)]
    [UsedImplicitly]
    public bool AppendFormatted(object? value, int alignment = 0, string? format = null)
        => _data.Handler.AppendFormatted(value, alignment, format);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Utf8StringHandler<T>(ReadOnlySpan<byte> str)
        => new(str);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Utf8StringHandler<T>(Span<byte> str)
        => new(str);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Utf8StringHandler<T>(StringU8 str)
        => new(str);

    #region Casts from InlineStringU8<TBacking>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Utf8StringHandler<T>(in InlineStringU8<byte> str)
        => FromInline(in str);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Utf8StringHandler<T>(in InlineStringU8<ushort> str)
        => FromInline(in str);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Utf8StringHandler<T>(in InlineStringU8<uint> str)
        => FromInline(in str);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Utf8StringHandler<T>(in InlineStringU8<ulong> str)
        => FromInline(in str);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Utf8StringHandler<T>(in InlineStringU8<UInt128> str)
        => FromInline(in str);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Utf8StringHandler<T>(in InlineStringU8<nuint> str)
        => FromInline(in str);
    #endregion

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Utf8StringHandler<T>(ReadOnlySpan<char> str)
    {
        if (str.IsEmpty)
            return new Utf8StringHandler<T>(new ReadOnlySpan<byte>());

        var handler = new Utf8StringHandler<T>(str.Length, 0, out var shouldAppend);
        if (shouldAppend)
            handler.AppendLiteral(str);
        return handler;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Utf8StringHandler<T>(string str)
        => str.AsSpan();

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private Utf8StringHandler(ReadOnlySpan<byte> utf8)
    {
        if (utf8.IsEmpty)
            _data = Data.Empty;
        else
            fixed (byte* ptr = utf8)
            {
                _data = new Data(ptr, utf8.Length);
            }
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private Utf8StringHandler(StringU8 utf8)
        : this(utf8.Span)
    { }

    internal static Utf8StringHandler<T> FromInline<TBacking>(in InlineStringU8<TBacking> str)
        where TBacking : unmanaged, IBinaryInteger<TBacking>
    {
        var bytes = str.GetBytes(out var isNullTerminated);
        if (isNullTerminated)
            return new Utf8StringHandler<T>(bytes);

        var handler = new Utf8StringHandler<T>(0, 1, out var shouldAppend);
        if (shouldAppend)
            handler.AppendFormatted(bytes);
        return handler;
    }

    public override string ToString()
        => GetSpan(out var span) ? Encoding.UTF8.GetString(span) : "<ERROR";
}

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit)]
internal unsafe ref struct Data(byte* start, int size)
{
    /// <remarks> We assume that the interpolated string handler is 32 bytes but does not use the last 2, as is currently the case. </remarks>
    [FieldOffset(0)]
    public Utf8.TryWriteInterpolatedStringHandler Handler;

    [FieldOffset(8)]
    public readonly byte* CustomBegin = start;

    [FieldOffset(24)]
    public readonly int CustomSize = size;

    [FieldOffset(31)]
    public readonly bool IsCustom = true;

    public byte* CustomEnd
        => CustomBegin + CustomSize;

    public static Data Empty
        => new(Null.Pointer, 0);

    private static readonly NullData Null = new();

    private class NullData
    {
        public readonly byte* Pointer;

        public NullData()
        {
            Pointer  = (byte*)Marshal.AllocHGlobal(1);
            *Pointer = 0;
        }

        ~NullData()
        {
            Marshal.FreeHGlobal((nint)Pointer);
        }
    }
}
