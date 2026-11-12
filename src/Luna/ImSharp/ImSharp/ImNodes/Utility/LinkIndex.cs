namespace ImSharp.ImNodes;

public static partial class Internal
{
    /// <summary> The internally used index type for ImNodes links. </summary>
    public readonly record struct LinkIndex(int Index) : IIndex<LinkIndex>
    {
        /// <summary> An invalid attribute index. </summary>
        public static readonly LinkIndex Invalid = new(IIndex.InvalidIndex);

        /// <inheritdoc/>
        public bool IsValid
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get { return Index >= 0; }
        }

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator LinkIndex(uint v)
            => new((int)v);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator LinkIndex(int v)
            => new(v);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static explicit operator uint(LinkIndex v)
            => (uint)v.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static explicit operator int(LinkIndex v)
            => v.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static LinkIndex operator ++(LinkIndex index)
            => new(index.Index + 1);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static LinkIndex operator --(LinkIndex index)
            => new(index.Index - 1);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static LinkIndex operator +(LinkIndex index, int offset)
            => new(index.Index + offset);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static LinkIndex operator -(LinkIndex index, int offset)
            => new(index.Index - offset);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static LinkIndex operator +(int offset, LinkIndex index)
            => new(index.Index + offset);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static LinkIndex operator -(int offset, LinkIndex index)
            => new(index.Index - offset);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator >(LinkIndex left, LinkIndex right)
            => left.Index > right.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator >=(LinkIndex left, LinkIndex right)
            => left.Index >= right.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator <(LinkIndex left, LinkIndex right)
            => left.Index < right.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator <=(LinkIndex left, LinkIndex right)
            => left.Index <= right.Index;

        /// <inheritdoc/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static LinkIndex FromInt(int index)
            => index;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool Used(this in IdObjectPool<LinkData> pool, LinkIndex index)
        => pool.InUse[index.Index];

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static ref LinkData GetWrite(this ref IdObjectPool<LinkData> pool, LinkIndex index)
        => ref pool.Pool[index.Index];

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static ref readonly LinkData Get(this in IdObjectPool<LinkData> pool, LinkIndex index)
        => ref pool.Pool[index.Index];
}
