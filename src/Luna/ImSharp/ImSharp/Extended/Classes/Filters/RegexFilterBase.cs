namespace ImSharp;

/// <summary> A regex filter that checks whether the items value match a given RegEx if the text is a valid RegEx, and whether it contains the text otherwise. </summary>
/// <typeparam name="TCacheItem"> The type of item to check. </typeparam>
public abstract class RegexFilterBase<TCacheItem> : TextFilterBase<TCacheItem>
{
    /// <summary> The options used for the RegEx compilation. </summary>
    public RegexOptions RegexOptions { get; init; } = RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase;

    /// <summary> If the filter can be parsed as a regular expression, this is set and compiled, and used for filtering. </summary>
    public Regex? Regex { get; protected set; }

    /// <summary> Update the filter and RegEx. </summary>
    /// <inheritdoc/>
    protected override bool SetInternal(string text)
    {
        if (!base.SetInternal(text))
            return false;

        Regex = GetRegex(text);
        return true;
    }

    /// <summary> Try to compile the given text to a valid regular expression. If that fails, return null instead of throwing. </summary>
    protected virtual Regex? GetRegex(string text)
    {
        if (text.Length is 0)
            return null;

        try
        {
            var regex = new Regex(text, RegexOptions);
            return regex;
        }
        catch
        {
            return null;
        }
    }

    /// <summary> Check if the given text matches the current RegEx, if there is one, or contains the current text otherwise. </summary>
    /// <inheritdoc/>
    public override bool WouldBeVisible(string text)
        => Text.Length is 0 || (Regex?.IsMatch(text) ?? text.Contains(Text, Comparison));
}

/// <summary> A basic regex filter that compares against items that already are of type string. </summary>
public sealed class RegexFilter : RegexFilterBase<string>
{
    /// <summary> Return self. </summary>
    protected override string ToFilterString(in string item, int globalIndex)
        => item;
}

/// <summary> A <see cref="RegexFilterBase{TCacheItem}"/> for <see cref="SimpleCacheItem{T}"/> </summary>
/// <typeparam name="T"> The base type of the items. </typeparam>
public sealed class SimpleRegexFilter<T> : RegexFilterBase<SimpleCacheItem<T>>
{
    /// <inheritdoc/>
    protected override string ToFilterString(in SimpleCacheItem<T> item, int globalIndex)
        => item.FilterString;
}
