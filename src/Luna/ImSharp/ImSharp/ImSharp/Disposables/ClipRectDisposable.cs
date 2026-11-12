namespace ImSharp;

// ReSharper disable MethodOverloadWithOptionalParameter
#pragma warning disable CS1573
public static partial class Im
{
    /// <summary> Push a global ClipRect and pop it on leaving scope. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ClipRectDisposable : IDisposable
    {
        /// <summary> The number of ClipRects currently pushed with this object. </summary>
        public int Count { get; private set; }

        /// <summary> Push a new global ClipRect. </summary>
        /// <param name="topLeft"> The top-left corner of the rectangle in screen coordinates. </param>
        /// <param name="bottomRight"> The bottom-right corner of the rectangle in screen coordinates. </param>
        /// <param name="intersectWithCurrentClipRect"> Whether to intersect the rectangle with the current existing ClipRect or to replace it. </param>
        /// <param name="condition"> If this is false, the ClipRect is not pushed. </param>
        /// <returns> A disposable object that can be used to push further ClipRects and pops those ClipRects after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep ClipRects pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [OverloadResolutionPriority(50)]
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ClipRectDisposable Push(in Vector2 topLeft, in Vector2 bottomRight, bool intersectWithCurrentClipRect = false,
            bool condition = true)
            => condition ? Push(topLeft, bottomRight, intersectWithCurrentClipRect) : this;

        /// <inheritdoc cref="Push(in Vector2,in Vector2,bool,bool)"/>
        [OverloadResolutionPriority(100)]
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ClipRectDisposable Push(in Vector2 topLeft, in Vector2 bottomRight, bool intersectWithCurrentClipRect = false)
        {
            Native.Methods.Stacks.PushClipRect(topLeft, bottomRight, intersectWithCurrentClipRect);
            ++Count;
            return this;
        }

        /// <param name="rectangle"> The rectangle in screen coordinates. </param>
        /// <inheritdoc cref="Push(in Vector2,in Vector2,bool,bool)"/>
        [OverloadResolutionPriority(50)]
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ClipRectDisposable Push(in Rectangle rectangle, bool intersectWithCurrentClipRect = false, bool condition = true)
            => condition ? Push(rectangle, intersectWithCurrentClipRect) : this;

        /// <inheritdoc cref="Push(in Rectangle,bool,bool)"/>
        [OverloadResolutionPriority(100)]
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ClipRectDisposable Push(in Rectangle rectangle, bool intersectWithCurrentClipRect = false)
        {
            Native.Methods.Stacks.PushClipRect(rectangle.Minimum, rectangle.Maximum, intersectWithCurrentClipRect);
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
                Native.Methods.Stacks.PopClipRect();
        }

        /// <summary> Pop all ClipRects pushed with this object. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
            => Pop(Count);

        /// <summary> Pop a number of ClipRects from the ClipRect stack without using an IDisposable. </summary>
        /// <remarks> Avoid using this function, and ClipRects across scopes, as much as possible. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void PopUnsafe(int count = 1)
        {
            while (count-- > 0)
                Native.Methods.Stacks.PopClipRect();
        }
    }
}
