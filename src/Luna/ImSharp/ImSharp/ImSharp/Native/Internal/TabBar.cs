namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct TabBar
            {
                public ImVector<TabItem> Tabs;
                public TabBarFlags       Flags;
                public ImGuiId           Id;
                public ImGuiId           SelectedTabId;
                public ImGuiId           NextSelectedTabId;
                public ImGuiId           VisibleTabId;
                public int               CurrentFrameVisible;
                public int               PreviousFrameVisible;
                public ImRect            BarRect;
                public float             CurrentTabsContentsHeight;
                public float             PreviousTabsContentsHeight;
                public float             WidthAllTabs;
                public float             WidthAllTabsIdeal;
                public float             ScrollingAnim;
                public float             ScrollingTarget;
                public float             ScrollingTargetDistanceToVisibility;
                public float             ScrollingSpeed;
                public float             ScrollingRectMinX;
                public float             ScrollingRectMaxX;
                public ImGuiId           ReorderRequestTabId;
                public short             ReorderRequestOffset;
                public sbyte             BeginCount;
                public ImBool            WantLayout;
                public ImBool            VisibleTabWasSubmitted;
                public ImBool            TabsAddedNew;
                public short             TabsActiveCount;
                public short             LastTabItemIndex;
                public float             ItemSpacingY;
                public ImVec2            FramePadding;
                public ImVec2            BackupCursorPosition;
                public TextBuffer        TabsNames;
            }
        }
    }
}
