namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe partial struct Io
        {
            public ConfigFlags       ConfigFlags;
            public BackendFlags      BackendFlags;
            public ImVec2            DisplaySize;
            public float             DeltaTime;
            public float             IniSavingRate;
            public byte*             IniFilename;
            public byte*             LogFilename;
            public float             MouseDoubleClickTime;
            public float             MouseDoubleClickMaxDist;
            public float             MouseDragThreshold;
            public float             KeyRepeatDelay;
            public float             KeyRepeatRate;
            public void*             UserData;
            public ImFontAtlas*      Fonts;
            public float             FontGlobalScale;
            public ImBool            FontAllowUserScaling;
            public ImFont*           FontDefault;
            public ImVec2            DisplayFramebufferScale;
            public ImBool            ConfigDockingNoSplit;
            public ImBool            ConfigDockingWithShift;
            public ImBool            ConfigDockingAlwaysTabBar;
            public ImBool            ConfigDockingTransparentPayload;
            public ImBool            ConfigViewportsNoAutoMerge;
            public ImBool            ConfigViewportsNoTaskBarIcon;
            public ImBool            ConfigViewportsNoDecoration;
            public ImBool            ConfigViewportsNoDefaultParent;
            public ImBool            MouseDrawCursor;
            public ImBool            ConfigMacOsxBehaviors;
            public ImBool            ConfigInputTrickleEventQueue;
            public ImBool            ConfigInputTextCursorBlink;
            public ImBool            ConfigDragClickToInputText;
            public ImBool            ConfigWindowsResizeFromEdges;
            public ImBool            ConfigWindowsMoveFromTitleBarOnly;
            public float             ConfigMemoryCompactTimer;
            public byte*             BackendPlatformName;
            public byte*             BackendRendererName;
            public void*             BackendPlatformUserData;
            public void*             BackendRendererUserData;
            public void*             BackendLanguageUserData;
            public ClipboardFuncs    Clipboard;
            public void*             ClipboardUserData;
            public SetImeData        SetPlatformImeDataFn;
            public nint              UnusedPadding;
            public ImBool            WantCaptureMouse;
            public ImBool            WantCaptureKeyboard;
            public ImBool            WantTextInput;
            public ImBool            WantSetMousePos;
            public ImBool            WantSaveIniSettings;
            public ImBool            NavActive;
            public ImBool            NavVisible;
            public float             Framerate;
            public int               MetricsRenderVertices;
            public int               MetricsRenderIndices;
            public int               MetricsRenderWindows;
            public int               MetricsActiveWindows;
            public int               MetricsActiveAllocations;
            public ImVec2            MouseDelta;
            public KeyArray          KeyMap;
            public KeyDownArray      KeysDown;
            public ImVec2            MousePos;
            public MouseBoolArray    MouseDown;
            public float             MouseWheel;
            public float             MouseWheelH;
            public ImGuiId           MouseHoveredViewport;
            public ImBool            KeyCtrl;
            public ImBool            KeyShift;
            public ImBool            KeyAlt;
            public ImBool            KeySuper;
            public NavInputArray     NavInputs;
            public ModFlags          KeyMods;
            public KeyDataArray      KeyData;
            public ImBool            WantCaptureMouseUnlessPopupClose;
            public ImVec2            MousePosPrev;
            public MouseVector2Array MouseClickedPos;
            public MouseDoubleArray  MouseClickedTime;
            public MouseBoolArray    MouseClicked;
            public MouseBoolArray    MouseDoubleClicked;
            public MouseShortArray   MouseClickedCount;
            public MouseShortArray   MouseClickedLastCount;
            public MouseBoolArray    MouseReleased;
            public MouseBoolArray    MouseDownOwned;
            public MouseBoolArray    MouseDownOwnedUnlessPopupClose;
            public MouseFloatArray   MouseDownDuration;
            public MouseFloatArray   MouseDownDurationPrev;
            public MouseVector2Array MouseDragMaxDistanceAbs;
            public MouseFloatArray   MouseDragMaxDistanceSqr;
            public NavInputArray     NavInputsDownDuration;
            public NavInputArray     NavInputsDownDurationPrev;
            public float             PenPressure;
            public ImBool            AppFocusLost;
            public ImBool            AppAcceptingEvents;
            public sbyte             BackendUsingLegacyKeyArrays;
            public ImBool            BackendUsingLegacyNavInputArray;
            public char              InputQueueSurrogate;
            public ImVector<ImWchar> InputQueueCharacters;

            [InlineArray((int)MouseButton.Count)]
            public struct MouseBoolArray
            {
                private ImBool _element;
            }

            [InlineArray((int)MouseButton.Count)]
            public struct MouseVector2Array
            {
                private ImVec2 _element;
            }

            [InlineArray((int)MouseButton.Count)]
            public struct MouseDoubleArray
            {
                private double _element;
            }

            [InlineArray((int)MouseButton.Count)]
            public struct MouseFloatArray
            {
                private float _element;
            }

            [InlineArray((int)MouseButton.Count)]
            public struct MouseShortArray
            {
                private ushort _element;
            }

            [InlineArray(KeyExtensions.Count)]
            public struct KeyArray
            {
                private int _element;
            }

            [InlineArray(KeyExtensions.Count)]
            public struct KeyDownArray
            {
                private ImBool _element;
            }

            [InlineArray(KeyExtensions.Count)]
            public struct KeyDataArray
            {
                private KeyData _element;
            }

            [InlineArray((int)NavigationInput.Count)]
            public struct NavInputArray
            {
                private float _element;
            }

            public struct SetImeData
            {
                public delegate* unmanaged<Internal.Context*, Viewport*, ImGuiPlatformImeData*, void> Func;
            }

            public struct ClipboardFuncs
            {
                public delegate* unmanaged<void*, byte*>       Get;
                public delegate* unmanaged<void*, byte*, void> Set;
            }

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiIO_AddKeyEvent")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddKeyEvent(Io* self, Key key, ImBool down);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiIO_AddKeyAnalogEvent")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddKeyAnalogEvent(Io* self, Key key, ImBool down, float velocity);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiIO_AddMousePosEvent")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddMousePosEvent(Io* self, float x, float y);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiIO_AddMouseButtonEvent")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddMouseButtonEvent(Io* self, int button, ImBool down);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiIO_AddMouseWheelEvent")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddMouseWheelEvent(Io* self, float wheelX, float wheelY);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiIO_AddMouseViewportEvent")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddMouseViewportEvent(Io* self, ImGuiId id);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiIO_AddFocusEvent")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddFocusEvent(Io* self, ImBool focused);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiIO_AddInputCharacter")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddInputCharacter(Io* self, uint c);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiIO_AddInputCharacterUTF16")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddInputCharacterUtf16(Io* self, ushort c);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiIO_AddInputCharactersUTF8")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddInputCharactersUtf8(Io* self, byte* c);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiIO_SetKeyEventNativeData")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void SetKeyEventNativeData(Io* self, Key key, int nativeKeyCode, int nativeScanCode, int nativeLegacyIndex);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiIO_SetAppAcceptingEvents")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void SetAppAcceptingEvents(Io* self, ImBool acceptingEvents);
        }
    }
}
