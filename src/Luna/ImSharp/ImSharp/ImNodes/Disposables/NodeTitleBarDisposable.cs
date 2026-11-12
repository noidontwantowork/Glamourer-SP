namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    /// <summary> A wrapper around a ImNodes node title bars. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ref struct NodeTitleBarDisposable : IDisposable
    {
        /// <summary> Whether the node title bar is already ended. </summary>
        public bool Alive { get; private set; }

        /// <summary> Create a title bar for the current node. </summary>
        /// <returns> A disposable object that ends the node title bar on disposal. Use with using. </returns>
        /// <remarks> This has to be called before adding any attributes to the node. </remarks>
        internal NodeTitleBarDisposable(bool _)
        {
            Api.BeginNodeTitleBar();
            Alive = true;
        }

        /// <summary> End the node title bar on leaving scope. </summary>
        public void Dispose()
        {
            if (!Alive)
                return;

            Api.EndNodeTitleBar();
            Alive = false;
        }
    }
}
