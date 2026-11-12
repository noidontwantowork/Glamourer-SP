using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct LastItemData
            {
                public ImGuiId         Id;
                public ItemFlags       InFlags;
                public ItemStatusFlags StatusFlags;
                public ImRect          Rect;
                public ImRect          NavRect;
                public ImRect          DisplayRect;
            }
        }
    }
}
