namespace ImSharp.Table;

/// <summary> The data required to draw a basic table without information about the table's cache. </summary>
/// <typeparam name="TCacheItem"> The type of the cached transformation of the items to display. </typeparam>
public abstract class TableData<TCacheItem>
{
    /// <summary> The default flags for a table. </summary>
    public const TableFlags DefaultFlags = TableFlags.RowBackground
      | TableFlags.Sortable
      | TableFlags.BordersOuter
      | TableFlags.ScrollY
      | TableFlags.ScrollX
      | TableFlags.PreciseWidths
      | TableFlags.BordersInnerVertical
      | TableFlags.NoBordersInBodyUntilResize;

    /// <summary> The ID of the table. This is not displayed and just used for a unique ID. </summary>
    public readonly StringU8 Id;

    /// <summary> The column definitions for the table. </summary>
    public readonly IReadOnlyList<ITableColumn<TCacheItem>> Columns;

    /// <summary> The flags used when drawing the table. </summary>
    public TableFlags Flags { get; set; } = DefaultFlags;

    /// <summary> Whether the table can be sorted or not. </summary>
    public bool Sortable
    {
        get => Flags.HasFlag(TableFlags.Sortable);
        protected internal set => Flags = value ? Flags | TableFlags.Sortable : Flags & ~TableFlags.Sortable;
    }

    /// <summary> The total number of columns. </summary>
    public int TotalColumns
        => Columns.Count;

    /// <summary> The number of currently visible columns, should be updated when the cache is drawn. </summary>
    public int VisibleColumns { get; protected internal set; }

    /// <summary> The total number of items in the table, should be updated when the cache is updated. </summary>
    public int TotalItems   { get; protected internal set; }

    /// <summary> The number of un-filtered items in the table, should be updated when the cache is updated. </summary>
    public int VisibleItems { get; protected internal set; }

    /// <summary> Get the size the table should occupy in pixels. Default is the entire space available in the window. </summary>
    public virtual Vector2 GetSize()
        => Im.ContentRegion.Available;

    /// <summary> Get the desired number of frozen columns and rows in the table. Default is 1 for each. </summary>
    public virtual (int Columns, int Rows) GetFrozenScroll()
        => (1, 1);

    /// <summary> Get the items to display. This is only called when updating the cache with the <see cref="IManagedCache.DirtyFlags.Custom"/> flag. </summary>
    public abstract IEnumerable<TCacheItem> GetItems();

    /// <summary> Create the table data with a given list of column definitions and an ID. </summary>
    public TableData(StringU8 id, params IReadOnlyList<ITableColumn<TCacheItem>> columns)
    {
        Id             = id;
        Columns        = columns;
        VisibleColumns = Columns.Count;
    }
}

/// <summary> The base class for all actual tables. </summary>
/// <typeparam name="TCacheItem"> The type of the cached transformation of the items to display. </typeparam>
/// <typeparam name="TTableCache"> The type of the cache used for the table to actually draw it. </typeparam>
/// <param name="id"><inheritdoc cref="TableData{TCacheItem}.Id"/></param>
/// <param name="columns"><inheritdoc cref="TableData{TCacheItem}.Columns"/></param>
public abstract class TableBase<TCacheItem, TTableCache>
    (StringU8 id, params IReadOnlyList<ITableColumn<TCacheItem>> columns) : TableData<TCacheItem>(id, columns)
    where TTableCache : TableCache<TCacheItem>
{
    /// <summary> Draw the table. </summary>
    public void Draw()
    {
        // Push the ID to obtain the correct cache.
        using var idPush = Im.Id.Push(Id);

        // Obtain an existing or create a new cache.
        var cache = CacheManager.Instance.GetOrCreateCache(Im.Id.Current, CreateCache);

        // Draw.
        PreDraw(cache);
        cache.Draw();
        PostDraw(cache);
        foreach (var column in Columns)
            column.PostDraw(cache);
    }

    /// <summary> The factory function that creates the cache used to draw the table. </summary>
    protected abstract TTableCache CreateCache();

    /// <summary> Invoked before the table gets drawn. </summary>
    /// <param name="cache"> The current cache. </param>
    protected virtual void PreDraw(in TTableCache cache)
    { }

    /// <summary> Invoked after the table gets drawn. </summary>
    /// <param name="cache"> The current cache. </param>
    protected virtual void PostDraw(in TTableCache cache)
    { }
}

/// <summary> A default table with a simple, pre-implemented cache without extra functionality. </summary>
/// <typeparam name="TCacheItem"> The type of the cached transformation of the items to display. </typeparam>
/// <param name="id"><inheritdoc cref="TableData{TCacheItem}.Id"/></param>
/// <param name="columns"><inheritdoc cref="TableData{TCacheItem}.Columns"/></param>
public abstract class DefaultTable<TCacheItem>(StringU8 id, params IReadOnlyList<ITableColumn<TCacheItem>> columns)
    : TableBase<TCacheItem, TableCache<TCacheItem>>(id, columns)
{
    /// <summary> Create the default cache type. </summary>
    protected override TableCache<TCacheItem> CreateCache()
        => new(this);
}
