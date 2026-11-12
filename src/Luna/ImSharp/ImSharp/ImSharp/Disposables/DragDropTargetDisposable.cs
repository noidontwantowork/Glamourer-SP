namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around a ImGui Drag and Drop Target. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct DragDropTargetDisposable : IDisposable
    {
        /// <summary> Whether the currently active drag and drop target is a full window. </summary>
        public static bool WindowTarget;

        /// <summary> Whether creating the drag and drop target succeeded. </summary>
        public readonly bool Success;

        /// <summary> Whether the drag and drop target is already ended. </summary>
        public bool Alive { get; private set; }

        /// <summary> Open a new Drag and Drop target on the last item and close it on leaving scope. </summary>
        /// <returns> A disposable object that indicates whether the target is active. Use with using. </returns>
        /// <remarks> You can use the returned object to check for a specific payload dropping with <see cref="IsDropping(Utf8LabelHandler,DragDropTargetFlags)"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal DragDropTargetDisposable(bool _)
        {
            Success = Native.Methods.DragDrop.BeginDragDropTarget();
            Alive   = true;
            if (Success)
                WindowTarget = false;
        }

        /// <summary> Open a new Drag and Drop target on a given viewport and close it on leaving scope. </summary>
        /// <returns> A disposable object that indicates whether the target is active. Use with using. </returns>
        /// <remarks> You can use the returned object to check for a specific payload dropping with <see cref="IsDropping(Utf8LabelHandler,DragDropTargetFlags)"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal DragDropTargetDisposable(bool _, bool _2)
        {
            Success = BeginDragDropTargetViewport(Viewport.Main);
            Alive   = true;
        }

        /// <summary> Open a new Drag and Drop target on a given window and close it on leaving scope. </summary>
        /// <returns> A disposable object that indicates whether the target is active. Use with using. </returns>
        /// <remarks> You can use the returned object to check for a specific payload dropping with <see cref="IsDropping(Utf8LabelHandler,DragDropTargetFlags)"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal DragDropTargetDisposable(bool _, bool _2, bool _3)
        {
            ref var window = ref *Window.Current.Pointer;
            var     rect   = ImRect.FromSize(window.Position, window.Size);
            Success = Native.Methods.Internal.DragDropTargetCustom(rect, window.Id);
            Alive   = true;
            if (Success)
                WindowTarget = true;
        }

        /// <summary> Open a new Drag and Drop target on the last item and close it on leaving scope. Use this if the item bound rectangle leaves the window. </summary>
        /// <returns> A disposable object that indicates whether the target is active. Use with using. </returns>
        /// <remarks> You can use the returned object to check for a specific payload dropping with <see cref="IsDropping(Utf8LabelHandler,DragDropTargetFlags)"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal DragDropTargetDisposable(bool _, bool _2, bool _3, bool _4)
        {
            Success = Native.Methods.DragDrop.BeginDragDropTarget();
            Alive   = true;
            if (Success)
                WindowTarget = true;
        }

        /// <remarks> This is copied from a new method implemented in imgui as of october 2025. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        private static bool BeginDragDropTargetViewport(Viewport viewport)
        {
            if (Window.Hovered(HoveredFlags.AnyWindow | HoveredFlags.AllowWhenBlockedByActiveItem))
                return false;

            if (!Context.DragDropActive)
                return false;

            var rect = ImRect.FromSize(viewport.Pointer->WorkPos, viewport.Pointer->WorkSize);
            var id   = viewport.Pointer->Id;
            if (!Mouse.IsHoveringRectangle(rect, false) || id == Context.Pointer->DragDropPayload.SourceId)
                return false;

            var g = Context.Pointer;
            g->DragDropTargetRect   = rect.Increase(-3.5f);
            g->DragDropTargetId     = id;
            g->DragDropWithinTarget = true;
            WindowTarget            = false;
            return true;
        }

        /// <summary> Check whether a specific payload is currently dropping on this target. </summary>
        /// <param name="type"> A user-defined string of at most 32 characters as text. If this is a UTF8 string, it HAS to be null-terminated. It should not start with '_' as those are reserved for ImGui internals. </param>
        /// <param name="flags"> Additional flags to control the payload checking. </param>
        /// <returns> True if the specified payload is currently dropping. </returns>
        /// <remarks> Use <seealso cref="Im.DragDrop.TryAcceptPayload(Utf8LabelHandler,DragDropTargetFlags,out Payload)"/> instead if you need the actual payload. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool IsDropping(Utf8LabelHandler type, DragDropTargetFlags flags = DragDropTargetFlags.None)
        {
            if (!Success)
                return false;

            using var clip = WindowTarget ? Window.DrawList.PushClipRectFullScreen() : null;
            return Native.Methods.DragDrop.AcceptDragDropPayload(type.Start(), flags) is not null;
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(DragDropTargetDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(DragDropTargetDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(DragDropTargetDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(DragDropTargetDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(DragDropTargetDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(DragDropTargetDisposable i, bool value)
            => i.Success || value;

        /// <summary> End the drag and drop target on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            if (Success)
                Native.Methods.DragDrop.EndDragDropTarget();
            Alive = false;
        }

        /// <summary> End a drag and drop target without using an IDisposable.</summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.DragDrop.EndDragDropTarget();
    }
}
