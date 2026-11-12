namespace ImSharp;

public static partial class ImEx
{
    private static ReadOnlySpan<byte> HexBytes
        => "0123456789ABCDEF"u8;

    /// <summary> Draw a set of bytes as a hex viewer, grouped in octets of alternating color. </summary>
    /// <param name="data"> The data to draw. </param>
    /// <param name="offsetColor"> The color for leading offset text. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
    /// <param name="color1"> The first color for the alternating blocks. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
    /// <param name="color2"> The second color for the alternating blocks. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.TextDisabled"/> is used. </param>
    public static unsafe void HexViewer(ReadOnlySpan<byte> data, ColorParameter offsetColor = default, ColorParameter color1 = default,
        ColorParameter color2 = default)
    {
        if (data.Length == 0)
            return;

        using var mono    = Im.Font.PushMono();
        var       emWidth = Im.Font.Current.GetCharacterAdvance('m');
        var       spacing = Im.Style.ItemInnerSpacing.X;

        // Get the required number of digits for the byte offset.
        var addressDigitCount = 8 - (BitOperations.LeadingZeroCount((uint)data.Length - 1) >> 2);
        // Spacing is correct for 32 bytes shown per line, too much for 16 and not enough for more, but should not generally matter much.
        var contentRegion = Im.ContentRegion.Available.X;
        var charsPerRow   = (int)MathF.Floor((contentRegion - 9 * spacing) / emWidth);
        var bytesPerRow   = (charsPerRow - addressDigitCount - 2) / 4;

        // Check that we actually need multiple lines and lock to power of 2.
        bytesPerRow = Math.Max(8, Math.Min(1 << BitOperations.Log2((uint)bytesPerRow), data.Length));
        if (bytesPerRow == data.Length)
            addressDigitCount = 0;

        // Set default colors if necessary.
        var c1 = color1.CheckDefault(ImGuiColor.Text);
        var c2 = color2.CheckDefault(ImGuiColor.TextDisabled);
        var o  = offsetColor.CheckDefault(ImGuiColor.Text);

        // Prepare the full buffer. by setting up constant values.
        var capacity = addressDigitCount + 2 + 4 * bytesPerRow; // address ':' {' ' hex hex} ' ' {printable}
        var buffer   = stackalloc byte[capacity + 1];
        buffer[capacity]          = 0;
        buffer[addressDigitCount] = (byte)':';
        var offset = addressDigitCount + 1;
        var end    = offset + 3 * bytesPerRow;
        for (var i = offset; i <= end; i += 3)
            buffer[i] = (byte)' ';

        var       numRows = (data.Length + bytesPerRow - 1) / bytesPerRow;
        using var clipper = new Im.ListClipper(numRows, Im.Style.TextHeightWithSpacing);
        foreach (var actualRow in clipper)
        {
            if (actualRow >= numRows)
                return;

            if (actualRow < 0)
                continue;

            // Prepare buffer data. Set the address first.
            var rowStart = actualRow * bytesPerRow;
            var bufferI  = 0;
            for (var i = addressDigitCount; i-- > 0;)
                buffer[bufferI++] = HexBytes[(rowStart >> (i << 2)) & 0xF];

            // Set the actual byte values for hex and printable.
            var numBytes        = Math.Min(data.Length - rowStart, bytesPerRow);
            var printableOffset = 3 * bytesPerRow + addressDigitCount + 2;
            for (var i = 0; i < numBytes; ++i)
            {
                var @byte = data[rowStart + i];
                buffer[bufferI += 2]        = HexBytes[@byte >> 4];
                buffer[++bufferI]           = HexBytes[@byte & 0xF];
                buffer[printableOffset + i] = @byte is >= 32 and < 127 ? @byte : (byte)'.';
            }

            // Clear lacking byte values for the last line.
            for (var i = numBytes; i < bytesPerRow; ++i)
            {
                buffer[bufferI += 2]        = (byte)' ';
                buffer[++bufferI]           = (byte)' ';
                buffer[printableOffset + i] = (byte)' ';
            }

            // Start drawing the text. First the address, if using more than a single row.
            var packStart = buffer + addressDigitCount + 2;
            if (bytesPerRow < data.Length)
            {
                using var color = Im.Color.Push(ImGuiColor.Text, o);
                Im.Native.Methods.Text.TextUnformatted(buffer, packStart);
                Im.Line.Same(0, spacing);
            }

            // Then the 8 byte blocks in alternating colors with slight spacing in between.
            for (var i = 0; i < bytesPerRow; i += 8)
            {
                var       packEnd = packStart + 24;
                using var color   = Im.Color.Push(ImGuiColor.Text, (i & 8) == 0 ? c1 : c2);
                Im.Native.Methods.Text.TextUnformatted(packStart, packEnd);
                Im.Line.Same(0, spacing);
                packStart = packEnd;
            }

            // Finally the printable block.
            Im.Native.Methods.Text.TextUnformatted(packStart, buffer + capacity);
        }
    }
}
