namespace ImSharp;

/// <summary> A status icon that also provides button-functionality. </summary>
/// <typeparam name="TIcon"> The type of the icons to draw. </typeparam>
/// <typeparam name="TData"> Additional data passed to the icon methods to control the drawing. </typeparam>
public interface IStatusIcon<out TIcon, TData>
{
    /// <summary> Get the icon to draw based on the passed data. </summary>
    /// <param name="data"> The passed data. </param>
    /// <returns> The icon to draw. </returns>
    public TIcon Icon(in TData data);

    /// <summary> The method to invoke on the passed data when the drawn icon is clicked. </summary>
    /// <param name="data"> The passed data. </param>
    public void OnClick(in TData data);

    /// <summary> An optional, additional label to push before the icon is drawn to make its ID unique. If this is empty, it is ignored. </summary>
    /// <param name="data"> The passed data. </param>
    /// <returns> The label to push as an ID. </returns>
    public ReadOnlySpan<byte> Label(in TData data)
        => ReadOnlySpan<byte>.Empty;

    /// <summary> Get whether the icon should be drawn at all based on the passed data. </summary>
    /// <param name="data"> The passed data. </param>
    /// <returns> True if the icon should be drawn, false otherwise. </returns>
    public bool Visible(in TData data)
        => true;

    /// <summary> Get the color of the icon when active, i.e. clicked on or held. </summary>
    /// <param name="data"> The passed data. </param>
    /// <returns> The color. </returns>
    public Vector4 ActiveColor(in TData data)
        => Im.Style[ImGuiColor.Text];

    /// <summary> Get the color of the icon when hovered by the mouse. </summary>
    /// <param name="data"> The passed data. </param>
    /// <returns> The color. </returns>
    public Vector4 HoveredColor(in TData data)
        => Im.Style[ImGuiColor.ButtonHovered];

    /// <summary> Get the color of the icon when not hovered nor active. </summary>
    /// <param name="data"> The passed data. </param>
    /// <returns> The color. </returns>
    public Vector4 Color(in TData data)
        => Im.Style[ImGuiColor.TextDisabled];

    /// <summary> Get whether the icon should draw a tooltip when hovered. </summary>
    /// <param name="data"> The passed data. </param>
    public bool HasTooltip(in TData data)
        => false;

    /// <summary> Draw the content of the tooltip for this icon. </summary>
    /// <param name="data"> The passed data. </param>
    /// <remarks> This is only invoked when <see cref="HasTooltip"/> returns true. It should not begin a tooltip itself. </remarks>
    public void DrawTooltip(in TData data)
    { }
}

