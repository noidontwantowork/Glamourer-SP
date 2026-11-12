namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe partial struct ListClipper
        {
            public int   DisplayStart;
            public int   DisplayEnd;
            public int   ItemsCount;
            public float ItemsHeight;
            public float StartPosY;
            public Data* TempData;

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiListClipper_ImGuiListClipper")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial ListClipper* Constructor();

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiListClipper_destroy")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void Destructor(ListClipper* self);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiListClipper_Begin")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void Begin(ListClipper* self, int count, float height);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiListClipper_End")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void End(ListClipper* self);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiListClipper_Step")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial ImBool Step(ListClipper* self);

            [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiListClipper_ForceDisplayRangeByIndices")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void ForceDisplayRangeByIndices(ListClipper* self, int itemMin, int itemMax);

            public unsafe partial struct Data
            {
                public ListClipper*    ListClipper;
                public float           LossynessOffset;
                public int             StepNo;
                public int             ItemsFrozen;
                public ImVector<Range> Ranges;
            }

            public unsafe partial struct Range
            {
                public int    Min;
                public int    Max;
                public ImBool PosToIndexConvert;
                public sbyte  PosToIndexOffsetMin;
                public sbyte  PosToIndexOffsetMax;

                public int Count
                    => Max - Min;
            }
        };
    }
}
