namespace ImSharp;

/// <summary> A flattened tree node that can be drawn in a linear list. </summary>
public interface IFlattenedTreeNode
{
    /// <summary> The unique Identifier for the node to use as an ID. </summary>
    public int Identifier { get; }

    /// <summary> The local index of the parent of this node, -1 if no parent exists. </summary>
    public int ParentIndex { get; set; }

    /// <summary> The index of the last direct child node of this node. -1 if this has no child nodes. </summary>
    public int StartsLineTo { get; set; }

    /// <summary> The indentation depth, or depth inside the tree, of this node. Or the number of ancestors. </summary>
    public int IndentationDepth { get; set; }

    /// <summary> An override for the default line color. Only used when <see cref="StartsLineTo"/> is non-negative.</summary>
    public ColorParameter LineColor { get; set; }

    /// <summary> Draw this node. Should not indent itself. </summary>
    /// <param name="flattenedIndex"> The index of the object. </param>
    public void Draw(int flattenedIndex);
}

public static class TreeLine
{
    /// <inheritdoc cref="Draw(IReadOnlyList{IFlattenedTreeNode},Rgba32,float,float,ColorParameter)"/>
    public static void Draw(IReadOnlyList<IFlattenedTreeNode> list, Rgba32 lineColor, ColorParameter alternatingLineColor = default)
        => Draw(list, lineColor, Im.Style.TextHeight, 2 * Im.Style.GlobalScale, alternatingLineColor);

