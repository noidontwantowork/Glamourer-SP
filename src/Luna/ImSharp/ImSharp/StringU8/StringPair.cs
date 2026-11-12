namespace ImSharp;

/// <summary> A text string encoded both in UTF16 and UTF8. </summary>
/// <param name="utf16"> The UTF16-encoded string, or null if it is lazily computed from <paramref name="utf8"/>. </param>
/// <param name="utf8"> The UTF8-encoded string, or <seealso cref="StringU8.Null"/> if it is lazily computed from <paramref name="utf16"/>. </param>
public struct StringPair(string? utf16, StringU8 utf8) : ISpanFormattable, IUtf8SpanFormattable
{
    private string? _utf16 = utf16;

    /// <summary> The UTF16-encoded string. </summary>
    public string Utf16
        => _utf16 ??= Utf8.ToString();

    /// <summary> The UTF8-encoded string. </summary>
    public StringU8 Utf8
    {
        get
        {
            if (field.IsNull)
            {
                if (_utf16 is null)
                    throw new InvalidOperationException("Both variants of the StringPair are null");

                field = new StringU8(Utf16);
            }

            return field;
        }
    } = utf8;

    /// <summary> The empty string pair. </summary>
    public static readonly StringPair Empty = new();

    /// <summary> Create an empty string pair. </summary>
    public StringPair()
        : this(string.Empty, StringU8.Empty)
    { }

    /// <summary> Create a pair from an existing UTF16-encoded string. </summary>
    /// <param name="text"> The UTF16-encoded string. </param>
    [OverloadResolutionPriority(20)]
    public StringPair(string text)
        : this(text, StringU8.Null)
    { }

    /// <summary> Create a pair from an existing UTF8-encoded string. </summary>
    /// <param name="text"> The UTF8-encoded string. </param>
    [OverloadResolutionPriority(20)]
    public StringPair(StringU8 text)
        : this(null, text)
    { }

    /// <summary> Create a pair from an interpolated string. </summary>
    /// <param name="handler"> The interpolated string. </param>
    [OverloadResolutionPriority(100)]
    public StringPair(DefaultInterpolatedStringHandler handler)
        : this(handler.ToStringAndClear(), StringU8.Null)
    { }

    /// <summary> Create a pair from an interpolated string. </summary>
    /// <param name="handler"> The interpolated string. </param>
    [OverloadResolutionPriority(50)]
    public StringPair(Utf8InterpolatedStringHandler handler)
        : this(null, new StringU8(ref handler))
    { }

    /// <summary> Create a pair from a UTF8 byte span. </summary>
    /// <param name="text"> The byte span. </param>
    [OverloadResolutionPriority(10)]
    public StringPair(ReadOnlySpan<byte> text)
        : this(new StringU8(text))
    { }

    /// <summary> Get whether the string is empty. </summary>
    public bool IsEmpty
        => _utf16 is null ? Utf8.IsEmpty : _utf16.Length is 0;

    public static implicit operator string(StringPair p)
        => p.Utf16;

    public static implicit operator ReadOnlySpan<char>(StringPair p)
        => p.Utf16;

    public static implicit operator StringU8(StringPair p)
        => p.Utf8;

    public static implicit operator ReadOnlySpan<byte>(StringPair p)
        => p.Utf8;

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider)
        => Utf16;

    /// <inheritdoc/>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        if (Utf16.Length > destination.Length)
        {
            charsWritten = 0;
            return false;
        }

        Utf16.CopyTo(destination);
        charsWritten = Utf16.Length;
        return true;
    }

    /// <inheritdoc/>
    public bool TryFormat(Span<byte> destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Utf8.TryFormat(destination, out bytesWritten, format, provider);

    /// <inheritdoc/>
    public override string ToString()
        => Utf16;
}
