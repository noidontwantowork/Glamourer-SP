namespace ImSharp;

/// <summary> Flags controlling the behaviour of a combo box. </summary>
[Flags]
public enum ComboFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Align the popup toward the left. </summary>
    PopupAlignLeft = 1 << 0,

    /// <summary> The combo will show approximately 4 items of <seealso cref="Im.Style.TextHeight"/>. </summary>
    /// <remarks> Using <seealso cref="Im.Window.SetNextSizeConstraints"/> before beginning the combo can force a specific size. </remarks>
    HeightSmall = 1 << 1,

    /// <summary> The combo will show approximately 8 items of <seealso cref="Im.Style.TextHeight"/> (Default). </summary>
    /// <remarks> Using <seealso cref="Im.Window.SetNextSizeConstraints"/> before beginning the combo can force a specific size. </remarks>
    HeightRegular = 1 << 2,

    /// <summary> The combo will show approximately 20 items of <seealso cref="Im.Style.TextHeight"/>. </summary>
    /// <remarks> Using <seealso cref="Im.Window.SetNextSizeConstraints"/> before beginning the combo can force a specific size. </remarks>
    HeightLarge = 1 << 3,

    /// <summary> The combo will show as many items as possible. </summary>
    /// <remarks> Using <seealso cref="Im.Window.SetNextSizeConstraints"/> before beginning the combo can force a specific size. </remarks>
    HeightLargest = 1 << 4,

    /// <summary> Do not display the square arrow button on the preview box. </summary>
    NoArrowButton = 1 << 5,

    /// <summary> Display only the square arrow button on the preview box. </summary>
    NoPreview = 1 << 6,

    /// <summary> The mask for different height options. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    HeightMask = HeightSmall | HeightRegular | HeightLarge | HeightLargest,

    /// <summary> Enables <seealso cref="Native.Methods.Internal.BeginComboPreview"/>. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    CustomPreview = 1 << 20,
}
