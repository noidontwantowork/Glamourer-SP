namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui tables. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct TableDisposable : IDisposable
    {
        /// <summary> Whether creating the table succeeded. This needs to be checked before calling any of the member methods. </summary>
        public readonly bool Success;

        /// <summary> Whether the table is already ended. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a table and end it on leaving scope. </summary>
        /// <param name="id"> The table ID as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="columns"> The number of columns in the table. </param>
        /// <param name="flags"> Additional flags to control the table's behaviour. </param>
        /// <param name="outerSize">
        /// The size the table is fixed to.
        /// <list type="bullet">
        ///     <item> If this is non-positive in X, right-align from the available region. (0 means full available width). </item>
        ///     <item> If this is positive in X, set a fixed width. </item>
        ///     <item> If both scroll-bars are disabled and this is negative in Y, right-align from the available region. (0 means full available width). </item>
        /// </list>
        /// The behaviour in Y is dependent on the existence of scroll-bars and other <paramref name="flags"/> (see <see href="https://github.com/ocornut/imgui/blob/master/imgui_tables.cpp" >imgui_tables.cpp</see>).
        /// </param>
        /// <param name="innerWidth"> The inner width in case the horizontal scroll-bar is enabled. If 0, fits into <paramref name="outerSize"/>.X, otherwise overrides the scrolling width. Negative values make no sense. </param>
        /// <returns> A disposable object that evaluates to true if any part of the begun table is currently visible and should be checked before using table functionality. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal TableDisposable(scoped ref Utf8LabelHandler id, int columns, TableFlags flags, Vector2 outerSize, float innerWidth)
        {
            Success = Native.Methods.Table.BeginTable(id.Start(), columns, flags, outerSize, innerWidth);
            Alive   = true;
        }

        /// <summary> Go to the next row. </summary>
        /// <param name="flags"> Configure the behaviour of the row (e.g. make it a header row). </param>
        /// <param name="minimumHeight"> Force the row to have a certain minimum height. </param>
        /// <remarks>
        /// After calling this you still need to specify a column with either <seealso cref="NextColumn"/> or <seealso cref="GoToColumn"/>. <br/>
        /// Only call this if the table object evaluates to true.
        /// </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly void NextRow(TableRowFlags flags = TableRowFlags.None, float minimumHeight = 0)
        {
            ImGuiStateException.CheckState(Success, Alive, "NextRow", "Table");
            Native.Methods.Table.TableNextRow(flags, minimumHeight);
        }

        /// <summary> Go to the next column. </summary>
        /// <returns> True if the column is visible. </returns>
        /// <remarks>
        /// This can go to the next row if it is currently the last column in the table. <seealso cref="NextRow"/> is not required. <br/>
        /// Only call this if the table object evaluates to true.
        /// </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly bool NextColumn()
        {
            ImGuiStateException.CheckState(Success, Alive, "NextColumn", "Table");
            return Native.Methods.Table.TableNextColumn();
        }

        /// <summary> Go to the next column and draw text in it. </summary>
        /// <param name="text"> The text. Does not have to be null-terminated. </param>
        /// <returns> True if the column is visible. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly bool DrawColumn<T>(ref Utf8StringHandler<T> text) where T : IStringHandlerBuffer
        {
            if (!NextColumn())
                return false;

            Text(ref text);
            return true;
        }

        /// <summary> Draw a horizontal separator for the current cell. </summary>
        public readonly void DrawHorizontalSeparator()
        {
            using var clip = Drawing.PushClipRect(Rectangle.FromSize(Window.Position, Window.Size));
            Window.DrawList.Shape.Line(Cursor.ScreenPosition.AddY(-Style.CellPadding.Y),
                Cursor.ScreenPosition + new Vector2(Window.Width, -Style.CellPadding.Y),
                ImGuiColor.Separator.Get(), Style.GlobalScale);
        }

        /// <summary> Go to the next column and draw frame-aligned text in it. </summary>
        /// <param name="text"> The text. Does not have to be null-terminated. </param>
        /// <returns> True if the column is visible. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly bool DrawFrameColumn<T>(ref Utf8StringHandler<T> text) where T : IStringHandlerBuffer
        {
            if (!NextColumn())
                return false;

            ImEx.TextFrameAligned(ref text);
            return true;
        }

        /// <inheritdoc cref="DrawColumn{T}"/>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public readonly bool DrawColumn(Utf8TextHandler text)
            => DrawColumn(ref text);

        /// <inheritdoc cref="DrawFrameColumn{T}"/>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public readonly bool DrawFrameColumn(Utf8TextHandler text)
            => DrawFrameColumn(ref text);

        /// <summary> Go to the next column and draw frame-aligned text in it. If the text is not fully visible, add a tooltip on hovering showing the full text. </summary>
        /// <param name="text"> The text. Does not have to be null-terminated. </param>
        /// <returns> True if the column is visible. </returns>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public readonly bool DrawFrameColumnWithTooltip(Utf8TextHandler text)
        {
            if (!NextColumn())
                return false;

            ImEx.TextFrameAligned(ref text);
            if (Cursor.PositionPreviousLine.X >= ContentRegion.Maximum.X)
                Tooltip.OnHover(ref text);
            return true;
        }

        /// <summary> Draw a data line consisting of a label and an arbitrary text. </summary>
        /// <param name="label"> The label as text. Does not have to be null-terminated. Used in the first column. </param>
        /// <param name="text"> The text. Does not have to be null-terminated. Used in the second column. </param>
        /// <returns> True if either column is visible. </returns>
        /// <remarks> Generally used with tables consisting of two columns that just group up labels with data. </remarks>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public readonly bool DrawDataPair(Utf8LabelHandler label, Utf8TextHandler text)
            => DrawColumn(ref label) | DrawColumn(ref text);

        /// <summary> Draw a data line consisting of a label and arbitrary data put into an interpolated string. </summary>
        /// <param name="label"> The label as text. Does not have to be null-terminated. Used in the first column. </param>
        /// <param name="data"> The data. Used in the second column using its <see cref="object.ToString"/> method. </param>
        /// <returns> True if either column is visible. </returns>
        /// <remarks> Generally used with tables consisting of two columns that just group up labels with data. </remarks>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public readonly bool DrawDataPair<T>(Utf8LabelHandler label, in T data)
            => DrawColumn(ref label) | DrawColumn($"{data}");

        /// <summary> Jump to a specific column in this row. </summary>
        /// <param name="columnIndex"> The column to jump to. </param>
        /// <returns> True if the column is visible. </returns>
        /// <remarks> Only call this if the table object evaluates to true. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly bool GoToColumn(int columnIndex)
        {
            ImGuiStateException.CheckState(Success, Alive, "GoToColumn", "Table");
            return Native.Methods.Table.TableSetColumnIndex(columnIndex);
        }

        /// <summary> Configure a column before using it. </summary>
        /// <param name="label"> The label of the column and the text displayed in an automatically generated header row as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Flags that control the columns behaviour. </param>
        /// <param name="initWidthOrWeight"> Depending on the passed flags, the initial width of the column in pixels, or the weight when stretching available space. </param>
        /// <param name="userId"> A user ID for the column. </param>
        /// <remarks> Only call this if the table object evaluates to true. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly void SetupColumn(Utf8LabelHandler label, TableColumnFlags flags = TableColumnFlags.None, float initWidthOrWeight = 0,
            ImGuiId userId = default)
        {
            ImGuiStateException.CheckState(Success, Alive, "SetupColumn", "Table");
            Native.Methods.Table.TableSetupColumn(label.Start(), flags, initWidthOrWeight, userId);
        }

        /// <summary> Setup frozen columns or rows that are always visible even if the table is scrolled away from them. </summary>
        /// <param name="numFrozenColumns"> The number of columns from the left to freeze. </param>
        /// <param name="numFrozenRows"> The number of rows from the top to freeze. </param>
        /// <remarks>
        /// Call this after setting up columns but before drawing them. <br/>
        /// Only call this if the table object evaluates to true.
        /// </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly void SetupScrollFreeze(int numFrozenColumns, int numFrozenRows)
        {
            ImGuiStateException.CheckState(Success, Alive, "SetupScrollFreeze", "Table");
            Native.Methods.Table.TableSetupScrollFreeze(numFrozenColumns, numFrozenRows);
        }

        /// <summary> Create a header-styled cell. </summary>
        /// <param name="label"> The text in the header cell as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <remarks> Only call this if the table object evaluates to true. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly void Header(Utf8LabelHandler label)
        {
            ImGuiStateException.CheckState(Success, Alive, "Header", "Table");
            Native.Methods.Table.TableHeader(label.Start());
        }

        /// <summary> Create a row of headers from the columns setup by <seealso cref="SetupColumn(Utf8LabelHandler,TableColumnFlags,float,ImGuiId)"/>. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly void HeaderRow()
        {
            ImGuiStateException.CheckState(Success, Alive, "HeaderRow", "Table");
            Native.Methods.Table.TableHeadersRow();
        }

        /// <summary> Get a reference to the current table's sorting state and configuration. </summary>
        /// <remarks> Only call this if the table object evaluates to true. </remarks>
        public readonly TableSortSpecifications SortSpecifications
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImGuiStateException.CheckState(Success, Alive, "GetSortSpecs", "Table");
                return Native.Methods.Table.TableGetSortSpecs();
            }
        }

        /// <summary> Get the number of columns in the current table. </summary>
        /// <remarks> Only call this if the table object evaluates to true. </remarks>
        public readonly int ColumnCount
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get

            {
                ImGuiStateException.CheckState(Success, Alive, "ColumnCount", "Table");
                return Native.Methods.Table.TableGetColumnCount();
            }
        }

        /// <summary> Get the current column index in the current table. </summary>
        /// <returns> -1 if the current row has not set a column index, the index otherwise. </returns>
        /// <remarks> Only call this if the table object evaluates to true. </remarks>
        public readonly int CurrentColumn
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImGuiStateException.CheckState(Success, Alive, "CurrentColumn", "Table");
                return Native.Methods.Table.TableGetColumnIndex();
            }
        }

        /// <summary> Get the current row index in the current table. </summary>
        public readonly int CurrentRow
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImGuiStateException.CheckState(Success, Alive, "CurrentRow", "Table");
                return Native.Methods.Table.TableGetRowIndex();
            }
        }

        /// <summary> Obtain the name of a column in the current table. </summary>
        /// <param name="column"> The index of the queried column, use -1 (or default argument) to use the current column. </param>
        /// <returns>
        /// The name of the column as setup by <seealso cref="SetupColumn(Utf8LabelHandler,TableColumnFlags,float,ImGuiId)"/>, an empty string if nothing was specified. <br/>
        /// Only call this if the table object evaluates to true. <br/>
        /// The returned string is not owned.
        /// </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly ReadOnlySpan<byte> GetColumnName(int column = -1)
        {
            ImGuiStateException.CheckState(Success, Alive, "GetColumnName", "Table");
            return NullTerminationHelpers.GetSpan(Native.Methods.Table.TableGetColumnName(column));
        }

        /// <summary> Obtain the name of a column in the current table as a null-terminated, owned UTF8 string. </summary>
        /// <param name="column"> The index of the queried column, use -1 (or default argument) to use the current column. </param>
        /// <returns>
        /// The name of the column as setup by <seealso cref="SetupColumn(Utf8LabelHandler,TableColumnFlags,float,ImGuiId)"/>, an empty string if nothing was specified. <br/>
        /// Only call this if the table object evaluates to true. <br/>
        /// Prefer to use <seealso cref="GetColumnName"/> if no ownership of the string is needed.
        /// </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly StringU8 GetColumnNameOwned(int column = -1)
            => NullTerminationHelpers.GetClone(Native.Methods.Table.TableGetColumnName(column));

        /// <summary> Obtain the name of a column in the current table as UTF16 string. </summary>
        /// <param name="column"> The index of the queried column, use -1 (or default argument) to use the current column. </param>
        /// <returns>
        /// The name of the column as setup by <seealso cref="SetupColumn(Utf8LabelHandler,TableColumnFlags,float,ImGuiId)"/>, an empty string if nothing was specified. <br/>
        /// Only call this if the table object evaluates to true.
        /// </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly string GetColumnNameUtf16(int column = -1)
            => NullTerminationHelpers.GetString(Native.Methods.Table.TableGetColumnName(column));

        /// <summary> Obtain the configuration flags of a column in the current table. </summary>
        /// <param name="column"> The index of the queried column, use -1 (or default argument) to use the current column. </param>
        /// <returns> The configuration flags of the column. </returns>
        /// <remarks> Only call this if the table object evaluates to true. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly TableColumnFlags GetColumnFlags(int column)
        {
            ImGuiStateException.CheckState(Success, Alive, "GetColumnFlags", "Table");
            return Native.Methods.Table.TableGetColumnFlags(column);
        }

        /// <summary> Change the user-configurable accessibility state of aa column. </summary>
        /// <param name="column"> The index of the column to change. </param>
        /// <param name="enabled"> True to show, false to hide the column. </param>
        /// <remarks>
        /// Only call this if the table object evaluates to true. <br/>
        /// Users can change these in the right-click context menu on headers.
        /// </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly void SetColumnEnabled(int column, bool enabled)
        {
            ImGuiStateException.CheckState(Success, Alive, "SetColumnEnabled", "Table");
            Native.Methods.Table.TableSetColumnEnabled(column, enabled);
        }

        /// <summary> Get the index of the column currently hovered. </summary>
        /// <returns> -1 if the table is not hovered, the column count if the remaining space on the right side is hovered, or the index of the hovered column. </returns>
        /// <remarks> Only call this if the table object evaluates to true. </remarks>
        public readonly int HoveredColumn
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImGuiStateException.CheckState(Success, Alive, "GetHoveredColumn", "Table");
                return Native.Methods.Internal.TableGetHoveredColumn();
            }
        }

        /// <summary> Change the background color of a cell, row or column. </summary>
        /// <param name="target"> The target to change. </param>
        /// <param name="color"> The new background color. </param>
        /// <param name="column"> The column index, if applicable. </param>
        /// <remarks>
        /// Only call this if the table object evaluates to true. <br/>
        /// See  <seealso cref="TableBackgroundTarget"/> for target descriptions.
        /// </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly void SetBackgroundColor(TableBackgroundTarget target, Rgba32 color, int column = -1)
        {
            ImGuiStateException.CheckState(Success, Alive, "SetBackgroundColor", "Table");
            Native.Methods.Table.TableSetBgColor(target, color, column);
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(TableDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(TableDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(TableDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(TableDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(TableDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(TableDisposable i, bool value)
            => i.Success || value;

        /// <summary> End the Table on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            if (Success)
                Native.Methods.Table.EndTable();
            Alive = false;
        }

        /// <summary> End a Table without using an IDisposable. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.Table.EndTable();
    }
}
