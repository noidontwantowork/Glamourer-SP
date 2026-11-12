namespace ImSharp.ImNodes;

public static unsafe partial class Internal
{
    public static void Select<TObject, TIndex>(in IdObjectPool<TObject> objects, ref ImVector<TIndex> selectedIndices, ImGuiId id)
        where TObject : unmanaged, IIdObject<TObject>
        where TIndex : unmanaged, IIndex<TIndex>
    {
        var index = TIndex.FromInt(objects.FindIndex(id));
        Debug.Assert(index.IsValid);
        Debug.Assert(!selectedIndices.Contains(index));
        selectedIndices.Add<TIndex>(index);
    }

    public static void ClearObjectSelection<TObject, TIndex>(in IdObjectPool<TObject> objects, ref ImVector<TIndex> selectedIndices, ImGuiId id)
        where TObject : unmanaged, IIdObject<TObject>
        where TIndex : unmanaged, IIndex<TIndex>
    {
        var objectIndex = TIndex.FromInt(objects.FindIndex(id));
        Debug.Assert(objectIndex.IsValid);
        var selectedIndex = selectedIndices.FindIndex(objectIndex);
        Debug.Assert(selectedIndex is not IIndex.InvalidIndex);
        selectedIndices.RemoveAt<TIndex>(selectedIndex);
    }

    public static bool IsObjectSelected<TObject, TIndex>(in IdObjectPool<TObject> objects, in ImVector<TIndex> selectedIndices, ImGuiId id)
        where TObject : unmanaged, IIdObject<TObject>
        where TIndex : unmanaged, IIndex<TIndex>
    {
        var objectIndex = TIndex.FromInt(objects.FindIndex(id));
        return selectedIndices.FindIndex(objectIndex) is not IIndex.InvalidIndex;
    }

    public static void DrawGrid(ref EditorContext editor, Vector2 canvasSize)
    {
        var offset           = editor.Panning;
        var lineColor        = ImNodes.Style[ImNodesColor.GridLine];
        var primaryLineColor = ImNodes.Style[ImNodesColor.GridLinePrimary];
        var drawPrimary      = ImNodes.Style.Flags.HasFlag(ImNodesStyleFlags.GridLinesPrimary);
        var spacing          = ImNodes.Style.GridSpacing;

        var drawList = ImNodes.Context->CanvasDrawList.Shape;
        for (var x = offset.X % spacing; x < canvasSize.X; x += spacing)
        {
            drawList.Line(editor.EditorToScreen(new Vector2(x, 0)), editor.EditorToScreen(canvasSize with { X = x }),
                drawPrimary && offset.X - x is 0 ? primaryLineColor : lineColor);
        }

        for (var y = offset.Y % spacing; y < canvasSize.Y; y += spacing)
        {
            drawList.Line(editor.EditorToScreen(new Vector2(0, y)), editor.EditorToScreen(canvasSize with { Y = y }),
                drawPrimary && offset.Y - y is 0 ? primaryLineColor : lineColor);
        }
    }

    public readonly record struct QuadOffsets(Vector2 TopLeft, Vector2 BottomLeft, Vector2 BottomRight, Vector2 TopRight)
    {
        public static QuadOffsets Calculate(float sideLength)
        {
            var pHalf = 0.5f * sideLength;
            var nHalf = -pHalf;
            return new QuadOffsets(new Vector2(nHalf, pHalf), new Vector2(nHalf, nHalf), new Vector2(pHalf, nHalf), new Vector2(pHalf, pHalf));
        }
    }

    public readonly record struct TriOffsets(Vector2 TopLeft, Vector2 BottomLeft, Vector2 Right)
    {
        private const float Sqrt3 = 1.7320508075688772935274463415058723669428052538103806280558069794f;

        public static TriOffsets Calculate(float sideLength)
        {
            var left     = -1f / 6f * Sqrt3 * sideLength;
            var right    = -2 * left;
            var vertical = 0.5f * sideLength;
            return new TriOffsets(new Vector2(left, vertical), new Vector2(left, -vertical), new Vector2(right, 0));
        }
    }

