namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static unsafe partial class Methods
        {
            public static partial class Menu
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginMenuBar")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginMenuBar();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndMenuBar")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndMenuBar();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginMainMenuBar")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginMainMenuBar();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndMainMenuBar")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndMainMenuBar();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginMenu")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginMenu(byte* label, ImBool enabled);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndMenu")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndMenu();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igMenuItem_Bool")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool MenuItem(byte* label, byte* shortcut, ImBool selected, ImBool enabled);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igMenuItem_BoolPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool MenuItem(byte* label, byte* shortcut, ImBool* selected, ImBool enabled);
            }
        }
    }
}
