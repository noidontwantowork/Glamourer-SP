namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Sliders
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSliderFloat")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SliderFloat(byte* label, float* value, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSliderFloat2")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SliderFloat2(byte* label, float* value, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSliderFloat3")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SliderFloat3(byte* label, float* value, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSliderFloat4")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SliderFloat4(byte* label, float* value, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSliderAngle")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SliderAngle(byte* label, float* valueRadians, float degreesMin, float degreesMax, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSliderInt")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SliderInt(byte* label, int* value, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSliderInt2")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SliderInt2(byte* label, int* value, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSliderInt3")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SliderInt3(byte* label, int* value, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSliderInt4")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SliderInt4(byte* label, int* value, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSliderScalar")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SliderScalar(byte* label, DataType type, void* value, void* minimum, void* maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSliderScalarN")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SliderScalarN(byte* label, DataType type, void* value, int components, void* minimum,
                    void* maximum,
                    byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igVSliderFloat")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool VSliderFloat(byte* label, ImVec2 size, float* value, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igVSliderInt")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool VSliderInt(byte* label, ImVec2 size, int* value, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igVSliderScalar")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool VSliderScalar(byte* label, ImVec2 size, DataType type, void* value, void* minimum, void* maximum,
                    byte* format,
                    SliderFlags flags);
            }
        }
    }
}
