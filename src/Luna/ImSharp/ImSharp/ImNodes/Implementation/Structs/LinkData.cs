namespace ImSharp.ImNodes;

using ImSharp;

public static partial class Internal
{
    public struct LinkData : IIdObject<LinkData>, ITrivialTypeInformation<LinkData>
    {
        public LinkId         Id;
        public AttributeIndex StartPinIndex;
        public AttributeIndex EndPinIndex;
        public ColorStyle     Colors;

        public struct ColorStyle
        {
            public Rgba32 Base;
            public Rgba32 Hovered;
            public Rgba32 Selected;
        }

        ImGuiId IIdObject<LinkData>.Id
        {
            get => Id;
            set => Id = value;
        }

        public void SaveStyle(ImNodes.ImNodesStyle style)
        {
            Colors.Base     = style[ImNodesColor.Link];
            Colors.Hovered  = style[ImNodesColor.LinkHovered];
            Colors.Selected = style[ImNodesColor.LinkSelected];
        }

        public static void PostCreation(int index)
        { }

        public static void UpdatePool(ref IdObjectPool<LinkData> pool)
            => IdObjectPool<LinkData>.DefaultUpdate(ref pool);

        public static unsafe void Create(LinkData* pointer, ImGuiId id)
            => pointer->Id = id.Id;
    }
}
