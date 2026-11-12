namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around disabled state. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class DisabledDisposable : IDisposable
    {
        /// <summary> The global count of disabled pushes to reenable. </summary>
        public static int GlobalCount
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Context.DisabledStackSize;
        }

        /// <summary> The number of disabled states currently pushed using this disposable. </summary>
        public int Count { get; private set; }

        /// <summary> Push a disabled state onto the stack. </summary>
        /// <param name="condition"> Whether to actually push a disabled state. </param>
        /// <returns> A disposable object that can be used to push further disabled states for whatever reason and pop them on leaving scope. Use with using.</returns>
        /// <remarks> If you need to keep a disabled state pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public DisabledDisposable Push(bool condition)
            => condition ? Push() : this;

        /// <inheritdoc cref="Push(bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public DisabledDisposable Push()
        {
            Native.Methods.Disabled.BeginDisabled(true);
            ++Count;
            return this;
        }

        /// <summary> Pop a number of disabled states. </summary>
        /// <param name="num"> The number of disabled states to pop. This is clamped to the number of disabled states pushed by this object. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Pop(int num = 1)
        {
            num   =  Math.Min(num, Count);
            Count -= num;
            while (num-- > 0)
                Native.Methods.Disabled.EndDisabled();
        }

        /// <summary> Pop all disabled states. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
            => Pop(Count);

        /// <summary> Pop a number of disabled states. </summary>
        /// <param name="num"> The number of disabled states to pop. The number is not checked against the disabled stack. </param>
        /// <remarks> Avoid using this function, and disabled states across scopes, as much as possible. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void PopUnsafe(int num = 1)
        {
            while (num-- > 0)
                Native.Methods.Disabled.EndDisabled();
        }
    }
}
