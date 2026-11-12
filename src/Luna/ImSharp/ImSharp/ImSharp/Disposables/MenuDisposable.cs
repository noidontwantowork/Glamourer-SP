#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)

namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui menus. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct MenuDisposable : IDisposable
    {
        /// <summary> Whether creating the menu bar succeeded. </summary>
        public readonly bool Success;

        /// <summary> Whether the menu is already ended. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a menu and end it on leaving scope. </summary>
        /// <param name="label"> The label of the menu as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="enabled"> Whether the menu is enabled or not. </param>
        /// <returns> A disposable object that evaluates to true if the menu was created. Use with using. </returns>
        /// <remarks> Can create or append to a menu that already exists in a menu bar. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal MenuDisposable(scoped ref Utf8LabelHandler label, bool enabled)
        {
            Success = Native.Methods.Menu.BeginMenu(label.Start(), enabled);
            Alive   = true;
        }

        /// <summary> Create a menu item. </summary>
        /// <param name="label"> The label of the item as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="shortcut"> Shortcuts highlighted in the menu as text. Note that ImGui does NOT process those by itself, it only highlights them. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="selected"> Whether the menu item should be shown as selected. </param>
        /// <param name="enabled"> Whether the menu item is enabled or not. </param>
        /// <returns> True when the item was activated this frame, false otherwise. </returns>
        /// <remarks> Only call this if the menu object evaluates to true. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool Item(Utf8LabelHandler label, Utf8HintHandler shortcut, bool selected = false, bool enabled = true)
        {
            ImGuiStateException.CheckState(Success, Alive, "MenuItem", "Menu");
            return Native.Methods.Menu.MenuItem(label.Start(), shortcut.Start(), selected, enabled);
        }

        /// <inheritdoc cref="Item(Utf8LabelHandler,Utf8HintHandler,bool,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool Item(Utf8LabelHandler label, bool selected = false, bool enabled = true)
        {
            ImGuiStateException.CheckState(Success, Alive, "MenuItem", "Menu");
            return Native.Methods.Menu.MenuItem(label.Start(), null, selected, enabled);
        }

        /// <param name="selected"> Whether the menu item should be shown as selected, which will be toggled if this returns true. </param>
        /// <returns> True when the item was activated this frame. </returns>
        /// <inheritdoc cref="Item(Utf8LabelHandler,Utf8HintHandler,bool,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool Item(Utf8LabelHandler label, Utf8HintHandler shortcut, ref bool selected, bool enabled = true)
        {
            ImGuiStateException.CheckState(Success, Alive, "MenuItem", "Menu");
            return Native.Methods.Menu.MenuItem(label.Start(), shortcut.Start(), (ImBool*)Unsafe.AsPointer(ref selected), enabled);
        }

        /// <inheritdoc cref="Item(Utf8LabelHandler,Utf8HintHandler,ref bool,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool Item(Utf8LabelHandler label, ref bool selected, bool enabled = true)
        {
            ImGuiStateException.CheckState(Success, Alive, "MenuItem", "Menu");
            return Native.Methods.Menu.MenuItem(label.Start(), null, (ImBool*)Unsafe.AsPointer(ref selected), enabled);
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(MenuDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(MenuDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(MenuDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(MenuDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(MenuDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(MenuDisposable i, bool value)
            => i.Success || value;

        /// <summary> End the menu on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            if (Success)
                Native.Methods.Menu.EndMenu();
            Alive = false;
        }

        /// <summary> End a menu without using an IDisposable. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.Menu.EndMenu();
    }
}
