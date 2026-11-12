using static ImSharp.ImNodes.ImNodes;

namespace ImSharp.ImNodes;

/// <summary> The ID type for ImNodes attributes. </summary>
public readonly record struct AttributeId(uint Id) : IAdditionOperators<AttributeId, int, AttributeId>,
    ISubtractionOperators<AttributeId, int, AttributeId>, IIncrementOperators<AttributeId>, IDecrementOperators<AttributeId>,
    ISpanFormattable, IUtf8SpanFormattable
{
    /// <summary> An invalid attribute ID. </summary>
    public static readonly AttributeId Invalid = new(ImGuiId.InvalidId);

    /// <summary> Whether the attribute ID is valid. </summary>
    public bool IsValid
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get { return Id is not uint.MaxValue; }
    }

    /// <summary> Get whether this attribute's pin is currently hovered by the mouse cursor. </summary>
    /// <remarks> Use after disposing the <seealso cref="NodeEditor"/>. </remarks>
    public unsafe bool Hovered
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get
        {
            AttributeId id;
            if (!Api.IsPinHovered(&id))
                return false;

            return id == Id;
        }
    }

    /// <summary> Get whether this attribute is currently active. </summary>
    public unsafe bool Active
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get
        {
            AttributeId id;
            if (!Api.IsAnyAttributeActive(&id))
                return false;

            return id == Id;
        }
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator AttributeId(uint v)
        => new(v);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator AttributeId(int v)
        => new((uint)v);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator AttributeId(ImGuiId v)
        => new(v.Id);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ImGuiId(AttributeId v)
        => new(v.Id);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static explicit operator uint(AttributeId v)
        => v.Id;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static explicit operator int(AttributeId v)
        => (int)v.Id;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static AttributeId operator ++(AttributeId id)
        => new(id.Id + 1);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static AttributeId operator --(AttributeId id)
        => new(id.Id - 1);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static AttributeId operator +(AttributeId id, int offset)
        => new((uint)(id.Id + offset));

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static AttributeId operator -(AttributeId id, int offset)
        => new((uint)(id.Id - offset));

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static AttributeId operator +(int offset, AttributeId id)
        => new((uint)(id.Id + offset));

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static AttributeId operator -(int offset, AttributeId id)
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
