namespace ImSharp;

public unsafe struct ImSharpContext : IDisposable
{
    public const long CurrentVersion = 2;

    public static ImSharpContext* EmptyPointer
        => ContextHolder.Context;

    public long Version;

    public byte* HintBuffer;
    public byte* LabelBuffer;
    public byte* TextBuffer;
    public byte* InputBuffer;

    public int HintBufferSize;
    public int LabelBufferSize;
    public int TextBufferSize;
    public int InputBufferSize;

    public void* ImGuiContext;
    public void* MonoFont;
    public void* DefaultFont;
    public void* ImNodesContext;

    public static ImSharpContext* SetupDefault()
    {
        var ret = (ImSharpContext*)Marshal.AllocHGlobal(sizeof(ImSharpContext));
        ret->Version        = CurrentVersion;
        ret->HintBuffer     = (byte*)Marshal.AllocHGlobal(128 * 1024 - 1);
        ret->HintBufferSize = 128 * 1024 - 1;
        ret->HintBuffer[0]  = 0;

        ret->InputBuffer     = (byte*)Marshal.AllocHGlobal(8 * 1024 * 1024 - 1);
        ret->InputBufferSize = 8 * 1024 * 1024 - 1;
        ret->InputBuffer[0]  = 0;

        ret->LabelBuffer     = (byte*)Marshal.AllocHGlobal(128 * 1024 - 1);
        ret->LabelBufferSize = 128 * 1024 - 1;
        ret->LabelBuffer[0]  = 0;

        ret->TextBuffer     = (byte*)Marshal.AllocHGlobal(4 * 1024 * 1024 - 1);
        ret->TextBufferSize = 4 * 1024 * 1024 - 1;
        ret->TextBuffer[0]  = 0;

        ret->ImGuiContext = Im.Context.Pointer;
        ret->MonoFont     = null;
        ret->DefaultFont  = null;

        ret->ImNodesContext = ImNodes.ImNodes.ImNodesContext.Create().Pointer;

        return ret;
    }

    public static void TearDownDefault(ImSharpContext* context)
    {
        Marshal.FreeHGlobal((nint)context->HintBuffer);
        Marshal.FreeHGlobal((nint)context->LabelBuffer);
        Marshal.FreeHGlobal((nint)context->TextBuffer);
        Marshal.FreeHGlobal((nint)context->InputBuffer);
        ((ImNodes.ImNodes.ImNodesContext)context->ImNodesContext).Destroy();

        context->Dispose();
        Marshal.FreeHGlobal((nint)context);
    }

    public void Dispose()
    {
        ImGuiContext    = null;
        HintBuffer      = null;
        LabelBuffer     = null;
        TextBuffer      = null;
        InputBuffer     = null;
        MonoFont        = null;
        DefaultFont     = null;
        HintBufferSize  = 0;
        LabelBufferSize = 0;
        TextBufferSize  = 0;
        InputBufferSize = 0;
        ImNodesContext = null;
    }

    private static readonly EmptyContextHolder ContextHolder = new();

    private sealed class EmptyContextHolder()
    {
        public readonly ImSharpContext* Context = Initialize();

        private static ImSharpContext* Initialize()
        {
            var ret = (ImSharpContext*)Marshal.AllocHGlobal(sizeof(ImSharpContext));
            *ret = default;
            return ret;
        }

        ~EmptyContextHolder()
            => Marshal.FreeHGlobal((nint)Context);
    }
}
