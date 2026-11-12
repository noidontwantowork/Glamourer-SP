#pragma warning disable CS9087 // This returns a parameter by reference but it is not a ref parameter

namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui menu bars. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct MenuBarDisposable : IDisposable
    {
        /// <summary> Whether creating the menu bar succeeded. </summary>
        public readonly bool Success;

        /// <summary> Whether the menu bar is already ended. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a menu bar and end it on leaving scope. </summary>
        /// <returns> A disposable object that evaluates to true if the main menu bar was created. Use with using. </returns>
        /// <remarks> Can create or append to a window that has <seealso cref="WindowFlags.MenuBar"/> set. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal MenuBarDisposable(bool _)
        {
            Success = Native.Methods.Menu.BeginMenuBar();
            Alive   = true;
        }

        /// <remarks> Only call this if the main menu bar object evaluates to true. </remarks>
        /// <inheritdoc cref="ImSharp.Im.MenuDisposable"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public readonly MenuDisposable Menu(Utf8LabelHandler label, bool enabled = true)
        {
            ImGuiStateException.CheckState(Success, Alive, "Menu", "MenuBar");
            return new MenuDisposable(ref label, enabled);
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(MenuBarDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(MenuBarDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(MenuBarDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(MenuBarDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(MenuBarDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(MenuBarDisposable i, bool value)
            => i.Success || value;

        /// <summary> End the menu bar on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            if (Success)
                Native.Methods.Menu.EndMenuBar();
            Alive = false;
        }

        /// <summary> End a menu bar without using an IDisposable. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.Menu.EndMenuBar();
    }
}
