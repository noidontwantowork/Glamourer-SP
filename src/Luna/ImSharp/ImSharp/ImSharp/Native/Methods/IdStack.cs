namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class IdStack
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushID_Str")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushId(byte* id);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushID_StrStr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushId(byte* id, byte* end);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushID_Int")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushId(int id);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushID_Ptr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushId(nint ptr);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPopID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PopId();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetID_Str")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId GetId(byte* start);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetID_StrStr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId GetId(byte* start, byte* end);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetID_Ptr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId GetId(nint ptr);
            }
        }
    }
}
