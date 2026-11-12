namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe partial struct ImGuiStyle
        {
            public float      Alpha;
            public float      DisabledAlpha;
            public ImVec2     WindowPadding;
            public float      WindowRounding;
            public float      WindowBorderSize;
            public ImVec2     WindowMinSize;
            public ImVec2     WindowTitleAlign;
            public Direction  WindowMenuButtonPosition;
            public float      ChildRounding;
            public float      ChildBorderSize;
            public float      PopupRounding;
            public float      PopupBorderSize;
            public ImVec2     FramePadding;
            public float      FrameRounding;
            public float      FrameBorderSize;
            public ImVec2     ItemSpacing;
            public ImVec2     ItemInnerSpacing;
            public ImVec2     CellPadding;
            public ImVec2     TouchExtraPadding;
            public float      IndentSpacing;
            public float      ColumnsMinSpacing;
            public float      ScrollbarSize;
            public float      ScrollbarRounding;
            public float      GrabMinSize;
            public float      GrabRounding;
            public float      LogSliderDeadzone;
            public float      TabRounding;
            public float      TabBorderSize;
            public float      TabMinWidthForCloseButton;
            public Direction  ColorButtonPosition;
            public ImVec2     ButtonTextAlign;
            public ImVec2     SelectableTextAlign;
            public ImVec2     DisplayWindowPadding;
            public ImVec2     DisplaySafeAreaPadding;
            public float      MouseCursorScale;
            public ImBool     AntiAliasedLines;
            public ImBool     AntiAliasedLinesUseTex;
            public ImBool     AntiAliasedFill;
            public float      CurveTessellationTol;
            public float      CircleTessellationMaxError;
            public ColorArray Colors;

            [InlineArray(ImGuiColorExtensions.ColorCount)]
            public struct ColorArray
            {
                private ImVec4 _element;

                public Vector4 this[ImGuiColor color]
                {
                    get => this[(int)color];
                    set => this[(int)color] = value;
                }
            }

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImGuiStyle_ScaleAllSizes")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void ScaleAllSizes(ImGuiStyle* self, float scaleFactor);
        }
    }
}
