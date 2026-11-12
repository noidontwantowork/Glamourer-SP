using System.Collections.Frozen;

namespace ImSharp;

/// <summary> Extensions for enum types. </summary>
public static class EnumExtensions
{
    /// <summary> Get the aggregate bit-wise OR of all passed values. </summary>
    /// <typeparam name="TEnum"> The type of the enum. </typeparam>
    /// <param name="values"> The separate enum values to OR up. </param>
    /// <returns> The aggregate value. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static TEnum Or<TEnum>(this IEnumerable<TEnum> values)
        where TEnum : unmanaged, Enum
        => values.Aggregate(default(TEnum), Or);

    /// <param name="value"> The value to check against the flags. </param>
    /// <typeparam name="TEnum"> The type of the enum, which should consist of flags. </typeparam>
    extension<TEnum>(TEnum value) where TEnum : unmanaged, Enum
    {
        /// <summary> Check whether at least one of the given flags is set. </summary>
        /// <param name="flags"> The flags to check for. </param>
        /// <returns> True if at least one flag in <paramref name="flags"/> is set in <paramref name="value"/>. </returns>
        /// <remarks> If <paramref name="flags"/> is 0, this always returns false. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool CheckAny(TEnum flags)
            => !EqualityComparer<TEnum>.Default.Equals(value.And(flags), default);

        /// <summary> Check whether all given flags are set. </summary>
        /// <param name="flags"> The flags to check for. </param>
        /// <returns> True if each flag in <paramref name="flags"/> is set in <paramref name="value"/>. </returns>
        /// <remarks> If <paramref name="flags"/> is 0, this always returns true. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool CheckAll(TEnum flags)
            => EqualityComparer<TEnum>.Default.Equals(value.And(flags), flags);

        /// <summary> Check whether none of the given flags are set. </summary>
        /// <param name="flags"> The flags to check for. </param>
        /// <returns> True if not a single flag in <paramref name="flags"/> is set in <paramref name="value"/>. </returns>
        /// <remarks> If <paramref name="flags"/> is 0, this always returns true. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool CheckNone(TEnum flags)
            => EqualityComparer<TEnum>.Default.Equals(value.And(flags), default);

        /// <summary> Return the bit-wise OR of two generic enum values. </summary>
        /// <param name="rhs"> The right-hand value. </param>
        /// <returns> The bit-wise OR of both values. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe TEnum Or(TEnum rhs)
            => sizeof(TEnum) switch
            {
                1 => ConvertOr<TEnum, byte>(value, rhs),
                2 => ConvertOr<TEnum, ushort>(value, rhs),
                4 => ConvertOr<TEnum, uint>(value, rhs),
                8 => ConvertOr<TEnum, ulong>(value, rhs),
                _ => throw new BitwiseEnumException<TEnum>(),
            };

        /// <summary> Return the bit-wise AND of two generic enum values. </summary>
        /// <param name="rhs"> The right-hand value. </param>
        /// <returns> The bit-wise AND of both values. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe TEnum And(TEnum rhs)
            => sizeof(TEnum) switch
            {
                1 => ConvertAnd<TEnum, byte>(value, rhs),
                2 => ConvertAnd<TEnum, ushort>(value, rhs),
                4 => ConvertAnd<TEnum, uint>(value, rhs),
                8 => ConvertAnd<TEnum, ulong>(value, rhs),
                _ => throw new BitwiseEnumException<TEnum>(),
            };

        /// <summary> Return the bit-wise AND of two generic enum values, with the right-hand value bit-wise inverted. </summary>
        /// <param name="rhs"> The right-hand value. </param>
        /// <returns> The bit-wise AND of <paramref cref="value"/> and the bit-wise inverse of <paramref cref="rhs"/>. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe TEnum AndNot(TEnum rhs)
            => sizeof(TEnum) switch
            {
                1 => ConvertAndNot<TEnum, byte>(value, rhs),
                2 => ConvertAndNot<TEnum, ushort>(value, rhs),
                4 => ConvertAndNot<TEnum, uint>(value, rhs),
                8 => ConvertAndNot<TEnum, ulong>(value, rhs),
                _ => throw new BitwiseEnumException<TEnum>(),
            };

