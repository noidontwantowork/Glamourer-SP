namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Items
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsItemHovered")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsItemHovered(HoveredFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsItemActive")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsItemActive();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsItemFocused")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsItemFocused();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsItemClicked")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsItemClicked(MouseButton button);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsItemVisible")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsItemVisible();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsItemEdited")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsItemEdited();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsItemActivated")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsItemActivated();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsItemDeactivated")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsItemDeactivated();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsItemDeactivatedAfterEdit")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsItemDeactivatedAfterEdit();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsItemToggledOpen")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsItemToggledOpen();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsAnyItemHovered")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsAnyItemHovered();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsAnyItemActive")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsAnyItemActive();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsAnyItemFocused")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsAnyItemFocused();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetItemRectMin")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetItemRectMin(ImVec2* ret);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetItemRectMax")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetItemRectMax(ImVec2* ret);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetItemRectSize")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetItemRectSize(ImVec2* ret);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetItemAllowOverlap")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetItemAllowOverlap();
            }
        }
    }
}
