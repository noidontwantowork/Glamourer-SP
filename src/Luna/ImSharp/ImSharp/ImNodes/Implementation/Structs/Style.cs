namespace ImSharp.ImNodes;

public static partial class Internal
{
    public struct Style()
    {
        public float             GridSpacing               = 24;
        public float             NodeCornerRounding        = 4;
        public ImVec2            NodePadding               = new(8, 8);
        public float             NodeBorderThickness       = 1;
        public float             LinkThickness             = 3;
        public float             LinkLineSegmentsPerLength = 0.1f;
        public float             LinkHoverDistance         = 10;
        public float             PinCircleRadius           = 4;
        public float             PinQuadSideLength         = 7;
        public float             PinTriangleSideLength     = 9.5f;
        public float             PinLineThickness          = 1;
        public float             PinHoverRadius            = 10;
        public float             PinOffset                 = 0;
        public ImVec2            MiniMapPadding            = new(8, 8);
        public ImVec2            MiniMapOffset             = new(4, 4);
        public ImNodesStyleFlags Flags                     = ImNodesStyleFlags.NodeOutline | ImNodesStyleFlags.GridLines;
        public ColorArray        Colors                    = new();

        [InlineArray(ImNodesColorExtensions.NumColors)]
        public struct ColorArray
        {
            private Rgba32 _element0;
        }
    }
}
