using System.Buffers.Binary;

namespace ImSharp;

/// <summary> A wrapper for RGBA32 colors as used in ImGui. </summary>
/// <param name="Color"> The color as a RGBA32 4-byte integer. </param>
public readonly record struct Rgba32(uint Color) : ISpanFormattable, IUtf8SpanFormattable
{
    /// <summary> A fully transparent color. </summary>
    public static readonly Rgba32 Transparent = new(0x00000000);

    /// <summary> A pure black color. </summary>
    public static readonly Rgba32 Black = new(0xFF000000);

    /// <summary> A pure white color. </summary>
    public static readonly Rgba32 White = new(0xFFFFFFFF);

    /// <summary> A pure red color. </summary>
    public static readonly Rgba32 Red = new(0xFF0000FF);

    /// <summary> A pure green color. </summary>
    public static readonly Rgba32 Green = new(0xFF00FF00);

    /// <summary> A pure blue color. </summary>
    public static readonly Rgba32 Blue = new(0xFFFF0000);

    /// <summary> A pure cyan color. </summary>
    public static readonly Rgba32 Cyan = new(0xFFFFFF00);

    /// <summary> A pure magenta color. </summary>
    public static readonly Rgba32 Magenta = new(0xFFFF00FF);

    /// <summary> A pure yellow color. </summary>
    public static readonly Rgba32 Yellow = new(0xFF00FFFF);

    /// <summary> A pure gray color. </summary>
    public static readonly Rgba32 Gray = new(0xFF808080);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Rgba32(uint color)
        => new(color);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Rgba32(Vector4 color)
        => new(color);

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator Rgba32(Vector3 color)
        => new(new Vector4(color, 1));

    /// <summary> Convert the 4-float color into a RGBA32 color normalized to byte values for a color channel. </summary>
    /// <remarks> Values outside [0, 1] will be clamped to 0 and <seealso cref="byte.MaxValue"/> respectively. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public Rgba32(Vector4 color)
        : this(Im.Native.Methods.Color.ColorConvertFloat4ToU32(color).Color)
    { }

    /// <summary> Convert the 3-float color into a RGBA32 color normalized to byte values for a color channel, with full alpha. </summary>
    /// <remarks> Values outside [0, 1] will be clamped to 0 and <seealso cref="byte.MaxValue"/> respectively. </remarks>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public Rgba32(Vector3 color)
        : this(Im.Native.Methods.Color.ColorConvertFloat4ToU32(new ImVec4(color.X, color.Y, color.Z, 1f)).Color)
    { }

    /// <summary> Convert byte values for colors into a single RGBA32 color. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    [OverloadResolutionPriority(100)]
    public Rgba32(byte r, byte g, byte b, byte a = 0xFF)
        : this(r | ((uint)g << 8) | ((uint)b << 16) | ((uint)a << 24))
    { }

    /// <summary> Convert byte values for colors into a single RGBA32 color. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    [OverloadResolutionPriority(50)]
    public Rgba32(float r, float g, float b, float a = 1f)
        : this(new Vector4(r, g, b, a))
    { }

    /// <summary> The red-channel byte. </summary>
    public byte R
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => (byte)Color;
    }

    /// <summary> The green-channel byte. </summary>
    public byte G
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => (byte)(Color >> 8);
    }

    /// <summary> The blue-channel byte. </summary>
    public byte B
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => (byte)(Color >> 16);
    }

    /// <summary> The alpha-channel byte. </summary>
    public byte A
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => (byte)(Color >> 24);
    }

    /// <summary> Whether the color is fully transparent, i.e. the alpha-channel is 0. </summary>
    public bool IsTransparent
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Color <= 0x00FFFFFF;
    }

    /// <summary> Whether the color is fully opaque, i.e. the alpha-channel is 0xFF. </summary>
    public bool IsOpaque
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Color >= 0xFF000000;
    }

    /// <summary> Whether the color is not fully transparent, i.e. the alpha-channel is not 0. </summary>
    public bool IsVisible
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Color > 0x00FFFFFF;
    }

    /// <summary> Convert the RGBA32 color into a 4-float color. </summary>
    /// <returns> A <seealso cref="Vector4"/> with each channel normalized to [0, 1]. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public Vector4 ToVector()
        => Im.Native.Methods.Color.ColorConvertU32ToFloat4(this);

    /// <summary> Write to string as #RRGGBBAA. </summary>
    public override string ToString()
        => $"#{BinaryPrimitives.ReverseEndianness(Color):X8}";

    /// <summary> Write to string as #RRGGBBAA. </summary>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        FormattableString formattable = $"";
        return formattable.ToString(formatProvider);
    }

    /// <summary> Get this color with a full alpha channel. </summary>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public Rgba32 FullAlpha()
        => new(Color | 0xFF000000);

    /// <summary> Get this color with halved alpha. </summary>
    [MethodImpl(ImSharpConfiguration.Inl)]
    public Rgba32 HalfTransparent()
        => (Color & 0x00FFFFFFu) | ((Color & 0xFE000000u) >> 1);

    /// <summary> Get this color with specified alpha in [0, 1]. </summary>
    [MethodImpl(ImSharpConfiguration.Inl)]
    [OverloadResolutionPriority(0)]
    public Rgba32 WithAlpha(float alpha)
        => (Color & 0x00FFFFFFu) | (uint)((byte)(Math.Clamp(alpha, 0, 1) * 0xFF) << 24);

    /// <summary> Get this color with specified alpha in [0, 1]. </summary>
    [MethodImpl(ImSharpConfiguration.Inl)]
    [OverloadResolutionPriority(100)]
    public Rgba32 WithAlpha(byte alpha)
        => (Color & 0x00FFFFFFu) | ((uint)alpha << 24);

    /// <summary> Obtain an approximation of the intensity of a color without taking into consideration the alpha value. </summary>
    /// <param name="color"> The color. </param>
    /// <returns> The approximated intensity. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static float Intensity(in Vector4 color)
        => 2 * color.X * color.X + 7 * color.Y * color.Y + color.Z * color.Z;

    /// <summary> Obtain an approximation of the intensity of this color without taking into consideration the alpha value. </summary>
    /// <returns> The approximated intensity. </returns>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public float Intensity()
        => Intensity(ToVector());

    /// <summary> Obtain the better choice of black and white for a given color regarding contrast. </summary>
    /// <param name="color"> The color to contrast. </param>
    /// <returns> Black if the intensity of the given color is high enough, white otherwise. </returns>
    public static Vector4 ContrastColor(Vector4 color)
        => Intensity(color) >= 4 ? new Vector4(0, 0, 0, color.W) : new Vector4(1, 1, 1, color.W);

    /// <summary> Obtain the better choice of black and white for this color regarding contrast. </summary>
    /// <returns> Black if the intensity of this color is high enough, white otherwise. </returns>
    public Rgba32 ContrastColor()
        => Intensity() >= 4 ? new Rgba32(Color & 0xFF000000u) : new Rgba32(Color | 0x00FFFFFFu);

    /// <summary> Mix this color with another at the mid-point per color channel, but with full alpha. </summary>
    /// <param name="other"> The other color to mix in. </param>
    /// <returns> The mixed color with no transparency. </returns>
    public Rgba32 Mix(Rgba32 other)
    {
        var r = ((Color & 0xFF) + (other.Color & 0xFF)) / 2;
        var g = (((Color >> 8) & 0xFF) + ((other.Color >> 8) & 0xFF)) / 2;
        var b = (((Color >> 16) & 0xFF) + ((other.Color >> 16) & 0xFF)) / 2;
        return r | (g << 8) | (b << 16) | 0xFF000000u;
    }

    /// <summary> Blends this color with the given primary color. </summary>
    /// <param name="overlayColor"> The blend color, can only be fully saturated <see cref="Black"/>, <see cref="White"/>, <see cref="Red"/>, <see cref="Green"/>, <see cref="Blue"/>, <see cref="Cyan"/>, <see cref="Magenta"/>, or <see cref="Yellow"/>. </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rgba32 HalfBlend(Rgba32 overlayColor)
        => (Color & 0xFF000000u) | ((Color & 0x00FEFEFEu) >> 1) | (overlayColor.Color & 0x00808080u);

    /// <summary> Return this color tinted by the given tint color. </summary>
    /// <param name="tint"> The tint color. </param>
    /// <returns> The tinted color. </returns>
    [OverloadResolutionPriority(100)]
    public Rgba32 Tinted(in Vector4 tint)
        => TintColor(ToVector(), tint);

    /// <inheritdoc cref="Tinted(in Vector4)"/>
    [OverloadResolutionPriority(50)]
    public Rgba32 Tinted(Rgba32 tint)
        => TintColor(ToVector(), tint.ToVector());

    /// <summary> Tint the first color with the second color according to the second color's alpha channel. </summary>
    /// <param name="color"> The base color. </param>
    /// <param name="tint"> The tint color. </param>
    /// <returns> The tinted color. </returns>
    public static Vector4 TintColor(in Vector4 color, in Vector4 tint)
    {
        var negAlpha = 1 - tint.W;
        var newAlpha = negAlpha * color.W + tint.W;
        var newR     = (negAlpha * color.W * color.X + tint.W * tint.X) / newAlpha;
        var newG     = (negAlpha * color.W * color.Y + tint.W * tint.Y) / newAlpha;
        var newB     = (negAlpha * color.W * color.Z + tint.W * tint.Z) / newAlpha;
        return new Vector4(newR, newG, newB, newAlpha);
    }

    /// <inheritdoc/>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        if (destination.Length < 9)
        {
            charsWritten = 0;
            return false;
        }

        charsWritten   = 9;
        destination[0] = '#';
        destination[1] = HexU16[(int)(Color >> 4) & 0xF];
        destination[2] = HexU16[(int)Color & 0xF];
        destination[3] = HexU16[(int)(Color >> 12) & 0xF];
        destination[4] = HexU16[(int)(Color >> 8) & 0xF];
        destination[5] = HexU16[(int)(Color >> 20) & 0xF];
        destination[6] = HexU16[(int)(Color >> 16) & 0xF];
        destination[7] = HexU16[(int)(Color >> 28)];
        destination[8] = HexU16[(int)(Color >> 24) & 0xF];
        return true;
    }

    /// <inheritdoc/>
    public bool TryFormat(Span<byte> destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        if (destination.Length < 9)
        {
            bytesWritten = 0;
            return false;
        }

        bytesWritten   = 9;
        destination[0] = (byte)'#';
        destination[1] = HexU8[(int)(Color >> 4) & 0xF];
        destination[2] = HexU8[(int)Color & 0xF];
        destination[3] = HexU8[(int)(Color >> 12) & 0xF];
        destination[4] = HexU8[(int)(Color >> 8) & 0xF];
        destination[5] = HexU8[(int)(Color >> 20) & 0xF];
        destination[6] = HexU8[(int)(Color >> 16) & 0xF];
        destination[7] = HexU8[(int)(Color >> 28)];
        destination[8] = HexU8[(int)(Color >> 24) & 0xF];
        return true;
    }

    /// <summary> Try to read a color value from a UTF8 string as formatted by our own output, i.e. #rrggbbaa. </summary>
    /// <param name="source"> The source span. </param>
    /// <param name="value"> The returned value on success. </param>
    /// <param name="allowNoAlpha"> Whether a shortened format #rrggbb is allowed for full alpha. </param>
    /// <returns> True on success. </returns>
    public static bool TryRead(ReadOnlySpan<byte> source, out Rgba32 value, bool allowNoAlpha = true)
    {
        if (source.Length is not 9 && (!allowNoAlpha || source.Length is not 7) || source[0] is not (byte)'#')
        {
            value = default;
            return false;
        }

        var ret = 0u;
        if (!Append(source[1], ref ret, 4)
         || !Append(source[2], ref ret, 0)
         || !Append(source[3], ref ret, 12)
         || !Append(source[4], ref ret, 8)
         || !Append(source[5], ref ret, 20)
         || !Append(source[6], ref ret, 16))
        {
            value = default;
            return false;
        }

        if (source.Length is 7)
        {
            value = ret | 0xFF000000;
            return true;
        }

        if (!Append(source[7], ref ret, 28)
         || !Append(source[8], ref ret, 24))
        {
            value = default;
            return false;
        }

        value = ret;
        return true;
    }


    private static ReadOnlySpan<char> HexU16
        => "0123456789ABCDEF";

    private static ReadOnlySpan<byte> HexU8
        => "0123456789ABCDEF"u8;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static bool Append(byte character, ref uint value, int shift)
    {
        if (!CheckCharacter(character, out var partialValue))
            return false;

        value |= partialValue << shift;
        return true;
    }

    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static bool CheckCharacter(byte character, out uint value)
    {
        if (character > 'f')
        {
            value = 0xFF;
            return false;
        }

        value = NumericalHex[character];
        return value is not 0xFF;
    }

    private static ReadOnlySpan<byte> NumericalHex
        =>
        [
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,

            // 0x30–0x3F: '0'–'9'
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
            0x08, 0x09, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,

            // 0x40–0x4F: 'A'–'F'
            0xFF, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,

            // 0x60–0x6F: 'a'–'f' also accepted
            0xFF, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
        ];
}
