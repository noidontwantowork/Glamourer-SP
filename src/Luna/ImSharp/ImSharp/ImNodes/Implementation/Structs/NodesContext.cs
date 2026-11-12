namespace ImSharp.ImNodes;

using ImSharp;

public static partial class Internal
{
    public unsafe struct NodesContext : IDisposable
    {
        public EditorContext* DefaultEditorContext;
        public EditorContext* EditorContext;

        public Im.DrawList              CanvasDrawList;
        public Im.Native.Storage        NodeToSubmissionIndexData;
        public ImVector<NodeIndex>      NodeIndexSubmissionOrder;
        public ImVector<NodeIndex>      NodeIndicesOverlappingWithMouse;
        public ImVector<AttributeIndex> OccludedPinIndices;

        public Vector2   CanvasOriginScreenSpace;
        public Rectangle CanvasRectangleScreenSpace;

        public Scope CurrentScope;

        public Io                       Io;
        public Style                    Style;
        public ImVector<ColorStackData> ColorModifierStack;
        public ImVector<StyleStackData> StyleModifierStack;
        public Im.Native.TextBuffer     TextBuffer;

        public AttributeFlags CurrentAttributeFlags;

        public ImVector<AttributeFlags> AttributeFlagStack;

        public NodeIndex      CurrentNodeIndex;
        public AttributeIndex CurrentPinIndex;
        public AttributeId    CurrentAttributeId;

        public NodeIndex      HoveredNodeIndex;
        public LinkIndex      HoveredLinkIndex;
        public AttributeIndex HoveredPinIndex;

        public LinkIndex DeletedLinkIndex;
        public LinkIndex SnapLinkIndex;

        public UiState ImNodesUiState;

        public AttributeId ActiveAttributeId;
        public bool        ActiveAttribute;

        public Vector2 MousePosition;
        public bool    LeftMouseClicked;
        public bool    LeftMouseReleased;
        public bool    AltMouseClicked;
        public bool    LeftMouseDragging;
        public bool    AltMouseDragging;
        public float   AltMouseScrollDelta;
        public bool    MultipleSelectModifier;

        public readonly Im.StateStorage NodeToSubmissionIndex
            => (Im.Native.Storage*)Unsafe.AsPointer(in NodeToSubmissionIndexData);

        public void Dispose()
        {
            ImNodes.Api.EditorContextFree(DefaultEditorContext);
            NodeToSubmissionIndexData.Data.Free<TrivialTypeInformation<Im.Native.StoragePair>>();
            NodeIndexSubmissionOrder.Free<NodeIndex>();
            NodeIndicesOverlappingWithMouse.Free<NodeIndex>();
            OccludedPinIndices.Free<AttributeIndex>();
            ColorModifierStack.Free<ColorStackData>();
            StyleModifierStack.Free<StyleStackData>();
            TextBuffer.Buffer.Free<TrivialTypeInformation<byte>>();
            AttributeFlagStack.Free<TrivialTypeInformation<AttributeFlags>>();
        }

        public void Initialize()
        {
            CanvasOriginScreenSpace    = Vector2.Zero;
            CanvasRectangleScreenSpace = Rectangle.Zero;
            CurrentScope               = Scope.None;
            CurrentPinIndex            = AttributeIndex.Invalid;
            CurrentNodeIndex           = NodeIndex.Invalid;
            DefaultEditorContext       = ImNodes.Api.EditorContextCreate();
            EditorContext              = DefaultEditorContext;
            CurrentAttributeFlags      = AttributeFlags.None;
            Style                      = new Style();
            Io                         = new Io();
            AttributeFlagStack.Add<TrivialTypeInformation<AttributeFlags>>(AttributeFlags.None);
            ImNodes.Api.StyleColorsDark((Style*)Unsafe.AsPointer(ref Style));
        }
    }
}
