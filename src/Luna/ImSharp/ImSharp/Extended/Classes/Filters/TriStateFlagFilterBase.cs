namespace ImSharp;

/// <summary> Basic filter for on-off pairs of flag based checks. </summary>
/// <typeparam name="TCacheItem"> The type of item to filter. </typeparam>
/// <typeparam name="TEnum"> The type of the flags to check against. </typeparam>
public abstract class TriStateFlagFilterBase<TCacheItem, TEnum> : IFilter<TCacheItem>
    where TEnum : unmanaged, Enum
{
    /// <summary> Pairs of flags that represent the on and off state for a specific value, and the name to display next to their tri-state checkbox. </summary>
    public abstract IReadOnlyList<(TEnum On, TEnum Off, StringU8 Name)> EnumData { get; }

    /// <summary> Get the true or false value for the filter flag pair at the given index from the item. </summary>
    public abstract bool GetValue(in TCacheItem item, int globalIndex, int triEnumIndex);

    /// <summary> The flags used to draw the combo of checkboxes. </summary>
    public ComboFlags ComboFlags { get; init; } = ComboFlags.NoArrowButton;

    /// <summary> The value used when no filter is enabled. </summary>
    public TEnum AllFlags { get; init; }

    /// <summary> Get the current filter value. </summary>
    public abstract TEnum FilterValue { get; protected set; }

    /// <inheritdoc/>
    public event Action? FilterChanged;

    /// <summary> Load a given value as the filter. </summary>
    /// <param name="value"> The new value for the filter. </param>
    public virtual void LoadValue(TEnum value)
    {
        var oldValue = FilterValue;
        SetValue(AllFlags, false);
        SetValue(value,    true);
        if (!FilterValue.Equals(oldValue))
            InvokeEvent();
    }

    /// <summary> Set a value according to the optional bool. </summary>
    /// <param name="onValue"> The flag representing the on state. </param>
    /// <param name="offValue"> The flag representing the off state. </param>
    /// <param name="enable">
    ///   The value to use for setting.
    ///   If true, <paramref name="onValue"/> should be set and <paramref name="offValue"/> should be unset, conversely for false.
    ///   If null, both should be set. </param>
    /// <returns> True if the filter value has changed. </returns>
    protected virtual bool SetValue(TEnum onValue, TEnum offValue, bool? enable)
        => enable switch
        {
            null => SetValue(onValue, true) | SetValue(offValue,  true),
            true => SetValue(onValue, true) | SetValue(offValue,  false),
            _    => SetValue(onValue, false) | SetValue(offValue, true),
        };

    /// <summary> Draw a filter that expands a combo of multiple tri-state checkboxes on click. </summary>
    /// <inheritdoc/>
    public virtual bool DrawFilter(ReadOnlySpan<byte> label, Vector2 availableRegion)
    {
        using var id    = Im.Id.Push("##Filter"u8);
        using var combo = BeginCombo(label, availableRegion, out var changes);
        if (!combo)
            return changes;

        for (var i = 0; i < EnumData.Count; ++i)
            changes |= DrawCheckbox(i);

        if (changes)
            InvokeEvent();

        return changes;
    }

    /// <summary> Draw a tri-state checkbox for the given pair of flags. </summary>
    protected virtual bool DrawCheckbox(int idx)
    {
        var (on, off, name) = EnumData[idx];
        bool? current = FilterValue.HasFlag(on)
            ? FilterValue.HasFlag(off)
                ? null
                : true
            : false;

        return TriStateCheckbox.Instance.Draw(name, current, out var tmp) && SetValue(on, off, tmp);
    }

    /// <inheritdoc/>
    public virtual bool WouldBeVisible(in TCacheItem item, int globalIndex)
    {
        foreach (var (index, (on, off, _)) in EnumData.Index())
        {
            var boolValue = GetValue(item, globalIndex, index);
            if (boolValue)
            {
                if (!FilterValue.HasFlag(on))
                    return false;
            }
            else if (!FilterValue.HasFlag(off))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary> Invoke the <see cref="FilterChanged"/> event. </summary>
    protected void InvokeEvent()
        => FilterChanged?.Invoke();

    /// <summary> Begin a combo using the available width, also handling the right-click to reset filters. </summary>
    /// <param name="label"> The text used as the preview text for the combo. </param>
    /// <param name="availableRegion"> The available content region, of which the width is used. </param>
    /// <param name="changes"> Whether any changes to the filter were applied. </param>
    /// <returns> The combo disposable. </returns>
    [MethodImpl(ImSharpConfiguration.Inl)]
    protected Im.ComboDisposable BeginCombo(ReadOnlySpan<byte> label, Vector2 availableRegion, out bool changes)
    {
        var       all   = FilterValue.HasFlag(AllFlags);
        using var style = ImStyleSingle.FrameRounding.Push(0);
        Im.Item.SetNextWidth(availableRegion.X);
        var color = ImGuiColor.FrameBackground.Push(ImEx.Table.ActiveFilterColor, !all);
        var combo = Im.Combo.Begin(""u8, label, ComboFlags);
        color.Dispose();
        changes = OnMiddleClick();
        return combo;
    }

    /// <summary> Usually clearing on middle-click, including the tooltip. </summary>
    /// <returns> True if the filter changed. </returns>
    protected virtual bool OnMiddleClick()
    {
        if (FilterValue.HasFlag(AllFlags))
            Im.Tooltip.OnHover("中键点击清除筛选。\n"u8);
        if (!Im.Item.MiddleClicked())
            return false;

        return Clear();
    }

    /// <summary> Set the flags in <paramref name="flags"/> to <paramref name="value"/>. </summary>
    /// <param name="flags"> The flags to toggle. </param>
    /// <param name="value"> Whether to turn the flags on or off. </param>
    /// <returns> True if the filter value changed. </returns>
    /// <exception cref="InvalidOperationException"> Only thrown for non-standard enum types. </exception>
    protected virtual bool SetValue(TEnum flags, bool value)
    {
        var newValue = value
            ? FilterValue.Or(flags)
            : FilterValue.AndNot(flags);
        if (newValue.Equals(FilterValue))
            return false;

        FilterValue = newValue;
        return true;
    }

    /// <inheritdoc/>
    public bool Clear()
    {
        if (!SetValue(AllFlags, true))
            return false;

        InvokeEvent();
        return true;
    }

    /// <inheritdoc/>
    public bool IsEmpty
        => FilterValue.Equals(AllFlags);
}
