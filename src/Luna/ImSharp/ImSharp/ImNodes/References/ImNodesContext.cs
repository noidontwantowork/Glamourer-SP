namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    /// <summary> A reference to the ImNodes context. </summary>
    /// <param name="pointer"> The native pointer to the context. </param>
    /// <remarks> These functions should generally not be necessary, except for <seealso cref="SetImGuiContext"/>. </remarks>
    public readonly unsafe ref struct ImNodesContext(Internal.NodesContext* pointer)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Internal.NodesContext* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator ImNodesContext(Internal.NodesContext* pointer)
            => new(pointer);

        /// <summary> Create a new ImNodes context. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ImNodesContext Create()
            => Api.CreateContext();

        /// <summary> Get the current ImNodes context. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ImNodesContext Get()
            => Api.GetCurrentContext();

        /// <summary> Set the current ImNodes context to this. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void SetToCurrent()
            => Api.SetCurrentContext(Pointer);

        /// <summary> Destroy this ImNodes context. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Destroy()
            => Api.DestroyContext(Pointer);

        /// <summary> If the used ImNode instance is a separate DLL from the ImGui instance, set the context with this. </summary>
        /// <param name="context"> The ImGui context from its own DLL. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetImGuiContext(Im.ImGuiContext context)
            => Api.SetImGuiContext(context.Pointer);
    }
}
