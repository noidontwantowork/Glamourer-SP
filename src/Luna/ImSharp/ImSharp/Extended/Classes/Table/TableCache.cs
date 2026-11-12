namespace ImSharp.Table;

/// <summary> The base implementation of a table cache, used to store a table's pre-processed data and it's filters as long as it is active. </summary>
/// <typeparam name="TCacheItem"> The type of the cached transformation of the items to display. </typeparam>
/// <param name="parent"> The table's base data and column definitions that were used to create this cache. </param>
public class TableCache<TCacheItem>(TableData<TCacheItem> parent) : FilterCache<TCacheItem>
{
    /// <summary> Whether the next update should re-sort the filtered data. </summary>
    protected bool SortDirty { get; set; } = true;

    /// <summary> The index for the column that is currently used for sorting. </summary>
    protected int SortIndex { get; set; } = -1;

    /// <summary> The sort direction in the column that is currently used for sorting, if any. </summary>
    protected SortDirection SortDirection { get; set; } = SortDirection.Ascending;

    /// <summary> Whether the data is still loading in some way. </summary>
    public bool Loading { get; protected set; } = false;

    /// <summary> The table's base data and column definitions that were used to create this cache. </summary>
    public TableData<TCacheItem> Parent { get; } = parent;

    /// <summary>
    ///   The default widths columns are drawn with if not resized by the user.
    ///   Gets updated when font or style change, or for headers with <see cref="ITableColumn{TCacheItem}.WidthDependsOnItems"/> when the custom data changes.
    /// </summary>
    protected readonly float[] HeaderDefaultWidths = new float[parent.Columns.Count];

    /// <summary> Draw the actual table. </summary>
    public void Draw()
    {
        if (Loading)
        {
            using var child = Im.Child.Begin("Table"u8, Parent.GetSize(), true);
            if (!child)
                return;

            var content   = Im.ContentRegion.Available;
            var minRadius = Math.Min(content.X, content.Y) / 3;
            Im.Cursor.X += content.X / 2 - minRadius;
            Im.Cursor.Y += content.Y / 2 - minRadius;
            ImEx.Spinner("Loading"u8, minRadius, 10, Im.Color.Get(ImGuiColor.Text));
            return;
        }

        // Use the table data to set up the table. We do not need to provide an ID since this is already pushed to get the cache.
        using var table = Im.Table.Begin("Table"u8, Parent.Columns.Count, Parent.Flags, Parent.GetSize());
        if (!table)
            return;

        // Nothing to draw without columns.
        if (Parent.Columns.Count is 0)
            return;

        // Set up the scroll freeze according to table data.
        var (columns, rows) = Parent.GetFrozenScroll();
        table.SetupScrollFreeze(columns, rows);

        // Widths have been updated when fetching the cache, set up columns.
        foreach (var (header, width) in Parent.Columns.Zip(HeaderDefaultWidths))
            table.SetupColumn(header.Label, header.Flags | (width > 1 ? TableColumnFlags.WidthFixed : TableColumnFlags.WidthStretch), width);

        // Draw the headers and filters, count visible columns.
        table.NextRow(TableRowFlags.Headers);
        Parent.VisibleColumns = 0;
        foreach (var (index, header) in Parent.Columns.Index())
        {
            using var id = Im.Id.Push(index);
            if (table.GetColumnFlags(index).HasFlag(TableColumnFlags.IsEnabled))
                ++Parent.VisibleColumns;
            else
                continue;

            if (!table.GoToColumn(index))
                continue;

            // Draw a header with no text to color the cell,
            // then draw the actual filter.
            table.Header(StringU8.Empty);
            Im.Line.NoSpacing();
            if (header.DrawFilter())
                FilterDirty = true;
        }

        // Update the sort state. Needs to be done during the table's draw because it requires it's sort specifications.
        CheckSort(table);
        UpdateSort();

        // Draw the visible rows using a clipper.
        using var clipper = new Im.ListClipper(FilteredItems.Count, 0);
        foreach (var globalIndex in clipper.Iterate(FilteredItems))
            DrawItem(table, UnfilteredItems[globalIndex], globalIndex);
    }

    /// <summary> Update the cache. Called whenever it is fetched. </summary>
    public override void Update()
    {
        HandleAdapter();
        if (Dirty is not IManagedCache.DirtyFlags.Clean)
        {
            UpdateData();
            UpdateColumnWidths();
            Dirty = IManagedCache.DirtyFlags.Clean;
        }

        UpdateFilter();
    }

    /// <summary> Update the column widths if the font or style changed, or the available items have changed and the column definition cares for that. </summary>
    protected virtual void UpdateColumnWidths()
    {
        // Can skip if neither of the flags is set.
        if ((Dirty & (IManagedCache.DirtyFlags.Font | IManagedCache.DirtyFlags.Style | IManagedCache.DirtyFlags.Custom))
            is IManagedCache.DirtyFlags.Clean)
            return;

        // Update the arrow width always, cheaper than checking for flags again.
        ImEx.Table.ArrowWidth = ImEx.Table.UnscaledArrowWidth * Im.Style.GlobalScale;

        for (var i = 0; i < HeaderDefaultWidths.Length; ++i)
        {
            // Update each column width if font or style changed.
            var header = Parent.Columns[i];
            var flags  = IManagedCache.DirtyFlags.Font | IManagedCache.DirtyFlags.Style;
            // But only update on item change if the column cares about that.
            if (header.WidthDependsOnItems)
                flags |= IManagedCache.DirtyFlags.Custom;

            if ((Dirty & flags) is not IManagedCache.DirtyFlags.Clean)
                HeaderDefaultWidths[i] = header.ComputeWidth(UnfilteredItems);
        }
    }

