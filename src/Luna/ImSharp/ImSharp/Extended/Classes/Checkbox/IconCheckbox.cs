namespace ImSharp;

/// <summary> A base class for a multi state checkbox displaying different icons. </summary>
public abstract class IconCheckbox<TValue, TIcon> : MultiStateCheckbox<TValue> where TIcon : unmanaged, IIconStandIn
{
    protected abstract (TIcon? Icon, Rgba32? Color) GetIcon(TValue value);

    protected override void RenderSymbol(TValue value, Vector2 position, float size)
    {
        var (maybeIcon, color) = GetIcon(value);
        if (!maybeIcon.HasValue)
            return;

        var icon = maybeIcon.Value.Span;

        // FIXME honor size parameter if possible
        using var font = Im.Font.Push(TIcon.Font);

        var iconSize     = Im.Font.CalculateSize(icon);
        var iconPosition = position + (new Vector2(size) - iconSize) * 0.5f;

        Im.Window.DrawList.Text(iconPosition, color ?? Im.Color.Get(ImGuiColor.CheckMark), icon);
    }
}

/// <summary> A two-state checkbox displaying an arbitrary icon instead of a checkmark. </summary>
internal sealed class IconCheckbox<TIcon>(TIcon icon = default, Rgba32? color = null) : IconCheckbox<bool, TIcon>
    where TIcon : unmanaged, IIconStandIn
{
    public static readonly IconCheckbox<TIcon> Instance = new();

    private TIcon   _icon  = icon;
    private Rgba32? _color = color;

    /// <summary> Draw a two-state checkbox displaying an arbitrary icon instead of a checkmark. </summary>
    /// <param name="label"> The label as text. Does not have to be null-terminated. </param>
    /// <param name="icon"> The icon to display for 'True'. </param>
    /// <param name="color"> The optional color to display the icon in. If null, <see cref="ImGuiColor.CheckMark"/> will be used. </param>
    /// <param name="value"> The input and output value of the checkbox. </param>
    /// <returns> True if the checkbox has been clicked in this frame, in which case <paramref name="value"/> will be flipped. </returns>
    public bool Draw(Utf8LabelHandler label, TIcon icon, Rgba32 color, ref bool value)
    {
        _icon  = icon;
        _color = color;
        return Draw(label, ref value);
    }

    /// <inheritdoc cref="Draw(Utf8LabelHandler,TIcon,Rgba32,ref bool)"/>
    public bool Draw(Utf8LabelHandler label, TIcon icon, ref bool value)
    {
        _icon  = icon;
        _color = null;
        return Draw(label, ref value);
    }

    protected override (TIcon? Icon, Rgba32? Color) GetIcon(bool value)
        => value ? (_icon, _color) : (null, null);

    protected override bool NextValue(bool value)
        => !value;

    protected override bool PreviousValue(bool value)
        => !value;
}

public static partial class ImEx
{
    /// <inheritdoc cref="IconCheckbox{TIcon}.Draw(Utf8LabelHandler,TIcon,ref bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool IconCheckbox<TIcon>(Utf8LabelHandler label, TIcon icon, ref bool value)
        where TIcon : unmanaged, IIconStandIn
        => ImSharp.IconCheckbox<TIcon>.Instance.Draw(label, icon, ref value);

    /// <inheritdoc cref="IconCheckbox{TIcon}.Draw(Utf8LabelHandler,TIcon,Rgba32,ref bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool IconCheckbox<TIcon>(Utf8LabelHandler label, TIcon icon, Rgba32 color, ref bool value)
        where TIcon : unmanaged, IIconStandIn
        => ImSharp.IconCheckbox<TIcon>.Instance.Draw(label, icon, color, ref value);

    /// <inheritdoc cref="IconCheckbox{TIcon}.Draw(Utf8LabelHandler,TIcon,ref bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool IconCheckbox<TIcon>(Utf8LabelHandler label, TIcon icon, bool value, out bool newValue)
        where TIcon : unmanaged, IIconStandIn
    {
        newValue = value;
        return ImSharp.IconCheckbox<TIcon>.Instance.Draw(label, icon, ref newValue);
    }

    /// <inheritdoc cref="IconCheckbox{TIcon}.Draw(Utf8LabelHandler,TIcon,Rgba32,ref bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool IconCheckbox<TIcon>(Utf8LabelHandler label, TIcon icon, Rgba32 color, bool value, out bool newValue)
        where TIcon : unmanaged, IIconStandIn
    {
        newValue = value;
        return ImSharp.IconCheckbox<TIcon>.Instance.Draw(label, icon, color, ref newValue);
    }
}
