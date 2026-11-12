namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Color
            {
                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igColorConvertU32ToFloat4")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImVec4 ColorConvertU32ToFloat4(Rgba32 color);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igColorConvertFloat4ToU32")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Rgba32 ColorConvertFloat4ToU32(ImVec4 color);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igColorConvertRGBtoHSV")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ColorConvertRgbToHsv(float r, float g, float b, float* h, float* s, float* v);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igColorConvertHSVtoRGB")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ColorConvertHsvToRgb(float h, float s, float v, float* r, float* g, float* b);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igGetStyleColorName")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial byte* GetStyleColorName(ImGuiColor color);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igColorEdit3")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool ColorEdit3(byte* label, float* color, ColorEditorFlags flags);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igColorEdit4")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool ColorEdit4(byte* label, float* color, ColorEditorFlags flags);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igColorPicker3")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool ColorPicker3(byte* label, float* color, ColorPickerFlags flags);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igColorPicker4")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool ColorPicker4(byte* label, float* color, ColorPickerFlags flags, float* referenceColor);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igColorButton")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool ColorButton(byte* description, ImVec4 color, ColorButtonFlags flags, ImVec2 size);

                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igSetColorEditOptions")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetColorEditOptions(ColorEditorFlags flags);
            }
        }
    }
}
