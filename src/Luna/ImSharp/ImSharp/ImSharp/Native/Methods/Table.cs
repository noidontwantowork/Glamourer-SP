namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Table
            {
                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igBeginTable")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginTable(byte* id, int columns, TableFlags flags, ImVec2 outerSize, float innerWidth);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igEndTable")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndTable();

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableNextRow")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void TableNextRow(TableRowFlags flags, float minHeight);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableNextColumn")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool TableNextColumn();

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableSetColumnIndex")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool TableSetColumnIndex(int columnIndex);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableSetupColumn")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void TableSetupColumn(byte* label, TableColumnFlags flags, float widthOrWeight, ImGuiId userId);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableSetupScrollFreeze")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void TableSetupScrollFreeze(int numFrozenColumns, int numFrozenRows);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableHeadersRow")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void TableHeadersRow();

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableHeader")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void TableHeader(byte* label);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableGetSortSpecs")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial TableSortSpecs* TableGetSortSpecs();

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableGetColumnCount")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial int TableGetColumnCount();

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableGetColumnIndex")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial int TableGetColumnIndex();

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableGetRowIndex")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial int TableGetRowIndex();

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableGetColumnName_Int")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial byte* TableGetColumnName(int column);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableGetColumnFlags")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial TableColumnFlags TableGetColumnFlags(int column);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableSetColumnEnabled")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void TableSetColumnEnabled(int column, ImBool enabled);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igTableSetBgColor")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void TableSetBgColor(TableBackgroundTarget target, Rgba32 color, int column);
            }
        }
    }
}
