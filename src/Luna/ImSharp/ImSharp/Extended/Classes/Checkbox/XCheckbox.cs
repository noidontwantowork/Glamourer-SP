namespace ImSharp;

/// <summary> A two-state Checkbox that displays either an X for True values and empty for False values. </summary>
internal sealed class XCheckbox : MultiStateCheckbox<bool>
{
    /// <summary> A static instance to draw more easily. </summary>
    public static readonly XCheckbox Instance = new();

    /// <inheritdoc/>
    protected override void RenderSymbol(bool value, Vector2 position, float size)
    {
        if (value)
            Im.Render.Cross(Im.Window.DrawList, position, Im.Color.Get(ImGuiColor.CheckMark), size);
    }

    /// <inheritdoc/>
    protected override bool NextValue(bool value)
        => !value;

    /// <inheritdoc/>
    protected override bool PreviousValue(bool value)
        => !value;
}

public static partial class ImEx
{
    /// <summary> Draw a checkbox that displays an X instead of a checkmark when true. </summary>
    /// <param name="label"> The label as text. Does not have to be null-terminated. </param>
    /// <param name="value"> The input and output value of the checkbox. </param>
    /// <returns> True if the checkbox has been clicked in this frame, in which case <paramref name="value"/> will be flipped. </returns>
    public static bool XCheckbox(Utf8LabelHandler label, ref bool value)
        => ImSharp.XCheckbox.Instance.Draw(label, ref value);
}

public class Magnifier()
{
    public static void Draw(int width, int height)
    {
        var pos = Im.Cursor.Position;
        
    }

}
