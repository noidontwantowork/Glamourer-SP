namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe struct ImFontBuilderIo
        {
            public delegate*<ImFontAtlas*, bool> Build;
        };
    }
}
