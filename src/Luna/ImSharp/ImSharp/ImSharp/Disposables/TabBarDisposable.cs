namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui tab bars. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct TabBarDisposable : IDisposable
    {
        /// <summary> Whether creating the tab bar succeeded. </summary>
        public readonly bool Success;

        /// <summary> Whether the tab bar is already ended. </summary>
        public bool Alive { get; private set; }

        /// <inheritdoc cref="TabItemDisposable(ref Utf8LabelHandler,ref bool, TabItemFlags)"/>
        /// <remarks> Only call this if the tab bar object evaluates to true. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly TabItemDisposable Item(Utf8LabelHandler label, ref bool open, TabItemFlags flags = TabItemFlags.None)
        {
            ImGuiStateException.CheckState(Success, Alive, "TabItem", "TabBar");
            return new TabItemDisposable(ref label, ref open, flags);
        }

        /// <inheritdoc cref="TabItemDisposable(ref Utf8LabelHandler, TabItemFlags)"/>
        /// <remarks> Only call this if the tab bar object evaluates to true. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly TabItemDisposable Item(Utf8LabelHandler label, TabItemFlags flags = TabItemFlags.None)
        {
            ImGuiStateException.CheckState(Success, Alive, "TabItem", "TabBar");
            return new TabItemDisposable(ref label, flags);
        }

        /// <summary> Draw a tab-button in the current tab bar. </summary>
        /// <param name="label"> The button label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Additional flags to control the tab button's behaviour. </param>
        /// <returns> True if the button has been clicked in this frame. </returns>
        /// <remarks> Only call this if the tab bar object evaluates to true. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly bool Button(Utf8LabelHandler label, TabItemFlags flags = TabItemFlags.None)
        {
            ImGuiStateException.CheckState(Success, Alive, "TabItemButton", "TabBar");
            return Native.Methods.TabBar.TabItemButton(label.Start(), flags);
        }

        /// <summary> Begin a tab bar and end it on leaving scope. </summary>
        /// <param name="label"> The tab bar label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Additional flags to control the tab bar's behaviour. </param>
        /// <returns> A disposable object that evaluates to true if any part of the begun tab bar is currently visible. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal TabBarDisposable(scoped ref Utf8LabelHandler label, TabBarFlags flags)
        {
            Success = Native.Methods.TabBar.BeginTabBar(label.Start(), flags);
            Alive   = true;
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(TabBarDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(TabBarDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(TabBarDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(TabBarDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(TabBarDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(TabBarDisposable i, bool value)
            => i.Success || value;

        /// <summary> End the tab bar on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            if (Success)
                Native.Methods.TabBar.EndTabBar();
            Alive = false;
        }

        /// <summary> End a tab bar without using an IDisposable. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.TabBar.EndTabBar();
    }
}
