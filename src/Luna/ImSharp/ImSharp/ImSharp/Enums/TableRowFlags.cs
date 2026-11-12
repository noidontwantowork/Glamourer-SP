namespace ImSharp;

/// <summary> Flags that control the behavior of a table row. </summary>
[Flags]
public enum TableRowFlags
{
    /// <summary> No special behaviour. </summary>
    None = 0,

    /// <summary> The row represents a header row and is styled accordingly. </summary>
    Headers = 1 << 0,
}
