namespace ImSharp;

public static partial class Im
{
    /// <summary> Wrapper class for methods querying or setting current or next window state. </summary>
    /// <param name="pointer"> The native pointer to the draw list. </param>
    public readonly unsafe ref struct Window(Native.Internal.Window* pointer)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Native.Internal.Window* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator Window(Native.Internal.Window* pointer)
            => new(pointer);

        /// <inheritdoc cref="WindowDisposable(ref Utf8LabelHandler,WindowFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static WindowDisposable Begin(Utf8LabelHandler name, WindowFlags flags = WindowFlags.None)
            => new(ref name, flags);

        /// <inheritdoc cref="WindowDisposable(ref Utf8LabelHandler,ref bool,WindowFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static WindowDisposable Begin(Utf8LabelHandler name, ref bool open, WindowFlags flags = WindowFlags.None)
            => new(ref name, ref open, flags);

        /// <summary> Set the position of the next window to be drawn. </summary>
        /// <param name="position"> The desired position on the screen in pixels. </param>
        /// <param name="condition"> Conditions for setting the position. </param>
        /// <param name="pivot"> A pivot, e.g. use (0.5, 0.5) to center the window on the given point instead of it being the top-left corner. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetNextPosition(Vector2 position, Condition condition = Condition.None, ImVec2 pivot = default)
            => Native.Methods.Window.SetNextWindowPos(position, condition, pivot);

        /// <summary> Set the size of the next window to be drawn. </summary>
        /// <param name="size"> The desired size in pixels. </param>
        /// <param name="condition"> Conditions for setting the size. </param>
        /// <remarks> Setting an axis to 0 makes this axis auto-fit to the content. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetNextSize(Vector2 size, Condition condition = Condition.None)
            => Native.Methods.Window.SetNextWindowSize(size, condition);

        /// <summary> Set the minimum and maximum size the user can resize the next window to be drawn to. </summary>
        /// <param name="minSize"> The minimum size. </param>
        /// <param name="maxSize"> The maximum size. </param>
        /// <remarks> Using 0 or <seealso cref="float.MaxValue"/> means no limit in that axis respectively. Setting -1 for both minimum and maximum in an axis preserves the current size. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetNextSizeConstraints(Vector2 minSize, Vector2 maxSize)
            => Native.Methods.Window.SetNextWindowSizeConstraints(minSize, maxSize, null, null);

        /// <summary> Set the content size (the scrollable client area) of the next window to be drawn. </summary>
        /// <param name="size"> The content size, enforcing the range of scrollbars and ignoring decorations and padding. </param>
        /// <remarks> Leave an axis at 0 to keep it automatic. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetNextContentSize(Vector2 size)
            => Native.Methods.Window.SetNextWindowContentSize(size);

        /// <summary> Force the collapsed state of next window to be drawn. </summary>
        /// <param name="collapsed"> The desired collapsed state. </param>
        /// <param name="condition"> Conditions for setting the state. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetNextCollapsed(bool collapsed = true, Condition condition = Condition.None)
            => Native.Methods.Window.SetNextWindowCollapsed(collapsed, condition);

        /// <summary> Focus the next window to be drawn. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void FocusNext()
            => Native.Methods.Window.SetNextWindowFocus();

        /// <summary> Set the scrolling values of the next window. </summary>
        /// <param name="scroll"> The desired scrolling values. </param>
        /// <remarks> Use negative values to ignore an axis. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetNextScroll(Vector2 scroll)
            => Native.Methods.Internal.SetNextWindowScroll(scroll);

        /// <summary> Set the background color alpha of the next window drawn. </summary>
        /// <param name="alpha"> The alpha value in [0, 1]. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetNextBackgroundAlpha(float alpha)
            => Native.Methods.Window.SetNextWindowBgAlpha(alpha);

        /// <summary> Set the viewport used for the next window drawn. </summary>
        /// <param name="viewportId"> The ID of the desired viewport. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetNextViewport(ImGuiId viewportId)
            => Native.Methods.Window.SetNextWindowViewport(viewportId);

