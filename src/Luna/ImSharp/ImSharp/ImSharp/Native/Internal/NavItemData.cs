using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct NavItemData
            {
                public Window*   Window;
                public ImGuiId   Id;
                public ImGuiId   FocusScopeId;
                public ImRect    RectRelative;
                public ItemFlags InFlags;
                public float     DistanceBox;
                public float     DistanceCenter;
                public float     DistanceAxial;
            }
        }
    }
}
