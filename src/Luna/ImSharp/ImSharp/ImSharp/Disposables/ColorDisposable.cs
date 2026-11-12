namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around color pushing. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ColorDisposable : IDisposable
    {
        /// <summary> The number of colors currently pushed using this disposable. </summary>
        public int Count { get; private set; }

        /// <summary> Push a color to the color stack. </summary>
        /// <param name="type"> The type of color to change. </param>
        /// <param name="color"> The color to change it to. </param>
        /// <param name="condition"> If this is false, the color is not pushed. </param>
        /// <returns> A disposable object that can be used to push further colors and pops those colors after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep colors pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorDisposable Push(ImGuiColor type, Rgba32 color, bool condition)
            => condition ? Push(type, color) : this;

        /// <summary> Push a color to the color stack. </summary>
        /// <param name="type"> The type of color to change. </param>
        /// <param name="color"> The color to change it to. If this is null/default, no color will be set. </param>
        /// <returns> A disposable object that can be used to push further colors and pops those colors after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep colors pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorDisposable Push(ImGuiColor type, ColorParameter color)
            => color.IsDefault ? this : Push(type, color.Color!.Value);

        /// <inheritdoc cref="Push(ImGuiColor,Rgba32,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public ColorDisposable Push(ImGuiColor type, Vector4 color, bool condition)
            => condition ? Push(type, color) : this;

        /// <inheritdoc cref="Push(ImGuiColor,Rgba32,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorDisposable Push(ImGuiColor type, Rgba32 color)
        {
            Native.Methods.Stacks.PushStyleColor(type, color);
            ++Count;
            return this;
        }

        /// <inheritdoc cref="Push(ImGuiColor,Vector4,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public ColorDisposable Push(ImGuiColor type, Vector4 color)
        {
            Native.Methods.Stacks.PushStyleColor(type, color);
            ++Count;
            return this;
        }

        /// <summary> Push the default value, i.e. the value as if nothing was ever pushed to this, of a color to the color stack. </summary>
        /// <param name="type"> The type of color to return to its default value. </param>
        /// <returns> A disposable object that can be used to push further colors and pops those colors after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep colors pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        public ColorDisposable PushDefault(ImGuiColor type)
        {
            foreach (var styleMod in Context.ColorStack.Where(m => m.Color == type))
                return Push(type, styleMod.BackupValue);

            return this;
        }

        /// <summary> Pop a number of colors. </summary>
        /// <param name="num"> The number of colors to pop. This is clamped to the number of colors pushed by this object. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorDisposable Pop(int num = 1)
        {
            num = Math.Min(num, Count);
            if (num > 0)
            {
                Count -= num;
                Native.Methods.Stacks.PopStyleColor(num);
            }

            return this;
        }

        /// <summary> Pop all pushed colors. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            Native.Methods.Stacks.PopStyleColor(Count);
            Count = 0;
        }

        /// <summary> Pop a number of colors. </summary>
        /// <param name="num"> The number of colors to pop. The number is not checked against the color stack. </param>
        /// <remarks> Avoid using this function, and colors across scopes, as much as possible. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void PopUnsafe(int num = 1)
            => Native.Methods.Stacks.PopStyleColor(num);
    }
}
