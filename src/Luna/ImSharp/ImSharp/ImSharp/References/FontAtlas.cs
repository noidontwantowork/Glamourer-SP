namespace ImSharp;

public static partial class Im
{
    /// <summary> A reference to a ImGui Font Atlas. </summary>
    /// <param name="pointer"> The native pointer to the font atlas. </param>
    public readonly unsafe ref struct FontAtlas(Native.ImFontAtlas* pointer)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Native.ImFontAtlas* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator FontAtlas(Native.ImFontAtlas* pointer)
            => new(pointer);

        /// <summary> Add the default font to the atlas. </summary>
        /// <returns> A reference to the created default font. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Font AddFontDefault()
            => Native.ImFontAtlas.AddFontDefault(Pointer, null);

        /// <summary> Add the default font to the atlas with specific configuration. </summary>
        /// <returns> A reference to the created default font. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Font AddFontDefault(FontConfig config)
            => Native.ImFontAtlas.AddFontDefault(Pointer, config.Pointer);

        /// <summary> Set the texture ID for the font atlas. </summary>
        /// <param name="id"> The desired texture ID from the graphics API. </param>
        /// <param name="textureIndex"> The index for the texture to set. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void SetTexId(ImTextureId id, int textureIndex = 0)
            => Native.ImFontAtlas.SetTexId(Pointer, textureIndex, id);

        /// <summary> Build the font atlas. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Build()
            => Native.ImFontAtlas.Build(Pointer);

        /// <summary> Obtain the texture data as a single-channel bitmap. </summary>
        /// <returns> The texture data. </returns>
        /// <remarks> 1 byte per pixel. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public TextureData GetTextureDataAsAlpha8(int textureIndex = 0)
        {
            var ret = new TextureData();
            Native.ImFontAtlas.GetTexDataAsAlpha8(Pointer, textureIndex, (byte**)&ret.PixelData, &ret.Width, &ret.Height, &ret.BytesPerPixel);
            return ret;
        }

        /// <summary> Obtain the texture data as RGBA32 bitmap. </summary>
        /// <returns> The texture data. </returns>
        /// <remarks> 4 bytes per pixel. Usually 75% wasted, prefer <seealso cref="GetTextureDataAsAlpha8"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public TextureData GetTextureDataAsRgba32(int textureIndex = 0)
        {
            var ret = new TextureData();
            Native.ImFontAtlas.GetTexDataAsRgba32(Pointer, textureIndex, (byte**)&ret.PixelData, &ret.Width, &ret.Height, &ret.BytesPerPixel);
            return ret;
        }

        /// <summary> Clear the output texture data from CPU memory. </summary>
        /// <remarks> Use this after copying the texture data to the GPU. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void ClearTexData()
            => Native.ImFontAtlas.ClearTexData(Pointer);


        /// <summary> Returned information about the provided texture. </summary>
        public struct TextureData
        {
            /// <summary> A pointer to the pixel data of the texture. </summary>
            public nint PixelData;

            /// <summary> The width of the texture in pixels. </summary>
            public int Width;

            /// <summary> The height of the texture in pixels. </summary>
            public int Height;

            /// <summary> The number of bytes per pixel, 1 for <seealso cref="GetTextureDataAsAlpha8"/> and 4 for <seealso cref="GetTextureDataAsRgba32"/>. </summary>
            public int BytesPerPixel;

            /// <summary> Deconstruct into a tuple of variables. </summary>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void Deconstruct(out int width, out int height, out int bytesPerPixel, out nint data)
            {
                width         = Width;
                height        = Height;
                bytesPerPixel = BytesPerPixel;
                data          = PixelData;
            }
        }
    }
}
