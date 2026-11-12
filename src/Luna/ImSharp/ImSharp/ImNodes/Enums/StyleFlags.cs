namespace ImSharp.ImNodes;

/// <summary> Flags that govern the style of ImNode. </summary>
[Flags]
public enum ImNodesStyleFlags : uint
{
    /// <summary> No specific style settings. </summary>
    None = 0,

    /// <summary> Whether nodes should have an outline or not. </summary>
    NodeOutline = 1 << 0,

    /// <summary> Whether the grid should show axis divider lines. </summary>
    GridLines = 1 << 2,

    /// <summary> Whether the grid should show primary axis divider lines. </summary>
    GridLinesPrimary = 1 << 3,

    /// <summary> Whether objects should snap to the grid. </summary>
    GridSnapping = 1 << 4,
}
