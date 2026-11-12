using ImSharp.ImNodes;

namespace ImSharp;

/// <summary> The internally used ID type. </summary>
public readonly record struct ImGuiId(uint Id) : ISpanFormattable, IUtf8SpanFormattable
{
    /// <summary> An invalid ID. </summary>
    public const uint InvalidId = uint.MaxValue;

    /// <summary> An invalid ID. </summary>
    public static readonly ImGuiId Invalid = new(InvalidId);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ImGuiId(uint v)
        => new(v);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ImGuiId(int v)
        => new((uint)v);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static explicit operator uint(ImGuiId v)
        => v.Id;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static explicit operator int(ImGuiId v)
        => (int)v.Id;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static ImGuiId operator ++(ImGuiId id)
        => new(id.Id + 1);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static ImGuiId operator --(ImGuiId id)
        => new(id.Id - 1);

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


    /// <summary> Whether a widget with this ID is currently active. </summary>
    public bool Active
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Im.Id.IsActive(this);
    }

    /// <summary> Whether a widget with this ID is currently active. </summary>
    public bool ActivePreviousFrame
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Im.Id.WasActive(this);
    }
}
