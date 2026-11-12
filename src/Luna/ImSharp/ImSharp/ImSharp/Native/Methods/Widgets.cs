namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Widgets
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igButton")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool Button(byte* label, ImVec2 size);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSmallButton")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SmallButton(byte* label);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igInvisibleButton")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool InvisibleButton(byte* id, ImVec2 size, ButtonFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igArrowButton")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool ArrowButton(byte* label, Direction direction);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igImage")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void Image(ImTextureId userTextureId, ImVec2 size, ImVec2 uvMinimum, ImVec2 uvMaximum,
                    ImVec4 tint, ImVec4 borderColor);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igImageButton")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool ImageButton(ImTextureId userTextureId, ImVec2 size, ImVec2 uvMinimum, ImVec2 uvMaximum,
                    int framePadding, ImVec4 background, ImVec4 tint);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCheckbox")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool Checkbox(byte* label, ImBool* value);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCheckboxFlags_IntPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool CheckboxFlags(byte* label, int* flags, int value);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCheckboxFlags_UintPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool CheckboxFlags(byte* label, uint* flags, uint value);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRadioButton_Bool")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool RadioButton(byte* label, ImBool active);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRadioButton_IntPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool RadioButton(byte* label, int* value, int buttonValue);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igProgressBar")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ProgressBar(float fraction, ImVec2 size, byte* overlay);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBullet")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void Bullet();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSelectable_Bool")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool Selectable(byte* label, ImBool selected, SelectableFlags flags, ImVec2 size);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSelectable_BoolPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool Selectable(byte* label, ImBool* selected, SelectableFlags flags, ImVec2 size);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginCombo")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginCombo(byte* label, byte* preview, ComboFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndCombo")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndCombo();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCombo_Str_arr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool Combo(byte* label, int* currentItem, byte** items, int itemsCount, int popupMaxHeightInItems);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCombo_Str")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool Combo(byte* label, int* currentItem, byte* itemsSeparatedByNull, int popupMaxHeightInItems);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCombo_FnBoolPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool Combo(byte* label, int* currentItem, delegate*<void*, int, byte**, ImBool> itemsGetter, void* data,
                    int itemsCount, int popupMaxHeightInItems);
            }
        }
    }
}
