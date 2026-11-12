#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace ImSharp;

/// <summary> A two-state Checkbox that displays either a checkmark for True values or an X for False values, with no empty state. </summary>
internal sealed class TriStateCheckbox : MultiStateCheckbox<bool?>
{
    /// <summary> A static instance to draw more easily. </summary>
    public static readonly TriStateCheckbox Instance = new();

    /// <summary> A static instance used by the colored variant to draw more easily. </summary>
    public static readonly TriStateCheckbox ColoredInstance = new();

    /// <summary> The color for the neutral state dot. If left default, <see cref="ImGuiColor.CheckMark"/> is used. </summary>
    public ColorParameter Neutral = ColorParameter.Default;

    /// <summary> The color for the on state checkmark. If left default, <see cref="ImGuiColor.CheckMark"/> is used. </summary>
    public ColorParameter On = ColorParameter.Default;

    /// <summary> The color for the off state cross. If left default, <see cref="ImGuiColor.CheckMark"/> is used. </summary>
    public ColorParameter Off = ColorParameter.Default;

    /// <inheritdoc/>
    protected override void RenderSymbol(bool? value, Vector2 position, float size)
    {
        switch (value)
        {
            case null:  Im.Render.Dot(Im.Window.DrawList, position, Neutral.CheckDefault(ImGuiColor.CheckMark), size); break;
            case true:  Im.Render.Checkmark(Im.Window.DrawList, position, On.CheckDefault(ImGuiColor.CheckMark), size); break;
            case false: Im.Render.Cross(Im.Window.DrawList, position, Off.CheckDefault(ImGuiColor.CheckMark), size); break;
        }
    }

    /// <summary> Draw the tri-state checkbox. </summary>
    /// <param name="label"> The label for the checkbox as text. Does not have to be null-terminated. </param>
    /// <param name="value"> The input/output value. </param>
    /// <param name="onFlag"> The flag representing the 'Only Enabled' state. </param>
    /// <param name="offFlag"> The flag representing the 'Only Disabled' state. </param>
    /// <returns> True when <paramref name="value"/> changed in this frame. </returns>
    /// <remarks> Neither flag being on is treated the same as both being on. </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Draw<T>(Utf8LabelHandler label, ref T value, T onFlag, T offFlag) where T : unmanaged, Enum
    {
        bool? converted = value.HasFlag(onFlag)
            ? value.HasFlag(offFlag)
                ? null
                : true
            : value.HasFlag(offFlag)
                ? false
                : null;
        if (!Draw(label, ref converted))
            return false;

        MergeEnum(ref value, onFlag, offFlag, converted);
        return true;
    }

    /// <inheritdoc/>
    protected override bool? NextValue(bool? value)
        => value switch
        {
            null  => true,
            true  => false,
            false => null,
        };

    /// <inheritdoc/>
    protected override bool? PreviousValue(bool? value)
        => value switch
        {
            null  => false,
            true  => null,
            false => true,
        };

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static unsafe void MergeEnum<T>(ref T ret, T onFlag, T offFlag, bool? value) where T : unmanaged, Enum
    {
        var (uVal, uOn, uOff) = sizeof(T) switch
        {
            1 => (*(byte*)Unsafe.AsPointer(ref ret), *(byte*)&onFlag, *(byte*)&offFlag),
            2 => (*(ushort*)Unsafe.AsPointer(ref ret), *(ushort*)&onFlag, *(ushort*)&offFlag),
            4 => (*(uint*)Unsafe.AsPointer(ref ret), *(uint*)&onFlag, *(uint*)&offFlag),
            8 => (*(ulong*)Unsafe.AsPointer(ref ret), *(uint*)&onFlag, *(uint*)&offFlag),
            _ => throw new ArgumentException($"Enum type {typeof(T)} has size {sizeof(T)} > 8, which is not supported for flag checkboxes."),
        };

        uVal = value switch
        {
            null  => uVal | uOn | uOff,
            true  => (uVal | uOn) & ~uOff,
            false => (uVal | uOff) & ~uOn,
        };

        ret = *(T*)&uVal;
    }
}

public static partial class ImEx
{
    /// <summary> Draw a checkbox that displays a checkmark for true, an X for false and a dot for null. </summary>
    /// <param name="label"> The label as text. Does not have to be null-terminated. </param>
    /// <param name="value"> The input and output value of the checkbox. </param>
    /// <returns> True if the checkbox has been clicked in this frame, in which case <paramref name="value"/> will be changed. </returns>
    public static bool TriStateCheckbox(Utf8LabelHandler label, ref bool? value)
        => ImSharp.TriStateCheckbox.Instance.Draw(label, ref value);

    /// <inheritdoc cref="TriStateCheckbox.Draw{T}(Utf8LabelHandler,ref T,T,T)"/>
    public static bool TriStateCheckbox<T>(Utf8LabelHandler label, ref T value, T onFlag, T offFlag) where T : unmanaged, Enum
        => ImSharp.TriStateCheckbox.Instance.Draw(label, ref value, onFlag, offFlag);

    /// <inheritdoc cref="TriStateCheckbox.Draw{T}(Utf8LabelHandler,ref T,T,T)"/>
    /// <param name="neutral"> The color for the neutral state dot. If left default, <see cref="ImGuiColor.CheckMark"/> is used. </param>
    /// <param name="on"> The color for the on state checkmark. If left default, <see cref="ImGuiColor.CheckMark"/> is used. </param>
    /// <param name="off"> The color for the off state cross. If left default, <see cref="ImGuiColor.CheckMark"/> is used. </param>
    public static bool TriStateCheckbox<T>(Utf8LabelHandler label, ref T value, T onFlag, T offFlag, ColorParameter neutral, ColorParameter on,
        ColorParameter off) where T : unmanaged, Enum
    {
        ImSharp.TriStateCheckbox.ColoredInstance.Neutral = neutral;
        ImSharp.TriStateCheckbox.ColoredInstance.On      = on;
        ImSharp.TriStateCheckbox.ColoredInstance.Off     = off;
        return ImSharp.TriStateCheckbox.ColoredInstance.Draw(label, ref value, onFlag, offFlag);
    }

    /// <inheritdoc cref="TriStateCheckbox(Utf8LabelHandler,ref bool?)"/>
    /// <param name="neutral"> The color for the neutral state dot. If left default, <see cref="ImGuiColor.CheckMark"/> is used. </param>
    /// <param name="on"> The color for the on state checkmark. If left default, <see cref="ImGuiColor.CheckMark"/> is used. </param>
    /// <param name="off"> The color for the off state cross. If left default, <see cref="ImGuiColor.CheckMark"/> is used. </param>
    public static bool TriStateCheckbox(Utf8LabelHandler label, ref bool? value, ColorParameter neutral, ColorParameter on, ColorParameter off)
    {
        ImSharp.TriStateCheckbox.ColoredInstance.Neutral = neutral;
        ImSharp.TriStateCheckbox.ColoredInstance.On      = on;
        ImSharp.TriStateCheckbox.ColoredInstance.Off     = off;
        return ImSharp.TriStateCheckbox.ColoredInstance.Draw(label, ref value);
    }
}
