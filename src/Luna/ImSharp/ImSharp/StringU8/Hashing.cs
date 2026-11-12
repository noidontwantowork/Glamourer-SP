using System.IO.Hashing;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace ImSharp;

/// <summary> Utility for hashing spans. </summary>
public static class Hashing
{
    /// <summary> Compute a (ASCII) case-insensitive hash of a byte span of data. </summary>
    /// <param name="data"> The data to hash. </param>
    /// <returns> A hash of the given data. </returns>
    /// <remarks> This will use SIMD optimization if available. If you know your data is too short or that Avx2 is unsupported, use <see cref="HashAsciiCaseInsensitiveNaive"/> instead. </remarks>
    public static int HashAsciiCaseInsensitive(ReadOnlySpan<byte> data)
        => HashAsciiCaseInsensitiveSimd(data);

    /// <summary> Compute a (ASCII) case-insensitive hash of a byte span of data. </summary>
    /// <param name="data"> The data to hash. </param>
    /// <returns> A hash of the given data. </returns>
    /// <remarks> Prefer <see cref="HashAsciiCaseInsensitive"/> unless you know Avx2 is not supported or the strings are too short anyway. </remarks>
    public static int HashAsciiCaseInsensitiveNaive(ReadOnlySpan<byte> data)
    {
        var hasher = new XxHash32();
        foreach (var b in data)
        {
            var c = (uint)(b - 'A') <= 'Z' - 'A'
                ? (byte)(b | 0x20)
                : b;

            hasher.Append(new ReadOnlySpan<byte>(ref c));
        }

        return (int)hasher.GetCurrentHashAsUInt32();
    }

    /// <summary> Compute a hash of a byte span of data. </summary>
    /// <param name="data"> The data to hash. </param>
    /// <returns> A hash of the given data. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static int HashCaseSensitive(ReadOnlySpan<byte> data)
        => (int)XxHash32.HashToUInt32(data);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static unsafe int HashAsciiCaseInsensitiveSimd(ReadOnlySpan<byte> data)
    {
        if (!Avx2.IsSupported)
            return HashAsciiCaseInsensitiveNaive(data);

        const int blockSize = 32;
        var       hasher    = new XxHash32();
        var       i         = 0;
        fixed (byte* ptr = data)
        {
            while (i <= data.Length - blockSize)
            {
                var chunk   = Avx.LoadVector256((sbyte*)ptr + i);
                var mask    = Avx2.And(Avx2.CompareGreaterThan(chunk, SimdData.MaskA), Avx2.CompareGreaterThan(SimdData.MaskZ, chunk));
                var lowered = Avx2.Or(chunk, Avx2.And(mask, SimdData.Bit32));
                hasher.Append(new ReadOnlySpan<byte>(&lowered, blockSize));
                i += blockSize;
            }
        }

        // Tail end bytes.
        foreach (var b in data[i..])
        {
            var c = (uint)(b - 'A') <= 'Z' - 'A'
                ? (byte)(b | 0x20)
                : b;

            hasher.Append(new ReadOnlySpan<byte>(ref c));
        }

        return (int)hasher.GetCurrentHashAsUInt32();
    }

    private static class SimdData
    {
        public static readonly Vector256<sbyte> MaskA = Vector256.Create((sbyte)((byte)'A' - 1));
        public static readonly Vector256<sbyte> MaskZ = Vector256.Create((sbyte)((byte)'Z' + 1));
        public static readonly Vector256<sbyte> Bit32 = Vector256.Create((sbyte)0x20);
    }
}
