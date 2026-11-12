// ReSharper disable MethodOverloadWithOptionalParameter

namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper class around image related methods. </summary>
    public static class Image
    {
        /// <summary> Draw an image by its ID. </summary>
        /// <param name="image"> The ID of the image. </param>
        /// <param name="size"> The size for the image in pixels. </param>
        /// <param name="uvMinimum"> The upper left corner of the UV mapping, normalized to [0,0]x[1,1] (default (0, 0)). </param>
        /// <param name="uvMaximum"> The lower right corner of the UV mapping, normalized to [0,0]x[1,1] (default (1, 1)). </param>
        /// <param name="tint"> A tint applied to the image (default pure white for no tint). </param>
        /// <param name="borderColor"> The color of the border drawn around the window (default transparent). </param>
        [OverloadResolutionPriority(50)]
        public static void Draw(ImTextureId image, Vector2 size, Vector2? uvMinimum = null, Vector2? uvMaximum = null, in Vector4? tint = null,
            in Vector4 borderColor = default)
            => Native.Methods.Widgets.Image(image, size, uvMinimum ?? Vector2.Zero, uvMaximum ?? Vector2.One, tint ?? Vector4.One, borderColor);

        /// <inheritdoc cref="Draw(ImTextureId,Vector2,Vector2?,Vector2?,in Vector4?,in Vector4)"/>
        [OverloadResolutionPriority(0)]
        public static void Draw(ImTextureId image, Vector2 size, Vector2? uvMinimum = null, Vector2? uvMaximum = null,
            ColorParameter tint = default, Rgba32 borderColor = default)
            => Native.Methods.Widgets.Image(image, size, uvMinimum ?? Vector2.Zero, uvMaximum ?? Vector2.One,
                tint.CheckDefault(Rgba32.White).ToVector(), borderColor.ToVector());

        /// <inheritdoc cref="Draw(ImTextureId,Vector2,Vector2?,Vector2?,in Vector4?,in Vector4)"/>
        [OverloadResolutionPriority(100)]
        public static void Draw(ImTextureId image, Vector2 size)
            => Native.Methods.Widgets.Image(image, size, new ImVec2(0, 0), new ImVec2(1, 1), new ImVec4(1, 1, 1, 1), new ImVec4(0, 0, 0, 0));

        /// <summary> Draw a scaled image by its ID. If the image is larger than <paramref name="iconSize"/>, show the full-sized image on hover. </summary>
        /// <param name="image"> The ID of the image. </param>
        /// <param name="iconSize"> The size to scale the image to in pixels. </param>
        /// <param name="imageSize"> The actual, unscaled size of the image in pixels. </param>
        public static void DrawScaled(ImTextureId image, Vector2 iconSize, Vector2 imageSize)
        {
            Draw(image, iconSize);

            if (iconSize.X <= imageSize.X && iconSize.Y <= imageSize.Y && Item.Hovered(HoveredFlags.AllowWhenDisabled))
                Tooltip.ImageOnHover(image, imageSize);
        }

        /// <summary> Draw an interactable button using an image by its ID. </summary>
        /// <param name="image"> The ID of the image. </param>
        /// <param name="size"> The size for the button in pixels. </param>
        /// <param name="uvMinimum"> The upper left corner of the UV mapping, normalized to [0,0]x[1,1] (default (0, 0)). </param>
        /// <param name="uvMaximum"> The lower right corner of the UV mapping, normalized to [0,0]x[1,1] (default (1, 1)). </param>
        /// <param name="tint"> A tint applied to the image (default pure white for no tint). </param>
        /// <param name="backgroundColor"> The color of the frame drawn around the image (default transparent). </param>
        /// <param name="framePadding"> The frame padding to use. If this is negative, the default style is used (by default). </param>
        [OverloadResolutionPriority(50)]
        public static bool Button(ImTextureId image, Vector2 size, Vector2? uvMinimum = null, Vector2? uvMaximum = null,
            in Vector4? tint = null, in Vector4 backgroundColor = default, int framePadding = -1)
            => Native.Methods.Widgets.ImageButton(image, size, uvMinimum ?? Vector2.Zero, uvMaximum ?? Vector2.One, framePadding,
                backgroundColor, tint ?? Vector4.One);

        /// <inheritdoc cref="Button(ImTextureId,Vector2,Vector2?,Vector2?,in Vector4?,in Vector4,int)"/>
        [OverloadResolutionPriority(0)]
        public static bool Button(ImTextureId image, Vector2 size, Vector2? uvMinimum = null, Vector2? uvMaximum = null,
            ColorParameter tint = default, Rgba32 backgroundColor = default, int framePadding = -1)
            => Native.Methods.Widgets.ImageButton(image, size, uvMinimum ?? Vector2.Zero, uvMaximum ?? Vector2.One, framePadding,
                backgroundColor.ToVector(), tint.CheckDefault(Rgba32.White).ToVector());

        /// <inheritdoc cref="Button(ImTextureId,Vector2,Vector2?,Vector2?,in Vector4?,in Vector4,int)"/>
        [OverloadResolutionPriority(75)]
        public static bool Button(ImTextureId image, Vector2 size, ColorParameter tint = default, Rgba32 backgroundColor = default,
            int framePadding = -1)
            => Native.Methods.Widgets.ImageButton(image, size, new ImVec2(0, 0), new ImVec2(1, 1), framePadding,
                backgroundColor.ToVector(), tint.CheckDefault(Rgba32.White).ToVector());

        /// <inheritdoc cref="Button(ImTextureId,Vector2,Vector2?,Vector2?,in Vector4?,in Vector4,int)"/>
        [OverloadResolutionPriority(100)]
        public static bool Button(ImTextureId image, Vector2 size, int framePadding = -1)
            => Native.Methods.Widgets.ImageButton(image, size, new ImVec2(0, 0), new ImVec2(1, 1), framePadding,
                new ImVec4(0, 0, 0, 0), new ImVec4(1, 1, 1, 1));
    }
}
