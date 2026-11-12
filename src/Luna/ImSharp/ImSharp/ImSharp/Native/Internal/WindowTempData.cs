using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct WindowTempData
            {
                public  ImVec2          CursorPosition;
                public  ImVec2          CursorPositionPreviousLine;
                public  ImVec2          CursorStartPosition;
                public  ImVec2          CursorMaxPosition;
                public  ImVec2          IdealMaxPosition;
                public  ImVec2          CurrentLineSize;
                public  ImVec2          PreviousLineSize;
                public  float           CurrentLineTextBaseOffset;
                public  float           PreviousLineTextBaseOffset;
                public  ImBool          IsSameLine;
                public  float           Indent;
                public  float           ColumnsOffset;
                public  float           GroupOffset;
                public  ImVec2          CursorStartPositionLossyness;
                public  NavigationLayer NavigationLayerCurrent;
                public  short           NavLayersActiveMask;
                public  short           NavLayersActiveMaskNext;
                public  ImGuiId         NavFocusScopeIdCurrent;
                public  ImBool          NavHideHighlightOneFrame;
                public  ImBool          NavHasScroll;
                public  ImBool          MenuBarAppending;
                public  ImVec2          MenuBarOffset;
                public  MenuColumns     MenuColumns;
                public  int             TreeDepth;
                public  uint            TreeJumpToParentOnPopMask;
                private ImVector<nint>  _childWindows;
                public  Storage*        StateStorage;
                public  OldColumns*     CurrentColumns;
                public  int             CurrentTableIndex;
                public  LayoutType      LayoutType;
                public  LayoutType      ParentLayoutType;
                public  float           ItemWidth;
                public  float           TextWrapPosition;
                public  ImVector<float> ItemWidthStack;
                public  ImVector<float> TextWrapPositionStack;

                public ImVector<Pointer<Window>> ChildWindows 
                {
                    get => *(ImVector<Pointer<Window>>*) Unsafe.AsPointer(ref _childWindows);
                    set => _childWindows = *(ImVector<nint>*) Unsafe.AsPointer(ref value);
                }
            }
        }
    }
}
