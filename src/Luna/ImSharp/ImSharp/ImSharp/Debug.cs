namespace ImSharp;

public static partial class Im
{
    /// <summary> Wrapper class for calling debug utility functions of ImGui. </summary>
    public static unsafe class Debug
    {
        /// <summary> Show a window demonstrating most ImGui features. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ShowDemoWindow()
            => Native.Methods.Information.ShowDemoWindow(null);

        /// <summary> Show a window demonstrating most ImGui features with a close-toggle. </summary>
        /// <param name="open"> Input/Output of the open-state of the window. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ShowDemoWindow(ref bool open)
            => Native.Methods.Information.ShowDemoWindow((ImBool*)Unsafe.AsPointer(ref open));

        /// <summary> Show a metrics and debugging window that displays most of ImGuis internal state. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ShowMetricsWindow()
            => Native.Methods.Information.ShowMetricsWindow(null);

        /// <summary> Show a metrics and debugging window that displays most of ImGuis internal state with a close-toggle. </summary>
        /// <param name="open"> Input/Output of the open-state of the window. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ShowMetricsWindow(ref bool open)
            => Native.Methods.Information.ShowMetricsWindow((ImBool*)Unsafe.AsPointer(ref open));

        /// <summary> Show a window containing a simplified debug log of important ImGui events. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ShowDebugLogWindow()
            => Native.Methods.Information.ShowDebugLogWindow(null);

        /// <summary> Show a window containing a simplified debug log of important ImGui events with a close toggle. </summary>
        /// <param name="open"> Input/Output of the open-state of the window. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ShowDebugLogWindow(ref bool open)
            => Native.Methods.Information.ShowDebugLogWindow((ImBool*)Unsafe.AsPointer(ref open));

        /// <summary> Show a window that displays information about items and state stacks when hovering over them. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ShowStackToolWindow()
            => Native.Methods.Information.ShowStackToolWindow(null);

        /// <summary> Show a window that displays information about items and state stacks when hovering over them with a close toggle </summary>
        /// <param name="open"> Input/Output of the open-state of the window. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ShowStackToolWindow(ref bool open)
            => Native.Methods.Information.ShowStackToolWindow((ImBool*)Unsafe.AsPointer(ref open));

        /// <summary> Show the ImGui About window displaying version and build information and credits. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ShowAboutWindow()
            => Native.Methods.Information.ShowAboutWindow(null);

        /// <summary> Show the ImGui About window displaying version and build information and credits, with a close toggle. </summary>
        /// <param name="open"> Input/Output of the open-state of the window. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ShowAboutWindow(ref bool open)
            => Native.Methods.Information.ShowAboutWindow((ImBool*)Unsafe.AsPointer(ref open));

        /// <summary> Get the english name of a key. </summary>
        /// <param name="key"> The queried key. </param>
        /// <returns> A null-terminated, unowned UTF8 string containing the name. </returns>
        /// <remarks> The key names are not guaranteed to stay consistent across versions and should not be used for comparisons. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe ReadOnlySpan<byte> GetKeyName(Key key)
            => MemoryMarshal.CreateReadOnlySpanFromNullTerminated(Native.Methods.KeyState.GetKeyName(key));

        /// <summary> Get the english name of a key. </summary>
        /// <param name="key"> The queried key. </param>
        /// <returns> A copied, owned UTF8 string containing the name. </returns>
        /// <remarks> The key names are not guaranteed to stay consistent across versions and should not be used for comparisons. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe StringU8 GetKeyNameOwned(Key key)
            => NullTerminationHelpers.GetClone(Native.Methods.KeyState.GetKeyName(key));

        /// <summary> Get the english name of a key. </summary>
        /// <param name="key"> The queried key. </param>
        /// <returns> A transcoded UTF16 string containing the name. </returns>
        /// <remarks> The key names are not guaranteed to stay consistent across versions and should not be used for comparisons. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe string GetKeyNameUtf16(Key key)
            => NullTerminationHelpers.GetString(Native.Methods.KeyState.GetKeyName(key));
    }
}
