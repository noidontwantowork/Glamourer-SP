namespace ImSharp;

/// <summary> Supported native data types for some functionality. </summary>
public enum DataType : int
{
    /// <summary> Signed 8-bit integer. </summary>
    S8,

    /// <summary> Unsigned 8-bit integer. </summary>
    U8,

    /// <summary> Signed 16-bit integer. </summary>
    S16,

    /// <summary> Unsigned 16-bit integer. </summary>
    U16,

    /// <summary> Signed 32-bit integer. </summary>
    S32,

    /// <summary> Unsigned 32-bit integer. </summary>
    U32,

    /// <summary> Signed 64-bit integer. </summary>
    S64,

    /// <summary> Unsigned 64-bit integer. </summary>
    U64,

    /// <summary> 32-bit floating point number. </summary>
    Float,

    /// <summary> 64-bit floating point number. </summary>
    Double,

    /// <summary> A string data type. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    String,

    /// <summary> A pointer type. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Pointer,

    /// <summary> A <seealso cref="ImGuiId"/> type. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Id,
}

public static class DataTypeExtensions
{
    /// <summary> Obtain the C# Type corresponding to the enum value. </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static Type ToType(this DataType type)
        => type switch
        {
            DataType.S8     => typeof(sbyte),
            DataType.U8     => typeof(byte),
            DataType.S16    => typeof(short),
            DataType.U16    => typeof(ushort),
            DataType.S32    => typeof(int),
            DataType.U32    => typeof(uint),
            DataType.S64    => typeof(long),
            DataType.U64    => typeof(ulong),
            DataType.Float  => typeof(float),
            DataType.Double => typeof(double),
            _               => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };

    /// <summary> Obtain the size in bytes corresponding to the enum value. </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static int ToSize(this DataType type)
        => type switch
        {
            DataType.S8     => sizeof(sbyte),
            DataType.U8     => sizeof(byte),
            DataType.S16    => sizeof(short),
            DataType.U16    => sizeof(ushort),
            DataType.S32    => sizeof(int),
            DataType.U32    => sizeof(uint),
            DataType.S64    => sizeof(long),
            DataType.U64    => sizeof(ulong),
            DataType.Float  => sizeof(float),
            DataType.Double => sizeof(double),
            _               => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };

    /// <summary> Obtain the C# Type corresponding to the enum value. </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static DataType From<T>(T value = default) where T : unmanaged, INumber<T>
    {
        if (typeof(T) == typeof(sbyte))
            return DataType.S8;

        if (typeof(T) == typeof(byte))
            return DataType.U8;

        if (typeof(T) == typeof(short))
            return DataType.S16;

        if (typeof(T) == typeof(ushort))
            return DataType.U16;

        if (typeof(T) == typeof(int))
            return DataType.S32;

        if (typeof(T) == typeof(uint))
            return DataType.U32;

        if (typeof(T) == typeof(long) || typeof(T) == typeof(nint))
            return DataType.S64;

        if (typeof(T) == typeof(ulong) || typeof(T) == typeof(nuint))
            return DataType.U64;

        if (typeof(T) == typeof(float))
            return DataType.Float;

        if (typeof(T) == typeof(double))
            return DataType.Double;

        throw new ArgumentOutOfRangeException($"Unsupported Type {typeof(T)}.");
    }
}
