namespace ImSharp.ImNodes;

public static partial class Internal
{
    /// <summary> Descriptor in what way a link is being created. </summary>
    public enum LinkCreationType : uint
    {
        /// <summary> A new link is being created. </summary>
        Standard,

        /// <summary> An existing link has been detached from one pin. </summary>
        FromDetach,
    }
}
