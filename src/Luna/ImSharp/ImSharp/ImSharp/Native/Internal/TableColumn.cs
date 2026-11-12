namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct TableColumn
            {
                public  TableColumnFlags      Flags;
                public  float                 WidthGiven;
                public  float                 MinX;
                public  float                 MaxX;
                public  float                 WidthRequest;
                public  float                 WidthAuto;
                public  float                 StretchWeight;
                public  float                 InitStretchWeightOrWidth;
                public  ImRect                ClipRect;
                public  ImGuiId               UserId;
                public  float                 WorkMinX;
                public  float                 WorkMaxX;
                public  float                 ItemWidth;
                public  float                 ContentMaxXFrozen;
                public  float                 ContentMaxXUnfrozen;
                public  float                 ContentMaxXHeadersUsed;
                public  float                 ContentMaxXHeadersIdeal;
                public  short                 NameOffset;
                public  TableColumnIndex      DisplayOrder;
                public  TableColumnIndex      IndexWithinEnabledSet;
                public  TableColumnIndex      PreviousEnabledColumn;
                public  TableColumnIndex      NextEnabledColumn;
                public  TableColumnIndex      SortOrder;
                public  TableDrawChannelIndex DrawChannelCurrent;
                public  TableDrawChannelIndex DrawChannelFrozen;
                public  TableDrawChannelIndex DrawChannelUnfrozen;
                public  ImBool                IsEnabled;
                public  ImBool                IsUserEnabled;
                public  ImBool                IsUserEnabledNextFrame;
                public  ImBool                IsVisibleX;
                public  ImBool                IsVisibleY;
                public  ImBool                IsRequestOutput;
                public  ImBool                IsSkipItems;
                public  ImBool                IsPreserveWidthAuto;
                public  sbyte                 NavLayerCurrent;
                public  byte                  AutoFitQueue;
                public  byte                  CannotSkipItemsQueue;
                private byte                  _sortData;
                public  byte                  SortDirectionsAvailableList;

                public byte SortDirection
                {
                    get => (byte)(_sortData & 0x3);
                    set => _sortData = (byte)((_sortData & ~0x3) | (value & 0x3));
                }

                public byte SortDirectionAvailableCount
                {
                    get => (byte)((_sortData >> 2) & 0x3);
                    set => _sortData = (byte)((_sortData & ~0xC) | ((value & 0x3) << 2));
                }

                public byte SortDirectionAvailableMask
                {
                    get => (byte)(_sortData >> 4);
                    set => _sortData = (byte)((_sortData & 0xF) | (value << 4));
                }
            }

            public record struct TableColumnIndex(sbyte Value)
            {
                public static implicit operator sbyte(TableColumnIndex i)
                    => i.Value;

                public static implicit operator TableColumnIndex(sbyte i)
                    => new(i);
            }

            public record struct TableDrawChannelIndex(byte Value)
            {
                public static implicit operator byte(TableDrawChannelIndex i)
                    => i.Value;

                public static implicit operator TableDrawChannelIndex(byte i)
                    => new(i);
            }
        }
    }
}
