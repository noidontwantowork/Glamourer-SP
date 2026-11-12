using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct OldColumnData
            {
                public float          OffsetNorm;
                public float          OffsetNormBeforeResize;
                public OldColumnFlags Flags;
                public ImRect         ClipRect;
            }
        }
    }
}
