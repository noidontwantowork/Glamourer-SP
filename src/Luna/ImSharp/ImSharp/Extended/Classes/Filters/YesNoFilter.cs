namespace ImSharp;

/// <summary> A flag indicating yes or no for wrapping <see cref="TriStateFlagFilterBase{TEnum,TCacheItem}"/>. </summary>
[Flags]
public enum YesNoFlag
{
    /// <summary> Filter for positive values. </summary>
    Yes = 0x01,

    /// <summary> Filter for negative values. </summary>
    No = 0x02,
};

/// <summary> Extensions for <see cref="YesNoFlag"/>. </summary>
public static class YesNoFlagExtensions
{
    /// <summary> Do not filter. </summary>
    public const YesNoFlag Either = YesNoFlag.Yes | YesNoFlag.No;

    extension(YesNoFlag)
    {
        /// <inheritdoc cref="YesNoFlagExtensions.Either"/>
        public static YesNoFlag Either
            => YesNoFlagExtensions.Either;
    }
}

/// <summary> A filter for a single state that can be enabled or disabled, or on or off. </summary>
/// <typeparam name="TCacheItem"> The type of item to check. </typeparam>
public abstract class YesNoFilter<TCacheItem> : TriStateFlagFilterBase<TCacheItem, YesNoFlag>
{
    /// <summary> The default label for the filter. </summary>
    public static readonly StringU8 EnabledString = new("启用"u8);

    /// <summary> The basic data for the filter. </summary>
    private readonly (YesNoFlag On, YesNoFlag Off, StringU8 Name)[] _enumData = [(YesNoFlag.Yes, YesNoFlag.No, EnabledString)];

    /// <summary> The value as a field for ref-access. </summary>
    protected YesNoFlag Value = YesNoFlag.Either;

    /// <inheritdoc/>
    public override YesNoFlag FilterValue
    {
        get => Value;
        protected set => Value = value;
    }

    public StringU8 FilterLabel
    {
        get => _enumData[0].Name;
        set => _enumData[0].Name = value;
    }

    /// <summary> Draw the filter as a single checkbox instead of a combo. </summary>
    /// <returns> True if the filter changed this frame. </returns>
    public virtual bool DrawCheckboxFilter()
    {
        using var id  = Im.Id.Push("##Filter"u8);
        var       ret = ImEx.TriStateCheckbox(FilterLabel, ref Value, YesNoFlag.Yes, YesNoFlag.No);
        if (ret)
            InvokeEvent();
        return OnMiddleClick() | ret;
    }

    /// <inheritdoc/>
    public override IReadOnlyList<(YesNoFlag On, YesNoFlag Off, StringU8 Name)> EnumData
        => _enumData;

    /// <inheritdoc/>
    protected override bool SetValue(YesNoFlag onValue, YesNoFlag offValue, bool? enable)
    {
        var newFilter = enable switch
        {
            null  => AllFlags,
            true  => YesNoFlag.Yes,
            false => YesNoFlag.No,
        };
        if (FilterValue == newFilter)
            return false;

        FilterValue = newFilter;
        return true;
    }
}
