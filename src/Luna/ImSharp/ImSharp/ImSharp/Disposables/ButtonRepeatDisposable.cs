namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around pushing button repeat states. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ButtonRepeatDisposable : IDisposable
    {
        /// <summary> The number of button repeat states currently pushed using this disposable. </summary>
        public int Count { get; private set; }

        /// <summary> Push a button repeat state to the button repeat state stack. </summary>
        /// <param name="repeat"> Whether to repeat buttons or not. </param>
        /// <param name="condition"> If this is false, the state is not pushed. </param>
        /// <returns> A disposable object that can be used to push further button repeat states and pops those states after leaving scope. Use with using. </returns>
        /// <remarks> If you need to keep button repeat states pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ButtonRepeatDisposable Push(bool repeat, bool condition = true)
        {
            if (condition)
            {
                Native.Methods.Stacks.PushButtonRepeat(repeat);
                ++Count;
            }

            return this;
        }

        /// <summary> Pop a number of button repeat states. </summary>
        /// <param name="num"> The number of button repeat states to pop. This is clamped to the number of states pushed by this object. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Pop(int num = 1)
        {
            num   =  Math.Min(num, Count);
            Count -= num;
            while (num-- > 0)
                Native.Methods.Stacks.PopButtonRepeat();
        }

        /// <summary> Pop all pushed button repeat states. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
            => Pop(Count);

        /// <summary> Pop a number of button repeat states. </summary>
        /// <param name="num"> The number of button repeat states to pop. The number is not checked against the button repeat state stack. </param>
        /// <remarks> Avoid using this function, and states across scopes, as much as possible. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void PopUnsafe(int num = 1)
        {
            while (num-- > 0)
                Native.Methods.Stacks.PopButtonRepeat();
        }
    }
}
