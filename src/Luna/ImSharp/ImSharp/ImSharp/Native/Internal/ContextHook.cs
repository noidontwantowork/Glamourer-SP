using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public unsafe struct ContextHook
            {
                public ImGuiId                                 HookId;
                public ContextHookType                         Type;
                public ImGuiId                                 Owner;
                public delegate*<Context*, ContextHook*, void> Callback;
                public void*                                   UserData;
            }
        }
    }
}
