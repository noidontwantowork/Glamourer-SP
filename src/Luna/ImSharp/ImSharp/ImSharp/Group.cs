namespace ImSharp;

public static partial class Im
{
    /// <inheritdoc cref="GroupDisposable(bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static GroupDisposable Group()
        => new(true);

    /// <inheritdoc cref="ColumnsDisposable(int,ref Utf8LabelHandler,bool)"/>
    public static ColumnsDisposable Columns(int count, Utf8LabelHandler label, bool border = false)
        => new(count, ref label, border);
}
