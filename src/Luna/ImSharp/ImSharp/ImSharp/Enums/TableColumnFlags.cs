namespace ImSharp;

/// <summary> Flags that control the behaviour of a table column. </summary>
[Flags]
public enum TableColumnFlags
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> This column is hidden at the start. </summary>
    DefaultHide = 1 << 0,

    /// <summary> This column is sorting at the start. </summary>
    DefaultSort = 1 << 1,

    /// <summary> This column will stretch. Default if <seealso cref="TableFlags.SizingStretchSame"/> or <seealso cref="TableFlags.SizingStretchProp"/> are set. </summary>
    WidthStretch = 1 << 2,

    /// <summary> This column will not stretch. Default if <seealso cref="TableFlags.SizingFixedFit"/> is set and the table is resizable. </summary>
    WidthFixed = 1 << 3,

    /// <summary> Disable manually resizing this column for users. </summary>
    NoResize = 1 << 4,

    /// <summary> Disable manually reordering this column for users. </summary>
    NoReorder = 1 << 5,

    /// <summary> Disable manually hiding this column for users. </summary>
    NoHide = 1 << 6,

    /// <summary> Disable clipping for this column, meaning it's content can overlap into adjacent columns. </summary>
    NoClip = 1 << 7,

    /// <summary> Disable the ability to sort on this field. </summary>
    NoSort = 1 << 8,

    /// <summary> Disable the ability to sort on this field in the ascending direction. </summary>
    NoSortAscending = 1 << 9,

    /// <summary> Disable the ability to sort on this field in the descending direction. </summary>
    NoSortDescending = 1 << 10,

    /// <summary> Do not consider the header text width when computing automatic column width. </summary>
    NoHeaderWidth = 1 << 11,

    /// <summary> Make the initial sort direction ascending when first sorting on this column (Default). </summary>
    PreferSortAscending = 1 << 12,

    /// <summary> Make the initial sort direction descending when first sorting on this column. </summary>
    PreferSortDescending = 1 << 13,

    /// <summary> Use the current indent value when entering a cell. Default for the first column. </summary>
    IndentEnable = 1 << 14,

    /// <summary> Ignore the current indent value when entering a cell. Default for all but the first column. </summary>
    IndentDisable = 1 << 15,

    /// <summary> Do not show this column at all, even in the user context menu. </summary>
    Disabled = 1 << 16,

    /// <summary> <seealso cref="Im.TableDisposable.HeaderRow"/> will submit an empty label for this column. </summary>
    /// <remarks> The column will still be named in the context menu or angled headers. Useful for some small columns. </remarks>
    NoHeaderLabel = 1 << 17,

    /// <summary> The column is not hidden by the user or API. </summary>
    /// <remarks> Read-only output flag returned from <seealso cref="Im.TableDisposable.GetColumnFlags"/>. </remarks>
    IsEnabled = 1 << 20,

    /// <summary> The column is not hidden by the user or API AND not clipped by scrolling. </summary>
    /// <remarks> Read-only output flag returned from <seealso cref="Im.TableDisposable.GetColumnFlags"/>. </remarks>
    IsVisible = 1 << 21,

    /// <summary> The column is currently part of the sort specs. </summary>
    /// <remarks> Read-only output flag returned from <seealso cref="Im.TableDisposable.GetColumnFlags"/>. </remarks>
    IsSorted = 1 << 22,

    /// <summary> The column is currently hovered by the mouse cursor. </summary>
    /// <remarks> Read-only output flag returned from <seealso cref="Im.TableDisposable.GetColumnFlags"/>. </remarks>
    IsHovered = 1 << 23,

    /// <summary> Mask for width options. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    WidthMask = WidthStretch | WidthFixed,

    /// <summary> Mask for indent options. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    IndentMask = IndentDisable | IndentEnable,

    /// <summary> Mask for status options. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    StatusMask = IsEnabled | IsVisible | IsSorted | IsHovered,

    /// <summary> Internal flag. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoDirectResize = 1 << 30,
}
