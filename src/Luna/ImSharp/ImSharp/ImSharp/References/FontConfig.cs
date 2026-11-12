namespace ImSharp;

public static partial class Im
{
    /// <summary> A reference to configuration data for fonts. </summary>
    /// <param name="pointer"> The native pointer to the font configuration. </param>
    public readonly unsafe ref struct FontConfig(Native.ImFontConfig* pointer)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Native.ImFontConfig* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator FontConfig(Native.ImFontConfig* pointer)
            => new(pointer);
    }
}
