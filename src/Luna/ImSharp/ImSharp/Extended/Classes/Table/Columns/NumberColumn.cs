namespace ImSharp.Table;

/// <summary> A basic column to display and filter integers. </summary>
/// <typeparam name="TNumber"> The type of number to use, mainly for choosing between integrals and floating points. </typeparam>
/// <typeparam name="TCacheItem"> The type of the cached transformation of the items to display. </typeparam>
public abstract class NumberColumn<TNumber, TCacheItem> : BasicColumn<TCacheItem>
    where TNumber : unmanaged, INumber<TNumber>
{
    protected NumberColumn(NumberFilterMethod filterMethod = NumberFilterMethod.LessEqual)
        => Filter = new NumberFilter(this) { Method = filterMethod };

    /// <summary> The filter used. </summary>
    protected NumberFilterBase<TNumber, TCacheItem> Filter { get; init; }

    /// <summary> Get the numerical value for this column from the item. </summary>
    /// <param name="item"> The row to fetch. </param>
    /// <param name="globalIndex"> The global index of the row. </param>
    /// <returns> The numerical value. </returns>
    public abstract TNumber ToValue(in TCacheItem item, int globalIndex);

    /// <summary> Get the number to display for this column as a sized string to draw it right-aligned from the item. </summary>
    /// <param name="item"> The row to fetch. </param>
    /// <param name="globalIndex"> The global index of the row. </param>
    /// <returns> The textual representation of the numerical value prepared for display. </returns>
    protected abstract StringU8 DisplayNumber(in TCacheItem item, int globalIndex);

    /// <summary> Get the text on which this number is compared against textual filters. </summary>
    /// <param name="item"> The row to fetch. </param>
    /// <param name="globalIndex"> The global index of the row. </param>
    /// <returns> The textual representation of the numerical value prepared for text comparisons. </returns>
    protected abstract string ComparisonText(in TCacheItem item, int globalIndex);

    /// <summary> Compare using numerical values. </summary>
    public override int Compare(in TCacheItem lhs, int lhsGlobalIndex, in TCacheItem rhs, int rhsGlobalIndex)
        => ToValue(lhs, lhsGlobalIndex).CompareTo(ToValue(rhs, rhsGlobalIndex));

    /// <summary> Use the given filter method on the numerical value or text. </summary>
    public override bool WouldBeVisible(in TCacheItem item, int globalIndex)
        => Filter.WouldBeVisible(item, globalIndex);

    /// <summary> Draw the display number aligned to the right. </summary>
    public override void DrawColumn(in TCacheItem item, int globalIndex)
    {
        ImEx.TextRightAligned(DisplayNumber(item, globalIndex));
        if (Im.Item.Hovered(HoveredFlags.AllowWhenDisabled))
            DrawTooltip(item, globalIndex);
    }

    /// <summary> A tooltip to draw when the text is hovered. This is only called when the text is hovered, but does not start a tooltip itself. </summary>
    /// <param name="item"> The drawn row. </param>
    /// <param name="globalIndex"> The global index of the drawn row. </param>
    protected virtual void DrawTooltip(in TCacheItem item, int globalIndex)
    { }

    public override bool DrawFilter()
        => Filter.DrawFilter(Label, FilterContentRegion);

    protected sealed class NumberFilter(NumberColumn<TNumber, TCacheItem> parent) : NumberFilterBase<TNumber, TCacheItem>
    {
        /// <inheritdoc/>
        protected override string ToFilterString(in TCacheItem item, int globalIndex)
            => parent.ComparisonText(item, globalIndex);

        /// <inheritdoc/>
        public override TNumber ToValue(in TCacheItem item, int globalIndex)
            => parent.ToValue(item, globalIndex);
    }
}
