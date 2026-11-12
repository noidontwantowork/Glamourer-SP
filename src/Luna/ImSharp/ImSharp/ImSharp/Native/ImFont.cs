namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe partial struct ImFont
        {
            public ImVector<ImFontGlyphHotData> IndexedHotData;
            public ImVector<float>              FrequentKerningPairs;
            public float                        FontSize;
            public ImVector<ImWchar>            IndexLookup;
            public ImVector<ImFontGlyph>        Glyphs;
            public ImFontGlyph*                 FallbackGlyph;
            public ImFontGlyphHotData*          FallbackHotData;
            public ImVector<ImFontKerningPair>  KerningPairs;
            public ImFontAtlas*                 ContainerAtlas;
            public ImFontConfig*                ConfigData;
            public short                        ConfigDataCount;
            public ImWchar                      FallbackChar;
            public ImWchar                      EllipsisChar;
            public ImWchar                      DotChar;
            public ImBool                       DirtyLookupTables;
            public float                        Scale;
            public float                        Ascent;
            public float                        Descent;
            public int                          MetricsTotalSurface;
            public PageMap                      Used4KPagesMap;

            [InlineArray((0xFFFF + 1) / 4096 / 8)]
            public struct PageMap
            {
                private byte _element;
            }

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImFont_GetCharAdvance")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial float GetCharAdvance(ImFont* self, ImWchar character);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImFont_CalcTextSizeA")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void CalcTextSize(ImVec2 *ret, ImFont* self, float size, float maxWidth, float wrapWidth, byte* testBegin, byte* textEnd, byte** remaining);
        }
    }
}
