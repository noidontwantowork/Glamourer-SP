namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct PopupData
            {
                public ImGuiId PopupId;
                public Window* Window;
                public Window* SourceWindow;
                public int     ParentNavLayer;
                public int     OpenFrameCount;
                public ImGuiId OpenParentId;
                public ImVec2  OpenPopupPosition;
                public ImVec2  OpenMousePosition;
            }
        }
    }
}
