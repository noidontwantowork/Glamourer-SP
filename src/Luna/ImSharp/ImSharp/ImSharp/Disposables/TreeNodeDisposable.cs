namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui tree nodes. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct TreeNodeDisposable : IDisposable
    {
        /// <summary> Whether creating the tree node succeeded and it is open. </summary>
        public readonly bool Success;

        /// <summary> Whether the tree node is already popped. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a tree node and end it on leaving scope. </summary>
        /// <param name="label"> The node label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Additional flags to control the tree's behaviour. </param>
        /// <returns> A disposable object that evaluates to true if the begun tree node is currently expanded. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal TreeNodeDisposable(scoped ref Utf8LabelHandler label, TreeNodeFlags flags)
        {
            Success = Native.Methods.Tree.TreeNodeEx(label.Start(), flags);
            Alive   = !flags.HasFlag(TreeNodeFlags.NoTreePushOnOpen);
        }

        /// <summary> Push a tree ID and indent without drawing a tree node. </summary>
        /// <param name="id"> The tree ID as a pointer. </param>
        /// <returns> A disposable object that unconditionally evaluates to true. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal TreeNodeDisposable(nint id)
        {
            Native.Methods.Tree.TreePush((void*)id);
            Success = true;
            Alive   = true;
        }

        /// <summary> Push a tree ID and indent without drawing a tree node. </summary>
        /// <param name="id"> The tree ID as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <returns> A disposable object that unconditionally evaluates to true. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal TreeNodeDisposable(scoped ref Utf8LabelHandler id)
        {
            Native.Methods.Tree.TreePush(id.Start());
            Success = true;
            Alive   = true;
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(TreeNodeDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(TreeNodeDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(TreeNodeDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(TreeNodeDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(TreeNodeDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(TreeNodeDisposable i, bool value)
            => i.Success || value;

        /// <summary> Pop the tree node on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            if (Success)
                Native.Methods.Tree.TreePop();
            Alive = false;
        }

        /// <summary> Pop a tree node without using an IDisposable. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void PopUnsafe()
            => Native.Methods.Tree.TreePop();
    }
}
