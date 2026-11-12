namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct WindowSettings
            {
                public ImGuiId     Id;
                public ImVec2Short Position;
                public ImVec2Short Size;
                public ImVec2Short ViewportPosition;
                public ImGuiId     ViewportId;
                public ImGuiId     DockId;
                public ImGuiId     ClassId;
                public short       DockOrder;
                public ImBool      Collapsed;
                public ImBool      WantApply;
            }
        }
    }
}
