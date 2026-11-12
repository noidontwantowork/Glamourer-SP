using static ImSharp.ImNodes.ImNodes;

namespace ImSharp.ImNodes;

/// <summary> A wrapper around an ImNodes node editor. </summary>
public ref struct NodeEditor : IDisposable
{
    /// <summary> Whether the node editor is already ended. </summary>
    public bool Alive { get; private set; }

    /// <summary> Begin a new node editor. </summary>
    /// <returns> A disposable object that ends the node editor on disposal. Use with using. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    internal NodeEditor(bool _)
    {
        Alive = true;
        Api.BeginNodeEditor();
    }

    /// <summary> Disable hover interactions over nodes, links and pins in this editor. </summary>
    /// <remarks> Uses custom non-ImNodes functionality. </remarks>
    public readonly unsafe void DisableHoverInteraction(bool value)
    {
        Context->ImNodesUiState =
            value ? Context->ImNodesUiState | Internal.UiState.NoHovering : Context->ImNodesUiState & ~Internal.UiState.NoHovering;
    }

    /// <summary> Disable selection of nodes and links in this editor. </summary>
    /// <remarks> Uses custom non-ImNodes functionality. This also allows window movement with the left mouse-button. </remarks>
    public readonly unsafe void DisableSelection(bool value)
    {
        if (value)
        {
            Context->ImNodesUiState |= Internal.UiState.NoSelection;
            Editor->SelectedLinkIndices.Clear<Internal.LinkIndex>();
            Editor->SelectedNodeIndices.Clear<Internal.NodeIndex>();
            Editor->SelectedNodeOffsets.Clear<TrivialTypeInformation<Vector2>>();
        }
        else
        {
            Context->ImNodesUiState &= ~Internal.UiState.NoSelection;
        }
    }

    /// <summary> End the node editor on leaving scope. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public void Dispose()
    {
        if (!Alive)
            return;

        Alive = false;
        Api.EndNodeEditor();
    }

    /// <inheritdoc cref="ImSharp.ImNodes.Node(NodeId)"/>
    public readonly Node Node(NodeId id)
        => new(id);

    /// <summary> Create a link between two attributes using this ID. </summary>
    /// <param name="id"> The desired unique ID of the new link. Can be any integer except for <seealso cref="LinkId.Invalid"/>. </param>
    /// <param name="start"> The ID of one of the attributes. This attribute has to have a pin and be created beforehand. </param>
    /// <param name="end"> The ID of the other of the attributes. This attribute has to have a pin and be created beforehand. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly void Link(LinkId id, AttributeId start, AttributeId end)
        => Api.CreateLink(id, start, end);

    /// <summary> Get whether this node editor is currently hovered by the mouse cursor. </summary>
    public readonly bool Hovered
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Api.IsEditorHovered();
    }

    /// <inheritdoc cref="MiniMap(float,Action{NodeId,nint},nint,MiniMapLocation)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public readonly unsafe void MiniMap(float sizeFraction = 0.2f, MiniMapLocation location = MiniMapLocation.TopRight)
        => Api.MiniMap(sizeFraction, location, null, nint.Zero);

    /// <inheritdoc cref="MiniMap(float,Action{NodeId,nint},nint,MiniMapLocation)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    [OverloadResolutionPriority(50)]
    public readonly unsafe void MiniMap(float sizeFraction, Action<NodeId> action, MiniMapLocation location = MiniMapLocation.TopRight)
    {
        var ptr = (delegate* unmanaged<NodeId, nint, void>)Marshal.GetFunctionPointerForDelegate(NewAction);
        Api.MiniMap(sizeFraction, location, ptr, nint.Zero);
        return;

        void NewAction(NodeId nodeId, nint data)
        {
            action(nodeId);
        }
    }

    /// <summary> Add a navigable mini map to the editor. </summary>
    /// <param name="sizeFraction"> The fraction of the editors size the mini map should take up. </param>
    /// <param name="location"> The anchor position for the minimap. </param>
    /// <param name="action"> Behavior when hovering over a node in the mini map. </param>
    /// <param name="data"> Additional user data for the behavior action beyond the hovered node ID. </param>
    /// <remarks> Call this before disposing this <seealso cref="NodeEditorDisposable"/> but after all nodes and links have been established. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    [OverloadResolutionPriority(50)]
    public readonly unsafe void MiniMap(float sizeFraction, Action<NodeId, nint> action, nint data = 0,
        MiniMapLocation location = MiniMapLocation.TopRight)
    {
        var ptr = (delegate* unmanaged<NodeId, nint, void>)Marshal.GetFunctionPointerForDelegate(action);
        Api.MiniMap(sizeFraction, location, ptr, data);
    }

    /// <inheritdoc cref="MiniMap(float,Action{NodeId,nint},nint,MiniMapLocation)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    [OverloadResolutionPriority(100)]
    public readonly unsafe void MiniMap(float sizeFraction, delegate* unmanaged<NodeId, nint, void> action, nint data = 0,
        MiniMapLocation location = MiniMapLocation.TopRight)
        => Api.MiniMap(sizeFraction, location, action, data);

    /// <inheritdoc cref="MiniMap(float,Action{NodeId,nint},nint,MiniMapLocation)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    [OverloadResolutionPriority(100)]
    public readonly unsafe void MiniMap(float sizeFraction, delegate* unmanaged<NodeId, void*, void> action, void* data = null,
        MiniMapLocation location = MiniMapLocation.TopRight)
        => Api.MiniMap(sizeFraction, location, (delegate* unmanaged<NodeId, nint, void>)action, (nint)data);
}
