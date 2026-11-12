namespace ImSharp;

public static partial class Im
{
    /// <summary> A reference to ImGui's draw data. </summary>
    /// <param name="pointer"> The native pointer to the draw data. </param>
    /// <remarks> This contains all draw command lists required to render the frame as well as position and size coordinates to use for the projection matrix. </remarks>
    public readonly unsafe ref struct DrawData(Native.ImDrawData* pointer)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Native.ImDrawData* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator DrawData(Native.ImDrawData* pointer)
            => new(pointer);

        /// <summary> The number of command lists. </summary>
        public int CommandListsCount
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->CommandListsCount;
        }

        /// <summary> The total number of vertices. </summary>
        public int TotalVertexCount
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->TotalVertexCount;
        }

        /// <summary> The required size for the total number of vertices in bytes. </summary>
        public int TotalVertexSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => TotalVertexCount * sizeof(Native.ImDrawVert);
        }

        /// <summary> The total number of indices. </summary>
        public int TotalIndexCount
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->TotalIndexCount;
        }

        /// <summary> The required size for the total number of indices in bytes. </summary>
        public int TotalIndexSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => TotalIndexCount * sizeof(Native.ImDrawIdx);
        }

        /// <summary> A list of all draw commands. </summary>
        public ReadOnlySpan<DrawList> CommandLists
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => new(Pointer->CommandLists, Pointer->CommandListsCount);
        }

        /// <summary> A helper to scale the ClipRects of each draw command. </summary>
        /// <param name="scale"> The desired scale. </param>
        /// <remarks>
        ///   Use this if your final output buffer is at a different scale than expected
        ///   or if your window resolution and your frame buffer resolution are not the same.
        /// </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void ScaleClipRects(Vector2 scale)
            => Native.ImDrawData.ScaleClipRects(Pointer, scale);

        /// <summary> Obtain the DrawData </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static DrawData Get()
            => Native.Methods.Main.GetDrawData();
    }
}
