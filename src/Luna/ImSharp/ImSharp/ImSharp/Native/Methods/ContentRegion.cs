namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class ContentRegion
            {
                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igGetContentRegionAvail")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetContentRegionAvail(ImVec2* ret);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igGetContentRegionMax")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetContentRegionMax(ImVec2* ret);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igGetWindowContentRegionMin")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetWindowContentRegionMin(ImVec2* ret);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igGetWindowContentRegionMax")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetWindowContentRegionMax(ImVec2* ret);
            }
        }
    }
}
