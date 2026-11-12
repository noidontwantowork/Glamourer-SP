namespace ImSharp;

/// <summary> A base index interface. </summary>
public interface IIndex : ISpanFormattable, IUtf8SpanFormattable
{
    /// <summary> An invalid index. </summary>
    public const int InvalidIndex = -1;

    /// <summary> The index. </summary>g
    public int Index { get; }

    /// <summary> Whether the index is valid. </summary>
    public bool IsValid { get; }

    public string ToString()
        => Index.ToString();

    /// <inheritdoc/>
    string IFormattable.ToString(string? format, IFormatProvider? formatProvider)
        => Index.ToString(format, formatProvider);

    /// <inheritdoc/>
    bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten,
        [StringSyntax(StringSyntaxAttribute.NumericFormat)]
        ReadOnlySpan<char> format, IFormatProvider? provider)
        => Index.TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc/>
    bool IUtf8SpanFormattable.TryFormat(Span<byte> destination, out int bytesWritten,
        [StringSyntax(StringSyntaxAttribute.NumericFormat)]
        ReadOnlySpan<char> format, IFormatProvider? provider)
        => Index.TryFormat(destination, out bytesWritten, format, provider);
}

/// <summary> A base index interface with appropriate math operators. </summary>
public interface IIndex<TSelf> : IAdditionOperators<TSelf, int, TSelf>, IComparable<TSelf>,
    ISubtractionOperators<TSelf, int, TSelf>, IIncrementOperators<TSelf>, IDecrementOperators<TSelf>,
    IComparisonOperators<TSelf, TSelf, bool>, IIndex, ITrivialTypeInformation<TSelf>
    where TSelf : unmanaged, IIndex<TSelf>
{
    /// <inheritdoc/>
    int IComparable<TSelf>.CompareTo(TSelf other)
        => Index.CompareTo(other.Index);

    /// <summary> Create this index type from an integer. </summary>
    /// <param name="index"> The index. </param>
    /// <returns> The index as this type. </returns>
    public abstract static TSelf FromInt(int index);
}
