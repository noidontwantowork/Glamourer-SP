namespace ImSharp;

public static partial class Im
{
    /// <summary> Wrapper class around TabBars. </summary>
    public static class TabBar
    {
        /// <inheritdoc cref="TabBarDisposable(ref Utf8LabelHandler,TabBarFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static TabBarDisposable Begin(Utf8LabelHandler label, TabBarFlags flags = TabBarFlags.None)
            => new(ref label, flags);

        /// <inheritdoc cref="TabBarDisposable.Item(Utf8LabelHandler,TabItemFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static TabItemDisposable BeginItem(Utf8LabelHandler label, TabItemFlags flags = TabItemFlags.None)
            => new(ref label, flags);

        /// <inheritdoc cref="TabBarDisposable.Item(Utf8LabelHandler,TabItemFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static TabItemDisposable BeginItem(Utf8LabelHandler label, ref bool open, TabItemFlags flags = TabItemFlags.None)
            => new(ref label, ref open, flags);

        /// <inheritdoc cref="TabBarDisposable.Button(Utf8LabelHandler,TabItemFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool Button(Utf8LabelHandler label, TabItemFlags flags = TabItemFlags.None)
            => Native.Methods.TabBar.TabItemButton(label.Start(), flags);

        /// <summary> Notify the tab bar or docking system of a closed tab or window ahead. </summary>
        /// <param name="label"> The label or ID as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <remarks> For tab bars: Call this after <seealso cref="Begin"/> but before submitting the item. For windows, just call with the window's name. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe void SetItemClosed(Utf8LabelHandler label)
            => Native.Methods.TabBar.SetTabItemClosed(label.Start());
    }
}
