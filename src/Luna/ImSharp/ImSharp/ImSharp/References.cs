namespace ImSharp;

public static unsafe partial class Im
{
    internal static Native.Internal.Context* ContextPointer;
    internal static Native.ImGuiStyle*       StylePointer;
    internal static Native.Io*               IoPointer;

    /// <summary> The current style data used by ImGui. </summary>
    /// <remarks> Make sure to update this by calling <seealso cref="ImSharpPerFrame.OnUpdate"/> at the start of your frames. </remarks>
    public static ImGuiStyle Style
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        get => StylePointer;
    }

    /// <summary> The current Input/Output data used by ImGui. </summary>
    /// <remarks> Make sure to update this by calling <seealso cref="ImSharpPerFrame.OnUpdate"/> at the start of your frames. </remarks>
    public static InputOutput Io
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        get => IoPointer;
    }

    /// <summary> The current internal context used by ImGui. </summary>
    /// <remarks> Make sure to update this by calling <seealso cref="ImSharpPerFrame.OnUpdate"/> at the start of your frames. </remarks>
    public static ImGuiContext Context
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        get => ContextPointer;
    }

    /// <remarks> Circumvent wrongly emitted error CS1612 when using setters. </remarks>
    /// <inheritdoc cref="Io"/>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static InputOutput GetIo()
        => IoPointer;

    /// <remarks> Circumvent wrongly emitted error CS1612 when using setters. </remarks>
    /// <inheritdoc cref="Style"/>
    public static ImGuiStyle GetStyle()
        => StylePointer;

    /// <remarks> Circumvent wrongly emitted error CS1612 when using setters. </remarks>
    /// <inheritdoc cref="Context"/>
    public static ImGuiContext GetContext()
        => ContextPointer;
}
