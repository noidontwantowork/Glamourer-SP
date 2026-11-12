namespace ImSharp;

public interface IStringHandlerBuffer
{
    /// <summary> Get a static size for the buffer used. </summary>
    public abstract static int Size { get; }

    /// <summary> Get a static pointer to the buffer. </summary>
    public abstract static unsafe byte* Buffer { get; }

    /// <summary> Get a static span of the buffer with its corresponding size. </summary>
    public abstract static Span<byte> Span { get; }

    /// <summary> Write an UTF16 string to the buffer and get the length. </summary>
    /// <param name="text"> The UTF16 text to write. </param>
    /// <param name="end"> The resulting end after transcoding to UTF8 within the buffer. </param>
    /// <returns> True if the text could be transcoded. </returns>
    public abstract static unsafe bool Write(ReadOnlySpan<char> text, out byte* end);

    /// <seealso cref="Write"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool Write<T>(ReadOnlySpan<char> text, out byte* end) where T : IStringHandlerBuffer
    {
        if (Encoding.UTF8.TryGetBytes(text, T.Span, out var written))
        {
            if (written < T.Size)
                T.Buffer[written] = 0;
            end = T.Buffer + written;
            return true;
        }

        T.Buffer[0] = 0;
        end         = T.Buffer;
        return false;
    }
}
