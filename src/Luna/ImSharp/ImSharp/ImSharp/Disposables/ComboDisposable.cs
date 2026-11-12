namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui combos. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct ComboDisposable : IDisposable
    {
        /// <summary> Whether creating the combo box succeeded and it is expanded. </summary>
        public readonly bool Success;

        /// <summary> Whether the combo box is already ended. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a combo field and end it on leaving scope. </summary>
        /// <param name="label"> The combo label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="preview"> The currently displayed string in the combo field as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Additional flags to control the combo's behaviour. </param>
        /// <returns> A disposable object that evaluates to true if the begun combo popup is currently open. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal ComboDisposable(scoped ref Utf8LabelHandler label, scoped ref Utf8TextHandler preview, ComboFlags flags)
        {
            Success = Native.Methods.Widgets.BeginCombo(label.Start(), preview.Start(), flags);
            Alive   = true;
        }

        /// <summary> Begin a combo popup and end it on leaving scope. </summary>
        /// <param name="id"> The combos popup ID as provided by <see cref="Im.Combo.DrawPreview"/>. </param>
        /// <param name="boundingBox"> The combo preview's bounding box as provided by <see cref="Im.Combo.DrawPreview"/>. </param>
        /// <param name="flags"> Additional flags to control the combo's behaviour. </param>
        /// <returns> A disposable object that evaluates to true if the begun combo popup is currently open. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal ComboDisposable(ImGuiId id, in Rectangle boundingBox, ComboFlags flags)
        {
            Success = Native.Methods.Internal.BeginComboPopup(id, boundingBox, flags);
            Alive   = true;
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(ComboDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(ComboDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(ComboDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(ComboDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(ComboDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(ComboDisposable i, bool value)
            => i.Success || value;

        /// <summary> End the combo box on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            if (Success)
                Native.Methods.Widgets.EndCombo();

            Alive = false;
        }

        /// <summary> End a combo box without using an IDisposable. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.Widgets.EndCombo();
    }
}
