namespace ImSharp;

public unsafe struct HintStringHandlerBuffer : IStringHandlerBuffer
{
    public static int Size
        => ImSharpConfiguration.Context->HintBufferSize;

    public static byte* Buffer
        => ImSharpConfiguration.Context->HintBuffer;

    public static Span<byte> Span
        => new(Buffer, Size);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool Write(ReadOnlySpan<char> text, out byte* end)
        => IStringHandlerBuffer.Write<HintStringHandlerBuffer>(text, out end);
}
