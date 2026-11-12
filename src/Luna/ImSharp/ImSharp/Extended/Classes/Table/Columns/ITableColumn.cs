using ImSharp.Table;

namespace ImSharp;

/// <summary> Interface for types that can be used to define columns for tables. </summary>
/// <typeparam name="TCacheItem"> The type of the item drawn per row. </typeparam>
public interface ITableColumn<TCacheItem>
{
    /// <summary> Get the label of the column as UTF8 string. HAS to be null-terminated. </summary>
    public ReadOnlySpan<byte> Label { get; }

    /// <summary> Get the flags used for this column. </summary>
    public TableColumnFlags Flags { get; }

    /// <summary> Compare two rows according to this column. </summary>
    /// <param name="lhs"> The left row. </param>
    /// <param name="lhsGlobalIndex"> The global index of the left row. </param>
    /// <param name="rhs"> The right row. </param>
    /// <param name="rhsGlobalIndex"> The global index of the right row. </param>
    /// <returns> A negative value if the left row compares smaller, positive if it compares larger, 0 if they compare equal. </returns>
    public int Compare(in TCacheItem lhs, int lhsGlobalIndex, in TCacheItem rhs, int rhsGlobalIndex);

    /// <summary> Whether the default width of this column needs to be updated when the list of items changes. </summary>
    public bool WidthDependsOnItems { get; }

    /// <summary> Compute the default width for this column. </summary>
    /// <param name="allItems"> All cache items if required to compute the width, can be ignored. </param>
    /// <returns> The default width of the column. </returns>
    /// <remarks> This is called when the font or style change, and depending on <see cref="WidthDependsOnItems"/> when the custom dirty flag is set. </remarks>
    public float ComputeWidth(IEnumerable<TCacheItem> allItems);

    /// <summary> Draw the filter in the header for this column. </summary>
    /// <returns> Whether the filter changed this frame. </returns>
    public bool DrawFilter();

    /// <summary> Draw a row's value for this column. </summary>
    /// <param name="item"> The row to draw. </param>
    /// <param name="globalIndex"> The global index of the row to draw. </param>
    public void DrawColumn(in TCacheItem item, int globalIndex);

    /// <summary> Execute actions before sorting is started. </summary>
    public void PreSort();

    /// <summary> Execute actions after sorting is started. </summary>
    public void PostSort();

    /// <summary> Execute column-specific actions after the parent table has finished drawing. </summary>
    /// <param name="cache"> The current table cache. </param>
    public void PostDraw(in TableCache<TCacheItem> cache);

    /// <summary> Filter rows according to this column's filter. </summary>
    /// <param name="item"> The row to check. </param>
    /// <param name="globalIndex"> The global index of the row to check. </param>
    /// <returns> True if the row should be visible, false if it should be filtered out. </returns>
    public bool WouldBeVisible(in TCacheItem item, int globalIndex);

    /// <summary> Compare two rows according to this column but in reverse order. </summary>
    /// <param name="lhs"> The left row. </param>
    /// <param name="lhsGlobalIndex"> The global index of the left row. </param>
    /// <param name="rhs"> The right row. </param>
    /// <param name="rhsGlobalIndex"> The global index of the right row. </param>
    /// <returns> A positive value if the left row compares smaller, negative if it compares larger, 0 if they compare equal. </returns>
    public int CompareInverse(in TCacheItem lhs, int lhsGlobalIndex, in TCacheItem rhs, int rhsGlobalIndex)
        => Compare(rhs, rhsGlobalIndex, lhs, lhsGlobalIndex);
}
