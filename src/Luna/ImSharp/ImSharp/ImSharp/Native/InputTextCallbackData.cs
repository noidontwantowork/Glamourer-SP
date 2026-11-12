namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe partial struct InputTextCallbackData
        {
            public InputTextFlags EventFlag;
            public InputTextFlags Flags;
            public void*          UserData;
            public ImWchar        EventChar;
            public Key            EventKey;
            public byte*          Buffer;
            public int            BufferTextLength;
            public int            BufferSize;
            public ImBool         BufferDirty;
            public int            CursorPosition;
            public int            SelectionStart;
            public int            SelectionEnd;

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImGuiInputTextCallbackData_DeleteChars")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void DeleteChars(InputTextCallbackData* self, int position, int byteCount);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImGuiInputTextCallbackData_InsertChars")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void InsertChars(InputTextCallbackData* self, int position, byte* text, byte* textEnd);
        }
    }
}
