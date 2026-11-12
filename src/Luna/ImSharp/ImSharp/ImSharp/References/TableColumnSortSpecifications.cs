namespace ImSharp;

public static partial class Im
{
    /// <summary> A reference to the column sort specifications of a single column in a table. </summary>
    /// <param name="pointer"> The native pointer to the column sort specifications. </param>
    public readonly unsafe struct TableColumnSortSpecifications(Native.TableColumnSortSpecs* pointer)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Native.TableColumnSortSpecs* Address = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator TableColumnSortSpecifications(Native.TableColumnSortSpecs* pointer)
            => new(pointer);

        /// <summary> The index of the column in the table. </summary>
        public short ColumnIndex
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Address->ColumnIndex;
        }


        /// <summary> The sort order of this column in case of multi-column sorting and the index in <seealso cref="TableSortSpecifications"/>. </summary>
        /// <remarks> If the table is sorted on a single column, this will be always be 0. </remarks>
        public short SortOrder
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Address->SortOrder;
        }

        /// <summary> The desired sort direction of this column. </summary>
        public SortDirection SortDirection
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Address->SortDirection;
        }

        /// <summary> The user ID of the column if specified by <seealso cref="TableDisposable.SetupColumn(ReadOnlySpan{byte},TableColumnFlags,float,ImGuiId)"/>. </summary>
        public ImGuiId ColumnUserId
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Address->ColumnUserId;
        }

        /// <summary> Deconstruct into a tuple of variables. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Deconstruct(out short columnIndex, out short sortOrder, out SortDirection sortDirection, out ImGuiId columnUserId)
        {
            columnIndex   = Address->ColumnIndex;
            sortOrder     = Address->SortOrder;
            sortDirection = Address->SortDirection;
            columnUserId  = Address->ColumnUserId;
        }

        /// <inheritdoc cref="Deconstruct(out short,out short,out ImSharp.SortDirection)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Deconstruct(out short columnIndex, out short sortOrder, out SortDirection sortDirection)
        {
            columnIndex   = Address->ColumnIndex;
            sortOrder     = Address->SortOrder;
            sortDirection = Address->SortDirection;
        }
    }
}
