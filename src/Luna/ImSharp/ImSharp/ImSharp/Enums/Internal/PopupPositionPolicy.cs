namespace ImSharp.Internal;

/// <summary> Internally used policies for popup handling. </summary>
public enum PopupPositionPolicy
{
    /// <summary> Default policy is popping up where the window is set to be positioned. </summary>
    Default = 0,

    /// <summary> Pop up below the combo box preview. </summary>
    ComboBox = 1,

    /// <summary> Pop up at the cursor. </summary>
    Tooltip = 2,
}
