namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    /// <summary> A wrapper around ImNodes style pushing. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class StyleDisposable : IDisposable
    {
        /// <summary> The number of ImNodes styles currently pushed using this disposable. </summary>
        public int Count { get; private set; }

        /// <summary> Push a ImNodes style variable to the style stack. </summary>
        /// <param name="type"> The type of style variable to change. </param>
        /// <param name="value"> The value to change it to. </param>
        /// <param name="condition"> If this is false, the style is not pushed. </param>
        /// <returns> A disposable object that can be used to push further ImNodes style variables and pops those style variables after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep styles pushed longer than the current scope, use without using and use <seealso cref="StyleDisposable.PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Push(ImNodesStyleSingle type, float value, bool condition)
            => condition ? Push(type, value) : this;

        /// <inheritdoc cref="Push(ImNodesStyleSingle,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Push(ImNodesStyleDouble type, Vector2 value, bool condition)
            => condition ? Push(type, value) : this;

        /// <inheritdoc cref="Push(ImNodesStyleSingle,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Push(ImNodesStyleSingle type, float value)
        {
            Api.PushStyleVar(type, value);
            ++Count;
            return this;
        }

        /// <inheritdoc cref="Push(ImNodesStyleSingle,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Push(ImNodesStyleDouble type, Vector2 value)
        {
            Api.PushStyleVar(type, value);
            ++Count;
            return this;
        }

        /// <summary> Push only the first value of a double-value style to the ImNodes style stack, keeping the second as-is. </summary>
        /// <param name="type"> The type of style variable to change. </param>
        /// <param name="value"> The value to change it to. </param>
        /// <param name="condition"> If this is false, the style is not pushed. </param>
        /// <returns> A disposable object that can be used to push further ImNodes style variables and pops those style variables after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep styles pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushX(ImNodesStyleDouble type, float value, bool condition)
            => condition ? Push(type, Style[type] with { X = value }) : this;

        /// <summary> Push only the second value of a double-value style to the ImNodes style stack, keeping the first as-is. </summary>
        /// <param name="type"> The type of style variable to change. </param>
        /// <param name="value"> The value to change it to. </param>
        /// <param name="condition"> If this is false, the style is not pushed. </param>
        /// <returns> A disposable object that can be used to push further ImNodes style variables and pops those style variables after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep styles pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushY(ImNodesStyleDouble type, float value, bool condition)
            => condition ? Push(type, Style[type] with { Y = value }) : this;

        /// <inheritdoc cref="PushX(ImNodesStyleDouble,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushX(ImNodesStyleDouble type, float value)
            => Push(type, Style[type] with { X = value });

        /// <inheritdoc cref="PushY(ImNodesStyleDouble,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushY(ImNodesStyleDouble type, float value)
            => Push(type, Style[type] with { Y = value });

        /// <summary> Pop a number of ImNodes style variables. </summary>
        /// <param name="num"> The number of style variables to pop. This is clamped to the number of style variables pushed by this object. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Pop(int num = 1)
        {
            num   =  Math.Min(num, Count);
            Count -= num;
            Api.PopStyle(num);
            return this;
        }

        /// <summary> Pop all pushed styles. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
            => Pop(Count);
    }
}
