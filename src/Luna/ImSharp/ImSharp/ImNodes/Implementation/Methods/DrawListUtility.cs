namespace ImSharp.ImNodes;

using ImSharp;
using static Im.Native;

public static unsafe partial class Internal
{
    public static void GrowChannels(Im.DrawList drawList, int numChannels)
    {
        var splitter = drawList.Splitter;
        if (splitter.Count is 1)
        {
            splitter.Split(numChannels + 1);
            return;
        }

        // NOTE: this logic has been lifted from ImDrawListSplitter::Split with slight modifications
        // to allow nested splits. The main modification is that we only create new ImDrawChannel
        // instances after splitter._Count, instead of over the whole splitter._Channels array like
        // the regular ImDrawListSplitter::Split method does.
        var oldCapacity   = splitter.Pointer->Channels.Size;
        var oldCount      = splitter.Count;
        var requiredCount = numChannels + oldCount;
        if (oldCapacity < requiredCount)
            splitter.Pointer->Channels.Resize<DrawChannelType>(requiredCount);
        splitter.Pointer->Count = requiredCount;
        for (var i = oldCount; i < requiredCount; ++i)
        {
            ref var channel = ref splitter.Pointer->Channels[i];
            // If we're inside the old capacity region of the array, we need to reuse the existing
            // memory of the command and index buffers.
            if (i < oldCapacity)
            {
                channel.CommandBuffer.Clear<TrivialTypeInformation<ImDrawCmd>>();
                channel.IndexBuffer.Clear<TrivialTypeInformation<ImDrawIdx>>();
            }
            // Else, we need to construct new draw channels.
            else
            {
                DrawChannelType.PlacementNew(Unsafe.AsPointer(ref channel));
            }

            // Maybe verify that this is zeroed correctly?
            var drawCommand = new ImDrawCmd
            {
                ClipRect  = drawList.Pointer->ClipRectStack[^1],
                TextureId = drawList.Pointer->TextureIdStack[^1],
            };
            channel.CommandBuffer.Resize<TrivialTypeInformation<ImDrawCmd>>(1);
            channel.CommandBuffer[0] = drawCommand;
        }
    }

