namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        [StructLayout(LayoutKind.Explicit)]
        public unsafe struct StoragePair
        {
            [FieldOffset(0)] public ImGuiId Key;
            [FieldOffset(8)] public int     IntegralValue;
            [FieldOffset(8)] public float   FloatValue;
            [FieldOffset(8)] public void*   PointerValue;
        }
    }
}
