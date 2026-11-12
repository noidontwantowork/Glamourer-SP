namespace ImSharp.Internal;

/// <summary> Flags that describe the behavior of a separator line. </summary>
[Flags]
public enum SeparatorFlags : uint
{
    /// <summary> No specific behavior. </summary>
    None = 0,

    /// <summary> Span a horizontal separator line. </summary>
    Horizontal = 1 << 0,

    /// <summary> Span a vertical separator line. </summary>
    Vertical = 1 << 1,

    /// <summary> Span all columns. </summary>
    SpanAllColumns = 1 << 2,
}
