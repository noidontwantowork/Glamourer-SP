namespace ImSharp;

/// <summary> An UTF8 string container that is not a ref-struct and is guaranteed to be null-terminated. </summary>
/// <remarks> Using this with memory mapped files will lead to undefined behavior, since mapped pointers are used to identify literals. </remarks>
#if HAS_NEWTONSOFT
// ReSharper disable once RedundantNameQualifier
[Newtonsoft.Json.JsonConverter(typeof(StringU8ConverterNewtonSoft))]
[System.Text.Json.Serialization.JsonConverter(typeof(StringU8Converter))]
#else
[System.Text.Json.Serialization.JsonConverter(typeof(StringU8Converter))]
#endif
public readonly partial struct StringU8 : IReadOnlyList<byte>, IEquatable<StringU8>, IComparable<StringU8>,
    IComparisonOperators<StringU8, StringU8, bool>, ISpanFormattable, IUtf8SpanFormattable
{
    private static readonly ConcurrentDictionary<nint, LiteralManager> AssemblyManagers = [];

    private static readonly ReadOnlyMemory<byte> EmptyData = new([0], 0, 0);

    /// <summary> The empty string. </summary>
    public static readonly StringU8 Empty = new(EmptyData);

    /// <summary> A string that is actually null and does not contain data, to not need optionals. </summary>
    public static readonly StringU8 Null = new(ReadOnlyMemory<byte>.Empty);

    /// <summary> A string representing null pointers. </summary>
    internal static readonly StringU8 NullString = new("<NULL>"u8);

    /// <summary> Whether the string is null instead of empty. </summary>
    /// <remarks> This should only be true for objects constructed with <c>default</c> or <see cref="Null"/>. </remarks>
    public bool IsNull
        => _value.IsEmpty;

    private static ArrayPool<byte> ArrayPool
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        get => ImSharpConfiguration.ArrayPool;
    }

    private readonly ReadOnlyMemory<byte> _value;

    /// <summary> The string as a byte span. </summary>
    public ReadOnlySpan<byte> Span
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => _value.Span;
    }

    /// <summary> The string as a byte memory. </summary>
    public ReadOnlyMemory<byte> Memory
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => _value;
    }

    /// <summary> Implicit conversion from the string to a byte span. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ReadOnlySpan<byte>(StringU8 s)
        => s.Span;

    /// <summary> Explicit conversion from a byte span to the string. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static explicit operator StringU8(ReadOnlySpan<byte> text)
        => new(text);

    /// <summary> Create a string from a managed byte array. </summary>
    /// <param name="data"> The byte array. </param>
    /// <remarks> Not checked for valid UTF8. Checks if the last byte in the array is 0, and avoids a copy if it is if the string is not empty. Editing the array afterward in that case is undefined behavior.</remarks>
    [OverloadResolutionPriority(100)]
    [MethodImpl(ImSharpConfiguration.Opt)]
    public StringU8(byte[] data)
    {
        if (data.Length is 0)
        {
            _value = EmptyData;
            return;
        }

        if (data[^1] is 0)
            _value = data.Length is 1
                ? EmptyData
                : data.AsMemory(0, data.Length - 1);
        else
            _value = AddNull(data);
    }

    /// <summary> Create a string from an unmanaged byte array without allocating data if the input is a UTF8-literal. </summary>
    /// <param name="data"> The span of unmanaged data. </param>
    /// <param name="nullTerminated"> Whether we assume null-termination of the span. </param>
    /// <exception cref="ArgumentException" />
    /// <remarks>
    /// If <paramref name="nullTerminated"/> is false and the string non-empty, always creates a copy of the given string.
    /// Otherwise, checks the byte after the array for 0 and throws if it is not actually a 0.
    /// UTF8-literals should be recognized through VirtualQuery and allocations avoided.
    /// </remarks>
    [OverloadResolutionPriority(50)]
    [MethodImpl(ImSharpConfiguration.Opt)]
    public unsafe StringU8(ReadOnlySpan<byte> data, bool nullTerminated = true)
    {
        if (data.Length is 0)
        {
            _value = EmptyData;
            return;
        }

        if (!nullTerminated)
        {
            _value = AddNull(data);
            return;
        }

        fixed (byte* ptr = data)
        {
            if (ptr[data.Length] is not 0)
                throw new ArgumentException("UTF8 string is supposedly null-terminated but is not.");

            if (!Interop.VirtualQuery((nint)ptr, out var info) || !info.Type.HasFlag(Interop.MemoryType.Mapped))
            {
                _value = AddNull(data);
            }
            else
            {
                var manager = AssemblyManagers.GetOrAdd(info.BaseAddress,
                    static (_, i) => new LiteralManager(i.AllocationBase, (int)(i.BaseAddress - i.AllocationBase + i.RegionSize)),
                    info);
                _value = manager.Memory.Slice((int)(ptr - (byte*)info.AllocationBase), data.Length);
            }
        }
    }

    /// <summary> Create a string from an unmanaged byte array. </summary>
    /// <param name="data"> The memory chunk of unmanaged data. </param>
    /// <param name="nullTerminated"> Whether we assume null-termination of the chunk. </param>
    /// <exception cref="ArgumentException" />
    /// <remarks>
    ///   If <paramref name="nullTerminated"/> is false and the string non-empty, always creates a copy of the given string.
    ///   Otherwise, checks the byte after the chunk for 0 and throws if it is not actually a 0, but avoids a copy.
    /// </remarks>
    [OverloadResolutionPriority(75)]
    [MethodImpl(ImSharpConfiguration.Opt)]
    public unsafe StringU8(ReadOnlyMemory<byte> data, bool nullTerminated = true)
    {
        if (data.Length is 0)
        {
            _value = EmptyData;
            return;
        }

        if (!nullTerminated)
        {
            _value = AddNull(data.Span);
            return;
        }

        fixed (byte* ptr = data.Span)
        {
            if (ptr[data.Length] is not 0)
                throw new ArgumentException("UTF8 string is supposedly null-terminated but is not.");

            _value = data;
        }
    }

    /// <summary> Efficiently create a string from an interpolated string. </summary>
    /// <param name="text"> The interpolated string input. </param>
    /// <remarks> Minimizes allocations and UTF16 string creations. </remarks>
    [OverloadResolutionPriority(100)]
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public StringU8(Utf8InterpolatedStringHandler text)
    {
        _value = text.WriteAndClear();
        _value = _value[..^1];
    }

    /// <inheritdoc cref="StringU8(Utf8InterpolatedStringHandler)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public StringU8(ref Utf8InterpolatedStringHandler text)
    {
        _value = text.WriteAndClear();
        _value = _value[..^1];
    }

    /// <inheritdoc cref="StringU8(Utf8InterpolatedStringHandler)"/>
    [OverloadResolutionPriority(101)]
    [MethodImpl(ImSharpConfiguration.OptInl)]
    // ReSharper disable once EntityNameCapturedOnly.Local
    public StringU8(IFormatProvider? provider, [InterpolatedStringHandlerArgument(nameof(provider))] Utf8InterpolatedStringHandler text)
        : this(text)
    { }

    /// <summary> Create a string from a UTF16 string. </summary>
    /// <param name="utf16"> The input UTF16 string. </param>
    /// <remarks> Minimizes allocations. </remarks>
    [OverloadResolutionPriority(50)]
    [MethodImpl(ImSharpConfiguration.Opt)]
    public StringU8(ReadOnlySpan<char> utf16)
    {
        if (utf16.Length is 0)
        {
            _value = EmptyData;
            return;
        }

        // The required space can not be larger than this.
        using var lease = ArrayPool.RentLease(utf16.Length * 4 + 1);
        var       count = Encoding.UTF8.GetBytes(utf16, lease.Array);
        var       bytes = new byte[count + 1];
        bytes[count] = 0;
        lease.Array.AsSpan(0, count).CopyTo(bytes);
        _value = bytes.AsMemory(0, count);
    }

    /// <summary> Create a string from a null-terminated C-style string pointer. </summary>
    /// <param name="ptr"> The string pointer. </param>
    /// <remarks> This will also avoid copies if the pointer points to literals. </remarks>
    [MethodImpl(ImSharpConfiguration.Opt)]
    public unsafe StringU8(byte* ptr)
        : this(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ptr))
    { }

    /// <summary> Create a string from an unmanaged byte array without checking validity. </summary>
    /// <param name="text"> The memory chunk of unmanaged data. </param>
    /// <remarks>
    ///   Use this function if you firmly know that the passed memory is valid, for example when it comes from a StringU8 itself.  
    /// </remarks>
    [OverloadResolutionPriority(75)]
    [MethodImpl(ImSharpConfiguration.Opt)]
    public static StringU8 CreateUnchecked(ReadOnlyMemory<byte> text)
        => new(text, 0);

    [MethodImpl(ImSharpConfiguration.Opt)]
    private StringU8(ReadOnlyMemory<byte> data, int _)
        => _value = data;

    /// <summary> Check whether two strings are byte-wise equal. </summary>
    [OverloadResolutionPriority(100)]
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool Equals(StringU8 other)
        => Span.EqualsCaseSensitive(other);

    /// <summary> Check whether two strings are byte-wise equal. </summary>
    [OverloadResolutionPriority(50)]
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool Equals(ReadOnlySpan<byte> other)
        => Span.EqualsCaseSensitive(other);

    /// <summary> Check whether two strings are byte-wise equal after disregarding case. </summary>
    [OverloadResolutionPriority(50)]
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool EqualsCaseInsensitive(ReadOnlySpan<byte> other)
        => Span.EqualsCaseInsensitive(other);

    /// <summary> Check whether two strings are byte-wise equal. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public override bool Equals(object? obj)
        => obj is StringU8 other && Equals(other);

    /// <summary> Lexicographically compare two strings byte-wise. </summary>
    [OverloadResolutionPriority(100)]
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public int CompareTo(StringU8 other)
        => Span.CompareCaseSensitive(other);

    /// <summary> Lexicographically compare two strings byte-wise. </summary>
    [OverloadResolutionPriority(50)]
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public int CompareTo(ReadOnlySpan<byte> other)
        => Span.CompareCaseSensitive(other);

    /// <summary> Lexicographically compare two strings byte-wise after disregarding case. </summary>
    [OverloadResolutionPriority(50)]
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public int CompareToCaseInsensitive(ReadOnlySpan<byte> other)
        => Span.CompareCaseInsensitive(other);

    /// <summary> Check if this string starts with the same bytes as <param name="other"/>. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool StartsWith(ReadOnlySpan<byte> other)
        => Span.StartsWithCaseSensitive(other);

    /// <summary> Check if this string starts with the same bytes as <param name="other"/> when disregarding case. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool StartsWithCaseInsensitive(ReadOnlySpan<byte> other)
        => Span.StartsWithCaseInsensitive(other);

    /// <summary> Check if this string ends with the same bytes as <param name="other"/>. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool EndsWith(ReadOnlySpan<byte> other)
        => Span.EndsWithCaseSensitive(other);

    /// <summary> Check if this string ends with the same bytes as <param name="other"/> when disregarding case. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool EndsWithCaseInsensitive(ReadOnlySpan<byte> other)
        => Span.EndsWithCaseInsensitive(other);

    /// <summary> Check if this string contains the bytes of <param name="other"/> at least once. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool Contains(ReadOnlySpan<byte> other)
        => Span.ContainsCaseSensitive(other);

    /// <summary> Check if this string contains the bytes of <param name="other"/> at least once when disregarding case. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool ContainsCaseInsensitive(ReadOnlySpan<byte> other)
        => Span.ContainsCaseInsensitive(other);

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public override string ToString()
        => Encoding.UTF8.GetString(this);

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public string ToString(string? format, IFormatProvider? formatProvider)
        => ToString();

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Encoding.UTF8.TryGetChars(_value.Span, destination, out charsWritten);

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool TryFormat(Span<byte> destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        if (!_value.Span.TryCopyTo(destination))
        {
            bytesWritten = 0;
            return false;
        }

        bytesWritten = Length;
        return true;
    }

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public override int GetHashCode()
        => Hashing.HashCaseSensitive(Span);

    /// <summary> Get a (ASCII) case-insensitive hash for this string. </summary>
    /// <returns> The hash. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public int GetCaseInsensitiveHashCode()
        => Hashing.HashAsciiCaseInsensitive(Span);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static ReadOnlyMemory<byte> AddNull(ReadOnlySpan<byte> data)
    {
        var ret = new byte[data.Length + 1];
        ret[data.Length] = 0;
        data.CopyTo(ret);
        return new Memory<byte>(ret, 0, data.Length);
    }

    /// <remarks> Only used internally to create the empty string. </remarks>
    [OverloadResolutionPriority(1000)]
    private StringU8(ReadOnlyMemory<byte> data)
        => _value = data;

    /// <inheritdoc/>
    public IEnumerator<byte> GetEnumerator()
    {
        for (var i = 0; i < _value.Length; ++i)
            yield return this[i];
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <summary> The length of the string without the null-terminator. </summary>
    public int Count
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => _value.Length;
    }

    /// <inheritdoc cref="Count"/>
    public int Length
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => _value.Length;
    }

    /// <summary> Whether the string is empty. </summary>
    public bool IsEmpty
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => _value.Length is 0;
    }

    /// <summary> Calculate the size this string will take up in pixels when drawn with the current font. </summary>
    /// <param name="hideTextAfterDashes"> Whether everything after the first ## is to be included or not. </param>
    /// <param name="wrapWidth"> The text wrap width to use for wrapping. 0 uses the current wrapping position, if any. </param>
    /// <returns> The required size to display the text. </returns>
    public Vector2 CalculateSize(bool hideTextAfterDashes = true, float wrapWidth = 0)
        => IsEmpty ? Vector2.Zero : Im.Font.CalculateSize(this, hideTextAfterDashes, wrapWidth);

    /// <summary> Calculate the size this string will take up in pixels when drawn with the given font. </summary>
    /// <param name="font"> The font in which this text will be drawn. </param>
    /// <param name="hideTextAfterDashes"> Whether everything after the first ## is to be included or not. </param>
    /// <param name="wrapWidth"> The text wrap width to use for wrapping. 0 uses the current wrapping position, if any. </param>
    /// <returns> The required size to display the text. </returns>
    public Vector2 CalculateSize(Im.Font font, bool hideTextAfterDashes = true, float wrapWidth = 0)
        => IsEmpty ? Vector2.Zero : font.CalculateTextSize(this, hideTextAfterDashes, wrapWidth);

    /// <summary> Access the byte at the given index. </summary>
    public byte this[int index]
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => _value.Span[index];
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator ==(StringU8 left, StringU8 right)
        => left.Equals(right);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator !=(StringU8 left, StringU8 right)
        => !left.Equals(right);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator >(StringU8 left, StringU8 right)
        => left.CompareTo(right) > 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator >=(StringU8 left, StringU8 right)
        => left.CompareTo(right) >= 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator <(StringU8 left, StringU8 right)
        => left.CompareTo(right) < 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator <=(StringU8 left, StringU8 right)
        => left.CompareTo(right) <= 0;

    public static StringU8 operator +(StringU8 left, ReadOnlySpan<byte> right)
    {
        var bytes = new byte[left.Length + right.Length + 1];
        left.Span.CopyTo(bytes);
        right.CopyTo(bytes.AsSpan(left.Length));
        bytes[^1] = 0;
        return new StringU8(bytes.AsMemory(0, bytes.Length - 1), true);
    }

    public static StringU8 operator +(ReadOnlySpan<byte> left, StringU8 right)
    {
        var bytes = new byte[left.Length + right.Length + 1];
        left.CopyTo(bytes);
        right.Span.CopyTo(bytes.AsSpan(left.Length));
        bytes[^1] = 0;
        return new StringU8(bytes.AsMemory(0, bytes.Length - 1), true);
    }

    public static StringU8 operator +(StringU8 left, StringU8 right)
    {
        var bytes = new byte[left.Length + right.Length + 1];
        left.Span.CopyTo(bytes);
        right.Span.CopyTo(bytes.AsSpan(left.Length));
        bytes[^1] = 0;
        return new StringU8(bytes.AsMemory(0, bytes.Length - 1), true);
    }
}
