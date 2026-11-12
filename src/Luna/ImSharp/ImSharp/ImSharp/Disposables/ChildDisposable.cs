namespace ImSharp;

#pragma warning disable CS1573
public static partial class Im
{
    /// <summary> A wrapper around ImGui child windows. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct ChildDisposable : IDisposable
    {
        /// <summary> Whether creating the child window succeeded and it is at least partly visible. </summary>
        public readonly bool Success;

        /// <summary> Whether the child window is already ended. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a child and end it on leaving scope. </summary>
        /// <param name="id"> The ID of the child as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="size"> The desired size of the child. </param>
        /// <param name="border"> Whether the child should be framed by a border. </param>
        /// <param name="flags"> Additional flags for the child. </param>
        /// <returns> A disposable object that evaluates to true if any part of the begun child is currently visible. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal ChildDisposable(scoped ref Utf8LabelHandler id, Vector2 size, bool border, WindowFlags flags)
        {
            Success = Native.Methods.Window.BeginChild(id.Start(), size, border, flags);
            Alive   = true;
        }

        /// <param name="id"> The ID of the child. </param>
        /// <inheritdoc cref="ChildDisposable(ref Utf8LabelHandler,Vector2,bool,WindowFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal ChildDisposable(ImGuiId id, Vector2 size, bool border, WindowFlags flags)
        {
            Success = Native.Methods.Window.BeginChild(id.Id, size, border, flags);
            Alive   = true;
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(ChildDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(ChildDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(ChildDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(ChildDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(ChildDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(ChildDisposable i, bool value)
            => i.Success || value;

        /// <summary> End the child window on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            Native.Methods.Window.EndChild();
            Alive = false;
        }

        /// <summary> End a child window without using an IDisposable.</summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.Window.EndChild();
    }
}
