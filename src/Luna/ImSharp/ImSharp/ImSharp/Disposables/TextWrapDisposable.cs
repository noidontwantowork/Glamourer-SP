namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around pushing text wrap positions. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class TextWrapDisposable : IDisposable
    {
        /// <summary> The number of text wrap positions currently pushed using this disposable. </summary>
        public int Count { get; private set; }

        /// <summary> Push a text wrap position to the text wrap stack. </summary>
        /// <param name="localX"> The window-local X coordinate at which to wrap text. If this is negative, no wrapping, if it is 0, wrap from here to the end of the available content region, and if it is positive, wrap from here. </param>
        /// <param name="condition"> If this is false, the position is not pushed. </param>
        /// <returns> A disposable object that can be used to push further text wrap positions and pops those positions after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep text wrap positions pushed longer than the current scope, use without using and use <seealso cref="Im.PopTextWrapPositionUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public TextWrapDisposable Push(float localX, bool condition)
            => condition ? Push(localX) : this;

        /// <inheritdoc cref="Push(float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public TextWrapDisposable Push(float localX)
        {
            Native.Methods.Stacks.PushTextWrapPos(localX);
            ++Count;
            return this;
        }

        /// <summary> Pop a number of text wrap positions. </summary>
        /// <param name="num"> The number of text wrap positions to pop. This is clamped to the number of positions pushed by this object. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Pop(int num = 1)
        {
            num   =  Math.Min(num, Count);
            Count -= num;
            while (num-- > 0)
                Native.Methods.Stacks.PopTextWrapPos();
        }

        /// <summary> Pop all pushed text wrap positions. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
            => Pop(Count);
    }
}
