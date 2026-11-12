namespace ImSharp;

/// <summary> A simple filter combo using <see cref="SimpleCacheItem{T}"/> for an arbitrary type. </summary>
/// <typeparam name="T"> The item type to display. </typeparam>
public abstract class SimpleFilterCombo<T> : FilterComboBase<SimpleCacheItem<T>>
{
    /// <summary> The currently selected object, updated on Draw. </summary>
    protected T? Current;

    /// <summary> Create a simple combo using a simple filter. </summary>
    /// <param name="filterType"> The type of simple filter to use. </param>
    public SimpleFilterCombo(SimpleFilterType filterType)
        => Filter = filterType.ToFilter<T>();

    /// <summary> Get the string used for the selectable in the combo, and the preview. </summary>
    public abstract StringU8 DisplayString(in T value);

    /// <summary> Get the string used for filtering in the combo. </summary>
    public abstract string FilterString(in T value);

    /// <summary> The color to use for the displayed text.</summary>
    public virtual ColorParameter TextColor(in T value)
        => ColorParameter.Default;

    /// <summary> The tooltip to display when hovering the item. </summary>
    public virtual StringU8 Tooltip(in T value)
        => StringU8.Empty;

    /// <summary> Get the base items to transform and display. </summary>
    public abstract IEnumerable<T> GetBaseItems();

    /// <inheritdoc/>
    /// <remarks> Preprocesses the list returned by <see cref="GetBaseItems"/> into <see cref="SimpleCacheItem{T}"/> using the abstract methods. </remarks>
    protected internal override IEnumerable<SimpleCacheItem<T>> GetItems()
        => GetBaseItems().Select(i => new SimpleCacheItem<T>(i, DisplayString(i), FilterString(i), TextColor(i), Tooltip(i)));

    /// <inheritdoc/>
    /// <remarks> Standard item height for selectables with spacing. </remarks>
    protected internal override float ItemHeight
        => Im.Style.TextHeightWithSpacing;

    /// <summary> Draw a single item as a selectable. </summary>
    protected internal override bool DrawItem(in SimpleCacheItem<T> item, int globalIndex, bool selected)
    {
        using var color = Im.Color.Push(ImGuiColor.Text, item.TextColor);
        var       ret   = Im.Selectable(item.DisplayString, selected);
        Im.Tooltip.OnHover(item.Tooltip, true);
        return ret;
    }

    /// <summary> Draw the combo with a label. </summary>
    /// <param name="label"> The label of the combo. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="current"> The item currently selected, used for its display string and for initializing the current selection. </param>
    /// <param name="tooltip"> The tooltip shown when hovering the combo box. Omitted if this is empty. </param>
    /// <param name="previewWidth"> The width of the preview box for the combo. </param>
    /// <param name="result"> If this returns true, this is the newly selected item. </param>
    /// <returns> True if a new item is selected. </returns>
    public virtual bool Draw(Utf8LabelHandler label, in T current, Utf8TextHandler tooltip, float previewWidth,
        [NotNullWhen(true)] out T? result)
    {
        Current = current;
        var display = DisplayString(current);
        if (base.Draw(label, display, tooltip, previewWidth, out var ret))
        {
            result = ret.Item!;
            return true;
        }

        result = default;
        return false;
    }

    /// <summary> Draw the combo with a label. </summary>
    /// <param name="label"> The label of the combo. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="selection"> The item currently selected, which is updated if this returns true. </param>
    /// <param name="tooltip"> The tooltip shown when hovering the combo box. Omitted if this is empty. </param>
    /// <param name="previewWidth"> The width of the preview box for the combo. </param>
    /// <returns> True if a new item is selected. </returns>
    public virtual bool Draw(Utf8LabelHandler label, ref T selection, Utf8TextHandler tooltip, float previewWidth)
    {
        if (!Draw(label, selection, tooltip, previewWidth, out var ret))
            return false;

        selection = ret;
        return true;
    }

    /// <summary> Selection is based on the current item used. </summary>
    protected internal override bool IsSelected(SimpleCacheItem<T> item, int globalIndex)
        => item.Item!.Equals(Current);
}
