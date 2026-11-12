namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            [InlineArray((int)WindowDockStyleColor.Count)]
            public struct WindowDockStyle
            {
                private Rgba32 _element;
            }
        }
    }
}
