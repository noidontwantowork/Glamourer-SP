namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct WindowStackData
            {
                public Window*      Window;
                public LastItemData ParentLastItemDataBackup;
                public StackSizes   StackSizesOnBegin;
            }
        }
    }
}
