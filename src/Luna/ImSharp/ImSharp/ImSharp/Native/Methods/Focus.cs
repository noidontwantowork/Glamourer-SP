namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static partial class Focus
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetItemDefaultFocus")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetItemDefaultFocus();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetKeyboardFocusHere")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetKeyboardFocusHere(int offset);
            }
        }
    }
}
