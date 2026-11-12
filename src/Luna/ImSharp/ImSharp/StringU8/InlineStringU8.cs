namespace ImSharp;

/// <summary>
/// A container for very short UTF8 strings that fit within an integer primitive.
/// It will be null-terminated if it has at least one spare byte of capacity.
/// Will treat any null byte in the middle of the string as a terminator.
/// </summary>
/// <typeparam name="TBacking">
/// The backing storage type of the inline string.
/// If this is not an unsigned integer primitive from the Base Class Library, the behavior is undefined.
/// </typeparam>
/// <remarks>
/// This structure works best when all the bytes past the null terminator are also null (see also <see cref="TruncateExcess"/>),
/// even though it tries to behave sensibly when this is not the case. <br />
/// Likewise, the methods and operators that accept spans work best when those spans do not contain null bytes. <br />
/// UTF8 well-formedness is the caller's responsibility.
/// </remarks>
#if HAS_NEWTONSOFT
// ReSharper disable once RedundantNameQualifier
[Newtonsoft.Json.JsonConverter(typeof(InlineStringU8ConverterNewtonSoft))]
#endif
[InlineStringU8JsonConverter]
[StructLayout(LayoutKind.Sequential)]
public struct InlineStringU8<TBacking>(TBacking value)
    : IReadOnlyList<byte>, IEquatable<InlineStringU8<TBacking>>, IEquatable<ReadOnlySpan<byte>>,
        IComparable<InlineStringU8<TBacking>>, IComparable<ReadOnlySpan<byte>>,
        IComparisonOperators<InlineStringU8<TBacking>, InlineStringU8<TBacking>, bool>,
        IEqualityOperators<InlineStringU8<TBacking>, TBacking, bool>,
        ISpanFormattable, IUtf8SpanFormattable where TBacking : unmanaged, IBinaryInteger<TBacking>
{
    /// <summary> The number of bytes that can be stored in a string with this backing type. </summary>
    public static unsafe int Capacity
        => sizeof(TBacking);

    /// <summary> The backing primitive. </summary>
    public TBacking Value = value;

    /// <summary> The length of the string without the null-terminator. </summary>
    public readonly int Length
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get
        {
            var byteScanPattern = GetByteScanPattern();
            var nullBytes       = ~(((Value & byteScanPattern) + byteScanPattern) | Value | byteScanPattern);
            return int.CreateTruncating(TBacking.TrailingZeroCount(nullBytes) >> 3);
        }
    }

    /// <inheritdoc cref="Length"/>
    readonly int IReadOnlyCollection<byte>.Count
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Length;
    }

    /// <summary> Whether the string is empty. </summary>
    public readonly bool IsEmpty
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => byte.CreateTruncating(Value) is 0;
    }

    /// <summary> Accesses the byte at the given index. </summary>
    public unsafe byte this[int i]
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        readonly get => i >= 0 && i < sizeof(TBacking)
            ? byte.CreateTruncating(Value >> (i << 3))
            : throw new IndexOutOfRangeException();
        set => this.AsBytes()[i] = value;
    }

    /// <summary> Creates an inline string that contains the given bytes. </summary>
    /// <param name="value"> The contents of the string. </param>
    /// <exception cref="ArgumentException"> The given contents do not fit within the backing primitive. </exception>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public InlineStringU8(ReadOnlySpan<byte> value)
        : this(default(TBacking))
    {
        value.CopyTo(this.AsBytes());
    }

    /// <summary> Creates an inline string that contains the UTF8 conversion of the given characters. </summary>
    /// <param name="value"> The contents of the string. </param>
    /// <exception cref="ArgumentException"> The given contents do not fit within the backing primitive. </exception>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public InlineStringU8(string value)
        : this(default(TBacking))
    {
        Encoding.UTF8.GetBytes(value, this.AsBytes());
    }

    /// <summary> Creates an inline string from an interpolated string. </summary>
    /// <param name="text"> The interpolated string input. </param>
    /// <remarks> Minimizes allocations and UTF16 string creations. </remarks>
    [OverloadResolutionPriority(100)]
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public InlineStringU8(Utf8InterpolatedStringHandler text)
        : this(default(TBacking))
    {
        text.WriteAndClear(this.AsBytes());
    }

    /// <inheritdoc cref="InlineStringU8(Utf8InterpolatedStringHandler)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public InlineStringU8(ref Utf8InterpolatedStringHandler text)
        : this(default(TBacking))
    {
        text.WriteAndClear(this.AsBytes());
    }

    /// <inheritdoc cref="InlineStringU8(Utf8InterpolatedStringHandler)"/>
    [OverloadResolutionPriority(101)]
    [MethodImpl(ImSharpConfiguration.OptInl)]
    // ReSharper disable once EntityNameCapturedOnly.Local
    public InlineStringU8(IFormatProvider? provider, [InterpolatedStringHandlerArgument(nameof(provider))] Utf8InterpolatedStringHandler text)
        : this(text)
    { }

    /// <summary> Creates an inline string from a null-terminated C-style string pointer. </summary>
    /// <param name="ptr"> The string pointer. </param>
    [MethodImpl(ImSharpConfiguration.Opt)]
    public unsafe InlineStringU8(byte* ptr)
        : this(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ptr))
    { }

    /// <summary> Check whether this string is equal to the given object. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public override readonly bool Equals(object? obj)
        => obj is InlineStringU8<TBacking> other && Equals(other);

    /// <summary>
    /// Check whether two strings are byte-wise equal.
    /// This considers the whole backing primitive and doesn't stop at the first null byte.
    /// </summary>
    [OverloadResolutionPriority(100)]
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly bool Equals(InlineStringU8<TBacking> other)
        => Value.Equals(other.Value);

    /// <summary> Check whether the backing primitive of this string is equal to the given value. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly bool Equals(TBacking other)
        => Value.Equals(other);

    /// <summary>
    /// Check whether two strings are byte-wise equal.
    /// This stops at the length of the given span, even if it contains null bytes.
    /// </summary>
    [OverloadResolutionPriority(50)]
    [MethodImpl(ImSharpConfiguration.Opt)]
    public readonly unsafe bool Equals(ReadOnlySpan<byte> other)
    {
        if (other.Length > sizeof(TBacking))
            return false;

        var bytes = this.AsReadOnlyBytes();
        return other.SequenceEqual(bytes[..other.Length]) && (other.Length == sizeof(TBacking) || bytes[other.Length] is 0);
    }

    /// <summary>
    /// Check whether two strings are byte-wise equal after disregarding case.
    /// This stops at the length of the given span, even if it contains null bytes.
    /// </summary>
    [OverloadResolutionPriority(50)]
    [MethodImpl(ImSharpConfiguration.Opt)]
    public readonly unsafe bool EqualsCaseInsensitive(ReadOnlySpan<byte> other)
    {
        if (other.Length > sizeof(TBacking))
            return false;

        var bytes = this.AsReadOnlyBytes();
        return other.EqualsCaseInsensitive(bytes[..other.Length]) && (other.Length == sizeof(TBacking) || bytes[other.Length] is 0);
    }

    /// <summary>
    /// Lexicographically compares two strings byte-wise.
    /// This considers the whole backing primitive and doesn't stop at the first null byte.
    /// </summary>
    [OverloadResolutionPriority(100)]
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly int CompareTo(InlineStringU8<TBacking> other)
        => this.AsReadOnlyBytes().SequenceCompareTo(other.AsReadOnlyBytes());

    /// <summary>
    /// Lexicographically compares two strings byte-wise.
    /// This stops at the length of the given span, even if it contains null bytes.
    /// </summary>
    [OverloadResolutionPriority(50)]
    [MethodImpl(ImSharpConfiguration.Opt)]
    public readonly unsafe int CompareTo(ReadOnlySpan<byte> other)
    {
        var bytes = this.AsReadOnlyBytes();
        if (other.Length > sizeof(TBacking))
            return bytes.SequenceCompareTo(other);

        var comparison = bytes[..other.Length].SequenceCompareTo(other);
        if (comparison is not 0)
            return comparison;

        return other.Length == sizeof(TBacking) || bytes[other.Length] is 0 ? 0 : 1;
    }

    /// <summary>
    /// Lexicographically compares two strings byte-wise after disregarding case.
    /// This stops at the length of the given span, even if it contains null bytes.
    /// </summary>
    [OverloadResolutionPriority(50)]
    [MethodImpl(ImSharpConfiguration.Opt)]
    public readonly unsafe int CompareToCaseInsensitive(ReadOnlySpan<byte> other)
    {
        var bytes = this.AsReadOnlyBytes();
        if (other.Length > sizeof(TBacking))
            return bytes.CompareCaseInsensitive(other);

        var comparison = bytes[..other.Length].CompareCaseInsensitive(other);
        if (comparison is not 0)
            return comparison;

        return other.Length == sizeof(TBacking) || bytes[other.Length] is 0 ? 0 : 1;
    }

    /// <summary> Checks if this string starts with the same bytes as <param name="other"/>. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly bool StartsWith(ReadOnlySpan<byte> other)
        => this.AsReadOnlyBytes().StartsWithCaseSensitive(other);

    /// <summary> Checks if this string starts with the same bytes as <param name="other"/> when disregarding case. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly bool StartsWithCaseInsensitive(ReadOnlySpan<byte> other)
        => this.AsReadOnlyBytes().StartsWithCaseInsensitive(other);

    /// <summary> Checks if this string ends with the same bytes as <param name="other"/>. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly bool EndsWith(ReadOnlySpan<byte> other)
        => this.GetBytes().EndsWithCaseSensitive(other);

    /// <summary> Checks if this string ends with the same bytes as <param name="other"/> when disregarding case. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly bool EndsWithCaseInsensitive(ReadOnlySpan<byte> other)
        => this.GetBytes().EndsWithCaseInsensitive(other);

    /// <summary> Checks if this string contains the bytes of <param name="other"/> at least once. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly bool Contains(ReadOnlySpan<byte> other)
        => this.AsReadOnlyBytes().ContainsCaseSensitive(other);

    /// <summary> Checks if this string contains the bytes of <param name="other"/> at least once when disregarding case. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly bool ContainsCaseInsensitive(ReadOnlySpan<byte> other)
        => this.AsReadOnlyBytes().ContainsCaseInsensitive(other);

    /// <summary>
    /// Puts this string into a different backing primitive.
    /// If the destination primitive is too small, the excess bytes will be truncated. 
    /// </summary>
    /// <typeparam name="TDestination"> The destination backing primitive type. </typeparam>
    /// <returns> This string in the desired backing primitive, if it fits. Otherwise, the starting part that fits. </returns>
    public readonly InlineStringU8<TDestination> IntoTruncating<TDestination>() where TDestination : unmanaged, IBinaryInteger<TDestination>
        => new(TDestination.CreateTruncating(Value));

    /// <summary>
    /// Puts this string into a different backing primitive.
    /// If the destination primitive is too small, an exception will be thrown. 
    /// </summary>
    /// <typeparam name="TDestination"> The destination backing primitive type. </typeparam>
    /// <returns> This string in the desired backing primitive. </returns>
    /// <exception cref="OverflowException"> This string is too long for the desired backing primitive. </exception>
    public readonly InlineStringU8<TDestination> IntoChecked<TDestination>() where TDestination : unmanaged, IBinaryInteger<TDestination>
        => new(TDestination.CreateChecked(Value));

    /// <summary> Clears all the bytes in the backing primitive that are past the string's null terminator. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public void TruncateExcess()
        => Truncate(Length);

    /// <summary> Clears all the bytes in the backing primitive that are past the desired length. </summary>
    /// <param name="length"> The desired maximum length. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public unsafe void Truncate(int length)
    {
        if (length < 0 || length > sizeof(TBacking))
            throw new ArgumentOutOfRangeException(nameof(length));

        if (length < sizeof(TBacking))
            Value &= ~(TBacking.AllBitsSet << (length << 3));
    }

    /// <summary> Takes a slice of the backing primitive starting at the desired position. </summary>
    /// <param name="start"> The starting position. </param>
    /// <returns> The sliced inline string. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public unsafe InlineStringU8<TBacking> Slice(int start)
    {
        if (start < 0 || start > sizeof(TBacking))
            throw new ArgumentOutOfRangeException(nameof(start));

        return start == sizeof(TBacking)
            ? default
            : new InlineStringU8<TBacking>(Value >> (start << 3));
    }

    /// <summary> Takes a slice of the backing primitive starting at the desired position, with the desired maximum length. </summary>
    /// <param name="start"> The starting position. </param>
    /// <param name="length"> The desired maximum length. </param>
    /// <returns> The sliced inline string. </returns>
    public InlineStringU8<TBacking> Slice(int start, int length)
    {
        var slice = Slice(start);
        slice.Truncate(length);
        return slice;
    }

    /// <inheritdoc/>
    public readonly IEnumerator<byte> GetEnumerator()
    {
        for (var i = 0; i < Capacity; ++i)
        {
            var b = byte.CreateTruncating(Value >> (i << 3));
            if (b is 0)
                break;

            yield return b;
        }
    }

    /// <inheritdoc/>
    readonly IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public override readonly string ToString()
        => Encoding.UTF8.GetString(this.GetBytes());

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
        => ToString();

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Encoding.UTF8.TryGetChars(this.GetBytes(), destination, out charsWritten);

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly bool TryFormat(Span<byte> destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        var bytes = this.GetBytes();
        if (!bytes.TryCopyTo(destination))
        {
            bytesWritten = 0;
            return false;
        }

        bytesWritten = bytes.Length;
        return true;
    }

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public override readonly int GetHashCode()
        => Value.GetHashCode();

    /// <summary> Get a (ASCII) case-insensitive hash for this string. </summary>
    /// <returns> The hash. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public int GetCaseInsensitiveHashCode()
        => Hashing.HashAsciiCaseInsensitive(this.AsReadOnlyBytes());

    /// <summary> Calculates the size this string will take up in pixels when drawn with the current font. </summary>
    /// <param name="hideTextAfterDashes"> Whether everything after the first ## is to be included or not. </param>
    /// <param name="wrapWidth"> The text wrap width to use for wrapping. 0 uses the current wrapping position, if any. </param>
    /// <returns> The required size to display the text. </returns>
    public Vector2 CalculateSize(bool hideTextAfterDashes = true, float wrapWidth = 0)
        => IsEmpty ? Vector2.Zero : Im.Font.CalculateSize(Utf8TextHandler.FromInline(in this), hideTextAfterDashes, wrapWidth);

    /// <summary> Calculates the size this string will take up in pixels when drawn with the given font. </summary>
    /// <param name="font"> The font in which this text will be drawn. </param>
    /// <param name="hideTextAfterDashes"> Whether everything after the first ## is to be included or not. </param>
    /// <param name="wrapWidth"> The text wrap width to use for wrapping. 0 uses the current wrapping position, if any. </param>
    /// <returns> The required size to display the text. </returns>
    public Vector2 CalculateSize(Im.Font font, bool hideTextAfterDashes = true, float wrapWidth = 0)
        => IsEmpty ? Vector2.Zero : font.CalculateTextSize(Utf8TextHandler.FromInline(in this), hideTextAfterDashes, wrapWidth);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator ==(InlineStringU8<TBacking> left, InlineStringU8<TBacking> right)
        => left.Value.Equals(right.Value);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator ==(InlineStringU8<TBacking> left, TBacking right)
        => left.Value.Equals(right);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator ==(TBacking left, InlineStringU8<TBacking> right)
        => left.Equals(right.Value);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator ==(InlineStringU8<TBacking> left, ReadOnlySpan<byte> right)
        => left.Equals(right);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator ==(ReadOnlySpan<byte> left, InlineStringU8<TBacking> right)
        => right.Equals(left);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator !=(InlineStringU8<TBacking> left, InlineStringU8<TBacking> right)
        => !left.Value.Equals(right.Value);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator !=(InlineStringU8<TBacking> left, TBacking right)
        => left.Value.Equals(right);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator !=(TBacking left, InlineStringU8<TBacking> right)
        => !left.Equals(right.Value);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator !=(InlineStringU8<TBacking> left, ReadOnlySpan<byte> right)
        => !left.Equals(right);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator !=(ReadOnlySpan<byte> left, InlineStringU8<TBacking> right)
        => !right.Equals(left);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator >(InlineStringU8<TBacking> left, InlineStringU8<TBacking> right)
        => left.CompareTo(right) > 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator >(InlineStringU8<TBacking> left, ReadOnlySpan<byte> right)
        => left.CompareTo(right) > 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator >(ReadOnlySpan<byte> left, InlineStringU8<TBacking> right)
        => right.CompareTo(left) < 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator >=(InlineStringU8<TBacking> left, InlineStringU8<TBacking> right)
        => left.CompareTo(right) >= 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator >=(InlineStringU8<TBacking> left, ReadOnlySpan<byte> right)
        => left.CompareTo(right) >= 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator >=(ReadOnlySpan<byte> left, InlineStringU8<TBacking> right)
        => right.CompareTo(left) <= 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator <(InlineStringU8<TBacking> left, InlineStringU8<TBacking> right)
        => left.CompareTo(right) < 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator <(InlineStringU8<TBacking> left, ReadOnlySpan<byte> right)
        => left.CompareTo(right) < 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator <(ReadOnlySpan<byte> left, InlineStringU8<TBacking> right)
        => right.CompareTo(left) > 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator <=(InlineStringU8<TBacking> left, InlineStringU8<TBacking> right)
        => left.CompareTo(right) <= 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator <=(InlineStringU8<TBacking> left, ReadOnlySpan<byte> right)
        => left.CompareTo(right) <= 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator <=(ReadOnlySpan<byte> left, InlineStringU8<TBacking> right)
        => right.CompareTo(left) >= 0;

    public static InlineStringU8<TBacking> operator +(InlineStringU8<TBacking> left, ReadOnlySpan<byte> right)
    {
        var leftLength = left.Length;
        if (leftLength + right.Length > Capacity)
            throw new ArgumentException("Inline string capacity exceeded while trying to concatenate");

        left.Truncate(leftLength);
        right.CopyTo(left.AsBytes()[leftLength..]);
        return left;
    }

    public static InlineStringU8<TBacking> operator +(ReadOnlySpan<byte> left, InlineStringU8<TBacking> right)
        => new InlineStringU8<TBacking>(left) + right;

    public static InlineStringU8<TBacking> operator +(InlineStringU8<TBacking> left, InlineStringU8<TBacking> right)
    {
        var leftLength = left.Length;
        if (leftLength + right.Length > Capacity)
            throw new ArgumentException("Inline string capacity exceeded while trying to concatenate");

        left.Truncate(leftLength);
        return new InlineStringU8<TBacking>(left.Value | (right.Value << (leftLength << 3)));
    }

    public void operator += (ReadOnlySpan<byte> other)
    {
        var length = Length;
        if (length + other.Length > Capacity)
            throw new ArgumentException("Inline string capacity exceeded while trying to concatenate");

        Truncate(length);
        other.CopyTo(this.AsBytes()[length..]);
    }

    public void operator += (InlineStringU8<TBacking> other)
    {
        var length = Length;
        if (length + other.Length > Capacity)
            throw new ArgumentException("Inline string capacity exceeded while trying to concatenate");

        Truncate(length);
        Value |= other.Value << (length << 3);
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static TBacking GetByteScanPattern()
    {
        const ulong pattern64 = 0x7F7F7F7F7F7F7F7FUL;

        return typeof(TBacking) == typeof(UInt128)
            ? TBacking.CreateTruncating(new UInt128(pattern64, pattern64))
            : TBacking.CreateTruncating(pattern64);
    }
}

