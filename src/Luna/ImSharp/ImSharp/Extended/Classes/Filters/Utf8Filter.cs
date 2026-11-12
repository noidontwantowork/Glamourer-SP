namespace ImSharp;

/// <summary> A text filter that checks whether the items value contains a given text encoded in UTF8. </summary>
/// <typeparam name="TCacheItem"> The type of item to check. </typeparam>
public abstract class Utf8FilterBase<TCacheItem> : IFilter<TCacheItem>
{
    /// <summary> The current filter value of this column as a string. </summary>
    public StringU8 Text { get; protected set; } = StringU8.Empty;

    /// <summary> Set the filter value when the text changes. </summary>
    /// <param name="text"> The new filter value. </param>
    /// <returns> True if the filter changed. </returns>
    public bool Set(ReadOnlySpan<byte> text)
    {
        if (!SetInternal(text))
            return false;

        InvokeEvent();
        return true;
    }

    /// <inheritdoc/>
    public virtual bool WouldBeVisible(in TCacheItem item, int globalIndex)
        => WouldBeVisible(ToFilterString(item, globalIndex));

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
    protected virtual bool SetInternal(ReadOnlySpan<byte> text)
    {
        if (text.SequenceEqual(Text))
            return false;

        Text = new StringU8(text, false);
        return true;
    }

    /// <summary> Usually clearing on middle-click, including the tooltip. </summary>
    /// <returns> True if the filter changed. </returns>
    protected virtual bool OnMiddleClick()
    {
        if (Text.Length > 0)
            Im.Tooltip.OnHover("中键点击清除筛选。\n"u8);
        if (!Im.Item.MiddleClicked())
            return false;

        Im.Id.ClearActive();
        return Clear();
    }

    /// <summary> Check if the text contains the filter text. </summary>
    /// <param name="text"> The given text. </param>
    /// <returns> True if the filter text is contained. </returns>
    protected virtual bool WouldBeVisible(ReadOnlySpan<byte> text)
        => Text.Length is 0 || text.ContainsCaseInsensitive(Text);

    /// <summary> Obtain the string to filter upon from the item to check. </summary>
    /// <param name="item"> The item to check. </param>
    /// <param name="globalIndex"> The global index of the item to check, if applicable. </param>
    /// <returns> The string that is compared against the filter. </returns>
    protected abstract ReadOnlySpan<byte> ToFilterString(in TCacheItem item, int globalIndex);

    /// <summary> Invoke the <see cref="FilterChanged"/> event. </summary>
    protected void InvokeEvent()
        => FilterChanged?.Invoke();

    /// <inheritdoc/>
    public bool Clear()
        => Set(StringU8.Empty);

    /// <inheritdoc/>
    public bool IsEmpty
        => Text.IsEmpty;
}
