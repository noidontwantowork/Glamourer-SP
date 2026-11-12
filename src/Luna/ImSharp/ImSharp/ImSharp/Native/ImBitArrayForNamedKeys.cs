namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        [InlineArray((KeyExtensions.NamedKeyCount + 31) >> 5)]
        public struct ImBitArrayForNamedKeys
        {
            private uint _element;
        }
    }
}
