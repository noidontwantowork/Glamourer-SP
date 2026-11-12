namespace ImSharp;

public static partial class ImEx
{
    /// <summary> A wrapper class containing input functions that only return on deactivation. </summary>
    public static class InputOnDeactivation
    {
        /// <summary> A text input that only returns true when the item was deactivated this frame and edited, not on every edit. </summary>
        /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="input"> The input string as text. Does not have to be null-terminated. </param>
        /// <param name="output"> When returning true, the resulting string output encoded in UTF8, otherwise undefined.  </param>
        /// <param name="hint"> An optional hint to display in the input box while it is empty. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <param name="maxLength"> The maximum length of the string. </param>
        /// <returns> True when the control has been active, edited and was deactivated this frame. False otherwise. </returns>
        /// <remarks> Changing <paramref name="input"/> across frames while this is active will not have an effect. </remarks>
        public static bool Text(Utf8LabelHandler label, Utf8TextHandler input, out StringU8 output, Utf8HintHandler hint = default,
            InputTextFlags flags = InputTextFlags.None, uint maxLength = uint.MaxValue)
        {
            if (Text(ref label, ref input, out var length, ref hint, flags, maxLength))
            {
                if (!InputStringHandlerBuffer.FrameStorageString.IsNull)
                    output = InputStringHandlerBuffer.FrameStorageString;
                else
                    output = length is 0 ? StringU8.Empty : new StringU8(InputStringHandlerBuffer.Span[..length], false);
                return true;
            }

            output = StringU8.Empty;
            return false;
        }

        /// <summary> A text input that only returns true when the item was deactivated this frame and edited, not on every edit. </summary>
        /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="input"> The input string as text. Does not have to be null-terminated. </param>
        /// <param name="output"> When returning true, the resulting string output encoded in UTF16, otherwise undefined.  </param>
        /// <param name="hint"> An optional hint to display in the input box while it is empty. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <param name="maxLength"> The maximum length of the string. </param>
        /// <returns> True when the control has been active, edited and was deactivated this frame. False otherwise. </returns>
        /// <remarks> Changing <paramref name="input"/> across frames while this is active will not have an effect. </remarks>
        public static bool Text(Utf8LabelHandler label, Utf8TextHandler input, out string output, Utf8HintHandler hint = default,
            InputTextFlags flags = InputTextFlags.None, uint maxLength = uint.MaxValue)
        {
            if (Text(ref label, ref input, out var length, ref hint, flags, maxLength))
            {
                if (!InputStringHandlerBuffer.FrameStorageString.IsNull)
                    output = InputStringHandlerBuffer.FrameStorageString.ToString();
                else
                    output = length is 0 ? string.Empty : Encoding.UTF8.GetString(InputStringHandlerBuffer.Span[..length]);
                return true;
            }

            output = string.Empty;
            return false;
        }

        /// <summary> A multi-line text input that only returns true when the item was deactivated this frame and edited, not on every edit. </summary>
        /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="input"> The input string as text. Does not have to be null-terminated. </param>
        /// <param name="newLength"> The new length of the resulting string that is stored within <seealso cref="InputStringHandlerBuffer"/> while this is active up until another text input activates. </param>
        /// <param name="size"> The size of the multi-line input widget in pixels. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <returns> True when the control has been active, edited and was deactivated this frame. False otherwise. </returns>
        /// <remarks> Changing <paramref name="input"/> across frames while this is active will not have an effect. </remarks>
        public static bool MultiLine(Utf8LabelHandler label, Utf8TextHandler input, out int newLength, Vector2 size,
            InputTextFlags flags = InputTextFlags.None)
            => MultiLine(ref label, ref input, out newLength, size, flags);