        /// <summary> Whether the current window appeared for the first time in this frame. </summary>
        public static bool Appearing
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Window.IsWindowAppearing();
        }

        /// <summary> Whether the current window is collapsed. </summary>
        public static bool Collapsed
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Window.IsWindowCollapsed();
        }

        /// <summary> Whether the current window is collapsed. </summary>
        /// <param name="flags"> Flags to control the focus check. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Focused(FocusedFlags flags = FocusedFlags.None)
            => Native.Methods.Window.IsWindowFocused(flags);

        /// <summary> Whether the current window is hovered by the mouse. </summary>
        /// <param name="flags"> Flags to control the hover check. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Hovered(HoveredFlags flags = HoveredFlags.None)
            => Native.Methods.Window.IsWindowHovered(flags);

        /// <summary> Get a reference to the draw list of the current window. </summary>
        public static DrawList DrawList
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Window.GetWindowDrawList();
        }

        /// <summary> Get the DPI scale associated with the current window's viewport. </summary>
        public static float DpiScale
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Window.GetWindowDpiScale();
        }

        /// <summary> Get the position of the current window in screen space. </summary>
        /// <remarks> This should rarely be useful. </remarks>

        public static Vector2 Position
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImVec2 ret;
                Native.Methods.Window.GetWindowPos(&ret);
                return ret;
            }
        }

        /// <summary> Get the size of the current window in pixels. </summary>
        /// <remarks> This should rarely be useful. </remarks>
        public static Vector2 Size
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImVec2 ret;
                Native.Methods.Window.GetWindowSize(&ret);
                return ret;
            }
        }

        /// <summary> Get the width of the current window in pixels. </summary>
        /// <remarks> This should rarely be useful. </remarks>
        public static float Width
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Window.GetWindowWidth();
        }

        /// <summary> Get the height of the current window in pixels. </summary>
        /// <remarks> This should rarely be useful. </remarks>
        public static float Height
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Window.GetWindowHeight();
        }

        /// <summary> Get a reference to the viewport associated with the current window. </summary>
        public static Viewport Viewport
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Window.GetWindowViewport();
        }

        /// <summary> Set the position of a window by name. </summary>
        /// <param name="name"> The name of the window as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="position"> The new position of the window. </param>
        /// <param name="condition"> Conditions for setting the position. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetPosition(Utf8LabelHandler name, Vector2 position, Condition condition = Condition.None)
            => Native.Methods.Window.SetWindowPos(name.Start(), position, condition);

        /// <summary> Set the size of a window by name. </summary>
        /// <param name="name"> The name of the window as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="size"> The new size of the window. If an axis is set to 0, it forces a fit-to-content on that axis. </param>
        /// <param name="condition"> Conditions for setting the size. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetSize(Utf8LabelHandler name, Vector2 size, Condition condition = Condition.None)
            => Native.Methods.Window.SetWindowSize(name.Start(), size, condition);

        /// <summary> Set the collapsed state of a window by name. </summary>
        /// <param name="name"> The name of the window as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="collapsed"> Whether the window should be collapsed or not. </param>
        /// <param name="condition"> Conditions for setting the collapsed state. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetCollapsed(Utf8LabelHandler name, bool collapsed, Condition condition = Condition.None)
            => Native.Methods.Window.SetWindowCollapsed(name.Start(), collapsed, condition);

        /// <summary> Focus a window by name. </summary>
        /// <param name="name"> The name of the window as text. If this is a UTF8 string, it HAS to be null-terminated.  </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetFocus(Utf8LabelHandler name)
            => Native.Methods.Window.SetWindowFocus(name.Start());

        /// <summary> Clear focus of all windows. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ClearFocus()
            => Native.Methods.Window.SetWindowFocus(null);

        /// <summary> Get the content boundary maximum for the full current window in window coordinates. </summary>
        public static Vector2 MaximumContentRegion
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImVec2 ret;
                Native.Methods.ContentRegion.GetWindowContentRegionMax(&ret);
                return ret;
            }
        }

        /// <summary> Get the content boundary minimum for the full current window in window coordinates. </summary>
        public static Vector2 MinimumContentRegion
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImVec2 ret;
                Native.Methods.ContentRegion.GetWindowContentRegionMin(&ret);
                return ret;
            }
        }

        /// <summary> Get a read-only reference to the current window. </summary>
        public static Window Current
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Internal.GetCurrentWindowRead();
        }

        /// <summary> Whether the current window should skip further items, e.g. when the window is not visible or collapsed. </summary>
        public bool SkipItems
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->SkipItems;
        }

        /// <summary> Get the current cursor position in the window in absolute coordinates. </summary>
        public Vector2 CursorPosition
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->TempData.CursorPosition;
        }

        /// <summary> Get the text base offset of the current line. </summary>
        public float CurrentLineTextBaseOffset
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->TempData.CurrentLineTextBaseOffset;
        }
    }
}
