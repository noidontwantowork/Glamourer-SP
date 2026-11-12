namespace ImSharp.Internal;

/// <summary> Horizontal or Vertical axis. Fixed to 0/1 so they can index vectors. </summary>
public enum Axis
{
    /// <summary> No known axis. </summary>
    None = -1,

    /// <summary> The horizontal axis. </summary>
    X = 0,

    /// <summary> The vertical axis. </summary>
    Y = 1,
}
