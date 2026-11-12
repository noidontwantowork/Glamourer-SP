#pragma warning disable CS9087 // This returns a parameter by reference but it is not a ref parameter

namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui main menu bars. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct MainMenuBarDisposable : IDisposable
    {
        /// <summary> Whether creating the main menu bar succeeded. </summary>
        public readonly bool Success;

        /// <summary> Whether the main menu bar is already ended. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a main menu bar and end it on leaving scope. </summary>
        /// <returns> A disposable object that evaluates to true if the main menu bar was created. Use with using. </returns>
        /// <remarks> Can create or append to a full screen menu bar at the top of the screen. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal MainMenuBarDisposable(bool _)
        {
            Success = Native.Methods.Menu.BeginMainMenuBar();
            Alive   = true;
        }

        /// <remarks> Only call this if the main menu bar object evaluates to true. </remarks>
        /// <inheritdoc cref="MenuDisposable(ref Utf8LabelHandler,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public MenuDisposable Menu(Utf8LabelHandler label, bool enabled = true)
        {
            ImGuiStateException.CheckState(Success, Alive, "Menu", "MainMenuBar");
            return new MenuDisposable(ref label, enabled);
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(MainMenuBarDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(MainMenuBarDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(MainMenuBarDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(MainMenuBarDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(MainMenuBarDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(MainMenuBarDisposable i, bool value)
            => i.Success || value;

        /// <summary> End the main menu bar on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            if (Success)
                Native.Methods.Menu.EndMainMenuBar();
            Alive = false;
        }

        /// <summary> End a main menu bar without using an IDisposable. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.Menu.EndMainMenuBar();
    }
}
