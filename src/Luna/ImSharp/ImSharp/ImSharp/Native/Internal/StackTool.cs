namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct StackTool
            {
                public int                      LastActiveFrame;
                public int                      StackLevel;
                public ImGuiId                  QueryId;
                public ImVector<StackLevelInfo> Results;
                public ImBool                   CopyToClipboardOnCtrlC;
                public float                    CopyToClipboardLastTime;

                public struct StackLevelInfo
                {
                    public  ImGuiId          Id;
                    public  sbyte            QueryFrameCount;
                    public  ImBool           QuerySuccess;
                    private byte             _dataType;
                    public  DescriptionArray Description;

                    public DataType DataType
                    {
                        get => (DataType)_dataType;
                        set => _dataType = (byte)value;
                    }

                    [InlineArray(57)]
                    public struct DescriptionArray
                    {
                        private byte _element;
                    }
                }
            }
        }
    }
}
