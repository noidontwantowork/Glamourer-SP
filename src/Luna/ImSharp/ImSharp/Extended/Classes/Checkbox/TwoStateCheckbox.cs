namespace ImSharp;

/// <summary> A two-state Checkbox that displays either a checkmark for True values or an X for False values, with no empty state. </summary>
internal sealed class TwoStateCheckbox : MultiStateCheckbox<bool>
{
    /// <summary> A static instance to draw more easily. </summary>
    public static readonly TwoStateCheckbox Instance = new();

    /// <inheritdoc/>
    protected override void RenderSymbol(bool value, Vector2 position, float size)
    {
        if (value)
            Im.Render.Checkmark(Im.Window.DrawList, position, Im.Color.Get(ImGuiColor.CheckMark), size);
        else
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
    /// <summary> Draw a checkbox that displays a checkmark or an X when true or false respectively. </summary>
    /// <param name="label"> The label as text. Does not have to be null-terminated. </param>
    /// <param name="value"> The input and output value of the checkbox. </param>
    /// <returns> True if the checkbox has been clicked in this frame, in which case <paramref name="value"/> will be flipped. </returns>
    public static bool TwoStateCheckbox(Utf8LabelHandler label, ref bool value)
        => ImSharp.TwoStateCheckbox.Instance.Draw(label, ref value);
}
