namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    /// <summary> A wrapper around ImNodes attribute flag pushing. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class AttributeFlagDisposable : IDisposable
    {
        /// <summary> The number of ImNodes attribute flags currently pushed using this disposable. </summary>
        public int Count { get; private set; }

        /// <summary> Push an attribute flag to the ImNodes attribute flag stack. </summary>
        /// <param name="flag"> The attribute flag to push. </param>
        /// <param name="condition"> If this is false, the attribute flag is not pushed. </param>
        /// <returns> A disposable object that can be used to push further attribute flags and pops those flags after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep attribute flags pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public AttributeFlagDisposable Push(AttributeFlags flag, bool condition)
            => condition ? Push(flag) : this;

        /// <inheritdoc cref="Push(AttributeFlags,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public AttributeFlagDisposable Push(AttributeFlags flag)
        {
            Api.PushAttributeFlag(flag);
            ++Count;
            return this;
        }

        /// <summary> Pop a number of attribute flags. </summary>
        /// <param name="num"> The number of attribute flags to pop. This is clamped to the number of attribute flags pushed by this object. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Pop(int num = 1)
        {
            num   =  Math.Min(num, Count);
            Count -= num;
            while (num-- > 0)
                Api.PopAttributeFlag();
        }

        /// <summary> Pop all pushed attribute flags. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
            => Pop(Count);
    }
}
