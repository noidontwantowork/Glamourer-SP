namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui collapsing headers that also push an ID. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct HeaderDisposable : IDisposable
    {
        /// <summary> Whether the collapsing header is currently opened. </summary>
        public readonly bool Success;

        /// <summary> Whether the ID node is already popped. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a collapsing header and push its label as an ID if it is open. </summary>
        /// <param name="label"> The header label and ID as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Additional flags to control the header's behaviour. </param>
        /// <returns> A disposable object that evaluates to true if the collapsing header is currently open. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal HeaderDisposable(scoped ref Utf8LabelHandler label, TreeNodeFlags flags)
        {
            var text = label.Start();
            Success = Native.Methods.Tree.CollapsingHeader(text, flags);
            if (Success)
                Native.Methods.IdStack.PushId(text);
            Alive = true;
        }

        /// <summary> Begin a collapsing header and push its label as an ID if it is open. </summary>
        /// <param name="label"> The header label and ID as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="visible"> If true, displays a small close button on the upper right of the header, which will set this to false when clicked. If false, do not display the header at all. </param>
        /// <param name="flags"> Additional flags to control the header's behaviour. </param>
        /// <returns> A disposable object that evaluates to true if the collapsing header is currently open. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal HeaderDisposable(scoped ref Utf8LabelHandler label, scoped ref bool visible, TreeNodeFlags flags)
        {
            var text = label.Start();
            Success = Native.Methods.Tree.CollapsingHeader(text, (ImBool*)Unsafe.AsPointer(ref visible), flags);
            if (Success)
                Native.Methods.IdStack.PushId(text);
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(HeaderDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(HeaderDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(HeaderDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(HeaderDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(HeaderDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(HeaderDisposable i, bool value)
            => i.Success || value;

        /// <summary> Pop the tree node on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            if (Success)
                Native.Methods.IdStack.PopId();
            Alive = false;
        }
    }
}
