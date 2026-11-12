namespace ImSharp;

/// <summary> The type of wide character used by ImGui. UTF16 in our case, could be configured to UTF32. </summary>
public readonly record struct ImWchar(ushort Value) : ISpanFormattable, IUtf8SpanFormattable
{
    public ImWchar(char value)
        : this((ushort)value)
    { }

    /// <summary> Returns the actual character. </summary>
    public char Character
        => (char)Value;

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public override string ToString()
        => Value.ToString();

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public string ToString(string? format, IFormatProvider? formatProvider)
        => Value.ToString();

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool TryFormat(Span<char> destination, out int charsWritten,
        [StringSyntax(StringSyntaxAttribute.NumericFormat)]
        ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        if (!destination.IsEmpty)
        {
            destination[0] = (char)Value;
            charsWritten   = 1;
            return true;
        }

        charsWritten = 0;
        return false;
    }

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool TryFormat(Span<byte> destination, out int bytesWritten,
        [StringSyntax(StringSyntaxAttribute.NumericFormat)]
        ReadOnlySpan<char> format, IFormatProvider? provider)
        => Encoding.UTF8.TryGetBytes([(char)Value], destination, out bytesWritten);
}
