namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Information
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igShowDemoWindow")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ShowDemoWindow(ImBool* open);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igShowMetricsWindow")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ShowMetricsWindow(ImBool* open);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igShowDebugLogWindow")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ShowDebugLogWindow(ImBool* open);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igShowStackToolWindow")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ShowStackToolWindow(ImBool* open);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igShowAboutWindow")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ShowAboutWindow(ImBool* open);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igShowStyleEditor")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ShowStyleEditor(ImGuiStyle* style);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igShowStyleSelector")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ShowStyleSelector(byte* label);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igShowFontSelector")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ShowFontSelector(byte* label);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igShowUserGuide")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ShowUserGuide();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetVersion")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial byte* GetVersion();
            }
        }
    }
}
