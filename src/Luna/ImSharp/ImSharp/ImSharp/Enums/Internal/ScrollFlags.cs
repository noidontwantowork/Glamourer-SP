namespace ImSharp.Internal;

/// <summary> Flags that control scrolling behavior. </summary>
[Flags]
public enum ScrollFlags : uint
{
    /// <summary> No specific behavior. </summary>
    None = 0,

    /// <summary> Keep the object visible on the X-axis edge. </summary>
    KeepVisibleEdgeX = 1 << 0,

    /// <summary> Keep the object visible on the Y-axis edge. </summary>
    KeepVisibleEdgeY = 1 << 1,

    /// <summary> Keep the object visible in the X-axis center. </summary>
    KeepVisibleCenterX = 1 << 2,

    /// <summary> Keep the object visible in the Y-axis center. </summary>
    KeepVisibleCenterY = 1 << 3,

    /// <summary> Keep the object always in the X-axis center. </summary>
    AlwaysCenterX = 1 << 4,

    /// <summary> Keep the object always in the Y-axis center. </summary>
    AlwaysCenterY = 1 << 5,

    /// <summary> Do not scroll the parent window. </summary>
    NoScrollParent = 1 << 6,

    /// <summary> Mask for the X-axis behavior. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    MaskX = KeepVisibleEdgeX | KeepVisibleCenterX | AlwaysCenterX,

    /// <summary> Mask for the Y-axis behavior. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    MaskY = KeepVisibleEdgeY | KeepVisibleCenterY | AlwaysCenterY,
}