    public static void DrawPinShape(Vector2 position, in PinData pin, Rgba32 color)
    {
        const int circleSegments = 8;
        var       drawList       = ImNodes.Context->CanvasDrawList.Shape;
        switch (pin.Shape)
        {
            case PinShape.Circle:
                drawList.Circle(position, ImNodes.Style.PinCircleRadius, color, ImNodes.Style.PinLineThickness, circleSegments);
                break;
            case PinShape.CircleFilled: drawList.CircleFilled(position, ImNodes.Style.PinCircleRadius, color, circleSegments); break;
            case PinShape.Square:
            {
                var offsets = QuadOffsets.Calculate(ImNodes.Style.PinQuadSideLength);
                drawList.Quad(position + offsets.TopLeft, position + offsets.BottomLeft, position + offsets.BottomRight,
                    position + offsets.TopRight, color, ImNodes.Style.PinLineThickness);
                break;
            }
            case PinShape.SquareFilled:
            {
                var offsets = QuadOffsets.Calculate(ImNodes.Style.PinQuadSideLength);
                drawList.QuadFilled(position + offsets.TopLeft, position + offsets.BottomLeft, position + offsets.BottomRight,
                    position + offsets.TopRight, color);
                break;
            }
            case PinShape.Triangle:
            {
                var offsets = TriOffsets.Calculate(ImNodes.Style.PinQuadSideLength);
                // NOTE: for some weird reason, the line drawn by AddTriangle is
                // much thinner than the lines drawn by AddCircle or AddQuad.
                // Multiplying the line thickness by two seemed to solve the
                // problem at a few different thickness values.
                drawList.Triangle(position + offsets.TopLeft, position + offsets.BottomLeft, position + offsets.Right, color,
                    2 * ImNodes.Style.PinLineThickness);
                break;
            }
            case PinShape.TriangleFilled:
            {
                var offsets = TriOffsets.Calculate(ImNodes.Style.PinQuadSideLength);
                drawList.TriangleFilled(position + offsets.TopLeft, position + offsets.BottomLeft, position + offsets.Right, color);
                break;
            }
            default: throw new InvalidEnumArgumentException($"{nameof(pin)}.{nameof(PinData.Shape)}", (int)pin.Shape, typeof(PinShape));
        }
    }

    public static void DrawPin(ref EditorContext editor, AttributeIndex pinIndex)
    {
        ref var          pin        = ref editor.Pins.GetWrite(pinIndex);
        ref readonly var parentRect = ref editor.Nodes.Get(pin.ParentNodeIndex).Rectangle;

        pin.Position = GetScreenSpacePinCoordinates(parentRect, pin.AttributeRectangle, pin.Type);
        var color = ImNodes.Context->HoveredPinIndex == pinIndex ? pin.Colors.Hovered : pin.Colors.Background;
        DrawPinShape(pin.Position, pin, color);
    }

    public static void DrawNode(ref EditorContext editor, NodeIndex nodeIndex)
    {
        ref readonly var node = ref editor.Nodes.Get(nodeIndex);
        Im.Cursor.Position = node.Origin + editor.Panning;
        var hovered = ImNodes.Context->HoveredNodeIndex == nodeIndex && editor.ClickInteraction.Type is not ClickInteractionType.BoxSelection;
        var (nodeBackground, titleBackground) = editor.SelectedNodeIndices.Contains(nodeIndex)
            ? (node.Colors.BackgroundSelected, node.Colors.TitleBarSelected)
            : hovered
                ? (node.Colors.BackgroundHovered, node.Colors.TitleBarHovered)
                : (node.Colors.Background, node.Colors.TitleBar);

        var drawList = ImNodes.Context->CanvasDrawList.Shape;

        // node base
        drawList.RectangleFilled(node.Rectangle, nodeBackground, node.Layout.CornerRounding);
        // title bar
        if (node.TitleBarContent.Height > 0)
            drawList.RectangleFilled(node.TitleRectangle, titleBackground, node.Layout.CornerRounding, ImDrawFlagsRectangle.RoundCornersTop);

        if (ImNodes.Style.Flags.HasFlag(ImNodesStyleFlags.NodeOutline))
            drawList.Rectangle(node.Rectangle, node.Colors.Outline, node.Layout.CornerRounding, ImDrawFlagsRectangle.RoundCornersAll,
                node.Layout.BorderThickness);

        foreach (var pin in node.PinIndices)
            DrawPin(ref editor, pin);
    }

