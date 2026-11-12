namespace ImSharp.ImNodes;

/// <summary> Flags controlling the behavior of attribute pins. </summary>
[Flags]
public enum AttributeFlags : uint
{
    /// <summary> No specific behavior. </summary>
    None = 0,

    /// <summary> Allow detaching a link by left-clicking and dragging the link at a pin it is connected to. </summary>
    /// <remarks>
    ///   The user has to actually delete the link for this to work. A deleted link can be
    ///   detected by calling <seealso cref="ImNodes.Link.LinkDestroyed"/> after disposing <seealso cref="ImNodes.NodeEditorDisposable"/>.
    /// </remarks>
    EnableLinkDetachWithDragClick = 1 << 0,

    /// <summary>
    ///   Visual snapping of an in progress link will trigger IsLink Created/Destroyed events.
    ///   Allows previewing the creation of a link while dragging it across attributes.
    /// </summary>
    /// <remarks>
    ///   The user has to actually delete the link for this to work. A deleted link can be
    ///   detected by calling <seealso cref="ImNodes.Link.LinkDestroyed"/> after disposing <seealso cref="ImNodes.NodeEditorDisposable"/>.
    /// </remarks>
    EnableLinkCreationOnSnap = 1 << 1,

    /// <summary> Disable all interactivity with this pin. </summary>
    /// <remarks> Custom, not supported in the original ImNodes. </remarks>
    DisableInteractivity = 1 << 2,
}

public static class AttributeFlagsExtensions
{
    extension(AttributeFlags flag)
    {
        /// <inheritdoc cref="ImNodes.AttributeFlagDisposable.Push(AttributeFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ImNodes.AttributeFlagDisposable Push()
            => new ImNodes.AttributeFlagDisposable().Push(flag);

        /// <inheritdoc cref="ImNodes.AttributeFlagDisposable.Push(AttributeFlags,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ImNodes.AttributeFlagDisposable Push(bool condition)
            => condition ? new ImNodes.AttributeFlagDisposable().Push(flag) : new ImNodes.AttributeFlagDisposable();

        /// <summary> Check whether the attribute flags currently disable pins. </summary>
        /// <remarks> Custom, not supported in the original ImNodes. </remarks>
        public bool IsDisabled
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => flag.HasFlag(AttributeFlags.DisableInteractivity);
        }
    }
}
