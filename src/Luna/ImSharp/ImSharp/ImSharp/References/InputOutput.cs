namespace ImSharp;

public static partial class Im
{
    /// <summary> A reference to the Input/Output data used by ImGui. </summary>
    /// <param name="pointer"> The native pointer to the IO struct. </param>
    public readonly unsafe ref struct InputOutput(Native.Io* pointer)
    {
        /// <summary> A mouse position signifying no available mouse. </summary>
        public static readonly Vector2 InvalidMouse = new(-float.MaxValue);

        /// <summary> The address of the native object. </summary>
        public readonly Native.Io* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator InputOutput(Native.Io* pointer)
            => new(pointer);

        /// <summary> The currently used font atlas. </summary>
        public FontAtlas Fonts
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => new(Pointer->Fonts);
        }

        /// <summary> The main display size in pixels. </summary>
        public Vector2 DisplaySize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->DisplaySize;
        }

        /// <summary> The global scale of all fonts. </summary>
        public float GlobalScale
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->FontGlobalScale;
        }

        /// <summary> The time elapsed since the last frame in seconds. </summary>
        public float DeltaTime
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->DeltaTime;
        }

        /// <summary> Flags set by the backend to communicate supported features. </summary>
        public BackendFlags BackendFlags
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->BackendFlags;
        }

        /// <summary> Flags set by the user or application to communicate intended features. </summary>
        public ConfigFlags ConfigFlags
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ConfigFlags;
        }

        /// <summary> Get the mousewheel movement delta in this frame. </summary>
        public float MouseWheel
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->MouseWheel;
        }

        /// <summary> Get the horizontal mousewheel movement delta in this frame. </summary>
        public float MouseWheelHorizontal
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->MouseWheelH;
        }

        /// <summary> The time before a held button starts repeating its input in seconds for buttons in repeat mode. </summary>
        public float KeyRepeatDelay
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->KeyRepeatDelay;
        }

        /// <summary> The frequency at which a held button repeats its input in seconds for buttons in repeat mode. </summary>
        public float KeyRepeatRate
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->KeyRepeatRate;
        }

        /// <summary> Used in situations where window coordinates are different from framebuffer coordinates. </summary>
        public Vector2 DisplayFrameBufferScale
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->DisplayFramebufferScale;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->DisplayFramebufferScale = value;
        }

        /// <summary> Whether the modifier key Control is currently held. </summary>
        public bool KeyControl
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->KeyCtrl;
        }

        /// <summary> Whether the modifier key Shift is currently held. </summary>
        public bool KeyShift
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->KeyShift;
        }

        /// <summary> Whether the modifier key Alt is currently held. </summary>
        public bool KeyAlt
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->KeyAlt;
        }

        /// <summary> Whether the modifier key Super/Command/Windows is currently held. </summary>
        public bool KeySuper
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->KeySuper;
        }

        /// <summary> Currently held modifier keys as flags. </summary>
        public ModFlags KeyModifiers
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->KeyMods;
        }

        /// <summary> The current position of the mouse cursor in screen coordinates. </summary>
        /// <remarks> Returns <seealso cref="InvalidMouse"/> if no mouse is available. </remarks>
        public Vector2 MousePosition
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->MousePos;
        }

        /// <summary> Tells ImGui to capture text input. </summary>
        public bool CaptureTextInput
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WantTextInput;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->WantTextInput = value;
        }

        /// <summary> Tells ImGui to capture keyboard inputs. </summary>
        public bool CaptureKeyboard
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WantCaptureKeyboard;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->WantCaptureKeyboard = value;
        }

        /// <summary> Tells ImGui to capture mouse inputs. </summary>
        public bool CaptureMouse
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WantCaptureMouse;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->WantCaptureMouse = value;
        }


        /// <summary> Access the <seealso cref="InputOutput"/> structure containing inputs, timings and configuration. </summary>
        /// <returns> A reference to the <seealso cref="InputOutput"/> structure. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static InputOutput Get()
            => Native.Methods.Main.GetIo();

        /// <summary> Queue a new key up/down event. </summary>
        /// <param name="key"> The translated key that triggers an event. </param>
        /// <param name="down"> Whether the event is pushing the key down or letting it go. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void AddKeyEvent(Key key, bool down)
            => Native.Io.AddKeyEvent(Pointer, key, down);

        /// <summary> Queue a new key up/down event for analogue values, e.g. for analog sticks on controllers. </summary>
        /// <param name="key"> The translated key that triggers an event. </param>
        /// <param name="down"> Whether the key is pushed down or released. </param>
        /// <param name="velocity"> The velocity of the control. </param>
        /// <remarks> Dead-zones should be handled by the backend. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void AddKeyEvent(Key key, bool down, float velocity)
            => Native.Io.AddKeyAnalogEvent(Pointer, key, down, velocity);

        /// <summary> Queue a mouse position update. </summary>
        /// <param name="mousePosition"> The new mouse position. Use <seealso cref="InvalidMouse"/> to signify no mouse. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void AddMousePositionEvent(Vector2 mousePosition)
            => Native.Io.AddMousePosEvent(Pointer, mousePosition.X, mousePosition.Y);

        /// <summary> Queue a mouse button update. </summary>
        /// <param name="buttonIdx"> The mouse button triggered. </param>
        /// <param name="isDown"> Whether the button is pushed down or released. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void AddMouseButtonEvent(MouseButton buttonIdx, bool isDown)
            => Native.Io.AddMouseButtonEvent(Pointer, (int)buttonIdx, isDown);

        /// <summary> Queue a mouse wheel update. </summary>
        /// <param name="wheelDeltaX"> The distance the horizontal mouse wheel scrolled. </param>
        /// <param name="wheelDeltaY"> The distance the vertical mouse wheel scrolled. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void AddMouseWheelEvent(float wheelDeltaX, float wheelDeltaY)
            => Native.Io.AddMouseWheelEvent(Pointer, wheelDeltaX, wheelDeltaY);

        /// <summary> Queue a mouse hovering viewport update. </summary>
        /// <param name="id"> The ID of the hovered viewport. </param>
        /// <remarks> Requires the backend to set the <seealso cref="BackendFlags.HasMouseHoveredViewport"/> flag. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void AddMouseViewportEvent(ImGuiId id)
            => Native.Io.AddMouseViewportEvent(Pointer, id);

        /// <summary> Queue a gain or loss of focus update. </summary>
        /// <param name="focused"> Whether the viewport was focused or unfocused. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void AddFocusEvent(bool focused)
            => Native.Io.AddFocusEvent(Pointer, focused);

        /// <summary> Queue a new character input. </summary>
        /// <param name="character"> The character as a UTF32 character. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void AddInputCharacter(uint character)
            => Native.Io.AddInputCharacter(Pointer, character);

        /// <summary> Queue a new character input. </summary>
        /// <param name="character"> The character as a UTF16 character. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void AddInputCharacter(char character)
            => Native.Io.AddInputCharacterUtf16(Pointer, character);

        /// <summary> Queue a new characters input from a string. </summary>
        /// <param name="str"> The string. If passing UTF8, this HAS to be null-terminated. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void AddInputCharacters(Utf8TextHandler str)
            => Native.Io.AddInputCharactersUtf8(Pointer, str.Start());
    }
}
