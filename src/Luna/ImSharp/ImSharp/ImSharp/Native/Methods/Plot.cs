namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Plot
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPlotLines_FloatPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool PlotLines(byte* label, float* values, int numValues, int offsetValues, byte* overlay,
                    float scaleMinimum, float scaleMaximum, ImVec2 size, int stride);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPlotLines_FnBoolPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool PlotLines(byte* label, delegate*<void*, int, float> getter, void* data, int numValues,
                    int offsetValues, byte* overlay, float scaleMinimum, float scaleMaximum, ImVec2 size);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPlotHistogram_FloatPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool PlotHistogram(byte* label, float* values, int numValues, int offsetValues, byte* overlay,
                    float scaleMinimum, float scaleMaximum, ImVec2 size, int stride);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPlotHistogram_FnFloatPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool PlotHistogram(byte* label, delegate*<void*, int, float> getter, void* data, int numValues,
                    int offsetValues, byte* overlay, float scaleMinimum, float scaleMaximum, ImVec2 size);
            }
        }
    }
}
