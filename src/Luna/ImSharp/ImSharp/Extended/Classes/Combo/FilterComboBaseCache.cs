namespace ImSharp;

/// <summary> The basic cache object used for <see cref="FilterComboBase{TCacheItem}"/>. </summary>
/// <typeparam name="TCacheItem"> The type of the cache items to draw. </typeparam>
/// <param name="parent"> The combo object this cache is created for. </param>
public class FilterComboBaseCache<TCacheItem>(FilterComboBase<TCacheItem> parent)
    : BasicFilterCache<TCacheItem>(parent.Filter)
{
    /// <summary> The parent filter combo using this cache. </summary>
    protected readonly FilterComboBase<TCacheItem> Parent = parent;

    /// <summary> The global index of the currently selected item if any. </summary>
    public int CurrentGlobalSelectionIndex { get; protected set; } = -1;

    /// <summary> The local index of the currently selected item if any. </summary>
    public int CurrentFilteredSelectionIndex { get; protected set; } = -1;

    /// <summary> Whether the scroll position should be set before drawing the list. </summary>
    protected bool SetScroll;

    /// <summary> Whether the popup should be closed in this frame. </summary>
    protected bool ClosePopup;

    /// <summary> The width with which to draw the expanded popup window. </summary>
    public float ComboWidth { get; protected set; }

    public virtual bool DrawList(out int selectedGlobalIndex)
    {
        // Create a child if we have drawn a filter before and thus the cursor isn't at 0, and we have more items than can be displayed.
        // We do not need a child if we have no filter, or can display all items either way.
        var       hasFilter  = Parent.Filter.IsVisible;
        var       beginChild = hasFilter && Count > Parent.MaximumItems;
        using var indent     = new Im.IndentDisposable();
        // Move the cursor upwards to center the selectables better, or remove the forced frame padding before the child.
        if (beginChild)
        {
            Im.Cursor.X = 0;
        }
        else
        {
            Im.Cursor.Y += Im.Style.ItemSpacing.Y / 2;
            indent.Indent(Im.Style.FramePadding.X);
        }

        using var child = beginChild ? Im.Child.Begin("child"u8, Im.ContentRegion.Available) : default;
        using var id    = Im.Id.Push("list"u8);

        // Try to find the current selection if we don't have one already.
        FindSelection();

        // Apply keyboard navigation. This also handles Enter and Escape.
        var ret = DrawKeyboardNavigation(out selectedGlobalIndex);

        // Set the current scroll position if necessary.
        if (SetScroll)
        {
            SetScroll = false;
            if (CurrentFilteredSelectionIndex >= 0)
                Im.Scroll.SetFromPositionY(CurrentFilteredSelectionIndex * Parent.ItemHeight - Im.Scroll.Y);
        }

        // Draw the clipped list of filtered items.
        Parent.PreDrawList();

        // Center the selectables better inside the child.
        if (beginChild)
            Im.Cursor.Y = Im.Style.ItemSpacing.Y / 2;
        using (var clipper = new Im.ListClipper(FilteredItems.Count, Parent.ItemHeight))
        {
            foreach (var globalIndex in clipper.Iterate(FilteredItems))
            {
                id.Push(globalIndex);
                // We need to move the selectables to align the text with the preview, if inside the child.
                if (beginChild)
                    Im.Cursor.X += Im.Style.FramePadding.X;

                if (Parent.DrawItem(UnfilteredItems[globalIndex], globalIndex, CurrentGlobalSelectionIndex == globalIndex))
                {
                    ret                 = true;
                    selectedGlobalIndex = globalIndex;
                    ClosePopup          = true;
                }

                id.Pop(globalIndex);
            }
        }

        // I don't know why this works, but it fixes the last selectable's offset to look nicer.
        Im.Cursor.Y -= Im.Style.GlobalScale;
        Parent.PostDrawList();

        // Close the popup if requested through keyboard navigation or selection.
        if (ClosePopup)
        {
            Im.Popup.CloseCurrent();
            Parent.OnPopupClosed();
            if (Parent.ClearFilterOnSelection)
                Parent.Filter.Clear();
            ClosePopup = false;
        }

        if (ret)
            CurrentGlobalSelectionIndex = -1;

        return ret;
    }

    /// <summary> Invoked when the combo popup is newly opened. </summary>
    protected virtual void FindSelection()
    {
        // Skip this if the window is not newly appearing, and we have a valid current selection.
        if (!Im.Window.Appearing && CurrentGlobalSelectionIndex >= 0)
            return;

        // We want to set the scroll position to the currently selected item if possible.
        SetScroll = true;

        // Search through the filtered items and set both indices if we find a selected match.
        foreach (var (filteredIndex, globalIndex) in FilteredItems.Index())
        {
            if (Parent.IsSelected(UnfilteredItems[globalIndex], globalIndex))
            {
                CurrentFilteredSelectionIndex = filteredIndex;
                CurrentGlobalSelectionIndex   = globalIndex;
                return;
            }
        }

        // If we reach this, there is no selected item in the filtered list.
        // Check the full list if there is a selected global item that is filtered out, otherwise nothing is selected.
        CurrentFilteredSelectionIndex = -1;
        foreach (var (globalIndex, item) in UnfilteredItems.Index())
        {
            if (Parent.IsSelected(item, globalIndex))
            {
                CurrentGlobalSelectionIndex = globalIndex;
                return;
            }
        }

        CurrentGlobalSelectionIndex = -1;
    }

    /// <summary> Use the parents function to get the items. </summary>
    protected override IEnumerable<TCacheItem> GetItems()
        => Parent.GetItems();

    /// <summary> Apply a mousewheel delta to the current selection. </summary>
    /// <param name="delta"> The mousewheel delta for this frame. </param>
    /// <param name="newIndex"> The global index of the newly selected item if any. </param>
    /// <returns> True if a new item was selected.</returns>
    public virtual bool HandleMouseWheel(int delta, out int newIndex)
    {
        // No changes if there are no items or the delta is 0.
        if (FilteredItems.Count is 0 || delta is 0)
        {
            newIndex = -1;
            return false;
        }

        // If we have only one item, we only incur a change if it was not selected before.
        if (FilteredItems.Count is 1)
        {
            var isChange = CurrentFilteredSelectionIndex is not 0;
            CurrentFilteredSelectionIndex = 0;
            CurrentGlobalSelectionIndex   = FilteredItems[0];
            newIndex                      = CurrentGlobalSelectionIndex;
            return isChange;
        }

        // If nothing is selected, try to find a selection.
        FindSelection();

        CurrentFilteredSelectionIndex = ImUtility.ApplyMouseWheelDelta(delta, CurrentFilteredSelectionIndex, FilteredItems.Count);
        CurrentGlobalSelectionIndex   = FilteredItems[CurrentFilteredSelectionIndex];
        newIndex                      = CurrentGlobalSelectionIndex;
        return true;
    }

    /// <summary> Apply keyboard navigation in the expanded combo list. </summary>
    /// <param name="selectedGlobalIndex"> If true is returned, the index of the newly selected item. </param>
    /// <returns> True if Enter is pressed and we have a selection. </returns>
    protected virtual bool DrawKeyboardNavigation(out int selectedGlobalIndex)
    {
        Im.GetIo().CaptureTextInput = true;
        // Enable keyboard navigation for going up and down,
        // jumping if reaching the end. This also scrolls to the element.
        if (FilteredItems.Count > 0)
        {
            if (Im.Keyboard.IsPressed(Key.DownArrow))
            {
                CurrentFilteredSelectionIndex = (CurrentFilteredSelectionIndex + 1) % FilteredItems.Count;
                CurrentGlobalSelectionIndex   = FilteredItems[CurrentFilteredSelectionIndex];
                SetScroll                     = true;
            }
            else if (Im.Keyboard.IsPressed(Key.UpArrow))
            {
                CurrentFilteredSelectionIndex = CurrentFilteredSelectionIndex < 0
                    ? FilteredItems.Count - 1
                    : (CurrentFilteredSelectionIndex - 1 + FilteredItems.Count) % FilteredItems.Count;
                CurrentGlobalSelectionIndex = FilteredItems[CurrentFilteredSelectionIndex];
                SetScroll                   = true;
            }
        }

        // Escape closes the popup without selection
        ClosePopup = Im.Keyboard.IsPressed(Key.Escape);

        // Enter selects the current selection if any, or the first available item.
        if (Im.Keyboard.IsPressed(Key.Enter))
        {
            ClosePopup = true;
            Parent.EnterPressed();
            if (CurrentFilteredSelectionIndex >= 0)
            {
                selectedGlobalIndex = FilteredItems[CurrentFilteredSelectionIndex];
                return true;
            }

            if (FilteredItems.Count > 0)
            {
                selectedGlobalIndex = FilteredItems[0];
                return true;
            }
        }

        selectedGlobalIndex = -1;
        return false;
    }

    /// <inheritdoc/>
    /// <remarks> Clears the filter according to the setting. </remarks>
    protected override void Dispose(bool disposing)
    {
        if (Parent.ClearFilterOnCacheDisposal)
            Parent.Filter.Clear();

        base.Dispose(disposing);
    }

    /// <inheritdoc/>
    protected override void UpdateFilter()
    {
        // Copy the default filter handling but keep the selected index if possible.
        if (!FilterDirty)
            return;

        // Add all items that are visible according to all filters.
        FilteredItems.Clear();
        CurrentFilteredSelectionIndex = -1;
        foreach (var (idx, item) in UnfilteredItems.Index())
        {
            if (WouldBeVisible(item, idx))
            {
                if (idx == CurrentGlobalSelectionIndex)
                    CurrentFilteredSelectionIndex = FilteredItems.Count;
                FilteredItems.Add(idx);
            }
        }

        // Notify that we have filtered.
        FilterDirty = false;
        OnFilterUpdate();
    }

    public override void Update()
    {
        var recomputeWidth =
            (Dirty & (IManagedCache.DirtyFlags.Font | IManagedCache.DirtyFlags.Style | IManagedCache.DirtyFlags.Custom))
            is not IManagedCache.DirtyFlags.Clean;
        base.Update();
        if (recomputeWidth)
            ComputeWidth();
        Dirty = IManagedCache.DirtyFlags.Clean;
    }

    /// <summary> Compute the required width for the combo. </summary>
    protected virtual void ComputeWidth()
        => ComboWidth = 200 * Im.Style.GlobalScale;
}
