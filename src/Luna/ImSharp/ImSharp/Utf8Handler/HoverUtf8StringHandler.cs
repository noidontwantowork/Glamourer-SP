namespace ImSharp;

[InterpolatedStringHandler]
[StructLayout(LayoutKind.Explicit)]
public unsafe ref struct HoverUtf8StringHandler
{
    /// <remarks> We assume that the interpolated string handler is 32 bytes but does not use the last 2, as is currently the case. </remarks>
    [FieldOffset(0)]
    private Utf8.TryWriteInterpolatedStringHandler _handler;

    [FieldOffset(31)] public readonly bool IsHovered;

    internal byte* Begin
        => TextStringHandlerBuffer.Buffer;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool GetEnd(out byte* end)
    {
        if (IsHovered && Utf8.TryWrite([], ref _handler, out var bytes))
        {
            end = TextStringHandlerBuffer.Buffer + bytes;
            if (TextStringHandlerBuffer.Size > bytes)
                TextStringHandlerBuffer.Buffer[bytes] = 0;
            return true;
        }

        end                               = TextStringHandlerBuffer.Buffer;
        TextStringHandlerBuffer.Buffer[0] = 0;
        return false;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public byte* Start()
    {
        if (IsHovered && Utf8.TryWrite([], ref _handler, out var bytes))
        {
            TextStringHandlerBuffer.Buffer[bytes] = 0;
            return TextStringHandlerBuffer.Buffer;
        }

        TextStringHandlerBuffer.Buffer[0] = 0;
        return TextStringHandlerBuffer.Buffer;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public byte* Start(out byte* end)
    {
        if (IsHovered && Utf8.TryWrite([], ref _handler, out var bytes))
        {
            end                                   = TextStringHandlerBuffer.Buffer + bytes;
            TextStringHandlerBuffer.Buffer[bytes] = 0;
            return TextStringHandlerBuffer.Buffer;
        }

        end                               = TextStringHandlerBuffer.Buffer;
        TextStringHandlerBuffer.Buffer[0] = 0;
        return TextStringHandlerBuffer.Buffer;
    }

    [MethodImpl(ImSharpConfiguration.Inl)]
    public HoverUtf8StringHandler(int literalLength, int formattedCount, HoveredFlags hoverFlags, out bool shouldAppend)
    {
        if (!Im.Item.Hovered(hoverFlags))
        {
            IsHovered = shouldAppend = false;
        }
        else
        {
            _handler = new Utf8.TryWriteInterpolatedStringHandler(literalLength, formattedCount,
                new Span<byte>(TextStringHandlerBuffer.Buffer, TextStringHandlerBuffer.Size),
                out shouldAppend);
            IsHovered = true;
        }
    }

    [MethodImpl(ImSharpConfiguration.Inl)]
    public HoverUtf8StringHandler(int literalLength, int formattedCount, out bool shouldAppend)
    {
        if (!Im.Item.Hovered(HoveredFlags.None))
        {
            IsHovered = shouldAppend = false;
        }
        else
        {
            _handler = new Utf8.TryWriteInterpolatedStringHandler(literalLength, formattedCount, TextStringHandlerBuffer.Span,
                out shouldAppend);
            IsHovered = true;
        }
    }

    [MethodImpl(ImSharpConfiguration.Inl)]
    public HoverUtf8StringHandler(int literalLength, int formattedCount, HoveredFlags hoverFlags, IFormatProvider? provider,
        out bool shouldAppend)
    {
        if (!Im.Item.Hovered(hoverFlags))
        {
            IsHovered = shouldAppend = false;
        }
        else
        {
            _handler = new Utf8.TryWriteInterpolatedStringHandler(literalLength, formattedCount, TextStringHandlerBuffer.Span, provider,
                out shouldAppend);
            IsHovered = true;
        }
    }

    [MethodImpl(ImSharpConfiguration.Inl)]
    public bool AppendLiteral(string value)
        => _handler.AppendLiteral(value);

    [MethodImpl(ImSharpConfiguration.Inl)]
    public bool AppendFormatted<TValue>(TValue value)
        => _handler.AppendFormatted(value);

    [MethodImpl(ImSharpConfiguration.Inl)]
    public bool AppendFormatted<TValue>(TValue value, string? format)
        => _handler.AppendFormatted(value, format);

    [MethodImpl(ImSharpConfiguration.Inl)]
    public bool AppendFormatted<TValue>(TValue value, int alignment)
        => _handler.AppendFormatted(value, alignment);

    [MethodImpl(ImSharpConfiguration.Inl)]
    public bool AppendFormatted<TValue>(TValue value, int alignment, string? format)
        => _handler.AppendFormatted(value, alignment, format);

    [MethodImpl(ImSharpConfiguration.Inl)]
    public bool AppendFormatted(scoped ReadOnlySpan<char> value)
        => _handler.AppendFormatted(value);

    [MethodImpl(ImSharpConfiguration.Inl)]
    public bool AppendFormatted(scoped ReadOnlySpan<char> value, int alignment = 0, string? format = null)
        => _handler.AppendFormatted(value, alignment, format);

    [MethodImpl(ImSharpConfiguration.Inl)]
    public bool AppendFormatted(scoped ReadOnlySpan<byte> utf8Value)
        => _handler.AppendFormatted(utf8Value);

    [MethodImpl(ImSharpConfiguration.Inl)]
    public bool AppendFormatted(scoped ReadOnlySpan<byte> utf8Value, int alignment = 0, string? format = null)
        => _handler.AppendFormatted(utf8Value, alignment, format);

    [MethodImpl(ImSharpConfiguration.Inl)]
    public bool AppendFormatted(string? value)
        => _handler.AppendFormatted(value);

    [MethodImpl(ImSharpConfiguration.Inl)]
    public bool AppendFormatted(string? value, int alignment = 0, string? format = null)
        => _handler.AppendFormatted(value, alignment, format);

    [MethodImpl(ImSharpConfiguration.Inl)]
    public bool AppendFormatted(object? value, int alignment = 0, string? format = null)
        => _handler.AppendFormatted(value, alignment, format);
}
