namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public struct ImDrawCmdHeader
        {
            public ImVec4      ClipRect;
            public ImTextureId TextureId;
            public uint        VertexOffset;
        };
    }
}
