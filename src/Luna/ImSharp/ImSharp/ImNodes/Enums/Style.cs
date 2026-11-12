namespace ImSharp.ImNodes;

/// <summary> Style Variables that use float values used internally by ImNodes. </summary>
public enum ImNodesStyleSingle : uint
{
    /// <summary> The spacing of the grid divider lines in pixels. </summary>
    GridSpacing,

    /// <summary> The rounding of node corners in pixels. </summary>
    NodeCornerRounding,

    /// <summary> The thickness of borders around nodes in pixels. </summary>
    NodeBorderThickness,

    /// <summary> The thickness of link lines in pixels. </summary>
    LinkThickness,

    /// <summary> The number of link line segments per length. </summary>
    LinkLineSegmentsPerLength,

    /// <summary> The additional distance checked for hovering link lines in pixels. </summary>
    LinkHoverDistance,

    /// <summary> The radius of circle-type pins in pixels. </summary>
    PinCircleRadius,

    /// <summary> The side length of square-type pins in pixels.  </summary>
    PinQuadSideLength,

    /// <summary> The side length of triangle-type pins in pixels.  </summary>
    PinTriangleSideLength,

    /// <summary> The thickness of lines for pins in pixels. </summary>
    PinLineThickness,

    /// <summary> The additional radius checked for hovering pins in pixels.</summary>
    PinHoverRadius,

    /// <summary> The offset for pins. </summary>
    PinOffset,
}

/// <summary> Style Variables that use pairs of float values used internally by ImNodes. </summary>
public enum ImNodesStyleDouble : uint
{
    /// <summary> The padding to the sides of nodes in pixels. </summary>
    NodePadding,

    /// <summary> The padding to the sides of the mini map in pixels. </summary>
    MiniMapPadding,

    /// <summary> The mini maps offset from the screen side in pixels. </summary>
    MiniMapOffset,
}

public static class ImNodesStyleExtensions
{
    extension(ImNodesStyleSingle type)
    {
        /// <inheritdoc cref="ImNodes.StyleDisposable.Push(ImNodesStyleSingle,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ImNodes.StyleDisposable Push(float value, bool condition)
            => condition ? new ImNodes.StyleDisposable() : new ImNodes.StyleDisposable().Push(type, value);

        /// <inheritdoc cref="ImNodes.StyleDisposable.Push(ImNodesStyleSingle,float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ImNodes.StyleDisposable Push(float value)
            => new ImNodes.StyleDisposable().Push(type, value);
    }

    extension(ImNodesStyleDouble type)
    {
        /// <inheritdoc cref="ImNodes.StyleDisposable.Push(ImNodesStyleDouble,Vector2,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ImNodes.StyleDisposable Push(Vector2 value, bool condition)
            => condition ? new ImNodes.StyleDisposable() : new ImNodes.StyleDisposable().Push(type, value);

        /// <inheritdoc cref="ImNodes.StyleDisposable.Push(ImNodesStyleDouble,Vector2)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ImNodes.StyleDisposable Push(Vector2 value)
            => new ImNodes.StyleDisposable().Push(type, value);

        /// <inheritdoc cref="ImNodes.StyleDisposable.PushX(ImNodesStyleDouble,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ImNodes.StyleDisposable PushX(float value, bool condition)
            => condition ? new ImNodes.StyleDisposable() : new ImNodes.StyleDisposable().PushX(type, value);

        /// <inheritdoc cref="ImNodes.StyleDisposable.PushX(ImNodesStyleDouble,float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ImNodes.StyleDisposable PushX(float value)
            => new ImNodes.StyleDisposable().PushX(type, value);

        /// <inheritdoc cref="ImNodes.StyleDisposable.PushY(ImNodesStyleDouble,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ImNodes.StyleDisposable PushY(float value, bool condition)
            => condition ? new ImNodes.StyleDisposable() : new ImNodes.StyleDisposable().PushY(type, value);

        /// <inheritdoc cref="ImNodes.StyleDisposable.PushY(ImNodesStyleDouble,float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ImNodes.StyleDisposable PushY(float value)
            => new ImNodes.StyleDisposable().PushY(type, value);
    }
}