    /// <summary> Draw a tree structure with a child-associating line from a flattened tree node list. </summary>
    /// <param name="list"> The list of flattened nodes, expected to have a valid and sensible structure. </param>
    /// <param name="lineColor"> The color to draw the associative line in if not overwritten by the folder node for lines in odd depth. </param>
    /// <param name="alternatingLineColor"> The color to draw the associative line in if not overwritten by the folder node for lines in even depth. If this is default, <paramref name="lineColor"/> is used. </param>
    /// <param name="itemHeight"> The height of each node in the tree WITHOUT spacing. For regular tree nodes, this should be <see cref="Im.ImGuiStyle.TextHeight"/>. </param>
    /// <param name="lineWidth"> The width of the line to draw in pixels. Should generally be 1, 2, or 3 at most. Possibly scaled by the <see cref="Im.ImGuiStyle.GlobalScale"/>. Default is 2 scaled.  </param>
    public static void Draw(IReadOnlyList<IFlattenedTreeNode> list, Rgba32 lineColor, float itemHeight, float lineWidth,
        ColorParameter alternatingLineColor = default)
    {
        if (list.Count is 0)
            return;

#if TESTING
        lineColor = lineColor.HalfTransparent();
#endif
        // Note that lines may be aliased in different ways depending on the renderer.
        var itemHeightWithSpacing = itemHeight + Im.Style.ItemSpacing.Y;
        // The indentation width is specified by the style.
        var indentationWidth = Im.Style.IndentSpacing;
        // Since we assume regular tree nodes, we use the spacing of the label to offset the line.
        var spacing = Im.Style.TreeNodeToLabelSpacing / 2;
        // The general offset for the vertical line from the cursor point. The -0.5 is for optimizing DX line rendering.
        var lineOffset = new Vector2(spacing - 0.5f, itemHeight);
        // The general offset for the horizontal line from the cursor point. The -0.5 is for optimizing DX line rendering.
        var horizontalOffset  = new Vector2(spacing - lineOffset.X, itemHeight / 2 - 0.5f);
        var actualAlternating = alternatingLineColor.CheckDefault(lineColor);
        var drawList          = Im.Window.DrawList.Shape;

        // Clip the list, despite the continuous lines.
        using var clipper = new Im.ListClipper(list.Count, itemHeightWithSpacing);

        // Keep track of the first index to differentiate potential parents clipped away,
        // and keep track of how many lines we are still missing.
        var       firstIndex        = -1;
        var       hasMissingParents = 0;
        var       currentDepth      = 0;
        using var indent            = new Im.IndentDisposable();

        var preScroll = Im.Scroll.Y;
        var max       = Im.ContentRegion.Available.Y;
        foreach (var localIndex in clipper)
        {
            var item = list[localIndex];
            if (clipper.ItemsInCurrentStep is 1)
            {
                // If an item is focused or active, the stepper produces one step that draws the active item at the start or end,
                // we do not want to consider this item for the line, just draw it invisibly.
                var startPos   = localIndex * clipper.ItemsHeight;
                var endPos     = startPos + clipper.ItemsHeight;
                var earlyFocus = endPos < preScroll;
                var lateFocus  = startPos > preScroll + max;
                if (earlyFocus || lateFocus)
                {
                    // Draw the item itself under the ID of the current identifier.
                    using (Im.Id.Push(item.Identifier))
                    {
                        item.Draw(localIndex);
                    }

                    continue;
                }
            }

            if (firstIndex is -1)
            {
                firstIndex        = localIndex;
                hasMissingParents = item.IndentationDepth;
            }

            // Handle new indentation due to changed depth.
            if (item.IndentationDepth != currentDepth)
            {
                indent.Indent((item.IndentationDepth - currentDepth) * indentationWidth);
                currentDepth = item.IndentationDepth;
            }

            // For any item that has a non-zero depth, we need to draw a horizontal line.
            if (currentDepth is not 0)
            {
                var parent = list[item.ParentIndex];
                // Start point is the right-most point of the line,
                // and we go back according to the indentation difference, which ~should~ always be 1.
                var start = Im.Cursor.ScreenPosition + horizontalOffset;
                var diff = parent.StartsLineTo >= 0
                    ? currentDepth - list[item.ParentIndex].IndentationDepth
                    : 1;
                var end = start with { X = start.X - diff * indentationWidth + lineOffset.X };
                while (parent.StartsLineTo < 0)
                    parent = list[parent.ParentIndex];
                drawList.Line(start, end, parent.LineColor.CheckDefault(int.IsOddInteger(currentDepth) ? lineColor : actualAlternating),
                    lineWidth);
            }

            // If this node starts a line, draw it. 
            StartLine(drawList, item, localIndex, Im.Cursor.ScreenPosition);

            // Handle missing parents due to clipping.
            if (hasMissingParents > 0 && item.ParentIndex >= 0 && item.ParentIndex < firstIndex)
            {
                var parent = list[item.ParentIndex];
                // We only draw the omitted lines for the last node of the parent for uniqueness.
                if (parent.StartsLineTo == localIndex)
                {
#if TESTING
                    var orig = lineColor;
                    lineColor = Rgba32.Red.HalfTransparent();
#endif
                    var cursorOffset = new Vector2(indentationWidth, (localIndex - item.ParentIndex) * itemHeightWithSpacing);
                    StartLine(drawList, parent, item.ParentIndex, Im.Cursor.ScreenPosition - cursorOffset);
                    --hasMissingParents;
#if TESTING
                    lineColor = orig;
#endif
                }
            }


            // Draw the item itself under the ID of the current identifier.
            using var id = Im.Id.Push(item.Identifier);
            item.Draw(localIndex);

            // Remove missing parents if they have nodes between start and end.
            if (currentDepth < hasMissingParents)
                hasMissingParents = currentDepth;
        }

        // Draw lines that stretch from before the first item to after the last item.
        if (hasMissingParents > 0)
        {
            var start = Im.Window.Position;
            start.X += lineOffset.X;
            var end = start;
            end.Y += Im.Window.Size.Y;
            var currentIndex = firstIndex;
            do
            {
                var s = start with { X = start.X + (hasMissingParents - 1) * indentationWidth };
                var e = end with { X = s.X };
#if TESTING
                drawList.Line(s, e, Rgba32.Green.HalfTransparent(), lineWidth);
#else
                currentIndex = list[currentIndex].ParentIndex;
                var parent = list[currentIndex];
                drawList.Line(s, e, parent.LineColor.CheckDefault(int.IsOddInteger(hasMissingParents) ? lineColor : actualAlternating),
                    lineWidth);
                --hasMissingParents;
#endif
            } while (hasMissingParents > 0);
        }

        return;

        void StartLine(in Im.DrawList.DrawListShapes drawList, in IFlattenedTreeNode node, int nodeIndex, Vector2 startPosition)
        {
            // If this node does not start a line, skip.
            if (node.StartsLineTo < 0)
                return;

            // Start is straight below the downward triangle for a tree node, immediately outside the selectable area.
            var start = startPosition + lineOffset;
            // End is at the center of the last horizontal line.
            var end = start with { Y = start.Y + (node.StartsLineTo - nodeIndex) * itemHeightWithSpacing - lineOffset.Y / 2 };
            drawList.Line(start, end, node.LineColor.CheckDefault(int.IsEvenInteger(node.IndentationDepth) ? lineColor : actualAlternating),
                lineWidth);
        }
    }
}
