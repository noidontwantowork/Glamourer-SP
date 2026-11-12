namespace ImSharp;

/// <summary> An unmanaged bit set of arbitrary size. </summary>
public struct ImBitSet : IReadOnlyList<bool>, IDisposable
{
    private const int BitShift     = 6;
    private const int BitMask      = 0b111111;
    private const int BitsPerValue = 1 << BitShift;

    private ImVector<ulong> _storage;

    /// <summary> The current capacity of the bit sets in available bits. </summary>
    public int Capacity
        => _storage.Capacity * BitsPerValue;

    /// <inheritdoc/>
    public IEnumerator<bool> GetEnumerator()
    {
        var numValues = Count >> BitShift;
        for (var i = 0; i < numValues; ++i)
        {
            var value = _storage[i];
            for (var j = 0; j < BitsPerValue; ++j)
            {
                yield return (value & 1UL) is 1;

                value >>= 1;
            }
        }

        var remainingValues = Count & BitMask;
        if (remainingValues is 0)
            yield break;

        var lastValue = _storage[numValues];
        for (var j = 0; j < remainingValues; ++j)
        {
            yield return (lastValue & 1UL) is 1;

            lastValue >>= 1;
        }
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <inheritdoc/>
    public int Count
        => _storage.Count;

    /// <summary> Add a new value at the end of the bitset. </summary>
    /// <param name="value"> The value. </param>
    public void Add(bool value)
    {
        var oldCount = Count;
        var newCount = oldCount + 1;
        if (Capacity < newCount)
        {
            // Can only be a multiple of BitsPerValue
            _storage.Size = oldCount >> BitShift;
            _storage.Reserve<TrivialTypeInformation<ulong>>(_storage.Size + 1);
        }

        _storage.Size  = newCount;
        this[oldCount] = value;
    }

    /// <summary> Set all bits from the first to the last bit to false or true. </summary>
    /// <param name="firstBit"> The index of the first bit to set. </param>
    /// <param name="lastBit"> The index of the last bit to set. </param>
    /// <param name="value"> Whether to set the bits to true or false. </param>
    public void SetRange(int firstBit, int lastBit, bool value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(firstBit);
        ArgumentOutOfRangeException.ThrowIfLessThan(firstBit, lastBit);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(lastBit, Count);
        if (firstBit == lastBit)
            this[firstBit] = value;
        _storage.Size = lastBit;
        Resize(lastBit, value);
    }

    /// <summary> Set all bits to false. </summary>
    public unsafe void Clear()
        => new Span<ulong>(_storage.Data, _storage.Capacity).Clear();

    /// <summary> Resize the bit set to the new value. If this increases the size, the new values are initialized to the given value. </summary>
    /// <param name="newSize"> The desired new size. </param>
    /// <param name="initValue"> The value to set newly added bits to, if <paramref name="newSize"/> is larger than the current size. </param>
    public unsafe void Resize(int newSize, bool initValue = false)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(newSize);
        if (newSize <= Count)
        {
            _storage.Size = newSize;
            return;
        }

        var newValue = initValue ? ulong.MaxValue : 0UL;
        if (newSize > Capacity)
        {
            var oldSize = Count;
            _storage.Size = (oldSize + BitsPerValue - 1) >> BitShift;
            _storage.Reserve<TrivialTypeInformation<ulong>>((newSize + BitsPerValue - 1) >> BitShift);
            _storage.Size = oldSize;
        }

        var startIndex = Count >> BitShift;
        var startBit   = Count & BitMask;
        if (initValue)
            _storage[startIndex] |= newValue & ~((1UL << startBit) - 1);
        else
            _storage[startIndex] &= (1UL << startBit) - 1;

        // We don't care about the values after the actual size.
        var pointer = _storage.Data + startIndex + 1;
        var end     = _storage.Data + _storage.Capacity;
        for (; pointer < end; ++pointer)
            *pointer = newValue;
    }

    /// <summary> Get whether all bits are currently false. </summary>
    public bool IsEmpty()
    {
        var numValues = Count >> BitShift;
        for (var i = 0; i < numValues; ++i)
        {
            if (_storage[i] is not 0)
                return false;
        }

        var remainingValues = Count & BitMask;
        if (remainingValues > 0)
            return (_storage[numValues] & ((1UL << remainingValues) - 1)) is 0;

        return true;
    }

    /// <summary> Count the number of bits currently set to true. </summary>
    public int CountSet()
    {
        var sum = 0;

        var numValues = Count >> BitShift;
        for (var i = 0; i < numValues; ++i)
            sum += BitOperations.PopCount(_storage[i]);

        var remainingValues = Count & BitMask;
        sum += BitOperations.PopCount(_storage[numValues] & ((1UL << remainingValues) - 1));
        return sum;
    }

    /// <inheritdoc/>
    public void Dispose()
        => _storage.Free<TrivialTypeInformation<ulong>>();

    /// <summary> Get or set the bit at the given index. </summary>
    public bool this[int index]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, Count);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            var valueIndex = index >> BitShift;
            var bitIndex   = index & BitMask;
            return ((_storage[valueIndex] >> bitIndex) & 1) is 1;
        }
        set
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, Count);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            var     valueIndex = index >> BitShift;
            var     bitIndex   = index & BitMask;
            ref var v          = ref _storage[valueIndex];
            var     flag       = 1UL << bitIndex;
            v = value ? v | flag : v & ~flag;
        }
    }
}
