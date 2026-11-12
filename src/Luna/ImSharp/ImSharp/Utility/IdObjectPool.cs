// ReSharper disable UnassignedField.Local

namespace ImSharp;

/// <summary> An object that knows its ID, type data and has some additional utility for the <see cref="IdObjectPool{T}"/>. </summary>
/// <typeparam name="TSelf"> The own type. </typeparam>
public unsafe interface IIdObject<TSelf> : ITypeInformation<TSelf>
    where TSelf : unmanaged, IIdObject<TSelf>
{
    /// <summary> The ID of the object. </summary>
    public ImGuiId Id { get; set; }

    /// <summary> Invoked after a new object is created within an <see cref="IdObjectPool{T}"/>. </summary>
    /// <param name="index"> The index in the object pool. </param>
    public abstract static void PostCreation(int index);

    /// <summary> The function to update the current <see cref="IdObjectPool{T}"/>. If not specialized, this should just invoke <see cref="IdObjectPool{T}.DefaultUpdate"/>. </summary>
    /// <param name="pool"> The pool to update. </param>
    public abstract static void UpdatePool(ref IdObjectPool<TSelf> pool);

    /// <summary> An extended version of <see cref="ITypeInformation{TValue}.PlacementNew"/> that gets passed the ID of the object. </summary>
    /// <param name="pointer"> The location to create the object at. </param>
    /// <param name="id"> The ID for the created object. </param>
    public abstract static void Create(TSelf* pointer, ImGuiId id);
}

/// <summary> An object pool to reuse allocated data for nodes, links and attributes. </summary>
/// <typeparam name="T"> The type of data to store. </typeparam>
public struct IdObjectPool<T> : IDisposable
    where T : unmanaged, IIdObject<T>
{
    /// <summary> A map from each objects <see cref="IIdObject{TSelf}.Id"/> to its current index. </summary>
    [UsedImplicitly]
    internal Im.Native.Storage IdMapStorage;

    /// <summary> The actual storage data for the objects. </summary>
    internal ImVector<T> Pool;

    /// <summary> A list of currently reusable indices. </summary>
    internal ImVector<int> FreeList;

    /// <summary> A bit set of all objects that are currently in use. </summary>
    internal ImBitSet InUse;

    /// <summary> The full count of allocated, but not necessarily used objects. </summary>
    public int FullCount
        => InUse.Count;

    /// <summary> Get the reference to the object at a specific index. </summary>
    /// <param name="index"> The index. </param>
    /// <returns> The object. </returns>
    public ref T this[int index]
        => ref Pool[index];

    /// <summary> Try to obtain an object by its ID. </summary>
    /// <param name="id"> The ID to look for. </param>
    /// <param name="ret"> On success, a pointer to the found object. </param>
    /// <returns> Whether an object with this ID exists or not. </returns>
    public unsafe bool TryGetObject(ImGuiId id, out T* ret)
    {
        var index = FindIndex(id);
        if (index is IIndex.InvalidIndex)
        {
            ret = null;
            return false;
        }

        ret = Pool.Data + index;
        return true;
    }

    /// <summary> Get this pools ID map as its wrapper struct. </summary>
    internal readonly unsafe Im.StateStorage IdMap
        => (Im.Native.Storage*)Unsafe.AsPointer(in IdMapStorage);

    /// <summary> Try to find the index of an object by its ID. </summary>
    /// <param name="id"> The ID to look for. </param>
    /// <returns> The index if it exists or <see cref="IIndex{TSelf}.InvalidIndex"/> otherwise. </returns>
    public readonly int FindIndex(ImGuiId id)
        => IdMap.GetInt(id, IIndex.InvalidIndex);

    /// <summary> Reset this object pool by marking all objects as not in use. </summary>
    public void Reset()
        => InUse.Clear();

    /// <summary> Find an existing object by its ID or create it if it does not exist. Also marks the object as in use. </summary>
    /// <param name="id"> The ID of the object. </param>
    /// <returns> The index of the found or created object. </returns>
    public unsafe int FindOrCreateIndex(ImGuiId id)
    {
        var index = FindIndex(id);
        // Object was found, mark it as in use and return.
        if (index is not IIndex.InvalidIndex)
        {
            InUse[index] = true;
            return index;
        }

        // Add a new object if necessary.
        if (!FreeList.PopBack(out var newIndex))
        {
            index = Pool.Count;
            Debug.Assert(index == InUse.Count);
            Pool.Resize<T>(index + 1);
            InUse.Add(true);
        }
        // Reuse existing storage.
        else
        {
            index           = newIndex;
            InUse[newIndex] = true;
        }

        // Create the object, and add it to the map.
        T.Create(Pool.Data + index, id);
        IdMap.SetInt(id, index);
        T.PostCreation(index);

        return index;
    }

    /// <summary> Find an existing object by its ID or create it if it does not exist. Also marks the object as in use. </summary>
    /// <param name="id"> The ID of the object. </param>
    /// <returns> A reference to the found or created object. </returns>
    public ref T FindOrCreateObject(ImGuiId id)
    {
        var index = FindOrCreateIndex(id);
        return ref Pool[index];
    }

    /// <summary> Free all storage allocated by this object pool. </summary>
    public void Dispose()
    {
        Pool.Free<T>();
        FreeList.Free<TrivialTypeInformation<int>>();
        InUse.Dispose();
        IdMapStorage.Data.Free<TrivialTypeInformation<Im.Native.StoragePair>>();
    }

    /// <summary> Update this object pool with its current data. </summary>
    public void Update()
        => T.UpdatePool(ref this);

    /// <summary> The default update method if not specialized further. </summary>
    /// <param name="pool"> The pool to update. </param>
    internal static unsafe void DefaultUpdate(ref IdObjectPool<T> pool)
    {
        // Iterate all created objects.
        for (var i = 0; i < pool.InUse.Count; ++i)
        {
            var id = pool.Pool[i].Id;
            // If the object is in use, or the state is weird, ignore it.
            if (pool.InUse[i] || pool.FindIndex(id) != i)
                continue;

            // Mark unused objects as free.
            pool.IdMap.SetInt(id, IIndex.InvalidIndex);
            pool.FreeList.Add<TrivialTypeInformation<int>>(i);
            T.Destroy(pool.Pool.Data + i);
        }
    }
}
