namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Viewport
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetPlatformIO")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial PlatformIo* GetPlatformIo();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igUpdatePlatformWindows")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void UpdatePlatformWindows();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRenderPlatformWindowsDefault")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void RenderPlatformWindowsDefault(void* platformRenderArg, void* rendererRenderArg);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDestroyPlatformWindows")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void DestroyPlatformWindows();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igFindViewportByID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Native.Viewport* FindViewportById(ImGuiId id);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igFindViewportByPlatformHandle")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Native.Viewport* FindViewportByPlatformHandle(void* platformHandle);
            }
        }
    }
}
