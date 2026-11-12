namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class DragSliders
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDragFloat")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragFloat(byte* label, float* value, float speed, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDragFloat2")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragFloat2(byte* label, float* value, float speed, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDragFloat3")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragFloat3(byte* label, float* value, float speed, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDragFloat4")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragFloat4(byte* label, float* value, float speed, float minimum, float maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDragFloatRange2")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragFloat(byte* label, float* currentMin, float* currentMax, float speed, float min, float max,
                    byte* format, byte* formatMax, SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDragInt")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragInt(byte* label, int* value, float speed, int minimum, int maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDragInt2")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragInt2(byte* label, int* value, float speed, int minimum, int maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDragInt3")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragInt3(byte* label, int* value, float speed, int minimum, int maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDragInt4")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragInt4(byte* label, int* value, float speed, int minimum, int maximum, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDragIntRange2")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragInt(byte* label, int* currentMin, int* currentMax, float speed, int minimum, int maximum,
                    byte* format, byte* formatMax, SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDragScalar")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragScalar(byte* label, DataType type, void* data, float speed, void* min, void* max, byte* format,
                    SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDragScalarN")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragScalarN(byte* label, DataType type, void* data, int components, float speed, void* min,
                    void* max,
                    byte* format, SliderFlags flags);
            }
        }
    }
}
