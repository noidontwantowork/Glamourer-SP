namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            /// <remarks> Other Text methods omitted because of lack of support of VarArgs. </remarks>
            public static unsafe partial class Text
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igTextUnformatted")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void TextUnformatted(byte* text, byte* end);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igValue_Bool")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void Value(byte* prefix, ImBool value);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igValue_Int")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void Value(byte* prefix, int value);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igValue_UInt")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void Value(byte* prefix, uint value);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igValue_Float")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void Value(byte* prefix, float value, byte* floatFormat);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCalcTextSize")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void CalcTextSize(ImVec2* ret, byte* text, byte* textEnd, ImBool hideDashes, float wrapWidth);
            }
        }
    }
}
