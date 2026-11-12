namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            [StructLayout(LayoutKind.Explicit)]
            public struct StyleMod
            {
                [FieldOffset(0)] public ImStyle VarIdx;
                [FieldOffset(4)] public int     BackupInt1;
                [FieldOffset(8)] public int     BackupInt2;

                [FieldOffset(4)] public float BackupFloat1;
                [FieldOffset(8)] public float BackupFloat2;

                [FieldOffset(4)] public ImVec2 BackupVec;
            }
        }
    }
}
