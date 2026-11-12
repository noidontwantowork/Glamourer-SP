namespace ImSharp.Internal;

/// <summary> Types of context hooks for different steps in the rendering pipeline. </summary>
public enum ContextHookType
{
    /// <summary> Before the start of a new frame. </summary>
    NewFramePre = 0,

    /// <summary> After the start of a new frame. </summary>
    NewFramePost = 1,

    /// <summary> Before the end of a frame. </summary>
    EndFramePre = 2,

    /// <summary> After the end of a frame. </summary>
    EndFramePost = 3,

    /// <summary> Before the rendering step. </summary>
    RenderPre = 4,

    /// <summary> After the rendering step. </summary>
    RenderPost = 5,

    /// <summary> On app shutdown. </summary>
    Shutdown = 6,

    /// <summary> When the context hook is pending removal. </summary>
    PendingRemoval = 7,
}
