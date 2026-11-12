namespace ImSharp;

public unsafe struct InputStringHandlerBuffer : IStringHandlerBuffer
{
    /// <summary>
    ///   A temporary storage string for <see cref="ImEx.InputOnDeactivation"/> text inputs,
    ///   so that one is able to deactivate an input by activating another input that is drawn earlier.
    ///   It is reset to null at the beginning of every frame by <see cref="ImSharpPerFrame.OnUpdate"/>.
    /// </summary>
    public static StringU8 FrameStorageString = StringU8.Null;

    public static int Size
        => ImSharpConfiguration.Context->InputBufferSize;

    public static byte* Buffer
        => ImSharpConfiguration.Context->InputBuffer;

    public static Span<byte> Span
        => new(Buffer, Size);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool Write(ReadOnlySpan<char> text, out byte* end)
        => IStringHandlerBuffer.Write<InputStringHandlerBuffer>(text, out end);

    /// <summary> Get the actual input data span for a text input for input after deactivation. </summary>
    /// <param name="id"> The ID of the input. </param>
    /// <param name="input"> The input data. </param>
    /// <returns> The actual buffer to use. </returns>
    public static Span<byte> GetInputSpan(ImGuiId id, ref Utf8TextHandler input)
    {
        if (id.ActivePreviousFrame)
        {
            if (FrameStorageString.IsNull)
                return Span;

            // This is safe since the string is an allocated and owned byte[] if it exists.
            var span = FrameStorageString.Span;
            return Unsafe.As<ReadOnlySpan<byte>, Span<byte>>(ref span);
        }

        var begin = input.Start(out var end);
        if (begin != TextStringHandlerBuffer.Buffer)
        {
            var span = new ReadOnlySpan<byte>(begin, (int)(end - begin));
            span.CopyTo(TextStringHandlerBuffer.Span);
            TextStringHandlerBuffer.Span[span.Length] = 0;
        }

        return TextStringHandlerBuffer.Span;
    }

    /// <summary> Using the current state data queried before and the result of the input method, return and prepare the data for active widgets. </summary>
    /// <param name="buffer"> The buffer passed to ImGui. </param>
    /// <param name="copyBuffer"> Whether the buffer might need to be copied (the input function returned true or the item activated). </param>
    /// <param name="saveTemp"> Whether we had a different input active before and need to store its data. </param>
    /// <param name="oldLength"> The length of the prior input, if any. </param>
    /// <param name="newLength"> The length of the text after the input function. </param>
    /// <returns> True if the input was deactivated this frame after being edited any time. </returns>
    public static bool ReturnActive(byte* buffer, bool copyBuffer, bool saveTemp, int oldLength, out int newLength)
    {
        newLength = Im.Context.Pointer->InputTextState.CurrentLengthA;
        if (copyBuffer && buffer != Buffer)
        {
            if (saveTemp)
                FrameStorageString = new StringU8(Span[..oldLength], false);

            TextStringHandlerBuffer.Span[..newLength].CopyTo(Span);
            Buffer[newLength] = 0;
        }

        return Im.Item.DeactivatedAfterEdit || Im.Item.Deactivated && Im.Keyboard.IsPressed(Key.Enter);
    }
}
