using static ImSharp.ImNodes.ImNodes;

namespace ImSharp.ImNodes;

/// <summary> The ID type for ImNodes links. </summary>
public readonly record struct LinkId(uint Id) : IAdditionOperators<LinkId, int, LinkId>, ISubtractionOperators<LinkId, int, LinkId>,
    IIncrementOperators<LinkId>, IDecrementOperators<LinkId>, ISpanFormattable, IUtf8SpanFormattable
{
    /// <summary> An invalid link ID. </summary>
    public static readonly LinkId Invalid = new(ImGuiId.InvalidId);

    /// <summary> Whether the link ID is valid. </summary>
    public bool IsValid
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get { return Id is not uint.MaxValue; }
    }

    /// <summary> Get whether this link is currently hovered by the mouse cursor. </summary>
    /// <remarks> Use after disposing the <seealso cref="ImSharp.ImNodes.NodeEditor"/>. </remarks>
    public unsafe bool Hovered
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get
        {
            LinkId id;
            if (!Api.IsLinkHovered(&id))
                return false;

            return id == Id;
        }
    }

    /// <summary> Get or set the selection state of this link. </summary>
    /// <remarks> Selecting an already selected link, or unselecting an unselected link, is an error. </remarks>
    public bool Selected
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Api.IsLinkSelected(Id);
        [MethodImpl(ImSharpConfiguration.OptInl)]
        set
        {
            if (value)
                Api.SelectLink(Id);
            else
                Api.ClearLinkSelection(Id);
        }
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator LinkId(uint v)
        => new(v);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator LinkId(int v)
        => new((uint)v);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator LinkId(ImGuiId v)
        => new(v.Id);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ImGuiId(LinkId v)
        => new(v.Id);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static explicit operator uint(LinkId v)
        => v.Id;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static explicit operator int(LinkId v)
        => (int)v.Id;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static LinkId operator ++(LinkId id)
        => new(id.Id + 1);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static LinkId operator --(LinkId id)
        => new(id.Id - 1);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static LinkId operator +(LinkId id, int offset)
        => new((uint)(id.Id + offset));

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static LinkId operator -(LinkId id, int offset)
        => new((uint)(id.Id - offset));

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static LinkId operator +(int offset, LinkId id)
        => new((uint)(id.Id + offset));

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static LinkId operator -(int offset, LinkId id)
        => new((uint)(id.Id - offset));

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public override string ToString()
        => Id.ToString();

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public string ToString(string? format, IFormatProvider? formatProvider)
        => Id.ToString(format, formatProvider);

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool TryFormat(Span<char> destination, out int charsWritten,
        [StringSyntax(StringSyntaxAttribute.NumericFormat)]
        ReadOnlySpan<char> format, IFormatProvider? provider)
        => Id.TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public bool TryFormat(Span<byte> destination, out int bytesWritten,
        [StringSyntax(StringSyntaxAttribute.NumericFormat)]
        ReadOnlySpan<char> format, IFormatProvider? provider)
        => Id.TryFormat(destination, out bytesWritten, format, provider);
}
