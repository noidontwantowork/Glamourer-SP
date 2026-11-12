using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct NextItemData
            {
                public NextItemDataFlags Flags;
                public float             Width;
                public ImGuiId           FocusScopeId;
                public Condition         OpenCondition;
                public ImBool            OpenVal;
            }
        }
    }
}
