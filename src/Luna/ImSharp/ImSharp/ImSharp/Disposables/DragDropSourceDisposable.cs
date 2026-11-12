namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around a ImGui Drag and Drop Source. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ref struct DragDropSourceDisposable : IDisposable
    {
        /// <summary> Whether creating the drag and drop source succeeded. </summary>
        public readonly bool Success;

        /// <summary> Whether the drag and drop source is already ended. </summary>
        public bool Alive { get; private set; }


        /// <summary> Open a new Drag and Drop source on the last item and close it on leaving scope. </summary>
        /// <param name="flags"> Additional flags to control the drag and drop behaviour. </param>
        /// <returns> A disposable object that indicates whether the source is active. Use with using. </returns>
        /// <remarks>
        /// You can draw things inside the source scope for a tooltip,
        /// and you should set a labeled payload from the returned object on success via <see cref="SetPayload(Utf8LabelHandler,Condition)"/>.
        /// </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal DragDropSourceDisposable(DragDropSourceFlags flags = DragDropSourceFlags.None)
        {
            Success = Native.Methods.DragDrop.BeginDragDropSource(flags);
            Alive   = true;
        }

        /// <summary> Simulate a payload for a drag and drop source, without actually setting ImGui-managed data. </summary>
        /// <param name="type"> A user-defined string of at most 32 characters as text. If this is a UTF8 string, it HAS to be null-terminated. It should not start with '_' as those are reserved for ImGui internals. </param>
        /// <param name="condition"> Conditions on when to set the payload. </param>
        /// <returns> True when the payload is accepted. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe bool SetPayload(Utf8LabelHandler type, Condition condition = Condition.None)
            => Native.Methods.DragDrop.SetDragDropPayload(type.Start(), null, 0, condition);

        /// <summary> Set payload for a drag and drop source managed by ImGui. </summary>
        /// <typeparam name="T"> An arbitrary unmanaged type for the payload. </typeparam>
        /// <param name="type"> A user-defined string of at most 32 characters as text. If this is a UTF8 string, it HAS to be null-terminated. It should not start with '_' as those are reserved for ImGui internals. </param>
        /// <param name="data"> The input data which will be copied byte-wise into ImGui storage and managed by it. </param>
        /// <param name="condition"> Conditions on when to set the payload. </param>
        /// <returns> True when the payload is accepted. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe bool SetPayload<T>(Utf8LabelHandler type, T data, Condition condition = Condition.None) where T : unmanaged
            => Native.Methods.DragDrop.SetDragDropPayload(type.Start(), &data, (ulong)sizeof(T), condition);

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(DragDropSourceDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(DragDropSourceDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(DragDropSourceDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(DragDropSourceDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(DragDropSourceDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(DragDropSourceDisposable i, bool value)
            => i.Success || value;

        /// <summary> End the drag and drop source on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            if (Success)
                Native.Methods.DragDrop.EndDragDropSource();
            Alive = false;
        }

        /// <summary> End a drag and drop source without using an IDisposable.</summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.DragDrop.EndDragDropSource();
    }
}
