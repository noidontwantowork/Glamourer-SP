namespace ImSharp;

/// <summary> An integral pair of dimensions in 2 dimensions. </summary>
/// <param name="Width"> The width of the object. </param>
/// <param name="Height"> The height of the object. </param>
public record struct Dimensions(uint Width, uint Height)
{
    /// <summary> A single invalid value. </summary>
    public const uint InvalidValue = uint.MaxValue;

    /// <summary> Invalid or unknown dimensions. </summary>
    public static readonly Dimensions Invalid = new(InvalidValue, InvalidValue);

    /// <summary> Empty dimensions. </summary>
    public static readonly Dimensions Zero = new();

    /// <summary> Single dimensions. </summary>
    public static readonly Dimensions One = new(1, 1);

    /// <summary> Create dimensions from a floating point vector. </summary>
    /// <param name="dim"> The vector. </param>
    public Dimensions(Vector2 dim)
        : this((uint)dim.X, (uint)dim.Y)
    { }

    /// <summary> Access the width and height by index. </summary>
    /// <param name="idx"> The index, should be 0 for Width or 1 for Height. </param>
    /// <returns> The Width or Height. </returns>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public uint this[int idx]
    {
        get => idx switch
        {
            0 => Width,
            1 => Height,
            _ => throw new ArgumentOutOfRangeException(nameof(idx)),
        };
        set
        {
            switch (idx)
            {
                case 0:  Width  = value; break;
                case 1:  Height = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(idx));
            }
        }
    }

    /// <summary> Half both dimensions separately or set them to 1 if this would result in 0. </summary>
    public Dimensions Half()
        => new(Math.Max(1, Width >> 1), Math.Max(1, Height >> 1));

    /// <summary> Quarter both dimensions separately or set them to 1 if this would result in 0. </summary>
    public Dimensions Quarter()
        => new(Math.Max(1, Width >> 2), Math.Max(1, Height >> 2));

    /// <summary> Alias for Width. </summary>
    public uint X
        => Width;

    /// <summary> Alias for Height. </summary>
    public uint Y
        => Height;

    /// <summary> Get the dimensions as a <see cref="Vector2"/>. </summary>
    public Vector2 AsVector
        => new(Width, Height);

    /// <summary> Get the dimensions as a signed pair (<see cref="ValueTuple{int, int}"/>). </summary>
    public (int Width, int Height) AsSigned
        => new((int)Width, (int)Height);

    public static implicit operator ValueTuple<uint, uint>(Dimensions d)
        => (d.Width, d.Height);

    public static implicit operator Dimensions((uint, uint) d)
        => new(d.Item1, d.Item2);

    public static implicit operator Dimensions((int, int) d)
        => new((uint)d.Item1, (uint)d.Item2);

    public static explicit operator Dimensions((float, float) d)
        => new((uint)d.Item1, (uint)d.Item2);

    public static explicit operator Dimensions(Vector2 d)
        => new((uint)d.X, (uint)d.Y);

    /// <inheritdoc/>
    public override string ToString()
        => $"{Width} x {Height}";
}

/// <summary> Extensions concerning <see cref="Dimensions"/> structs. </summary>
public static class DimensionsExtensions
{
    extension(ArgumentOutOfRangeException)
    {
        /// <summary> Throw if either of the passed dimensions is either 0 or <see cref="uint.MaxValue"/>. </summary>
        /// <param name="dimensions"> The dimensions to check. </param>
        /// <param name="paramName"> The parameter name. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfInvalidSize(Dimensions dimensions, [CallerArgumentExpression(nameof(dimensions))] string? paramName = null)
        {
            if (dimensions.Width is 0 or Dimensions.InvalidValue)
                throw new ArgumentOutOfRangeException(paramName is null ? "Width" : $"{paramName}.Width", dimensions.Width,
                    "Dimension must be greater than zero and not invalid.");

            if (dimensions.Height is 0 or Dimensions.InvalidValue)
                throw new ArgumentOutOfRangeException(paramName is null ? "Height" : $"{paramName}.Height", dimensions.Width,
                    "Dimension must be greater than zero and not invalid.");
        }
    }
}
