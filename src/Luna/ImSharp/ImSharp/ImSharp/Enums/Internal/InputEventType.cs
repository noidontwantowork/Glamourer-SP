namespace ImSharp.Internal;

/// <summary> Different types of input events. </summary>
public enum InputEventType
{
    /// <summary> No event. </summary>
    None = 0,

    /// <summary> The mouse cursor position changed. </summary>
    MousePosition = 1,

    /// <summary> The mouse wheel was rolled. </summary>
    MouseWheel = 2,

    /// <summary> A mouse button was pressed. </summary>
    MouseButton = 3,

    /// <summary> The mouse changed viewport boundaries. </summary>
    MouseViewport = 4,

    /// <summary> A key was pressed. </summary>
    Key = 5,

    /// <summary> A text was input. </summary>
    Text = 6,

    /// <summary> The window focus changed. </summary>
    Focus = 7,
}
