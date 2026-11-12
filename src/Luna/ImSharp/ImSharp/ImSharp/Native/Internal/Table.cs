namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct Table
            {
                public  ImGuiId                        Id;
                public  TableFlags                     Flags;
                public  void*                          RawData;
                public  TableTempData*                 TempData;
                public  ImSpan<TableColumn>            Columns;
                public  ImSpan<TableColumnIndex>       DisplayOrderToIndex;
                public  ImSpan<TableCellData>          RowCellData;
                public  ulong                          EnabledMaskByDisplayOrder;
                public  ulong                          EnabledMaskByIndex;
                public  ulong                          VisibleMaskByIndex;
                public  ulong                          RequestOutputMaskByIndex;
                public  TableFlags                     SettingsLoadedFlags;
                public  int                            SettingsOffset;
                public  int                            LastFrameActive;
                public  int                            ColumnsCount;
                public  int                            CurrentRow;
                public  int                            CurrentColumn;
                public  short                          InstanceCurrent;
                public  short                          InstanceInteracted;
                public  float                          RowPositionY1;
                public  float                          RowPositionY2;
                public  float                          RowMinHeight;
                public  float                          RowTextBaseline;
                public  float                          RowIndentOffsetX;
                private uint                           _flagsData;
                public  int                            RowBgColorCounter;
                public  BgColorArray                   RowBgColor;
                public  uint                           BorderColorStrong;
                public  uint                           BorderColorLight;
                public  float                          BorderX1;
                public  float                          BorderX2;
                public  float                          HostIndentX;
                public  float                          MinColumnWidth;
                public  float                          OuterPaddingX;
                public  float                          CellPaddingX;
                public  float                          CellPaddingY;
                public  float                          CellSpacingY1;
                public  float                          CellSpacingX2;
                public  float                          InnerWidth;
                public  float                          ColumnsGivenWidth;
                public  float                          ColumnsAutoFitWidth;
                public  float                          ColumnsStretchSumWeights;
                public  float                          ResizedColumnNextWidth;
                public  float                          ResizeLockMinContentsX2;
                public  float                          RefScale;
                public  ImRect                         OuterRect;
                public  ImRect                         InnerRect;
                public  ImRect                         WorkRect;
                public  ImRect                         InnerClipRect;
                public  ImRect                         BgClipRect;
                public  ImRect                         Bg0ClipRectForDrawCommand;
                public  ImRect                         Bg2ClipRectForDrawCommand;
                public  ImRect                         HostClipRect;
                public  ImRect                         HostBackupInnerClipRect;
                public  Window*                        OuterWindow;
                public  Window*                        InnerWindow;
                public  TextBuffer                     ColumnsNames;
                public  ImDrawListSplitter*            DrawSplitter;
                public  TableInstanceData              InstanceDataFirst;
                public  ImVector<TableInstanceData>    InstanceDataExtra;
                public  TableColumnSortSpecs           SortSpecsSingle;
                public  ImVector<TableColumnSortSpecs> SortSpecsMulti;
                public  TableSortSpecs                 SortSpecs;
                public  TableColumnIndex               SortSpecsCount;
                public  TableColumnIndex               ColumnsEnabledCount;
                public  TableColumnIndex               ColumnsEnabledFixedCount;
                public  TableColumnIndex               DeclaredColumnsCount;
                public  TableColumnIndex               HoveredColumnBody;
                public  TableColumnIndex               HoveredColumnBorder;
                public  TableColumnIndex               AutoFitSingleColumn;
                public  TableColumnIndex               ResizedColumn;
                public  TableColumnIndex               LastResizedColumn;
                public  TableColumnIndex               HeldHeaderColumn;
                public  TableColumnIndex               ReorderColumn;
                public  TableColumnIndex               ReorderColumnDirection;
                public  TableColumnIndex               LeftMostEnabledColumn;
                public  TableColumnIndex               RightMostEnabledColumn;
                public  TableColumnIndex               LeftMostStretchedColumn;
                public  TableColumnIndex               RightMostStretchedColumn;
                public  TableColumnIndex               ContextPopupColumn;
                public  TableColumnIndex               FreezeRowsRequest;
                public  TableColumnIndex               FreezeRowsCount;
                public  TableColumnIndex               FreezeColumnsRequest;
                public  TableColumnIndex               FreezeColumnsCount;
                public  TableColumnIndex               RowCellDataCurrent;
                public  TableDrawChannelIndex          DummyDrawChannel;
                public  TableDrawChannelIndex          Bg2DrawChannelCurrent;
                public  TableDrawChannelIndex          Bg2DrawChannelUnfrozen;
                public  ImBool                         IsLayoutLocked;
                public  ImBool                         IsInsideRow;
                public  ImBool                         IsInitializing;
                public  ImBool                         IsSortSpecsDirty;
                public  ImBool                         IsUsingHeaders;
                public  ImBool                         IsContextPopupOpen;
                public  ImBool                         IsSettingsRequestLoad;
                public  ImBool                         IsSettingsDirty;
                public  ImBool                         IsDefaultDisplayOrder;
                public  ImBool                         IsResetAllRequest;
                public  ImBool                         IsResetDisplayOrderRequest;
                public  ImBool                         IsUnfrozenRows;
                public  ImBool                         IsDefaultSizingPolicy;
                public  ImBool                         MemoryCompacted;
                public  ImBool                         HostSkipItems;


                public TableRowFlags RowFlags
                {
                    get => (TableRowFlags)(_flagsData & 0xFFFFu);
                    set => _flagsData = (_flagsData & ~0xFFFFu) | ((uint)value & 0xFFFFu);
                }

                public TableRowFlags LastRowFlags
                {
                    get => (TableRowFlags)(_flagsData >> 16);
                    set => _flagsData = (_flagsData & 0xFFFFu) | (((uint)value & 0xFFFFu) << 16);
                }

                [InlineArray(2)]
                public struct BgColorArray
                {
                    private uint _element;
                }
            }
        }
    }
}
