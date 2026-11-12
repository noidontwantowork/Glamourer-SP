namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Disabled
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginDisabled")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void BeginDisabled(ImBool disabled);


                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndDisabled")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndDisabled();
            }
        }
    }
}
