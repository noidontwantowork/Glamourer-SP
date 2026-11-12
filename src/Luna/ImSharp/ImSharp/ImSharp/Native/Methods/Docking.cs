namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Docking
            {
                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igDockSpace")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId DockSpace(ImGuiId id, ImVec2 size, DockNodeFlags flags, WindowClass* windowClass);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igDockSpaceOverViewport")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId DockSpaceOverViewport(Native.Viewport* viewport, DockNodeFlags flags,
                    WindowClass* windowClass);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igSetNextWindowDockID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextWindowDockId(ImGuiId dockId, Condition condition);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igSetNextWindowClass")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextWindowClass(WindowClass* windowClass);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igGetWindowDockID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId GetWindowDockId();

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igIsWindowDocked")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsWindowDocked();
            }
        }
    }
}
