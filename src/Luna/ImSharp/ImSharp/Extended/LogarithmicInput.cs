namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Draw a numerical input where the buttons half or double the value respectively. </summary>
    /// <typeparam name="T"> The type of number. </typeparam>
    /// <param name="label"> The label of the input as text. Does not have to be null-terminated. </param>
    /// <param name="displayFormat"> The format to display the number in the (read-only) display frame. </param>
    /// <param name="input"> The input value. </param>
    /// <param name="outputValue"> The output value. </param>
    /// <param name="lowerLimit"> The minimum supported value. If this is not default, the set-to-zero button is removed. </param>
    /// <returns> True if the value changed this frame. </returns>
    public static bool LogarithmicInput<T>(Utf8LabelHandler label, Utf8HintHandler displayFormat, T input, out T outputValue, T lowerLimit = default)
        where T : unmanaged, INumber<T>
    {
        using var id    = Im.Id.Push(label);
        using var group = Im.Group();
        Im.Input.Scalar("##i"u8, ref input, displayFormat, T.Zero, T.Zero, InputTextFlags.ReadOnly);

        outputValue = input;
        var (isOne, isZero, isLimit) = T.MultiplicativeIdentity.Equals(input)
            ? (true, false, lowerLimit.Equals(input))
            : (false, T.Zero.Equals(input), lowerLimit.Equals(input));
        var size = new Vector2(Im.Style.FrameHeight);
        var ret  = false;
        Im.Line.SameInner();
        if (Button("-"u8, size, isZero || isLimit ? StringU8.Empty : isOne ? "将值设置为0。"u8 : "将当前值减半。"u8, isZero || isLimit))
        {
            outputValue = isOne ? T.AdditiveIdentity : input / (T.MultiplicativeIdentity + T.MultiplicativeIdentity);
            ret         = true;
        }

        Im.Line.SameInner();
        if (Button("+"u8, size, isZero ? "将值设置为1."u8 : "将当前值加倍."u8))
        {
            outputValue = isZero ? T.MultiplicativeIdentity : input + input;
            ret         = true;
        }

        if (lowerLimit.Equals(default))
        {
            Im.Line.SameInner();
            if (Button("0"u8, size, isZero ? StringU8.Empty : "将值设置为0."u8, isZero))
            {
                outputValue = T.AdditiveIdentity;
                ret         = true;
            }
        }

        TextLabel(ref label);

        return ret;
    }

    /// <inheritdoc cref="LogarithmicInput{T}(Utf8LabelHandler,Utf8HintHandler,T,out T,T)"/>
    public static bool LogarithmicInput<T>(Utf8LabelHandler label, T input, out T scalar, T lowerLimit)
        where T : unmanaged, INumber<T>
        => LogarithmicInput(label, $"{input}", input, out scalar, lowerLimit);

    /// <summary> Draw a numerical input where the buttons half or double the value respectively. </summary>
    /// <typeparam name="T"> The type of number. </typeparam>
    /// <param name="label"> The label of the input as text. Does not have to be null-terminated. </param>
    /// <param name="displayFormat"> The format to display the number in the (read-only) display frame. </param>
    /// <param name="value"> The input/output value. </param>
    /// <param name="lowerLimit"> The minimum supported value. If this is not default, the set-to-zero button is removed. </param>
    /// <returns> True if the value changed this frame. </returns>
    public static bool LogarithmicInput<T>(Utf8LabelHandler label, Utf8HintHandler displayFormat, ref T value, T lowerLimit = default)
        where T : unmanaged, INumber<T>
    {
        if (!LogarithmicInput(label, displayFormat, value, out var tmp, lowerLimit))
            return false;

        value = tmp;
        return true;
    }

    /// <inheritdoc cref="LogarithmicInput{T}(Utf8LabelHandler,Utf8HintHandler,ref T, T)"/>
    public static bool LogarithmicInput<T>(Utf8LabelHandler label, ref T value, T lowerLimit = default)
        where T : unmanaged, INumber<T>
        => LogarithmicInput(label, $"{value}", ref value, lowerLimit);
}
