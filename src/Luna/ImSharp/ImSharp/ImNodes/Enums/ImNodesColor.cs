namespace ImSharp.ImNodes;

/// <summary> Color variables used in ImNodes. </summary>
public enum ImNodesColor : uint
{
    /// <summary> The background of regular nodes. </summary>
    NodeBackground,

    /// <summary> The background of regular nodes when currently hovered by the mouse. </summary>
    NodeBackgroundHovered,

    /// <summary> The background of regular nodes when currently selected. </summary>
    NodeBackgroundSelected,

    /// <summary> The outline of regular nodes. </summary>
    NodeOutline,

    /// <summary> The color of the node title bar. </summary>
    TitleBar,

    /// <summary> The color of the node title bar when currently hovered by the mouse. </summary>
    TitleBarHovered,

    /// <summary> The color of the node title bar when currently selected. </summary>
    TitleBarSelected,

    /// <summary> The color of a link between nodes. </summary>
    Link,

    /// <summary> The color of a link between nodes when currently hovered by the mouse. </summary>
    LinkHovered,

    /// <summary> The color of a link between nodes when currently selected. </summary>
    LinkSelected,

    /// <summary> The color of a node pin. </summary>
    Pin,

    /// <summary> The color of a node pin when currently hovered by the mouse. </summary>
    PinHovered,

    /// <summary> The background tint of the box selector when holding the mouse to select multiple objects. </summary>
    BoxSelector,

    /// <summary> The outline of the box selector when holding the mouse to select multiple objects. </summary>
    BoxSelectorOutline,

    /// <summary> The background of the main grid. </summary>
    GridBackground,

    /// <summary> The lines of the main grid. </summary>
    GridLine,

    /// <summary> The primary lines of the main grid. </summary>
    GridLinePrimary,

    /// <summary> The background of the mini map. </summary>
    MiniMapBackground,

    /// <summary> The background of the mini map when currently hovered by the mouse. </summary>
    MiniMapBackgroundHovered,

    /// <summary> The outline of the mini map. </summary>
    MiniMapOutline,

    /// <summary> The outline of the mini map when currently hovered by the mouse. </summary>
    MiniMapOutlineHovered,

    /// <summary> The background of nodes in the mini map. </summary>
    MiniMapNodeBackground,

    /// <summary> The background of nodes in the mini map when currently hovered by the mouse. </summary>
    MiniMapNodeBackgroundHovered,

    /// <summary> The background of nodes in the mini map when currently selected. </summary>
    MiniMapNodeBackgroundSelected,

    /// <summary> The outline of nodes in the mini map. </summary>
    MiniMapNodeOutline,

    /// <summary> The color of links between nodes in the mini map.</summary>
    MiniMapLink,

    /// <summary> The color of links between nodes in the mini map when currently selected.</summary>
    MiniMapLinkSelected,

    /// <summary> The background color of the mini map canvas. </summary>
    MiniMapCanvas,

    /// <summary> The outline of the mini map canvas. </summary>
    MiniMapCanvasOutline,
}

public static class ImNodesColorExtensions
{
    public const int NumColors = 29;

    extension(ImNodesColor type)
    {
        /// <inheritdoc cref="ImNodes.ColorDisposable.Push(ImNodesColor,Rgba32,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ImNodes.ColorDisposable Push(Rgba32 color, bool condition)
            => new ImNodes.ColorDisposable().Push(type, color, condition);

        /// <inheritdoc cref="ImNodes.ColorDisposable.Push(ImNodesColor,ColorParameter)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ImNodes.ColorDisposable Push(ColorParameter color)
            => color.IsDefault ? new ImNodes.ColorDisposable() : new ImNodes.ColorDisposable().Push(type, color.Color!.Value);

        /// <inheritdoc cref="ImNodes.ColorDisposable.Push(ImNodesColor,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ImNodes.ColorDisposable Push(Rgba32 color)
            => new ImNodes.ColorDisposable().Push(type, color);
    }
}
