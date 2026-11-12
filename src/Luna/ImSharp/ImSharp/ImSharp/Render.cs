using ImSharp.Internal;

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper for render utility functions. Those usually reference ImGui internals. </summary>
    public static class Render
    {
        /// <summary> Render a frame. </summary>
        /// <param name="upperLeftCorner"> The upper left corner of the frame's rectangle in screen coordinates. </param>
        /// <param name="lowerRightCorner"> The lower right corner of the frame's rectangle in screen coordinates. </param>
        /// <param name="backgroundColor"> The background (main) color of the frame. </param>
        /// <param name="rounding"> The rounding of the frame's rectangle. </param>
        /// <param name="borderColor"> The color of the border of the frame. If this is transparent, no border is drawn. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void Frame(Vector2 upperLeftCorner, Vector2 lowerRightCorner, Rgba32 backgroundColor, float rounding,
            Rgba32 borderColor = default)
        {
            if (borderColor.IsTransparent || Style.FrameBorderThickness <= 0)
            {
                Native.Methods.Internal.RenderFrame(upperLeftCorner, lowerRightCorner, backgroundColor, ImBool.False, rounding);
            }
            else
            {
                var style    = Style.AsWritable();
                var oldColor = style[ImGuiColor.Border];
                style[ImGuiColor.Border] = borderColor.ToVector();
                Native.Methods.Internal.RenderFrame(upperLeftCorner, lowerRightCorner, backgroundColor, ImBool.True, rounding);
                style[ImGuiColor.Border] = oldColor;
            }
        }

        /// <inheritdoc cref="Frame(Vector2,Vector2,Rgba32,float,Rgba32)"/>
        /// <param name="rectangle"> The frame's rectangle in screen coordinates. </param>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void Frame(in Rectangle rectangle, Rgba32 backgroundColor, float rounding, Rgba32 borderColor = default)
            => Frame(rectangle.Minimum, rectangle.Maximum, backgroundColor, rounding, borderColor);

        /// <summary> Render a frame's border without the frame. </summary>
        /// <param name="upperLeftCorner"> The upper left corner of the frame's rectangle in screen coordinates. </param>
        /// <param name="lowerRightCorner"> The lower right corner of the frame's rectangle in screen coordinates. </param>
        /// <param name="color"> The color for the border. If this is transparent, the current style is used. </param>
        /// <param name="rounding"> The rounding of the frame's rectangle. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void FrameBorder(Vector2 upperLeftCorner, Vector2 lowerRightCorner, Rgba32 color, float rounding)
        {
            if (Style.FrameBorderThickness <= 0)
                return;

            if (color.IsTransparent)
            {
                Native.Methods.Internal.RenderFrameBorder(upperLeftCorner, lowerRightCorner, rounding);
            }
            else
            {
                var style    = Style.AsWritable();
                var oldColor = style[ImGuiColor.Border];
                style[ImGuiColor.Border] = color.ToVector();
                Native.Methods.Internal.RenderFrameBorder(upperLeftCorner, lowerRightCorner, rounding);
                style[ImGuiColor.Border] = oldColor;
            }
        }

        /// <inheritdoc cref="FrameBorder(Vector2,Vector2,Rgba32,float)"/>
        /// <param name="rectangle"> The frame's rectangle in screen coordinates. </param>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void FrameBorder(in Rectangle rectangle, Rgba32 color, float rounding)
            => FrameBorder(rectangle.Minimum, rectangle.Maximum, color, rounding);

        /// <summary> Render a navigation highlight box around the given item. </summary>
        /// <param name="upperLeftCorner"> The upper left corner of the highlight box's rectangle in screen coordinates. </param>
        /// <param name="lowerRightCorner"> The lower right corner of the highlight box's rectangle in screen coordinates. </param>
        /// <param name="id"> The ID of the associated item. </param>
        /// <param name="flags"> Additional flag to control the highlight box's behaviour. </param>
        /// <remarks> Generally only useful when creating your own widgets. </remarks>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void NavigationHighlight(Vector2 upperLeftCorner, Vector2 lowerRightCorner, ImGuiId id,
            NavigationHighlightFlags flags = NavigationHighlightFlags.None)
            => Native.Methods.Internal.RenderNavHighlight(new ImRect(upperLeftCorner, lowerRightCorner), id, flags);

        /// <inheritdoc cref="NavigationHighlight(Vector2,Vector2,ImGuiId,NavigationHighlightFlags)"/>>
        /// <param name="rectangle"> The bounding box for the highlight box in screen coordinates. </param>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void NavigationHighlight(in Rectangle rectangle, ImGuiId id,
            NavigationHighlightFlags flags = NavigationHighlightFlags.None)
            => Native.Methods.Internal.RenderNavHighlight(rectangle, id, flags);

        /// <summary> Render a simple cross (X) in a square. </summary>
        /// <param name="drawList"> The draw list to render in. </param>
        /// <param name="position"> The upper left corner of the square. </param>
        /// <param name="color"> The color of the cross. </param>
        /// <param name="size"> The size of the square in pixels. </param>
        public static void Cross(DrawList drawList, Vector2 position, Rgba32 color, float size)
        {
            var offset = (int)size & 1;
            var thickness = Math.Max(size / 5, 1);
            var padding = new Vector2(thickness / 3f);
            size -= padding.X * 2 + offset;
            position += padding;
            var otherCorner = position + new Vector2(size);
            drawList.Shape.Line(position, otherCorner, color, thickness);
            position.X += size;
            otherCorner.X -= size;
            drawList.Shape.Line(position, otherCorner, color, thickness);
        }

        /// <summary> Render a simple checkmark in a square. </summary>
        /// <param name="drawList"> The draw list to render in. </param>
        /// <param name="position"> The upper left corner of the square in screen coordinates. </param>
        /// <param name="color"> The color of the checkmark. </param>
        /// <param name="size"> The size of the square in pixels. </param>
        public static void Checkmark(DrawList drawList, Vector2 position, Rgba32 color, float size)
        {
            var thickness = Math.Max(size / 5, 1);
            size -= thickness / 2;
            var padding = new Vector2(thickness / 4);
            position += padding;

            var third = size / 3;
            var bx = position.X + third;
            var by = position.Y + size - third / 2;
            drawList.Path.LineTo(new Vector2(bx - third,        by - third));
            drawList.Path.LineTo(new Vector2(bx,                by));
            drawList.Path.LineTo(new Vector2(bx + third * 2.0f, by - third * 2.0f));
            drawList.Path.FinishStroke(color, 0, thickness);
        }

        /// <summary> Render a simple dot in a square. </summary>
        /// <param name="drawList"> The draw list to render in. </param>
        /// <param name="position"> The upper left corner of the square in screen coordinates. </param>
        /// <param name="color"> The color of the dot. </param>
        /// <param name="size"> The size of the square in pixels. </param>
        public static void Dot(DrawList drawList, Vector2 position, Rgba32 color, float size)
        {
            var padding = size / 7;
            var pos = position + new Vector2(size / 2);
            size = size / 2 - padding;
            drawList.Shape.CircleFilled(pos, size, color);
        }

        /// <summary> Render a simple dash in a square. </summary>
        /// <param name="drawList"> The draw list to render in. </param>
        /// <param name="position"> The upper left corner of the square in screen coordinates. </param>
        /// <param name="color"> The color of the dash. </param>
        /// <param name="size"> The size of the square in pixels. </param>
        public static void Dash(DrawList drawList, Vector2 position, Rgba32 color, float size)
        {
            var offset = (int)size & 1;
            var thickness = (int)Math.Max(size / 4, 1) | offset;
            var padding = thickness / 2;
            position.X += padding;
            position.Y += size / 2;
            size -= padding * 2;

            var otherCorner = position with { X = position.X + size };
            drawList.Shape.Line(position, otherCorner, color, thickness);
        }
    }
}
