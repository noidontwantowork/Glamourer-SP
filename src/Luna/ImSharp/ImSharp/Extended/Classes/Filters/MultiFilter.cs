namespace ImSharp;

/// <summary> A filter that combines multiple filters into one. </summary>
/// <typeparam name="TCacheItem"> The type of the item. </typeparam>
/// <remarks> This does not support drawing. </remarks>
public class MultiFilter<TCacheItem> : IFilter<TCacheItem>
{
    /// <summary> The list of combined filters. </summary>
    public readonly IReadOnlyList<IFilter<TCacheItem>> Filters;

    /// <summary> Create a filter combining several other filters. </summary>
    /// <param name="filters"> The filters that should be combined. </param>
    public MultiFilter(params IReadOnlyList<IFilter<TCacheItem>> filters)
        => Filters = filters;

    /// <summary> Create a filter combining several other filters. </summary>
    /// <param name="filters"> The filters that should be combined. </param>
    public MultiFilter(params IEnumerable<IFilter<TCacheItem>> filters)
        => Filters = filters.ToArray();

    /// <inheritdoc/>
    public bool WouldBeVisible(in TCacheItem item, int globalIndex)
    {
        foreach (var filter in Filters)
        {
            if (!filter.WouldBeVisible(item, globalIndex))
                return false;
        }

        return true;
    }

    /// <inheritdoc/>
    public event Action FilterChanged
    {
        add
        {
            foreach (var filter in Filters)
                filter.FilterChanged += value;
        }
        remove
        {
            foreach (var filter in Filters)
                filter.FilterChanged -= value;
        }
    }

    /// <summary> Not implemented for a multi filter. </summary>
    public bool DrawFilter(ReadOnlySpan<byte> label, Vector2 availableRegion)
    {
        return false;
    }

    /// <inheritdoc/>
    public bool Clear()
        => Filters.Aggregate(false, (current, filter) => current | filter.Clear());

    /// <inheritdoc/>
    public bool IsEmpty
        => Filters.All(f => f.IsEmpty);
}
