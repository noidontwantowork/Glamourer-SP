namespace ImSharp;

/// <summary> Extensions for UTF8 text comparison. </summary>
public static partial class TextExtensions
{
    extension(ReadOnlySpan<byte> haystack)
    {
        /// <summary> Check whether a UTF8 string contains another while disregarding (ASCII) case. </summary>
        /// <param name="needle"> The contained string. </param>
        /// <returns> Whether the string is contained. </returns>
        public unsafe bool ContainsCaseInsensitive(ReadOnlySpan<byte> needle)
        {
            if (needle.IsEmpty)
                return true;

            var length    = haystack.Length;
            var subLength = needle.Length;
            if (subLength > length)
                return false;

            if (subLength is 1)
                return ContainsLength1SpecialCase(haystack, needle);

            fixed (byte* ptr = needle, hay = haystack)
            {
                return ContainsDefaultCase(hay, ptr, hay + length, subLength);
            }
        }

        /// <summary> Check whether a two UTF8 strings equal each other while disregarding (ASCII) case. </summary>
        /// <param name="other"> The other string. </param>
        /// <returns> Whether the strings are equal. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe bool EqualsCaseInsensitive(ReadOnlySpan<byte> other)
        {
            if (other.Length != haystack.Length)
                return false;

            fixed (byte* lhs = haystack, rhs = other)
            {
                return memicmp(lhs, rhs, (ulong)haystack.Length) is 0;
            }
        }

        /// <summary> Compare two UTF8 strings while disregarding (ASCII) case. </summary>
        /// <param name="other"> The other string. </param>
        /// <returns> A negative or positive integer if <paramref name="haystack"/> is lexicographically less or more than <paramref name="other"/>, and 0 if they are equal. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe int CompareCaseInsensitive(ReadOnlySpan<byte> other)
        {
            var shorter = Math.Min(haystack.Length, other.Length);
            fixed (byte* lhs = haystack, rhs = other)
            {
                var cmp = memicmp(lhs, rhs, (ulong)shorter);
                if (cmp is not 0)
                    return cmp;

                return haystack.Length > other.Length ? 1 : -1;
            }
        }

        /// <summary> Check whether a UTF8 string starts with another while disregarding (ASCII) case. </summary>
        /// <param name="needle"> The contained string. </param>
        /// <returns> Whether the string is contained. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe bool StartsWithCaseInsensitive(ReadOnlySpan<byte> needle)
        {
            if (haystack.Length < needle.Length)
                return false;

            fixed (byte* lhs = haystack, rhs = needle)
            {
                return memicmp(lhs, rhs, (ulong)needle.Length) is 0;
            }
        }

        /// <summary> Check whether a UTF8 string ends with another while disregarding (ASCII) case. </summary>
        /// <param name="needle"> The contained string. </param>
        /// <returns> Whether the string is contained. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe bool EndsWithCaseInsensitive(ReadOnlySpan<byte> needle)
        {
            var diff = haystack.Length - needle.Length;
            if (diff < 0)
                return false;

            fixed (byte* lhs = haystack, rhs = needle)
            {
                return memicmp(lhs + diff, rhs, (ulong)needle.Length) is 0;
            }
        }

        /// <summary> Check whether a UTF8 string contains another. </summary>
        /// <param name="needle"> The contained string. </param>
        /// <returns> Whether the string is contained. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool ContainsCaseSensitive(ReadOnlySpan<byte> needle)
        {
            if (needle.IsEmpty)
                return true;

            return haystack.IndexOf(needle) >= 0;
        }

        /// <summary> Check whether a two UTF8 strings equal each other. </summary>
        /// <param name="other"> The other string. </param>
        /// <returns> Whether the strings are equal. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool EqualsCaseSensitive(ReadOnlySpan<byte> other)
            => haystack.SequenceEqual(other);

        /// <summary> Compare two UTF8 strings. </summary>
        /// <param name="other"> The other string. </param>
        /// <returns> A negative or positive integer if <paramref name="haystack"/> is lexicographically less or more than <paramref name="other"/>, and 0 if they are equal. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public int CompareCaseSensitive(ReadOnlySpan<byte> other)
            => haystack.SequenceCompareTo(other);

        /// <summary> Check whether a UTF8 string starts with another. </summary>
        /// <param name="needle"> The contained string. </param>
        /// <returns> Whether the string is contained. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool StartsWithCaseSensitive(ReadOnlySpan<byte> needle)
            => haystack.StartsWith(needle);

        /// <summary> Check whether a UTF8 string ends with another. </summary>
        /// <param name="needle"> The contained string. </param>
        /// <returns> Whether the string is contained. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool EndsWithCaseSensitive(ReadOnlySpan<byte> needle)
            => haystack.EndsWith(needle);
    }


    /// <summary> Faster special case for single-length needles. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static bool ContainsLength1SpecialCase(ReadOnlySpan<byte> haystack, ReadOnlySpan<byte> other)
    {
        var needle = AsciiLowerCaseBytes[other[0]];
        foreach (var character in haystack)
        {
            if (AsciiLowerCaseBytes[character] == needle)
                return true;
        }

        return false;
    }

    /// <summary> Normal case for arbitrary length needles. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static unsafe bool ContainsDefaultCase(byte* hayStack, byte* other, byte* end, int subLength)
    {
        var start = AsciiLowerCaseBytes[other[0]];
        ++other;
        --subLength;
        end -= subLength;
        for (; hayStack < end; ++hayStack)
        {
            if (AsciiLowerCaseBytes[*hayStack] != start)
                continue;

            if (memicmp(hayStack + 1, other, (ulong)subLength) is 0)
                return true;
        }

        return false;
    }

    /// <summary> A map of all ASCII bytes to their lower case variants. </summary>
    private static readonly byte[] AsciiLowerCaseBytes = Enumerable.Range(0, 256)
        .Select(i => i < 0x80 ? (byte)char.ToLowerInvariant((char)i) : (byte)i)
        .ToArray();

    [LibraryImport("msvcrt.dll", EntryPoint = "_memicmp", SetLastError = false)]
    private static unsafe partial int memicmp(void* b1, void* b2, ulong count);
}
