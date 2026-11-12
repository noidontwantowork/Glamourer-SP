using Microsoft.Extensions.Logging;

namespace ImSharp;

/// <summary> A manager to handle managed caches for UI display in a single space. </summary>
/// <remarks> Any object of this type will subscribe its update function to <seealso cref="ImSharpPerFrame.Update"/>. </remarks>
public class CacheManager : IDisposable
{
    private record CacheData(IManagedCache Cache, DateTime Time, int LastRequest)
    {
        public DateTime Time        { get; set; } = Time;
        public int      LastRequest { get; set; } = LastRequest;
    }

    /// <summary> The default cache manager that internal objects can use. </summary>
    /// <remarks> It is possible to set the logger and service provider of this instance. </remarks>
    public static readonly CacheManager Instance = new(null);

    /// <inheritdoc cref="GetOrCreateCache"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static TResult GetOrCreateGlobalCache<TResult>(ImGuiId id, Func<TResult> factory)
        where TResult : class, IManagedCache
        => Instance.GetOrCreateCache(id, factory);

    /// <inheritdoc cref="TryGetCache"/>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    public static bool TryGetGlobalCache<TResult>(ImGuiId id, [NotNullWhen(true)] out TResult? result)
        where TResult : class, IManagedCache
        => Instance.TryGetCache(id, out result);

    /// <summary> A custom logger to set when the manager should not use the global logger. </summary>
    public ILogger? CustomLogger
    {
        get => field;
        set
        {
            field = value;
            UpdateLogger(ImSharpConfiguration.Logger);
        }
    }

    /// <summary> The logger the internal functions write to. </summary>
    public ILogger Logger { get; private set; }

    private readonly Dictionary<ImGuiId, CacheData> _caches     = [];
    private readonly Dictionary<ImGuiId, object>    _storedData = [];

    /// <summary> Invoked when the cache manager is requested to set font dirty flags on all caches. </summary>
    public event Action? OnFontDirty;

    /// <summary> Invoked when the cache manager is requested to set style dirty flags on all caches. </summary>
    public event Action? OnStyleDirty;

    /// <summary> Invoked when the cache manager is requested to set color dirty flags on all caches. </summary>
    public event Action? OnColorsDirty;

    /// <summary> Create a manager to handle managed caches for UI display in a single space. </summary>
    /// <param name="logger"> A logger. </param>
    protected CacheManager(ILogger? logger)
    {
        CustomLogger                       =  logger;
        Logger                             =  CustomLogger ?? ImSharpConfiguration.Logger;
        ImSharpPerFrame.Update             += CheckCaches;
        ImSharpConfiguration.LoggerChanged += UpdateLogger;
    }

    /// <summary> Try to obtain an existing cache of a specific type without triggering any updates on it or recreating it for false types. </summary>
    /// <typeparam name="TResult"> The expected type of the cache. </typeparam>
    /// <param name="id"> The ID of the queried cache. </param>
    /// <param name="result"> On success, the returned existing cache, otherwise null. </param>
    /// <returns> True if the cache exists and is assignable to the expected type. </returns>
    public bool TryGetCache<TResult>(ImGuiId id, [NotNullWhen(true)] out TResult? result)
        where TResult : class, IManagedCache
    {
        if (_caches.TryGetValue(id, out var cache) && cache.Cache is TResult c)
        {
            cache.LastRequest = Im.State.FrameCount;
            cache.Time        = NextDeletion(cache.Cache.KeepAliveDuration);
            result            = c;
            return true;
        }

        result = null;
        return false;
    }

    /// <summary> Get or create a new cache for a given ID, and update the last access for it. </summary>
    /// <typeparam name="TResult"> The type of the cache. </typeparam>
    /// <param name="id"> The ID to store the cache under. </param>
    /// <param name="factory"> The factory function to create the cache if it does not exist already. </param>
    /// <returns> The existing or newly created cache. </returns>
    /// <remarks>
    ///   If there is a cache for <paramref name="id"/> stored but its type is not compatible with <typeparamref name="TResult"/>
    ///   it will be disposed and replaced by a newly created cache.
    /// </remarks>
    public TResult GetOrCreateCache<TResult>(ImGuiId id, Func<TResult> factory)
        where TResult : class, IManagedCache
    {
        if (!_caches.TryGetValue(id, out var tuple))
        {
            var cache = factory();
            if (_storedData.TryGetValue(id, out var data))
                cache.ApplyStoredData(data);
            cache.Update();
            _caches.Add(id, new CacheData(cache, NextDeletion(cache.KeepAliveDuration), Im.State.FrameCount));
            Logger.LogDebug("[CacheManager] Created new cache of type {Type:l} for ID {ID}.", typeof(TResult).Name, id.Id);
            return cache;
        }

        if (CheckAndUpdateCache<TResult>(id, tuple) is { } existingCache)
            return existingCache;

        if (tuple.Cache.SaveStoredData() is { } obj)
            _storedData[id] = obj;
        (tuple.Cache as IDisposable)?.Dispose();
        var newCache = factory();
        newCache.Update();
        if (_storedData.TryGetValue(id, out var newData))
            newCache.ApplyStoredData(newData);
        _caches[id] = new CacheData(newCache, NextDeletion(newCache.KeepAliveDuration), Im.State.FrameCount);
        Logger.LogInformation("[CacheManager] Replaced existing cache of type {OldType:l} with new type {NewType:l} for ID {ID}.",
            new TypeWrapper(tuple.Cache),
            typeof(TResult).Name, id.Id);
        return newCache;
    }

