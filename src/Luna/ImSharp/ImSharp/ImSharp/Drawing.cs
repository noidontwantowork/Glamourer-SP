

// ReSharper disable MethodOverloadWithOptionalParameter

namespace ImSharp;

public static partial class Im
{
    /// <summary> Wrapper class for general drawing related functions. </summary>
    public static unsafe class Drawing
    {
        /// <summary> Get the background draw list for the viewport associated with the current window. </summary>
        /// <remarks> The background draw list is the first that renders, so anything else is rendered on top of it. </remarks>
        public static DrawList BackgroundDrawList
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.DrawList.GetBackgroundDrawList();
        }

        /// <summary> Get the foreground draw list for the viewport associated with the current window. </summary>
        /// <remarks> The foreground draw list is the last that renders, so it renders on top of everything else. </remarks>
        public static DrawList ForegroundDrawList
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.DrawList.GetForegroundDrawList();
        }

        /// <inheritdoc cref="ClipRectDisposable.Push(in Rectangle,bool)"/>
        [OverloadResolutionPriority(100)]
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ClipRectDisposable PushClipRect(in Rectangle rectangle, bool intersectWithCurrentClipRect = false)
            => new ClipRectDisposable().Push(rectangle, intersectWithCurrentClipRect);

        /// <inheritdoc cref="ClipRectDisposable.Push(in Rectangle,bool,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(50)]
        public static ClipRectDisposable PushClipRect(in Rectangle rectangle, bool intersectWithCurrentClipRect = false, bool condition = true)
            => new ClipRectDisposable().Push(rectangle, intersectWithCurrentClipRect, condition);

        /// <inheritdoc cref="ClipRectDisposable.Push(in Vector2,in Vector2,bool)"/>
        [OverloadResolutionPriority(100)]
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ClipRectDisposable PushClipRect(in Vector2 upperLeftCorner, in Vector2 lowerRightCorner,
            bool intersectWithCurrentClipRect = false)
            => new ClipRectDisposable().Push(upperLeftCorner, lowerRightCorner, intersectWithCurrentClipRect);

        /// <inheritdoc cref="ClipRectDisposable.Push(in Vector2,in Vector2,bool,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(50)]
        public static ClipRectDisposable PushClipRect(in Vector2 upperLeftCorner, in Vector2 lowerRightCorner,
            bool intersectWithCurrentClipRect = false, bool condition = true)
            => new ClipRectDisposable().Push(upperLeftCorner, lowerRightCorner, intersectWithCurrentClipRect, condition);

        /// <summary> Test if the rectangle of the given size starting from the current cursor position is visible and not clipped. </summary>
        /// <param name="size"> The size of the rectangle. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsRectangleVisible(in Vector2 size)
            => Native.Methods.Utility.IsRectVisible(size);

        /// <summary> Test if the rectangle given in screen space coordinates is visible and not clipped. </summary>
        /// <param name="rectangle"> The rectangle to check. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsRectangleVisible(in Rectangle rectangle)
            => Native.Methods.Utility.IsRectVisible(rectangle.Minimum, rectangle.Maximum);

        /// <summary> Test if the rectangle given in screen space coordinates is visible and not clipped. </summary>
        /// <param name="upperLeft"> The upper left corner of the rectangle in screen space coordinates. </param>
        /// <param name="lowerRight"> The lower right corner of the rectangle in screen space coordinates. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsRectangleVisible(in Vector2 upperLeft, in Vector2 lowerRight)
            => Native.Methods.Utility.IsRectVisible(upperLeft, lowerRight);

        /// <summary> Test if the rectangle given in screen space coordinates is visible and not clipped. </summary>
        /// <param name="upperLeft"> The upper left corner of the rectangle in screen space coordinates. </param>
        /// <param name="size"> The size of the rectangle in pixels. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsRectangleVisibleSize(in Vector2 upperLeft, in Vector2 size)
            => Native.Methods.Utility.IsRectVisible(upperLeft, upperLeft + size);
    }
}
