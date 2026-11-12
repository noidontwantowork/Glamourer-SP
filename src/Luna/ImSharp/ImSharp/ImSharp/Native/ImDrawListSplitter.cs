namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe partial struct ImDrawListSplitter
        {
            public int                     Current;
            public int                     Count;
            public ImVector<ImDrawChannel> Channels;

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImDrawListSplitter_Split")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void Split(ImDrawListSplitter* self, ImDrawList* drawList, int count);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImDrawListSplitter_Merge")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void Merge(ImDrawListSplitter* self, ImDrawList* drawList);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImDrawListSplitter_SetCurrentChannel")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void SetCurrentChannel(ImDrawListSplitter* self, ImDrawList* drawList, int channelIndex);
        };
    }
}
