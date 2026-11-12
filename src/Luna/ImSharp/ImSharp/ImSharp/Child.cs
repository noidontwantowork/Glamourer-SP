namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper class around Child functions. </summary>
    public static class Child
    {
        /// <inheritdoc cref="ChildDisposable(ref Utf8LabelHandler,Vector2,bool,WindowFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ChildDisposable Begin(Utf8LabelHandler id, Vector2 size, bool border = false, WindowFlags flags = WindowFlags.None)
            => new(ref id, size, border, flags);

        /// <inheritdoc cref="ChildDisposable(ImGuiId,Vector2,bool,WindowFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ChildDisposable Begin(ImGuiId id, Vector2 size, bool border = false, WindowFlags flags = WindowFlags.None)
            => new(id, size, border, flags);

        /// <inheritdoc cref="ChildDisposable(ref Utf8LabelHandler,Vector2,bool,WindowFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ChildDisposable Begin(Utf8LabelHandler id, bool border = false, WindowFlags flags = WindowFlags.None)
            => new(ref id, ContentRegion.Available, border, flags);

        /// <inheritdoc cref="ChildDisposable(ImGuiId,Vector2,bool,WindowFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ChildDisposable Begin(ImGuiId id, bool border = false, WindowFlags flags = WindowFlags.None)
            => new(id, ContentRegion.Available, border, flags);
    }
}
