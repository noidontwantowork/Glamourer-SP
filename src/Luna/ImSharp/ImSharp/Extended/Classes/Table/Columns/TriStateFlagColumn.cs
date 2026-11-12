namespace ImSharp.Table;

/// <summary> A table column representing a check on multiple pairs of flags that represent off- and on-states for some values. </summary>
/// <typeparam name="TEnum"> The type of the enum that defines the flags. </typeparam>
/// <typeparam name="TCacheItem"> The type of the cached transformation of the items to display. </typeparam>
public abstract class TriStateFlagColumn<TEnum, TCacheItem> : BasicColumn<TCacheItem>
    where TEnum : unmanaged, Enum
{
    /// <summary> The filter used. </summary>
    public TriStateFlagFilterBase<TCacheItem, TEnum> Filter { get; protected init; }

    /// <summary> Create a new Tri-State Flag Column. </summary>
    public TriStateFlagColumn()
        => Filter = new TriStateFlagFilter(this);

    /// <inheritdoc cref="TriStateFlagFilterBase{TCacheItem,TEnum}.EnumData"/>
    protected abstract IReadOnlyList<(TEnum On, TEnum Off, StringU8 Name)> TriEnumData { get; }

    /// <summary> Get the text to display for a row. </summary>
    /// <param name="item"> The row to check. </param>
    /// <param name="globalIndex"> The global index of the row to check </param>
    /// <returns> The text for this cell. </returns>
    protected abstract StringU8 DisplayString(in TCacheItem item, int globalIndex);

    /// <inheritdoc cref="TriStateFlagFilterBase{TCacheItem,TEnum}.GetValue"/>
    protected abstract bool GetValue(in TCacheItem item, int globalIndex, int triEnumIndex);

    /// <inheritdoc/>
    public override int Compare(in TCacheItem lhs, int lhsGlobalIndex, in TCacheItem rhs, int rhsGlobalIndex)
        => DisplayString(lhs, lhsGlobalIndex).CompareTo(DisplayString(rhs, rhsGlobalIndex));

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

    protected class TriStateFlagFilter : TriStateFlagFilterBase<TCacheItem, TEnum>
    {
        private readonly TriStateFlagColumn<TEnum, TCacheItem> _parent;

        public TriStateFlagFilter(TriStateFlagColumn<TEnum, TCacheItem> parent)
        {
            _parent     = parent;
            AllFlags    = _parent.TriEnumData.Aggregate(default(TEnum), (a, b) => a.Or(b.On).Or(b.Off));
            FilterValue = AllFlags;
        }

        /// <inheritdoc/>
        public sealed override TEnum FilterValue { get; protected set; }

        /// <inheritdoc/>
        public override IReadOnlyList<(TEnum On, TEnum Off, StringU8 Name)> EnumData
            => _parent.TriEnumData;

        /// <inheritdoc/>
        public override bool GetValue(in TCacheItem item, int globalIndex, int triEnumIndex)
            => _parent.GetValue(item, globalIndex, triEnumIndex);
    }
}
