namespace ImSharp;

/// <summary> Flags that control the behaviour of a table. </summary>
[Flags]
public enum TableFlags
{
    /// <summary> No special behaviour. </summary>
    None = 0,

    /// <summary> Columns can be resized by the user. </summary>
    Resizable = 1 << 0,

    /// <summary> Columns can be reordered by the user if a header row is drawn. </summary>
    Reorderable = 1 << 1,

    /// <summary> Columns can be hidden by the user with the right-click context menu. </summary>
    Hideable = 1 << 2,

    /// <summary> Columns can be sorted. This does not do any sorting of data, it just allows to query <seealso cref="Im.TableSortSpecifications"/> and the users to see and change sort state. </summary>
    Sortable = 1 << 3,

    /// <summary> Disable persisting the column order, width and sort settings when saving to .ini files. </summary>
    NoSavedSettings = 1 << 4,

    /// <summary> Enable right-clicking on the column body to open the context menu. </summary>
    ContextMenuInBody = 1 << 5,

    /// <summary> Each row has alternating colors using <seealso cref="ImGuiColor.TableRowBackground"/> and <seealso cref="ImGuiColor.TableRowBackgroundAlt"/> respectively. </summary>
    RowBackground = 1 << 6,

    /// <summary> Draw horizontal borders between rows. </summary>
    BordersInnerHorizontal = 1 << 7,

    /// <summary> Draw horizontal borders at the top and bottom. </summary>
    BordersOuterHorizontal = 1 << 8,

    /// <summary> Draw vertical borders between columns. </summary>
    BordersInnerVertical = 1 << 9,

    /// <summary> Draw vertical borders on the left and right end. </summary>
    BordersOuterVertical = 1 << 10,

    /// <summary> Disable the vertical borders in the columns body but display them in headers. </summary>
    NoBordersInBody = 1 << 11,

    /// <summary> Disable the vertical borders in the columns body unless hovered. Always displays them in headers. </summary>
    NoBordersInBodyUntilResize = 1 << 12,

    /// <summary> Columns match their size to their content's width. </summary>
    SizingFixedFit = 1 << 13,

    /// <summary> Every column matches the maximum content width of all columns. </summary>
    SizingFixedSame = 2 << 13,

    /// <summary> Columns stretch their width with default weights proportional to their content's width. </summary>
    SizingStretchProp = 3 << 13,

    /// <summary> Columns stretch their width with default weights being equal. </summary>
    SizingStretchSame = 4 << 13,

    /// <summary> Make the outer width of the table fit to the columns and ignore the provided outer size. </summary>
    /// <remarks> Can not be used with scroll bars or stretch columns. </remarks>
    NoHostExtendX = 1 << 16,

    /// <summary> Make the outer height stop exactly at the provided outer size. </summary>
    /// <remarks> Can not be used with scroll bars. Anything below the limit will be clipped. </remarks>
    NoHostExtendY = 1 << 17,

    /// <summary> Disable keeping columns always minimally visible when no horizontal scrollbar is enabled and the table gets too small for all columns. </summary>
    NoKeepColumnsVisible = 1 << 18,

    /// <summary> Disable distributing remaining width to stretched columns. </summary>
    PreciseWidths = 1 << 19,

    /// <summary> Disable clipping individual columns so that cells may overflow into other columns. </summary>
    /// <remarks> Incompatible with <seealso cref="Im.TableDisposable.SetupScrollFreeze"/>. </remarks>
    NoClip = 1 << 20,

    /// <summary> Enable outermost padding. Default if <seealso cref="BordersOuterVertical"/> is on. </summary>
    PadOuterX = 1 << 21,

    /// <summary> Disable outermost padding. Default if <seealso cref="BordersOuterVertical"/> is off. </summary>
    NoPadOuterX = 1 << 22,

    /// <summary> Disable inner padding between columns. </summary>
    NoPadInnerX = 1 << 23,

    /// <summary> Enable horizontal scrolling. Requires the outer size of the table to be fixed. </summary>
    ScrollX = 1 << 24,

    /// <summary> Enable vertical scrolling. Requires the outer size of the table to be fixed. </summary>
    ScrollY = 1 << 25,

    /// <summary> Enables sorting on multiple columns when shift is held while sorting. </summary>
    /// <remarks> May return sort specs where the count is > 1. </remarks>
    SortMulti = 1 << 26,

    /// <summary> Enables toggling off sorting instead of only flipping between ascending and descending. </summary>
    /// <remarks> May return sort specs where the count is == 1. </remarks>
    SortTriState = 1 << 27,

    /// <summary> Draw all horizontal borders. </summary>
    BordersHorizontal = BordersInnerHorizontal | BordersOuterHorizontal,

    /// <summary> Draw all vertical borders. </summary>
    BordersVertical = BordersInnerVertical | BordersOuterVertical,

    /// <summary> Draw all inner borders. </summary>
    BordersInner = BordersInnerVertical | BordersInnerHorizontal,

    /// <summary> Draw all outer borders. </summary>
    BordersOuter = BordersOuterVertical | BordersOuterHorizontal,

    /// <summary> Draw all borders. </summary>
    Borders = BordersInner | BordersOuter,
}
