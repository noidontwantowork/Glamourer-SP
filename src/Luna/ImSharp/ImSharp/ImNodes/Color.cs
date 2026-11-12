namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    /// <summary> A wrapper class for color-related queries and actions. </summary>
    public static class Color
    {
        /// <summary> Get the current ImNodes style color as a RGBA32 uint. </summary>
        /// <param name="color"> The requested color. </param>
        /// <returns> The requested color as RGBA32. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static Rgba32 Get(ImNodesColor color)
            => Style[color];

        /// <summary> Create a new, empty <see cref="ColorDisposable"/> to push colors to. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ColorDisposable Empty()
            => new();

        /// <inheritdoc cref="ColorDisposable.Push(ImNodesColor,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ColorDisposable Push(ImNodesColor type, Rgba32 color)
            => new ColorDisposable().Push(type, color);

        /// <inheritdoc cref="ColorDisposable.Push(ImNodesColor,Rgba32,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ColorDisposable Push(ImNodesColor type, Rgba32 color, bool condition)
            => condition ? new ColorDisposable().Push(type, color) : new ColorDisposable();

        /// <inheritdoc cref="ColorDisposable.Push(ImNodesColor,ColorParameter)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ColorDisposable Push(ImNodesColor type, ColorParameter color)
            => color.IsDefault ? new ColorDisposable() : new ColorDisposable().Push(type, color.Color!.Value);

        /// <summary> Pop a number of ImNodes colors. </summary>
        /// <param name="num"> The number of colors to pop. The number is not checked against the ImNodes color stack. </param>
        /// <remarks> Avoid using this function, and colors across scopes, as much as possible. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void PopUnsafe(int num = 1)
        {
            while (num-- > 0)
                Api.PopColorStyle();
        }
    }
}