        /// <summary> A multi-line text input that only returns true when the item was deactivated this frame and edited, not on every edit. </summary>
        /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="input"> The input string as text. Does not have to be null-terminated. </param>
        /// <param name="output"> When returning true, the resulting string output encoded in UTF8, otherwise undefined.  </param>
        /// <param name="size"> The size of the multi-line input widget in pixels. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <returns> True when the control has been active, edited and was deactivated this frame. False otherwise. </returns>
        /// <remarks> Changing <paramref name="input"/> across frames while this is active will not have an effect. </remarks>
        public static bool MultiLine(Utf8LabelHandler label, Utf8TextHandler input, out StringU8 output, Vector2 size,
            InputTextFlags flags = InputTextFlags.None)
        {
            if (MultiLine(ref label, ref input, out var length, size, flags))
            {
                if (!InputStringHandlerBuffer.FrameStorageString.IsNull)
                    output = InputStringHandlerBuffer.FrameStorageString;
                else
                    output = length is 0 ? StringU8.Empty : new StringU8(InputStringHandlerBuffer.Span[..length], false);
                return true;
            }

            output = StringU8.Empty;
            return false;
        }

        /// <summary> A text input that only returns true when the item was deactivated this frame and edited, not on every edit. </summary>
        /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="input"> The input string as text. Does not have to be null-terminated. </param>
        /// <param name="output"> When returning true, the resulting string output encoded in UTF16, otherwise undefined.  </param>
        /// <param name="size"> The size of the multi-line input widget in pixels. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <returns> True when the control has been active, edited and was deactivated this frame. False otherwise. </returns>
        /// <remarks> Changing <paramref name="input"/> across frames while this is active will not have an effect. </remarks>
        public static bool MultiLine(Utf8LabelHandler label, Utf8TextHandler input, out string output, Vector2 size,
            InputTextFlags flags = InputTextFlags.None)
        {
            if (MultiLine(ref label, ref input, out var length, size, flags))
            {
                if (!InputStringHandlerBuffer.FrameStorageString.IsNull)
                    output = InputStringHandlerBuffer.FrameStorageString.ToString();
                else
                    output = length is 0 ? string.Empty : Encoding.UTF8.GetString(InputStringHandlerBuffer.Span[..length]);
                return true;
            }

            output = string.Empty;
            return false;
        }

        /// <summary> A scalar text input that only returns true when the item was deactivated this frame and edited, not on every edit. </summary>
        /// <typeparam name="T"> The scalar type for the input. </typeparam>
        /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="value"> The current value to display if not active. </param>
        /// <param name="newValue"> The returned value after deactivation. </param>
        /// <param name="format"> The printf format-string to display and parse the number in as a UTF8 string. HAS to be null-terminated. </param>
        /// <param name="step"> The step when clicking the +/- buttons. If this is 0, the buttons will not be displayed. </param>
        /// <param name="stepFast"> The step when holding the +/- buttons for a while. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <returns> True if the value changed and the widget was deactivated this frame. </returns>
        public static unsafe bool Scalar<T>(Utf8LabelHandler label, in T value, out T newValue, Utf8HintHandler format, T step = default,
            T stepFast = default,
            InputTextFlags flags = InputTextFlags.None) where T : unmanaged, INumber<T>
        {
            flags &= ~InputTextFlags.EnterReturnsTrue;
            var id = Im.Id.Get(ref label);
            if (id.Active)
            {
                var pointer = (T*)Unsafe.AsPointer(ref Storage<T>.Data);
                Im.Native.Methods.Inputs.InputScalar(label.Start(), DataTypeExtensions.From<T>(), pointer, step.Equals(default) ? null : &step,
                    &stepFast, format.Start(), flags);
            }
            else
            {
                var tmpValue = value;
                if (Im.Native.Methods.Inputs.InputScalar(label.Start(), DataTypeExtensions.From<T>(), &tmpValue,
                        step.Equals(default) ? null : &step,
                        &stepFast, format.Start(), flags)
                 || Im.Item.Activated)
                    Storage<T>.Data = tmpValue;
            }

            if (!Im.Item.DeactivatedAfterEdit)
            {
                newValue = value;
                return false;
            }

            newValue = Storage<T>.Data;
            return true;
        }

        /// <summary> A scalar text input that only returns true when the item was deactivated this frame and edited, not on every edit. </summary>
        /// <typeparam name="T"> The scalar type for the input. </typeparam>
        /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="value"> The current value to display if not active, and the output value after deactivation. </param>
        /// <param name="format"> The printf format-string to display and parse the number in as a UTF8 string. HAS to be null-terminated. </param>
        /// <param name="step"> The step when clicking the +/- buttons. If this is 0, the buttons will not be displayed. </param>
        /// <param name="stepFast"> The step when holding the +/- buttons for a while. </param>
        /// <param name="flags"> Additional flags controlling the input behavior. </param>
        /// <returns> True if the value changed and the widget was deactivated this frame. </returns>
        public static bool Scalar<T>(Utf8LabelHandler label, ref T value, Utf8HintHandler format, T step = default, T stepFast = default,
            InputTextFlags flags = InputTextFlags.None) where T : unmanaged, INumber<T>
        {
            if (!Scalar(label, value, out var newValue, format, step, stepFast, flags))
                return false;

            value = newValue;
            return true;
        }

