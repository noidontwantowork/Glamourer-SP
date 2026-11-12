namespace ImSharp;

/// <summary> A wrapper around an arbitrary ID type for textures. Could be changed when compiling ImGui. </summary>
public readonly record struct ImTextureId(nint Value) : ISpanFormattable, IUtf8SpanFormattable, IComparable<ImTextureId>
{
    /// <summary> An empty texture ID. </summary>
    public static readonly ImTextureId Zero = new(nint.Zero);

    /// <summary> Get whether this is an invalid ID. </summary>
    public bool IsNull
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Value == nint.Zero;
    }

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public override string ToString()
        => Value.ToString("X");

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public string ToString(string? format, IFormatProvider? formatProvider)
        => Value.ToString(format, formatProvider);

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool TryFormat(Span<char> destination, out int charsWritten,
        [StringSyntax(StringSyntaxAttribute.NumericFormat)]
        ReadOnlySpan<char> format, IFormatProvider? provider)
        => Value.TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool TryFormat(Span<byte> destination, out int bytesWritten,
        [StringSyntax(StringSyntaxAttribute.NumericFormat)]
        ReadOnlySpan<char> format, IFormatProvider? provider)
        => Value.TryFormat(destination, out bytesWritten, format, provider);

    /// <inheritdoc/>
    public int CompareTo(ImTextureId other)
        => Value.CompareTo(other.Value);
};
