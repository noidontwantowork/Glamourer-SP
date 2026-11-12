namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper class for line-related actions. </summary>
    public static class Line
    {
        /// <summary> Undo the last carriage return after an item and stay on the same line. </summary>
        /// <param name="offsetFromStartX"> Offset to put the cursor on in window coordinates. If this is 0, use the current cursor position. </param>
        /// <param name="spacing"> Spacing from the resulting offset. If this is negative, if <paramref name="offsetFromStartX"/> is 0, this is set to 0, otherwise it is set to <seealso cref="ImGuiStyle.ItemSpacing"/>.X.</param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void Same(float offsetFromStartX, float spacing = -1)
            => Native.Methods.Layout.SameLine(offsetFromStartX, spacing);

        /// <summary> Undo the last carriage return after an item and stay on the same line with a regular <seealso cref="ImGuiStyle.ItemSpacing"/>.X distance to the last item. </summary>
        public static void Same()
            => Native.Methods.Layout.SameLine(0, -1);

        /// <summary> Undo the last carriage return after an item and stay on the same line while moving the cursor horizontally by <seealso cref="ImGuiStyle.ItemInnerSpacing"/>.X. </summary>
        /// <remarks> Use this when you add a custom label to a widget, group buttons, or similar. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SameInner()
            => Native.Methods.Layout.SameLine(0, Style.ItemInnerSpacing.X);

        /// <summary> Undo the last carriage return after an item and stay on the same line without adding any spacing. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void NoSpacing()
            => Native.Methods.Layout.SameLine(0, 0);

        /// <summary> Undo a previously called <seealso cref="Same"/> or force a new line. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void New()
            => Native.Methods.Layout.NewLine();

        /// <summary> Add vertical spacing of <seealso cref="ImGuiStyle.ItemSpacing"/>.Y. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void Spacing()
            => Native.Methods.Layout.Spacing();
    }
}
