namespace ImSharp.Table;

/// <summary> A simple base implementation of a column. </summary>
/// <typeparam name="TCacheItem"> The type of the cached transformation of the items to display. </typeparam>
public abstract class BasicColumn<TCacheItem> : ITableColumn<TCacheItem>
{
    /// <summary> The label of the column, used in the header. </summary>
    public StringU8 Label { get; init; } = StringU8.Empty;

    /// <summary> The flags to draw the column with. </summary>
    public TableColumnFlags Flags { get; init; } = TableColumnFlags.NoResize;

    /// <summary> Whether the column width depends on the items displayed or not. </summary>
    public bool WidthDependsOnItems { get; init; } = false;

    /// <summary> The default width for the column in pixels. This gets scaled by <see cref="Im.ImGuiStyle.GlobalScale"/>. </summary>
    public float UnscaledWidth { get; init; } = 100f;

    /// <inheritdoc/>
    ReadOnlySpan<byte> ITableColumn<TCacheItem>.Label
        => Label.Span;

    /// <summary> Use the default comparer for the type to compare items. </summary>
    public virtual int Compare(in TCacheItem lhs, int lhsGlobalIndex, in TCacheItem rhs, int rhsGlobalIndex)
        => lhs is IComparable ? Comparer<TCacheItem>.Default.Compare(lhs, rhs) : 0;

    /// <summary> The width is just the scaled <see cref="UnscaledWidth"/>. </summary>
    public virtual float ComputeWidth(IEnumerable<TCacheItem> _)
        => UnscaledWidth * Im.Style.GlobalScale;

    /// <summary> Draw the header that is just a text filter of the label aligned to the frame. </summary>
    public virtual bool DrawFilter()
        => NopFilter<TCacheItem>.Instance.DrawFilter(Label, FilterContentRegion);

    /// <summary> Drawing the column still needs to be implemented. </summary>
    public abstract void DrawColumn(in TCacheItem item, int globalIndex);

    /// <inheritdoc/>
    public virtual void PreSort()
    { }

    /// <inheritdoc/>
    public virtual void PostSort()
    { }

    /// <inheritdoc/>
    public virtual void PostDraw(in TableCache<TCacheItem> cache)
    { }

    /// <summary> No filtering is supported. </summary>
    public virtual bool WouldBeVisible(in TCacheItem item, int globalIndex)
        => true;

    /// <summary> The available content region for a filter. </summary>
    protected static Vector2 FilterContentRegion
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get
        {
            var content = Im.ContentRegion.Available;
            content.X -= ImEx.Table.ArrowWidth;
            return content;
        }
    }
}
