namespace ImSharp;

/// <summary> Flags that control the behavior of a drag and drop objects. </summary>
[Flags]
public enum DragDropFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Prevent the behavior of <seealso cref="Im.DragDrop.Source"/> to open a tooltip. </summary>
    SourceNoPreviewTooltip = 1 << 0,

    /// <summary> Normally, when dragging items, <seealso cref="Im.Item.Hovered"/> returns false. Setting this flag prevents that. </summary>
    SourceNoDisableHover = 1 << 1,

    /// <summary> Normally, when dragging items, tree nodes and collapsing headers can be opened by holding the dragged item on top of them. Setting this flag prevents that. </summary>
    SourceNoHoldToOpenOthers = 1 << 2,

    /// <summary> Allow items without IDs (<seealso cref="Im.Text(Utf8TextHandler)"/>, <seealso cref="Im.Image"/>) to be used as drag sources by using temporary identifiers. </summary>
    SourceAllowNullId = 1 << 3,

    /// <summary> Denotes an external source from a different app. </summary>
    /// <remarks> Will always return true. Only one extern source can be active simultaneously. </remarks>
    SourceExtern = 1 << 4,

    /// <summary> Automatically expire the payload if the source stops existing (otherwise the payload is preserved while the source is being dragged.) </summary>
    SourceAutoExpirePayload = 1 << 5,

    /// <summary> <seealso cref="Im.DragDrop.AcceptPayload"/> returns true even before the mouse button is released. </summary>
    /// <remarks> Use <seealso cref="Im.Payload.Delivery"/> to test if the payload needs to be delivered. </remarks>
    AcceptBeforeDelivery = 1 << 10,

    /// <summary> Do not draw the default highlight rectangle when hovering over this target. </summary>
    AcceptNoDrawDefaultRect = 1 << 11,

    /// <summary> Hide the tooltip created through <seealso cref="Im.DragDrop.Source"/> when on this target. </summary>
    AcceptNoPreviewTooltip = 1 << 2,

    /// <summary> For peeking ahead and inspecting the payload before delivery. </summary>
    AcceptPeekOnly = AcceptBeforeDelivery | AcceptNoDrawDefaultRect,
}

/// <summary> Flags that control the behavior of a drag and drop source. </summary>
[Flags]
public enum DragDropSourceFlags : uint
{
    /// <inheritdoc cref="DragDropFlags.None"/>
    None = 0,

    /// <inheritdoc cref="DragDropFlags.SourceNoPreviewTooltip"/>
    SourceNoPreviewTooltip = 1 << 0,

    /// <inheritdoc cref="DragDropFlags.SourceNoDisableHover"/>
    SourceNoDisableHover = 1 << 1,

    /// <inheritdoc cref="DragDropFlags.SourceNoHoldToOpenOthers"/>
    SourceNoHoldToOpenOthers = 1 << 2,

    /// <inheritdoc cref="DragDropFlags.SourceAllowNullId"/>
    SourceAllowNullId = 1 << 3,

    /// <inheritdoc cref="DragDropFlags.SourceExtern"/>
    SourceExtern = 1 << 4,

    /// <inheritdoc cref="DragDropFlags.SourceAutoExpirePayload"/>
    SourceAutoExpirePayload = 1 << 5,
}

/// <summary> Flags that control the behavior of a drag and drop target. </summary>
[Flags]
public enum DragDropTargetFlags : uint
{
    /// <inheritdoc cref="DragDropFlags.None"/>
    None = 0,

    /// <inheritdoc cref="DragDropFlags.AcceptBeforeDelivery"/>
    AcceptBeforeDelivery = 1 << 10,

    /// <inheritdoc cref="DragDropFlags.AcceptNoDrawDefaultRect"/>
    AcceptNoDrawDefaultRect = 1 << 11,

    /// <inheritdoc cref="DragDropFlags.AcceptNoPreviewTooltip"/>
    AcceptNoPreviewTooltip = 1 << 2,

    /// <inheritdoc cref="DragDropFlags.AcceptPeekOnly"/>
    AcceptPeekOnly = AcceptBeforeDelivery | AcceptNoDrawDefaultRect,
}
