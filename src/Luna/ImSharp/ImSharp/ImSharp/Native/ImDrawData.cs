namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe partial struct ImDrawData
        {
            public ImBool       Valid;
            public int          CommandListsCount;
            public int          TotalIndexCount;
            public int          TotalVertexCount;
            public ImDrawList** CommandLists;
            public ImVec2       DisplayPos;
            public ImVec2       DisplaySize;
            public ImVec2       FramebufferScale;
            public Viewport*    OwnerViewport;

            [MethodImpl(ImSharpConfiguration.Inl)]
            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawData_ScaleClipRects")]
            public static partial void ScaleClipRects(ImDrawData* self, ImVec2 scale);
        }
    }
}
