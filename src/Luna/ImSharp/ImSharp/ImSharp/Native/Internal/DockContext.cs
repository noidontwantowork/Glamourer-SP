namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Internal
        {
            public struct DockContext
            {
                public Storage        Nodes;
                public ImVector<nint> Requests;
                public ImVector<nint> NodesSettings;
                public ImBool         WantFullRebuild;
            }
        }
    }
}
