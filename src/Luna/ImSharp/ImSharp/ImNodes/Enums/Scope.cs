namespace ImSharp.ImNodes;

public static partial class Internal
{
    /// <summary> The current scope of the editor. </summary>
    [Flags]
    public enum Scope : uint
    {
        /// <summary> No ImNodes scope is active. </summary>
        None = 1 << 0,

        /// <summary> An editor scope was begun. </summary>
        Editor = 1 << 1,

        /// <summary> A node scope was begun. </summary>
        Node = 1 << 2,

        /// <summary> An attribute scope was begun. </summary>
        Attribute = 1 << 3,
    }

    extension(Scope scope)
    {
        /// <summary> Check that we are in the requested scope and return a reference to the ImNodes <see cref="NodesContext"/>. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe ref NodesContext Check()
        {
            ref var context = ref *ImNodes.Context;
            Debug.Assert(context.CurrentScope.HasFlag(scope));
            return ref context;
        }

        /// <summary> Check that we are in the requested scope and return a reference to the ImNodes <see cref="NodesContext"/>. </summary>
        /// <paramref name="newScope"> The scope to open. </paramref>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public unsafe ref NodesContext Check(Scope newScope)
        {
            ref var context = ref *ImNodes.Context;
            Debug.Assert(context.CurrentScope.HasFlag(scope));
            context.CurrentScope = newScope;
            return ref context;
        }
    }
}
