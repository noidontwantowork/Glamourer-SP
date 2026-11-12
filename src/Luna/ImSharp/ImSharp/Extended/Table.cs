namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Wrapper around simple table draw methods. </summary>
    public static class Table
    {

        /// <summary> The default width of the sorting arrow in table headers. </summary>
        public const float UnscaledArrowWidth = 10;

        /// <summary> The scaled width of the sorting arrow in table headers. </summary>
        public static float ArrowWidth { get; internal set; }

        /// <summary> The frame background color used for highlighting active filter combos in table headers. </summary>
        public static Vector4 ActiveFilterColor { get; set; } = new Rgba32(0x803030A0).ToVector();

        /// <summary> Draw a simple table with the given data using the drawRow action. </summary>
        /// <typeparam name="T"> The type of the rows to draw. </typeparam>
        /// <param name="id"> The ID for the table. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="data"> The list of rows. </param>
        /// <param name="drawRow"> The action to draw a single row. </param>
        /// <param name="flags"> Additional flags to control the table's behavior. </param>
        /// <param name="columnTitles"> The titles for the separate columns and the resulting column count. </param>
        public static void Draw<T>(Utf8LabelHandler id, IEnumerable<T> data, Action<T> drawRow, TableFlags flags = TableFlags.None,
            params ReadOnlySpan<StringU8> columnTitles)
        {
            if (columnTitles.Length == 0)
                return;

            using var table = Im.Table.Begin(id, columnTitles.Length, flags);
            if (!table)
                return;

            foreach (var title in columnTitles)
            {
                table.NextColumn();
                table.Header(title);
            }

            foreach (var datum in data)
            {
                table.NextRow();
                drawRow(datum);
            }
        }

        /// <summary> Draw a simple table inside a collapsing header with the given data using the drawRow action. </summary>
        /// <typeparam name="T"> The type of the rows to draw. </typeparam>
        /// <param name="label"> The label for the collapsing header as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="data"> The list of rows. </param>
        /// <param name="drawRow"> The action to draw a single row. </param>
        /// <param name="flags"> Additional flags to control the table's behavior. </param>
        /// <param name="columnTitles"> The titles for the separate columns and the resulting column count. </param>
        public static void DrawTabbed<T>(Utf8LabelHandler label, IEnumerable<T> data, Action<T> drawRow, TableFlags flags = TableFlags.None,
            params ReadOnlySpan<StringU8> columnTitles)
        {
            using var header = Im.Tree.HeaderId(label);
            if (!header)
                return;

            Draw(""u8, data, drawRow, flags, columnTitles);
        }

        /// <inheritdoc cref="Draw{T}(Utf8LabelHandler,IEnumerable{T},Action{T},TableFlags,ReadOnlySpan{StringU8})"/>
        public static void Draw<T>(Utf8LabelHandler id, IEnumerable<T> data, Action<T> drawRow, TableFlags flags = TableFlags.None,
            params ReadOnlySpan<string> columnTitles)
        {
            if (columnTitles.Length == 0)
                return;

            using var table = Im.Table.Begin(id, columnTitles.Length, flags);
            if (!table)
                return;

            foreach (var title in columnTitles)
            {
                table.NextColumn();
                table.Header(title);
            }

            foreach (var datum in data)
            {
                table.NextRow();
                drawRow(datum);
            }
        }

        /// <inheritdoc cref="DrawTabbed{T}(Utf8LabelHandler,IEnumerable{T},Action{T},TableFlags,ReadOnlySpan{StringU8})"/>
        public static void DrawTabbed<T>(Utf8LabelHandler label, IEnumerable<T> data, Action<T> drawRow, TableFlags flags = TableFlags.None,
            params ReadOnlySpan<string> columnTitles)
        {
            using var header = Im.Tree.HeaderId(label);
            if (!header)
                return;

            Draw(""u8, data, drawRow, flags, columnTitles);
        }
    }
}
