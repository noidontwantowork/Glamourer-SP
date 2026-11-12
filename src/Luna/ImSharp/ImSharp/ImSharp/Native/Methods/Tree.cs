namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static unsafe partial class Methods
        {
            /// <remarks> Other TreeNode methods omitted because of lack of support of VarArgs. </remarks>
            public static unsafe partial class Tree
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igTreeNodeEx_Str")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool TreeNodeEx(byte* label, TreeNodeFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igTreePush_Str")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void TreePush(byte* id);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igTreePush_Ptr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void TreePush(void* id);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igTreePop")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void TreePop();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetTreeNodeToLabelSpacing")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetTreeNodeToLabelSpacing();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCollapsingHeader_BoolPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool CollapsingHeader(byte* label, ImBool* visible, TreeNodeFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCollapsingHeader_TreeNodeFlags")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool CollapsingHeader(byte* label, TreeNodeFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetNextItemOpen")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextItemOpen(ImBool open, Condition condition);
            }
        }
    }
}
