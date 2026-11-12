namespace ImSharp;

/// <summary> An interface to represent an icon font and the possible values for it for use in generics. </summary>
public interface IIconStandIn
{
    /// <summary> Determine whether this icon is empty. </summary>
    /// <remarks> This should be equivalent to checking whether <see cref="Span"/> is empty. </remarks>
    public bool IsEmpty { get; }

    /// <summary> Get the value to print this icon as a null-terminated UTF8 string. </summary>
    public ReadOnlySpan<byte> Span { get; }

    /// <summary> Get the font used for this implementation of icon types. </summary>
    public abstract static Im.Font Font { get; }
}
