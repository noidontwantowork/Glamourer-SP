namespace ImSharp;

/// <summary> An interface any filter. </summary>
public interface IFilter
{
    /// <summary> An event fired whenever this filter changes in any way. </summary>
    public event Action FilterChanged;

    /// <summary> Draw a basic input for this filter. </summary>
    /// <param name="label"> The label for the input. Can be used arbitrarily. Should be null-terminated. </param>
    /// <param name="availableRegion"> The currently available region to draw the filter in. </param>
    /// <returns> True if the filter changed (in which case <see cref="FilterChanged"/> is also invoked). </returns>
    public bool DrawFilter(ReadOnlySpan<byte> label, Vector2 availableRegion);

    /// <summary> Whether the filter draws a visible widget or not. </summary>
    public bool IsVisible
        => true;

    /// <summary> Clear the filter. </summary>
    /// <returns> True if the filter changed. </returns>
    /// <remarks> <see cref="IsEmpty"/> should be true after calling this. </remarks>
    public bool Clear();

    /// <summary> Whether the filter is currently empty, in which case nothing would be filtered out and everything is visible. </summary>
    public bool IsEmpty { get; }
}

/// <summary> An interface representing a filter for arbitrary items. </summary>
/// <typeparam name="TCacheItem"> The type of the item. </typeparam>
public interface IFilter<TCacheItem> : IFilter
{
    /// <summary> Whether an item should be visible or filtered out according to this filter. </summary>
    /// <param name="item"> The item to check. </param>
    /// <param name="globalIndex"> The index of the item to check, if applicable. </param>
    /// <returns> True if the item should be visible, false if it should be filtered out. </returns>
    public bool WouldBeVisible(in TCacheItem item, int globalIndex);
}
