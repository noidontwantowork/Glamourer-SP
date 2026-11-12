namespace ImSharp;

public unsafe struct TextStringHandlerBuffer : IStringHandlerBuffer
{
    public static int Size
        => ImSharpConfiguration.Context->TextBufferSize;

    public static byte* Buffer
        => ImSharpConfiguration.Context->TextBuffer;

    public static Span<byte> Span
        => new(Buffer, Size);

    public static bool Write(ReadOnlySpan<char> text, out byte* end)
        => IStringHandlerBuffer.Write<TextStringHandlerBuffer>(text, out end);
}
