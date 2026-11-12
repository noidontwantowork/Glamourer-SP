namespace ImSharp;

/// <summary> A text filter that checks whether the items value contain each separated token of the filter. </summary>
/// <typeparam name="TCacheItem"> The type of item to check. </typeparam>
public abstract class PartwiseFilterBase<TCacheItem> : TextFilterBase<TCacheItem>
{
    /// <summary> The options to use when splitting the text. </summary>
    public StringSplitOptions SplitOptions { get; init; } = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    /// <summary> The character on which to split. </summary>
    public char Splitter { get; init; } = ' ';

    /// <summary> The split tokens of the input string. </summary>
    public string[] Parts { get; protected set; } = [];

    /// <summary> Update the filter and tokens. </summary>
    /// <inheritdoc/>
    protected override bool SetInternal(string text)
    {
        if (!base.SetInternal(text))
            return false;

        Parts = text.Split(Splitter, SplitOptions);
        return true;
    }

    /// <summary> Check if the given text matches each token of the filter. </summary>
    /// <inheritdoc/>
    public override bool WouldBeVisible(string text)
        => Parts.Length is 0 || Parts.All(p => text.Contains(p, Comparison));
}

/// <summary> A basic partwise filter that compares against items that already are of type string. </summary>
public sealed class PartwiseFilter : TextFilterBase<string>
{
    /// <summary> Return self. </summary>
    protected override string ToFilterString(in string item, int globalIndex)
        => item;
}

/// <summary> A <see cref="PartwiseFilterBase{TCacheItem}"/> for <see cref="SimpleCacheItem{T}"/> </summary>
/// <typeparam name="T"> The base type of the items. </typeparam>
public sealed class SimplePartwiseFilter<T> : PartwiseFilterBase<SimpleCacheItem<T>>
{
    /// <inheritdoc/>
    protected override string ToFilterString(in SimpleCacheItem<T> item, int globalIndex)
        => item.FilterString;
}
