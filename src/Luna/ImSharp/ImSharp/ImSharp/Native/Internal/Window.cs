using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe partial struct Window
            {
                public  byte*                Name;
                public  ImGuiId              Id;
                public  WindowFlags          Flags;
                public  WindowFlags          FlagsPreviousFrame;
                public  WindowClass          WindowClass;
                public  ViewportP*           Viewport;
                public  ImGuiId              ViewportId;
                public  ImVec2               ViewportPosition;
                public  int                  ViewportAllowPlatformMonitorExtend;
                public  ImVec2               Position;
                public  ImVec2               Size;
                public  ImVec2               SizeFull;
                public  ImVec2               ContentSize;
                public  ImVec2               ContentSizeIdeal;
                public  ImVec2               ContentSizeExplicit;
                public  ImVec2               WindowPadding;
                public  float                WindowRounding;
                public  float                WindowBorderSize;
                public  int                  NameBufferLength;
                public  ImGuiId              MoveId;
                public  ImGuiId              TabId;
                public  ImGuiId              ChildId;
                public  ImVec2               Scroll;
                public  ImVec2               ScrollMax;
                public  ImVec2               ScrollTarget;
                public  ImVec2               ScrollTargetCenterRatio;
                public  ImVec2               ScrollTargetEdgeSnapDistance;
                public  ImVec2               ScrollbarSizes;
                public  ImBool               ScrollbarX;
                public  ImBool               ScrollbarY;
                public  ImBool               ViewportOwned;
                public  ImBool               Active;
                public  ImBool               WasActive;
                public  ImBool               WriteAccessed;
                public  ImBool               Collapsed;
                public  ImBool               WantCollapseToggle;
                public  ImBool               SkipItems;
                public  ImBool               Appearing;
                public  ImBool               Hidden;
                public  ImBool               IsFallbackWindow;
                public  ImBool               IsExplicitChild;
                public  ImBool               HasCloseButton;
                public  sbyte                ResizeBorderHeld;
                public  short                BeginCount;
                public  short                BeginOrderWithinParent;
                public  short                BeginOrderWithinContext;
                public  short                FocusOrder;
                public  ImGuiId              PopupId;
                public  sbyte                AutoFitFramesX;
                public  sbyte                AutoFitFramesY;
                public  sbyte                AutoFitChildAxes;
                public  ImBool               AutoFitOnlyGrows;
                public  Direction            AutoPositionLastDirection;
                public  sbyte                HiddenFramesCanSkipItems;
                public  sbyte                HiddenFramesCannotSkipItems;
                public  sbyte                HiddenFramesForRenderOnly;
                public  sbyte                DisableInputsFrames;
                private uint                 _conditionData;
                public  ImVec2               SetWindowPositionValue;
                public  ImVec2               SetWindowPositionPivot;
                public  ImVector<ImGuiId>    IdStack;
                public  WindowTempData       TempData;
                public  ImRect               OuterRectClipped;
                public  ImRect               InnerRect;
                public  ImRect               InnerClipRect;
                public  ImRect               WorkRect;
                public  ImRect               ParentWorkRect;
                public  ImRect               ClipRect;
                public  ImRect               ContentRegionRect;
                public  ImVec2Short          HitTestHoleSize;
                public  ImVec2Short          HitTestHoleOffset;
                public  int                  LastFrameActive;
                public  int                  LastFrameJustFocused;
                public  float                LastTimeActive;
                public  float                ItemWidthDefault;
                public  Storage              StateStorage;
                public  ImVector<OldColumns> ColumnsStorage;
                public  float                FontWindowScale;
                public  float                FontDpiScale;
                public  int                  SettingsOffset;
                public  ImDrawList*          DrawList;
                public  ImDrawList           DrawListInstance;
                public  Window*              ParentWindow;
                public  Window*              ParentWindowInBeginStack;
                public  Window*              RootWindow;
                public  Window*              RootWindowPopupTree;
                public  Window*              RootWindowDockTree;
                public  Window*              RootWindowForTitleBarHighlight;
                public  Window*              RootWindowForNav;
                public  Window*              NavLastChildNavWindow;
                public  NavIdArray           NavLastIds;
                public  NavRectArray         NavRectRel;
                public  int                  MemoryDrawListIndexCapacity;
                public  int                  MemoryDrawListVertexCapacity;
                public  ImBool               MemoryCompacted;
                private byte                 _dockData;
                public  short                DockOrder;
                public  WindowDockStyle      DockStyle;
                public  DockNode*            DockNode;
                public  DockNode*            DockNodeAsHost;
                public  ImGuiId              DockId;
                public  ItemStatusFlags      DockTabItemStatusFlags;
                public  ImRect               DockTabItemRect;

                #region Bitfields

                public Condition SetWindowPositionAllowFlags
                {
                    get => (Condition)(_conditionData & 0xFFu);
                    set => _conditionData = (_conditionData & ~0xFFu) | ((uint)value & 0xFFu);
                }

                public Condition SetWindowSizeAllowFlags
                {
                    get => (Condition)((_conditionData >> 8) & 0xFFu);
                    set => _conditionData = (_conditionData & ~0xFF00u) | (((uint)value & 0xFFu) << 8);
                }

                public Condition SetWindowCollapsedAllowFlags
                {
                    get => (Condition)((_conditionData >> 16) & 0xFFu);
                    set => _conditionData = (_conditionData & ~0xFF0000u) | (((uint)value & 0xFFu) << 16);
                }

                public Condition SetWindowDockAllowFlags
                {
                    get => (Condition)((_conditionData >> 24) & 0xFFu);
                    set => _conditionData = (_conditionData & ~0xFF000000u) | (((uint)value & 0xFFu) << 24);
                }

                public bool DockIsActive
                {
                    get => (_dockData & 1) is 1;
                    set => _dockData = (byte)(value ? _dockData | 1u : _dockData & ~1u);
                }

                public bool DockNodeIsVisible
                {
                    get => (_dockData & 2) is 2;
                    set => _dockData = (byte)(value ? _dockData | 2u : _dockData & ~2u);
                }

                public bool DockTabIsVisible
                {
                    get => (_dockData & 4) is 4;
                    set => _dockData = (byte)(value ? _dockData | 4u : _dockData & ~4u);
                }

                public bool DockTabWantClose
                {
                    get => (_dockData & 8) is 8;
                    set => _dockData = (byte)(value ? _dockData | 8u : _dockData & ~8u);
                }

                #endregion

                [InlineArray((int)NavigationLayer.Count)]
                public struct NavIdArray
                {
                    private ImGuiId _element;
                }

                [InlineArray((int)NavigationLayer.Count)]
                public struct NavRectArray
                {
                    private ImRect _element;
                }

                #region Methods

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiWindow_GetID_Str")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId GetId(Window* self, byte* textStart, byte* textEnd);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiWindow_GetID_Ptr")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId GetId(Window* self, void* pointer);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiWindow_GetID_Int")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId GetId(Window* self, int value);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiWindow_GetIDFromRectangle")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId GetIdFromRectangle(Window* self, ImRect rect);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiWindow_Rect")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId Rect(ImRect* rectOut, Window* self);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiWindow_CalcFontSize")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float CalcFontSize(Window* self);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiWindow_TitleBarHeight")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float TitleBarHeight(Window* self);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiWindow_TitleBarRect")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId TitleBarRect(ImRect* rectOut, Window* self);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiWindow_MenuBarHeight")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial float MenuBarHeight(Window* self);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "ImGuiWindow_MenuBarRect")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImGuiId MenuBarRect(ImRect* rectOut, Window* self);

                #endregion
            }
        }
    }
}
