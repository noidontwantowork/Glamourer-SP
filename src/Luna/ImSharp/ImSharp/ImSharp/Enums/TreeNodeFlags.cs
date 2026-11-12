namespace ImSharp;

/// <summary> Flags controlling the behaviour of tree nodes. </summary>
[Flags]
public enum TreeNodeFlags : uint
{
    /// <summary> No specific behaviour. </summary>
    None = 0,

    /// <summary> The tree node is currently selected. </summary>
    Selected = 1 << 0,

    /// <summary> The tree node should be framed with a background like a button. </summary>
    Framed = 1 << 1,

    /// <summary> Subsequent items are allowed to overlap this for hit testing. </summary>
    AllowOverlap = 1 << 2,

    /// <summary> When this tree node is opened, no ID and indentation are pushed on the stack. </summary>
    NoTreePushOnOpen = 1 << 3,

    /// <summary> Do not automatically open this node when Logging is active, which would happen by default. </summary>
    NoAutoOpenOnLog = 1 << 4,

    /// <summary> This tree should be open by default. </summary>
    DefaultOpen = 1 << 5,

    /// <summary> Only open on double clicks instead of single clicks. </summary>
    OpenOnDoubleClick = 1 << 6,

    /// <summary> Only open when clicking the arrow part. </summary>
    OpenOnArrow = 1 << 7,

    /// <summary> The node can not be collapsed and does not display an arrow. </summary>
    Leaf = 1 << 8,

    /// <summary> Display a bullet instead of an arrow. The node can still be opened and closed. </summary>
    Bullet = 1 << 9,

    /// <summary> Pad the node text to the frame padding. Equivalent to aligning the text manually before the node. </summary>
    FramePadding = 1 << 10,

    /// <summary> Extend the hit box of this node to the right-most edge, even if it is not framed.  </summary>
    SpanAvailWidth = 1 << 11,

    /// <summary> Extend the hit box to the left-most and right-most edges. </summary>
    SpanFullWidth = 1 << 12,

    /// <summary> Going left may move to this node from all items submitted before it is popped. </summary>
    NavLeftJumpsBackHere = 1 << 13,

    /// <summary> The default flags for a collapsing header. </summary>
    CollapsingHeader = NoAutoOpenOnLog | NoTreePushOnOpen | Framed,

    /// <summary> Instruct the tree node to clip the label for a trailing button. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    ClipLabelForTrailingButton = 1 << 20,
}
