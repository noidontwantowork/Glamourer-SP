namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            /// <summary> A raw byte blob. </summary>
            /// <typeparam name="T"> Unused except for a reference name. </typeparam>
            public struct ImChunkStream<T> where T : unmanaged
            {
                public static string Name
                    => nameof(T);

                public ImVector<byte> Buffer;
            }
        }
    }
}
