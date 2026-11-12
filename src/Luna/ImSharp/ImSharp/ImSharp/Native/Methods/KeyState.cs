namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static unsafe partial class Methods
        {
            public static unsafe partial class KeyState
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsKeyDown")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsKeyDown(Key key);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsKeyPressed")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsKeyPressed(Key key, ImBool repeat);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsKeyReleased")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsKeyReleased(Key key);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetKeyPressedAmount")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial int GetKeyPressedAmount(Key key, float repeatDelay, float rate);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetKeyName")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial byte* GetKeyName(Key key);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetNextFrameWantCaptureKeyboard")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextFrameWantCaptureKeyboard(ImBool capture);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsMouseDown")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsMouseDown(MouseButton button);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsMouseClicked")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsMouseClicked(MouseButton button, ImBool repeat);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsMouseReleased")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsMouseReleased(MouseButton button);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsMouseDoubleClicked")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsMouseDoubleClicked(MouseButton button);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetMouseClickedCount")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial int GetMouseClickedCount(MouseButton button);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsMouseHoveringRect")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsMouseHoveringRect(ImVec2 minimum, ImVec2 maximum, ImBool clip);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsMousePosValid")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsMousePosValid(ImVec2* mousePos);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsAnyMouseDown")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsAnyMouseDown();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetMousePos")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetMousePos(ImVec2* ret);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetMousePosOpeningCurrentPopup")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetMousePosOpeningCurrentPopup(ImVec2* ret);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsMouseDragging")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsMouseDragging(MouseButton button, float lockThreshold);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetMouseDragDelta")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetMouseDragDelta(ImVec2* ret, MouseButton button, float lockThreshold);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igResetMouseDragDelta")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ResetMouseDragDelta(MouseButton button);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetMouseCursor")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial MouseCursor GetMouseCursor();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetMouseCursor")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetMouseCursor(MouseCursor cursor);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetNextFrameWantCaptureMouse")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextFrameWantCaptureMouse(ImBool capture);
            }
        }
    }
}
