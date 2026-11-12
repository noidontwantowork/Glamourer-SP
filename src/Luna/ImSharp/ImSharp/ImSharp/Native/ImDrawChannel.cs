namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public struct ImDrawChannel
        {
            public ImVector<ImDrawCmd> CommandBuffer;
            public ImVector<ImDrawIdx> IndexBuffer;
        };
    }
}
