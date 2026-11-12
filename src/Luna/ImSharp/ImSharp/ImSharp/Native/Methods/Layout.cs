namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Layout
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSeparator")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void Separator();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSameLine")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SameLine(float offsetFromStart, float spacing);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igNewLine")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void NewLine();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSpacing")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void Spacing();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDummy")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void Dummy(ImVec2 size);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIndent")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void Indent(float width);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igUnindent")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void Unindent(float width);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginGroup")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void BeginGroup();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndGroup")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndGroup();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetCursorPos")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetCursorPos(ImVec2* ret);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetCursorPosX")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetCursorPosX();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetCursorPosY")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetCursorPosY();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetCursorPos")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetCursorPos(ImVec2 localPosition);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetCursorPosX")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetCursorPosX(float localX);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetCursorPosY")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetCursorPosY(float localY);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetCursorStartPos")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetCursorStartPos(ImVec2* ret);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetCursorScreenPos")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetCursorScreenPos(ImVec2* ret);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetCursorScreenPos")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetCursorScreenPos(ImVec2 pos);

                /// <summary> Aligns the next item drawn to frame padding, i.e. centers it vertically on a frame. </summary>
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igAlignTextToFramePadding")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void AlignTextToFramePadding();

                /// <summary> Returns the height of a text line, i.e. the current FontSize. </summary>
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetTextLineHeight")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetTextLineHeight();

                /// <summary> Returns the height of a text line with added spacing, i.e. the current FontSize + ItemSpacing.Y. </summary>
                /// <remarks> This is the distance in pixels between 2 consecutive lines of text. </remarks>
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetTextLineHeightWithSpacing")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetTextLineHeightWithSpacing();

                /// <summary> Returns the height of a framed object, i.e. the current FontSize + FramePadding.Y * 2. </summary>
                /// <remarks> This is the default height of buttons, inputs, etc. </remarks>
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetFrameHeight")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetFrameHeight();

                /// <summary> Returns the height of a framed object with added spacing, i.e. the current FontSize + FramePadding.Y * 2 + ItemSpacing.Y. </summary>
                /// <remarks> This is the distance in pixels between 2 consecutive framed objects. </remarks>
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetFrameHeightWithSpacing")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetFrameHeightWithSpacing();
            }
        }
    }
}
