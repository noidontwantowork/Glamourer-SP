namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper class for Popup functions. </summary>
    public static unsafe class Popup
    {
        /// <summary> Set the state of a popup to open. </summary>
        /// <param name="id"> The ID of the popup as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Additional flags to control the popups behavior. </param>
        /// <remarks> The ID is relative to the current ID stack, so this should be called at the same level of the stack as <seealso cref="Begin"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void Open(Utf8LabelHandler id, PopupFlags flags = PopupFlags.None)
            => Native.Methods.Popup.OpenPopup(id.Start(), flags);

        /// <summary> Set the state of a popup to open. </summary>
        /// <param name="id"> The ID of the popup. </param>
        /// <param name="flags"> Additional flags to control the popups behavior. </param>
        /// <remarks> The ID is relative to the current ID stack, so this should be called at the same level of the stack as <seealso cref="Begin"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void Open(ImGuiId id, PopupFlags flags = PopupFlags.None)
            => Native.Methods.Popup.OpenPopup(id, flags);

        /// <summary> Opens a popup when the last drawn item is clicked. </summary>
        /// <param name="id"> The ID of the popup as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Additional flags to control the popups behavior and the button to click to open it. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void OpenOnItemClick(Utf8LabelHandler id, PopupContextFlags flags = PopupContextFlags.MouseButtonRight)
            => Native.Methods.Popup.OpenPopupOnItemClick(id.Start(), flags);

        /// <summary> Query whether a popup is currently open. </summary>
        /// <param name="id"> The ID of the popup as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> The type of query to check for. </param>
        /// <returns> Whether a popup defined by the flags and ID is currently open. </returns>
        /// <remarks> The ID is relative to the current ID stack, so this should be called at the same level of the stack as <seealso cref="Open(Utf8LabelHandler,PopupFlags)"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsOpen(Utf8LabelHandler id, PopupQueryFlags flags = PopupQueryFlags.None)
            => Native.Methods.Popup.IsPopupOpen(id.Start(), flags);

        /// <summary> Query whether a popup is currently open. </summary>
        /// <param name="id"> The absolute ID of the popup as text. </param>
        /// <param name="flags"> The type of query to check for. </param>
        /// <returns> Whether a popup defined by the flags and ID is currently open. </returns>
        /// <remarks> The ID is NOT relative to the current ID stack. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsOpen(ImGuiId id, PopupQueryFlags flags = PopupQueryFlags.None)
            => Native.Methods.Internal.IsPopupOpen(id, flags);

        /// <summary> Close the currently open popup inside a <seealso cref="Begin"/> scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void CloseCurrent()
            => Native.Methods.Popup.CloseCurrentPopup();

        /// <inheritdoc cref="PopupDisposable.ContextItem(ref Utf8LabelHandler,PopupContextFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static PopupDisposable BeginContextItem(Utf8LabelHandler id, PopupContextFlags flags = PopupContextFlags.MouseButtonRight)
            => PopupDisposable.ContextItem(ref id, flags);

        /// <inheritdoc cref="PopupDisposable.ContextItem(PopupContextFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static PopupDisposable BeginContextItem(PopupContextFlags flags = PopupContextFlags.MouseButtonRight)
            => PopupDisposable.ContextItem(flags);

        /// <inheritdoc cref="PopupDisposable.ContextWindow"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static PopupDisposable BeginContextWindow(Utf8LabelHandler id, PopupContextFlags flags = PopupContextFlags.MouseButtonDefault)
            => PopupDisposable.ContextWindow(ref id, flags);

        /// <inheritdoc cref="PopupDisposable.ContextVoid"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static PopupDisposable BeginContextVoid(Utf8LabelHandler id, PopupContextFlags flags = PopupContextFlags.MouseButtonDefault)
            => PopupDisposable.ContextVoid(ref id, flags);

        /// <inheritdoc cref="PopupDisposable(ref Utf8LabelHandler,WindowFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static PopupDisposable Begin(Utf8LabelHandler id, WindowFlags flags = WindowFlags.None)
            => new(ref id, flags);

        /// <inheritdoc cref="PopupDisposable.Modal(ref Utf8LabelHandler,ref bool,WindowFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static PopupDisposable BeginModal(Utf8LabelHandler id, ref bool open, WindowFlags flags = WindowFlags.None)
            => PopupDisposable.Modal(ref id, ref open, flags);

        /// <inheritdoc cref="PopupDisposable.Modal(ref Utf8LabelHandler,WindowFlags)"/>
        public static PopupDisposable BeginModal(Utf8LabelHandler id, WindowFlags flags = WindowFlags.None)
            => PopupDisposable.Modal(ref id, flags);

        /// <inheritdoc cref="PopupDisposable.Resizable(ref Utf8LabelHandler,WindowFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static PopupDisposable BeginResizable(Utf8LabelHandler id, WindowFlags flags = WindowFlags.None)
            => PopupDisposable.Resizable(ref id, flags);
    }
}
