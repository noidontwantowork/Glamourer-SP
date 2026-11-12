namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Settings
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igLoadIniSettingsFromDisk")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void LoadIniSettingsFromDisk(byte* iniFileName);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igLoadIniSettingsFromMemory")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void LoadIniSettingsFromMemory(byte* iniData, ulong iniSize);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSaveIniSettingsToDisk")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SaveIniSettingsToDisk(byte* iniFileName);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSaveIniSettingsToMemory")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial byte* SaveIniSettingsToMemory(ulong* outIniSize);
            }
        }
    }
}
