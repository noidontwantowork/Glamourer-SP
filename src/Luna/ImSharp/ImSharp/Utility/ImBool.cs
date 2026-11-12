namespace ImSharp;

/// <summary> A wrapper for easier marshaling of ImGui bools. </summary>
/// <param name="v"> The value as a byte. </param>
public readonly struct ImBool(byte v)
{
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public ImBool(bool value)
        : this(value ? (byte)1 : (byte)0)
    { }

    /// <summary> The true value. </summary>
    public static readonly ImBool True = new(1);

    /// <summary> The false value. </summary>
    public static readonly ImBool False = new(0);

    /// <summary> The actual byte value. 0 represents false, anything else true. </summary>
    public readonly byte Value = v;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator bool(ImBool v)
        => v.Value is not 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static implicit operator ImBool(bool v)
        => v ? True : False;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator true(ImBool v)
        => v.Value is not 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool operator false(ImBool v)
        => v.Value is 0;

    [MethodImpl(ImSharpConfiguration.OptInl)]
    public override string ToString()
        => ((bool)this).ToString();
}
