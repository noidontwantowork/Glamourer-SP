namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Clipboard
            {
                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igGetClipboardText")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial byte* GetClipboardText();

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igSetClipboardText")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetClipboardText(byte* text);
            }
        }
    }
}
