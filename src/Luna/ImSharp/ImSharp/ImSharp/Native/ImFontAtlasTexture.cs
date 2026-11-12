namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe struct ImFontAtlasTexture
        {
            public ImTextureId TexId;
            public byte*       TexPixelsAlpha8;
            public uint*       TexPixelsRgba32;
        }
    }
}
