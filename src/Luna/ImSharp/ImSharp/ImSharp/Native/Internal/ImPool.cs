namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct ImPool<T> where T : unmanaged
            {
                public ImVector<T> Buffer;
                public Storage     Map;
                public ImPoolIndex FreeIndex;
                public ImPoolIndex AliveCount;
            }

            public readonly record struct ImPoolIndex(int Value)
            {
                public static implicit operator int(ImPoolIndex v)
                    => v.Value;

                public static implicit operator ImPoolIndex(int v)
                    => new(v);
            }
        }
    }
}