    public static void DrawLink(ref EditorContext editor, LinkIndex linkIndex)
    {
        // It's possible for a link to be deleted in begin_link_interaction. A user
        // may detach a link, resulting in the link wire snapping to the mouse
        // position.
        // In other words, skip rendering the link if it was deleted.
        if (ImNodes.Context->DeletedLinkIndex == linkIndex)
            return;

        ref readonly var link     = ref editor.Links.Get(linkIndex);
        ref readonly var startPin = ref editor.Pins.Get(link.StartPinIndex);
        ref readonly var endPin   = ref editor.Pins.Get(link.EndPinIndex);

        var bezier = CubicBezier.Generate(startPin.Position, endPin.Position, ImNodes.Style.LinkLineSegmentsPerLength,
            startPin.Type is AttributeType.Input);
        var hovered = ImNodes.Context->HoveredLinkIndex == linkIndex && editor.ClickInteraction.Type is not ClickInteractionType.BoxSelection;

        var color = editor.SelectedLinkIndices.Contains(linkIndex) ? link.Colors.Selected : hovered ? link.Colors.Hovered : link.Colors.Base;
        ImNodes.Context->CanvasDrawList.Shape.BezierCubic(bezier.Point0, bezier.Point1, bezier.Point2, bezier.Point3, color,
            ImNodes.Style.LinkThickness, bezier.NumSegments);
    }

    public static void BeginPinAttribute(AttributeId id, AttributeType type, PinShape shape, NodeIndex nodeIndex)
    {
        ref var context = ref Scope.Node.Check(Scope.Node | Scope.Attribute);
        ref var editor  = ref *ImNodes.Editor;

        Im.Group();
        Im.Id.Push(id.Id);

        context.CurrentAttributeId = id;
        var pinIndex = editor.Pins.FindOrCreateIndex(id.Id);
        context.CurrentPinIndex = pinIndex;
        ref var pin = ref editor.Pins.GetWrite(pinIndex);
        pin.Id                = id;
        pin.ParentNodeIndex   = nodeIndex;
        pin.Type              = type;
        pin.Shape             = shape;
        pin.Flags             = context.CurrentAttributeFlags;
        pin.Colors.Background = ImNodes.Style[ImNodesColor.Pin];
        pin.Colors.Hovered    = ImNodes.Style[ImNodesColor.PinHovered];
    }

    public static void EndPinAttribute()
    {
        ref var context = ref Scope.Attribute.Check(Scope.Node);
        Im.IdDisposable.PopUnsafe();
        Im.GroupDisposable.EndUnsafe();
        if (Im.Item.Active && !context.CurrentAttributeFlags.IsDisabled)
        {
            context.ActiveAttribute   = true;
            context.ActiveAttributeId = context.CurrentAttributeId;
        }

        ref var editor = ref *ImNodes.Editor;
        ref var pin    = ref editor.Pins.GetWrite(context.CurrentPinIndex);
        ref var node   = ref editor.Nodes.GetWrite(context.CurrentNodeIndex);
        pin.AttributeRectangle = Im.Item.Bounds;
        node.PinIndices.Add<AttributeIndex>(context.CurrentPinIndex);
    }
}
