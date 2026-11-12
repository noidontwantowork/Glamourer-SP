namespace ImSharp;

public static partial class Im
{
    /// <summary> A reference to the sort specifications of a table. </summary>
    /// <param name="pointer"> The native pointer to the sort specifications. </param>
    public readonly unsafe struct TableSortSpecifications(Native.TableSortSpecs* pointer) : IReadOnlyList<TableColumnSortSpecifications>
    {
        /// <summary> The address of the native object. </summary>
        public readonly Native.TableSortSpecs* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator TableSortSpecifications(Native.TableSortSpecs* pointer)
            => new(pointer);

        /// <summary> Whether the sort specifications have changed since the last call. </summary>
        /// <remarks> Make sure to set this to false after sorting. </remarks>
        public bool Dirty
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->Dirty;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->Dirty = value;
        }

        /// <summary> Get a reference to the sort specifications of the given column. </summary>
        public TableColumnSortSpecifications this[int index]
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->Columns + index;
        }

        /// <inheritdoc/>
        public IEnumerator<TableColumnSortSpecifications> GetEnumerator()
        {
            var count = Count;
            for (var i = 0; i < count; ++i)
                yield return this[i];
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <inheritdoc/>
        public int Count
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->Count;
        }

        /// <inheritdoc/>
        TableColumnSortSpecifications IReadOnlyList<TableColumnSortSpecifications>.this[int index]
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => this[index];
        }
    }
}
