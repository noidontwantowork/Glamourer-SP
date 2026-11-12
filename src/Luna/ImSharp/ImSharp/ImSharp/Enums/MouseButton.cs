namespace ImSharp;

/// <summary> Identify mouse buttons.  </summary>
public enum MouseButton
{
    /// <summary> The left button.  </summary>
    Left = 0,

    /// <summary> The right button.  </summary>
    Right = 1,

    /// <summary> The middle button.  </summary>
    Middle = 2,

    /// <summary> The first thumb button. </summary>
    /// <remarks> Not used or named by ImGui proper. </remarks>
    Thumb1 = 3,

    /// <summary> The second thumb button. </summary>
    /// <remarks> Not used or named by ImGui proper. </remarks>
    Thumb2 = 4,

    /// <summary> The number of supported mouse buttons. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Count = 5,
}
