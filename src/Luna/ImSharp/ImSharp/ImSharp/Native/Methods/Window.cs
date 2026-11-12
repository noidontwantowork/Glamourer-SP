namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static unsafe partial class Methods
        {
            public static partial class Window
            {
                #region Main

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBegin")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool Begin(byte* name, ImBool* open, WindowFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEnd")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void End();

                #endregion

                #region Children

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginChild_Str")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginChild(byte* fmt, ImVec2 size, ImBool border, WindowFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginChild_ID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginChild(ImGuiId id, ImVec2 size, ImBool border, WindowFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndChild")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndChild();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginChildFrame")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginChildFrame(ImGuiId id, ImVec2 size, WindowFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndChildFrame")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndChildFrame();

                #endregion

                #region Current Window

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsWindowAppearing")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsWindowAppearing();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsWindowCollapsed")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsWindowCollapsed();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsWindowFocused")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsWindowFocused(FocusedFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsWindowHovered")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsWindowHovered(HoveredFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetWindowDrawList")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImDrawList* GetWindowDrawList();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetWindowDpiScale")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetWindowDpiScale();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetWindowPos")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetWindowPos(ImVec2* ret);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetWindowSize")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetWindowSize(ImVec2* ret);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetWindowWidth")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetWindowWidth();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetWindowHeight")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetWindowHeight();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetWindowViewport")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Native.Viewport* GetWindowViewport();

                #endregion

                #region Manipulation

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetNextWindowPos")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextWindowPos(ImVec2 pos, Condition condition, ImVec2 pivot);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetNextWindowSize")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextWindowSize(ImVec2 size, Condition condition);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetNextWindowSizeConstraints")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextWindowSizeConstraints(ImVec2 minSize, ImVec2 maxSize,
                    delegate* unmanaged<SizeCallbackData*, void> callback, void* callbackData);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetNextWindowContentSize")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextWindowContentSize(ImVec2 minSize);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetNextWindowCollapsed")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextWindowCollapsed(ImBool collapsed, Condition condition);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetNextWindowFocus")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextWindowFocus();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetNextWindowBgAlpha")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextWindowBgAlpha(float alpha);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetNextWindowViewport")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextWindowViewport(ImGuiId viewport);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetWindowPos_Vec2")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetWindowPos(ImVec2 pos, Condition condition);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetWindowSize_Vec2")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetWindowSize(ImVec2 size, Condition condition);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetWindowCollapsed_bool")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetWindowCollapsed(ImBool collapsed, Condition condition);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetWindowFocus_Nil")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetWindowFocus();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetWindowFontScale")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetWindowFontScale(float scale);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetWindowPos_Str")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetWindowPos(byte* name, ImVec2 pos, Condition condition);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetWindowSize_Str")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetWindowSize(byte* name, ImVec2 pos, Condition condition);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetWindowCollapsed_Str")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetWindowCollapsed(byte* name, ImBool collapsed, Condition condition);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetWindowFocus_Str")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetWindowFocus(byte* name);

                #endregion
            }
        }
    }
}
