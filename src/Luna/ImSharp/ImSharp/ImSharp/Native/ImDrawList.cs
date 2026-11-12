namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe partial struct ImDrawList
        {
            public ImVector<ImDrawCmd>   CommandBuffer;
            public ImVector<ImDrawIdx>   IndexBuffer;
            public ImVector<ImDrawVert>  VertexBuffer;
            public ImDrawListFlags       Flags;
            public uint                  VertexCurrentIndex;
            public void*                 Data;
            public byte*                 OwnerName;
            public ImDrawVert*           VertexWritePointer;
            public ImDrawIdx*            IndexWritePointer;
            public ImVector<ImVec4>      ClipRectStack;
            public ImVector<ImTextureId> TextureIdStack;
            public ImVector<ImVec2>      Path;
            public ImDrawCmdHeader       CommandHeader;
            public ImDrawListSplitter    Splitter;
            public float                 FringeScale;

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PushClipRect")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PushClipRect(ImDrawList* self, ImVec2 clipRectMin, ImVec2 clipRectMax,
                ImBool intersectWithCurrentClipRect);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PushClipRectFullScreen")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PushClipRectFullScreen(ImDrawList* self);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PopClipRect")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PopClipRect(ImDrawList* self);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PushTextureID")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PushTextureId(ImDrawList* self, ImTextureId textureId);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PopTextureId")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PopTextureId(ImDrawList* self);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_GetClipRectMin")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void GetClipRectMin(ImVec2* rectOut, ImDrawList* self);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_GetClipRectMax")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void GetClipRectMax(ImVec2* rectOut, ImDrawList* self);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddLine")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddLine(ImDrawList* self, ImVec2 p1, ImVec2 p2, Rgba32 color, float thickness);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddRect")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddRect(ImDrawList* self, ImVec2 pMin, ImVec2 pMax, Rgba32 color, float rounding, ImDrawFlagsRectangle flags,
                float thickness);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddRectFilled")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddRectFilled(ImDrawList* self, ImVec2 pMin, ImVec2 pMax, Rgba32 color, float rounding,
                ImDrawFlagsRectangle flags);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddRectFilledMultiColor")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddRectFilledMultiColor(ImDrawList* self, ImVec2 pMin, ImVec2 pMax, Rgba32 colorUpperLeft,
                Rgba32 colorUpperRight, Rgba32 colorBottomRight, Rgba32 colorBottomLeft);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddQuad")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddQuad(ImDrawList* self, ImVec2 p1, ImVec2 p2, ImVec2 p3, ImVec2 p4, Rgba32 color, float thickness);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddQuadFilled")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddQuadFilled(ImDrawList* self, ImVec2 p1, ImVec2 p2, ImVec2 p3, ImVec2 p4, Rgba32 color);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddTriangle")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddTriangle(ImDrawList* self, ImVec2 p1, ImVec2 p2, ImVec2 p3, Rgba32 color, float thickness);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddTriangleFilled")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddTriangleFilled(ImDrawList* self, ImVec2 p1, ImVec2 p2, ImVec2 p3, Rgba32 color);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddCircle")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddCircle(ImDrawList* self, ImVec2 center, float radius, Rgba32 color, int numSegments, float thickness);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddCircleFilled")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddCircleFilled(ImDrawList* self, ImVec2 center, float radius, Rgba32 color, int numSegments);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddNgon")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddNgon(ImDrawList* self, ImVec2 center, float radius, Rgba32 color, int numSegments, float thickness);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddNgonFilled")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddNgonFilled(ImDrawList* self, ImVec2 center, float radius, Rgba32 color, int numSegments);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddText_Vec2")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddText(ImDrawList* self, ImVec2 pos, Rgba32 color, byte* textBegin, byte* textEnd);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddText_FontPtr")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddText(ImDrawList* self, ImFont* font, float fontSize, ImVec2 pos, Rgba32 color, byte* textBegin,
                byte* textEnd, float wrapWidth, ImVec4* cpuFineClipRect);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddPolyline")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddPolyline(ImDrawList* self, ImVec2* points, int numPoints, Rgba32 color, ImDrawFlagsPath flags,
                float thickness);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddConvexPolyFilled")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddConvexPolyFilled(ImDrawList* self, ImVec2* points, int numPoints, Rgba32 color);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddBezierCubic")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddBezierCubic(ImDrawList* self, ImVec2 p1, ImVec2 p2, ImVec2 p3, ImVec2 p4, Rgba32 color, float thickness,
                int numSegments);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddBezierQuadratic")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddBezierQuadratic(ImDrawList* self, ImVec2 p1, ImVec2 p2, ImVec2 p3, Rgba32 color, float thickness,
                int numSegments);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddImage")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddImage(ImDrawList* self, ImTextureId userTextureId, ImVec2 minPosition, ImVec2 maxPosition,
                ImVec2 minUv, ImVec2 maxUv, uint color);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddImageQuad")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddImageQuad(ImDrawList* self, ImTextureId userTextureId, ImVec2 p1, ImVec2 p2, ImVec2 p3, ImVec2 p4,
                ImVec2 uv1, ImVec2 uv2, ImVec2 uv3, ImVec2 uv4, Rgba32 color);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_AddImageRounded")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void AddImageRounded(ImDrawList* self, ImTextureId userTextureId, ImVec2 minPosition, ImVec2 maxPosition,
                ImVec2 minUv, ImVec2 maxUv, Rgba32 color, float rounding, ImDrawFlagsRectangle flags);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PathClear")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PathClear(ImDrawList* self);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PathLineTo")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PathLineTo(ImDrawList* self, ImVec2 pos);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PathLineToMergeDuplicate")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PathLineToMergeDuplicate(ImDrawList* self, ImVec2 pos);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PathFillConvex")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PathFillConvex(ImDrawList* self, Rgba32 color);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PathStroke")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PathStroke(ImDrawList* self, Rgba32 color, ImDrawFlagsPath flags, float thickness);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PathArcTo")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PathArcTo(ImDrawList* self, ImVec2 center, float radius, float minAngle, float maxAngle,
                int numSegments);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PathArcToFast")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PathArcToFast(ImDrawList* self, ImVec2 center, float radius, int minClockPosition, int maxClockPosition);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PathBezierCubicCurveTo")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PathBezierCubicCurveTo(ImDrawList* self, ImVec2 p2, ImVec2 p3, ImVec2 p4, int numSegments);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PathBezierQuadraticCurveTo")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PathBezierQuadraticCurveTo(ImDrawList* self, ImVec2 p2, ImVec2 p3, int numSegments);

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImDrawList_PathRect")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial void PathRect(ImDrawList* self, ImVec2 rectMin, ImVec2 rectMax, float rounding, ImDrawFlagsRectangle flags);
        }
    }
}
