// ReSharper disable MemberHidesStaticFromOuterClass

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace ImSharp;

public static partial class Im
{
    /// <summary> Wrapper class for mouse-related functions. </summary>
    public static unsafe class Mouse
    {
        /// <summary> Get whether the specified mouse button is currently held down. </summary>
        /// <param name="button"> The mouse button to query. </param>
        /// <returns> Whether the mouse button is being held. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsDown(MouseButton button)
            => Native.Methods.KeyState.IsMouseDown(button);

        /// <summary> Get whether the specified mouse button was clicked at least once this frame (went from not down to down). </summary>
        /// <param name="button"> The mouse button to query. </param>
        /// <returns> Whether the mouse has been clicked at least once this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsClicked(MouseButton button, bool repeat = false)
            => Native.Methods.KeyState.IsMouseClicked(button, repeat);

        /// <summary> Get whether the specified mouse button was released this frame (went from down to not down). </summary>
        /// <param name="button"> The mouse button to query. </param>
        /// <returns> Whether the mouse has been released this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsReleased(MouseButton button)
            => Native.Methods.KeyState.IsMouseReleased(button);

        /// <summary> Get whether the specified mouse button was clicked at least twice in succession (went from not down to down). </summary>
        /// <param name="button"> The mouse button to query. </param>
        /// <returns> Whether the mouse has been clicked at least twice this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsDoubleClicked(MouseButton button)
            => Native.Methods.KeyState.IsMouseDoubleClicked(button);

        /// <summary> Get the number of successive mouse clicks when a click happens. </summary>
        /// <param name="button"> The mouse button to query. </param>
        /// <returns> The number of successive mouse clicks if the mouse was clicked this frame, 0 otherwise. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static int GetClickedCount(MouseButton button)
            => Native.Methods.KeyState.GetMouseClickedCount(button);

        /// <summary> Query whether the mouse is currently hovering the given rectangle. </summary>
        /// <param name="upperLeft"> The upper-left corner of the queried rectangle in screen coordinates. </param>
        /// <param name="lowerRight"> The lower-right corner of the queried rectangle in screen coordinates. </param>
        /// <param name="clip"> Whether to use the current clipping settings or not. </param>
        /// <returns> Whether the mouse is hovering the queried rectangle regardless of focus, ordering or blocking. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsHoveringRectangle(Vector2 upperLeft, Vector2 lowerRight, bool clip = true)
            => Native.Methods.KeyState.IsMouseHoveringRect(upperLeft, lowerRight, clip);

        /// <inheritdoc cref="IsHoveringRectangle(Vector2,Vector2,bool)"/>
        /// <param name="rectangle"> The queried rectangle in screen coordinates. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsHoveringRectangle(in Rectangle rectangle, bool clip = true)
            => Native.Methods.KeyState.IsMouseHoveringRect(rectangle.Minimum, rectangle.Maximum, clip);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsDragging(MouseButton button, float lockThreshold)
            => Native.Methods.KeyState.IsMouseDragging(button, lockThreshold);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static Vector2 GetDragDelta(MouseButton button, float lockThreshold)
        {
            ImVec2 ret;
            Native.Methods.KeyState.GetMouseDragDelta(&ret, button, lockThreshold);
            return ret;
        }

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ResetDragDelta(MouseButton button)
            => Native.Methods.KeyState.ResetMouseDragDelta(button);

        /// <summary> Whether the current mouse position is valid or there is no mouse available. </summary>
        /// <returns> True if there is a valid mouse position. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsPositionValid()
            => Native.Methods.KeyState.IsMousePosValid(null);

        /// <summary> Whether the queried mouse position is valid. </summary>
        /// <returns> True if the position is not <seealso cref="InputOutput.InvalidMouse"/>. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsPositionValid(Vector2 position)
            => Native.Methods.KeyState.IsMousePosValid((ImVec2*)&position);

        /// <summary> Get whether any mouse button is currently held down. </summary>
        public static bool AnyButtonDown
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.KeyState.IsAnyMouseDown();
        }

        /// <summary> Get the current mouse position in screen coordinates. </summary>
        public static Vector2 Position
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImVec2 ret;
                Native.Methods.KeyState.GetMousePos(&ret);
                return ret;
            }
        }

        /// <summary> Get the mouse position in screen coordinates at the time of opening the popup we are currently in, if any. </summary>
        public static Vector2 PositionOnOpeningCurrentPopup
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImVec2 ret;
                Native.Methods.KeyState.GetMousePosOpeningCurrentPopup(&ret);
                return ret;
            }
        }

        /// <summary> Get or set the current cursor type. </summary>
        /// <remarks> This is updated during the frame and reset in <seealso cref="Main.NewFrame"/>. If you use software rendering, ImGui will render the cursor for you. </remarks>
        public static MouseCursor Cursor
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.KeyState.GetMouseCursor();
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Native.Methods.KeyState.SetMouseCursor(value);
        }

        /// <summary> Override the flag signalizing that ImGui wants to capture mouse buttons in the next frame. </summary>
        /// <param name="value"> Whether ImGui should capture the mouse buttons or not. </param>
        /// <remarks> The flag has to be handled by the application in some way, and is not handled automatically. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void WantCaptureNextFrame(bool value)
            => Native.Methods.KeyState.SetNextFrameWantCaptureMouse(value);
    }
}
