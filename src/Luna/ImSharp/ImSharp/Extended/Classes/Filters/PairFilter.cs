namespace ImSharp;

/// <summary> A filter that combines two filters into one. </summary>
/// <typeparam name="TCacheItem"> The type of the item. </typeparam>
/// <remarks> This does not support drawing. </remarks>
public class PairFilter<TCacheItem> : IFilter<TCacheItem>
{
    /// <summary> The first combined filter. </summary>
    public readonly IFilter<TCacheItem> Filter1;

    /// <summary> The second combined filter. </summary>
    public readonly IFilter<TCacheItem> Filter2;

    /// <summary> Create a filter combining two other filters. </summary>
    /// <param name="filter1"> The first filter that should be combined. </param>
    /// <param name="filter2"> The second filter that should be combined. </param>
    public PairFilter(IFilter<TCacheItem> filter1, IFilter<TCacheItem> filter2)
    {
        Filter1 = filter1;
        Filter2 = filter2;
    }

    /// <inheritdoc/>
    public bool WouldBeVisible(in TCacheItem item, int globalIndex)
        => Filter1.WouldBeVisible(item, globalIndex) && Filter2.WouldBeVisible(item, globalIndex);

    /// <inheritdoc/>
    public event Action? FilterChanged
    {
        add
        {
            Filter1.FilterChanged += value;
            Filter2.FilterChanged += value;
        }
        remove
        {
            Filter1.FilterChanged -= value;
            Filter2.FilterChanged -= value;
        }
    }

    /// <summary> Not implemented for a default pair filter. </summary>
    public virtual bool DrawFilter(ReadOnlySpan<byte> label, Vector2 availableRegion)
    {
        return false;
    }

    /// <inheritdoc/>
    public bool Clear()
        => Filter1.Clear() | Filter2.Clear();

    /// <inheritdoc/>
    public bool IsEmpty
        => Filter1.IsEmpty && Filter2.IsEmpty;
}
