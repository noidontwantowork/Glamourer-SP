namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct TableTempData
            {
                public int                TableIndex;
                public float              LastTimeActive;
                public ImVec2             UserOuterSize;
                public ImDrawListSplitter DrawSplitter;
                public ImRect             HostBackupWorkRect;
                public ImRect             HostBackupParentWorkRect;
                public ImVec2             HostBackupPreviousLineSize;
                public ImVec2             HostBackupCurrentLineSize;
                public ImVec2             HostBackupCursorMaxPosition;
                public float              HostBackupColumnsOffset;
                public float              HostBackupItemWidth;
                public int                HostBackupItemWidthStackSize;
            }
        }
    }
}
