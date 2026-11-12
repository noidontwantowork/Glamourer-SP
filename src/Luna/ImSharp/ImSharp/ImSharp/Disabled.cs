namespace ImSharp;

public static partial class Im
{
    /// <inheritdoc cref="DisabledDisposable.Push(bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static DisabledDisposable Disabled(bool condition)
        => new DisabledDisposable().Push(condition);

    /// <inheritdoc cref="DisabledDisposable.Push()"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static DisabledDisposable Disabled()
        => new DisabledDisposable().Push();

    /// <inheritdoc cref="EnabledDisposable(bool)"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static EnabledDisposable Enabled(bool condition)
        => new(condition);

    /// <inheritdoc cref="EnabledDisposable()"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static EnabledDisposable Enabled()
        => new();
}
