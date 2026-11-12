namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Column
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igColumns")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void Columns(int count, byte* id, ImBool border);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igNextColumn")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void NextColumn();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetColumnIndex")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial int GetColumnIndex();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetColumnWidth")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetColumnWidth(int index);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetColumnWidth")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetColumnWidth(int index, float width);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetColumnOffset")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetColumnOffset(int index);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetColumnOffset")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetColumnOffset(int index, float offset);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetColumnsCount")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial int GetColumnsCount();
            }
        }
    }
}
