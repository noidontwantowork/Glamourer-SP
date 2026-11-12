namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static unsafe partial class Methods
        {
            public static unsafe partial class Popup
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginPopup")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginPopup(byte* id, WindowFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginPopupModal")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginPopupModal(byte* label, ImBool* open, WindowFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndPopup")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndPopup();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igOpenPopup_Str")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void OpenPopup(byte* id, PopupFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igOpenPopup_ID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void OpenPopup(ImGuiId id, PopupFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igOpenPopupOnItemClick")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void OpenPopupOnItemClick(byte* id, PopupContextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCloseCurrentPopup")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void CloseCurrentPopup();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginPopupContextItem")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginPopupContextItem(byte* id, PopupContextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginPopupContextWindow")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginPopupContextWindow(byte* id, PopupContextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginPopupContextVoid")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginPopupContextVoid(byte* id, PopupContextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsPopupOpen_Str")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsPopupOpen(byte* id, PopupQueryFlags flags);
            }
        }
    }
}
