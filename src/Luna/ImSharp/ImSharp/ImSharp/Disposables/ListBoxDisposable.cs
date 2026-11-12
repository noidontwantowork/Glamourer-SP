namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui list boxes. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct ListBoxDisposable : IDisposable
    {
        /// <summary> Whether creating the list box succeeded and it is expanded. </summary>
        public readonly bool Success;

        /// <summary> Whether the list box is already ended. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a list box and end it on leaving scope. </summary>
        /// <param name="label"> The list box label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="size">
        /// The size of the box. If these values are greater than 0, use them as pixel counts.
        /// If .X == 0, use the current item width.
        /// If .y == 0, use an arbitrary default height of about 7 items.
        /// If they are less than 0, align to the right or bottom respectively.
        /// </param>
        /// <returns> A disposable object that evaluates to true if the begun list box is currently visible. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal ListBoxDisposable(scoped ref Utf8LabelHandler label, Vector2 size)
        {
            Success = Native.Methods.ListBox.BeginListBox(label.Start(), size);
            Alive   = true;
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(ListBoxDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(ListBoxDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(ListBoxDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(ListBoxDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(ListBoxDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(ListBoxDisposable i, bool value)
            => i.Success || value;

        /// <summary> End the list box on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            if (Success)
                Native.Methods.ListBox.EndListBox();

            Alive = false;
        }

        /// <summary> End a list box without using an IDisposable. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.ListBox.EndListBox();
    }
}
