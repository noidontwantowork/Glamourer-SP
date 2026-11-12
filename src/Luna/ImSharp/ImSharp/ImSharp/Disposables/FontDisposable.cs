namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around pushing fonts. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class FontDisposable : IDisposable
    {
        /// <summary> The number of fonts currently pushed using this disposable. </summary>
        public int Count { get; private set; }

        /// <summary> Push a font to the font stack. </summary>
        /// <param name="font"> The font to push. </param>
        /// <param name="condition"> If this is false, the font is not pushed. </param>
        /// <returns> A disposable object that can be used to push further fonts and pops those fonts after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep fonts pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public FontDisposable Push(Font font, bool condition)
            => condition ? Push(font) : this;

        /// <inheritdoc cref="Push(ImSharp.Im.Font,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe FontDisposable Push(Font font)
        {
            Native.Methods.Stacks.PushFont(font.Pointer);
            ++Count;
            return this;
        }

        /// <summary> Pop a number of fonts. </summary>
        /// <param name="num"> The number of fonts to pop. This is clamped to the number of fonts pushed by this object. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Pop(int num = 1)
        {
            num   =  Math.Min(num, Count);
            Count -= num;
            while (num-- > 0)
                Native.Methods.Stacks.PopFont();
        }

        /// <summary> Pop all pushed fonts. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
            => Pop(Count);

        /// <summary> Pop a number of fonts. </summary>
        /// <param name="num"> The number of fonts to pop. The number is not checked against the font stack. </param>
        /// <remarks> Avoid using this function, and fonts across scopes, as much as possible. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void PopUnsafe(int num = 1)
        {
            while (num-- > 0)
                Native.Methods.Stacks.PopFont();
        }
    }
}
