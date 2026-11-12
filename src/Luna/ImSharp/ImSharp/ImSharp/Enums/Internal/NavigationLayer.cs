namespace ImSharp.Internal;

/// <summary> Denotes the current navigation layer. </summary>
public enum NavigationLayer
{
    /// <summary> The main scrolling layer. </summary>
    Main = 0,

    /// <summary> The menu layer, accessed via <seealso cref="NavigationInput.Menu"/> </summary>
    Menu = 1,

    /// <summary> The number of different navigation layers. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Count,
}
