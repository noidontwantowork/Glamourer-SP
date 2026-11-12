namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Utility
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsRectVisible_Nil")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsRectVisible(ImVec2 size);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsRectVisible_Vec2")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsRectVisible(ImVec2 minimum, ImVec2 maximum);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetTime")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial double GetTime();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetFrameCount")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial int GetFrameCount();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetStateStorage")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetStateStorage(Storage* storage);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetStateStorage")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Storage* GetStateStorage();
            }
        }
    }
}
