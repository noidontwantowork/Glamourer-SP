namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Context
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCreateContext")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Native.Internal.Context* CreateContext(ImFontAtlas* sharedFontAtlasRef);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDestroyContext")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void DestroyContext(Native.Internal.Context* context);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetCurrentContext")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Native.Internal.Context* GetCurrentContext();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetCurrentContext")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetCurrentContext(Native.Internal.Context* context);
            }
        }
    }
}
