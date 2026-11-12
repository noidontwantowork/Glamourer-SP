namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe partial struct ImFontAtlas
        {
            public ImFontAtlasFlags                Flags;
            public ImVector<ImFontAtlasTexture>    Textures;
            public int                             TexDesiredWidth;
            public int                             TexDesiredHeight;
            public int                             TexGlyphPadding;
            public ImBool                          Locked;
            public ImBool                          TexReady;
            public ImBool                          TexPixelsUseColors;
            public int                             TexWidth;
            public int                             TexHeight;
            public ImVec2                          TexUvScale;
            public ImVec2                          TexUvWhitePixel;
            public ImVector<Pointer<ImFont>>       Fonts;
            public ImVector<ImFontAtlasCustomRect> CustomRects;
            public ImVector<ImFontConfig>          ConfigData;
            public UvLineArray                     TexUvLines;
            public ImFontBuilderIo*                FontBuilderIo;
            public uint                            FontBuilderFlags;
            public int                             PackIdMouseCursors;
            public int                             PackIdLines;

            [InlineArray(64)]
            public struct UvLineArray
            {
                private ImVec4 _element;
            }

            [MethodImpl(ImSharpConfiguration.Inl)]
            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImFontAtlas_AddFontDefault")]
            public static partial ImFont* AddFontDefault(ImFontAtlas* self, ImFontConfig* config);

            [MethodImpl(ImSharpConfiguration.Inl)]
            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImFontAtlas_Build")]
            public static partial void Build(ImFontAtlas* self);

            [MethodImpl(ImSharpConfiguration.Inl)]
            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImFontAtlas_ClearTexData")]
            public static partial void ClearTexData(ImFontAtlas* self);

            [MethodImpl(ImSharpConfiguration.Inl)]
            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImFontAtlas_SetTexID")]
            public static partial void SetTexId(ImFontAtlas* self, int textureIndex, ImTextureId id);

            [MethodImpl(ImSharpConfiguration.Inl)]
            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImFontAtlas_GetTexDataAsAlpha8")]
            public static partial void GetTexDataAsAlpha8(ImFontAtlas* self, int textureIndex, byte** pixels, int* width, int* height, int* bytesPerPixel);

            [MethodImpl(ImSharpConfiguration.Inl)]
            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImFontAtlas_GetTexDataAsRGBA32")]
            public static partial void GetTexDataAsRgba32(ImFontAtlas* self, int textureIndex, byte** pixels, int* width, int* height, int* bytesPerPixel);
        }
    }
}
