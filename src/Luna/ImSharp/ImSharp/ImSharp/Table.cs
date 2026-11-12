namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper class around Table related functions. </summary>
    /// <remarks> Prefer to use the functions from the table object itself, as they have additional safety checks when compiled in debug mode. Some functions are only available from the table object. </remarks>
    public static unsafe class Table
    {
        /// <inheritdoc cref="TableDisposable(ref Utf8LabelHandler,int,ImSharp.TableFlags,System.Numerics.Vector2,float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static TableDisposable Begin(Utf8LabelHandler id, int columns, TableFlags flags = TableFlags.None, Vector2 outerSize = default,
            float innerWidth = 0)
            => new(ref id, columns, flags, outerSize, innerWidth);

        /// <inheritdoc cref="TableDisposable.NextRow"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void NextRow(TableRowFlags flags = TableRowFlags.None, float minimumRowHeight = 0)
            => Native.Methods.Table.TableNextRow(flags, minimumRowHeight);

        /// <inheritdoc cref="TableDisposable.NextColumn"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool NextColumn()
            => Native.Methods.Table.TableNextColumn();

        /// <inheritdoc cref="TableDisposable.DrawColumn"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool DrawColumn(Utf8TextHandler text)
            => DrawColumn(ref text);

        /// <inheritdoc cref="TableDisposable.DrawColumn"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool DrawColumn<T>(ref Utf8StringHandler<T> text) where T : IStringHandlerBuffer
        {
            if (!Native.Methods.Table.TableNextColumn())
                return false;

            Text(ref text);
            return true;
        }

        /// <inheritdoc cref="TableDisposable.GoToColumn"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool GoToColumn(int columnIndex)
            => Native.Methods.Table.TableSetColumnIndex(columnIndex);

        /// <inheritdoc cref="TableDisposable.HeaderRow"/>
        public static void HeaderRow()
            => Native.Methods.Table.TableHeadersRow();

        /// <inheritdoc cref="TableDisposable.Header"/>
        public static void Header(Utf8LabelHandler label)
            => Native.Methods.Table.TableHeader(label.Start());

        /// <inheritdoc cref="TableDisposable.SetBackgroundColor"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetBackgroundColor(TableBackgroundTarget target, Rgba32 color, int column = -1)
            => Native.Methods.Table.TableSetBgColor(target, color, column);

        /// <inheritdoc cref="TableDisposable.SetBackgroundColor"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetBackgroundColor(TableBackgroundTarget target, ColorParameter color, int column = -1)
        {
            if(!color.IsDefault)
                Native.Methods.Table.TableSetBgColor(target, color.Color!.Value, column);
        }
    }
}
