namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct GroupData
            {
                public ImGuiId WindowId;
                public ImVec2  BackupCursorPos;
                public ImVec2  BackupCursorMaxPos;
                public float   BackupIndent;
                public float   BackupGroupOffset;
                public ImVec2  BackupCurrLineSize;
                public float   BackupCurrLineTextBaseOffset;
                public ImGuiId BackupActiveIdIsAlive;
                public ImBool  BackupActiveIdPreviousFrameIsAlive;
                public ImBool  BackupHoveredIdIsAlive;
                public ImBool  EmitItem;
            }
        }
    }
}
