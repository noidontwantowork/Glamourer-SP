namespace ImSharp;

/// <summary> Helper methods for handling null-termination correctly. </summary>
public static unsafe class NullTerminationHelpers
{
    /// <summary> Get an unowned UTF8 string from a null-terminated pointer to UTF8 text. </summary>
    /// <param name="text"> The pointer to the UTF8 text. HAS to be null-terminated. </param>
    /// <returns> A span of the UTF8 text up to but not including the null-terminator. </returns>
    /// <remarks> If <paramref name="text"/> is null or an immediate null-terminator, the returned span still is followed by a null-terminator. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static ReadOnlySpan<byte> GetSpan(byte* text)
    {
        var span = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(text);
        // Make sure the empty span is still null-terminated.
        if (span.IsEmpty)
            return StringU8.Empty;

        return span;
    }

    /// <summary> Get a UTF16 string from a null-terminated pointer to UTF8 text. </summary>
    /// <param name="text"> The pointer to the UTF8 text. HAS to be null-terminated. </param>
    /// <returns> The UTF16 string. </returns>
    /// <remarks> If <paramref name="text"/> is null, an empty string is returned. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static string GetString(byte* text)
    {
        var span = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(text);
        return Encoding.UTF8.GetString(span);
    }

    /// <summary> Get an owned UTF8 string from a null-terminated pointer to UTF8 text. </summary>
    /// <param name="text"> The pointer to the UTF8 text. HAS to be null-terminated. </param>
    /// <returns> The owned UTF8 string. </returns>
    /// <remarks> If <paramref name="text"/> is null, an empty string is returned. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static StringU8 GetClone(byte* text)
    {
        var span = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(text);
        return span.CloneNullTerminated();
    }
}
