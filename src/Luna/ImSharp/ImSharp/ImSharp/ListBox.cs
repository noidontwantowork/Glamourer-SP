namespace ImSharp;

public static partial class Im
{
    /// <summary> Wrapper class around ListBox functions. </summary>
    public static class ListBox
    {
        /// <inheritdoc cref="ListBoxDisposable(ref Utf8LabelHandler,Vector2)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ListBoxDisposable Begin(Utf8LabelHandler label, Vector2 size = default)
            => new(ref label, size);
    }
}
