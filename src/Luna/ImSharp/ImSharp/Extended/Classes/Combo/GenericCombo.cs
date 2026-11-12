namespace ImSharp;

/// <summary> The delegate to convert an arbitrary value into an UTF8 display string. </summary>
/// <typeparam name="T"> The type of the value. </typeparam>
/// <param name="value"> The value. </param>
/// <returns> The display string. </returns>
public delegate StringU8 DisplayDelegate<T>(in T value);

/// <summary> The delegate to convert an arbitrary value into an UTF16 filter string. </summary>
/// <typeparam name="T"> The type of the value. </typeparam>
/// <param name="value"> The value. </param>
/// <returns> The filter string. </returns>
public delegate string FilterDelegate<T>(in T value);

/// <summary> The delegate to convert an arbitrary value into an UTF8 tooltip string. </summary>
/// <typeparam name="T"> The type of the value. </typeparam>
/// <param name="value"> The value. </param>
/// <returns> The tooltip. </returns>
public delegate StringU8 TooltipDelegate<T>(in T value);

/// <summary> A combo over a set of named values with custom display, filter, and tooltip functions. </summary>
/// <param name="displayFunction"> The function to obtain a display string for a value. </param>
/// <param name="filterFunction"> The function to obtain a filter string for a value. </param>
/// <param name="tooltip"> The optional function to obtain a tooltip for a value. </param>
public class GenericCombo<T>(
    IReadOnlyList<T> values,
    DisplayDelegate<T> displayFunction,
    FilterDelegate<T> filterFunction,
    TooltipDelegate<T>? tooltip)
    : SimpleFilterCombo<T>(SimpleFilterType.Text)
{
    /// <summary> The list of values to display in the combo. </summary>
    public readonly IReadOnlyList<T> Values = values;

    /// <summary> The function to get the display string for an item. </summary>
    public readonly DisplayDelegate<T> DisplayFunction = displayFunction;

    /// <summary> The function to get the filter string for an item. </summary>
    public readonly FilterDelegate<T> FilterFunction = filterFunction;

    /// <summary> The function to get the tooltip string for an item. </summary>
    public readonly TooltipDelegate<T> TooltipFunction = tooltip ?? NoTooltip;

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

    /// <summary> Default tooltip function returning an empty string. </summary>
    private static StringU8 NoTooltip(in T value)
        => StringU8.Empty;
}
