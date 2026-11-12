namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class ListBox
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginListBox")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginListBox(byte* label, ImVec2 size);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndListBox")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndListBox();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igListBox_Str_arr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DrawListBox(byte* label, int* currentItem, byte** items, int itemCount, int heightInItems);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igListBox_FnBoolPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DrawListBox(byte* label, int* currentItem, delegate*<void*, int, byte**, ImBool> getter,
                    void* data,
                    int itemCount, int heightInItems);
            }
        }
    }
}
