namespace ImSharp;

/// <summary> Flags that control the behavior of color edit widgets. </summary>
/// <remarks> All values shared with <seealso cref="ColorPickerFlags"/> and <seealso cref="ColorButtonFlags"/> are compatible. </remarks>
[Flags]
public enum ColorEditorFlags : uint
{
    /// <summary> No specific behavior. </summary>
    None = 0,

    /// <summary> Ignore the alpha component, only read 3 components from the input. </summary>
    NoAlpha = 1 << 1,

    /// <summary> Disable the picker normally appearing when clicking on the color square. </summary>
    NoPicker = 1 << 2,

    /// <summary> Disable toggling the color edit options menu when right-clicking on inputs or the small preview. </summary>
    NoOptions = 1 << 3,

    /// <summary> Disable the color square preview next to the input widgets. </summary>
    NoSmallPreview = 1 << 4,

    /// <summary> Disable the input widgets. </summary>
    NoInputs = 1 << 5,

    /// <summary> Disable the color information tooltip when hovering over the preview. </summary>
    NoTooltip = 1 << 6,

    /// <summary> Disable the display of the inline text label, but still show it in the tooltip and picker. </summary>
    NoLabel = 1 << 7,

    /// <summary> Disable the inputs as a drag and drop target. </summary>
    NoDragDrop = 1 << 9,

    /// <summary> Show the vertical alpha gradient in the picker. </summary>
    AlphaBar = 1 << 16,

    /// <summary> Display the preview square as a transparent color over a checkerboard according to the alpha value, instead of opaque. </summary>
    AlphaPreview = 1 << 17,

    /// <summary> Display half the preview square as a transparent color over a checkerboard according to the alpha value, and the other half opaque. </summary>
    AlphaPreviewHalf = 1 << 18,

    /// <summary> Disable [0..1] limits in RGBA displays. </summary>
    /// <remarks> Should also use <see cref="Float"/>. This is a WIP. </remarks>
    Hdr = 1 << 19,

    /// <summary> Override the display type to show RGB values. </summary>
    DisplayRgb = 1 << 20,

    /// <summary> Override the display type to show HSV values. </summary>
    DisplayHsv = 1 << 21,

    /// <summary> Override the display type to show Hex values. </summary>
    DisplayHex = 1 << 22,

    /// <summary> Display values as integers formatted in a range of [0..255] </summary>
    Uint8 = 1 << 23,

    /// <summary> Display values as floating point numbers formatted in a range of [0..1] (without <see cref="Hdr"/>). </summary>
    /// <remarks> No round-trip of values when changing from and to integers is guaranteed. </remarks>
    Float = 1 << 24,

    /// <summary> Input and output data values are given as RGB values. </summary>
    InputRgb = 1 << 27,

    /// <summary> Input and output data values are given as HSV values. </summary>
    InputHsv = 1 << 28,
}

/// <summary> Flags that control the behavior of color pickers. </summary>
/// <remarks> All values shared with <seealso cref="ColorEditorFlags"/> and <seealso cref="ColorButtonFlags"/> are compatible. </remarks>
[Flags]
public enum ColorPickerFlags : uint
{
    /// <inheritdoc cref="ColorEditorFlags.None"/>
    None = 0,

    /// <inheritdoc cref="ColorEditorFlags.NoAlpha"/>
    NoAlpha = ColorEditorFlags.NoAlpha,

    /// <inheritdoc cref="ColorEditorFlags.NoSmallPreview"/>
    NoSmallPreview = ColorEditorFlags.NoSmallPreview,

    /// <inheritdoc cref="ColorEditorFlags.NoInputs"/>
    NoInputs = ColorEditorFlags.NoInputs,

    /// <inheritdoc cref="ColorEditorFlags.NoTooltip"/>
    NoTooltip = ColorEditorFlags.NoTooltip,

    /// <inheritdoc cref="ColorEditorFlags.NoLabel"/>
    NoLabel = ColorEditorFlags.NoLabel,

    /// <summary> Disable the bigger color preview on the right side of the picker, use the small color preview instead. </summary>
    NoSidePreview = 1 << 8,

