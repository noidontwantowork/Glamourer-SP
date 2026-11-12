namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe struct TableSortSpecs
        {
            public TableColumnSortSpecs* Columns;
            public int                   Count;
            public ImBool                Dirty;
        }
    }
}