        /// <summary> Return the bit-wise NOT of a generic enum value. </summary>
        /// <returns> The bit-wise inverse of the value. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe TEnum Not()
            => sizeof(TEnum) switch
            {
                1 => ConvertNot<TEnum, byte>(value),
                2 => ConvertNot<TEnum, ushort>(value),
                4 => ConvertNot<TEnum, uint>(value),
                8 => ConvertNot<TEnum, ulong>(value),
                _ => throw new BitwiseEnumException<TEnum>(),
            };

        /// <summary> Return the bit-wise XOR of two generic enum values. </summary>
        /// <param name="rhs"> The right-hand value. </param>
        /// <returns> The bit-wise XOR of both values. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe TEnum Xor(TEnum rhs)
            => sizeof(TEnum) switch
            {
                1 => ConvertXor<TEnum, byte>(value, rhs),
                2 => ConvertXor<TEnum, ushort>(value, rhs),
                4 => ConvertXor<TEnum, uint>(value, rhs),
                8 => ConvertXor<TEnum, ulong>(value, rhs),
                _ => throw new BitwiseEnumException<TEnum>(),
            };
    }

    extension<T>(T v) where T : unmanaged, Enum
    {
        /// <summary> Get all values of an enumeration more efficiently than with <see cref="Enum.GetValues"/>. </summary>
        /// <remarks> If used in a generic context, you can invoke <see cref="ImSharp.EnumExtensions.get_Values{T}"/> instead. </remarks>
        public static IReadOnlyList<T> Values
            => Values<T>.Data;

        /// <summary> Get all names of an enumeration more efficiently than with <see cref="Enum.GetNames"/>. </summary>
        /// <remarks> If used in a generic context, you can invoke <see cref="ImSharp.EnumExtensions.get_NamesU16{T}"/> instead. </remarks>
        public static IReadOnlyList<string> NamesU16
            => NamesU16<T>.Data;

        /// <summary> Get all names of an enumeration more efficiently than with <see cref="Enum.GetNames"/>. </summary>
        /// <remarks> If used in a generic context, you can invoke <see cref="ImSharp.EnumExtensions.get_NamesU8{T}"/> instead. </remarks>
        public static IReadOnlyList<StringU8> NamesU8
            => NamesU8<T>.Data;

        /// <summary> Get all names and values of an enumeration more efficiently than with <see cref="Enum.GetNames"/>. </summary>
        /// <remarks> If used in a generic context, you can invoke <see cref="ImSharp.EnumExtensions.get_NamesAndValuesU16{T}"/> instead. </remarks>
        public static IReadOnlyList<(string Name, T Value)> NamesAndValuesU16
            => NamesAndValuesU16<T>.Data;

        /// <summary> Get all names and values of an enumeration more efficiently than with <see cref="Enum.GetNames"/>. </summary>
        /// <remarks> If used in a generic context, you can invoke <see cref="ImSharp.EnumExtensions.get_NamesAndValuesU8{T}"/> instead. </remarks>
        public static IReadOnlyList<(StringU8 Name, T Value)> NamesAndValuesU8
            => NamesAndValuesU8<T>.Data;

        /// <summary> Try to parse an enumeration name to its value more efficiently than with <see cref="Enum.TryParse"/>. </summary>
        /// <param name="name"> The name to parse. </param>
        /// <param name="value"> The value on success, <c>default</c> on failure. </param>
        /// <returns> True on success, false on failure.</returns>
        /// <remarks> This is case-insensitive. </remarks>
        [OverloadResolutionPriority(50)]
        public static bool Parse(ReadOnlySpan<char> name, out T value)
            => LookupU16<T>.SpanLookup.TryGetValue(name, out value);

        /// <summary> Try to parse an enumeration name to its value more efficiently than with <see cref="Enum.TryParse"/>. </summary>
        /// <param name="name"> The name to parse. </param>
        /// <param name="value"> The value on success, <c>default</c> on failure. </param>
        /// <returns> True on success, false on failure.</returns>
        /// <remarks> This is case-insensitive. </remarks>
        [OverloadResolutionPriority(100)]
        public static bool Parse(string name, out T value)
            => LookupU16<T>.Data.TryGetValue(name, out value);

