using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Internal
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igImHashData")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId ImHashData(void* data, ulong size, uint seed);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igImHashStr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId ImHashStr(byte* data, ulong size, uint seed);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igImAlphaBlendColors")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Rgba32 ImAlphaBlendColors(Rgba32 colorA, Rgba32 colorB);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igImBezierCubicCalc")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ImBezierCubicCalc(ImVec2* ret, ImVec2 p1, ImVec2 p2, ImVec2 p3, ImVec2 p4, float t);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igImBezierCubicClosestPoint")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ImBezierCubicClosestPoint(ImVec2* ret, ImVec2 p1, ImVec2 p2, ImVec2 p3, ImVec2 p4, ImVec2 point,
                    int numSegments);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igImBezierCubicClosestPointCasteljau")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ImBezierCubicClosestPointCasteljau(ImVec2* ret, ImVec2 p1, ImVec2 p2, ImVec2 p3, ImVec2 p4,
                    ImVec2 point, float tesselationTolerance);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igImBezierQuadraticCalc")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ImBezierQuadraticCalc(ImVec2* ret, ImVec2 p1, ImVec2 p2, ImVec2 p3, float t);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igImLineClosestPoint")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ImLineClosestPoint(ImVec2* ret, ImVec2 a, ImVec2 b, ImVec2 point);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igImTriangleContainsPoint")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool ImTriangleContainsPoint(ImVec2 a, ImVec2 b, ImVec2 c, ImVec2 point);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igImTriangleClosestPoint")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ImTriangleClosestPoint(ImVec2* ret, ImVec2 a, ImVec2 b, ImVec2 c, ImVec2 point);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetCurrentWindowRead")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Native.Internal.Window* GetCurrentWindowRead();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetCurrentWindow")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Native.Internal.Window* GetCurrentWindow();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igFindWindowByID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Native.Internal.Window* FindWindowById(ImGuiId id);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igFindWindowByName")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Native.Internal.Window* FindWindowByName(byte* name);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igUpdateWindowParentAndRootLinks")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void UpdateWindowParentAndRootLinks(Native.Internal.Window* window, WindowFlags flags,
                    Native.Internal.Window* parentWindow);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCalcWindowNextAutoFitSize")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void CalcWindowNextAutoFitSize(ImVec2* ret, Native.Internal.Window* window);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsWindowChildOf")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsWindowChildOf(Native.Internal.Window* window, Native.Internal.Window* potentialParent,
                    ImBool popupHierarchy, ImBool dockHierarchy);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsWindowWithinBeginStackOf")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool
                    IsWindowWithinBeginStackOf(Native.Internal.Window* window, Native.Internal.Window* potentialParent);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsWindowAbove")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsWindowAbove(Native.Internal.Window* potentialAbove, Native.Internal.Window* potentialBelow);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsWindowNavFocusable")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsWindowNavFocusable(Native.Internal.Window* window);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igScrollToItem")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ScrollToItem(ScrollFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igScrollToRect")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ScrollToRect(Native.Internal.Window* window, in ImRect rect, ScrollFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igScrollToRectEx")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ScrollToRectEx(ImVec2* ret, Native.Internal.Window* window, in ImRect rect, ScrollFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetItemID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId GetItemId();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetItemStatusFlags")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ItemStatusFlags GetItemStatusFlags();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetItemFlags")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ItemFlags GetItemFlags();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetActiveID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId GetActiveId();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetFocusID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId GetFocusId();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetActiveID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetActiveID(ImGuiId id, Native.Internal.Window* window);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetFocusID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetFocusID(ImGuiId id, Native.Internal.Window* window);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igClearActiveID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ClearActiveId();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetHoveredID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId GetHoveredId();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetHoveredID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetHoveredId(ImGuiId id);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igKeepAliveID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void KeepAliveId(ImGuiId id);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igMarkItemEdited")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void MarkItemEdited(ImGuiId id);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igPushOverrideID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void PushOverrideId(ImGuiId id);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetIDWithSeed")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId GetIdWithSeed(byte* begin, byte* end, ImGuiId seed);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igItemSize_Vec2")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ItemSize(ImVec2 size, float textBaselineY);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCalcItemSize")]
                public static partial void CalcItemSize(ImVec2* result, ImVec2 min, float defaultWidth, float defaultHeight);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igItemAdd")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool ItemAdd(in ImRect boundingBox, ImGuiId id, ImRect* navigationBoundingBox, ItemFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igItemHoverable")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ItemHoverable(in ImRect boundingBox, ImGuiId id);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igActivateItem")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ActivateItem(ImGuiId id);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsDragDropActive")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsDragDropActive();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsDragDropBeingAccepted")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsDragDropBeingAccepted();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginDragDropTargetCustom")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragDropTargetCustom(in ImRect boundingBox, ImGuiId id);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRenderArrow")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void RenderArrow(ImDrawList* drawList, ImVec2 pos, Rgba32 color, Direction direction,
                    float scale);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRenderBullet")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void RenderBullet(ImDrawList* drawList, ImVec2 pos, Rgba32 color);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRenderCheckMark")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void RenderCheckMark(ImDrawList* drawList, ImVec2 pos, Rgba32 color, float size);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRenderArrowPointingAt")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void RenderArrowPointingAt(ImDrawList* drawList, ImVec2 pos, ImVec2 halfSize,
                    Direction direction, Rgba32 color);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRenderArrowDockMenu")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void RenderArrowDockMenu(ImDrawList* drawList, ImVec2 minimum, float size, Rgba32 color);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRenderRectFilledRangeH")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void RenderRectFilledRangeH(ImDrawList* drawList, in ImRect rect, Rgba32 color,
                    float xStartNorm, float xEndNorm, float rounding);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRenderRectFilledWithHole")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void RenderRectFilledWithHole(ImDrawList* drawList, in ImRect outer, in ImRect inner,
                    Rgba32 color, float rounding);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRenderFrame")]
                public static partial void RenderFrame(ImVec2 min, ImVec2 max, Rgba32 fillColor, ImBool border, float rounding);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRenderFrameBorder")]
                public static partial void RenderFrameBorder(ImVec2 min, ImVec2 max, float rounding);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRenderNavHighlight")]
                public static partial void RenderNavHighlight(ImRect bb, ImGuiId id, NavigationHighlightFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igRenderTextClippedEx")]
                public static partial void RenderTextClippedEx(ImDrawList* drawList, ImVec2 posMin, ImVec2 posMax, byte* text, byte* textDisplayEnd,
                    ImVec2* textSizeIfKnown, ImVec2 align, ImRect* clipRect);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igButtonEx")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool ButtonEx(byte* label, ImVec2 size, ButtonFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCloseButton")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool CloseButton(ImGuiId id, ImVec2 pos);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCollapseButton")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool CollapseButton(ImGuiId id, ImVec2 pos, Native.Internal.DockNode* dockNode);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCheckboxFlags_IntPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool CheckboxFlags(byte* label, long* flags, long value);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igCheckboxFlags_UintPtr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool CheckboxFlags(byte* label, ulong* flags, ulong value);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igButtonBehavior")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool ButtonBehavior(in ImRect boundingBox, ImGuiId id, ImBool* hovered, ImBool* held,
                    ButtonFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igDragBehavior")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool DragBehavior(ImGuiId id, DataType dataType, void* value, float speed, void* minimum, void* maximum,
                    byte* format, SliderFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSliderBehavior")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SliderBehavior(ImRect boundingBox, ImGuiId id, DataType dataType, void* value, void* minimum,
                    void* maximum, byte* format, SliderFlags flags, ImRect* outGrabBound);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSplitterBehavior")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SplitterBehavior(ImRect boundingBox, ImGuiId id, Axis axis, float* sizeLeft, float* sizeRight,
                    float minSizeLeft, float minSizeRight, float hoverExtend, float hoverDelay, Rgba32 color);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igTableGetHoveredColumn")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial int TableGetHoveredColumn();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetNextWindowScroll")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetNextWindowScroll(ImVec2 scroll);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginComboPopup")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginComboPopup(ImGuiId id, ImRect rect, ComboFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetItemUsingMouseWheel")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetItemUsingMouseWheel();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igImRotate")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ImRotate(ImVec2* ret, ImVec2 value, float cos, float sin);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igIsPopupOpen_ID")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool IsPopupOpen(ImGuiId id, PopupQueryFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igColorTooltip")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void ColorTooltip(byte* text, float* color, ColorEditorFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginPopupEx")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginPopupEx(ImGuiId id, WindowFlags flags);
            }
        }
    }
}
