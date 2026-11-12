namespace ImSharp;

/// <summary> A tokenized text filter that splits its input string into tokens with different attributes and search behavior. </summary>
/// <typeparam name="TTokenType"> The enumeration type that defines the different token types. </typeparam>
/// <typeparam name="TCacheItem"> The type of item to check. </typeparam>
/// <typeparam name="TToken"> The token type. </typeparam>
/// <remarks>
///   The filter splits on whitespace unless delimited by double quotes. <br/>
///   Tokens can be prepended with a single character to denote the type of token. <br/>
///   All tokens have to be fulfilled to match the search string. <br/>
///   Prepend a '-' to a token to invert the search space. <br/>
///   Prepend a '?' to a token to make the token optional. If there are optional tokens, at least one of them has to match. <br/>
///   Some tokens may support a 'None' keyword to check for the absence of a certain attribute.
/// </remarks>
public abstract class TokenizedFilter<TTokenType, TCacheItem, TToken> : IFilter<TCacheItem>
    where TTokenType : unmanaged, Enum
    where TToken : IFilterToken<TTokenType, TToken>, new()
{
    /// <summary> The current state of the filter. </summary>
    protected enum FilterState
    {
        /// <summary> No filters are set, everything is visible. </summary>
        NoFilters = 0,

        /// <summary> Normal filtering is applied. </summary>
        Normal = 1,

        /// <summary> We have mutually exclusive filters that can not produce matches, nothing is visible. </summary>
        NoMatches = 2,
    }

    /// <inheritdoc/>
    public event Action? FilterChanged;

    /// <summary> Draw a non-rounded text input using the available width and the given label as a hint. </summary>
    /// <inheritdoc/>
    public virtual bool DrawFilter(ReadOnlySpan<byte> label, Vector2 availableRegion)
    {
        using var style = ImStyleSingle.FrameRounding.Push(0);

        Im.Item.SetNextWidth(availableRegion.X);
        var tmp = Text;
        var ret = Im.Input.Text("##Filter"u8, ref tmp, label) && Set(tmp);
        DrawTooltip();
        ret |= OnMiddleClick();
        return ret;
    }

    /// <summary> Invoke the <see cref="FilterChanged"/> event. </summary>
    protected void InvokeEvent()
        => FilterChanged?.Invoke();

    /// <summary> Set the filter to any value and parse it. </summary>
    /// <param name="text"> The new filter value. </param>
    /// <returns> True if the filter changed and <see cref="FilterChanged"/> is invoked. </returns>
    public bool Set(string text)
    {
        if (!SetInternal(text))
            return false;

        InvokeEvent();
        return true;
    }

    /// <summary> Set the filter to any value and parse it. </summary>
    /// <param name="text"> The new filter value. </param>
    /// <returns> True if the filter changed. </returns>
    /// <remarks> Does not invoke <see cref="FilterChanged"/> itself. </remarks>
    protected virtual bool SetInternal(string text)
    {
        if (text == Text)
            return false;

        Text = text;
        Parse(Text);
        return true;
    }

    /// <inheritdoc/>
    public virtual bool Clear()
        => Set(string.Empty);


    /// <summary> The current filter state. </summary>
    protected FilterState State = FilterState.NoFilters;

    /// <summary> The current raw text within the filter. </summary>
    public string Text { get; private set; } = string.Empty;

    /// <summary> The list of general tokens, at least one of which has to match. </summary>
    protected readonly List<TToken> General = [];

    /// <summary> The list of forced tokens, all of which have to match. </summary>
    protected readonly List<TToken> Forced = [];

    /// <summary> The list of negated tokens, none of which may match. </summary>
    protected readonly List<TToken> Negated = [];

    /// <summary> The list of 'None' tokens together with whether they are negated, all of which have to match. </summary>
    protected readonly List<(TTokenType, bool)> None = [];

    /// <summary> Check whether the given item is in the search space of the given token. </summary>
    /// <param name="token"> The token to check the search space of. </param>
    /// <param name="cacheItem"> The item to check. </param>
    /// <returns> True if the item is in the search space and should not be filtered out. </returns>
    protected abstract bool Matches(in TToken token, in TCacheItem cacheItem);

    /// <summary> Check whether the given item matches the 'None' condition of the given type. </summary>
    /// <param name="type"> The type of the token to check. </param>
    /// <param name="negated"> Whether the 'None' condition is negated, i.e. it has to have any arbitrary value set for this attribute. </param>
    /// <param name="cacheItem"> The item to check. </param>
    /// <returns> True if the item has nothing set for the attribute of <paramref name="type"/> when <paramref name="negated"/> is false, or if it has anything set when <paramref name="negated"/> is true. </returns>
    protected abstract bool MatchesNone(TTokenType type, bool negated, in TCacheItem cacheItem);

    /// <summary> Draw a tooltip for the filter input. </summary>
    protected virtual void DrawTooltip()
    { }

    /// <inheritdoc/>
    public virtual bool WouldBeVisible(in TCacheItem cacheItem, int globalIndex)
    {
        switch (State)
        {
            case FilterState.NoFilters: return true;
            case FilterState.NoMatches: return false;
        }

        // Avoid lambdas due to teh in parameter.
        // All None filters have to match.
        foreach (var noneToken in None)
        {
            if (!MatchesNone(noneToken.Item1, noneToken.Item2, cacheItem))
                return false;
        }

        // All forced entries have to exist.
        foreach (var forcedToken in Forced)
        {
            if (!Matches(forcedToken, cacheItem))
                return false;
        }

        // No negated entry may exist.
        foreach (var negatedToken in Negated)
        {
            if (Matches(negatedToken, cacheItem))
                return false;
        }

        // At least one of the general entries has to exist.
        if (General.Count is 0)
            return true;

        foreach (var generalToken in General)
        {
            if (Matches(generalToken, cacheItem))
                return true;
        }

        return false;
    }

    /// <summary> Usually clearing on middle-click, including the tooltip. </summary>
    /// <returns> True if the filter changed. </returns>
    protected virtual bool OnMiddleClick()
    {
        if (Text.Length > 0)
            Im.Tooltip.OnHover("\n中键点击清除筛选。"u8);
        if (!Im.Item.MiddleClicked())
            return false;

        Im.Id.ClearActive();
        return Clear();
    }

    /// <summary> Parse the given input string into specialized tokens. </summary>
    /// <param name="input"> The input string. </param>
    public void Parse(ReadOnlySpan<char> input)
    {
        General.Clear();
        Forced.Clear();
        Negated.Clear();
        None.Clear();
        if (input.Length is 0)
        {
            State = FilterState.NoFilters;
            return;
        }

        var list          = Forced;
        var currentOffset = 0;
        var start         = 0;
        var count         = -1;
        var typeToken     = default(TTokenType);
        while (currentOffset < input.Length)
        {
            switch (input[currentOffset])
            {
                case ' ' or '\t' or '\n': ++currentOffset; break;
                case '?' when ReferenceEquals(list, Forced)
                 && typeToken.Equals(default(TTokenType))
                 && currentOffset + 1 < input.Length
                 && !char.IsWhiteSpace(input[currentOffset + 1]):
                    list = General;
                    ++currentOffset;
                    break;
                case '-' when ReferenceEquals(list, Forced)
                 && typeToken.Equals(default(TTokenType))
                 && currentOffset + 1 < input.Length
                 && !char.IsWhiteSpace(input[currentOffset + 1]):
                    list = Negated;
                    ++currentOffset;
                    break;
                case '"' when currentOffset + 1 < input.Length:
                    count = input[(currentOffset + 1)..].IndexOf('"');
                    if (count is -1)
                    {
                        start = currentOffset;
                        count = input[start..].IndexOfAny(' ', '\t', '\n');
                        if (count is -1)
                            count = input.Length - start;
                    }
                    else
                    {
                        start         =  currentOffset + 1;
                        currentOffset += 2;
                    }

                    break;
                default:
                    if (typeToken.Equals(default(TTokenType))
                     && currentOffset + 1 < input.Length
                     && input[currentOffset + 1] is ':'
                     && TToken.ConvertToken(input[currentOffset], out var tok))
                    {
                        typeToken     =  tok;
                        currentOffset += 2;
                    }
                    else
                    {
                        start = currentOffset;
                        count = input[start..].IndexOfAny(' ', '\t', '\n');
                        if (count is -1)
                            count = input.Length - start;
                    }

                    break;
            }

            if (count <= 0)
                continue;

            var spanText = input.Slice(start, count);
            {
                if (TToken.AllowsNone(typeToken) && spanText.Equals("none", StringComparison.OrdinalIgnoreCase))
                    None.Add((typeToken, ReferenceEquals(list, Negated)));
                else
                    list.Add(new TToken
                    {
                        Needle = spanText.ToString().ToLowerInvariant(),
                        Type   = typeToken,
                    });
            }

            currentOffset += count;
            start         =  0;
            count         =  -1;
            list          =  Forced;
            typeToken     =  default;
        }

        PostProcessing();
    }

    /// <summary> Process all tokens for any inconsistencies or simplifications. </summary>
    protected virtual void PostProcessing()
    {
        // Remove all optional tags that are negated.
        foreach (var item in Negated)
            General.RemoveAll(i => i.Equals(item));

        // Remove all optional tags that are restricted by None.
        foreach (var none in None.Where(none => !none.Item2))
            General.RemoveAll(i => i.Type.Equals(none.Item1));

        foreach (var item in Forced)
        {
            // Remove all optional tags that are forced anyway.
            General.RemoveAll(i => i.Equals(item));

            // If any negated tag is contained in a forced match, we can not have matches.
            if (Negated.Any(i => item.Contains(i)))
            {
                State = FilterState.NoMatches;
                return;
            }

            // If any tag is forced that also has to be not set, we can not have matches.
            if (None.Any(i => !i.Item2 && i.Item1.Equals(item.Type)))
            {
                State = FilterState.NoMatches;
                return;
            }
        }

        // If only a single general tag remains, it is forced.
        if (General.Count is 1)
        {
            Forced.Add(General[0]);
            General.Clear();
        }

        if (TToken.ProcessList(Forced,  TokenModifier.Forced)
         || TToken.ProcessList(General, TokenModifier.General)
         || TToken.ProcessList(Negated, TokenModifier.Negated))
        {
            State = FilterState.NoMatches;
            return;
        }

        // Check if we have any filters.
        State = General.Count is 0 && Forced.Count is 0 && Negated.Count is 0 && None.Count is 0
            ? FilterState.NoFilters
            : FilterState.Normal;
    }

    /// <inheritdoc/>
    public virtual bool IsEmpty
        => Text.Length is 0;
}
