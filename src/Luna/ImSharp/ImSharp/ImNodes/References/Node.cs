using static ImSharp.ImNodes.ImNodes;

namespace ImSharp.ImNodes;

/// <summary> A wrapper around ImNodes nodes. </summary>
public unsafe ref struct Node : IDisposable
{
    /// <summary> A direct reference to the internal data. </summary>
    private readonly ref Internal.NodeData _data;

    /// <summary> The unique ID of the node. </summary>
    public readonly NodeId Id
        => _data.Id;

    /// <summary> Whether the node is already ended. </summary>
    public bool Alive { get; private set; }


    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator NodeId(Node node)
        => node.Id;

    /// <summary> Begin a new node inside the current editor. </summary>
    /// <param name="id"> The desired unique ID of the new node. Can be any integer except for <seealso cref="NodeId.Invalid"/>. </param>
    /// <returns> A disposable object that ends the node on disposal. Use with using. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    internal Node(NodeId id)
    {
        _data = ref Api.BeginNodeInternal(id);
        Alive = true;
    }

    /// <inheritdoc cref="AttributeDisposable(ImSharp.ImNodes.AttributeId,PinShape,Internal.AttributeType)"/>
    public readonly AttributeDisposable InputPin(AttributeId id, PinShape shape = PinShape.CircleFilled)
        => new(id, shape, Internal.AttributeType.Input);

    /// <inheritdoc cref="AttributeDisposable(ImSharp.ImNodes.AttributeId,PinShape,Internal.AttributeType)"/>
    public readonly AttributeDisposable OutputPin(AttributeId id, PinShape shape = PinShape.CircleFilled)
        => new(id, shape, Internal.AttributeType.Output);

    /// <inheritdoc cref="AttributeDisposable(ImSharp.ImNodes.AttributeId,PinShape,Internal.AttributeType)"/>
    public readonly AttributeDisposable StaticAttribute(AttributeId id)
        => new(id, default, Internal.AttributeType.Static);

    /// <summary> Get or set the ability to click and drag this node. </summary>
    public readonly bool Draggable
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => _data.Draggable;
        [MethodImpl(ImSharpConfiguration.OptInl)]
        set => _data.Draggable = value;
    }

    /// <inheritdoc cref="ImNodes.NodeTitleBarDisposable(bool)"/>
    public readonly NodeTitleBarDisposable TitleBar()
        => Alive ? new NodeTitleBarDisposable(true) : default;

    /// <summary> Get the dimensions of this node. </summary>
    public readonly Vector2 Dimensions
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => _data.Rectangle.Size;
    }

    /// <summary> Get or set the position of this node in the screen space coordinate system, i.e. relative to the upper left corner of the containing window. </summary>
    public readonly Vector2 ScreenSpacePosition
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Editor->GridToScreen(_data.Origin);
        [MethodImpl(ImSharpConfiguration.OptInl)]
        set => _data.Origin = Editor->ScreenToGrid(value);
    }

    /// <summary> Get or set the position of this node in the editor coordinate system, i.e. relative to the upper left corner of the containing node editor. </summary>
    public readonly Vector2 EditorSpacePosition
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Editor->GridToEditor(_data.Origin);
        [MethodImpl(ImSharpConfiguration.OptInl)]
        set => _data.Origin = Editor->EditorToGrid(value);
    }

    /// <summary> Get or set the position of this node in the grid coordinate system, i.e. relative to the upper left corner of the containing node editor translated by the current panning (see <seealso cref="ImNodes.EditorContext.Panning"/>). </summary>
    public readonly Vector2 GridSpacePosition
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => _data.Origin;
        [MethodImpl(ImSharpConfiguration.OptInl)]
        set => _data.Origin = value;
    }

    /// <summary> Snap this node's origin to the grid if <seealso cref="ImNodesStyleFlags.GridSnapping"/> is enabled. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly void SnapToGrid()
        => _data.Origin = Internal.SnapOriginToGrid(_data.Origin);

    /// <summary> End the node on leaving scope. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public void Dispose()
    {
        if (!Alive)
            return;

        Api.EndNode();
        Alive = false;
    }
}
