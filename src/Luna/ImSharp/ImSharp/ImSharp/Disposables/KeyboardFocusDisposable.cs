namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around pushing keyboard focus states. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class KeyboardFocusDisposable : IDisposable
    {
        /// <summary> The number of keyboard focus states currently pushed using this disposable. </summary>
        public int Count { get; private set; }

        /// <summary> Push a keyboard focus state to the keyboard focus state stack. </summary>
        /// <param name="allow"> Whether to allow keyboard focus for items or not. </param>
        /// <param name="condition"> If this is false, the state is not pushed. </param>
        /// <returns> A disposable object that can be used to push further keyboard focus states and pops those states after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep keyboard focus states pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public KeyboardFocusDisposable Push(bool allow, bool condition)
            => condition ? Push(allow) : this;

        /// <inheritdoc cref="Push(bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public KeyboardFocusDisposable Push(bool allow)
        {
            Native.Methods.Stacks.PushAllowKeyboardFocus(allow);
            ++Count;
            return this;
        }

        /// <summary> Pop a number of keyboard focus states. </summary>
        /// <param name="num"> The number of keyboard focus states to pop. This is clamped to the number of states pushed by this object. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Pop(int num = 1)
        {
            num   =  Math.Min(num, Count);
            Count -= num;
            while (num-- > 0)
                Native.Methods.Stacks.PopAllowKeyboardFocus();
        }

        /// <summary> Pop all pushed keyboard focus states. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
            => Pop(Count);

        /// <summary> Pop a number of keyboard focus states. </summary>
        /// <param name="num"> The number of keyboard focus states to pop. The number is not checked against the keyboard focus state stack. </param>
        /// <remarks> Avoid using this function, and states across scopes, as much as possible. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void PopUnsafe(int num = 1)
        {
            while (num-- > 0)
                Native.Methods.Stacks.PopAllowKeyboardFocus();
        }
    }
}