/// <summary> Extensions related to <see cref="InlineStringU8{TBacking}"/>. </summary>
public static class InlineStringU8Extensions
{
    /// <summary> Wraps an integer primitive in an inline string, by reference. </summary>
    /// <param name="value"> The original reference. </param>
    /// <typeparam name="TBacking"> The type of the original reference. </typeparam>
    /// <returns> An inline string reference pointing to the same memory location. </returns>
    public static ref InlineStringU8<TBacking> AsInlineStringU8<TBacking>(this ref TBacking value)
        where TBacking : unmanaged, IBinaryInteger<TBacking>
        => ref MemoryMarshal.Cast<TBacking, InlineStringU8<TBacking>>(new Span<TBacking>(ref value))[0];

    /// <summary>
    /// Gets a (writable) span over the bytes of an inline string's backing primitive.
    /// Overwriting the null terminator and past it allows to grow the string, up to the backing primitive's capacity.
    /// </summary>
    /// <param name="value"> The inline string. </param>
    /// <typeparam name="TBacking"> The backing storage type of the inline string. </typeparam>
    /// <returns> A span over the bytes of the inline string's backing primitive. </returns>
    public static Span<byte> AsBytes<TBacking>(this ref InlineStringU8<TBacking> value) where TBacking : unmanaged, IBinaryInteger<TBacking>
        => MemoryMarshal.AsBytes(new Span<InlineStringU8<TBacking>>(ref value));

