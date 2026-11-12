namespace ImSharp;

/// <summary> An interface to represent managed caches used by <seealso cref="CacheManager"/>. </summary>
public interface IManagedCache
{
    /// <summary> Flags to denote different types of dirtiness for caches. </summary>
    [Flags]
    public enum DirtyFlags : byte
    {
        /// <summary> The cache does not need any updates. </summary>
        Clean = 0,

        /// <summary> The cache's underlying data or custom attributes changed and it needs updates. </summary>
        Custom = 1,

        /// <summary> The global font changed and text sizes need recalculation. </summary>
        Font = 2,

        /// <summary> Global style variables changed and item sizes and positions need recalculation. </summary>
        Style = 4,

        /// <summary> Global colors changed and stored color values need recalculation. </summary>
        Colors = 8,

        /// <summary> Everything should be updated. </summary>
        Dirty = Custom | Font | Style | Colors,
    }

    /// <summary> The dirty state of the cache. </summary>
    public DirtyFlags Dirty { get; set; }

    /// <summary> The duration of not being seen a cache should be kept alive. </summary>
    /// <remarks> Set this to <seealso cref="TimeSpan.MaxValue"/> for persistent caches. Set it to non-positive values for caches that should get removed immediately. </remarks>
    public TimeSpan KeepAliveDuration { get; set; }

    /// <summary> Update the caches state. This should handle the individual <seealso cref="DirtyFlags"/> sensibly and should set <seealso cref="Dirty"/> to <seealso cref="DirtyFlags.Clean"/> when finished. </summary>
    public void Update();

    /// <summary> Invoked when the cache has not been requested for at least one frame since the last time it has been requested. </summary>
    public void SkippedRequests();

    /// <summary> Apply data that should be stored even if the cache is removed from the cache manager. Called by the manager when a new cache object is created, before Update is called, if data for this ID already exists. </summary>
    public void ApplyStoredData(object existingData);

    /// <summary> Save data that should be stored when the cache is removed from the cache manager. Called by the manager when a cache object is removed. </summary>
    public object? SaveStoredData();
}

/// <summary> A basic disposable cache implementation. </summary>
public abstract class BasicCache() : IManagedCache, IDisposable
{
    /// <summary> Create a disposable cache with specific initial data. </summary>
    /// <param name="keepAliveDuration"> The duration of not being seen a cache should be kept alive. </param>
    /// <param name="dirtyStart"> The dirty state of the cache on creation. </param>
    public BasicCache(TimeSpan keepAliveDuration, IManagedCache.DirtyFlags dirtyStart = IManagedCache.DirtyFlags.Dirty)
        : this()
    {
        KeepAliveDuration = keepAliveDuration;
        Dirty             = dirtyStart;
    }

    /// <inheritdoc/>
    public IManagedCache.DirtyFlags Dirty { get; set; } = IManagedCache.DirtyFlags.Dirty;

    /// <inheritdoc/>
    public TimeSpan KeepAliveDuration { get; set; } = TimeSpan.FromSeconds(5);

    public int LastRequestedFrame { get; set; } = -1;

    /// <inheritdoc/>
    public abstract void Update();

    /// <inheritdoc/>
    public virtual void SkippedRequests()
    { }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    public virtual void ApplyStoredData(object existingData)
    { }

    /// <inheritdoc/>
    public virtual object? SaveStoredData()
        => null;

    ~BasicCache()
        => Dispose(false);

    /// <summary> Custom disposal. </summary>
    /// <param name="disposing"> Whether the disposal comes from a finalizer or a <seealso cref="Dispose()"/>. </param>
    protected virtual void Dispose(bool disposing)
    { }

    /// <summary> Query whether the font settings have changed since the last time this cache was updated. </summary>
    protected bool FontDirty
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Dirty.HasFlag(IManagedCache.DirtyFlags.Font);
    }

    /// <summary> Query whether the global ImGui style settings have changed since the last time this cache was updated. </summary>
    protected bool StyleDirty
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Dirty.HasFlag(IManagedCache.DirtyFlags.Style);
    }

    /// <summary> Query whether any color settings have changed since the last time this cache was updated. </summary>
    protected bool ColorsDirty
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Dirty.HasFlag(IManagedCache.DirtyFlags.Colors);
    }

    /// <summary> Query whether the cache's own dirty state changed since the last time this cache was updated. </summary>
    protected bool CustomDirty
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Dirty.HasFlag(IManagedCache.DirtyFlags.Custom);
    }

    /// <summary> Query whether anything has changed since the last time this cache was updated. </summary>
    protected bool AnyDirty
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        get => Dirty is not IManagedCache.DirtyFlags.Clean;
    }
}
