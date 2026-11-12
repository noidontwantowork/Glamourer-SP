namespace ImSharp;

public static partial class Im
{
    public static unsafe class Main
    {
        /// <summary> Tell ImGui to render the collected frame. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void Render()
            => Native.Methods.Main.Render();

        /// <summary> Tell ImGui to start a new frame. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void NewFrame()
            => Native.Methods.Main.NewFrame();

        /// <summary> Tell ImGui to end the current frame. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndFrame()
            => Native.Methods.Main.EndFrame();

        /// <summary> Allocate memory using ImGuis allocation method. </summary>
        /// <param name="size"> The requested size of the memory chunk. </param>
        /// <returns> A pointer to the allocated memory chunk.</returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static nint Alloc(ulong size)
            => (nint)Native.Methods.Memory.MemAlloc(size);

        /// <summary> Allocate memory using ImGuis allocation method. </summary>
        /// <typeparam name="T"> The unmanaged struct to allocate space for. </typeparam>
        /// <param name="count"> The number of values to allocate space for. </param>
        /// <returns> A pointer to the allocated memory chunk.</returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static T* Alloc<T>(int count = 1) where T : unmanaged
            => (T*)Alloc((ulong)(count * sizeof(T)));

        /// <summary> Free memory allocated using ImGuis allocation method. </summary>
        /// <param name="pointer"> The pointer to the memory chunk to free. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void Free(nint pointer)
            => Free((void*)pointer);

        /// <summary> Free memory allocated using ImGuis allocation method. </summary>
        /// <param name="pointer"> The pointer to the memory chunk to free. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void Free(void* pointer)
            => Native.Methods.Memory.MemFree(pointer);
    }
}
