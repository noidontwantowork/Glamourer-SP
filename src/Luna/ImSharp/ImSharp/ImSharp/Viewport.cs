namespace ImSharp;

public static partial class Im
{
    /// <summary> A reference to a viewport. </summary>
    /// <param name="pointer"> The native pointer to the viewport. </param>
    public readonly unsafe ref struct Viewport(Native.Viewport* pointer)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Native.Viewport* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator Viewport(Native.Viewport* pointer)
            => new(pointer);

        /// <summary> Get the primary viewport. This can never be invalid. </summary>
        public static Viewport Main
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.DrawList.GetMainViewport();
        }

        /// <summary> Get the size of the viewport in pixels. </summary>
        public Vector2 Size
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->Size;
        }

        /// <summary> Get the global position of the viewport in pixels. </summary>
        public Vector2 Position
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->Pos;
        }

        /// <summary> Get the center of the viewport in pixels. </summary>
        public Vector2 Center
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Position + 0.5f * Size;
        }

        /// <summary> Set the position of the next drawn window relative to this viewport. </summary>
        /// <param name="position"> The desired position for the next window.</param>
        /// <param name="condition"> Conditions to set the position. </param>
        /// <param name="pivot"> The pivot for the position. </param>
        public void SetNextWindowPositionRelative(Vector2 position, Condition condition = Condition.None, Vector2 pivot = default)
            => Window.SetNextPosition(position + Position, condition, pivot);

        /// <summary> Get the background draw list for this viewport. </summary>
        /// <remarks> The background draw list is the first that renders, so anything else is rendered on top of it. </remarks>
        public DrawList Background
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.DrawList.GetBackgroundDrawList(Pointer);
        }

        /// <summary> Get the foreground draw list for this viewport. </summary>
        /// <remarks> The foreground draw list is the last that renders, so it renders on top of everything else. </remarks>
        public DrawList Foreground
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.DrawList.GetForegroundDrawList(Pointer);
        }

        /// <summary> Helper function for backends to get a specific viewport by ID. </summary>
        /// <param name="id"> The Viewport ID. </param>
        /// <returns> A reference to the viewport that may be invalid. </returns>
        public static Viewport FindById(ImGuiId id)
            => Native.Methods.Viewport.FindViewportById(id);

        /// <summary> Helper function for backends to get a specific viewport by platform-specific handle. </summary>
        /// <param name="handle"> The handle to query. </param>
        /// <returns> A reference to the viewport that may be invalid. </returns>
        /// <remarks> Typical handles are <c>HWND</c>, <c>MyWindow*</c> or <c>GLFWwindow*</c> and similar. </remarks>
        public static Viewport FindByPlatformHandle(nint handle)
            => Native.Methods.Viewport.FindViewportByPlatformHandle((void*)handle);
    }
}
