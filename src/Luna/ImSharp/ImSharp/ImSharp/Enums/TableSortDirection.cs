namespace ImSharp;

/// <summary> A sorting direction. </summary>
public enum SortDirection : byte
{
    /// <summary> This column is not sorted. </summary>
    None = 0,

    /// <summary> Ascending, i.e. 0 to 9, A to Z... </summary>
    Ascending = 1,

    /// <summary> Descending, i.e. 9 to 0, Z to A... </summary>
    Descending = 2,
}
