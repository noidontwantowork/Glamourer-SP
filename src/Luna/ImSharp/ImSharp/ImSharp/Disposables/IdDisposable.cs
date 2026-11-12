namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ID pushing. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed unsafe class IdDisposable : IDisposable
    {
        /// <summary> The number of IDs currently pushed using this disposable. </summary>
        public int Count { get; private set; }

        /// <summary> A monotonously increasing counter used for <see cref="PushNext"/>. </summary>
        public int MonotoneCounter { get; private set; }

        /// <summary> Push a monotonously increasing numerical ID to the ID stack and pop it on leaving scope. The next ID is stored in this object and incremented on every call of this method. </summary>
        /// <returns> A disposable object that counts the number of pushes and can be used to push further IDs. Use with using. </returns>
        /// <remarks> If you need to keep IDs pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public IdDisposable PushNext()
        {
            ++Count;
            Native.Methods.IdStack.PushId(MonotoneCounter++);
            return this;
        }

        /// <summary> Push a numerical ID to the ID stack and pop it on leaving scope. </summary>
        /// <param name="id"> The ID. </param>
        /// <returns> A disposable object that counts the number of pushes and can be used to push further IDs. Use with using. </returns>
        /// <remarks> If you need to keep IDs pushed longer than the current scope, use without using and use <seealso cref="PopUnsafe"/>. </remarks>
        [OverloadResolutionPriority(100)]
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public IdDisposable Push(ImGuiId id)
        {
            ++Count;
            Native.Methods.IdStack.PushId((int)id.Id);
            return this;
        }

        /// <inheritdoc cref="Push(ImGuiId)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public IdDisposable Push(nint ptr)
        {
            ++Count;
            Native.Methods.IdStack.PushId(ptr);
            return this;
        }

        /// <inheritdoc cref="Push(ImGuiId)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public IdDisposable Push(Utf8LabelHandler label)
        {
            ++Count;
            Native.Methods.IdStack.PushId(label.Start(out var end), end);
            return this;
        }

        /// <inheritdoc cref="Push(ImGuiId)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal IdDisposable Push<T>(scoped ref Utf8StringHandler<T> label) where T : IStringHandlerBuffer
        {
            ++Count;
            Native.Methods.IdStack.PushId(label.Start(out var end), end);
            return this;
        }

        /// <summary> Pop a number of IDs from the ID stack. </summary>
        /// <param name="count"> The number of IDs to pop. This is clamped to the number of IDs pushed by this object. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Pop(int count = 1)
        {
            if (count > Count)
                count = Count;
            Count -= count;
            while (count-- > 0)
                Native.Methods.IdStack.PopId();
        }

        /// <summary> Pop all pushed IDs. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
            => Pop(Count);

        /// <summary> Pop a number of IDs from the ID stack without using an IDisposable. </summary>
        /// <remarks> Avoid using this function, and IDs across scopes, as much as possible. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void PopUnsafe(int num = 1)
        {
            while (num-- > 0)
                Native.Methods.IdStack.PopId();
        }
    }
}
