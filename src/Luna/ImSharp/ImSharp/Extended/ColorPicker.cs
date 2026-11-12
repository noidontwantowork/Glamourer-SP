namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Draw a button-only preview of an RGB color with an optional contrasted letter within the button. </summary>
    /// <param name="label"> The label and ID for the picker as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="tooltip"> An optional tooltip when the label is hovered. Does not have to be null-terminated. </param>
    /// <param name="input"> The input color. </param>
    /// <param name="output"> The output color. </param>
    /// <param name="letter"> The letter to draw inside the button. If this is whitespace, no letter is drawn. </param>
    /// <returns> True when the output color changed in this frame. </returns>
    public static bool ColorPickerButton(Utf8LabelHandler label, Utf8TextHandler tooltip, Vector3 input, out Vector3 output,
        char letter = ' ')
    {
        output = input;
        var ret = Im.Color.Editor(label, ref input,
            ColorEditorFlags.NoInputs
          | ColorEditorFlags.DisplayRgb
          | ColorEditorFlags.InputRgb
          | ColorEditorFlags.NoTooltip
          | ColorEditorFlags.Hdr);

        if (!char.IsWhiteSpace(letter) && Im.Item.Visible)
        {
            Span<byte> text   = stackalloc byte[8];
            var        length = Encoding.UTF8.GetBytes([letter], text);
            text[length] = 0;
            text         = text[..length];
            var textSize  = Im.Font.CalculateSize(text);
            var center    = Im.Item.UpperLeftCorner + (Im.Item.Size - textSize) / 2;
            var textColor = Rgba32.ContrastColor(new Vector4(output, 0.7f));
            Im.Window.DrawList.Text(center, textColor, text);
        }

        output = input;

        Im.Tooltip.OnHover(tooltip);
        return ret;
    }
}
