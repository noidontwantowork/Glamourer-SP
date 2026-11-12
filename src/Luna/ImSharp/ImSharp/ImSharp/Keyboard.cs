namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper class containing keyboard state and actions. </summary>
    public static class Keyboard
    {
        /// <inheritdoc cref="KeyboardFocusDisposable.Push(bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static KeyboardFocusDisposable PushAllowFocus(bool allowFocus, bool condition)
            => new KeyboardFocusDisposable().Push(allowFocus, condition);

        /// <inheritdoc cref="KeyboardFocusDisposable.Push(bool,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static KeyboardFocusDisposable PushAllowFocus(bool allowFocus)
            => new KeyboardFocusDisposable().Push(allowFocus);

        /// <summary> Get whether a specific key is currently being held down. </summary>
        /// <param name="key"> The key to check for. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsDown(Key key)
            => Native.Methods.KeyState.IsKeyDown(key);

        /// <summary> Get whether a specific key was pressed in this frame (went from not down to down). </summary>
        /// <param name="key"> The key to check for. </param>
        /// <param name="repeat"> If this is true, this also triggers when the key was not previously down according to <seealso cref="InputOutput.KeyRepeatDelay"/> and <seealso cref="InputOutput.KeyRepeatRate"/>. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsPressed(Key key, bool repeat = true)
            => Native.Methods.KeyState.IsKeyPressed(key, repeat);

        /// <summary> Get whether a specific key was released in this frame (went from down to not down). </summary>
        /// <param name="key"> The key to check for. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsReleased(Key key)
            => Native.Methods.KeyState.IsKeyReleased(key);

        /// <summary> Set the keyboard focus on the next drawn widget. </summary>
        /// <param name="offset"> Use positive offsets to focus subcomponents of widgets consisting of multiple components. Use -1 to focus the previous widget. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetFocusHere(int offset = 0)
            => Native.Methods.Focus.SetKeyboardFocusHere(offset);

        /// <summary> Get the number of triggered key presses in the last frame according to the passed values. </summary>
        /// <param name="key"> The key to check for. </param>
        /// <param name="repeatDelay"> The repeat delay to use, see <seealso cref="InputOutput.KeyRepeatDelay"/>. </param>
        /// <param name="repeatRate"> The repeat rate to use, see  <seealso cref="InputOutput.KeyRepeatRate"/> </param>
        /// <returns> The number of key presses in the last frame. Usually 0 or 1, but can be more for low repeat delay and rate or high update rate. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static int GetPressedAmount(Key key, float repeatDelay, float repeatRate)
            => Native.Methods.KeyState.GetKeyPressedAmount(key, repeatDelay, repeatRate);

        /// <summary> Override the flag signalizing that ImGui wants to capture the keyboard in the next frame. </summary>
        /// <param name="value"> Whether ImGui should capture the keyboard or not. </param>
        /// <remarks> The flag has to be handled by the application in some way, and is not handled automatically. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void WantCaptureNextFrame(bool value)
            => Native.Methods.KeyState.SetNextFrameWantCaptureKeyboard(value);
    }
}
