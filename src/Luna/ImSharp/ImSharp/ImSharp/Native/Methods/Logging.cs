namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            /// <remarks> LogText omitted due to lack of VarArgs support. </remarks>
            public static unsafe partial class Logging
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igLogToTTY")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void LogToTty(int autoOpenDepth);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igLogToFile")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void LogToFile(int autoOpenDepth, byte* fileName);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igLogToClipboard")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void LogToClipboard(int autoOpenDepth);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igLogFinish")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void LogFinish();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igLogButtons")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void LogButtons();
            }
        }
    }
}
