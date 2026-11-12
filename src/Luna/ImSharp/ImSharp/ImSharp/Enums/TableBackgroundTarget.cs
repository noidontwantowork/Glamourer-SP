namespace ImSharp;

/// <summary> Targets for <seealso cref="Im.TableDisposable.SetBackgroundColor"/>. </summary>
[Flags]
public enum TableBackgroundTarget
{
    /// <summary> No target. </summary>
    None = 0,

    /// <summary> Set row background color 0 (generally used for background, automatically set when <seealso cref="TableFlags.RowBackground"/> is used). </summary>
    Row0 = 1,

    /// <summary> Set row background color 1 (generally used for selection marking). </summary>
    Row1 = 2,

    /// <summary> Set cell background color (top-most color). </summary>
    Cell = 3,
}
