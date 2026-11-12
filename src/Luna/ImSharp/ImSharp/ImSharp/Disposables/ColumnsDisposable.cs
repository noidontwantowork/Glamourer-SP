namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around creating pre-table style column separation. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct ColumnsDisposable : IDisposable
    {
        /// <summary> The columns before pushing this to revert to. </summary>
        public readonly int LastColumns;

        /// <summary> Get the current number of columns. </summary>
        public int Count
            => Native.Methods.Column.GetColumnsCount();

        /// <summary> Get the index of the current column. </summary>
        public int Current
            => Native.Methods.Column.GetColumnIndex();

        /// <summary> Move to the next column. </summary>
        public void Next()
            => Native.Methods.Column.NextColumn();

        /// <summary> Get or set the offset of the current column. </summary>
        public float Offset
        {
            get => Native.Methods.Column.GetColumnOffset(Current);
            set => Native.Methods.Column.SetColumnOffset(Current, value);
        }

        /// <summary> Get the offset of a column by index. </summary>
        public float GetOffset(int index)
            => Native.Methods.Column.GetColumnOffset(index);

        /// <summary> Set the offset of a column by index. </summary>
        public void SetOffset(int index, float value)
            => Native.Methods.Column.SetColumnOffset(index, value);

        /// <summary> Get or set the width of the current column. </summary>
        public float Width
        {
            get => Native.Methods.Column.GetColumnWidth(Current);
            set => Native.Methods.Column.SetColumnWidth(Current, value);
        }

        /// <summary> Get the width of a column by index. </summary>
        public float GetWidth(int index)
            => Native.Methods.Column.GetColumnWidth(index);

        /// <summary> Set the width of a column by index. </summary>
        public void SetWidth(int index, float width)
            => Native.Methods.Column.SetColumnWidth(index, width);

        /// <summary> Create a new column separation. </summary>
        /// <param name="count"> The number of columns to separate. </param>
        /// <param name="id"> An ID for the separation. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="border"> Whether the columns should be separated by borders. </param>
        /// <remarks> The columns system is outdated. Prefer to use <see cref="Table"/> instead. </remarks>
        public ColumnsDisposable(int count, scoped ref Utf8LabelHandler id, bool border = false)
        {
            LastColumns = Count;
            Native.Methods.Column.Columns(count, id.Start(out _), border);
        }

        /// <summary> Revert to the prior number of columns. </summary>
        public void Dispose()
            => Native.Methods.Column.Columns(Math.Max(LastColumns, 1), null, true);
    }
}
