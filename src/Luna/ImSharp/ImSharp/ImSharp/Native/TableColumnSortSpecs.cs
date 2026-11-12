namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        /// <summary> The sorting specifications of a single column in a table. </summary>
        public struct TableColumnSortSpecs
        {
            public ImGuiId       ColumnUserId;
            public short         ColumnIndex;
            public short         SortOrder;
            public SortDirection SortDirection;
        }
    }
}