        /// <inheritdoc cref="Scalar{T}(Utf8LabelHandler,in T,out T,T,T,InputTextFlags)"/>
        public static unsafe bool Scalar<T>(Utf8LabelHandler label, in T value, out T newValue, T step = default, T stepFast = default,
            InputTextFlags flags = InputTextFlags.None) where T : unmanaged, INumber<T>
        {
            flags &= ~InputTextFlags.EnterReturnsTrue;
            var id = Im.Id.Get(ref label);
            if (id.Active)
            {
                var pointer = (T*)Unsafe.AsPointer(ref Storage<T>.Data);
                Im.Native.Methods.Inputs.InputScalar(label.Start(), DataTypeExtensions.From<T>(), pointer, step.Equals(default) ? null : &step,
                    &stepFast, Im.Input.DefaultInputFormat<T>().Start(), flags);
            }
            else
            {
                var tmpValue = value;
                if (Im.Native.Methods.Inputs.InputScalar(label.Start(), DataTypeExtensions.From<T>(), &tmpValue,
                        step.Equals(default) ? null : &step,
                        &stepFast, Im.Input.DefaultInputFormat<T>().Start(), flags)
                 || Im.Item.Activated)
                    Storage<T>.Data = tmpValue;
            }

            if (!Im.Item.DeactivatedAfterEdit)
            {
                newValue = value;
                return false;
            }

            newValue = Storage<T>.Data;
            return true;
        }

        /// <inheritdoc cref="Scalar{T}(Utf8LabelHandler,ref T,T,T,InputTextFlags)"/>
        public static bool Scalar<T>(Utf8LabelHandler label, ref T value, T step = default, T stepFast = default,
            InputTextFlags flags = InputTextFlags.None) where T : unmanaged, INumber<T>
        {
            if (!Scalar(label, value, out var newValue, step, stepFast, flags))
                return false;

            value = newValue;
            return true;
        }

