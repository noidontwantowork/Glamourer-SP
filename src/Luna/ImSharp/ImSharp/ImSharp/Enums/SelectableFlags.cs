namespace ImSharp;

/// <summary> Flags controlling the behaviour of a selectable. </summary>
[Flags]
public enum SelectableFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> Clicking this does not close the parent popup window. </summary>
    NoAutoClosePopups = 1 << 0,

    /// <summary> The selectable frame will span all columns of its containing table. </summary>
    /// <remarks> The text will still be clipped in the current column. </remarks>
    SpanAllColumns = 1 << 1,

    /// <summary> Generate press events on double clicks. </summary>
    AllowDoubleClick = 1 << 2,

    /// <summary> Can not be selected and the text is greyed out. </summary>
    Disabled = 1 << 3,

    /// <summary> Hit testing allows subsequent items to overlap this one. </summary>
    AllowOverlap = 1 << 4,

    /// <summary> Do not hold an active ID.  </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoHoldingActiveId = 1 << 20,

    /// <summary> Auto-select this selectable when moved into via navigation.  </summary>
    /// <remarks> WIP. </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    SelectOnNav = 1 << 21,

    /// <summary> Override the button behavior to react on button down instead of click and release. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    SelectOnClick = 1 << 22,

    /// <summary> Override the button behavior to react on button release instead of click and release. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    SelectOnRelease = 1 << 23,

    /// <summary> Span all available width even if declared less.  </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    SpanAvailableWidth = 1 << 24,

    /// <summary> Always show as active when the mouse is held down, even if not hovered anymore.  </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    DrawHoveredWhenHeld = 1 << 25,

    /// <summary> Set the navigation / focus ID on mouse hover. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    SetNavIdOnHover = 1 << 26,

    /// <summary> Disable padding each side of the selectable with half <seealso cref="Native.ImGuiStyle.ItemSpacing"/>. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    NoPadWithHalfSpacing = 1 << 27,
}
