namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    public static unsafe partial class Api
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SetImGuiContext(Im.Native.Internal.Context* context)
            => Im.ImGuiContext.SetCurrent(context);

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static Internal.NodesContext* CreateContext()
        {
            var ret = Im.Main.Alloc<Internal.NodesContext>();
            *ret = new Internal.NodesContext();
            if (Context is null)
                SetCurrentContext(ret);
            ret->Initialize();
            return ret;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void DestroyContext(Internal.NodesContext* context)
        {
            if (context is null)
                context = Context;
            context->Dispose();
            if (Context == context)
                SetCurrentContext(null);
            if (context is not null)
                Im.Main.Free(context);
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static Internal.NodesContext* GetCurrentContext()
            => Context;

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SetCurrentContext(Internal.NodesContext* context)
            => Context = context;
    }
}
