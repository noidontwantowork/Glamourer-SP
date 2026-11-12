using ImSharp.Containers;

namespace ImSharp;

/// <summary> A type-less base cache for items that are transformed for a cache and then can be filtered.  </summary>
public abstract class FilterCache : BasicCache
{
    /// <summary> Whether the cache owns the <see cref="FilterCache{TCacheItem}.UnfilteredItems"/> list and should dispose it and its content on disposal. </summary>
    protected bool DisposeItems { get; set; } = true;

    /// <summary> Whether <see cref="FilterCache{TCacheItem}.UnfilteredItems"/> is owned by this cache or not. </summary>
    protected bool UnfilteredItemsOwned = false;

    /// <summary> Whether the next update should re-filter the global data. </summary>
    protected bool FilterDirty { get; set; } = true;

    /// <summary> The global indices of items that are currently visible according to the filters. </summary>
    /// <remarks> Indices refer to <see cref="FilterCache{TCacheItem}.UnfilteredItems"/>. </remarks>
    protected readonly List<int> FilteredItems = [];

    /// <summary> Try to delete a single item from the list of cached items. </summary>
    /// <param name="index"> The unfiltered, global index of the item to delete. </param>
    /// <returns> True if the item was deleted. </returns>
    public abstract bool DeleteSingleItem(int index);
}

/// <summary> A base cache for items that are transformed for a cache and then can be filtered.  </summary>
/// <typeparam name="TCacheItem"> The transformed, cached item type. </typeparam>
public abstract class FilterCache<TCacheItem> : FilterCache, IReadOnlyList<TCacheItem>
{
    /// <summary> The pre-processed list of all available items to display. </summary>
    protected IReadOnlyList<TCacheItem> UnfilteredItems = [];

    /// <inheritdoc cref="UnfilteredItems"/>
    public IReadOnlyList<TCacheItem> AllItems
        => UnfilteredItems;

    /// <inheritdoc/>
    public sealed override bool DeleteSingleItem(int index)
    {
        if (!UnfilteredItemsOwned || index < 0 || index >= UnfilteredItems.Count)
            return false;

        var list = (List<TCacheItem>)UnfilteredItems;
        if (DisposeItems)
            (list[index] as IDisposable)?.Dispose();
        list.RemoveAt(index);
        for (var i = 0; i < FilteredItems.Count; ++i)
        {
            var filteredIndex = FilteredItems[i];
            if (filteredIndex == index)
                FilteredItems.RemoveAt(i--);
            else if (filteredIndex > index)
                FilteredItems[i] = filteredIndex - 1;
        }

        return true;
    }

    /// <summary> Try to update a single item from the list of cached items to a changed one. </summary>
    /// <param name="index"> The unfiltered, global index of the item to swap. </param>
    /// <param name="newValue"> The new data for the item. </param>
    /// <param name="disposeOld"> Whether to dispose the old item or not. </param>
    /// <returns> True if the item was updated. </returns>
    public bool UpdateSingleItem(int index, in TCacheItem newValue, bool disposeOld)
    {
        if (!UnfilteredItemsOwned || index < 0 || index >= UnfilteredItems.Count)
            return false;

        var list = (List<TCacheItem>)UnfilteredItems;
        if (DisposeItems && disposeOld)
            (list[index] as IDisposable)?.Dispose();
        list[index] = newValue;
        return true;
    }

    /// <summary> Update the actual item data if <see cref="BasicCache.CustomDirty"/>. </summary>
    protected virtual void UpdateData()
    {
        if (!CustomDirty)
            return;

        // Update all items and notify that we need to re-filter and re-sort.
        var items = GetItems();
        DisposeUnfilteredItems();
        if (items is IReadOnlyList<TCacheItem> list)
        {
            UnfilteredItems      = list;
            UnfilteredItemsOwned = false;
        }
        else
        {
            UnfilteredItems      = items.ToList();
            UnfilteredItemsOwned = true;
        }

        FilterDirty = true;
        OnDataUpdate();
        Dirty &= ~IManagedCache.DirtyFlags.Custom;
    }

