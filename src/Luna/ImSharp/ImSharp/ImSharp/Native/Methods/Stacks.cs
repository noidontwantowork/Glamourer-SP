namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Stacks
            {
                #region Global

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushFont")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushFont(ImFont* font);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPopFont")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PopFont();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushStyleColor_U32")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushStyleColor(ImGuiColor type, Rgba32 color);

                [OverloadResolutionPriority(100)]
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushStyleColor_Vec4")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushStyleColor(ImGuiColor type, ImVec4 color);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPopStyleColor")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PopStyleColor(int num);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushStyleVar_Float")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushStyleVar(ImStyle type, float value);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushStyleVar_Vec2")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushStyleVar(ImStyle type, ImVec2 value);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPopStyleVar")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PopStyleVar(int num);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushAllowKeyboardFocus")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushAllowKeyboardFocus(ImBool allow);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPopAllowKeyboardFocus")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PopAllowKeyboardFocus();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushButtonRepeat")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushButtonRepeat(ImBool repeat);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPopButtonRepeat")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PopButtonRepeat();

                #endregion

                #region Current Window

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushItemWidth")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushItemWidth(float width);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPopItemWidth")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PopItemWidth();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetNextItemWidth")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextItemWidth(float width);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCalcItemWidth")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float CalcItemWidth();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushTextWrapPos")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushTextWrapPos(float localX);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPopTextWrapPos")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PopTextWrapPos();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushClipRect")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushClipRect(ImVec2 minimum, ImVec2 maximum, ImBool intersectWithCurrent);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPopClipRect")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PopClipRect();

                #endregion
            }
        }
    }
}