    /// <summary> Check each single row for visibility against the filters in all columns. </summary>
    /// <param name="value"> The row to check. </param>
    /// <param name="globalIndex"> The global index of the row. </param>
    /// <returns> True if the row is visible, false otherwise. </returns>
    protected override bool WouldBeVisible(in TCacheItem value, int globalIndex)
    {
        // No LINQ due to 'in' modifier.
        foreach (var header in Parent.Columns)
        {
            if (!header.WouldBeVisible(value, globalIndex))
                return false;
        }

        return true;
    }

    /// <summary> Update the sorting of the filtered items according to the current sort index. </summary>
    protected virtual void UpdateSort()
    {
        if (!SortDirty)
            return;

        // If the sort index is not usable, set it to -1.
        if (Parent.Columns.Count <= SortIndex)
            SortIndex = -1;

        // Return if there is no sorting specified.
        if (SortIndex < 0)
        {
            SortDirty = false;
            return;
        }

        // Sort according to the chosen header and direction.
        var header = Parent.Columns[SortIndex];
        switch (SortDirection)
        {
            case SortDirection.Ascending:
                header.PreSort();
                StableSort(header, false);
                header.PostSort();
                break;
            case SortDirection.Descending:
                header.PreSort();
                StableSort(header, true);
                header.PostSort();
                break;
            default: SortIndex = -1; break;
        }

        SortDirty = false;
    }

    /// <summary> Sort the existing filtered items in the correct direction using a stable sort. </summary>
    /// <param name="column"> The column to use the comparer of. </param>
    /// <param name="descending"> The direction to sort in. </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void StableSort(ITableColumn<TCacheItem> column, bool descending)
    {
        var tmpList = FilteredItems.Index().ToList();
        if (descending)
            tmpList.Sort((a, b) =>
            {
                var ret = column.CompareInverse(UnfilteredItems[a.Item], a.Item, UnfilteredItems[b.Item], b.Item);
                return ret is not 0 ? ret : a.Index.CompareTo(b.Index);
            });
        else
            tmpList.Sort((a, b) =>
            {
                var ret = column.Compare(UnfilteredItems[a.Item], a.Item, UnfilteredItems[b.Item], b.Item);
                return ret is not 0 ? ret : a.Index.CompareTo(b.Index);
            });
        var i = 0;
        foreach (var (_, item) in tmpList)
            FilteredItems[i++] = item;
    }

    /// <summary> Draw a row. </summary>
    /// <param name="table"> The table we are currently in. </param>
    /// <param name="item"> The row to draw. </param>
    /// <param name="globalIndex"> The global index of the row to draw. </param>
    protected virtual void DrawItem(in Im.TableDisposable table, in TCacheItem item, int globalIndex)
    {
        // Every row has its own global ID.
        using var id = Im.Id.Push(globalIndex);
        foreach (var (column, header) in Parent.Columns.Index())
        {
            if (!table.NextColumn())
                continue;

            // And then an additional column ID.
            id.Push(column);
            header.DrawColumn(item, globalIndex);
            id.Pop();
        }
    }

    /// <summary> Check the sort specifications of the current table and update the sort flag. </summary>
    protected virtual void CheckSort(in Im.TableDisposable table)
    {
        var fullSpecs = table.SortSpecifications;
        if (!fullSpecs.Dirty && !SortDirty)
            return;

        fullSpecs.Dirty = false;
        SortDirty       = true;
        // No sort specifications at all.
        if (fullSpecs.Count is 0)
        {
            SortIndex = -1;
            SortDirty = false;
            return;
        }

        // We only take into account the current sorting. Others are handled through stability or not at all.
        var specs = fullSpecs[0];
        SortIndex     = specs.ColumnIndex;
        SortDirection = specs.SortDirection;
    }

    /// <inheritdoc/>
    protected override IEnumerable<TCacheItem> GetItems()
        => Parent.GetItems();

    /// <inheritdoc/>
    protected override void OnDataUpdate()
    {
        SortDirty         = true;
        Parent.TotalItems = UnfilteredItems.Count;
    }

    /// <inheritdoc/>
    protected override void OnFilterUpdate()
    {
        SortDirty           = true;
        Parent.VisibleItems = FilteredItems.Count;
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(true);
        DisposeUnfilteredItems();
    }

    /// <summary> Load the sort order of the table. </summary>
    public override void ApplyStoredData(object? existingData)
    {
        if (existingData is not StoredData data)
            return;

        SortDirty     = true;
        SortIndex     = data.SortIndex;
        SortDirection = data.SortDirection;
    }

    /// <summary> Save the sort order of the table. </summary>
    public override object SaveStoredData()
        => new StoredData(SortIndex, SortDirection);

    private sealed record StoredData(int SortIndex, SortDirection SortDirection);
}
