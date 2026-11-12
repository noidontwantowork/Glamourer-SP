using static ImSharp.ImNodes.ImNodes;

namespace ImSharp.ImNodes;

/// <summary> The ID type for ImNodes nodes. </summary>
public readonly record struct NodeId(uint Id) : IAdditionOperators<NodeId, int, NodeId>, ISubtractionOperators<NodeId, int, NodeId>,
    IIncrementOperators<NodeId>, IDecrementOperators<NodeId>, ISpanFormattable, IUtf8SpanFormattable
{
    /// <summary> An invalid node ID. </summary>
    public static readonly AttributeId Invalid = new(ImGuiId.InvalidId);

    /// <summary> Whether the node ID is valid. </summary>
    public bool IsValid
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get { return Id is not uint.MaxValue; }
    }

    /// <summary> Get whether this node is currently hovered by the mouse cursor. </summary>
    /// <remarks> Use after disposing the <seealso cref="NodeEditor"/>. </remarks>
    public unsafe bool Hovered
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get
        {
            NodeId id;
            if (!Api.IsNodeHovered(&id))
                return false;

            return id == Id;
        }
    }

    /// <summary> Get or set the selection state of this node. </summary>
    /// <remarks> Selecting an already selected node, or unselecting an unselected node, is an error. </remarks>
    public bool Selected
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Api.IsNodeSelected(Id);
        [MethodImpl(ImSharpConfiguration.OptInl)]
        set
        {
            if (value)
                Api.SelectNode(Id);
            else
                Api.ClearNodeSelection(Id);
        }
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator NodeId(uint v)
        => new(v);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator NodeId(int v)
        => new((uint)v);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator NodeId(ImGuiId v)
        => new(v.Id);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ImGuiId(NodeId v)
        => new(v.Id);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static explicit operator uint(NodeId v)
        => v.Id;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static explicit operator int(NodeId v)
        => (int)v.Id;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static NodeId operator ++(NodeId id)
        => new(id.Id + 1);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static NodeId operator --(NodeId id)
        => new(id.Id - 1);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static NodeId operator +(NodeId id, int offset)
        => new((uint)(id.Id + offset));

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static NodeId operator -(NodeId id, int offset)
        => new((uint)(id.Id - offset));

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static NodeId operator +(int offset, NodeId id)
        => new((uint)(id.Id + offset));

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static NodeId operator -(int offset, NodeId id)
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