    public static void SwapChannels(Im.DrawList drawList, int lhsIndex, int rhsIndex)
    {
        if (lhsIndex == rhsIndex)
            return;

        var splitter = drawList.Splitter;
        Debug.Assert(lhsIndex >= 0 && lhsIndex < splitter.Count);
        Debug.Assert(rhsIndex >= 0 && rhsIndex < splitter.Count);
        ref var lhsChannel = ref splitter.Pointer->Channels[lhsIndex];
        ref var rhsChannel = ref splitter.Pointer->Channels[rhsIndex];
        lhsChannel.CommandBuffer.Swap<TrivialTypeInformation<ImDrawCmd>>(ref rhsChannel.CommandBuffer);
        lhsChannel.IndexBuffer.Swap<TrivialTypeInformation<ImDrawIdx>>(ref rhsChannel.IndexBuffer);

        var currentChannel = splitter.Current;
        if (currentChannel == lhsIndex)
            splitter.Pointer->Current = rhsIndex;
        else if (currentChannel == rhsIndex)
            splitter.Pointer->Current = lhsIndex;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void SetDrawList(Im.DrawList windowDrawList)
    {
        ImNodes.Context->CanvasDrawList = windowDrawList;
        ImNodes.Context->NodeToSubmissionIndex.Clear();
        ImNodes.Context->NodeIndexSubmissionOrder.Clear<NodeIndex>();
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void AddNode(NodeIndex nodeIndex)
    {
        ImNodes.Context->NodeToSubmissionIndex.SetInt(nodeIndex.Index, ImNodes.Context->NodeIndexSubmissionOrder.Count);
        ImNodes.Context->NodeIndexSubmissionOrder.Add<NodeIndex>(nodeIndex);
        GrowChannels(ImNodes.Context->CanvasDrawList, 2);
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void AppendClickInteractionChannel()
        => GrowChannels(ImNodes.Context->CanvasDrawList, 1);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static int SubmissionToBackgroundChannelIndex(int submissionIndex)
        => 1 + 2 * submissionIndex;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static int SubmissionToForegroundChannelIndex(int submissionIndex)
        => SubmissionToBackgroundChannelIndex(submissionIndex) + 1;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void ActivateClickInteractionChannel()
    {
        var splitter = ImNodes.Context->CanvasDrawList.Splitter;
        splitter.SetChannel(splitter.Count - 1);
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void ActivateCurrentNodeForeground()
    {
        var index = SubmissionToForegroundChannelIndex(ImNodes.Context->NodeIndexSubmissionOrder.Count - 1);
        ImNodes.Context->CanvasDrawList.Splitter.SetChannel(index);
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void ActivateNodeBackground(NodeIndex nodeIndex)
    {
        var index = ImNodes.Context->NodeToSubmissionIndex.GetInt(nodeIndex.Index, IIndex.InvalidIndex);
        Debug.Assert(index is not IIndex.InvalidIndex);
        index = SubmissionToBackgroundChannelIndex(index);
        ImNodes.Context->CanvasDrawList.Splitter.SetChannel(index);
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void SwapSubmissionIndices(int lhs, int rhs)
    {
        if (lhs == rhs)
            return;

        var lhsForeground = SubmissionToForegroundChannelIndex(lhs);
        var rhsForeground = SubmissionToForegroundChannelIndex(rhs);
        var lhsBackground = SubmissionToBackgroundChannelIndex(lhs);
        var rhsBackground = SubmissionToBackgroundChannelIndex(rhs);

        SwapChannels(ImNodes.Context->CanvasDrawList, lhsBackground, rhsBackground);
        SwapChannels(ImNodes.Context->CanvasDrawList, lhsForeground, rhsForeground);
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static void SortChannelsByDepth(in ImVector<NodeIndex> nodeIndexDepthOrder)
    {
        var context = ImNodes.Context;
        if (context->NodeToSubmissionIndex.Count < 2)
            return;

        ref var submissionOrder = ref context->NodeIndexSubmissionOrder;
        Debug.Assert(nodeIndexDepthOrder.Count == submissionOrder.Count);
        var startIndex = nodeIndexDepthOrder.Count - 1;
        while (nodeIndexDepthOrder[startIndex] == submissionOrder[startIndex])
        {
            // early out if submission order and depth order are the same
            if (--startIndex is 0)
                return;
        }

        for (var depthIndex = startIndex; depthIndex > 0; --depthIndex)
        {
            var nodeIndex = nodeIndexDepthOrder[depthIndex];

            // Find the current index of the node_idx in the submission order array
            var submissionIndex = IIndex.InvalidIndex;
            for (var i = 0; i < submissionOrder.Count; ++i)
            {
                if (submissionOrder[i] == nodeIndex)
                {
                    submissionIndex = i;
                    break;
                }
            }

            Debug.Assert(submissionIndex is not IIndex.InvalidIndex);

            if (submissionIndex == depthIndex)
                continue;

            for (var j = submissionIndex; j < depthIndex; ++j)
            {
                SwapSubmissionIndices(j, j + 1);
                (submissionOrder[j], submissionOrder[j + 1]) = (submissionOrder[j + 1], submissionOrder[j]);
            }
        }
    }

    private sealed class DrawChannelType : ITypeInformation<ImDrawChannel>
    {
        public static bool TriviallyMovable
            => true;

        public static bool TriviallyDestructible
            => false;

        public static bool TriviallyConstructible
            => true;

        public static void Destroy(ImDrawChannel* @object)
        {
            @object->CommandBuffer.Free<TrivialTypeInformation<ImDrawCmd>>();
            @object->IndexBuffer.Free<TrivialTypeInformation<ImDrawIdx>>();
        }

        public static ref ImDrawChannel PlacementNew(void* address)
            => ref TrivialTypeInformation<ImDrawChannel>.PlacementNew(address);
    }
}
