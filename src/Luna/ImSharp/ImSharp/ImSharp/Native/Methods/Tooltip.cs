namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            /// <remarks> SetTooltip omitted because of lack of support of VarArgs. </remarks>
            public static unsafe partial class Tooltip
            {
                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igBeginTooltip")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void BeginTooltip();


                [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "igEndTooltip")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndTooltip();
            }
        }
    }
}
