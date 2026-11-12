namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct ImDrawListSharedData
            {
                public ImTextureId        TexIdCommon;
                public ImVec2             TexUvWhitePixel;
                public ImFont*            Font;
                public float              FontSize;
                public float              CurveTesselationTolerance;
                public float              CircleSegmentMaxError;
                public ImVec4             ClipRectFullScreen;
                public ImDrawListFlags    InitialFlags;
                public ArcVertexArray     ArcFastVtx;
                public float              ArcFastRadiusCutoff;
                public CircleSegmentArray CircleSegmentCounts;
                public ImVec4*            TexUvLines;

                [InlineArray(48)]
                public struct ArcVertexArray
                {
                    private ImVec2 _element;
                }

                [InlineArray(64)]
                public struct CircleSegmentArray
                {
                    private byte _element;
                }
            }

            [InlineArray(2)]
            public struct ImDrawDataBuilder
            {
                private ImVector<Pointer<ImDrawList>> _layers;
            }
        }
    }
}
