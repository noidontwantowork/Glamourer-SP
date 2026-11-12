namespace ImSharp.ImNodes;

public static partial class Internal
{
    /// <summary> The different types of attributes and pins. </summary>
    public enum AttributeType : uint
    {
        /// <summary> Unknown attribute type. </summary>
        None,

        /// <summary> Input Pins are rendered on the left side of the node. </summary>
        Input,

        /// <summary> Output pins are rendered on the right side of the node. </summary>
        Output,

        /// <summary> Static attributes have no pin and can not be linked, but can be checked for activity. </summary>
        Static,
    }
}
