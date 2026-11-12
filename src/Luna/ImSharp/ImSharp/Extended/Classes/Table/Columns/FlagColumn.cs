namespace ImSharp.Table;

/// <summary> A table column representing multiple flags that represent some values. </summary>
/// <typeparam name="TEnum"> The type of the enum that defines the flags. </typeparam>
/// <typeparam name="TCacheItem"> The type of the cached transformation of the items to display. </typeparam>
public abstract class FlagColumn<TEnum, TCacheItem> : BasicColumn<TCacheItem>
    where TEnum : unmanaged, Enum
{
    /// <summary> The filter used. </summary>
    public FlagFilterBase<TCacheItem, TEnum> Filter { get; init; }

    /// <summary> Create a new Flag Column. </summary>
    public FlagColumn()
        => Filter = new FlagFilter(this);

    /// <summary> Create a new Flag Column without setting up a filter. </summary>
    /// <remarks> Use this is you set up the enum data through a constructor parameter and set the filter manually. </remarks>
    protected FlagColumn(bool _)
        => Filter = null!;

    /// <summary> Get the text to display for a row. </summary>
    /// <param name="item"> The row to check. </param>
    /// <param name="globalIndex"> The global index of the row to check </param>
    /// <returns> The text for this cell. </returns>
    protected abstract StringU8 DisplayString(in TCacheItem item, int globalIndex);

    protected abstract IReadOnlyList<(TEnum Value, StringU8 Name)> EnumData { get; }

    /// <inheritdoc cref="FlagFilterBase{TCacheItem,TEnum}.GetValue"/>
    protected abstract TEnum GetValue(in TCacheItem item, int globalIndex);

    /// <inheritdoc/>
    public override int Compare(in TCacheItem lhs, int lhsGlobalIndex, in TCacheItem rhs, int rhsGlobalIndex)
        => Comparer<TEnum>.Default.Compare(Filter.GetValue(lhs, lhsGlobalIndex), Filter.GetValue(rhs, rhsGlobalIndex));

    /// <inheritdoc/>
    public override bool WouldBeVisible(in TCacheItem item, int globalIndex)
        => Filter.WouldBeVisible(item, globalIndex);

    /// <inheritdoc/>
    public override void DrawColumn(in TCacheItem item, int globalIndex)
    {
        Im.Text(DisplayString(item, globalIndex));
        if (Im.Item.Hovered(HoveredFlags.AllowWhenDisabled))
            DrawTooltip(item, globalIndex);
    }

    /// <summary> A tooltip to draw when the text is hovered. This is only called when the text is hovered, but does not start a tooltip itself. </summary>
    /// <param name="item"> The drawn row. </param>
    /// <param name="globalIndex"> The global index of the drawn row. </param>
    protected virtual void DrawTooltip(in TCacheItem item, int globalIndex)
    { }

    /// <summary> Draw an expandable combo filter for the separate flags. </summary>
    /// <inheritdoc/>
    public override bool DrawFilter()
        => Filter.DrawFilter(Label, FilterContentRegion);

    protected class FlagFilter : FlagFilterBase<TCacheItem, TEnum>
    {
        protected readonly FlagColumn<TEnum, TCacheItem> Parent;

        public FlagFilter(FlagColumn<TEnum, TCacheItem> parent)
        {
            Parent      = parent;
            AllFlags    = Parent.EnumData.Aggregate(default(TEnum), (a, b) => a.Or(b.Value));
            FilterValue = AllFlags;
        }

        /// <inheritdoc/>
        public override IReadOnlyList<(TEnum Value, StringU8 Name)> EnumData
            => Parent.EnumData;

        /// <inheritdoc/>
        public override TEnum GetValue(in TCacheItem item, int globalIndex)
            => Parent.GetValue(item, globalIndex);

        /// <inheritdoc/>
        public sealed override TEnum FilterValue { get; protected set; }
    }
}
