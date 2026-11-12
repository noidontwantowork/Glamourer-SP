namespace ImSharp.Table;

/// <summary> A basic column to display and filter text. </summary>
/// <typeparam name="TCacheItem"> The type of the cached transformation of the items to display. </typeparam>
public abstract class TextColumn<TCacheItem> : BasicColumn<TCacheItem>
{
    /// <summary> The filter used. </summary>
    protected RegexFilterBase<TCacheItem> Filter { get; init; }

    /// <remarks> Text columns should be resizable by default. </remarks>
    protected TextColumn()
    {
        Flags  &= ~TableColumnFlags.NoResize;
        Filter =  new RegexFilter(this);
    }

    /// <summary> Get the text used for comparison of items according to this column from the item. </summary>
    /// <param name="item"> The row to fetch. </param>
    /// <param name="globalIndex"> The global index of the row. </param>
    /// <returns> Text that is used for comparing against other rows. </returns>
    protected abstract string ComparisonText(in TCacheItem item, int globalIndex);

    /// <summary> Get the text displayed in the column from the item. </summary>
    /// <param name="item"> The row to fetch. </param>
    /// <param name="globalIndex"> The global index of the row. </param>
    /// <returns> Text that is displayed. </returns>
    protected abstract StringU8 DisplayText(in TCacheItem item, int globalIndex);

    /// <summary> Compares two rows according to the comparison text. </summary>
    /// <inheritdoc/>
    public override int Compare(in TCacheItem lhs, int lhsGlobalIndex, in TCacheItem rhs, int rhsGlobalIndex)
        => string.Compare(ComparisonText(lhs, lhsGlobalIndex), ComparisonText(rhs, rhsGlobalIndex), Filter.Comparison);

    /// <summary> Display the row using the display text. </summary>
    /// <inheritdoc/>
    public override void DrawColumn(in TCacheItem item, int globalIndex)
    {
        Im.Text(DisplayText(item, globalIndex));
        if (Im.Item.Hovered(HoveredFlags.AllowWhenDisabled))
            DrawTooltip(item, globalIndex);
    }

    /// <summary> A tooltip to draw when the text is hovered. This is only called when the text is hovered, but does not start a tooltip itself. </summary>
    /// <param name="item"> The drawn row. </param>
    /// <param name="globalIndex"> The global index of the drawn row. </param>
    protected virtual void DrawTooltip(in TCacheItem item, int globalIndex)
    { }

    /// <summary> Draw a textual input filter that uses the <see cref="BasicColumn{TCacheItem}.Label"/> as a hint and converts to regular expression if possible. </summary>
    /// <inheritdoc/>
    public override bool DrawFilter()
        => Filter.DrawFilter(Label, FilterContentRegion);

    /// <summary> Filter rows according to the regular expression, if set, otherwise on whether they contain the text. </summary>
    /// <inheritdoc/>
    public override bool WouldBeVisible(in TCacheItem item, int globalIndex)
        => Filter.WouldBeVisible(item, globalIndex);

    protected class RegexFilter(TextColumn<TCacheItem> parent) : RegexFilterBase<TCacheItem>
    {
        protected override string ToFilterString(in TCacheItem item, int globalIndex)
            => parent.ComparisonText(item, globalIndex);
    }
}