    /// <inheritdoc cref="ColorEditorFlags.AlphaBar"/>
    AlphaBar = ColorEditorFlags.AlphaBar,

    /// <inheritdoc cref="ColorEditorFlags.AlphaPreview"/>
    AlphaPreview = ColorEditorFlags.AlphaPreview,

    /// <inheritdoc cref="ColorEditorFlags.AlphaPreviewHalf"/>
    AlphaPreviewHalf = ColorEditorFlags.AlphaPreviewHalf,

    /// <inheritdoc cref="ColorEditorFlags.Uint8"/>
    Uint8 = ColorEditorFlags.Uint8,

    /// <inheritdoc cref="ColorEditorFlags.Float"/>
    Float = ColorEditorFlags.Float,

    /// <summary> Show a bar for the hue and rectangles for saturation/value. </summary>
    HueBar = 1 << 25,

    /// <summary> Show a wheel for the hue and a triangle for saturation/value. </summary>
    HueWheel = 1 << 26,

    /// <inheritdoc cref="ColorEditorFlags.InputRgb"/>
    InputRgb = ColorEditorFlags.InputRgb,

    /// <inheritdoc cref="ColorEditorFlags.InputHsv"/>
    InputHsv = ColorEditorFlags.InputHsv,
}

/// <summary> Flags that control the behavior of color buttons. </summary>
/// <remarks> All values shared with <seealso cref="ColorEditorFlags"/> and <seealso cref="ColorPickerFlags"/> are compatible. </remarks>
[Flags]
public enum ColorButtonFlags : uint
{
    /// <inheritdoc cref="ColorEditorFlags.None"/>
    None = 0,

    /// <inheritdoc cref="ColorEditorFlags.NoAlpha"/>
    NoAlpha = ColorEditorFlags.NoAlpha,

    /// <summary> Disable the button as a drag and drop source. </summary>
    NoDragDrop = ColorEditorFlags.NoDragDrop,

    /// <summary> Disable potential frame borders borders. Default. </summary>
    NoBorder = 1 << 10,

    /// <inheritdoc cref="ColorEditorFlags.AlphaPreview"/>
    AlphaPreview = ColorEditorFlags.AlphaPreview,

    /// <inheritdoc cref="ColorEditorFlags.AlphaPreviewHalf"/>
    AlphaPreviewHalf = ColorEditorFlags.AlphaPreviewHalf,

    /// <summary> Select any combination using one or more RGB values. </summary>
    DisplayRgb = ColorEditorFlags.DisplayRgb,

    /// <summary> Select any combination using one or more HSV values. </summary>
    DisplayHsv = ColorEditorFlags.DisplayHsv,

    /// <summary> Select any combination using one or more Hex values. </summary>
    DisplayHex = ColorEditorFlags.DisplayHex,

    /// <inheritdoc cref="ColorEditorFlags.Uint8"/>
    Uint8 = ColorEditorFlags.Uint8,

    /// <inheritdoc cref="ColorEditorFlags.Float"/>
    Float = ColorEditorFlags.Float,
}

public static class ColorEditorFlagExtensions
{
    /// <summary> The default options if the application or user do not specify other options. </summary>
    /// <remarks> This combines values for color edits, pickers and buttons. </remarks>
    public const ColorEditorFlags DefaultOptions =
        ColorEditorFlags.Uint8 | ColorEditorFlags.DisplayRgb | ColorEditorFlags.InputRgb | (ColorEditorFlags) ColorPickerFlags.HueBar;

    /// <summary> The mask for display settings. </summary>
    public const ColorEditorFlags DisplayMask = ColorEditorFlags.DisplayRgb | ColorEditorFlags.DisplayHex | ColorEditorFlags.DisplayHsv;

    /// <summary> The mask for data types. </summary>
    public const ColorEditorFlags DataTypeMask = ColorEditorFlags.Uint8 | ColorEditorFlags.Float;

    /// <summary> The mask for picker choices. </summary>
    public const ColorPickerFlags PickerMask = ColorPickerFlags.HueBar | ColorPickerFlags.HueWheel;

    /// <summary> The mask for input choices. </summary>
    public const ColorEditorFlags InputMask = ColorEditorFlags.InputHsv | ColorEditorFlags.InputRgb;
}
