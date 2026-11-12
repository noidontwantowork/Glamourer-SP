namespace ImSharp;

public readonly partial struct StringU8
{
    private static int HoleEstimate
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        get => ImSharpConfiguration.FormatHoleEstimate;
    }


    /// <summary> Join a collection of strings with a given separator. </summary>
    /// <param name="separator"> The separator to put between each pair of strings. </param>
    /// <param name="strings"> The list of strings to join. </param>
    /// <returns> The joined strings. </returns>
    [MethodImpl(ImSharpConfiguration.Opt)]
    public static StringU8 Join(byte separator, params IReadOnlyCollection<StringU8> strings)
    {
        switch (strings.Count)
        {
            case 0: return Empty;
            case 1: return strings.First();
        }

        var size  = strings.Count + strings.Sum(s => s.Length);
        var array = new byte[size];
        array[^1] = 0;
        var idx = 0;
        foreach (var word in strings.SkipLast(1))
        {
            word._value.CopyTo(array.AsMemory(idx));
            idx          += word.Length;
            array[idx++] =  separator;
        }

        strings.Last()._value.CopyTo(array.AsMemory(idx));

        return new StringU8(array.AsMemory(..^1));
    }

    /// <inheritdoc cref="Join(byte,IReadOnlyCollection{StringU8})"/>
    [MethodImpl(ImSharpConfiguration.Opt)]
    public static StringU8 Join(ReadOnlySpan<byte> separator, params IReadOnlyCollection<StringU8> strings)
    {
        switch (strings.Count)
        {
            case 0: return Empty;
            case 1: return strings.First();
        }

        var size  = (strings.Count - 1) * separator.Length + strings.Sum(s => s.Length) + 1;
        var array = new byte[size];
        array[^1] = 0;
        var idx = 0;
        foreach (var word in strings.SkipLast(1))
        {
            word._value.CopyTo(array.AsMemory(idx));
            idx += word.Length;
            separator.CopyTo(array.AsSpan(idx));
            idx += separator.Length;
        }

        strings.Last()._value.CopyTo(array.AsMemory(idx));

        return new StringU8(array.AsMemory(..^1));
    }

    /// <inheritdoc cref="Join(byte,IReadOnlyCollection{StringU8})"/>
    [MethodImpl(ImSharpConfiguration.Opt)]
    public static unsafe StringU8 Join(char separator, params IReadOnlyCollection<StringU8> strings)
    {
        var count = Encoding.UTF8.GetByteCount(&separator, 1);
        if (count is 1)
            return Join((byte)separator, strings);

        Span<byte> sep = stackalloc byte[4];
        sep = sep[..count];
        Encoding.UTF8.GetBytes(new ReadOnlySpan<char>(&separator, 1), sep);

        return Join(sep, strings);
    }

    /// <inheritdoc cref="Join(byte,IReadOnlyCollection{StringU8})"/>
    [MethodImpl(ImSharpConfiguration.Opt)]
    public static StringU8 Join(ReadOnlySpan<char> separator, params IReadOnlyCollection<StringU8> strings)
    {
        var count = Encoding.UTF8.GetByteCount(separator);
        if (count is 1)
            return Join((byte)separator[0], strings);

        using var lease = ArrayPool.RentLease(count);
        Encoding.UTF8.GetBytes(separator, lease.Array);
        return Join(lease.Span, strings);
    }

    /// <inheritdoc cref="Join(byte,IReadOnlyCollection{StringU8})"/>
    [OverloadResolutionPriority(100)]
    [MethodImpl(ImSharpConfiguration.Opt)]
    public static StringU8 Join(byte separator, params IReadOnlyCollection<string?> strings)
    {
        if (strings.Count is 0)
            return Empty;

        var array = ArrayPool.Rent(strings.Count + strings.Sum(s => s?.Length ?? NullString.Length) * 4);
        try
        {
            if (strings.Count is 1)
                return ToU8String(ref array, strings.First());

            var idx = 0;
            foreach (var text in strings.SkipLast(1))
            {
                AppendU8String(ref array, ref idx, text);
                var span = array.AsSpan(idx);
                if (span.Length < 1)
                    ExchangeArray(ref array, array.Length * 2, idx);
                array[idx++] = separator;
            }

            AppendU8String(ref array, ref idx, strings.Last());
            return new StringU8(AddNull(array.AsSpan(0, idx)));
        }
        finally
        {
            ArrayPool.Return(array);
        }
    }

    /// <inheritdoc cref="Join(byte,IReadOnlyCollection{StringU8})"/>
    [OverloadResolutionPriority(100)]
    [MethodImpl(ImSharpConfiguration.Opt)]
    public static StringU8 Join(ReadOnlySpan<byte> separator, params IReadOnlyCollection<string?> strings)
    {
        if (strings.Count is 0)
            return Empty;

        var array = ArrayPool.Rent(strings.Count * separator.Length + strings.Sum(s => s?.Length ?? NullString.Length) * 4);
        try
        {
            if (strings.Count is 1)
                return ToU8String(ref array, strings.First());

            var idx = 0;
            foreach (var text in strings.SkipLast(1))
            {
                AppendU8String(ref array, ref idx, text);
                while (!separator.TryCopyTo(array.AsSpan(idx)))
                    ExchangeArray(ref array, array.Length * 2, idx);
                idx += separator.Length;
            }

            AppendU8String(ref array, ref idx, strings.Last());
            return new StringU8(AddNull(array.AsSpan(0, idx)));
        }
        finally
        {
            ArrayPool.Return(array);
        }
    }

    /// <inheritdoc cref="Join(byte,IReadOnlyCollection{StringU8})"/>
    [MethodImpl(ImSharpConfiguration.Opt)]
    public static StringU8 Join<T>(byte separator, IEnumerable<T> data)
    {
        var array = ArrayPool.Rent((1 + HoleEstimate) * 16);
        try
        {
            var idx = 0;
            foreach (var text in data)
            {
                AppendU8String(ref array, ref idx, text);
                var span = array.AsSpan(idx);
                if (span.Length < 1)
                    ExchangeArray(ref array, array.Length * 2, idx);
                array[idx++] = separator;
            }

            if (idx is 0)
                return Empty;

            return new StringU8(AddNull(array.AsSpan(0, idx - 1)));
        }
        finally
        {
            ArrayPool.Return(array);
        }
    }

    /// <inheritdoc cref="Join(byte,IReadOnlyCollection{StringU8})"/>
    [MethodImpl(ImSharpConfiguration.Opt)]
    public static StringU8 Join<T>(ReadOnlySpan<byte> separator, IEnumerable<T> data)
    {
        var array = ArrayPool.Rent((separator.Length + HoleEstimate) * 16);
        try
        {
            var idx = 0;
            foreach (var text in data)
            {
                AppendU8String(ref array, ref idx, text);
                while (!separator.TryCopyTo(array.AsSpan(idx)))
                    ExchangeArray(ref array, array.Length * 2, idx);
                idx += separator.Length;
            }

            if (idx is 0)
                return Empty;

            idx -= separator.Length;
            return new StringU8(AddNull(array.AsSpan(0, idx)));
        }
        finally
        {
            ArrayPool.Return(array);
        }
    }

    /// <inheritdoc cref="Join(byte,IReadOnlyCollection{StringU8})"/>
    [OverloadResolutionPriority(100)]
    [MethodImpl(ImSharpConfiguration.Opt)]
    public static unsafe StringU8 Join(char separator, params IReadOnlyCollection<string?> strings)
    {
        var count = Encoding.UTF8.GetByteCount(&separator, 1);
        if (count is 1)
            return Join((byte)separator, strings);

        Span<byte> sep = stackalloc byte[4];
        sep = sep[..count];
        Encoding.UTF8.GetBytes(new ReadOnlySpan<char>(&separator, 1), sep);

        return Join(sep, strings);
    }

    /// <inheritdoc cref="Join(byte,IReadOnlyCollection{StringU8})"/>
    [OverloadResolutionPriority(100)]
    [MethodImpl(ImSharpConfiguration.Opt)]
    public static StringU8 Join(ReadOnlySpan<char> separator, params IReadOnlyCollection<string?> strings)
    {
        var count = Encoding.UTF8.GetByteCount(separator);
        if (count is 1)
            return Join((byte)separator[0], strings);

        using var lease = ArrayPool.RentLease(count);
        Encoding.UTF8.GetBytes(separator, lease.Array);
        return Join(lease.Span, strings);
    }

    private static void ExchangeArray(ref byte[] array, int minSize, int copyExistingLength)
    {
        if (minSize < array.Length)
            return;

        var newSize  = (int)BitOperations.RoundUpToPowerOf2((uint)minSize);
        var oldArray = array;
        array = ArrayPool.Rent(newSize);
        oldArray.AsMemory(0, copyExistingLength).CopyTo(array);
        ArrayPool.Return(oldArray);
    }

    private static StringU8 ToU8String(ref byte[] array, object? text)
    {
        int bytes;
        if (text is IUtf8SpanFormattable format)
        {
            while (!format.TryFormat(array, out bytes, string.Empty, null))
                ExchangeArray(ref array, array.Length * 2, 0);
            return new StringU8(AddNull(array.AsSpan(0, bytes)));
        }

        var asString = text is null ? null : text as string ?? text.ToString();
        if (asString is null)
            return NullString;

        while (!Encoding.UTF8.TryGetBytes(asString, array, out bytes))
            ExchangeArray(ref array, array.Length * 2, 0);
        return new StringU8(AddNull(array.AsSpan(0, bytes)));
    }

    private static void AppendU8String(ref byte[] array, ref int idx, object? text)
    {
        int bytes;
        if (text is IUtf8SpanFormattable format)
        {
            while (!format.TryFormat(array.AsSpan(idx), out bytes, string.Empty, null))
                ExchangeArray(ref array, array.Length * 2, idx);
        }
        else
        {
            var asString = text is null ? null : text as string ?? text.ToString();

            if (asString is null)
            {
                bytes = NullString.Length;
                while (!NullString._value.TryCopyTo(array.AsMemory(idx)))
                    ExchangeArray(ref array, array.Length * 2, idx);
            }
            else
            {
                while (!Encoding.UTF8.TryGetBytes(asString, array.AsSpan(idx), out bytes))
                    ExchangeArray(ref array, array.Length * 2, idx);
            }
        }

        idx += bytes;
    }

    private static StringU8 ToU8String(ref byte[] array, string? text)
    {
        if (text is null)
            return NullString;

        int bytes;
        while (!Encoding.UTF8.TryGetBytes(text, array, out bytes))
            ExchangeArray(ref array, array.Length * 2, 0);
        return new StringU8(AddNull(array.AsSpan(0, bytes)));
    }

    private static void AppendU8String(ref byte[] array, ref int idx, string? text)
    {
        int bytes;
        if (text is null)
        {
            bytes = NullString.Length;
            while (!NullString._value.TryCopyTo(array.AsMemory(idx)))
                ExchangeArray(ref array, array.Length * 2, idx);
        }
        else
        {
            while (!Encoding.UTF8.TryGetBytes(text, array.AsSpan(idx), out bytes))
                ExchangeArray(ref array, array.Length * 2, idx);
        }

        idx += bytes;
    }
}
