namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class DrawList
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetMainViewport")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Native.Viewport* GetMainViewport();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetBackgroundDrawList_Nil")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImDrawList* GetBackgroundDrawList();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetForegroundDrawList_Nil")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImDrawList* GetForegroundDrawList();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetBackgroundDrawList_ViewportPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImDrawList* GetBackgroundDrawList(Native.Viewport* viewport);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetForegroundDrawList_ViewportPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImDrawList* GetForegroundDrawList(Native.Viewport* viewport);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetDrawListSharedData")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Native.Internal.ImDrawListSharedData* GetDrawListSharedData();
            }
        }
    }
}
