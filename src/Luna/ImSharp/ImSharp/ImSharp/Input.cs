namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper class for all input widgets. </summary>
    public static unsafe class Input
    {
        /// <summary> Draw a text input. </summary>
        /// <param name="label"> The input label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="buffer"> The buffer preloaded with the input data, which should have enough space to edit inside the buffer. </param>
        /// <param name="result"> When true is returned, an owned, null-terminated string of UTF8 bytes. </param>
        /// <param name="hint"> An optional hint to display in the input box as long as the input is empty as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <returns> Whether the value changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Text(Utf8LabelHandler label, Span<byte> buffer, out StringU8 result, Utf8HintHandler hint = default,
            InputTextFlags flags = InputTextFlags.None)
        {
            if (!Text(label.Start(), buffer.Start(), (uint)buffer.Length, hint.Start(), flags))
            {
                result = StringU8.Empty;
                return false;
            }

            result = buffer[..Context.Pointer->InputTextState.CurrentLengthA].CloneNullTerminated();
            return true;
        }

        /// <summary> Draw a text input. </summary>
        /// <param name="label"> The input label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="buffer"> The buffer preloaded with the input data, which should have enough space to edit inside the buffer. </param>
        /// <param name="length"> The new length of the data contained in the buffer. </param>
        /// <param name="hint"> An optional hint to display in the input box as long as the input is empty as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <param name="maxLength"> The maximum length of the input string. </param>
        /// <returns> Whether the value changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Text(Utf8LabelHandler label, Span<byte> buffer, out ulong length, Utf8HintHandler hint = default,
            InputTextFlags flags = InputTextFlags.None, uint maxLength = uint.MaxValue)
        {
            if (maxLength >= buffer.Length)
                maxLength = (uint)buffer.Length;
            if (!Text(label.Start(), buffer.Start(), maxLength, hint.Start(), flags))
            {
                length = 0;
                return false;
            }

            length = (ulong)Context.Pointer->InputTextState.CurrentLengthA;
            return true;
        }

        /// <summary> Draw a text input. </summary>
        /// <param name="label"> The input label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="text"> A UTF16 string to edit. This gets updated if the value changes. </param>
        /// <param name="hint"> An optional hint to display in the input box as long as the input is empty as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <param name="maxLength"> The maximum length of the input string. </param>
        /// <returns> Whether the value changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Text(Utf8LabelHandler label, ref string text, Utf8HintHandler hint = default,
            InputTextFlags flags = InputTextFlags.None, uint maxLength = uint.MaxValue)
        {
            text.AsSpan().CopyInto<TextStringHandlerBuffer>(out _);
            if (maxLength >= TextStringHandlerBuffer.Size)
                maxLength = (uint)TextStringHandlerBuffer.Size;
            var ret = Text(label.Start(), TextStringHandlerBuffer.Buffer, maxLength, hint.Start(), flags);

            if (Item.Edited)
                text = Encoding.UTF8.GetString(new ReadOnlySpan<byte>(TextStringHandlerBuffer.Buffer,
                    Context.Pointer->InputTextState.CurrentLengthA));
            return ret;
        }

        /// <summary> Draw a text input. </summary>
        /// <param name="label"> The input label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="text"> A UTF8 string to edit. This gets updated if the value changes. </param>
        /// <param name="hint"> An optional hint to display in the input box as long as the input is empty as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <param name="maxLength"> The maximum length of the input string. </param> 
        /// <returns> Whether the value changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Text(Utf8LabelHandler label, ref StringU8 text, Utf8HintHandler hint = default,
            InputTextFlags flags = InputTextFlags.None, uint maxLength = uint.MaxValue)
        {
            text.Span.CopyInto<TextStringHandlerBuffer>();
            if (maxLength >= TextStringHandlerBuffer.Size)
                maxLength = (uint)TextStringHandlerBuffer.Size;
            var ret = Text(label.Start(), TextStringHandlerBuffer.Buffer, maxLength, hint.Start(), flags);

            if (Item.Edited)
                text = new StringU8(new ReadOnlySpan<byte>(TextStringHandlerBuffer.Buffer, Context.Pointer->InputTextState.CurrentLengthA),
                    false);

            return ret;
        }

        /// <summary> Draw a text input of a specific size spanning multiple lines. </summary>
        /// <param name="label"> The input label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="buffer"> The buffer preloaded with the input data, which should have enough space to edit inside the buffer. </param>
        /// <param name="result"> When true is returned, an owned, null-terminated string of UTF8 bytes. </param>
        /// <param name="size"> The size of the input widget. If this is 0 in an axis, spans the available area. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <returns> Whether the value changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool MultiLine(Utf8LabelHandler label, Span<byte> buffer, out StringU8 result, Vector2 size = default,
            InputTextFlags flags = InputTextFlags.None)
        {
            if (Native.Methods.Inputs.InputTextMultiline(label.Start(), buffer.Start(), (ulong)buffer.Length, size, flags, null, null))
            {
                result = buffer[..Context.Pointer->InputTextState.CurrentLengthA].CloneNullTerminated();
                return true;
            }

            result = StringU8.Empty;
            return false;
        }

        /// <summary> Draw a text input of a specific size spanning multiple lines. </summary>
        /// <param name="label"> The input label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="buffer"> The buffer preloaded with the input data, which should have enough space to edit inside the buffer. </param>
        /// <param name="length"> The new length of the data contained in the buffer. </param>
        /// <param name="size"> The size of the input widget. If this is 0 in an axis, spans the available area. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <returns> Whether the value changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool MultiLine(Utf8LabelHandler label, Span<byte> buffer, out ulong length, Vector2 size = default,
            InputTextFlags flags = InputTextFlags.None)
        {
            if (!Native.Methods.Inputs.InputTextMultiline(label.Start(), buffer.Start(), (ulong)buffer.Length, size,
                    flags, null, null))
            {
                length = 0;
                return false;
            }

            length = (ulong)Context.Pointer->InputTextState.CurrentLengthA;
            return true;
        }

        /// <summary> Draw a text input of a specific size spanning multiple lines. </summary>
        /// <param name="label"> The input label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="text"> A UTF8 string to edit. This gets updated if the value changes. </param>
        /// <param name="size"> The size of the input widget. If this is 0 in an axis, spans the available area. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <returns> Whether the value changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool MultiLine(Utf8LabelHandler label, ref StringU8 text, Vector2 size = default,
            InputTextFlags flags = InputTextFlags.None)
        {
            text.Span.CopyInto<TextStringHandlerBuffer>();
            var ret = Native.Methods.Inputs.InputTextMultiline(label.Start(), TextStringHandlerBuffer.Buffer,
                (ulong)TextStringHandlerBuffer.Size, size, flags, null, null);

            if (Item.Edited)
                text = new StringU8(new ReadOnlySpan<byte>(TextStringHandlerBuffer.Buffer, Context.Pointer->InputTextState.CurrentLengthA),
                    false);
            return ret;
        }

        /// <summary> Draw a text input of a specific size spanning multiple lines. </summary>
        /// <param name="label"> The input label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="text"> A UTF16 string to edit. This gets updated if the value changes. </param>
        /// <param name="size"> The size of the input widget. If this is 0 in an axis, spans the available area. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <returns> Whether the value changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool MultiLine(Utf8LabelHandler label, ref string text, Vector2 size = default,
            InputTextFlags flags = InputTextFlags.None)
        {
            text.AsSpan().CopyInto<TextStringHandlerBuffer>(out _);
            var ret = Native.Methods.Inputs.InputTextMultiline(label.Start(), TextStringHandlerBuffer.Buffer,
                (ulong)TextStringHandlerBuffer.Size,
                size, flags, null, null);

            if (Item.Edited)
                text = Encoding.UTF8.GetString(new ReadOnlySpan<byte>(TextStringHandlerBuffer.Buffer,
                    Context.Pointer->InputTextState.CurrentLengthA));
            return ret;
        }

        /// <summary> Draw a text input for numerical values, with optional +/- buttons. </summary>
        /// <typeparam name="T"> The type of the numeric value to change. </typeparam>
        /// <param name="label"> The input label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="value"> The numeric input/output value. </param>
        /// <param name="format"> The printf format-string to display and parse the number in as text. If this is a UTF8 string, it HAS to be null-terminated </param>
        /// <param name="step"> The step when clicking the +/- buttons. If this is 0, the buttons will not be displayed. </param>
        /// <param name="stepFast"> The step when holding the +/- buttons for a while. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <returns> Whether the value changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Scalar<T>(Utf8LabelHandler label, ref T value, Utf8HintHandler format, T step = default, T stepFast = default,
            InputTextFlags flags = InputTextFlags.None) where T : unmanaged, INumber<T>
            => Native.Methods.Inputs.InputScalar(label.Start(), DataTypeExtensions.From<T>(), Unsafe.AsPointer(ref value),
                step.Equals(default) ? null : &step, &stepFast, format.Start(), flags);

        /// <summary> Draw a text input for numerical values, with optional +/- buttons, using the default format for that type. </summary>
        /// <inheritdoc cref="Scalar{T}(Utf8LabelHandler,ref T,Utf8HintHandler,T,T,InputTextFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Scalar<T>(Utf8LabelHandler label, ref T value, T step = default, T stepFast = default,
            InputTextFlags flags = InputTextFlags.None) where T : unmanaged, INumber<T>
            => Native.Methods.Inputs.InputScalar(label.Start(), DataTypeExtensions.From<T>(), Unsafe.AsPointer(ref value),
                step.Equals(default) ? null : &step, &stepFast, DefaultInputFormat<T>().Start(), flags);

        /// <summary> Draw a group of text inputs for multiple numerical values, with optional +/- buttons. </summary>
        /// <typeparam name="T"> The type of the numeric value to change. </typeparam>
        /// <param name="label"> The input label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="values"> The numeric input/output values. </param>
        /// <param name="format"> The printf format-string to display and parse the number in as text. If this is a UTF8 string, it HAS to be null-terminated </param>
        /// <param name="step"> The step when clicking the +/- buttons. If this is 0, the buttons will not be displayed. </param>
        /// <param name="stepFast"> The step when holding the +/- buttons for a while. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <returns> Whether the value changed in this frame. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool ScalarN<T>(Utf8LabelHandler label, Span<T> values, Utf8HintHandler format, T step = default, T stepFast = default,
            InputTextFlags flags = InputTextFlags.None) where T : unmanaged, INumber<T>
            => Native.Methods.Inputs.InputScalarN(label.Start(), DataTypeExtensions.From<T>(),
                (T*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(values)), values.Length, step.Equals(default) ? null : &step, &stepFast,
                format.Start(), flags);

        /// <summary> Draw a group of text inputs for multiple numerical values, with optional +/- buttons, using the default format for that type. </summary>
        /// <inheritdoc cref="ScalarN{T}(Utf8LabelHandler,Span{T},Utf8HintHandler,T,T,InputTextFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool ScalarN<T>(Utf8LabelHandler label, Span<T> values, T step = default, T stepFast = default,
            InputTextFlags flags = InputTextFlags.None) where T : unmanaged, INumber<T>
            => Native.Methods.Inputs.InputScalarN(label.Start(), DataTypeExtensions.From<T>(),
                (T*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(values)), values.Length, step.Equals(default) ? null : &step, &stepFast,
                DefaultInputFormat<T>().Start(), flags);

        /// <summary> Return the default input format string for a given type. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal static ReadOnlySpan<byte> DefaultInputFormat<T>()
        {
            if (typeof(T) == typeof(byte))
                return "%u"u8;

            if (typeof(T) == typeof(ushort))
                return "%u"u8;

            if (typeof(T) == typeof(uint))
                return "%u"u8;

            if (typeof(T) == typeof(ulong))
                return "%llu"u8;

            if (typeof(T) == typeof(sbyte))
                return "%d"u8;

            if (typeof(T) == typeof(short))
                return "%d"u8;

            if (typeof(T) == typeof(int))
                return "%d"u8;

            if (typeof(T) == typeof(long))
                return "%lld"u8;

            if (typeof(T) == typeof(float))
                return "%.6f"u8;

            if (typeof(T) == typeof(double))
                return "%.6f"u8;

            if (typeof(T) == typeof(nint))
                return "0x%llx"u8;

            if (typeof(T) == typeof(nuint))
                return "0x%llx"u8;

            return [];
        }

        /// <summary> Handle the hint and text length callback. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal static bool Text(byte* label, byte* buffer, uint bufferLength, byte* hint, InputTextFlags flags)
            => *hint is 0
                ? Native.Methods.Inputs.InputText(label, buffer, bufferLength, flags, null, null)
                : Native.Methods.Inputs.InputTextWithHint(label, hint, buffer, bufferLength, flags, null, null);
    }
}
