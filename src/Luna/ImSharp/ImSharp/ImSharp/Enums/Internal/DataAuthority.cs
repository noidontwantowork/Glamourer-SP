namespace ImSharp.Internal;

/// <summary> Stores the source authority of a field. </summary>
public enum DataAuthority
{
    /// <summary> Automatically compute authority. </summary>
    Auto = 0,

    /// <summary> A dock node has source authority. </summary>
    DockNode = 1,

    /// <summary> A window has source authority. </summary>
    Window = 2,
}
