namespace ImSharp;

public static partial class Im
{
    /// <summary> Wrapper class for Drag and Drop methods. </summary>
    public static class DragDrop
    {
        /// <inheritdoc cref="DragDropSourceDisposable(DragDropSourceFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static DragDropSourceDisposable Source(DragDropSourceFlags flags = DragDropSourceFlags.None)
            => new(flags);

        /// <inheritdoc cref="DragDropTargetDisposable(bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static DragDropTargetDisposable Target()
            => new(true);

        /// <inheritdoc cref="DragDropTargetDisposable(bool,bool,bool)"/>
        public static DragDropTargetDisposable TargetUnclipped()
            => new(true, true, true, true);

        /// <inheritdoc cref="DragDropTargetDisposable(bool,bool)"/>
        public static DragDropTargetDisposable TargetViewport()
            => new(true, true);

        /// <inheritdoc cref="DragDropTargetDisposable(bool,bool,bool)"/>
        public static DragDropTargetDisposable TargetWindow()
            => new(true, true, true);

        /// <inheritdoc cref="DragDropSourceDisposable.SetPayload(Utf8LabelHandler,Condition)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool SetPayload(Utf8LabelHandler id, Condition condition = Condition.None)
            => Native.Methods.DragDrop.SetDragDropPayload(id.Start(), null, 0, condition);

        /// <inheritdoc cref="DragDropSourceDisposable.SetPayload{T}(Utf8LabelHandler,T,Condition)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool SetPayload<T>(Utf8LabelHandler id, T data, Condition condition = Condition.None) where T : unmanaged
            => Native.Methods.DragDrop.SetDragDropPayload(id.Start(), &data, (ulong)sizeof(T), condition);

        /// <summary> Lok at the current payload without accepting it, if any is set. </summary>
        /// <returns> A reference to the current payload which can be invalid. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe Payload PeekPayload()
            => Native.Methods.DragDrop.GetDragDropPayload();

        /// <summary> Try to look at the current payload without accepting it, if any is set. </summary>
        /// <param name="payload"> A reference to the current payload on success. </param>
        /// <returns> True if ImGui currently has a payload and <paramref name="payload"/> is populated. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool TryPeekPayload(out Payload payload)
        {
            payload = Native.Methods.DragDrop.GetDragDropPayload();
            return payload.Valid;
        }

        /// <summary> Accept a specific payload, if the current payload matches the type. </summary>
        /// <param name="type"> A user-defined string of at most 32 characters as text. If this is a UTF8 string, it HAS to be null-terminated.
        /// It should not start with '_' as those are reserved for ImGui internals. </param>
        /// <param name="flags"> Additional flags to control the payload checking. </param>
        /// <returns> A reference to the accepted payload which can be invalid. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe Payload AcceptPayload(Utf8LabelHandler type, DragDropTargetFlags flags = DragDropTargetFlags.None)
            => Native.Methods.DragDrop.AcceptDragDropPayload(type.Start(), flags);

        /// <summary> Try to accept a specific payload, if the current payload matches the type. </summary>
        /// <param name="type"> A user-defined string of at most 32 characters as text. If this is a UTF8 string, it HAS to be null-terminated.
        /// It should not start with '_' as those are reserved for ImGui internals. </param>
        /// <param name="flags"> Additional flags to control the payload checking. </param>
        /// <param name="payload"> A reference to the accepted payload on success. </param>
        /// <returns> True if the payload was successfully accepted and <paramref name="payload"/> is populated. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool TryAcceptPayload(Utf8LabelHandler type, DragDropTargetFlags flags, out Payload payload)
        {
            payload = Native.Methods.DragDrop.AcceptDragDropPayload(type.Start(), flags);
            return payload.Valid;
        }

        /// <inheritdoc cref="TryAcceptPayload(Utf8LabelHandler,DragDropTargetFlags,out Payload)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool TryAcceptPayload(Utf8LabelHandler type, out Payload payload)
        {
            payload = Native.Methods.DragDrop.AcceptDragDropPayload(type.Start(), DragDropTargetFlags.None);
            return payload.Valid;
        }
    }
}