    extension<TBacking>(in InlineStringU8<TBacking> value) where TBacking : unmanaged, IBinaryInteger<TBacking>
    {
        /// <summary> Gets a read-only span over the bytes of an inline string's backing primitive. </summary>
        /// <returns> A span over the bytes of the inline string's backing primitive. </returns>
        public ReadOnlySpan<byte> AsReadOnlyBytes()
            => MemoryMarshal.AsBytes(new ReadOnlySpan<InlineStringU8<TBacking>>(in value));

        /// <summary> Gets a read-only span over the bytes of an inline string, stopping at the null terminator. </summary>
        /// <returns> A span over the bytes of the inline string. </returns>
        public ReadOnlySpan<byte> GetBytes()
            => value.AsReadOnlyBytes()[..value.Length];

        /// <summary> Gets a read-only span over the bytes of an inline string, stopping at the null terminator. </summary>
        /// <param name="isNullTerminated"> On return, whether there is a null terminator just after the returned span. </param>
        /// <returns> A span over the bytes of the inline string. </returns>
        public unsafe ReadOnlySpan<byte> GetBytes(out bool isNullTerminated)
        {
            var length = value.Length;
            isNullTerminated = length < sizeof(TBacking);
            return value.AsReadOnlyBytes()[..length];
        }
    }
}
