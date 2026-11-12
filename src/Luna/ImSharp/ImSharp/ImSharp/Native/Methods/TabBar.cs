namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class TabBar
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginTabBar")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginTabBar(byte* id, TabBarFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndTabBar")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndTabBar();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginTabItem")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginTabItem(byte* id, byte* open, TabItemFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndTabItem")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndTabItem();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igTabItemButton")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool TabItemButton(byte* label, TabItemFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetTabItemClosed")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetTabItemClosed(byte* tabOrDockedWindowLabel);
            }
        }
    }
}
