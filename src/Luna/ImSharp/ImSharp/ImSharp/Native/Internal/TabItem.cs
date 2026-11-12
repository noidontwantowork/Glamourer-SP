namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct TabItem
            {
                public ImGuiId      Id;
                public TabItemFlags Flags;
                public Window*      Window;
                public int          LastFrameVisible;
                public int          LastFrameSelected;
                public float        Offset;
                public float        Width;
                public float        ContentWidth;
                public float        RequestedWidth;
                public int          NameOffset;
                public short        BeginOrder;
                public short        IndexDuringLayout;
                public ImBool       WantClose;
            }
        }
    }
}
