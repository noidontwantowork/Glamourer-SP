namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    /// <summary> Get the number of selected nodes in the last node editor. </summary>
    /// <remarks> Use after disposing the <seealso cref="ImSharp.ImNodes.NodeEditor"/>. </remarks>
    public static int SelectedNodeCount
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        get => Api.NumSelectedNodes();
    }

    /// <summary> Get the number of selected links in the last node editor. </summary>
    /// <remarks> Use after disposing the <seealso cref="ImSharp.ImNodes.NodeEditor"/>. </remarks>
    public static int SelectedLinkCount
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        get => Api.NumSelectedLinks();
    }

    /// <summary> Get the IDs of selected nodes in the last node editor. </summary>
    /// <returns> A newly allocated array of the IDs of all selected nodes. </returns>
    /// <remarks> Use after disposing the <seealso cref="ImSharp.ImNodes.NodeEditor"/>. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe NodeId[] GetSelectedNodes()
    {
        var count = SelectedNodeCount;
        var ret   = new NodeId[count];
        fixed (NodeId* ptr = ret)
        {
            Api.GetSelectedNodes(ptr);
        }

        return ret;
    }

    /// <summary> Get the IDs of selected links in the last node editor. </summary>
    /// <returns> A newly allocated array of the IDs of all selected links. </returns>
    /// <remarks> Use after disposing the <seealso cref="ImSharp.ImNodes.NodeEditor"/>. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe LinkId[] GetSelectedLinks()
    {
        var count = SelectedLinkCount;
        var ret   = new LinkId[count];
        fixed (LinkId* ptr = ret)
        {
            Api.GetSelectedLinks(ptr);
        }

        return ret;
    }

    /// <summary> Try to write the IDs of selected nodes in the last node editor into the given span. </summary>
    /// <param name="target"> A contiguous array to write the IDs to. If this is not large enough, it is not filled at all. </param>
    /// <returns> The number of selected nodes. </returns>
    /// <remarks> Use after disposing the <seealso cref="ImSharp.ImNodes.NodeEditor"/>. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe int TryGetSelectedNodes(Span<NodeId> target)
    {
        var count = SelectedNodeCount;
        if (target.Length < count)
            return count;

        fixed (NodeId* ptr = target)
        {
            Api.GetSelectedNodes(ptr);
        }

        return count;
    }

    /// <summary> Try to write the IDs of selected links in the last node editor into the given span. </summary>
    /// <param name="target"> A contiguous array to write the IDs to. If this is not large enough, it is not filled at all. </param>
    /// <returns> The number of selected links. </returns>
    /// <remarks> Use after disposing the <seealso cref="ImSharp.ImNodes.NodeEditor"/>. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static unsafe bool TryGetSelectedLinks(Span<LinkId> target)
    {
        var count = SelectedLinkCount;
        if (target.Length < count)
            return false;

        fixed (LinkId* ptr = target)
        {
            Api.GetSelectedLinks(ptr);
        }

        return true;
    }

    /// <summary> Clear all selected nodes in the last node editor. </summary>
    /// <remarks> Use after disposing the <seealso cref="ImSharp.ImNodes.NodeEditor"/>. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void ClearSelectedNodes()
        => Api.ClearNodeSelection();

    /// <summary> Clear all selected links in the last node editor. </summary>
    /// <remarks> Use after disposing the <seealso cref="ImSharp.ImNodes.NodeEditor"/>. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void ClearSelectedLinks()
        => Api.ClearLinkSelection();
}
