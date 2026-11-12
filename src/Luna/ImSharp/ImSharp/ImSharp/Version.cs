namespace ImSharp;

public static partial class Im
{
    /// <summary> Information about the ImGui version these bindings are intended for. </summary>
    public static class Version
    {
        /// <summary> The ImGui Version used as a number. </summary>
        public const int VersionNumber = 18800;

        /// <summary> The ImGui Version used. </summary>
        public const string VersionString = "1.88";

        /// <summary> Whether Tables are supported in this ImGui Version. </summary>
        public const bool HasTable = true;

        /// <summary> Whether Viewports are supported in this ImGui Version. </summary>
        public const bool HasViewport = true;

        /// <summary> Whether Docking is supported in this ImGui Version. </summary>
        public const bool HasDocking = true;

#if IMPLOT
        /// <summary> Whether the auxiliary library ImPlot is included in this ImGui version. </summary>
        public const bool HasImPlot = true;

        /// <summary> The ImPlot DLL name to invoke. </summary>
        public const string CImPlotLibrary = "cimplot";

        public const string ImPlotVersionString = "0.14";
#else
        /// <summary> Whether the auxiliary library ImPlot is included in this ImGui version. </summary>
        public const bool HasImPlot = false;
#endif

        /// <summary> The DLL name to invoke. </summary>
        public const string CImGuiLibrary = "cimgui";

        /// <summary> Obtain the version string compiled into the currently used DLL. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe ReadOnlySpan<byte> GetDllVersion()
        {
            var ptr = Native.Methods.Information.GetVersion();
            return NullTerminationHelpers.GetSpan(ptr);
        }
    }
}
