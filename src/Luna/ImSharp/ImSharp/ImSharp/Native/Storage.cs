namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe partial struct Storage
        {
            public ImVector<StoragePair> Data;

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_Clear")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void Clear(Storage* self);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_GetInt")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial int GetInt(Storage* self, ImGuiId id, int defaultValue);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_SetInt")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void SetInt(Storage* self, ImGuiId id, int value);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_GetBool")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial ImBool GetBool(Storage* self, ImGuiId id, ImBool defaultValue);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_SetBool")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void SetBool(Storage* self, ImGuiId id, ImBool value);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_GetFloat")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial float GetFloat(Storage* self, ImGuiId id, float defaultValue);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_SetFloat")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void SetFloat(Storage* self, ImGuiId id, float value);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_GetVoidPtr")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial nint GetPointer(Storage* self, ImGuiId id, nint defaultValue);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_SetVoidPtr")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void SetPointer(Storage* self, ImGuiId id, nint value);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_GetIntRef")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial int* GetIntRef(Storage* self, ImGuiId id, int defaultValue);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_GetBoolRef")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial ImBool* GetBoolRef(Storage* self, ImGuiId id, ImBool defaultValue);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_GetFloatRef")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial float* GetFloatRef(Storage* self, ImGuiId id, float defaultValue);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_GetVoidPtrRef")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial nint* GetPointerRef(Storage* self, ImGuiId id, nint defaultValue);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_SetAllInt")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void SetAllInt(Storage* self, int value);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiStorage_BuildSortByKey")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void BuildSortByKey(Storage* self);
        }
    }
}
