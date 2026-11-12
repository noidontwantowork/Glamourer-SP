namespace ImSharp;

/// <summary> A wrapper around the vector type used by ImGui. </summary>
public unsafe struct ImVector<T> : IReadOnlyList<T>
    where T : unmanaged
{
    /// <summary> The number of objects stored. </summary>
    public int Size;

    /// <summary> The number of objects that space was allocated for. </summary>
    public int Capacity;

    /// <summary> The pointer to the allocated space. </summary>
    public T* Data;

    /// <summary> Access the object at the given index. </summary>
    /// <remarks> Does not check for size. </remarks>
    public readonly ref T this[int index]
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => ref Data[index];
    }

    /// <inheritdoc/>
    T IReadOnlyList<T>.this[int index]
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Data[index];
    }

    /// <inheritdoc/>
    public readonly IEnumerator<T> GetEnumerator()
    {
        for (var i = 0; i < Size; ++i)
            yield return this[i];
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <inheritdoc/>
    public readonly int Count
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Size;
    }

    /// <summary> Find the index of a given value in this vector, if it exists. </summary>
    /// <param name="value"> The value to search for. </param>
    /// <returns> The index of the first occurence of the value in the vector, or an invalid index. </returns>
    public readonly int FindIndex(in T value)
        => new ReadOnlySpan<T>(Data, Size).IndexOf(value);

    /// <summary> Delete the value at the given index. </summary>
    /// <param name="index"> The index to remove. </param>
    public void RemoveAt<TTypeInformation>(int index) where TTypeInformation : ITypeInformation<T>
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Size);
        CheckMovable<TTypeInformation>();
        TTypeInformation.Destroy(Data + index);
        var span       = new Span<T>(Data, Size);
        var targetSpan = span[index..Size];
        var sourceSpan = targetSpan[1..];
        sourceSpan.CopyTo(targetSpan);
        --Size;
    }

    /// <summary> Add the given value at the end of the vector. </summary>
    /// <param name="value"> The value to add. </param>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public void Add<TTypeInformation>(in T value) where TTypeInformation : ITypeInformation<T>
    {
        var oldSize = Size;
        if (Capacity == oldSize)
            Reserve<TTypeInformation>(CapacityGrowth(oldSize + 1, Capacity));
        Data[oldSize] = value;
        ++Size;
    }

    /// <summary> Clear all data from the vector. </summary>
    public void Clear<TTypeInformation>() where TTypeInformation : ITypeInformation<T>
    {
        FreeAll<TTypeInformation>();
        Size = 0;
    }

    /// <summary> Resize the vector the same way ImGui does internally. </summary>
    /// <param name="newSize"> The new size. </param>
    [MethodImpl(ImSharpConfiguration.Opt)]
    public void Resize<TTypeInformation>(int newSize) where TTypeInformation : ITypeInformation<T>
    {
        if (newSize < Size)
        {
            if (!TTypeInformation.TriviallyDestructible)
            {
                var end = Data + newSize;
                for (var ptr = Data + Size - 1; end >= ptr; --ptr)
                    TTypeInformation.Destroy(ptr);
            }
        }
        else
        {
            if (newSize > Capacity)
                Reserve<TTypeInformation>(CapacityGrowth(newSize, Capacity));

            if (TTypeInformation.TriviallyConstructible)
            {
                new Span<T>(Data + Size, newSize - Size).Clear();
            }
            else
            {
                var ptr = Data + Size;

                for (var end = Data + newSize; ptr < end; ++ptr)
                    TTypeInformation.PlacementNew(ptr);
            }
        }

        Size = newSize;
    }

    /// <summary> Reserve space for the vector the same way ImGui does internally. </summary>
    /// <param name="newCapacity"> The new capacity. </param>
    /// <remarks> This can only grow the capacity. </remarks>
    [MethodImpl(ImSharpConfiguration.Opt)]
    public void Reserve<TTypeInformation>(int newCapacity) where TTypeInformation : ITypeInformation<T>
    {
        if (newCapacity <= Capacity)
            return;

        CheckMovable<TTypeInformation>();
        var newData = Im.Main.Alloc<T>(newCapacity);
        if (Data is not null)
        {
            new ReadOnlySpan<T>(Data, Size).CopyTo(new Span<T>(newData, newCapacity));
            Im.Main.Free(Data);
        }

        Data     = newData;
        Capacity = newCapacity;
    }

    /// <summary> Try to remove and obtain the last object in this vector. </summary>
    /// <param name="value"> The obtained value, if there is any. </param>
    /// <returns> True if the vector had at least one element, false otherwise. </returns>
    public bool PopBack(out T value)
    {
        if (Size is 0)
        {
            value = default;
            return false;
        }

        value = Data[--Size];
        return true;
    }

    /// <summary> Swap the content of this vector with the other vector. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public void Swap<TTypeInformation>(ref ImVector<T> other) where TTypeInformation : ITypeInformation<T>
    {
        CheckMovable<TTypeInformation>();
        (Size, other.Size)         = (other.Size, Size);
        (Capacity, other.Capacity) = (other.Capacity, Capacity);
        var tmp = Data;
        Data       = other.Data;
        other.Data = tmp;
    }

    /// <summary> Free the vector's data and reset it. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public void Free<TTypeInformation>() where TTypeInformation : ITypeInformation<T>
    {
        FreeAll<TTypeInformation>();
        Size     = 0;
        Capacity = 0;
        if (Data is not null)
        {
            Im.Main.Free(Data);
            Data = null;
        }
    }

    /// <summary> Growth schema for vectors. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static int CapacityGrowth(int newCapacity, int oldCapacity)
    {
        var targetCapacity = oldCapacity > 0 ? oldCapacity + (oldCapacity >> 1) : 8;
        return targetCapacity > newCapacity ? targetCapacity : newCapacity;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static void CheckMovable<TTypeInformation>() where TTypeInformation : ITypeInformation<T>
    {
        if (!TTypeInformation.TriviallyMovable)
            throw new NotSupportedException($"Can not move type {typeof(T).Name} as it is not trivially movable.");
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private void FreeAll<TTypeInformation>() where TTypeInformation : ITypeInformation<T>
    {
        if (TTypeInformation.TriviallyDestructible)
            return;

        var ptr = Data;
        for (var end = ptr + Size; ptr < end; ++ptr)
            TTypeInformation.Destroy(ptr);
    }
}
