namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Start drawing a single group of multiple text pieces with different colors. </summary>
    /// <param name="firstText"> The first piece of the text. </param>
    /// <param name="color"> The color for the first text. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
    /// <returns> A wrapper object for function chaining. </returns>
    /// <remarks> Use chaining of the <see cref="MultiColorText.Then"/> family for additional text, and use <see cref="MultiColorText.End"/> to end the group. </remarks>
    [MethodImpl(ImSharpConfiguration.Inl)]
    [OverloadResolutionPriority(50)]
    public static MultiColorText TextMultiColored(Utf8TextHandler firstText, ColorParameter color = default)
        => new(firstText, color);

    /// <inheritdoc cref="TextMultiColored(Utf8TextHandler,ColorParameter)"/>
    [MethodImpl(ImSharpConfiguration.Inl)]
    [OverloadResolutionPriority(100)]
    public static MultiColorText TextMultiColored(Utf8TextHandler firstText, Vector4 color)
        => new(firstText, color);

    /// <inheritdoc cref="TextMultiColored(Utf8TextHandler,ColorParameter)"/>
    [MethodImpl(ImSharpConfiguration.Inl)]
    [OverloadResolutionPriority(75)]
    public static MultiColorText TextMultiColored(Utf8TextHandler firstText, Rgba32 color)
        => new(firstText, color);

    /// <summary> Wrapper struct to draw a multicolored text. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly ref struct MultiColorText
    {
        /// <summary> Used internally for <see cref="Then"/>. </summary>
        [MethodImpl(ImSharpConfiguration.Inl)]
        private MultiColorText(Utf8TextHandler text, Vector4 color, bool _)
        {
            Im.Line.NoSpacing();
            var c = ImGuiColor.Text.Push(color);
            Im.Text(text);
            c.Pop();
        }

        /// <inheritdoc cref="ImEx.TextMultiColored(Utf8TextHandler,Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.Inl)]
        [OverloadResolutionPriority(75)]
        internal MultiColorText(Utf8TextHandler text, Rgba32 color)
        {
            Im.Group();
            var c = ImGuiColor.Text.Push(color);
            Im.Text(text);
            c.Pop();
        }

        /// <inheritdoc cref="ImEx.TextMultiColored(Utf8TextHandler,ColorParameter)"/>
        [MethodImpl(ImSharpConfiguration.Inl)]
        [OverloadResolutionPriority(50)]
        internal MultiColorText(Utf8TextHandler text, ColorParameter color = default)
        {
            Im.Group();
            var c = ImGuiColor.Text.Push(color.CheckDefault(ImGuiColor.Text));
            Im.Text(text);
            c.Pop();
        }

        /// <inheritdoc cref="ImEx.TextMultiColored(Utf8TextHandler,Vector4)"/>
        [MethodImpl(ImSharpConfiguration.Inl)]
        [OverloadResolutionPriority(100)]
        internal MultiColorText(Utf8TextHandler text, Vector4 color)
        {
            Im.Group();
            var c = ImGuiColor.Text.Push(color);
            Im.Text(text);
            c.Pop();
        }

        /// <summary> Draw the next part of the text in a new color. </summary>
        /// <param name="text"> The next part of the text. </param>
        /// <param name="color"> The next color. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
        /// <returns> A wrapper object for function chaining. </returns>
        /// <remarks> Use chaining of the <see cref="MultiColorText.Then"/> family for additional text, and use <see cref="MultiColorText.End"/> to end the group. </remarks>
        [MethodImpl(ImSharpConfiguration.Inl)]
        [OverloadResolutionPriority(50)]
        public MultiColorText Then(Utf8TextHandler text, ColorParameter color = default)
            => new(text, color.CheckDefault(ImGuiColor.Text).ToVector(), true);

        /// <inheritdoc cref="Then(Utf8TextHandler,ColorParameter)"/>
        [MethodImpl(ImSharpConfiguration.Inl)]
        [OverloadResolutionPriority(75)]
        public MultiColorText Then(Utf8TextHandler text, Rgba32 color)
            => new(text, color.ToVector(), true);

        /// <inheritdoc cref="Then(Utf8TextHandler,ColorParameter)"/>
        [MethodImpl(ImSharpConfiguration.Inl)]
        [OverloadResolutionPriority(100)]
        public MultiColorText Then(Utf8TextHandler text, Vector4 color)
            => new(text, color, true);

        /// <summary> End the text group. </summary>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public void End()
            => Im.GroupDisposable.EndUnsafe();
    }
}
