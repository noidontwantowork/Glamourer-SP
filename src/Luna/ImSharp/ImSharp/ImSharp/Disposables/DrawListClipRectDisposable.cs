namespace ImSharp;
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)

public static partial class Im
{
    /// <summary> Push a draw list specific ClipRect and pop it on leaving scope. </summary>
    /// <param name="drawList"> The native pointer to the draw list. </param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe sealed class DrawListClipRectDisposable(Native.ImDrawList* drawList) : IDisposable
    {
        /// <summary> The number of ClipRects currently pushed with this object. </summary>
        public int Count { get; private set; }

        /// <summary> Push a new ClipRect into the draw list. </summary>
        /// <param name="topLeft"> The top-left corner of the rectangle in screen coordinates. </param>
        /// <param name="bottomRight"> The bottom-right corner of the rectangle in screen coordinates. </param>
        /// <param name="intersectWithCurrentClipRect"> Whether to intersect the rectangle with the current existing ClipRect or to replace it. </param>
        /// <param name="condition"> If this is false, the ClipRect is not pushed. </param>
        /// <returns> A disposable object that can be used to push further ClipRects and pops those ClipRects after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep ClipRects pushed longer than the current scope, use without using and use <seealso cref="DrawList.PopClipRectUnsafe"/>. </remarks>
        [OverloadResolutionPriority(50)]
        [MethodImpl(ImSharpConfiguration.OptInl)]
        // ReSharper disable once MethodOverloadWithOptionalParameter
        public DrawListClipRectDisposable Push(Vector2 topLeft, Vector2 bottomRight, bool intersectWithCurrentClipRect = false,
            bool condition = true)
            => condition ? Push(topLeft, bottomRight, intersectWithCurrentClipRect) : this;

        /// <inheritdoc cref="Push(Vector2,Vector2,bool,bool)"/>
        [OverloadResolutionPriority(100)]
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public DrawListClipRectDisposable Push(Vector2 topLeft, Vector2 bottomRight, bool intersectWithCurrentClipRect = false)
        {
            Native.ImDrawList.PushClipRect(drawList, topLeft, bottomRight, intersectWithCurrentClipRect);
            ++Count;
            return this;
        }

        /// <summary> Push a new ClipRect into the draw list. </summary>
        /// <param name="rectangle"> The rectangle in screen coordinates. </param>
        /// <param name="intersectWithCurrentClipRect"> Whether to intersect the rectangle with the current existing ClipRect or to replace it. </param>
        /// <param name="condition"> If this is false, the ClipRect is not pushed. </param>
        /// <returns> A disposable object that can be used to push further ClipRects and pops those ClipRects after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep ClipRects pushed longer than the current scope, use without using and use <seealso cref="DrawList.PopClipRectUnsafe"/>. </remarks>
        [OverloadResolutionPriority(50)]
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public DrawListClipRectDisposable Push(in Rectangle rectangle, bool intersectWithCurrentClipRect = false, bool condition = true)
            => condition ? Push(rectangle.Minimum, rectangle.Maximum, intersectWithCurrentClipRect) : this;

        /// <inheritdoc cref="Push(in Rectangle,bool,bool)"/>
        [OverloadResolutionPriority(100)]
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public DrawListClipRectDisposable Push(in Rectangle rectangle, bool intersectWithCurrentClipRect = false)
        {
            Native.ImDrawList.PushClipRect(drawList, rectangle.Minimum, rectangle.Maximum, intersectWithCurrentClipRect);
            ++Count;
            return this;
        }

        /// <summary> Replace the current ClipRect of a draw list with a full screen rectangle. </summary>
        /// <param name="condition"> If this is false, the ClipRect is not pushed. </param>
        /// <returns> A disposable object that can be used to push further ClipRects and pops those ClipRects after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep ClipRects pushed longer than the current scope, use without using and use <seealso cref="DrawList.PopClipRectUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public DrawListClipRectDisposable PushFullScreen(bool condition = true)
        {
            if (!condition)
                return this;

            Native.ImDrawList.PushClipRectFullScreen(drawList);
            ++Count;
            return this;
        }

        /// <summary> Pop a number of ClipRects in this draw list. </summary>
        /// <param name="count"> The number of ClipRects to pop. This is clamped to the number of ClipRects pushed by this object. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Pop(int count = 1)
        {
            if (count is 0)
                return;

            count =  Math.Min(count, Count);
            Count -= count;
            while (count-- > 0)
                Native.ImDrawList.PopClipRect(drawList);
        }

        /// <summary> Pop all ClipRects pushed with this object. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
            => Pop(Count);
    }
}
