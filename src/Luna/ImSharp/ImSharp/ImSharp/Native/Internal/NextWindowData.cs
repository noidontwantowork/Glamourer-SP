using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct NextWindowData
            {
                public NextWindowDataFlags                Flags;
                public Condition                          PositionCondition;
                public Condition                          SizeCondition;
                public Condition                          CollapsedCondition;
                public Condition                          DockCondition;
                public ImVec2                             PositionValue;
                public ImVec2                             PositionPivot;
                public ImVec2                             SizeValue;
                public ImVec2                             ContentSizeValue;
                public ImVec2                             ScrollValue;
                public ImBool                             PositionUndock;
                public ImBool                             CollapsedValue;
                public ImRect                             SizeConstraintRect;
                public delegate*<SizeCallbackData*, void> SizeCallback;
                public void*                              SizeCallbackUserData;
                public float                              BgAlphaValue;
                public ImGuiId                            ViewportId;
                public ImGuiId                            DockId;
                public WindowClass                        WindowClass;
                public ImVec2                             MenuBarOffsetMinVal;
            }
        }
    }
}
