namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe struct ImDrawCmd
        {
            public ImVec4         ClipRect;
            public ImTextureId    TextureId;
            public uint           VertexOffset;
            public uint           IndexOffset;
            public uint           ElementCount;
            public ImDrawCallback UserCallback;
            public void*          UserCallbackData;
        };
    }
}
