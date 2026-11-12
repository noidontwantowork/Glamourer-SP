namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe struct ImDrawCallback
        {
            public delegate* unmanaged<DrawList, ImDrawCmd*, void> Func;
        }
    }
}
