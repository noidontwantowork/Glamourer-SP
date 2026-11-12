namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct MenuColumns
            {
                public uint        TotalWidth;
                public uint        NextTotalWidth;
                public ushort      Spacing;
                public ushort      OffsetIcon;
                public ushort      OffsetLabel;
                public ushort      OffsetShortcut;
                public ushort      OffsetMark;
                public WidthsArray Widths;

                [InlineArray(4)]
                public struct WidthsArray
                {
                    private ushort _element;
                }
            }
        }
    }
}