    /// <summary> Check all caches for disposal. </summary>
    /// <remarks> Any cache that has not been retrieved for at least its <seealso cref="IManagedCache.KeepAliveDuration"/> frames will be disposed and removed. </remarks>
    protected void CheckCaches()
    {
        var now = DateTime.UtcNow;
        foreach (var (id, (cache, time, _)) in _caches)
        {
            if (time >= now)
                continue;

            if (cache.SaveStoredData() is { } obj)
                _storedData[id] = obj;
            (cache as IDisposable)?.Dispose();
            _caches.Remove(id);
            Logger.LogTrace("[CacheManager] Removed cache of type {Type:l} for ID {ID}.", new TypeWrapper(cache), id.Id);
        }
    }

    /// <summary> Set the custom dirty flag for a specific cache by its ID. </summary>
    /// <param name="id"> The ID of the cache to set the flag for. </param>
    /// <remarks> If no cache for this ID exists, this does nothing. </remarks>
    public void SetCustomDirty(ImGuiId id)
    {
        if (_caches.TryGetValue(id, out var pair))
        {
            pair.Cache.Dirty |= IManagedCache.DirtyFlags.Custom;
            Logger.LogTrace("[CacheManager] Set custom dirty flag for ID {ID}.", id.Id);
        }
    }

    /// <summary> Set the full dirty flag for a specific cache by its ID. </summary>
    /// <param name="id"> The ID of the cache to set the flag for. </param>
    /// <remarks> If no cache for this ID exists, this does nothing. </remarks>
    public void SetDirty(ImGuiId id)
    {
        if (_caches.TryGetValue(id, out var pair) && pair.Cache.Dirty is not IManagedCache.DirtyFlags.Dirty)
        {
            pair.Cache.Dirty |= IManagedCache.DirtyFlags.Dirty;
            Logger.LogTrace("[CacheManager] Set full dirty flag for ID {ID}.", id.Id);
        }
    }

    /// <summary> Set the font dirty flag for all caches. </summary>
    public void SetFontDirty()
    {
        Logger.LogTrace("[CacheManager] Set font size dirty flag for all caches.");
        OnFontDirty?.Invoke();
        foreach (var (cache, _, _) in _caches.Values)
            cache.Dirty |= IManagedCache.DirtyFlags.Font;
    }

    /// <summary> Set the style dirty flag for all caches. </summary>
    public void SetStyleDirty()
    {
        Logger.LogTrace("[CacheManager] Set style dirty flag for all caches.");
        OnStyleDirty?.Invoke();
        foreach (var (cache, _, _) in _caches.Values)
            cache.Dirty |= IManagedCache.DirtyFlags.Style;
    }

    /// <summary> Set the colors dirty flag for all caches. </summary>
    public void SetColorsDirty()
    {
        Logger.LogTrace("[CacheManager] Set colors dirty flag for all caches.");
        OnColorsDirty?.Invoke();
        foreach (var (cache, _, _) in _caches.Values)
            cache.Dirty |= IManagedCache.DirtyFlags.Colors;
    }

    /// <summary> Dispose and remove all stored caches. </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary> Dispose and remove all stored caches. </summary>
    protected virtual void Dispose(bool disposing)
    {
        foreach (var (cache, _, _) in _caches.Values)
            (cache as IDisposable)?.Dispose();
        _caches.Clear();
        ImSharpPerFrame.Update             -= CheckCaches;
        ImSharpConfiguration.LoggerChanged -= UpdateLogger;
    }

    /// <summary> Update the logger if it changes in the global configuration. </summary>
    private void UpdateLogger(ILogger obj)
        => Logger = CustomLogger ?? obj;

    ~CacheManager()
        => Dispose(false);

    /// <summary> Safely add the current time count and the keep alive duration without overflow. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    private static DateTime NextDeletion(TimeSpan keepAliveDuration)
    {
        if (keepAliveDuration == TimeSpan.MaxValue)
            return DateTime.MaxValue;
        if (keepAliveDuration < TimeSpan.Zero)
            return DateTime.MinValue;

        return DateTime.UtcNow + keepAliveDuration;
    }

    /// <summary> Check a pre-existing cache to be the correct type and update it if it is. </summary>
    [MethodImpl(ImSharpConfiguration.OptInl)]
    private TResult? CheckAndUpdateCache<TResult>(ImGuiId id, CacheData cache)
        where TResult : class, IManagedCache
    {
        if (cache.Cache is not TResult res)
            return null;

        if (cache.LastRequest < Im.State.FrameCount - 1)
            res.SkippedRequests();
        res.Update();
        cache.Time        = NextDeletion(res.KeepAliveDuration);
        cache.LastRequest = Im.State.FrameCount;
        return res;
    }

    private readonly struct TypeWrapper(object obj)
    {
        public override string ToString()
            => obj.GetType().Name;
    }
}
