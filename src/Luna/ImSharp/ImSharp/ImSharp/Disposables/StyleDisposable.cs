namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around style pushing. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class StyleDisposable : IDisposable
    {
        /// <summary> The number of styles currently pushed using this disposable. </summary>
        public int Count { get; private set; }

        /// <summary> Push a style variable to the style stack. </summary>
        /// <param name="type"> The type of style variable to change. </param>
        /// <param name="value"> The value to change it to. </param>
        /// <param name="condition"> If this is false, the style is not pushed. </param>
        /// <returns> A disposable object that can be used to push further style variables and pops those style variables after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep styles pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Push(ImStyleSingle type, float value, bool condition)
            => condition ? Push(type, value) : this;

        /// <inheritdoc cref="Push(ImStyleSingle,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Push(ImStyleDouble type, Vector2 value, bool condition)
            => condition ? Push(type, value) : this;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Push(ImStyleSingle type, float value)
        {
            Native.Methods.Stacks.PushStyleVar((ImStyle)type, value);
            ++Count;
            return this;
        }

        /// <inheritdoc cref="Push(ImStyleSingle,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Push(ImStyleDouble type, Vector2 value)
        {
            Native.Methods.Stacks.PushStyleVar((ImStyle)type, value);
            ++Count;
            return this;
        }

        /// <summary> Push the default value, i.e. the value as if nothing was ever pushed to this, of a style variable to the style stack. </summary>
        /// <param name="type"> The type of style variable to return to its default value. </param>
        /// <returns> A disposable object that can be used to push further style variables and pops those style variables after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep styles pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        public StyleDisposable PushDefault(ImStyleDouble type)
        {
            foreach (var styleMod in Context.StyleStack.Where(m => m.VarIdx == (ImStyle)type))
                return Push(type, styleMod.BackupVec);

            return this;
        }

        /// <inheritdoc cref="PushDefault(ImStyleDouble)"/>
        public StyleDisposable PushDefault(ImStyleSingle type)
        {
            foreach (var styleMod in Context.StyleStack.Where(m => m.VarIdx == (ImStyle)type))
                return Push(type, styleMod.BackupFloat1);

            return this;
        }

        /// <summary> Push only the first value of a double-value style to the style stack, keeping the second as-is. </summary>
        /// <param name="type"> The type of style variable to change. </param>
        /// <param name="value"> The value to change it to. </param>
        /// <param name="condition"> If this is false, the style is not pushed. </param>
        /// <returns> A disposable object that can be used to push further style variables and pops those style variables after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep styles pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushX(ImStyleDouble type, float value, bool condition)
            => condition ? Push(type, Style[type] with { X = value }) : this;

        /// <summary> Push only the second value of a double-value style to the style stack, keeping the first as-is. </summary>
        /// <param name="type"> The type of style variable to change. </param>
        /// <param name="value"> The value to change it to. </param>
        /// <param name="condition"> If this is false, the style is not pushed. </param>
        /// <returns> A disposable object that can be used to push further style variables and pops those style variables after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep styles pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushY(ImStyleDouble type, float value, bool condition)
            => condition ? Push(type, Style[type] with { Y = value }) : this;

        /// <inheritdoc cref="PushX(ImStyleDouble,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushX(ImStyleDouble type, float value)
            => Push(type, Style[type] with { X = value });

        /// <inheritdoc cref="PushY(ImStyleDouble,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushY(ImStyleDouble type, float value)
            => Push(type, Style[type] with { Y = value });

        /// <summary> Pop a number of style variables. </summary>
        /// <param name="num"> The number of style variables to pop. This is clamped to the number of style variables pushed by this object. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Pop(int num = 1)
        {
            num = Math.Min(num, Count);
            if (num > 0)
            {
                Count -= num;
                Native.Methods.Stacks.PopStyleVar(num);
            }

            return this;
        }

        /// <summary> Pop all pushed styles. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            Native.Methods.Stacks.PopStyleVar(Count);
            Count = 0;
        }

        /// <summary> Pop a number of style variables. </summary>
        /// <param name="num"> The number of style variables to pop. The number is not checked against the style stack. </param>
        /// <remarks> Avoid using this function, and styles across scopes, as much as possible. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void PopUnsafe(int num = 1)
            => Native.Methods.Stacks.PopStyleVar(num);
    }
}
