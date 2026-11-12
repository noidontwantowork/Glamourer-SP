namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public struct WindowClass
        {
            public ImGuiId       ClassId;
            public ImGuiId       ParentViewportId;
            public ViewportFlags ViewportFlagsOverrideSet;
            public ViewportFlags ViewportFlagsOverrideClear;
            public TabItemFlags  TabItemFlagsOverrideSet;
            public DockNodeFlags DockNodeFlagsOverrideSet;
            public ImBool        DockingAlwaysTabBar;
            public ImBool        DockingAllowUnclassed;
        }
    }
}