        /// <summary> A dragging scalar input that only returns true when the item was deactivated this frame and edited, not on every edit. </summary>
        /// <typeparam name="T"> The scalar type for the input. </typeparam>
        /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="value"> The current value to display if not active. </param>
        /// <param name="newValue"> The returned value after deactivation. </param>
        /// <param name="format"> The printf format-string to display and parse the number in as a UTF8 string. HAS to be null-terminated. </param>
        /// <param name="min"> The optional minimum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
        /// <param name="max"> The optional maximum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
        /// <param name="speed"> The dragging speed as the value increase per pixel of mouse movement. </param>
        /// <param name="flags"> Additional flags controlling the sliders behavior. </param>
        /// <returns> True if the value changed and the widget was deactivated this frame. </returns>
        public static bool Drag<T>(Utf8LabelHandler label, in T value, out T newValue, Utf8HintHandler format, T? min = null, T? max = null,
            float speed = 1, SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
        {
            var id = Im.Id.Get(ref label);
            if (id.Active)
            {
                Im.Drag(label, ref Storage<T>.Data, format, min, max, speed, flags);
            }
            else
            {
                var tmpValue = value;
                if (Im.Drag(label, ref tmpValue, format, min, max, speed, flags) || Im.Item.Activated)
                    Storage<T>.Data = tmpValue;
            }

            if (!Im.Item.DeactivatedAfterEdit)
            {
                newValue = value;
                return false;
            }

            newValue = Storage<T>.Data;
            return true;
        }

        /// <summary> A dragging scalar input that only returns true when the item was deactivated this frame and edited, not on every edit. </summary>
        /// <typeparam name="T"> The scalar type for the input. </typeparam>
        /// <param name="label"> The label as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="value"> The current value to display if not active. </param>
        /// <param name="format"> The printf format-string to display and parse the number in as a UTF8 string. HAS to be null-terminated. </param>
        /// <param name="min"> The optional minimum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
        /// <param name="max"> The optional maximum value for the scalar. This is only applied on manual input when the <seealso cref="SliderFlags.AlwaysClamp"/> flag is set. </param>
        /// <param name="speed"> The dragging speed as the value increase per pixel of mouse movement. </param>
        /// <param name="flags"> Additional flags controlling the sliders behavior. </param>
        /// <returns> True if the value changed and the widget was deactivated this frame. </returns>
        public static bool Drag<T>(Utf8LabelHandler label, ref T value, Utf8HintHandler format, T? min = null, T? max = null,
            float speed = 1, SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
        {
            if (!Drag(label, value, out var newValue, format, min, max, speed, flags))
                return false;

            value = newValue;
            return true;
        }

        /// <inheritdoc cref="Drag{T}(Utf8LabelHandler,in T,out T,Utf8HintHandler,T?,T?,float,SliderFlags)"/>
        public static bool Drag<T>(Utf8LabelHandler label, in T value, out T newValue, T? min = null, T? max = null,
            float speed = 1, SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
        {
            var id = Im.Id.Get(ref label);
            if (id.Active)
            {
                Im.Drag(label, ref Storage<T>.Data, min, max, speed, flags);
            }
            else
            {
                var tmpValue = value;
                if (Im.Drag(label, ref tmpValue, min, max, speed, flags) || Im.Item.Activated)
                    Storage<T>.Data = tmpValue;
            }

            if (!Im.Item.DeactivatedAfterEdit)
            {
                newValue = value;
                return false;
            }

            newValue = Storage<T>.Data;
            return true;
        }

        /// <inheritdoc cref="Drag{T}(Utf8LabelHandler,ref T,Utf8HintHandler,T?,T?,float,SliderFlags)"/>
        public static bool Drag<T>(Utf8LabelHandler label, ref T value, T? min = null, T? max = null,
            float speed = 1, SliderFlags flags = SliderFlags.None) where T : unmanaged, INumber<T>
        {
            if (!Drag(label, value, out var newValue, min, max, speed, flags))
                return false;

            value = newValue;
            return true;
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        private static unsafe bool Text(ref Utf8LabelHandler label, ref Utf8TextHandler input, out int newLength, ref Utf8HintHandler hint,
            InputTextFlags flags, uint maxLength)
        {
            var id   = Im.Id.Get(ref label);
            var span = InputStringHandlerBuffer.GetInputSpan(id, ref input);
            flags &= ~InputTextFlags.EnterReturnsTrue;
            if (maxLength < span.Length)
                span = span[..(int)maxLength];

            var currentLength  = Im.Context.Pointer->InputTextState.CurrentLengthA;
            var saveTempString = Im.Context.InputTextId != 0 && Im.Context.InputTextId != id;
            var buffer         = span.Start();
            var copyBuffer     = Im.Input.Text(label.Start(), buffer, (uint)span.Length, hint.Start(), flags) || Im.Item.Activated;
            return InputStringHandlerBuffer.ReturnActive(buffer, copyBuffer, saveTempString, currentLength, out newLength);
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        private static unsafe bool MultiLine(ref Utf8LabelHandler label, ref Utf8TextHandler input, out int newLength, Vector2 widgetSize,
            InputTextFlags flags)
        {
            var id   = Im.Id.Get(ref label);
            var span = InputStringHandlerBuffer.GetInputSpan(id, ref input);
            flags &= ~InputTextFlags.EnterReturnsTrue;

            var currentLength  = Im.Context.Pointer->InputTextState.CurrentLengthA;
            var saveTempString = Im.Context.InputTextId != 0 && Im.Context.InputTextId != id;
            var buffer         = span.Start();
            var copyBuffer =
                Im.Native.Methods.Inputs.InputTextMultiline(label.Start(), buffer, (uint)span.Length, widgetSize, flags, null, null)
             || Im.Item.Activated;
            return InputStringHandlerBuffer.ReturnActive(buffer, copyBuffer, saveTempString, currentLength, out newLength);
        }

        private static class Storage<T> where T : unmanaged, INumber<T>
        {
            public static T Data;
        }
    }
}
