namespace ImSharp;

/// <summary> Represents a rented array, that must be returned to a pool after use. </summary>
/// <param name="pool"> The pool to which the array must be returned. </param>
/// <param name="array"> The rented array. </param>
/// <param name="requestedLength"> The length that was requested from the pool. </param>
/// <typeparam name="T"> The type of the array's elements. </typeparam>
/// <remarks> If <typeparamref name="T"/> is <see cref="Byte"/>, this struct is equivalent to <see cref="BufferLease{T}"/>. </remarks>
public readonly struct ArrayLease<T>(ArrayPool<T> pool, T[] array, int requestedLength) : IDisposable
{
    /// <summary> The rented array. </summary>
    public readonly T[] Array = array;

    /// <summary> The length that was requested from the pool. </summary>
    public readonly int RequestedLength = requestedLength;

    /// <summary> A slice at the beginning of the array, of the requested length. </summary>
    public ArraySegment<T> Segment
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        get => new(Array, 0, RequestedLength);
    }

    /// <summary> A span over a slice at the beginning of the array, of the requested length. </summary>
    public Span<T> Span
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        get => Array.AsSpan(0, RequestedLength);
    }

    /// <summary> Returns the rented array to the pool. </summary>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public void Dispose()
        => pool.Return(Array);
}

/// <summary> Represents a rented buffer used to store <c>unmanaged</c> structs, that must be returned to a pool after use. </summary>
/// <param name="pool"> The pool to which the buffer must be returned. </param>
/// <param name="array"> The rented buffer. </param>
/// <param name="requestedLength"> The length that was requested from the pool. </param>
/// <typeparam name="T"> The type that will actually be stored in the buffer's bytes. </typeparam>
/// <remarks> If <typeparamref name="T"/> is <see cref="Byte"/>, this struct is equivalent to <see cref="ArrayLease{T}"/>. </remarks>
public readonly struct BufferLease<T>(ArrayPool<byte> pool, byte[] array, int requestedLength) : IDisposable where T : unmanaged
{
    /// <summary> The rented buffer. </summary>
    public readonly byte[] Array = array;

    /// <summary> The length that was requested from the pool, in <typeparamref name="T"/>s. </summary>
    public readonly int RequestedLength = requestedLength;

    /// <summary> A slice at the beginning of the buffer, of the requested length. </summary>
    public unsafe ArraySegment<byte> Segment
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        get => new(Array, 0, RequestedLength * sizeof(T));
    }

    /// <summary> A span over a slice at the beginning of the buffer, of the requested length. </summary>
    public unsafe Span<T> Span
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        get => MemoryMarshal.Cast<byte, T>(Array.AsSpan(0, RequestedLength * sizeof(T)));
    }

    /// <summary> Returns the rented buffer to the pool. </summary>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public void Dispose()
        => pool.Return(Array);
}

/// <summary> Extensions to <see cref="ArrayPool{T}"/> to rent arrays as lease structs, for use with <c>using</c>. </summary>
public static class ArrayPoolExtensions
{
    /// <summary> Retrieves a buffer that is at least the requested length. </summary>
    /// <param name="pool"> The pool from which to rent the buffer. </param>
    /// <param name="minimumLength"> The minimum length of the array. </param>
    /// <typeparam name="T"> The type of the array's elements. </typeparam>
    /// <returns> A lease for an array of type T that is at least <paramref name="minimumLength" /> in length. Use with <c>using</c>. </returns>
    [OverloadResolutionPriority(10)]
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static ArrayLease<T> RentLease<T>(this ArrayPool<T> pool, int minimumLength)
        => new(pool, pool.Rent(minimumLength), minimumLength);

    /// <summary> Retrieves a buffer that is at least the requested length. </summary>
    /// <param name="pool"> The pool from which to rent the buffer. </param>
    /// <param name="minimumLength"> The minimum length of the array. It will be multiplied by the size of <typeparamref name="T"/>. </param>
    /// <typeparam name="T"> The type that will actually be stored into the array. </typeparam>
    /// <returns> A lease for an array of bytes that is at least <paramref name="minimumLength" /> <typeparamref name="T"/>s in length. Use with <c>using</c>. </returns>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public static unsafe BufferLease<T> RentLease<T>(this ArrayPool<byte> pool, int minimumLength) where T : unmanaged
        => new(pool, pool.Rent(minimumLength * sizeof(T)), minimumLength);
}
