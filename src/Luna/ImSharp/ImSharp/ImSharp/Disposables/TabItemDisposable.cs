namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui tab items. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct TabItemDisposable : IDisposable
    {
        /// <summary> Whether creating the tab item succeeded, and it is currently selected. </summary>
        public readonly bool Success;

        /// <summary> Whether the tab item is already ended. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a tab item and end it on leaving scope. </summary>
        /// <param name="label"> The tab item label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="open"> Whether the tab item is currently open. If this is provided, the tab item will render a close button that controls this value. </param>
        /// <param name="flags"> Additional flags to control the tab item's behaviour. </param>
        /// <returns> A disposable object that evaluates to true if the tab item is currently opened. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal TabItemDisposable(scoped ref Utf8LabelHandler label, scoped ref bool open, TabItemFlags flags)
        {
            Success = Native.Methods.TabBar.BeginTabItem(label.Start(), (byte*)Unsafe.AsPointer(ref open), flags);
            Alive   = true;
        }

        /// <inheritdoc cref="TabItemDisposable(ref Utf8LabelHandler,ref bool,TabItemFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal TabItemDisposable(scoped ref Utf8LabelHandler label, TabItemFlags flags)
        {
            Success = Native.Methods.TabBar.BeginTabItem(label.Start(), null, flags);
            Alive   = true;
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(TabItemDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(TabItemDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(TabItemDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(TabItemDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(TabItemDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(TabItemDisposable i, bool value)
            => i.Success || value;

        /// <summary> End the tab item on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            if (Success)
                Native.Methods.TabBar.EndTabItem();
            Alive = false;
        }

        /// <summary> End a tab item without using an IDisposable. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.TabBar.EndTabItem();
    }
}
