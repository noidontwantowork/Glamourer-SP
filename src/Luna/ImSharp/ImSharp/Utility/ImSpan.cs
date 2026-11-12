namespace ImSharp;

/// <summary> A wrapper around the span type used by ImGui. </summary>
public readonly unsafe struct ImSpan<T>(T* begin, T* end) : IReadOnlyList<T> where T : unmanaged
{
    /// <summary> The pointer to the start of the span. </summary>
    public readonly T* Begin = begin;

    /// <summary> The pointer to the start of the span. </summary>
    public readonly T* End = end;

    /// <summary> Create a span from a beginning and a size. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public ImSpan(T* begin, int count)
        : this(begin, begin + count)
    { }

    /// <inheritdoc/>
    public int Count
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => (int)(End - Begin);
    }

    /// <summary> Access the object at the given index. </summary>
    /// <remarks> Does not check for size. </remarks>
    public ref T this[int index]
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => ref Begin[index];
    }

    /// <inheritdoc/>
    T IReadOnlyList<T>.this[int index]
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Begin[index];
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        var count = Count;
        for (var i = 0; i < count; ++i)
            yield return this[i];
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
