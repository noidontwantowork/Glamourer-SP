namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Get the optimal width for a GUID input field. </summary>
    public static float GuidInputWidth
        => 36 * Im.Font.Mono.GetCharacterAdvance('0') + 2 * Im.Style.FramePadding.X;

    /// <summary> Draw a text input field that only accepts valid GUID input and shows a format hint for it. </summary>
    /// <param name="label"> The label of the input as text. Does not have to be null-terminated. </param>
    /// <param name="initialText"> Optional initial text entered into the input as text. Does not have to be null-terminated. </param>
    /// <param name="guid"> The parsed and returned GUID if the current input is valid, otherwise an empty GUID. </param>
    /// <param name="width"> The width for the input in pixels. If this is 0, <see cref="GuidInputWidth"/> will be used. </param>
    /// <returns> True if the item is deactivated after being edited and the parsed GUID is valid, false otherwise. </returns>
    public static unsafe bool GuidInput(Utf8LabelHandler label, Utf8TextHandler initialText, out Guid guid, float width = 0)
    {
        if (!SplitLabel(ref label, out var visible, out var labelId))
        {
            guid = Guid.Empty;
            return false;
        }

        Im.Item.SetNextWidth(width is 0 ? GuidInputWidth : width);
        using var _      = Im.Id.Push(labelId);
        using var group  = Im.Group();
        var       id     = Im.Id.Current;
        var       buffer = InputStringHandlerBuffer.Buffer;
        var       size   = (ulong)InputStringHandlerBuffer.Size;
        if (!id.Active)
        {
            buffer = TextStringHandlerBuffer.Buffer;
            size   = (ulong)TextStringHandlerBuffer.Size;
            var begin = initialText.Start(out var end);
            if (begin != TextStringHandlerBuffer.Buffer)
            {
                var l = (int)(end - begin);
                new ReadOnlySpan<byte>(begin, l).CopyTo(TextStringHandlerBuffer.Span);
                TextStringHandlerBuffer.Buffer[l] = 0;
            }
        }

        var hint   = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"u8;
        var empty  = "\0"u8;
        var length = 0;
        using (Im.Font.PushMono())
        {
            var input = Im.Native.Methods.Inputs.InputTextWithHint(empty.Start(), hint.Start(), buffer, size,
                InputTextFlags.CallbackEdit | InputTextFlags.CallbackCharFilter | InputTextFlags.CallbackAlways, &FilterInput, &length);
            if ((input || Im.Item.Activated) && buffer != InputStringHandlerBuffer.Buffer)
            {
                TextStringHandlerBuffer.Span[..length].CopyTo(InputStringHandlerBuffer.Span);
                InputStringHandlerBuffer.Buffer[length] = 0;
            }

            if (id.Active && length is > 0 and < 36)
            {
                using var color      = Im.Color.Push(ImGuiColor.Text, Im.Style[ImGuiColor.TextDisabled]);
                var       textLength = Im.Font.CalculateSize(new ReadOnlySpan<byte>(buffer, length)) with { Y = 0 };
                Im.Window.DrawList.TextClipped(Im.Item.UpperLeftCorner + Im.Style.FramePadding + textLength,
                    Im.Item.LowerRightCorner - Im.Style.FramePadding, hint[length..]);
            }
        }

        var ret = Im.Item.DeactivatedAfterEdit;

        if (ret && length is 36)
        {
            Span<char> t = stackalloc char[37];
            t[36] = '\0';
            t     = t[..^1];
            Encoding.UTF8.GetChars(new ReadOnlySpan<byte>(buffer, 36), t);
            (guid, ret) = Guid.TryParseExact(t, "D", out var g) ? (g, true) : (Guid.Empty, false);
        }
        else
        {
            guid = Guid.Empty;
            ret  = false;
        }

        if (!visible.IsEmpty)
        {
            Im.Line.SameInner();
            TextFrameAligned(visible);
        }

        return ret;
    }

    /// <inheritdoc cref="GuidInput(Utf8LabelHandler,Utf8TextHandler,out Guid, float)"/>
    public static bool GuidInput(Utf8LabelHandler label, ref Guid? guid, float width = 0)
    {
        Span<byte> span = stackalloc byte[37];
        if (guid.HasValue)
        {
            span[^1] = 0;
            if (!guid.Value.TryFormat(span, out var count) || count is not 36)
                return false;
        }
        else
        {
            span[0] = 0;
        }

        if (!GuidInput(label, span, out var newGuid, width))
            return false;

        guid = newGuid;
        return true;
    }

    [UnmanagedCallersOnly]
    private static unsafe int FilterInput(Im.Native.InputTextCallbackData* input)
    {
        // Only allow hexadecimal values and dashes, remove other symbols.
        if (input->EventFlag is InputTextFlags.CallbackCharFilter)
        {
            var lowerCase = char.ToLowerInvariant(input->EventChar.Character);
            if (lowerCase is (< '0' or > '9') and (< 'a' or > 'f') and not '-')
                return 1;

            // Only allow lowercase ascii
            if (input->EventChar.Value != lowerCase)
                input->EventChar = new ImWchar(lowerCase);

            return 0;
        }

        if (input->EventFlag is InputTextFlags.CallbackEdit)
        {
            var length = input->BufferTextLength;
            for (var i = 0; i < length; ++i)
            {
                switch (input->Buffer[i])
                {
                    // Valid characters.
                    case >= (byte)'0' and <= (byte)'9': continue;
                    case >= (byte)'a' and <= (byte)'f': continue;
                    // Only allow dashes at the correct positions.
                    case (byte)'-' when i is 8 or 13 or 18 or 23: continue;
                    // Turn valid uppercase characters lowercase.
                    case >= (byte)'A' and <= (byte)'F':
                        input->Buffer[i]   += 32;
                        input->BufferDirty =  true;
                        continue;

                    // Delete all other characters.
                    default:
                        Im.Native.InputTextCallbackData.DeleteChars(input, i--, 1);
                        --length;
                        input->BufferDirty = true;
                        break;
                }
            }

            // Add dashes in the correct positions if necessary.
            if (length > 8)
            {
                var dash = stackalloc byte[2]
                {
                    (byte)'-',
                    0,
                };
                if (input->Buffer[8] is not (byte)'-')
                {
                    Im.Native.InputTextCallbackData.InsertChars(input, 8, dash, dash + 1);
                    input->BufferDirty = true;
                    ++length;
                }

                if (length > 13)
                {
                    if (input->Buffer[13] is not (byte)'-')
                    {
                        Im.Native.InputTextCallbackData.InsertChars(input, 13, dash, dash + 1);
                        input->BufferDirty = true;
                        ++length;
                    }

                    if (length > 18)
                    {
                        if (input->Buffer[18] is not (byte)'-')
                        {
                            Im.Native.InputTextCallbackData.InsertChars(input, 18, dash, dash + 1);
                            input->BufferDirty = true;
                            ++length;
                        }

                        if (length > 23)
                            if (input->Buffer[23] is not (byte)'-')
                            {
                                Im.Native.InputTextCallbackData.InsertChars(input, 23, dash, dash + 1);
                                input->BufferDirty = true;
                                ++length;
                            }
                    }
                }
            }

            // Delete all extra characters.
            if (length > 36)
            {
                Im.Native.InputTextCallbackData.DeleteChars(input, 36, length - 36);
                input->BufferDirty = true;
            }

            *(int*)input->UserData = length;
        }
        else
        {
            *(int*)input->UserData = input->BufferTextLength;
        }

        return 0;
    }
}