        /// <summary> Try to parse an enumeration name to its value more efficiently than with <see cref="Enum.TryParse"/>. </summary>
        /// <param name="name"> The name to parse. </param>
        /// <param name="value"> The value on success, <c>default</c> on failure. </param>
        /// <returns> True on success, false on failure.</returns>
        /// <remarks> This is (ASCII) case-insensitive. </remarks>
        [OverloadResolutionPriority(50)]
        public static bool Parse(ReadOnlySpan<byte> name, out T value)
            => LookupU8<T>.SpanLookup.TryGetValue(name, out value);

        /// <summary> Try to parse an enumeration name to its value more efficiently than with <see cref="Enum.TryParse"/>. </summary>
        /// <param name="name"> The name to parse. </param>
        /// <param name="value"> The value on success, <c>default</c> on failure. </param>
        /// <returns> True on success, false on failure.</returns>
        /// <remarks> This is (ASCII) case-insensitive. </remarks>
        [OverloadResolutionPriority(100)]
        public static bool Parse(StringU8 name, out T value)
            => LookupU8<T>.Data.TryGetValue(name, out value);

        /// <summary> Get whether an enumeration value is defined by name at least once. </summary>
        public bool Defined
            => TextU8<T>.Data.ContainsKey(v);

        /// <summary> Get the first name of the given value in the enumeration. </summary>
        public string String
            => TextU16<T>.Data.TryGetValue(v, out var t) ? t : v.ToString();

        /// <summary> Get the first name of the given value in the enumeration as a UTF8 string. </summary>
        public StringU8 StringU8
            => TextU8<T>.Data.TryGetValue(v, out var t) ? t : new StringU8($"{v}");

        /// <summary> Whether the enumeration is marked as flags. </summary>
        public static bool IsFlags
            => Flags<T>.IsFlags;

