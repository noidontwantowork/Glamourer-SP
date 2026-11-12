namespace ImSharp;

/// <summary> Comparers for UTF8 strings. </summary>
public static class StringU8Comparer
{
    /// <summary> A default byte-wise comparer. Is also an alternate lookup comparer for <see cref="ReadOnlySpan{byte}"/>. </summary>
    public static IEqualityComparer<StringU8> Ordinal
        => StringU8ComparerIgnoreCase.Instance;

    /// <summary> A comparer that ignores (ASCII) case. Is also an alternate lookup comparer for <see cref="ReadOnlySpan{byte}"/>. </summary>
    public static IEqualityComparer<StringU8> OrdinalIgnoreAsciiCase
        => StringU8ComparerIgnoreCase.Instance;
}

/// <summary> A comparer that ignores ASCII case. </summary>
internal sealed class StringU8ComparerIgnoreCase : IEqualityComparer<StringU8>, IAlternateEqualityComparer<ReadOnlySpan<byte>, StringU8>
{
    /// <inheritdoc cref="StringU8ComparerIgnoreCase"/> 
    public static readonly StringU8ComparerIgnoreCase Instance = new();

    /// <inheritdoc/>
    public bool Equals(StringU8 x, StringU8 y)
        => x.EqualsCaseInsensitive(y);

    /// <inheritdoc/>
    public int GetHashCode(StringU8 obj)
        => obj.GetCaseInsensitiveHashCode();

    /// <inheritdoc/>
    public bool Equals(ReadOnlySpan<byte> alternate, StringU8 other)
        => alternate.EqualsCaseInsensitive(other);

    /// <inheritdoc/>
    public int GetHashCode(ReadOnlySpan<byte> alternate)
        => Hashing.HashAsciiCaseInsensitive(alternate);

    /// <inheritdoc/>
    public StringU8 Create(ReadOnlySpan<byte> alternate)
        => new(alternate, false);
}

/// <summary> A default comparer. </summary>
internal sealed class StringU8OrdinalComparer : IEqualityComparer<StringU8>, IAlternateEqualityComparer<ReadOnlySpan<byte>, StringU8>
{
    /// <inheritdoc cref="StringU8OrdinalComparer"/> 
    public static readonly StringU8OrdinalComparer Instance = new();

    /// <inheritdoc/>
    public bool Equals(StringU8 x, StringU8 y)
        => x.Equals(y);

    /// <inheritdoc/>
    public int GetHashCode(StringU8 obj)
        => obj.GetHashCode();

    /// <inheritdoc/>
    public bool Equals(ReadOnlySpan<byte> alternate, StringU8 other)
        => alternate.EqualsCaseSensitive(other);

    /// <inheritdoc/>
    public int GetHashCode(ReadOnlySpan<byte> alternate)
        => Hashing.HashCaseSensitive(alternate);

    /// <inheritdoc/>
    public StringU8 Create(ReadOnlySpan<byte> alternate)
        => new(alternate, false);
}
