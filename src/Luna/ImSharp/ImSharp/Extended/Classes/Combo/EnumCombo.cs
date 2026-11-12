namespace ImSharp;

/// <summary> A basic combo over all available enumeration values. </summary>
/// <typeparam name="T"></typeparam>
public class EnumCombo<T> : SimpleFilterCombo<T>
    where T : unmanaged, Enum
{
    /// <summary> The list of values to display in the combo. </summary>
    public readonly IReadOnlyList<T> Values;

    /// <summary> The function to get the display string for an item. </summary>
    public readonly Func<T, StringU8> DisplayFunction;

    /// <summary> The function to get the filter string for an item. </summary>
    public readonly Func<T, string> FilterFunction;

    /// <summary> The function to get the tooltip string for an item. </summary>
    public readonly Func<T, StringU8> TooltipFunction;

    /// <inheritdoc/>
    public override StringU8 DisplayString(in T value)
        => DisplayFunction(value);

    /// <inheritdoc/>
    public override string FilterString(in T value)
        => FilterFunction(value);

    /// <inheritdoc/>
    public override IEnumerable<T> GetBaseItems()
        => Values;

    /// <inheritdoc/>
    public override StringU8 Tooltip(in T value)
        => TooltipFunction.Invoke(value);

    /// <summary> Create the default combo using all available values. </summary>
    /// <param name="displayFunction"> The function to obtain a display string for a value. </param>
    /// <param name="filterFunction"> The function to obtain a filter string for a value. </param>
    /// <param name="tooltip"> The optional function to obtain a tooltip for a value. </param>
    public EnumCombo(Func<T, StringU8>? displayFunction = null, Func<T, string>? filterFunction = null, Func<T, StringU8>? tooltip = null)
        : this(displayFunction, filterFunction, tooltip, EnumExtensions.get_Values<T>())
    {}

    /// <summary> Create a combo using only the supplied values. </summary>
    /// <param name="values"> All values to list. </param>
    /// <param name="displayFunction"> The function to obtain a display string for a value. </param>
    /// <param name="filterFunction"> The function to obtain a filter string for a value. </param>
    /// <param name="tooltip"> The optional function to obtain a tooltip for a value. </param>
    public EnumCombo(Func<T, StringU8>? displayFunction = null, Func<T, string>? filterFunction = null, Func<T, StringU8>? tooltip = null, params IReadOnlyList<T> values)
        : base(SimpleFilterType.Text)
    {
        Values = values;
        DisplayFunction = displayFunction ?? DefaultDisplayString;
        FilterFunction = filterFunction ?? DefaultFilterString;
        TooltipFunction = tooltip ?? NoTooltip;
    }

    /// <summary> Create a combo using all named values of the enumeration type except for the supplied values. </summary>
    /// <param name="displayFunction"> The function to obtain a display string for a value. </param>
    /// <param name="filterFunction"> The function to obtain a filter string for a value. </param>
    /// <param name="tooltip"> The optional function to obtain a tooltip for a value. </param>
    /// <param name="exclusions"> The excluded values. </param>
    /// <returns> The combo. </returns>
    public static EnumCombo<T> Excluding(Func<T, StringU8>? displayFunction = null, Func<T, string>? filterFunction = null, Func<T, StringU8>? tooltip = null, params HashSet<T> exclusions)
        => new(displayFunction, filterFunction, tooltip, EnumExtensions.get_Values<T>().Where(v => !exclusions.Contains(v)).ToArray());

    /// <summary> Default tooltip function returning an empty string. </summary>
    private static StringU8 NoTooltip(T value)
        => StringU8.Empty;

    /// <summary> Default tooltip function returning an empty string. </summary>
    private static StringU8 DefaultDisplayString(T value)
        => new($"{value}");

    /// <summary> Default tooltip function returning an empty string. </summary>
    private static string DefaultFilterString(T value)
        => $"{value}";
}
