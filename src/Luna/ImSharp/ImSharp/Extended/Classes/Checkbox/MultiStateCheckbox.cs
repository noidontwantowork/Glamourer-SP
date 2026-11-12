namespace ImSharp;

/// <summary> Draw a checkbox that toggles forward or backward between different states. </summary>
public abstract class MultiStateCheckbox<T>
{
    /// <summary> Render the symbol corresponding to <paramref name="value"/> starting at <paramref name="position"/> and with <paramref name="size"/> as box size. </summary>
    protected abstract void RenderSymbol(T value, Vector2 position, float size);

    /// <summary> Increment the value. </summary>
    protected abstract T NextValue(T value);

    /// <summary> Decrement the value. </summary>
    protected abstract T PreviousValue(T value);

    /// <summary> Draw the multi state checkbox. </summary>
    /// <param name="label"> The label for the checkbox as a UTF8 string. HAS to be null-terminated. </param>
    /// <param name="value"> The input/output value. </param>
    /// <returns> True when <paramref name="value"/> changed in this frame. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Draw(Utf8LabelHandler label, ref T value)
        => Draw(label, value, out value);

    /// <summary> Draw the multi state checkbox. </summary>
    /// <param name="label"> The label for the checkbox as a UTF8 string. HAS to be null-terminated. </param>
    /// <param name="currentValue"> The input value. </param>
    /// <param name="newValue"> The output value. </param>
    /// <returns> True when this was toggled this frame and a new value is returned. </returns>
    public bool Draw(Utf8LabelHandler label, T currentValue, out T newValue)
    {
        newValue = currentValue;
        var window = Im.Window.Current;
        if (window.SkipItems || !ImEx.SplitLabel(ref label, out var visibleText, out var id))
            return false;

        var labelSize = Im.Font.CalculateSize(visibleText, false);
        // Calculate the bounding box of the checkbox including the label.
        var squareSize  = Im.Style.FrameHeight;
        var screenPos   = window.CursorPosition;
        var itemSize    = new Vector2(squareSize + (labelSize.X > 0 ? Im.Style.ItemInnerSpacing.X + labelSize.X : 0), squareSize);
        var boundingBox = new Rectangle(screenPos, screenPos + itemSize);

        // Add the item to internals. Skip it if it is clipped.
        Im.Item.SetSize(itemSize, Im.Style.FramePadding.Y);
        if (!Im.Item.Add(boundingBox, id))
            return false;

        // Handle user interaction.
        var returnValue = Im.Behavior.Button(boundingBox, id, out var hovered, out var held);
        if (returnValue)
            newValue = NextValue(currentValue);
        else
        {
            returnValue = Im.Item.RightClicked();
            if (returnValue)
                newValue = PreviousValue(currentValue);
        }

        // Draw the checkbox.
        var checkBoundingBox = new Rectangle(screenPos, screenPos + new Vector2(squareSize));
        Im.Render.NavigationHighlight(boundingBox, id);
        Im.Render.Frame(checkBoundingBox.Minimum, checkBoundingBox.Maximum, ImEx.GetFrameBackgroundColor(hovered, held), Im.Style.FrameRounding);

        // Draw the desired symbol into the checkbox.
        var paddingSize = Math.Max(1, (int)(squareSize / 6));
        RenderSymbol(currentValue, screenPos + new Vector2(paddingSize), squareSize - paddingSize * 2);

        // Add the label if there is one visible.
        if (labelSize.X > 0)
        {
            var labelPos = new Vector2(checkBoundingBox.Maximum.X + Im.Style.ItemInnerSpacing.X, checkBoundingBox.Minimum.Y + Im.Style.FramePadding.Y);
            Im.DrawList.Window.Text(labelPos, Im.Color.Get(ImGuiColor.Text), visibleText);
        }

        return returnValue;
    }
}
