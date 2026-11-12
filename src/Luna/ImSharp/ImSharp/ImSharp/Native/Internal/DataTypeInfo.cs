namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct DataTypeInfo
            {
                public ulong Size;
                public byte* Name;
                public byte* PrintFormat;
                public byte* ScanFormat;
            }
        }
    }
}
