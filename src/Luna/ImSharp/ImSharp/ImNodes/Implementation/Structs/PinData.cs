namespace ImSharp.ImNodes;

using ImSharp;

public static partial class Internal
{
    public struct PinData : IIdObject<PinData>, ITrivialTypeInformation<PinData>
    {
        public AttributeId    Id;
        public NodeIndex      ParentNodeIndex;
        public Rectangle      AttributeRectangle;
        public AttributeType  Type;
        public PinShape       Shape;
        public Vector2        Position;
        public AttributeFlags Flags;
        public ColorStyle     Colors;

        public struct ColorStyle
        {
            public Rgba32 Background;
            public Rgba32 Hovered;
        }

        ImGuiId IIdObject<PinData>.Id
        {
            get => Id.Id;
            set => Id = value.Id;
        }

        public static void PostCreation(int index)
        { }

        public static unsafe void UpdatePool(ref IdObjectPool<PinData> pool)
            => IdObjectPool<PinData>.DefaultUpdate(ref pool);

        public static unsafe void Create(PinData* pointer, ImGuiId id)
        {
            pointer->Id    = id.Id;
            pointer->Shape = PinShape.CircleFilled;
        }

        public static unsafe void Destroy(PinData* pointer)
            => pointer->Id = AttributeId.Invalid;
    }
}
