namespace ImSharp;

public static partial class Im
{
    /// <summary> Create a non-existent item taking up the given width. </summary>
    /// <param name="width"> The width. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void Dummy(float width)
        => Native.Methods.Layout.Dummy(new Vector2(width, 0));

    /// <summary> Create a non-existent item taking up the given width and height. </summary>
    /// <param name="width"> The width. </param>
    /// <param name="height"> The height. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void Dummy(float width, float height)
        => Native.Methods.Layout.Dummy(new Vector2(width, height));

    /// <summary> Create a non-existent item taking up the given width and height. </summary>
    /// <param name="size"> The size. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void Dummy(Vector2 size)
        => Native.Methods.Layout.Dummy(size);

    /// <summary> Create a non-existent item taking up the given width scaled by the global scale. </summary>
    /// <param name="width"> The unscaled width. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void ScaledDummy(float width)
        => Native.Methods.Layout.Dummy(new Vector2(width * Io.GlobalScale, 0));

    /// <summary> Create a non-existent item taking up the given width and height scaled by the global scale. </summary>
    /// <param name="width"> The unscaled width. </param>
    /// <param name="height"> The unscaled height. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void ScaledDummy(float width, float height)
        => Native.Methods.Layout.Dummy(new Vector2(width * Io.GlobalScale, height * Io.GlobalScale));

    /// <summary> A square dummy the size of the current <seealso cref="ImGuiStyle.FrameHeight"/> (e.g. the size of a checkbox). </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void FrameDummy()
        => Native.Methods.Layout.Dummy(new Vector2(Style.FrameHeight));
}