    /// <summary> Update the cache. Called whenever it is fetched. </summary>
    public override void Update()
    {
        HandleAdapter();
        if (Dirty is not IManagedCache.DirtyFlags.Clean)
            UpdateData();

        if (FilterDirty)
            UpdateFilter();
    }

    /// <summary> Update the filtered items. </summary>
    protected virtual void UpdateFilter()
    {
        if (!FilterDirty)
            return;

        // Add all items that are visible according to all filters.
        FilteredItems.Clear();
        foreach (var (idx, item) in UnfilteredItems.Index())
        {
            if (WouldBeVisible(item, idx))
                FilteredItems.Add(idx);
        }

        // Notify that we have filtered.
        FilterDirty = false;
        OnFilterUpdate();
    }

    /// <summary> Check whether a specific item should be visible or is filtered out. </summary>
    /// <param name="item"> The item to check. </param>
    /// <param name="globalIndex"> The global index of the item to check. </param>
    /// <returns> True if the item is not filtered out. </returns>
    protected abstract bool WouldBeVisible(in TCacheItem item, int globalIndex);

    /// <summary> Get an enumeration of all available items before filtering. </summary>
    protected abstract IEnumerable<TCacheItem> GetItems();

    /// <summary> Invoked when the local data cache has been updated. </summary>
    protected virtual void OnDataUpdate()
    { }

    /// <summary> Invoked when the global items have been freshly filtered. </summary>
    protected virtual void OnFilterUpdate()
    { }

    /// <inheritdoc/>
    public IEnumerator<TCacheItem> GetEnumerator()
        => FilteredItems.Select(i => AllItems[i]).GetEnumerator();

    /// <summary> Get the visible items together with their global indices. </summary>
    public IEnumerable<(TCacheItem Item, int GlobalIndex)> GetItemsWithIndices()
        => FilteredItems.Select(i => (AllItems[i], i));

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <summary> The number of filtered items. </summary>
    public int Count
        => FilteredItems.Count;

    /// <summary> Get a filtered item by its index. </summary>
    public TCacheItem this[int index]
        => AllItems[FilteredItems[index]];

    /// <summary> Handle the case where our unfiltered list of items is a cache adapter. </summary>
    protected virtual void HandleAdapter()
    {
        if (UnfilteredItems is not CacheListAdapter adapter)
            return;

        // If we have a cache adapter, we do not care for the custom dirty flag.
        // Instead, we check only the adapter's own dirty flag and act accordingly.
        Dirty &= ~IManagedCache.DirtyFlags.Custom;
        if (!adapter.Dirty)
            return;

        FilterDirty = true;
        OnDataUpdate();
        adapter.Dirty = false;
    }

    /// <summary> Dispose of all current items in the cache if they are Disposable, as well as their current container. </summary>
    protected void DisposeUnfilteredItems()
    {
        if (!DisposeItems)
            return;

        if (typeof(TCacheItem).IsAssignableTo(typeof(IDisposable)))
            foreach (var item in UnfilteredItems)
                ((IDisposable)item!).Dispose();
        (UnfilteredItems as IDisposable)?.Dispose();
    }
}

/// <summary> A basic filter cache associated with a filter. </summary>
/// <typeparam name="TCacheItem"> The transformed, cached item type. </typeparam>
public abstract class BasicFilterCache<TCacheItem> : FilterCache<TCacheItem>
{
    /// <summary> The associated filter. </summary>
    public readonly IFilter<TCacheItem> Filter;

    /// <summary> Create a basic filter cache. </summary>
    /// <param name="filter"> The associated filter. </param>
    public BasicFilterCache(IFilter<TCacheItem> filter)
    {
        Filter               =  filter;
        Filter.FilterChanged += OnFilterChanged;
    }

    /// <inheritdoc/>
    protected override bool WouldBeVisible(in TCacheItem item, int globalIndex)
        => Filter.WouldBeVisible(item, globalIndex);

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(true);
        Filter.FilterChanged -= OnFilterChanged;
        DisposeUnfilteredItems();
    }

    /// <summary> Set the filter dirty. </summary>
    private void OnFilterChanged()
        => FilterDirty = true;
}