        /// <summary> Whether the value only consists of defined flags. </summary>
        public bool FlagsDefined
            => Flags<T>.IsFlags ? EqualityComparer<T>.Default.Equals(Flags<T>.All.Or(v), Flags<T>.All) : v.Defined;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static unsafe TEnum ConvertOr<TEnum, TInteger>(TEnum lhs, TEnum rhs)
        where TEnum : unmanaged, Enum
        where TInteger : unmanaged, IBitwiseOperators<TInteger, TInteger, TInteger>
    {
        var ret = *(TInteger*)&lhs | *(TInteger*)&rhs;
        return *(TEnum*)&ret;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static unsafe TEnum ConvertAnd<TEnum, TInteger>(TEnum lhs, TEnum rhs)
        where TEnum : unmanaged, Enum
        where TInteger : unmanaged, IBitwiseOperators<TInteger, TInteger, TInteger>
    {
        var ret = *(TInteger*)&lhs & *(TInteger*)&rhs;
        return *(TEnum*)&ret;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static unsafe TEnum ConvertNot<TEnum, TInteger>(TEnum lhs)
        where TEnum : unmanaged, Enum
        where TInteger : unmanaged, IBitwiseOperators<TInteger, TInteger, TInteger>
    {
        var ret = ~*(TInteger*)&lhs;
        return *(TEnum*)&ret;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static unsafe TEnum ConvertAndNot<TEnum, TInteger>(TEnum lhs, TEnum rhs)
        where TEnum : unmanaged, Enum
        where TInteger : unmanaged, IBitwiseOperators<TInteger, TInteger, TInteger>
    {
        var ret = *(TInteger*)&lhs & ~*(TInteger*)&rhs;
        return *(TEnum*)&ret;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static unsafe TEnum ConvertXor<TEnum, TInteger>(TEnum lhs, TEnum rhs)
        where TEnum : unmanaged, Enum
        where TInteger : unmanaged, IBitwiseOperators<TInteger, TInteger, TInteger>
    {
        var ret = *(TInteger*)&lhs ^ *(TInteger*)&rhs;
        return *(TEnum*)&ret;
    }

    /// <summary> Exception thrown when a bitwise operator on a generic enum fails. </summary>
    /// <typeparam name="TEnum"> The type of Enum, used for name and size. </typeparam>
    /// <param name="name"> The name of the method, automatically supplied. </param>
    private sealed unsafe class BitwiseEnumException<TEnum>([CallerMemberName] string? name = null)
        : InvalidOperationException(
            $"Unable to use bit-wise enum extension {name} on {typeof(TEnum).Name} since its size is {sizeof(TEnum)}. Must be 1, 2, 4, or 8.")
        where TEnum : unmanaged, Enum;


    /// <summary> The static container for the data is only initialized when used. </summary>
    private static class Values<T> where T : unmanaged, Enum
    {
#pragma warning disable
        public static readonly IReadOnlyList<T> Data = Enum.GetValues<T>();
#pragma warning restore
    }

    /// <summary> The static container for the data is only initialized when used. </summary>
    private static class NamesU16<T> where T : unmanaged, Enum
    {
#pragma warning disable
        public static readonly IReadOnlyList<string> Data = Enum.GetNames<T>();
#pragma warning restore
    }

    /// <summary> The static container for the data is only initialized when used. </summary>
    private static class NamesAndValuesU16<T> where T : unmanaged, Enum
    {
#pragma warning disable
        public static readonly IReadOnlyList<(string Name, T Value)> Data = NamesU16<T>.Data.Zip(Values<T>.Data).ToArray();
#pragma warning restore
    }

    /// <summary> The static container for the data is only initialized when used. </summary>
    private static class NamesU8<T> where T : unmanaged, Enum
    {
#pragma warning disable
        public static readonly IReadOnlyList<StringU8> Data = Enum.GetNames<T>().Select(v => new StringU8(v)).ToArray();
#pragma warning restore
    }

    /// <summary> The static container for the data is only initialized when used. </summary>
    private static class NamesAndValuesU8<T> where T : unmanaged, Enum
    {
#pragma warning disable
        public static readonly IReadOnlyList<(StringU8 Name, T Value)> Data = NamesU8<T>.Data.Zip(Values<T>.Data).ToArray();
#pragma warning restore
    }

    /// <summary> The static container for the data is only initialized when used. </summary>
    private static class LookupU16<T> where T : unmanaged, Enum
    {
#pragma warning disable
        public static readonly FrozenDictionary<string, T> Data = NamesAndValuesU16<T>.Data
            .ToFrozenDictionary(p => p.Name, p => p.Value, StringComparer.OrdinalIgnoreCase);

        public static readonly FrozenDictionary<string, T>.AlternateLookup<ReadOnlySpan<char>> SpanLookup =
            Data.GetAlternateLookup<ReadOnlySpan<char>>();
#pragma warning restore
    }

    /// <summary> The static container for the data is only initialized when used. </summary>
    private static class LookupU8<T> where T : unmanaged, Enum
    {
#pragma warning disable
        public static readonly FrozenDictionary<StringU8, T> Data = NamesAndValuesU8<T>.Data
            .ToFrozenDictionary(p => p.Name, p => p.Value, StringU8Comparer.OrdinalIgnoreAsciiCase);

        public static readonly FrozenDictionary<StringU8, T>.AlternateLookup<ReadOnlySpan<byte>> SpanLookup =
            Data.GetAlternateLookup<ReadOnlySpan<byte>>();
#pragma warning restore
    }

    /// <summary> The static container for the data is only initialized when used. </summary>
    private static class TextU16<T> where T : unmanaged, Enum
    {
#pragma warning disable
        public static readonly FrozenDictionary<T, string> Data = NamesAndValuesU16<T>.Data.DistinctBy(p => p.Value)
            .ToFrozenDictionary(p => p.Value, p => p.Name);
#pragma warning restore
    }

    /// <summary> The static container for the data is only initialized when used. </summary>
    private static class TextU8<T> where T : unmanaged, Enum
    {
#pragma warning disable
        public static readonly FrozenDictionary<T, StringU8> Data = NamesAndValuesU8<T>.Data.DistinctBy(p => p.Value)
            .ToFrozenDictionary(p => p.Value, p => p.Name);
#pragma warning restore
    }

    /// <summary> The static container for the data is only initialized when used. </summary>
    private static class Flags<T> where T : unmanaged, Enum
    {
#pragma warning disable
        public static readonly bool IsFlags = typeof(T).GetCustomAttributes(typeof(FlagsAttribute), false).Any();
        public static readonly T    All     = IsFlags ? Values<T>.Data.Or() : default;
#pragma warning restore
    }
}
