namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct TableSettings
            {
                public ImGuiId          Id;
                public TableFlags       SaveFlags;
                public float            RefScale;
                public TableColumnIndex ColumnsCount;
                public TableColumnIndex ColumnsCountMax;
                public ImBool           WantApply;
            }
        }
    }
}
