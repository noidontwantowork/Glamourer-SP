namespace ImSharp.ImNodes;

public static partial class Internal
{
    /// <summary> The internally used index type for ImNodes nodes. </summary>
    public readonly record struct NodeIndex(int Index) : IIndex<NodeIndex>
    {
        /// <summary> An invalid attribute index. </summary>
        public static readonly NodeIndex Invalid = new(IIndex.InvalidIndex);

        /// <inheritdoc/>
        public bool IsValid
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get { return Index >= 0; }
        }

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator NodeIndex(uint v)
            => new((int)v);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator NodeIndex(int v)
            => new(v);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static explicit operator uint(NodeIndex v)
            => (uint)v.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static explicit operator int(NodeIndex v)
            => v.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static NodeIndex operator ++(NodeIndex index)
            => new(index.Index + 1);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static NodeIndex operator --(NodeIndex index)
            => new(index.Index - 1);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static NodeIndex operator +(NodeIndex index, int offset)
            => new(index.Index + offset);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static NodeIndex operator -(NodeIndex index, int offset)
            => new(index.Index - offset);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static NodeIndex operator +(int offset, NodeIndex index)
            => new(index.Index + offset);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static NodeIndex operator -(int offset, NodeIndex index)
            => new(index.Index - offset);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator >(NodeIndex left, NodeIndex right)
            => left.Index > right.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator >=(NodeIndex left, NodeIndex right)
            => left.Index >= right.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator <(NodeIndex left, NodeIndex right)
            => left.Index < right.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator <=(NodeIndex left, NodeIndex right)
            => left.Index <= right.Index;

        /// <inheritdoc/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static NodeIndex FromInt(int index)
            => index;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool Used(this in IdObjectPool<NodeData> pool, NodeIndex index)
        => pool.InUse[index.Index];

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static ref NodeData GetWrite(this ref IdObjectPool<NodeData> pool, NodeIndex index)
        => ref pool.Pool[index.Index];

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static ref readonly NodeData Get(this in IdObjectPool<NodeData> pool, NodeIndex index)
        => ref pool.Pool[index.Index];
}
