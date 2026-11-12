namespace ImSharp;

/// <summary> A simple cache item wrapper that has everything a basic combo needs to display and filter its items. </summary>
/// <typeparam name="T"> The base type of the item. </typeparam>
/// <param name="Item"> The item value itself. </param>
/// <param name="DisplayString"> The string to display in the selectable. </param>
/// <param name="FilterString"> The string used for filtering the data. </param>
/// <param name="TextColor"> The color to display the text in. </param>
/// <param name="Tooltip"> The tooltip displayed when hovering the selectable. </param>
public record SimpleCacheItem<T>(T Item, StringU8 DisplayString, string FilterString, Vector4 TextColor, StringU8 Tooltip)
{
    /// <summary> Create a cache item wrapper from the given data. </summary>
    /// <param name="item"> The item value itself.  </param>
    /// <param name="displayString"> The string to display in the selectable. </param>
    /// <param name="filterString"> The string used for filtering the data. </param>
    /// <param name="textColor"> The color to display the text in. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
    /// <param name="tooltip"> The tooltip displayed when hovering the selectable. </param>
    public SimpleCacheItem(T item, StringU8 displayString, string filterString, ColorParameter textColor, StringU8 tooltip)
        : this(item, displayString, filterString, textColor.CheckDefault(ImGuiColor.Text).ToVector(), tooltip)
    { }

    /// <inheritdoc cref="SimpleCacheItem{T}"/>
    public SimpleCacheItem(T item, StringU8 displayString, string filterString, ColorParameter textColor = default)
        : this(item, displayString, filterString, textColor, StringU8.Empty)
    { }
}
