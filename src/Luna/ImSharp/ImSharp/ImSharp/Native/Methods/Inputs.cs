namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Inputs
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputText")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputText(byte* label, byte* buffer, uint bufferSize, InputTextFlags flags,
                    delegate* unmanaged<InputTextCallbackData*, int> callback, void* userData);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputTextMultiline")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputTextMultiline(byte* label, byte* buffer, ulong bufferSize, ImVec2 size, InputTextFlags flags,
                    delegate* unmanaged<InputTextCallbackData*, int> callback, void* userData);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputTextWithHint")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputTextWithHint(byte* label, byte* hint, byte* buffer, ulong bufferSize, InputTextFlags flags,
                    delegate* unmanaged<InputTextCallbackData*, int> callback, void* userData);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputFloat")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputFloat(byte* label, float* value, float step, float fastStep, byte* format,
                    InputTextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputFloat2")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputFloat2(byte* label, float* value, float step, float fastStep, byte* format,
                    InputTextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputFloat3")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputFloat3(byte* label, float* value, float step, float fastStep, byte* format,
                    InputTextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputFloat4")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputFloat4(byte* label, float* value, float step, float fastStep, byte* format,
                    InputTextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputInt")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputInt(byte* label, int* value, float step, float fastStep, byte* format, InputTextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputInt2")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputInt2(byte* label, int* value, float step, float fastStep, byte* format, InputTextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputInt3")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputInt3(byte* label, int* value, float step, float fastStep, byte* format, InputTextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputInt4")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputInt4(byte* label, int* value, float step, float fastStep, byte* format, InputTextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputDouble")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputDouble(byte* label, double* value, double step, double fastStep, byte* format,
                    InputTextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputScalar")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputScalar(byte* label, DataType type, void* value, void* step, void* fastStep, byte* format,
                    InputTextFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInputScalarN")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InputScalarN(byte* label, DataType type, void* value, int components, void* step, void* fastStep,
                    byte* format, InputTextFlags flags);
            }
        }
    }
}
