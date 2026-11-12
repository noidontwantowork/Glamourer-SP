namespace ImSharp;

/// <summary> Colors for window docking. </summary>
public enum WindowDockStyleColor
{
    /// <summary> Text color. </summary>
    Text = 0,

    /// <summary> Docking tab color when neither hovered nor active. </summary>
    Tab = 1,

    /// <summary> Docking tab color when hovered. </summary>
    TabHovered = 2,

    /// <summary> Docking tab color when active. </summary>
    TabActive = 3,

    /// <summary> Docking tab color when unfocused. </summary>
    TabUnfocused = 4,

    /// <summary> Docking tab color when unfocused but active. </summary>
    TabUnfocusedActive = 5,

    /// <summary> The number of different <seealso cref="WindowDockStyleColor"/> </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Count,
}
