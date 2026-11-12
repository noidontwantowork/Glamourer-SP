namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    /// <summary> A wrapper around ImNodes color pushing. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ColorDisposable : IDisposable
    {
        /// <summary> The number of ImNodes colors currently pushed using this disposable. </summary>
        public int Count { get; private set; }

        /// <summary> Push a color to the ImNodes color stack. </summary>
        /// <param name="type"> The type of ImNodes color to change. </param>
        /// <param name="color"> The color to change it to. </param>
        /// <param name="condition"> If this is false, the color is not pushed. </param>
        /// <returns> A disposable object that can be used to push further colors and pops those colors after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep colors pushed longer than the current scope, use without using and use <seealso cref="Im.ColorDisposable.PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorDisposable Push(ImNodesColor type, Rgba32 color, bool condition)
            => condition ? Push(type, color) : this;

        /// <inheritdoc cref="Push(ImNodesColor,Rgba32,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public ColorDisposable Push(ImNodesColor type, Rgba32 color)
        {
            Api.PushColorStyle(type, color.Color);
            ++Count;
            return this;
        }

        /// <summary> Push a color to the ImNodes color stack. </summary>
        /// <param name="type"> The type of ImNodes color to change. </param>
        /// <param name="color"> The color to change it to. If this is null, no color will be set. </param>
        /// <returns> A disposable object that can be used to push further colors and pops those colors after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep colors pushed longer than the current scope, use without using and use <seealso cref="Im.ColorDisposable.PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(50)]
        public ColorDisposable Push(ImNodesColor type, ColorParameter color)
            => color.IsDefault ? this : Push(type, color.Color!.Value);

        /// <summary> Pop a number of colors. </summary>
        /// <param name="num"> The number of colors to pop. This is clamped to the number of colors pushed by this object. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorDisposable Pop(int num = 1)
        {
            num   =  Math.Min(num, Count);
            Count -= num;
            while (num-- > 0)
                Api.PopColorStyle();
            return this;
        }

        /// <summary> Pop all pushed colors. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
            => Pop(Count);
    }
}
