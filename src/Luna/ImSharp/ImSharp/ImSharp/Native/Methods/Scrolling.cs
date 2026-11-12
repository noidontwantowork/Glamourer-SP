namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static partial class Scrolling
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetScrollX")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetScrollX();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetScrollY")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetScrollY();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetScrollX_Float")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetScrollX(float value);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetScrollY_Float")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetScrollY(float value);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetScrollMaxX")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetScrollMaxX();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetScrollMaxY")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetScrollMaxY();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetScrollHereX")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetScrollHereX(float centerRatio);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetScrollHereY")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetScrollHereY(float centerRatio);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetScrollFromPosX_Float")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetScrollFromPosX(float local, float centerRatio);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetScrollFromPosY_Float")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetScrollFromPosY(float local, float centerRatio);
            }
        }
    }
}
