namespace ImSharp;

public unsafe struct LabelStringHandlerBuffer : IStringHandlerBuffer
{
    public static int Size
        => ImSharpConfiguration.Context->LabelBufferSize;

    public static byte* Buffer
        => ImSharpConfiguration.Context->LabelBuffer;

    public static Span<byte> Span
        => new(Buffer, Size);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool Write(ReadOnlySpan<char> text, out byte* end)
        => IStringHandlerBuffer.Write<LabelStringHandlerBuffer>(text, out end);
}
