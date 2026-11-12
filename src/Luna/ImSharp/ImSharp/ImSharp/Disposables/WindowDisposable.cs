namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui windows. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public unsafe ref struct WindowDisposable : IDisposable
    {
        /// <summary> Whether creating the window succeeded. </summary>
        public readonly bool Success;

        /// <summary> Whether the window is already ended. </summary>
        public bool Alive { get; private set; }

        /// <inheritdoc cref="WindowDisposable(ref Utf8LabelHandler,ref bool,WindowFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal WindowDisposable(scoped ref Utf8LabelHandler name, WindowFlags flags = WindowFlags.None)
        {
            Success = Im.Native.Methods.Window.Begin(name.Start(), null, flags);
            Alive   = true;
        }

        /// <summary> Open a new ImGui Window. </summary>
        /// <param name="name"> The label of the window as a UTF8 string. HAS to be null-terminated. </param>
        /// <param name="open"> Whether the user used the top-right close button. Omit this to not have this button. </param>
        /// <param name="flags"> Additional flags to control the window's behaviour. </param>
        /// <returns> A disposable object that evaluates to true if any part of the window's interior is visible, i.e. it is not fully obstructed or collapsed. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal WindowDisposable(scoped ref Utf8LabelHandler name, ref bool open, WindowFlags flags = WindowFlags.None)
        {
            Success = Im.Native.Methods.Window.Begin(name.Start(), (ImBool*)Unsafe.AsPointer(ref open), flags);
            Alive   = true;
        }

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator bool(WindowDisposable value)
            => value.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator true(WindowDisposable i)
            => i.Success;

        /// <summary> Conversion to bool. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator false(WindowDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on NOT operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator !(WindowDisposable i)
            => !i.Success;

        /// <summary> Conversion to bool on AND operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator &(WindowDisposable i, bool value)
            => i.Success && value;

        /// <summary> Conversion to bool on OR operators. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator |(WindowDisposable i, bool value)
            => i.Success || value;

        /// <summary> End the window on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            Im.Native.Methods.Window.End();
            Alive = false;
        }

        /// <summary> End a window without using an IDisposable. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Im.Native.Methods.Window.End();
    }
}
