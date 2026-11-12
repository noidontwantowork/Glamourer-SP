namespace ImSharp;

/// <summary> A manager to create Memory{byte} of string literals. </summary>
/// <param name="allocationBase"> The allocation base as returned by <seealso cref="Interop.VirtualQuery"/>. </param>
/// <param name="allocationSize"> The allocation base as returned by <seealso cref="Interop.VirtualQuery"/>. </param>
internal sealed unsafe class LiteralManager(nint allocationBase, int allocationSize) : MemoryManager<byte>
{
    [MethodImpl(ImSharpConfiguration.OptInl)]
    protected override void Dispose(bool disposing)
    { }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public override Span<byte> GetSpan()
        => new((void*)allocationBase, allocationSize);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public override MemoryHandle Pin(int elementIndex = 0)
        => new((void*)(allocationBase + elementIndex));

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public override void Unpin()
    { }
}
