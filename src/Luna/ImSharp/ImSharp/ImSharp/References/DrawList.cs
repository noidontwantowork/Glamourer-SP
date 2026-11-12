#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)

namespace ImSharp;

public static partial class Im
{
    /// <summary> A reference to a single ImGui draw list. </summary>
    /// <param name="pointer"> The native pointer to the draw list. </param>
    /// <remarks> Generally there is one draw list per window. </remarks>
    public readonly unsafe struct DrawList(Native.ImDrawList* pointer)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Native.ImDrawList* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator DrawList(Native.ImDrawList* pointer)
            => new(pointer);

        /// <summary> The list of draw commands in this draw list. </summary>
        public IReadOnlyList<Native.ImDrawCmd> CommandBuffer
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->CommandBuffer;
        }

        /// <summary> The list of vertices in this draw list. </summary>
        public IReadOnlyList<Native.ImDrawVert> VertexBuffer
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->VertexBuffer;
        }

        /// <summary> The list of indices in this draw list. </summary>
        public IReadOnlyList<Native.ImDrawIdx> IndexBuffer
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->IndexBuffer;
        }

        /// <summary> An opaque pointer to the vertex buffer data for use in APIs. </summary>
        public nint VertexBufferData
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => (nint)Pointer->VertexBuffer.Data;
        }

        /// <summary> The size of the vertex buffer in bytes. </summary>
        public uint VertexBufferSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => (uint)(Pointer->VertexBuffer.Size * sizeof(Native.ImDrawVert));
        }

        /// <summary> An opaque pointer to the index buffer data for use in APIs. </summary>
        public nint IndexBufferData
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => (nint)Pointer->IndexBuffer.Data;
        }

        /// <summary> The size of the index buffer in bytes. </summary>
        public uint IndexBufferSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => (uint)(Pointer->IndexBuffer.Size * sizeof(Native.ImDrawIdx));
        }

        /// <summary> Get the index of the current vertex. </summary>
        public uint CurrentVertex
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->VertexCurrentIndex;
        }

        /// <summary> Get the current draw list splitter for this draw list. </summary>
        public DrawListSplitter Splitter
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => new(&Pointer->Splitter, this);
        }

        /// <summary> Get the current clipping rectangle of this draw list. </summary>
        /// <returns> The clipping rectangle. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Rectangle GetClipRect()
            => new(GetClipRectMinimum(), GetClipRectMaximum());

        /// <summary> Get the current minimum point of the clipping rectangle of this draw list. </summary>
        /// <returns> The upper-left corner of the clipping rectangle. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 GetClipRectMinimum()
        {
            ImVec2 vec;
            Native.ImDrawList.GetClipRectMin(&vec, Pointer);
            return vec;
        }

        /// <summary> Get the current maximum point of the clipping rectangle of this draw list. </summary>
        /// <returns> The lower-right corner of the clipping rectangle. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public Vector2 GetClipRectMaximum()
        {
            ImVec2 vec;
            Native.ImDrawList.GetClipRectMax(&vec, Pointer);
            return vec;
        }

        /// <summary> Add geometric shapes to the draw list. </summary>
        public DrawListShapes Shape
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => new(Pointer);
        }

        /// <summary> Add some pre-defined renders to the draw list. </summary>
        public DrawListRender Render
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => new(Pointer);
        }

        /// <summary> Add stateful paths to the draw list. </summary>
        /// <remarks>
        /// Draw a path using <seealso cref="DrawListPath.LineTo"/>, <seealso cref="DrawListPath.ArcTo"/>, <seealso cref="DrawListPath.ArcToFast"/>,
        /// <seealso cref="DrawListPath.Rectangle(in Rectangle,float,ImDrawFlagsRectangle)"/>, <seealso cref="DrawListPath.BezierCubicCurveTo"/>, <seealso cref="DrawListPath.BezierQuadraticCurveTo"/>,
        /// then either draw a line along the path using <seealso cref="DrawListPath.FinishStroke"/> or fill the convex area spanned by the path using <seealso cref="DrawListPath.FinishFillConvex"/>. </remarks>
        public DrawListPath Path
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => new(Pointer);
        }

        /// <summary> Add text using the current font. </summary>
        /// <param name="position"> The start position of the text block (top-left corner) in screen coordinates. </param>
        /// <param name="color"> The color for the text. </param>
        /// <param name="text"> The text input. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Text(Vector2 position, Rgba32 color, Utf8TextHandler text)
            => Native.ImDrawList.AddText(Pointer, position, color.Color, text.Start(out var end), end);

        /// <inheritdoc cref="Text(Vector2,Rgba32,Utf8TextHandler)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Text<T>(Vector2 position, Rgba32 color, ref Utf8StringHandler<T> text) where T : IStringHandlerBuffer
            => Native.ImDrawList.AddText(Pointer, position, color.Color, text.Start(out var end), end);

        /// <summary> Add text using the provided font. </summary>
        /// <param name="font"> A reference to the font to be used. </param>
        /// <param name="fontSize"> The desired size for the font. </param>
        /// <param name="position"> The start position of the text block (top-left corner) in screen coordinates. </param>
        /// <param name="color"> The color for the text. </param>
        /// <param name="text"> The text input. </param>
        /// <param name="wrapWidth"> The wrapping width for the text. Use 0 for no wrapping. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Text(Font font, float fontSize, Vector2 position, Rgba32 color, Utf8TextHandler text, float wrapWidth = 0f)
            => Native.ImDrawList.AddText(Pointer, font.Pointer, fontSize, position, color.Color, text.Start(out var end), end, wrapWidth, null);

        /// <inheritdoc cref="Text(Font,float,Vector2,Rgba32,Utf8TextHandler,float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Text<T>(Font font, float fontSize, Vector2 position, Rgba32 color, ref Utf8StringHandler<T> text, float wrapWidth = 0f)
            where T : IStringHandlerBuffer
            => Native.ImDrawList.AddText(Pointer, font.Pointer, fontSize, position, color.Color, text.Start(out var end), end, wrapWidth, null);

        /// <param name="cpuFineClipRect"> A clipping Rectangle. </param>
        /// <inheritdoc cref="Text(Font,float,Vector2,Rgba32,Utf8TextHandler,float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Text(Font font, float fontSize, Vector2 position, Rgba32 color, Utf8TextHandler text, float wrapWidth,
            Vector4 cpuFineClipRect)
        {
            ImVec4 clipRect = cpuFineClipRect;
            Native.ImDrawList.AddText(Pointer, font.Pointer, fontSize, position, color.Color, text.Start(out var end), end, wrapWidth,
                &clipRect);
        }

        /// <summary> Draw text clipped to a specific rectangle. </summary>
        /// <param name="upperLeftCorner"> The upper left corner of the clipping rectangle in screen coordinates. </param>
        /// <param name="lowerRightCorner"> The lower right corner of the clipping rectangle in screen coordinates. </param>
        /// <param name="text"> The text. Does not have to be null-terminated. </param>
        /// <param name="knownTextSize"> The size of the text if it is already known. Leave default if not known. </param>
        /// <param name="alignment"> The alignment of the text in the rectangle within [0,1]x[0,1]. Default is (0, 0). </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void TextClipped(Vector2 upperLeftCorner, Vector2 lowerRightCorner, Utf8TextHandler text, Vector2? knownTextSize = null,
            Vector2 alignment = default)
        {
            if (knownTextSize.HasValue)
            {
                ImVec2 size = knownTextSize.Value;
                Native.Methods.Internal.RenderTextClippedEx(Pointer, upperLeftCorner, lowerRightCorner, text.Start(out var end), end, &size,
                    alignment, null);
            }
            else
            {
                Native.Methods.Internal.RenderTextClippedEx(Pointer, upperLeftCorner, lowerRightCorner, text.Start(out var end), end, null,
                    alignment, null);
            }
        }

        /// <inheritdoc cref="TextClipped(Vector2,Vector2,Utf8TextHandler,Vector2?,Vector2)"/>
        /// <param name="rectangle"> The clipping rectangle in screen coordinates. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void TextClipped(in Rectangle rectangle, Utf8TextHandler text, Vector2? knownTextSize = null, Vector2 alignment = default)
        {
            if (knownTextSize.HasValue)
            {
                ImVec2 size = knownTextSize.Value;
                Native.Methods.Internal.RenderTextClippedEx(Pointer, rectangle.Minimum, rectangle.Maximum, text.Start(out var end), end, &size,
                    alignment, null);
            }
            else
            {
                Native.Methods.Internal.RenderTextClippedEx(Pointer, rectangle.Minimum, rectangle.Maximum, text.Start(out var end), end, null,
                    alignment, null);
            }
        }

        /// <inheritdoc cref="TextClipped(Vector2,Vector2,Utf8TextHandler,Vector2?,Vector2)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void TextClipped<T>(Vector2 upperLeftCorner, Vector2 lowerRightCorner, ref Utf8StringHandler<T> text, Vector2? knownTextSize,
            Vector2 alignment = default) where T : IStringHandlerBuffer
        {
            if (knownTextSize.HasValue)
            {
                ImVec2 size = knownTextSize.Value;
                Native.Methods.Internal.RenderTextClippedEx(Pointer, upperLeftCorner, lowerRightCorner, text.Start(out var end), end, &size,
                    alignment, null);
            }
            else
            {
                Native.Methods.Internal.RenderTextClippedEx(Pointer, upperLeftCorner, lowerRightCorner, text.Start(out var end), end, null,
                    alignment, null);
            }
        }

        /// <inheritdoc cref="TextClipped(in Rectangle,Utf8TextHandler,Vector2?,Vector2)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void TextClipped<T>(in Rectangle rectangle, ref Utf8StringHandler<T> text, Vector2? knownTextSize = null,
            Vector2 alignment = default)
            where T : IStringHandlerBuffer
        {
            if (knownTextSize.HasValue)
            {
                ImVec2 size = knownTextSize.Value;
                Native.Methods.Internal.RenderTextClippedEx(Pointer, rectangle.Minimum, rectangle.Maximum, text.Start(out var end), end, &size,
                    alignment, null);
            }
            else
            {
                Native.Methods.Internal.RenderTextClippedEx(Pointer, rectangle.Minimum, rectangle.Maximum, text.Start(out var end), end, null,
                    alignment, null);
            }
        }

        /// <summary> Add a rectangular texture by its ID. </summary>
        /// <param name="userTextureId"> The texture ID. </param>
        /// <param name="minimum"> The upper-left corner of the image in screen coordinates. </param>
        /// <param name="maximum"> The lower-right corner of the image in screen coordinates. </param>
        /// <param name="uvMinimum"> The normalized texture coordinate for the upper-left corner, usually (0, 0). </param>
        /// <param name="uvMaximum"> The normalized texture coordinate for the lower-right corner, usually (1, 1). </param>
        /// <param name="color"> A tint. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Image(ImTextureId userTextureId, Vector2 minimum, Vector2 maximum, Vector2 uvMinimum, Vector2 uvMaximum, Rgba32 color)
            => Native.ImDrawList.AddImage(Pointer, userTextureId, minimum, maximum, uvMinimum, uvMaximum, color.Color);

        /// <param name="rectangle"> The rectangle to draw the image in screen coordinates. </param>
        /// <inheritdoc cref="Image(ImTextureId,Vector2,Vector2,Vector2,Vector2,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Image(ImTextureId userTextureId, in Rectangle rectangle, Vector2 uvMinimum, Vector2 uvMaximum, Rgba32 color)
            => Native.ImDrawList.AddImage(Pointer, userTextureId, rectangle.Minimum, rectangle.Maximum, uvMinimum, uvMaximum, color.Color);

        /// <param name="uvRectangle"> The rectangle of the normalized texture coordinate, usually (0, 0) to (1, 1). </param>
        /// <inheritdoc cref="Image(ImTextureId,in Rectangle,Vector2,Vector2,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Image(ImTextureId userTextureId, in Rectangle rectangle, in Rectangle uvRectangle, Rgba32 color)
            => Native.ImDrawList.AddImage(Pointer, userTextureId, rectangle.Minimum, rectangle.Maximum, uvRectangle.Minimum,
                uvRectangle.Maximum,
                color.Color);

        /// <inheritdoc cref="Image(ImTextureId,Vector2,Vector2,Vector2,Vector2,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Image(ImTextureId userTextureId, Vector2 minimum, Vector2 maximum, Vector2 uvMinimum, Vector2 uvMaximum)
            => Native.ImDrawList.AddImage(Pointer, userTextureId, minimum, maximum, uvMinimum, uvMaximum, 0xFFFFFFFF);

        /// <param name="rectangle"> The rectangle to draw the image in screen coordinates. </param>
        /// <inheritdoc cref="Image(ImTextureId,Vector2,Vector2,Vector2,Vector2,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Image(ImTextureId userTextureId, in Rectangle rectangle, Vector2 uvMinimum, Vector2 uvMaximum)
            => Native.ImDrawList.AddImage(Pointer, userTextureId, rectangle.Minimum, rectangle.Maximum, uvMinimum, uvMaximum, 0xFFFFFFFF);

        /// <param name="uvRectangle"> The rectangle of the normalized texture coordinate, usually (0, 0) to (1, 1). </param>
        /// <inheritdoc cref="Image(ImTextureId,in Rectangle,Vector2,Vector2,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Image(ImTextureId userTextureId, in Rectangle rectangle, in Rectangle uvRectangle)
            => Native.ImDrawList.AddImage(Pointer, userTextureId, rectangle.Minimum, rectangle.Maximum, uvRectangle.Minimum,
                uvRectangle.Maximum, 0xFFFFFFFF);

        /// <inheritdoc cref="Image(ImTextureId,in Rectangle,Vector2,Vector2,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Image(ImTextureId userTextureId, in Rectangle rectangle)
            => Native.ImDrawList.AddImage(Pointer, userTextureId, rectangle.Minimum, rectangle.Maximum, new ImVec2(0, 0), new ImVec2(1, 1),
                0xFFFFFFFF);

        /// <inheritdoc cref="Image(ImTextureId,Vector2,Vector2,Vector2,Vector2,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Image(ImTextureId userTextureId, Vector2 minimum, Vector2 maximum)
            => Native.ImDrawList.AddImage(Pointer, userTextureId, minimum, maximum, new ImVec2(0, 0), new ImVec2(1, 1), 0xFFFFFFFF);

        /// <inheritdoc cref="Image(ImTextureId,in Rectangle,Vector2,Vector2,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Image(ImTextureId userTextureId, in Rectangle rectangle, Rgba32 color)
            => Native.ImDrawList.AddImage(Pointer, userTextureId, rectangle.Minimum, rectangle.Maximum, new ImVec2(0, 0), new ImVec2(1, 1),
                color.Color);

        /// <inheritdoc cref="Image(ImTextureId,Vector2,Vector2,Vector2,Vector2,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Image(ImTextureId userTextureId, Vector2 minimum, Vector2 maximum, Rgba32 color)
            => Native.ImDrawList.AddImage(Pointer, userTextureId, minimum, maximum, new ImVec2(0, 0), new ImVec2(1, 1), color.Color);

        /// <summary> Add a quadrilateral texture by its ID. </summary>
        /// <param name="userTextureId"> The texture ID. </param>
        /// <param name="topLeft"> The top-left corner of the quadrilateral in screen coordinates. </param>
        /// <param name="topRight"> The top-right corner of the quadrilateral in screen coordinates. </param>
        /// <param name="bottomRight"> The bottom-right corner of the quadrilateral in screen coordinates. </param>
        /// <param name="bottomLeft"> The bottom-left corner of the quadrilateral in screen coordinates. </param>
        /// <param name="uvTopLeft"> The normalized texture coordinate for the top-left corner, usually (0, 0). </param>
        /// <param name="uvTopRight"> The normalized texture coordinate for the top-right corner, usually (1, 0). </param>
        /// <param name="uvBottomRight"> The normalized texture coordinate for the bottom-right corner, usually (1, 1). </param>
        /// <param name="uvBottomLeft"> The normalized texture coordinate for the bottom-left corner, usually (0, 1). </param>
        /// <param name="color"> A color tint. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void ImageQuad(ImTextureId userTextureId, Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft,
            Vector2 uvTopLeft, Vector2 uvTopRight, Vector2 uvBottomRight, Vector2 uvBottomLeft, Rgba32 color)
            => Native.ImDrawList.AddImageQuad(Pointer, userTextureId, topLeft, topRight, bottomRight, bottomLeft, uvTopLeft, uvTopRight,
                uvBottomRight, uvBottomLeft, color.Color);

        /// <inheritdoc cref="ImageQuad(ImTextureId,Vector2,Vector2,Vector2,Vector2,Vector2,Vector2,Vector2,Vector2,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void ImageQuad(ImTextureId userTextureId, Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft,
            Rgba32 color)
            => Native.ImDrawList.AddImageQuad(Pointer, userTextureId, topLeft, topRight, bottomRight, bottomLeft, new ImVec2(0, 0),
                new ImVec2(1, 0), new ImVec2(1, 1), new ImVec2(0, 1), color.Color);

        /// <inheritdoc cref="ImageQuad(ImTextureId,Vector2,Vector2,Vector2,Vector2,Vector2,Vector2,Vector2,Vector2,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void ImageQuad(ImTextureId userTextureId, Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft,
            Vector2 uvTopLeft, Vector2 uvTopRight, Vector2 uvBottomRight, Vector2 uvBottomLeft)
            => Native.ImDrawList.AddImageQuad(Pointer, userTextureId, topLeft, topRight, bottomRight, bottomLeft, uvTopLeft, uvTopRight,
                uvBottomRight, uvBottomLeft, 0xFFFFFFFF);

        /// <summary> Add a rectangular texture with rounded corners by its ID. </summary>
        /// <param name="userTextureId"> The texture ID. </param>
        /// <param name="minimum"> The upper-left corner of the image in screen coordinates. </param>
        /// <param name="maximum"> The lower-right corner of the image in screen coordinates. </param>
        /// <param name="uvMinimum"> The normalized texture coordinate for the upper-left corner, usually (0, 0). </param>
        /// <param name="uvMaximum"> The normalized texture coordinate for the lower-right corner, usually (1, 1). </param>
        /// <param name="color"> A tint. </param>
        /// <param name="rounding"> The amount of rounding in pixels. </param>
        /// <param name="flags"> Which corners to round. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void ImageRounded(ImTextureId userTextureId, Vector2 minimum, Vector2 maximum, Vector2 uvMinimum, Vector2 uvMaximum,
            Rgba32 color,
            float rounding, ImDrawFlagsRectangle flags = ImDrawFlagsRectangle.None)
            => Native.ImDrawList.AddImageRounded(Pointer, userTextureId, minimum, maximum, uvMinimum, uvMaximum, color.Color, rounding, flags);

        /// <param name="rectangle"> The rectangle to draw the image in screen coordinates. </param>
        /// <inheritdoc cref="ImageRounded(ImTextureId,Vector2,Vector2,Vector2,Vector2,Rgba32,float,ImDrawFlagsRectangle)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void ImageRounded(ImTextureId userTextureId, in Rectangle rectangle, Vector2 uvMinimum, Vector2 uvMaximum, Rgba32 color,
            float rounding, ImDrawFlagsRectangle flags = ImDrawFlagsRectangle.None)
            => Native.ImDrawList.AddImageRounded(Pointer, userTextureId, rectangle.Minimum, rectangle.Maximum, uvMinimum, uvMaximum,
                color.Color,
                rounding, flags);

        /// <param name="uvRectangle"> The rectangle of the normalized texture coordinate, usually (0, 0) to (1, 1). </param>
        /// <inheritdoc cref="ImageRounded(ImTextureId,in Rectangle,Vector2,Vector2,Rgba32,float,ImDrawFlagsRectangle)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void ImageRounded(ImTextureId userTextureId, in Rectangle rectangle, in Rectangle uvRectangle, Rgba32 color, float rounding,
            ImDrawFlagsRectangle flags = ImDrawFlagsRectangle.None)
            => Native.ImDrawList.AddImageRounded(Pointer, userTextureId, rectangle.Minimum, rectangle.Maximum, uvRectangle.Minimum,
                uvRectangle.Maximum,
                color.Color, rounding, flags);

        /// <inheritdoc cref="ImageRounded(ImTextureId,in Rectangle,Vector2,Vector2,Rgba32,float,ImDrawFlagsRectangle)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void ImageRounded(ImTextureId userTextureId, in Rectangle rectangle, float rounding,
            ImDrawFlagsRectangle flags = ImDrawFlagsRectangle.None)
            => Native.ImDrawList.AddImageRounded(Pointer, userTextureId, rectangle.Minimum, rectangle.Maximum, new ImVec2(0, 0),
                new ImVec2(1,                                                                                                1),
                0xFFFFFFFF, rounding, flags);

        /// <inheritdoc cref="ImageRounded(ImTextureId,Vector2,Vector2,Vector2,Vector2,Rgba32,float,ImDrawFlagsRectangle)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void ImageRounded(ImTextureId userTextureId, Vector2 minimum, Vector2 maximum, float rounding,
            ImDrawFlagsRectangle flags = ImDrawFlagsRectangle.None)
            => Native.ImDrawList.AddImageRounded(Pointer, userTextureId, minimum, maximum, new ImVec2(0, 0), new ImVec2(1, 1), 0xFFFFFFFF,
                rounding,
                flags);

        /// <inheritdoc cref="ImageRounded(ImTextureId,in Rectangle,Vector2,Vector2,Rgba32,float,ImDrawFlagsRectangle)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void ImageRounded(ImTextureId userTextureId, in Rectangle rectangle, Rgba32 color, float rounding,
            ImDrawFlagsRectangle flags = ImDrawFlagsRectangle.None)
            => Native.ImDrawList.AddImageRounded(Pointer, userTextureId, rectangle.Minimum, rectangle.Maximum, new ImVec2(0, 0),
                new ImVec2(1,                                                                                                1),
                color.Color, rounding, flags);

        /// <inheritdoc cref="ImageRounded(ImTextureId,Vector2,Vector2,Vector2,Vector2,Rgba32,float,ImDrawFlagsRectangle)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void ImageRounded(ImTextureId userTextureId, Vector2 minimum, Vector2 maximum, Rgba32 color, float rounding,
            ImDrawFlagsRectangle flags = ImDrawFlagsRectangle.None)
            => Native.ImDrawList.AddImageRounded(Pointer, userTextureId, minimum, maximum, new ImVec2(0, 0), new ImVec2(1, 1), color.Color,
                rounding, flags);

        /// <inheritdoc cref="DrawListClipRectDisposable.Push(Vector2,Vector2,bool,bool)"/>
        public DrawListClipRectDisposable PushClipRect(Vector2 minimum, Vector2 maximum, bool intersectWithCurrentClipRect = false,
            bool condition = true)
            => new DrawListClipRectDisposable(Pointer).Push(minimum, maximum, intersectWithCurrentClipRect, condition);

        /// <inheritdoc cref="DrawListClipRectDisposable.Push(in Rectangle,bool,bool)"/>
        public DrawListClipRectDisposable PushClipRect(in Rectangle rectangle, bool intersectWithCurrentClipRect = false, bool condition = true)
            => new DrawListClipRectDisposable(Pointer).Push(rectangle, intersectWithCurrentClipRect, condition);

        /// <inheritdoc cref="DrawListClipRectDisposable.PushFullScreen(bool)"/>
        public DrawListClipRectDisposable PushClipRectFullScreen(bool condition = true)
            => new DrawListClipRectDisposable(Pointer).PushFullScreen(condition);

        /// <summary> Pop any number of ClipRects pushed into this draw list. </summary>
        /// <param name="count"> The unchecked number of ClipRects to pop. </param>
        /// <remarks> Avoid using this function, and ClipRects across scopes, as much as possible. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void PopClipRectUnsafe(int count = 1)
        {
            while (count-- > 0)
                Native.ImDrawList.PopClipRect(Pointer);
        }

        public readonly ref struct DrawListRender(Native.ImDrawList* pointer)
        {
            public void Arrow(Vector2 position, Rgba32 color, Direction direction, float scale)
                => Native.Methods.Internal.RenderArrow(pointer, position, color, direction, scale);

            public void Bullet(Vector2 position, Rgba32 color)
                => Native.Methods.Internal.RenderBullet(pointer, position, color);

            public void Checkmark(Vector2 position, Rgba32 color, float size)
                => Native.Methods.Internal.RenderCheckMark(pointer, position, color, size);
        }

        /// <inheritdoc cref="Drawing.ForegroundDrawList"/>
        public static DrawList Foreground
            => Drawing.ForegroundDrawList;

        /// <inheritdoc cref="Drawing.BackgroundDrawList"/>
        public static DrawList Background
            => Drawing.BackgroundDrawList;

        /// <inheritdoc cref="Im.Window.DrawList"/>
        public static DrawList Window
            => Im.Window.DrawList;

        /// <summary> Wrapper for all functions adding shapes. </summary>
        /// <param name="pointer"> The native pointer to the draw list. </param>
        public readonly ref struct DrawListShapes(Native.ImDrawList* pointer)
        {
            /// <summary> Add a line. </summary>
            /// <param name="startPoint"> The start point of the line in screen coordinates. </param>
            /// <param name="endPoint"> The end point of the line in screen coordinates. </param>
            /// <param name="color"> The color of the line, default is white. </param>
            /// <param name="thickness"> The thickness of the line in pixels. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void Line(Vector2 startPoint, Vector2 endPoint, ColorParameter color = default, float thickness = 1)
                => Native.ImDrawList.AddLine(pointer, startPoint, endPoint, color.CheckDefault(Rgba32.White), thickness);

            /// <summary> Add a hollow rectangle. </summary>
            /// <param name="minimum"> The top-left corner of the rectangle in screen coordinates. </param>
            /// <param name="maximum"> The bottom-right corner of the rectangle in screen coordinates. </param>
            /// <param name="color"> The color of the lines, default is white. </param>
            /// <param name="rounding"> The rounding of the rectangles corners in pixels. </param>
            /// <param name="flags"> Which corners to round. </param>
            /// <param name="thickness"> The thickness of the lines in pixels. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void Rectangle(Vector2 minimum, Vector2 maximum, ColorParameter color = default, float rounding = 0,
                ImDrawFlagsRectangle flags = ImDrawFlagsRectangle.None, float thickness = 1)
                => Native.ImDrawList.AddRect(pointer, minimum, maximum, color.CheckDefault(Rgba32.White), rounding, flags, thickness);

            /// <param name="rectangle"> The rectangle in screen coordinates. </param>
            /// <inheritdoc cref="Rectangle(Vector2,Vector2,ColorParameter,float,ImDrawFlagsRectangle,float)"/>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void Rectangle(in Rectangle rectangle, ColorParameter color = default, float rounding = 0,
                ImDrawFlagsRectangle flags = ImDrawFlagsRectangle.None, float thickness = 1)
                => Native.ImDrawList.AddRect(pointer, rectangle.Minimum, rectangle.Maximum, color.CheckDefault(Rgba32.White), rounding, flags,
                    thickness);

            /// <summary> Add a filled rectangle. </summary>
            /// <param name="minimum"> The top-left corner of the rectangle in screen coordinates. </param>
            /// <param name="maximum"> The bottom-right corner of the rectangle in screen coordinates. </param>
            /// <param name="color"> The color of the rectangle, default is white. </param>
            /// <param name="rounding"> The rounding of the rectangles corners in pixels. </param>
            /// <param name="flags"> Which corners to round. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void RectangleFilled(Vector2 minimum, Vector2 maximum, ColorParameter color = default, float rounding = 0,
                ImDrawFlagsRectangle flags = ImDrawFlagsRectangle.None)
                => Native.ImDrawList.AddRectFilled(pointer, minimum, maximum, color.CheckDefault(Rgba32.White), rounding, flags);

            /// <param name="rectangle"> The rectangle in screen coordinates. </param>
            /// <inheritdoc cref="RectangleFilled(Vector2,Vector2,ColorParameter,float,ImDrawFlagsRectangle)"/>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void RectangleFilled(in Rectangle rectangle, ColorParameter color = default, float rounding = 0,
                ImDrawFlagsRectangle flags = ImDrawFlagsRectangle.None)
                => Native.ImDrawList.AddRectFilled(pointer, rectangle.Minimum, rectangle.Maximum, color.CheckDefault(Rgba32.White), rounding,
                    flags);

            /// <summary> Add a filled rectangle with color gradients generated from the corner colors. </summary>
            /// <param name="minimum"> The top-left corner of the rectangle in screen coordinates. </param>
            /// <param name="maximum"> The bottom-right corner of the rectangle in screen coordinates. </param>
            /// <param name="colorUpperLeft"> The color in the top-left corner. </param>
            /// <param name="colorUpperRight"> The color in the top-right corner. </param>
            /// <param name="colorBottomRight"> The color in the bottom-right corner. </param>
            /// <param name="colorBottomLeft"> The color in the bottom-left corner. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void RectangleMulticolor(Vector2 minimum, Vector2 maximum, Rgba32 colorUpperLeft, Rgba32 colorUpperRight,
                Rgba32 colorBottomRight, Rgba32 colorBottomLeft)
                => Native.ImDrawList.AddRectFilledMultiColor(pointer, minimum, maximum, colorUpperLeft.Color, colorUpperRight.Color,
                    colorBottomRight.Color, colorBottomLeft.Color);

            /// <param name="rectangle"> The rectangle in screen coordinates. </param>
            /// <inheritdoc cref="RectangleMulticolor(Vector2,Vector2,Rgba32,Rgba32,Rgba32,Rgba32)"/>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void RectangleMulticolor(in Rectangle rectangle, Rgba32 colorUpperLeft, Rgba32 colorUpperRight, Rgba32 colorBottomRight,
                Rgba32 colorBottomLeft)
                => Native.ImDrawList.AddRectFilledMultiColor(pointer, rectangle.Minimum, rectangle.Maximum, colorUpperLeft.Color,
                    colorUpperRight.Color, colorBottomRight.Color, colorBottomLeft.Color);

            /// <summary> Add a hollow quadrilateral. </summary>
            /// <param name="topLeft"> The top-left corner of the quadrilateral in screen coordinates. </param>
            /// <param name="topRight"> The top-right corner of the quadrilateral in screen coordinates. </param>
            /// <param name="bottomRight"> The bottom-right corner of the quadrilateral in screen coordinates. </param>
            /// <param name="bottomLeft"> The bottom-left corner of the quadrilateral in screen coordinates. </param>
            /// <param name="color"> The color of the lines, default is white. </param>
            /// <param name="thickness"> The thickness of the lines in pixels. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void Quad(Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft,
                ColorParameter color = default, float thickness = 1)
                => Native.ImDrawList.AddQuad(pointer, topLeft, topRight, bottomRight, bottomLeft, color.CheckDefault(Rgba32.White), thickness);

            /// <summary> Add a filled quadrilateral. </summary>
            /// <param name="topLeft"> The top-left corner of the quadrilateral in screen coordinates. </param>
            /// <param name="topRight"> The top-right corner of the quadrilateral in screen coordinates. </param>
            /// <param name="bottomRight"> The bottom-right corner of the quadrilateral in screen coordinates. </param>
            /// <param name="bottomLeft"> The bottom-left corner of the quadrilateral in screen coordinates. </param>
            /// <param name="color"> The color of the quadrilateral, default is white. </param>
            /// <remarks> Ensure that the corners are given clockwise, the anti-aliasing fringe depends on it. </remarks>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void QuadFilled(Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft, ColorParameter color = default)
                => Native.ImDrawList.AddQuadFilled(pointer, topLeft, topRight, bottomRight, bottomLeft, color.CheckDefault(Rgba32.White));

            /// <summary> Add a hollow triangle. </summary>
            /// <param name="point1"> The first corner of the triangle in screen coordinates. </param>
            /// <param name="point2"> The second corner of the triangle in screen coordinates. </param>
            /// <param name="point3"> The third corner of the triangle in screen coordinates. </param>
            /// <param name="color"> The color of the lines, default is white. </param>
            /// <param name="thickness"> The thickness of the lines in pixels. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void Triangle(Vector2 point1, Vector2 point2, Vector2 point3, ColorParameter color = default, float thickness = 1)
                => Native.ImDrawList.AddTriangle(pointer, point1, point2, point3, color.CheckDefault(Rgba32.White), thickness);

            /// <summary> Add a filled triangle. </summary>
            /// <param name="point1"> The first corner of the triangle in screen coordinates. </param>
            /// <param name="point2"> The second corner of the triangle in screen coordinates. </param>
            /// <param name="point3"> The third corner of the triangle in screen coordinates. </param>
            /// <param name="color"> The color of the lines, default is white. </param>
            /// <remarks> Ensure that the corners are given clockwise, the anti-aliasing fringe depends on it. </remarks>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void TriangleFilled(Vector2 point1, Vector2 point2, Vector2 point3, ColorParameter color = default)
                => Native.ImDrawList.AddTriangleFilled(pointer, point1, point2, point3, color.CheckDefault(Rgba32.White));

            /// <summary> Add a hollow circle. </summary>
            /// <param name="center"> The center point of the circle in screen coordinates. </param>
            /// <param name="radius"> The radius of the circle. </param>
            /// <param name="color"> The color of the line, default is white. </param>
            /// <param name="thickness"> The thickness of the line in pixels. </param>
            /// <param name="numSegments"> Use 0 to automatically calculate tesselation (recommended) or specify the number of segments. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void Circle(Vector2 center, float radius, ColorParameter color = default, float thickness = 1,
                int numSegments = 0)
                => Native.ImDrawList.AddCircle(pointer, center, radius, color.CheckDefault(Rgba32.White), numSegments, thickness);

            /// <summary> Add a filled circle. </summary>
            /// <param name="center"> The center point of the circle in screen coordinates. </param>
            /// <param name="radius"> The radius of the circle. </param>
            /// <param name="color"> The color of the circle, default is white. </param>
            /// <param name="numSegments"> Use 0 to automatically calculate tesselation (recommended) or specify the number of segments. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void CircleFilled(Vector2 center, float radius, ColorParameter color = default, int numSegments = 0)
                => Native.ImDrawList.AddCircleFilled(pointer, center, radius, color.CheckDefault(Rgba32.White), numSegments);

            /// <summary> Add a hollow regular n-gon (a shape with n equal sides and equal angles between them). </summary>
            /// <param name="center"> The center point of the n-gon in screen coordinates. </param>
            /// <param name="radius"> The distance to the corner points. </param>
            /// <param name="numSegments"> The n in the n-gon.</param>
            /// <param name="color"> The color of the lines, default is white. </param>
            /// <param name="thickness"> The thickness of the lines in pixels. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void NGon(Vector2 center, float radius, int numSegments, ColorParameter color = default, float thickness = 1)
                => Native.ImDrawList.AddNgon(pointer, center, radius, color.CheckDefault(Rgba32.White), numSegments, thickness);

            /// <summary> Add a filled regular n-gon (a shape with n equal sides and equal angles between them). </summary>
            /// <param name="center"> The center point of the n-gon in screen coordinates. </param>
            /// <param name="radius"> The distance to the corner points. </param>
            /// <param name="color"> The color of the n-gon, default is white. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void NGonFilled(Vector2 center, float radius, int numSegments, ColorParameter color = default)
                => Native.ImDrawList.AddNgonFilled(pointer, center, radius, color.CheckDefault(Rgba32.White), numSegments);

            /// <summary> Add multiple lines connecting a set of points. </summary>
            /// <param name="points"> A set of points in screen coordinates. </param>
            /// <param name="color"> The color of the lines, default is white. </param>
            /// <param name="flags"> Whether the last point should connect back to the first. </param>
            /// <param name="thickness"> The thickness of the lines in pixels. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void Polyline(ReadOnlySpan<Vector2> points, ColorParameter color = default, ImDrawFlagsPath flags = ImDrawFlagsPath.None,
                float thickness = 1)
            {
                fixed (Vector2* ptr = points)
                {
                    Native.ImDrawList.AddPolyline(pointer, (ImVec2*)ptr, points.Length, color.CheckDefault(Rgba32.White), flags, thickness);
                }
            }

            /// <summary> Fill the space between multiple points interpreted as a convex, irregular polygon. </summary>
            /// <param name="points"> A set of points in screen coordinates. </param>
            /// <param name="color"> The color of the convex polygon, default is white. </param>
            /// <remarks> Ensure that the corners are given clockwise, the anti-aliasing fringe depends on it. </remarks>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void ConvexPolygonFilled(ReadOnlySpan<Vector2> points, ColorParameter color = default)
            {
                fixed (Vector2* ptr = points)
                {
                    Native.ImDrawList.AddConvexPolyFilled(pointer, (ImVec2*)ptr, points.Length, color.CheckDefault(Rgba32.White));
                }
            }

            /// <summary> Add a cubic Bézier curve spanned by four points. </summary>
            /// <param name="point1"> The first point in screen coordinates. </param>
            /// <param name="point2"> The second point in screen coordinates. </param>
            /// <param name="point3"> The third point in screen coordinates. </param>
            /// <param name="point4"> The fourth point in screen coordinates. </param>
            /// <param name="color"> The color of the curve, default is white. </param>
            /// <param name="thickness"> The thickness of the curve in pixels. </param>
            /// <param name="numSegments"> Use 0 to automatically calculate tesselation (recommended) or specify the number of segments </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void BezierCubic(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4,
                ColorParameter color = default, float thickness = 1, int numSegments = 0)
                => Native.ImDrawList.AddBezierCubic(pointer, point1, point2, point3, point4, color.CheckDefault(Rgba32.White), thickness,
                    numSegments);

            /// <summary> Add a quadratic Bézier curve spanned by three points. </summary>
            /// <param name="point1"> The first point in screen coordinates. </param>
            /// <param name="point2"> The second point in screen coordinates. </param>
            /// <param name="point3"> The third point in screen coordinates. </param>
            /// <param name="color"> The color of the curve, default is white. </param>
            /// <param name="thickness"> The thickness of the curve in pixels. </param>
            /// <param name="numSegments"> Use 0 to automatically calculate tesselation (recommended) or specify the number of segments </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void BezierQuadratic(Vector2 point1, Vector2 point2, Vector2 point3,
                ColorParameter color = default, float thickness = 1, int numSegments = 0)
                => Native.ImDrawList.AddBezierQuadratic(pointer, point1, point2, point3, color.CheckDefault(Rgba32.White), thickness,
                    numSegments);
        }

        public readonly ref struct DrawListPath(Native.ImDrawList* pointer)
        {
            /// <summary> Add a from the current position line to a new point. </summary>
            /// <param name="position"> The new position in screen coordinates. </param>
            /// <remarks> If this is the first point in a new path, this just sets the start position. </remarks>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public DrawListPath LineTo(Vector2 position)
            {
                Native.ImDrawList.PathLineTo(pointer, position);
                return this;
            }

            /// <summary> Add a from the current position line to a new point, but merge it with the prior point if it is the same. </summary>
            /// <param name="position"> The new position in screen coordinates. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public DrawListPath LineToMergeDuplicate(Vector2 position)
            {
                Native.ImDrawList.PathLineToMergeDuplicate(pointer, position);
                return this;
            }

            /// <summary> Add an arc as a section of the given circle. </summary>
            /// <param name="center"> The center of the circle in screen coordinates. </param>
            /// <param name="radius"> The radius of the circle in pixels. </param>
            /// <param name="minimumAngle"> The start angle of the circle segment to draw. </param>
            /// <param name="maximumAngle"> The end angle of the circle segment to draw. </param>
            /// <param name="numSegments"> Use 0 to automatically calculate tesselation (recommended) or specify the number of segments </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public DrawListPath ArcTo(Vector2 center, float radius, float minimumAngle, float maximumAngle, int numSegments = 0)
            {
                Native.ImDrawList.PathArcTo(pointer, center, radius, minimumAngle, maximumAngle, numSegments);
                return this;
            }

            /// <summary> Add a more efficient arc as a section of the given circle given by clock positions. </summary>
            /// <param name="center"> The center of the circle in screen coordinates. </param>
            /// <param name="radius"> The radius of the circle in pixels. </param>
            /// <param name="minimumClockPosition"> The start clock position of the circle segment in [0, 11]. </param>
            /// <param name="maximumClockPosition"> The end clock position of the circle segment in [0, 11]. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public DrawListPath ArcToFast(Vector2 center, float radius, int minimumClockPosition, int maximumClockPosition)
            {
                Native.ImDrawList.PathArcToFast(pointer, center, radius, minimumClockPosition, maximumClockPosition);
                return this;
            }

            /// <summary> Add a cubic Bézier curve from the current position using three other points. </summary>
            /// <param name="point2"> The second control point in screen coordinates. </param>
            /// <param name="point3"> The third control point in screen coordinates. </param>
            /// <param name="point4"> The fourth control point in screen coordinates. </param>
            /// <param name="numSegments"> Use 0 to automatically calculate tesselation (recommended) or specify the number of segments </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public DrawListPath BezierCubicCurveTo(Vector2 point2, Vector2 point3, Vector2 point4, int numSegments = 0)
            {
                Native.ImDrawList.PathBezierCubicCurveTo(pointer, point2, point3, point4, numSegments);
                return this;
            }

            /// <summary> Add a quadratic Bézier curve from the current position using two other points. </summary>
            /// <param name="point2"> The second control point in screen coordinates. </param>
            /// <param name="point3"> The third control point in screen coordinates. </param>
            /// <param name="numSegments"> Use 0 to automatically calculate tesselation (recommended) or specify the number of segments </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public DrawListPath BezierQuadraticCurveTo(Vector2 point2, Vector2 point3, int numSegments = 0)
            {
                Native.ImDrawList.PathBezierQuadraticCurveTo(pointer, point2, point3, numSegments);
                return this;
            }

            /// <summary> Add a rectangle. </summary>
            /// <param name="minimum"> The top-left corner of the rectangle in screen coordinates. </param>
            /// <param name="maximum"> The bottom-right corner of the rectangle in screen coordinates. </param>
            /// <param name="rounding"> The rounding of the corners in pixels. </param>
            /// <param name="flags"> Which corners to round. </param>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public DrawListPath Rectangle(Vector2 minimum, Vector2 maximum, float rounding = 0,
                ImDrawFlagsRectangle flags = ImDrawFlagsRectangle.None)
            {
                Native.ImDrawList.PathRect(pointer, minimum, maximum, rounding, flags);
                return this;
            }

            /// <param name="rectangle"> The rectangle in screen coordinates. </param>
            /// <inheritdoc cref="Rectangle(Vector2,Vector2,float,ImDrawFlagsRectangle)"/>>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public DrawListPath Rectangle(in Rectangle rectangle, float rounding = 0, ImDrawFlagsRectangle flags = ImDrawFlagsRectangle.None)
            {
                Native.ImDrawList.PathRect(pointer, rectangle.Minimum, rectangle.Maximum, rounding, flags);
                return this;
            }

            /// <summary> Connect the current path and draw the actual lines. </summary>
            /// <param name="color"> The color of the lines, default is white. </param>
            /// <param name="flags"> Whether the last point of the path should connect back to the first point. </param>
            /// <param name="thickness"> The thickness of the lines in pixels. </param>
            /// <remarks> This clears the current path. </remarks>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void FinishStroke(ColorParameter color = default, ImDrawFlagsPath flags = 0, float thickness = 1)
                => Native.ImDrawList.PathStroke(pointer, color.CheckDefault(Rgba32.White), flags, thickness);

            /// <summary> Fill the convex area spanned between the current points. </summary>
            /// <param name="color"> The color of the shape, default is white. </param>
            /// <remarks> This clears the current path. </remarks>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void FinishFillConvex(ColorParameter color = default)
                => Native.ImDrawList.PathFillConvex(pointer, color.CheckDefault(Rgba32.White));

            /// <summary> Clear the current path without drawing anything. </summary>
            [MethodImpl(ImSharpConfiguration.OptInl)]
            public void Clear()
                => Native.ImDrawList.PathClear(pointer);
        }
    }
}
