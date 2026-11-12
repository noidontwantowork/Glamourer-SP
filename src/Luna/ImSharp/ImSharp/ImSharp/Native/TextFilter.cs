namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public struct TextFilter
        {
            public Buffer              InputBuffer;
            public ImVector<TextRange> Filters;
            public int                 CountGrep;

            [InlineArray(256)]
            public struct Buffer
            {
                private byte _element;
            }
        }
    }
}
