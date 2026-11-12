using ImSharp.Internal;

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace ImSharp;

public static partial class Im
{
    /// <summary> Wrapper class for internal behavior functions, which generally should only be used when creating entirely custom widgets. </summary>
    public static class Behavior
    {
        /// <summary> Helper shortcut to simulate button behavior without drawing a button. </summary>
        /// <param name="upperLeftCorner"> The upper left corner of the bounding box rectangle that should be checked for button functionality in screen coordinates. </param>
        /// <param name="lowerRightCorner"> The lower right corner of the bounding box rectangle that should be checked for button functionality in screen coordinates. </param>
        /// <param name="id"> The ID of the associated item. </param>
        /// <param name="hovered"> Returns whether the bounding box is currently hovered by the mouse cursor. </param>
        /// <param name="held"> Returns whether the bounding box is currently hovered by the mouse cursor while the associated button is being held. </param>
        /// <param name="flags"> Additional flags to control the button's behavior. </param>
        /// <returns> Whether the bounding box was clicked in this frame. </returns>
        /// <remarks> This should generally only be used when creating custom widgets. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool Button(Vector2 upperLeftCorner, Vector2 lowerRightCorner, ImGuiId id, out bool hovered, out bool held,
            ButtonFlags flags = ButtonFlags.None)
        {
            fixed (bool* hov = &hovered, hel = &held)
            {
                return Native.Methods.Internal.ButtonBehavior(new ImRect(upperLeftCorner, lowerRightCorner), id, (ImBool*)hov, (ImBool*)hel,
                    flags);
            }
        }

        /// <inheritdoc cref="Button(Vector2,Vector2,ImGuiId,out bool,out bool,ButtonFlags)"/>
        /// <param name="boundingBox"> The bounding box rectangle that should be checked for button functionality in screen coordinates. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool Button(in Rectangle boundingBox, ImGuiId id, out bool hovered, out bool held,
            ButtonFlags flags = ButtonFlags.None)
        {
            fixed (bool* hov = &hovered, hel = &held)
            {
                return Native.Methods.Internal.ButtonBehavior(boundingBox, id, (ImBool*)hov, (ImBool*)hel, flags);
            }
        }

        /// <summary> Helper shortcut to simulate splitter behavior and drawing a splitter line. </summary>
        /// <param name="upperLeftCorner"> The upper left corner of the bounding box rectangle that contains the entire split area in screen coordinates. </param>
        /// <param name="lowerRightCorner"> The lower right corner of the bounding box rectangle that contains the entire split area in screen coordinates. </param>
        /// <param name="id"> The ID of the associated item. </param>
        /// <param name="axis"> The directional axis for the splitter. </param>
        /// <param name="sizeLeft"> The current size to the left or top of the splitter in pixels. </param>
        /// <param name="sizeRight"> The current size to the right or bottom of the splitter in pixels. </param>
        /// <param name="minimumSizeLeft"> The minimal allowed size of the left or top part in pixels. </param>
        /// <param name="minimumSizeRight"> The minimal allowed size of the right or bottom part in pixels. </param>
        /// <param name="hoverExtend"> The additional space orthogonal to the <paramref name="axis"/> that can be hovered and gripped for interactions. </param>
        /// <param name="hoverDelay"> The delay before the hover highlight starts in seconds. </param>
        /// <param name="color"> The color of the drawn line. </param>
        /// <returns> Whether the splitter was dragged this frame and thus changed the left and right sizes. </returns>
        /// <remarks> This should generally only be used when creating custom widgets. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool Splitter(Vector2 upperLeftCorner, Vector2 lowerRightCorner, ImGuiId id, Axis axis, ref float sizeLeft,
            ref float sizeRight, float minimumSizeLeft, float minimumSizeRight, float hoverExtend, float hoverDelay, Rgba32 color)
            => Native.Methods.Internal.SplitterBehavior(new ImRect(upperLeftCorner, lowerRightCorner), id, axis,
                (float*)Unsafe.AsPointer(ref sizeLeft), (float*)Unsafe.AsPointer(ref sizeRight), minimumSizeLeft, minimumSizeRight, hoverExtend,
                hoverDelay, color);

        /// <inheritdoc cref="Splitter(Vector2,Vector2,ImGuiId,Axis,ref float,ref float,float,float,float,float,Rgba32)"/>
        /// <param name="boundingBox"> The bounding box rectangle that contains the entire split area in screen coordinates. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool Splitter(in Rectangle boundingBox, ImGuiId id, Axis axis, ref float sizeLeft,
            ref float sizeRight, float minimumSizeLeft, float minimumSizeRight, float hoverExtend, float hoverDelay, Rgba32 color)
            => Native.Methods.Internal.SplitterBehavior(boundingBox, id, axis,
                (float*)Unsafe.AsPointer(ref sizeLeft), (float*)Unsafe.AsPointer(ref sizeRight), minimumSizeLeft, minimumSizeRight, hoverExtend,
                hoverDelay, color);
    }
}
