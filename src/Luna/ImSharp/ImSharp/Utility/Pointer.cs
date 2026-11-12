namespace ImSharp;

/// <summary> A wrapper around pointers for use with generics. </summary>
/// <typeparam name="T"> The pointer type. </typeparam>
/// <param name="value"> The pointer. </param>
public readonly unsafe struct Pointer<T>(T* value) where T : unmanaged
{
    /// <summary> Get the actual pointer. </summary>
    public readonly T* Value = value;

    /// <summary> Get the pointer as a C# pointer type. </summary>
    public nint AsNint
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => (nint)Value;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator T*(Pointer<T> pointer)
        => pointer.Value;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Pointer<T>(T* pointer)
        => new(pointer);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static explicit operator Pointer<T>(nint pointer)
        => new((T*)pointer);

    public override string ToString()
        => $"0x{(nint)Value:X}";
}
