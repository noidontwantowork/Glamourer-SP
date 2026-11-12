namespace ImSharp.ImNodes;

using ImSharp;

public static partial class Internal
{
    public struct ClickInteractionState
    {
        public ClickInteractionType Type;
        public LinkCreationData     LinkCreation;
        public Rectangle            BoxSelector;

        public struct LinkCreationData
        {
            public AttributeIndex   StartPinIndex;
            public AttributeIndex   EndPinIndex;
            public LinkCreationType Type;
        }
    }
}
