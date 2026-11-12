namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Style
            {
                #region Styles

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igStyleColorsDark")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void StyleColorsDark(ImGuiStyle* style);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igStyleColorsLight")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void StyleColorsLight(ImGuiStyle* style);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igStyleColorsClassic")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void StyleColorsClassic(ImGuiStyle* style);

                #endregion

                #region Read Access

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetFont")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImFont* GetFont();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetFontSize")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float GetFontSize();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetFontTexIdWhitePixel")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImTextureId GetFontTexIdWhitePixel();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetFontTexUvWhitePixel")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetFontTexUvWhitePixel(ImVec2* ret);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetColorU32_Col")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Rgba32 GetColorU32(ImGuiColor color, float alpha);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetColorU32_Vec4")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Rgba32 GetColorU32(in ImVec4 color);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetColorU32_U32")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Rgba32 GetColorU32(Rgba32 color);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetStyleColorVec4")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImVec4 GetStyleColorVec4(ImGuiColor color);

                #endregion
            }
        }
    }
}
