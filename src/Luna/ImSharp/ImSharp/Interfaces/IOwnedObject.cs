namespace ImSharp;

/// <summary> Type information for an unmanaged type. </summary>
/// <typeparam name="TValue"> The type itself. </typeparam>
public interface ITypeInformation<TValue>
    where TValue : unmanaged
{
    /// <summary> Whether the type is trivially movable, i.e. does not contain any self-pointers or similar. </summary>
    public abstract static bool TriviallyMovable { get; }

    /// <summary> Whether the type is trivially destructible, i.e. <see cref="Destroy"/> does nothing. </summary>
    public abstract static bool TriviallyDestructible { get; }

    /// <summary> Whether the type is trivially constructible, i.e. <see cref="PlacementNew"/> only assigns <see langword="default"/>. </summary>
    public abstract static bool TriviallyConstructible { get; }

    /// <summary> Destroy the object at the given location. </summary>
    /// <param name="object"> The location to destroy. </param>
    public abstract static unsafe void Destroy(TValue* @object);

    /// <summary> Create a new object on allocated memory. </summary>
    /// <param name="address"> The object to create. </param>
    /// <returns> A reference to the created object. </returns>
    public abstract static unsafe ref TValue PlacementNew(void* address);
}

/// <summary> An interface for a trivial type managing no resources. </summary>
/// <typeparam name="TValue"> The trivial value type. </typeparam>
public interface ITrivialTypeInformation<TValue> : ITypeInformation<TValue>
    where TValue : unmanaged
{
    /// <inheritdoc/>
    static bool ITypeInformation<TValue>.TriviallyDestructible
        => true;

    /// <inheritdoc/>
    static bool ITypeInformation<TValue>.TriviallyConstructible
        => true;

    /// <inheritdoc/>
    static bool ITypeInformation<TValue>.TriviallyMovable
        => true;

    /// <inheritdoc/>
    static unsafe void ITypeInformation<TValue>.Destroy(TValue* @object)
    { }

    /// <inheritdoc/>
    static unsafe ref TValue ITypeInformation<TValue>.PlacementNew(void* address)
    {
        ref var self = ref *(TValue*)address;
        self = default;
        return ref self;
    }
}

/// <summary> An implementation of trivial type information for any value type. </summary>
/// <typeparam name="TValue"> An arbitrary trivial value type. </typeparam>
public sealed class TrivialTypeInformation<TValue> : ITrivialTypeInformation<TValue>
    where TValue : unmanaged
{
    /// <inheritdoc/>
    public static unsafe ref TValue PlacementNew(void* address)
    {
        ref var self = ref *(TValue*)address;
        self = default;
        return ref self;
    }
}
