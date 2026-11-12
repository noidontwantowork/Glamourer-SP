namespace ImSharp.ImNodes;

using ImSharp;

public static partial class Internal
{
    public unsafe struct EditorContext : IDisposable
    {
        public IdObjectPool<NodeData> Nodes;
        public IdObjectPool<PinData>  Pins;
        public IdObjectPool<LinkData> Links;

        public ImVector<NodeIndex> NodeDepthOrder;

        public Vector2 Panning;
        public Vector2 AutoPanningDelta;

        public Rectangle GridContentBounds;

        public ImVector<NodeIndex> SelectedNodeIndices;
        public ImVector<LinkIndex> SelectedLinkIndices;

        public ImVector<Vector2> SelectedNodeOffsets;
        public Vector2           PrimaryNodeOffset;

        public ClickInteractionState ClickInteraction;
        public MiniMapState          MiniMap;

        public struct MiniMapState
        {
            public bool                                     Enabled;
            public MiniMapLocation                          Location;
            public float                                    SizeFraction;
            public delegate* unmanaged<NodeId, void*, void> NodeHoveringCallback;
            public void*                                    NodeHoveringCallbackUserData;

            public Rectangle RectangleScreenSpace;
            public Rectangle ContentScreenSpace;
            public float     Scaling;
        }

        public void Dispose()
        {
            Nodes.Dispose();
            Pins.Dispose();
            Links.Dispose();
            NodeDepthOrder.Free<NodeIndex>();
            SelectedNodeIndices.Free<NodeIndex>();
            SelectedLinkIndices.Free<LinkIndex>();
            SelectedNodeOffsets.Free<TrivialTypeInformation<Vector2>>();
        }

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 ScreenToGrid(Vector2 point)
            => point - OriginScreenSpace - Panning;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Rectangle ScreenToGrid(in Rectangle rectangle)
            => new(ScreenToGrid(rectangle.Minimum), ScreenToGrid(rectangle.Maximum));

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 GridToScreen(Vector2 point)
            => point + OriginScreenSpace + Panning;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 GridToEditor(Vector2 point)
            => point + Panning;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 EditorToGrid(Vector2 point)
            => point - Panning;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 EditorToScreen(Vector2 point)
            => point + OriginScreenSpace;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 MiniMapToGrid(Vector2 point)
            => (point - MiniMap.ContentScreenSpace.Minimum) / MiniMap.Scaling + GridContentBounds.Minimum;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 ScreenToMiniMap(Vector2 point)
            => (ScreenToGrid(point) - GridContentBounds.Minimum) * MiniMap.Scaling + MiniMap.ContentScreenSpace.Minimum;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Rectangle ScreenToMiniMap(in Rectangle rectangle)
            => new(ScreenToMiniMap(rectangle.Minimum), ScreenToMiniMap(rectangle.Maximum));

        private static unsafe Vector2 OriginScreenSpace
            => ImNodes.Context->CanvasOriginScreenSpace;
    }
}
