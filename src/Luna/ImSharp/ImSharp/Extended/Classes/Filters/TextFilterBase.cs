namespace ImSharp;

/// <summary> A text filter that checks whether the items value contains a given text. </summary>
/// <typeparam name="TCacheItem"> The type of item to check. </typeparam>
public abstract class TextFilterBase<TCacheItem> : IFilter<TCacheItem>
{
    /// <summary> The comparison type to use to check for containment. </summary>
    public StringComparison Comparison { get; init; } = StringComparison.OrdinalIgnoreCase;

    /// <summary> The current filter value of this column as a string. </summary>
    public string Text { get; protected set; } = string.Empty;

    /// <summary> Set the filter value when the text changes. </summary>
    /// <param name="text"> The new filter value. </param>
    /// <returns> True if the filter changed. </returns>
    public bool Set(string text)
    {
        if (!SetInternal(text))
            return false;

        InvokeEvent();
        return true;
    }

    /// <inheritdoc/>
    public virtual bool WouldBeVisible(in TCacheItem item, int globalIndex)
        => IsEmpty || WouldBeVisible(ToFilterString(item, globalIndex));

    /// <inheritdoc/>
    public event Action? FilterChanged;

    /// <summary> Draw a non-rounded text input using the available width and the given label as a hint. </summary>
    /// <inheritdoc/>
    public virtual bool DrawFilter(ReadOnlySpan<byte> label, Vector2 availableRegion)
    {
        using var id    = Im.Id.Push(label);
        using var style = ImStyleSingle.FrameRounding.Push(0);
        Im.Item.SetNextWidth(availableRegion.X);
        var tmp = Text;
        var ret = Im.Input.Text("##Filter"u8, ref tmp, label) && Set(tmp);
        ret |= OnMiddleClick();
        return ret;
    }


    /// <summary> Set the filter value when the text changes. </summary>
    /// <param name="text"> The new filter value. </param>
    /// <returns> True if the filter changed. </returns>
    /// <remarks> Does not trigger <see cref="FilterChanged"/> itself. </remarks>
    protected virtual bool SetInternal(string text)
    {
        if (Text == text)
            return false;

        Text = text;
        return true;
    }

    /// <summary> Check if the text contains the filter text. </summary>
    /// <param name="text"> The given text. </param>
    /// <returns> True if the filter text is contained. </returns>
    public virtual bool WouldBeVisible(string text)
        => Text.Length is 0 || text.Contains(Text, Comparison);

    /// <summary> Obtain the string to filter upon from the item to check. </summary>
    /// <param name="item"> The item to check. </param>
    /// <param name="globalIndex"> The global index of the item to check, if applicable. </param>
    /// <returns> The string that is compared against the filter. </returns>
    protected abstract string ToFilterString(in TCacheItem item, int globalIndex);

    /// <summary> Invoke the <see cref="FilterChanged"/> event. </summary>
    protected void InvokeEvent()
        => FilterChanged?.Invoke();

    /// <inheritdoc/>
    public virtual bool Clear()
        => Set(string.Empty);

    /// <summary> Usually clearing on middle-click, including the tooltip. </summary>
    /// <returns> True if the filter changed. </returns>
    protected virtual bool OnMiddleClick()
    {
        if (Text.Length > 0)
            Im.Tooltip.OnHover("中键点击清除筛选。\n"u8, true);
        if (!Im.Item.MiddleClicked())
            return false;

        Im.Id.ClearActive();
        return Clear();
    }

    /// <inheritdoc/>
    public bool IsEmpty
        => Text.Length is 0;
}

/// <summary> A basic text filter that compares against items that already are of type string. </summary>
public sealed class TextFilter : TextFilterBase<string>
{
    /// <summary> Return self. </summary>
    protected override string ToFilterString(in string item, int globalIndex)
        => item;
}

/// <summary> A <see cref="TextFilterBase{TCacheItem}"/> for <see cref="SimpleCacheItem{T}"/> </summary>
/// <typeparam name="T"> The base type of the items. </typeparam>
public sealed class SimpleTextFilter<T> : TextFilterBase<SimpleCacheItem<T>>
{
    /// <inheritdoc/>
    protected override string ToFilterString(in SimpleCacheItem<T> item, int globalIndex)
        => item.FilterString;
}
