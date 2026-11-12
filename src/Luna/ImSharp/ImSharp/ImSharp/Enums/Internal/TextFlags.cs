namespace ImSharp.Internal;

/// <summary> Flags that govern the behavior of text, used by <seealso cref="Native.Methods.Internal"/>. </summary>
[Flags]
public enum TextFlags : uint
{
    /// <summary> No specific behavior. </summary>
    None = 0,

    /// <summary> Unknown. </summary>
    NoWidthForLargeClippedText = 1 << 0,
}
