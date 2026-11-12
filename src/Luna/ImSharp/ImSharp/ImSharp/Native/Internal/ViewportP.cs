namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct ViewportP
            {
                public Viewport          ImGuiViewport;
                public int               Index;
                public int               LastFrameActive;
                public int               LastFrontMostStampCount;
                public ImGuiId           LastNameHash;
                public ImVec2            LastPosition;
                public float             Alpha;
                public float             LastAlpha;
                public short             PlatformMonitor;
                public ImBool            PlatformWindowCreated;
                public Window*           Window;
                public IntArray          DrawListsLastFrame;
                public DrawListArray     DrawLists;
                public ImDrawData        DrawDataP;
                public ImDrawDataBuilder DrawDataBuilder;
                public ImVec2            LastPlatformPosition;
                public ImVec2            LastPlatformSize;
                public ImVec2            WorkOffsetMin;
                public ImVec2            WorkOffsetMax;
                public ImVec2            BuildWorkOffsetMin;
                public ImVec2            BuildWorkOffsetMax;

                [InlineArray(2)]
                public struct IntArray
                {
                    private int _element;
                }

                [InlineArray(2)]
                public struct DrawListArray
                {
                    private Pointer<ImDrawList> _element;
                }
            }
        }
    }
}
