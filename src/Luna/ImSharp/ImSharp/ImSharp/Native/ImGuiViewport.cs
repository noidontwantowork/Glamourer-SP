namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe struct Viewport
        {
            public ImGuiId       Id;
            public ViewportFlags Flags;
            public ImVec2        Pos;
            public ImVec2        Size;
            public ImVec2        WorkPos;
            public ImVec2        WorkSize;
            public float         DpiScale;
            public ImGuiId       ParentViewportId;
            public ImDrawData*   DrawDatA;
            public void*         RendererUserData;
            public void*         PlatformUserData;
            public void*         PlatformHandle;
            public void*         PlatformHandleRaw;
            public ImBool        PlatformRequestMove;
            public ImBool        PlatformRequestResize;
            public ImBool        PlatformRequestClose;
        }
    }
}
