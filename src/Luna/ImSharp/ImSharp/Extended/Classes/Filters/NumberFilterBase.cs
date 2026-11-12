namespace ImSharp;

/// <summary> The method used to filter numbers against the filter value. </summary>
public enum NumberFilterMethod : byte
{
    /// <summary> Only accept numbers with the same value. </summary>
    Equal = 0,

    /// <summary> Only accept numbers with less or equal value. </summary>
    LessEqual = 1,

    /// <summary> Only accept numbers with greater or equal value. </summary>
    GreaterEqual = 2,

    /// <summary> Do not compare numerical values, only text. </summary>
    TextOnly = 3,
};

public abstract class NumberFilterBase<TNumber, TCacheItem> : RegexFilterBase<TCacheItem>
    where TNumber : unmanaged, INumber<TNumber>
{
    /// <summary> The comparison method to use when the filter could be parsed as a number. </summary>
    public NumberFilterMethod Method { get; init; } = NumberFilterMethod.LessEqual;

    /// <summary> The parsed numerical value of the filter, if any. </summary>
    public TNumber? Number { get; protected set; } = null;

    /// <summary> Get the numerical value of the item to compare against, if the filter could be parsed into a number. </summary>
    /// <param name="item"> The item to get the value of. </param>
    /// <param name="globalIndex"> The index of the item. </param>
    /// <returns> The numerical value of the item. </returns>
    public abstract TNumber ToValue(in TCacheItem item, int globalIndex);

    /// <summary> Update the filter and try to parse it as a number. </summary>
    /// <param name="text"> The new input text. </param>
    /// <returns> True if the filter changed.</returns>
    protected override bool SetInternal(string text)
    {
        if (!base.SetInternal(text))
            return false;

        if (TNumber.TryParse(text, NumberStyles.Any, null, out var number))
        {
            Number = number;
            Regex  = null;
        }
        else
        {
            Number = null;
        }

        return true;
    }

    /// <summary> Use the given filter method on a numerical value. </summary>
    public bool WouldBeVisible(TNumber number)
        => Text.Length is 0
         || (Number.HasValue
                ? Method switch
                {
                    NumberFilterMethod.Equal        => number == Number.Value,
                    NumberFilterMethod.LessEqual    => number <= Number.Value,
                    NumberFilterMethod.GreaterEqual => number >= Number.Value,
                    _                               => true,
                }
                : WouldBeVisible(number.ToString()!));

    /// <summary> Use the given filter method on the numerical value or text. </summary>
    public override bool WouldBeVisible(in TCacheItem item, int globalIndex)
    {
        if (!Number.HasValue || Method is NumberFilterMethod.TextOnly)
            return base.WouldBeVisible(item, globalIndex);

        var value = ToValue(item, globalIndex);
        return Method switch
        {
            NumberFilterMethod.Equal        => value == Number.Value,
            NumberFilterMethod.LessEqual    => value <= Number.Value,
            NumberFilterMethod.GreaterEqual => value >= Number.Value,
            _                               => true,
        };
    }
}
