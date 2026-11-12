namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Draw text rotated by 90°. </summary>
    /// <param name="text"> The given text. Does not have to be null-terminated. </param>
    /// <param name="alignToFrame"> Whether the text should be aligned to frame. </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void RotatedText(Utf8TextHandler text, bool alignToFrame = false)
    {
        var dl             = Im.Window.DrawList;
        var startVertexIdx = (int)dl.CurrentVertex;
        var screenPos      = Im.Cursor.ScreenPosition;
        using (dl.PushClipRectFullScreen())
        {
            dl.Text(screenPos, Im.Style[ImGuiColor.Text], ref text);
        }

        var textSize     = Im.Font.CalculateSize(ref text, false);
        var endVertexIdx = (int)dl.CurrentVertex;
        var (offset, dummy) = alignToFrame
            ? (new Vector2(Im.Style.FramePadding.Y,                   textSize.X),
                new Vector2(2 * Im.Style.FramePadding.Y + textSize.Y, textSize.X))
            : (new Vector2(0, textSize.X), new Vector2(textSize.Y, textSize.X));
        offset += screenPos;
        for (var index = startVertexIdx; index < endVertexIdx; ++index)
        {
            ref var vertex = ref dl.Pointer->VertexBuffer[index];
            vertex.Position = Rotate(vertex.Position - screenPos, 0, -1) + offset;
        }

        Im.Dummy(dummy);
    }

    /// <summary> Rotate a vector. </summary>
    /// <param name="vec"> The vector to rotate. </param>
    /// <param name="cos"> The cosine of the rotation angle. </param>
    /// <param name="sin"> The sine of the rotation angle. </param>
    /// <returns> The rotated vector. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Vector2 Rotate(Vector2 vec, float cos, float sin)
    {
        ImVec2 ret;
        Im.Native.Methods.Internal.ImRotate(&ret, vec, cos, sin);
        return ret;
    }
}
