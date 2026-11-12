using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct ComboPreviewData
            {
                public ImRect     PreviewRect;
                public ImVec2     BackupCursorPos;
                public ImVec2     BackupCursorMaxPos;
                public ImVec2     BackupCursorPosPrevLine;
                public float      BackupPrevLineTextBaseOffset;
                public LayoutType BackupLayout;
            }
        }
    }
}
