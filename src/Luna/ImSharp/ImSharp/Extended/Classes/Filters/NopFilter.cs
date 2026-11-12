namespace ImSharp;

/// <summary> A filter doing nothing. </summary>
/// <typeparam name="TCacheItem"> The item to not filter. </typeparam>
public sealed class NopFilter<TCacheItem> : IFilter<TCacheItem>
{
    /// <summary> A static instance of this object type. </summary>
    public static readonly NopFilter<TCacheItem> Instance = new();

    /// <summary> Draw the given label as text. </summary>
    /// <inheritdoc/>
    public bool DrawFilter(ReadOnlySpan<byte> label, Vector2 availableRegion)
    {
        ImEx.TextFrameAligned(label);
        return false;
    }

    /// <summary> Return true. All items are visible. </summary>
    /// <inheritdoc/>
    public bool WouldBeVisible(in TCacheItem item, int globalIndex)
        => true;

    /// <summary> Never triggered. </summary>
    public event Action FilterChanged
    {
        add { }
        remove { }
    }

    /// <inheritdoc/>
    public bool IsVisible
        => false;

    /// <inheritdoc/>
    public bool Clear()
        => false;

    /// <summary> Use <see cref="Instance"/> instead. </summary>
    private NopFilter()
    { }

    /// <inheritdoc/>
    public bool IsEmpty
        => true;
}
