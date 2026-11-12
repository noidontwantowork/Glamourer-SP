namespace ImSharp.ImNodes;

public static partial class Internal
{
    /// <summary> The internally used index type for ImNodes attributes. </summary>
    public readonly record struct AttributeIndex(int Index) : IIndex<AttributeIndex>
    {
        /// <summary> An invalid attribute index. </summary>
        public static readonly AttributeIndex Invalid = new(IIndex.InvalidIndex);

        /// <inheritdoc/>
        public bool IsValid
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get { return Index >= 0; }
        }

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator AttributeIndex(uint v)
            => new((int)v);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator AttributeIndex(int v)
            => new(v);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static explicit operator uint(AttributeIndex v)
            => (uint)v.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static explicit operator int(AttributeIndex v)
            => v.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static AttributeIndex operator ++(AttributeIndex index)
            => new(index.Index + 1);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static AttributeIndex operator --(AttributeIndex index)
            => new(index.Index - 1);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static AttributeIndex operator +(AttributeIndex index, int offset)
            => new(index.Index + offset);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static AttributeIndex operator -(AttributeIndex index, int offset)
            => new(index.Index - offset);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static AttributeIndex operator +(int offset, AttributeIndex index)
            => new(index.Index + offset);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static AttributeIndex operator -(int offset, AttributeIndex index)
            => new(index.Index - offset);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator >(AttributeIndex left, AttributeIndex right)
            => left.Index > right.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator >=(AttributeIndex left, AttributeIndex right)
            => left.Index >= right.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator <(AttributeIndex left, AttributeIndex right)
            => left.Index < right.Index;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool operator <=(AttributeIndex left, AttributeIndex right)
            => left.Index <= right.Index;

        /// <inheritdoc/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static AttributeIndex FromInt(int index)
            => index;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool Used(this in IdObjectPool<PinData> pool, AttributeIndex index)
        => pool.InUse[index.Index];

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static ref PinData GetWrite(this ref IdObjectPool<PinData> pool, AttributeIndex index)
        => ref pool.Pool[index.Index];

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static ref readonly PinData Get(this in IdObjectPool<PinData> pool, AttributeIndex index)
        => ref pool.Pool[index.Index];
}
