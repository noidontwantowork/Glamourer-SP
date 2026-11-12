using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct DockNode
            {
                public  ImGuiId                   Id;
                public  DockNodeFlags             SharedFlags;
                public  DockNodeFlags             LocalFlags;
                public  DockNodeFlags             LocalFlagsInWindows;
                public  DockNodeFlags             MergedFlags;
                public  DockNodeState             State;
                public  DockNode*                 ParentNode;
                public  ChildArray                ChildNodes;
                public  ImVector<Pointer<Window>> Windows;
                public  TabBar*                   TabBar;
                public  ImVec2                    Position;
                public  ImVec2                    Size;
                public  ImVec2                    SizeRef;
                public  Axis                      SplitAxis;
                public  WindowClass               WindowClass;
                public  uint                      LastBgColor;
                public  Window*                   HostWindow;
                public  Window*                   VisibleWindow;
                public  DockNode*                 CentralNode;
                public  DockNode*                 OnlyNodeWithWindows;
                public  int                       CountNodeWithWindows;
                public  int                       LastFrameAlive;
                public  int                       LastFrameActive;
                public  int                       LastFrameFocused;
                public  ImGuiId                   LastFocusedNodeId;
                public  ImGuiId                   SelectedTabId;
                public  ImGuiId                   WantCloseTabId;
                private uint                      _data;

                public DataAuthority AuthorityForPos
                {
                    get => (DataAuthority)(_data & 0x7u);
                    set => _data = (_data & ~0x7u) | ((uint)value & 0x7u);
                }

                public DataAuthority AuthorityForSize
                {
                    get => (DataAuthority)((_data >> 3) & 0x7u);
                    set => _data = (_data & ~(0x7u << 3)) | (((uint)value & 0x7u) << 3);
                }

                public DataAuthority AuthorityForViewport
                {
                    get => (DataAuthority)((_data >> 6) & 0x7u);
                    set => _data = (_data & ~(0x7u << 6)) | (((uint)value & 0x7u) << 6);
                }

                public bool IsVisible
                {
                    get => (_data & (1u << 9)) != 0u;
                    set => _data = value ? _data | (1u << 9) : _data & ~(1u << 9);
                }

                public bool IsFocused
                {
                    get => (_data & (1u << 10)) != 0u;
                    set => _data = value ? _data | (1u << 10) : _data & ~(1u << 10);
                }

                public bool IsBgDrawnThisFrame
                {
                    get => (_data & (1u << 11)) != 0u;
                    set => _data = value ? _data | (1u << 11) : _data & ~(1u << 11);
                }

                public bool HasCloseButton
                {
                    get => (_data & (1u << 12)) != 0u;
                    set => _data = value ? _data | (1u << 12) : _data & ~(1u << 12);
                }

                public bool HasWindowMenuButton
                {
                    get => (_data & (1u << 13)) != 0u;
                    set => _data = value ? _data | (1u << 13) : _data & ~(1u << 13);
                }

                public bool HasCentralNodeChild
                {
                    get => (_data & (1u << 14)) != 0u;
                    set => _data = value ? _data | (1u << 14) : _data & ~(1u << 14);
                }

                public bool WantCloseAll
                {
                    get => (_data & (1u << 15)) != 0u;
                    set => _data = value ? _data | (1u << 15) : _data & ~(1u << 15);
                }

                public bool WantLockSizeOnce
                {
                    get => (_data & (1u << 16)) != 0u;
                    set => _data = value ? _data | (1u << 16) : _data & ~(1u << 16);
                }

                public bool WantMouseMove
                {
                    get => (_data & (1u << 17)) != 0u;
                    set => _data = value ? _data | (1u << 17) : _data & ~(1u << 17);
                }

                public bool WantHiddenTabBarUpdate
                {
                    get => (_data & (1u << 18)) != 0u;
                    set => _data = value ? _data | (1u << 18) : _data & ~(1u << 18);
                }

                public bool WantHiddenTabBarToggle
                {
                    get => (_data & (1u << 19)) != 0u;
                    set => _data = value ? _data | (1u << 19) : _data & ~(1u << 19);
                }

                [InlineArray(2)]
                public struct ChildArray
                {
#pragma warning disable CS9184 // 'Inline arrays' language feature is not supported for inline array types with element field which is either a 'ref' field, or has type that is not valid as a type argument.
                    private DockNode* _element;
#pragma warning restore CS9184 // 'Inline arrays' language feature is not supported for inline array types with element field which is either a 'ref' field, or has type that is not valid as a type argument.
                }
            }
        }
    }
}
