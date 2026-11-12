namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe struct ImFontConfig
        {
            public void*     FontData;
            public int       FontDataSize;
            public ImBool    FontDataOwnedByAtlas;
            public int       FontNo;
            public float     SizePixels;
            public int       OversampleH;
            public int       OversampleV;
            public ImBool    PixelSnapH;
            public ImVec2    GlyphExtraSpacing;
            public ImVec2    GlyphOffset;
            public ImWchar*  GlyphRanges;
            public float     GlyphMinAdvanceX;
            public float     GlyphMaxAdvanceX;
            public ImBool    MergeMode;
            public uint      FontBuilderFlags;
            public float     RasterizerMultiply;
            public float     RasterizerDensity;
            public ImWchar   EllipsisChar;
            public NameArray Name;
            public ImFont*   DstFont;

            [InlineArray(40)]
            public struct NameArray
            {
                private byte _element;
            }
        }
    }
}
