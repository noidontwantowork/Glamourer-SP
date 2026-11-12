namespace ImSharp;

/// <summary> A wrapper for color parameters that can be left optional to use style color instead. </summary>
/// <param name="Color"> The color value or null when using style colors. </param>
public readonly record struct ColorParameter(Rgba32? Color = null)
{
    /// <summary> Use the style's color value for this. </summary>
    public static readonly ColorParameter Default = default;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ColorParameter(uint? color)
        => new(color);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ColorParameter(Rgba32 color)
        => new(color);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ColorParameter(Rgba32? color)
        => new(color);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ColorParameter(Vector4 color)
        => new(color);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ColorParameter(ImGuiColor color)
        => new(Im.Color.Get(color));

    /// <summary> Check whether this color should use the style's color instead. </summary>
    public bool IsDefault
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => !Color.HasValue;
    }

    /// <summary> Check whether this color is not defaulted and not fully transparent. </summary>
    public bool IsVisible
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Color is { IsTransparent: false };
    }

    /// <summary> Get this color or the appropriate color from the style if this is left default. </summary>
    /// <param name="color"> The style color to fetch. </param>
    /// <returns> The correct color. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public Rgba32 CheckDefault(ImGuiColor color)
        => Color ?? Im.Color.Get(color);

    /// <summary> Get this color or the custom default color. </summary>
    /// <param name="color"> The custom default color. </param>
    /// <returns> The correct color. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public Rgba32 CheckDefault(Rgba32 color)
        => Color ?? color;

    /// <inheritdoc/>
    public override string ToString()
        => Color?.ToString() ?? "Default";
}
