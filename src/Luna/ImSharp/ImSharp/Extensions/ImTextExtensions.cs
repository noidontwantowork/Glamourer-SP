namespace ImSharp;

public static class ImTextExtensions
{
    private static byte _null = 0;

    /// <summary> Clone the given byte span with an appended null-terminator. </summary>
    [MethodImpl(ImSharpConfiguration.Opt)]
    public static StringU8 CloneNullTerminated(this ReadOnlySpan<byte> input)
    {
        if (input.Length == 0)
            return StringU8.Empty;

        var bytes = new byte[input.Length + 1];
        input.CopyTo(bytes);
        bytes[input.Length] = 0;
        return new StringU8(bytes);
    }

    /// <summary> Clone the given byte span with an appended null-terminator. </summary>
    [MethodImpl(ImSharpConfiguration.Opt)]
    public static StringU8 CloneNullTerminated(this Span<byte> input)
    {
        if (input.Length == 0)
            return StringU8.Empty;

        var bytes = new byte[input.Length + 1];
        input.CopyTo(bytes);
        bytes[input.Length] = 0;
        return new StringU8(bytes);
    }

    /// <summary> Copy a given span with null-termination into a buffer or throw if too large. </summary>
    [MethodImpl(ImSharpConfiguration.Opt)]
    internal static unsafe void CopyInto<T>(this ReadOnlySpan<byte> data) where T : IStringHandlerBuffer
    {
        if (data.Length >= T.Size)
            throw new ImSharpSizeException();

        data.CopyTo(T.Span);
        T.Buffer[data.Length] = 0;
    }

    /// <summary> Transcode a given span with null-termination into a buffer or throw if too large. </summary>
    [MethodImpl(ImSharpConfiguration.Opt)]
    internal static unsafe void CopyInto<T>(this ReadOnlySpan<char> data, out int bytesWritten) where T : IStringHandlerBuffer
    {
        if (!Encoding.UTF8.TryGetBytes(data, T.Span, out bytesWritten) || bytesWritten == T.Size)
            throw new ImSharpSizeException();

        T.Buffer[bytesWritten] = 0;
    }

    /// <summary> Read a null-terminated string from the buffer and clone it with null-terminator. </summary>
    internal static StringU8 ReadNullTerminated(this Span<byte> buffer)
    {
        var nullTerminator = buffer.IndexOf((byte)0);
        if (nullTerminator == -1)
        {
            var result = new byte[buffer.Length + 1];
            buffer.CopyTo(result);
            result[buffer.Length] = 0;
            return new StringU8(result);
        }
        else
        {
            var result = new byte[nullTerminator + 1];
            buffer[..(nullTerminator + 1)].CopyTo(result);
            return new StringU8(result);
        }
    }


    /// <summary> Read a null-terminated string from the buffer and copy it into the given buffer. </summary>
    [MethodImpl(ImSharpConfiguration.Opt)]
    internal static unsafe Span<byte> CopyNullTerminated<T>(this Span<byte> buffer) where T : IStringHandlerBuffer
    {
        var nullTerminator = buffer.IndexOf((byte)0);
        if (nullTerminator == -1)
        {
            buffer.CopyTo(T.Span);
            T.Buffer[buffer.Length] = 0;
        }

        buffer[..(nullTerminator + 1)].CopyTo(T.Span);
        return T.Span[..(nullTerminator + 1)];
    }

    /// <summary> Get the starting pointer for a span. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    internal static unsafe byte* Start(this Span<byte> text)
    {
        if (text.IsEmpty)
            return (byte*)Unsafe.AsPointer(ref _null);

        fixed (byte* ptr = text)
        {
            return ptr;
        }
    }

    /// <summary> Get the starting and ending pointer for a span. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    internal static unsafe byte* Start(this Span<byte> text, out byte* end)
    {
        if (text.IsEmpty)
        {
            end = (byte*)Unsafe.AsPointer(ref _null);
            return end;
        }

        fixed (byte* ptr = text)
        {
            end = ptr + text.Length;
            return ptr;
        }
    }

    /// <summary> Get the starting pointer for a span. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    internal static unsafe byte* Start(this ReadOnlySpan<byte> text)
    {
        if (text.IsEmpty)
            return (byte*)Unsafe.AsPointer(ref _null);

        fixed (byte* ptr = text)
        {
            return ptr;
        }
    }

    /// <summary> Get the starting and ending pointer for a span. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    internal static unsafe byte* Start(this ReadOnlySpan<byte> text, out byte* end)
    {
        if (text.IsEmpty)
        {
            end = (byte*)Unsafe.AsPointer(ref _null);
            return end;
        }

        fixed (byte* ptr = text)
        {
            end = ptr + text.Length;
            return ptr;
        }
    }

    /// <summary> Return the formatted span of a text handler or throw if you can not. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    internal static ReadOnlySpan<byte> Span<T>(this ref Utf8StringHandler<T> text) where T : IStringHandlerBuffer
        => text.GetSpan(out var span) ? span : throw new Utf8FormatException();

    /// <summary> Return the transcoded span of the text or throw if you can not. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    internal static unsafe ReadOnlySpan<byte> Span<T>(this ReadOnlySpan<char> text) where T : IStringHandlerBuffer
        => T.Write(text, out var end) ? new ReadOnlySpan<byte>(T.Buffer, (int)(end - T.Buffer)) : throw new Utf8FormatException();
}
