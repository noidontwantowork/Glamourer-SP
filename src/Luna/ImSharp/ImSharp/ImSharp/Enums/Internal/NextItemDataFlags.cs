namespace ImSharp.Internal;

/// <summary> Flags that describe settings to be applied to the next window. </summary>
[Flags]
public enum NextItemDataFlags : uint
{
    /// <summary> Nothing is to be applied. </summary>
    None = 0,

    /// <summary> A specific width is to be applied. </summary>
    HasWidth = 1 << 0,

    /// <summary> The open state of the item is to be changed. </summary>
    HasOpen = 1 << 1,
}
