namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Main
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetIO")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Io* GetIo();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetStyle")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiStyle* GetStyle();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igNewFrame")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void NewFrame();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndFrame")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndFrame();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRender")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void Render();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetDrawData")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImDrawData* GetDrawData();
            }
        }
    }
}
