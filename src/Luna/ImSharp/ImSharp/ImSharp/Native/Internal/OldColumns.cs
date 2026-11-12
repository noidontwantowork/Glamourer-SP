using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct OldColumns
            {
                public ImGuiId                 Id;
                public OldColumnFlags          Flags;
                public ImBool                  IsFirstFrame;
                public ImBool                  IsBeingResized;
                public int                     Current;
                public int                     Count;
                public float                   OffMinX;
                public float                   OffMaxX;
                public float                   LineMinY;
                public float                   LineMaxY;
                public float                   HostCursorPositionY;
                public float                   HostCursorMaxPositionX;
                public ImRect                  HostInitialClipRect;
                public ImRect                  HostBackupClipRect;
                public ImRect                  HostBackupParentWorkRect;
                public ImVector<OldColumnData> Columns;
                public ImDrawListSplitter      Splitter;
            }
        }
    }
}
