namespace ImSharp.ImNodes;

using ImSharp;

public static partial class Internal
{
    public struct NodeData : IIdObject<NodeData>, IDisposable
    {
        public NodeId                   Id;
        public Vector2                  Origin;
        public Rectangle                TitleBarContent;
        public Rectangle                Rectangle;
        public ColorStyle               Colors;
        public LayoutStyle              Layout;
        public ImVector<AttributeIndex> PinIndices;
        public bool                     Draggable;

        public void SaveStyle(ImNodes.ImNodesStyle style)
        {
            Colors.Background         = style[ImNodesColor.NodeBackground];
            Colors.BackgroundHovered  = style[ImNodesColor.NodeBackgroundHovered];
            Colors.BackgroundSelected = style[ImNodesColor.NodeBackgroundSelected];
            Colors.Outline            = style[ImNodesColor.NodeOutline];
            Colors.TitleBar           = style[ImNodesColor.TitleBar];
            Colors.TitleBarHovered    = style[ImNodesColor.TitleBarHovered];
            Colors.TitleBarSelected   = style[ImNodesColor.TitleBarSelected];
            Layout.CornerRounding     = style.NodeCornerRounding;
            Layout.Padding            = style.NodePadding;
            Layout.BorderThickness    = style.NodeBorderThickness;
        }

        public struct ColorStyle
        {
            public Rgba32 Background;
            public Rgba32 BackgroundHovered;
            public Rgba32 BackgroundSelected;
            public Rgba32 Outline;
            public Rgba32 TitleBar;
            public Rgba32 TitleBarHovered;
            public Rgba32 TitleBarSelected;
        }

        public struct LayoutStyle
        {
            public Vector2 Padding;
            public float   CornerRounding;
            public float   BorderThickness;
        }

        ImGuiId IIdObject<NodeData>.Id
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Id.Id;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Id = value.Id;
        }

        public readonly Vector2 ContentOrigin
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Origin + new Vector2(0, TitleBarContent.Height + 2 * Layout.Padding.Y) + Layout.Padding;
        }

        public readonly Vector2 TitleBarOrigin
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Origin + Layout.Padding;
        }

        public readonly Rectangle TitleRectangle
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                var rect = TitleBarContent.Expand(Layout.Padding);
                return new Rectangle(rect.Minimum, rect.Minimum + new Vector2(Rectangle.Width, rect.Height));
            }
        }

        public static unsafe void PostCreation(int index)
            => ImNodes.Editor->NodeDepthOrder.Add<NodeIndex>(index);

        public static unsafe void UpdatePool(ref IdObjectPool<NodeData> pool)
        {
            for (var i = 0; i < pool.FullCount; ++i)
            {
                if (pool.Used(i))
                {
                    pool.GetWrite(i).PinIndices.Clear<AttributeIndex>();
                }
                else
                {
                    var id = pool.GetWrite(i);
                    if (pool.FindIndex(id.Id) != i)
                        continue;

                    // Remove node idx form depth stack the first time we detect that this idx slot is unused
                    ref var depthStack = ref ImNodes.Editor->NodeDepthOrder;
                    var     index      = depthStack.FindIndex(i);
                    depthStack.RemoveAt<NodeIndex>(index);

                    pool.IdMap.SetInt(id.Id, IIndex.InvalidIndex);
                    pool.FreeList.Add<TrivialTypeInformation<int>>(i);
                    Destroy(pool.Pool.Data + i);
                }
            }
        }

        public static unsafe void Create(NodeData* pointer, ImGuiId id)
            => pointer->Id = id.Id;

        public static bool TriviallyMovable
            => true;

        public static bool TriviallyDestructible
            => false;

        public static bool TriviallyConstructible
            => true;

        public static unsafe void Destroy(NodeData* pointer)
        {
            pointer->Id = ImGuiId.Invalid;
            pointer->PinIndices.Free<AttributeIndex>();
        }

        public static unsafe ref NodeData PlacementNew(void* address)
            => ref TrivialTypeInformation<NodeData>.PlacementNew(address);

        public void Dispose()
            => PinIndices.Free<AttributeIndex>();
    }
}
