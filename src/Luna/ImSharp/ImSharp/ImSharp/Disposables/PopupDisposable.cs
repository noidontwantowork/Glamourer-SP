using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui popups. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct PopupDisposable : IDisposable
    {
        /// <summary> Whether creating the popup succeeded and it is open. </summary>
        public readonly bool Success;

        /// <summary> Whether the popup is already ended. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a popup and end it on leaving scope. </summary>
        /// <param name="id"> The ID of the popup as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Flags to forward to the popup window creation. </param>
        /// <returns> A disposable object that evaluates to true if the begun popup is currently open. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal PopupDisposable(scoped ref Utf8LabelHandler id, WindowFlags flags = WindowFlags.None)
        {
            Success = Native.Methods.Popup.BeginPopup(id.Start(), flags);
            Alive   = true;
        }

        /// <summary> Open a popup when clicking on the last drawn item and begin it. </summary>
        /// <param name="id"> The ID of the popup. </param>
        /// <param name="flags"> Additional flags to control the popups behavior and the button to click to open it. </param>
        /// <returns> A disposable object that evaluates to true if the begun popup is currently open. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal static PopupDisposable ContextItem(scoped ref Utf8LabelHandler id,
            PopupContextFlags flags = PopupContextFlags.MouseButtonRight)
            => new(Native.Methods.Popup.BeginPopupContextItem(id.Start(), flags));

        /// <summary> Open a popup associated with the last drawn item when clicking on it and begin it. </summary>
        /// <param name="flags"> Additional flags to control the popups behavior and the button to click to open it. </param>
        /// <returns> A disposable object that evaluates to true if the begun popup is currently open. Use with using. </returns>
        /// <remarks> This only works if the last item does have an ID (notably not for <seealso cref="ImSharp.Im.Text(Utf8TextHandler)"/>), otherwise you need to pass an ID for the popup. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal static PopupDisposable ContextItem(PopupContextFlags flags = PopupContextFlags.MouseButtonRight)
            => new(Native.Methods.Popup.BeginPopupContextItem(null, flags));

        /// <summary> Open a popup when clicking on empty space in the current window and begin it. </summary>
        /// <param name="id"> The ID of the popup. </param>
        /// <param name="flags"> Additional flags to control the popups behavior and the button to click to open it. </param>
        /// <returns> A disposable object that evaluates to true if the begun popup is currently open. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal static PopupDisposable ContextWindow(scoped ref Utf8LabelHandler id,
            PopupContextFlags flags = PopupContextFlags.MouseButtonRight)
            => new(Native.Methods.Popup.BeginPopupContextWindow(id.Start(), flags));

        /// <summary> Open a popup when clicking somewhere where no windows are and begin it. </summary>
        /// <param name="id"> The ID of the popup. </param>
        /// <param name="flags"> Additional flags to control the popups behavior and the button to click to open it. </param>
        /// <returns> A disposable object that evaluates to true if the begun popup is currently open. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal static PopupDisposable ContextVoid(scoped ref Utf8LabelHandler id,
            PopupContextFlags flags = PopupContextFlags.MouseButtonRight)
            => new(Native.Methods.Popup.BeginPopupContextVoid(id.Start(), flags));

        /// <summary> Begin a modal popup and end it on leaving scope. </summary>
        /// <param name="title"> The title of the popup as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="open"> Whether the modal should be kept open or closed. </param>
        /// <param name="flags"> Flags to pass to the popup window creation. </param>
        /// <returns> A disposable object that evaluates to true if the begun popup is currently open. Use with using. </returns>
        /// <remarks> Modal popups block interactions behind the popup and can not be closed by the user, add a dimming background and have a title bar. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal static PopupDisposable Modal(scoped ref Utf8LabelHandler title, scoped ref bool open, WindowFlags flags = WindowFlags.None)
            => new(Native.Methods.Popup.BeginPopupModal(title.Start(), (ImBool*)Unsafe.AsPointer(ref open), flags));

        /// <inheritdoc cref="Modal(ref Utf8LabelHandler,ref bool,WindowFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal static PopupDisposable Modal(scoped ref Utf8LabelHandler title, WindowFlags flags = WindowFlags.None)
            => new(Native.Methods.Popup.BeginPopupModal(title.Start(), null, flags));

        /// <summary> Begin a resizable popup and end it on leaving scope. </summary>
        /// <param name="id"> The ID of the popup as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Flags to forward to the popup window creation. </param>
        /// <returns> A disposable object that evaluates to true if the begun popup is currently open. Use with using. </returns>
        internal static PopupDisposable Resizable(scoped ref Utf8LabelHandler id, WindowFlags flags = WindowFlags.None)
        {
            // Copy implementation of BeginPopup.
            if (Context.Pointer->OpenPopupStack.Size <= Context.Pointer->BeginPopupStack.Size)
            {
                Context.Pointer->NextWindowData.Flags = NextWindowDataFlags.None;
                return new PopupDisposable(false);
            }

            flags |= WindowFlags.NoTitleBar | WindowFlags.NoSavedSettings;
            return new PopupDisposable(Native.Methods.Internal.BeginPopupEx(Id.Get(ref id), flags));
        }

        [MethodImpl(ImSharpConfiguration.OptInl)]
        private PopupDisposable(bool success)
        {
            Success = success;
            Alive   = true;
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(PopupDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(PopupDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(PopupDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(PopupDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(PopupDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(PopupDisposable i, bool value)
            => i.Success || value;

        /// <summary> End the popup on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            if (Success)
                Native.Methods.Popup.EndPopup();

            Alive = false;
        }

        /// <summary> End a popup without using an IDisposable. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.Popup.EndPopup();
    }
}
