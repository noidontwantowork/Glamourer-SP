namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Draw a tree node with an optional list of icon-buttons in the same line, while clipping any overextending text. </summary>
    /// <typeparam name="TIcon"> The type of the icons to draw. </typeparam>
    /// <typeparam name="TData"> Additional data relevant to the tree node and its icons. </typeparam>
    /// <typeparam name="TData2"> Assignable from <see cref="TData"/>. </typeparam>
    /// <param name="label"> The label used for the tree node as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="flags"> Additional flags to pass to the tree node. </param>
    /// <param name="data"> The data to pass to the icon buttons. </param>
    /// <param name="toggledOpen"> Returns whether this tree node was toggled open in this frame. </param>
    /// <param name="icons"> A number of icons to draw right-aligned on the available space for this tree-node. </param>
    /// <returns> A disposable object that evaluates to true if the begun tree node is currently expanded. Use with using. </returns>
    public static Im.TreeNodeDisposable IconTreeNode<TIcon, TData, TData2>(Utf8LabelHandler label, TreeNodeFlags flags, in TData data,
        out bool toggledOpen, params IReadOnlyCollection<IStatusIcon<TIcon, TData2>> icons)
        where TIcon : IIconStandIn
        where TData : TData2
    {
        // We only care about visible icons.
        var visibleCount = 0;
        foreach (var icon in icons)
        {
            if (icon.Visible(data))
                ++visibleCount;
        }

        Im.TreeNodeDisposable node;
        // Short-cut to save work when no icons are visible.
        if (visibleCount is 0)
        {
            node        = Im.Tree.Node(label, flags);
            toggledOpen = Im.Tree.ToggledOpen();
            return node;
        }

        // We need to add a clipping rectangle to prevent the text from overlapping with the buttons.
        var availableRegion = Im.ContentRegion.Available.X;
        var iconStart       = availableRegion - visibleCount * Im.Style.TextHeight;
        var iconSize        = new Vector2(Im.Style.TextHeight);
        var treeNodeEnd     = iconStart - Im.Style.ItemInnerSpacing.X;
        var treeRect        = Rectangle.FromSize(Im.Cursor.ScreenPosition, new Vector2(treeNodeEnd, Im.Style.TextHeightWithSpacing));
        var startPos        = Im.Cursor.Position;

        // Start drawing icons. There is at least one visible.
        Im.Cursor.X = iconStart;
        // Spare some work by just pushing the font and styles once instead of per icon.
        using (var font = Im.Font.Push(TIcon.Font))
        {
            using var style = new Im.ColorStyleDisposable().Push(ImGuiColor.Button, Vector4.Zero)
                .Push(ImGuiColor.ButtonHovered,    Vector4.Zero)
                .Push(ImGuiColor.ButtonActive,     Vector4.Zero)
                .Push(ImGuiColor.Border,           Vector4.Zero)
                .Push(ImStyleDouble.FramePadding,  Vector2.Zero)
                .Push(ImStyleSingle.FrameRounding, 0);

            foreach (var (index, icon) in icons.Index())
            {
                if (!icon.Visible(data))
                    continue;

                // Use the icon information and passed data to get the color.
                // Optionally push an ID for the button.
                var       l  = icon.Label(data);
                using var id = l.IsEmpty ? null : Im.Id.Push(l);

                // Handle the color of the icon depending on activity and hovering.
                var hovering = Im.Mouse.IsHoveringRectangle(Rectangle.FromSize(Im.Cursor.ScreenPosition, iconSize));
                var c = hovering
                    ? Im.Mouse.IsDown(MouseButton.Left)
                        ? icon.ActiveColor(data)
                        : icon.HoveredColor(data)
                    : icon.Color(data);

                style.Push(ImGuiColor.Text, c);
                if (Im.Button(icon.Icon(data).Span, iconSize))
                    icon.OnClick(data);
                style.PopColor();

                // Draw a tooltip
                if (hovering && icon.HasTooltip(data))
                {
                    using var tt = Im.Tooltip.Begin();
                    // Draw the tooltip without icon font.
                    font.Push(Im.Font.Default);
                    icon.DrawTooltip(data);
                    font.Pop();
                }

                // Same line with no spacing.
                if (index != icons.Count - 1)
                    Im.Line.NoSpacing();
            }
        }

        // Draw the tree node as the last item because this is what we want to interact with in other functions.
        Im.Cursor.Position = startPos;
        using (Im.Drawing.PushClipRect(treeRect, true))
        {
            node        = Im.Tree.Node(label, flags);
            toggledOpen = Im.Tree.ToggledOpen();
        }

        // Handle the background over the icons when hovering the tree node or activating it.
        var hovered = Im.Item.Hovered();
        var background = flags.HasFlag(TreeNodeFlags.Selected)
            ? Im.Style[ImGuiColor.Header]
            : hovered
                ? Im.Item.Active
                    ? Im.Style[ImGuiColor.HeaderActive]
                    : Im.Style[ImGuiColor.HeaderHovered]
                : Vector4.Zero;
        if (background.W is not 0)
            Im.Window.DrawList.Shape.RectangleFilled(
                Rectangle.FromSize(new Vector2(treeRect.Maximum.X, treeRect.Minimum.Y),
                    new Vector2(availableRegion,                   Im.Style.TextHeight)), background);

        return node;
    }
}
