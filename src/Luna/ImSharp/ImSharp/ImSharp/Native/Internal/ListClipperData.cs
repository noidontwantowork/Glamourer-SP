namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct ListClipperData
            {
                public ListClipper*               ListClipper;
                public float                      LossynessOffset;
                public int                        StepNumber;
                public int                        ItemsFrozen;
                public ImVector<ListClipperRange> Ranges;
            }
        }
    }
}
