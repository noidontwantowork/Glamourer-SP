namespace ImSharp.Containers;

/// <summary> Type-less base class for cache list adapters. </summary>
public abstract class CacheListAdapter
{
    /// <summary> A change-counter to keep track of invoked changes. </summary>
    public uint Revision { get; protected set; }

    /// <summary> An additional tracker of invoked changed that can be cleaned up and checked by the parent. </summary>
    public bool Dirty { get; set; }

    /// <summary> The number of items that can be obtained. The items are lazily initialized. </summary>
    public abstract int Count { get; }
}

/// <summary> Source-type-less base class for cache list adapters. </summary>
/// <typeparam name="TCacheItem"> The type of item provided by the adapter. </typeparam>
/// <param name="count"> The initial capacity of the adapter.</param>
public abstract class CacheListAdapter<TCacheItem>(int count) : CacheListAdapter, IReadOnlyList<TCacheItem>
    where TCacheItem : class
{
    /// <summary> The actual storage cache of lazily initialized items. </summary>
    protected readonly List<TCacheItem?> CacheItems = new(count);

    /// <inheritdoc/>
    /// <remarks> Initializes all items that are reached during iteration. </remarks>
    public abstract IEnumerator<TCacheItem> GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <summary> Get the item at the given index. </summary>
    /// <param name="index"> The index to query. </param>
    /// <returns> The item at the index if the source list is large enough for it. </returns>
    /// <remarks> If the source item is not initialized yet, it will be automatically initialized. </remarks>
    public abstract TCacheItem this[int index] { get; }
}

/// <summary> An adapter binding to another list and supplying transformed items based on that list. </summary>
/// <typeparam name="TSourceItem"> The type of the source items. </typeparam>
/// <typeparam name="TCacheItem"> The type of the transformed items. </typeparam>
public class CacheListAdapter<TSourceItem, TCacheItem> : CacheListAdapter<TCacheItem>, IDisposable
    where TCacheItem : class
{
    /// <summary> The source list. </summary>
    private readonly IReadOnlyList<TSourceItem> _source;

    /// <summary> The transforming function, invoked when new items are initialized. </summary>
    private readonly Func<TSourceItem, TCacheItem> _converter;

    /// <summary> Create a new adapter for the given list using the specified converter. </summary>
    /// <param name="source"> The associated list. If this is a <see cref="ObservableList{T}"/>, the adapter automatically subscribes to its events and updates its own state with changes in it. </param>
    /// <param name="converter"> The transforming function, invoked whenever new items are initialized in the cache. </param>
    public CacheListAdapter(IReadOnlyList<TSourceItem> source, Func<TSourceItem, TCacheItem> converter)
        : base(source.Count)
    {
        _source    = source;
        _converter = converter;
        Subscribe();
    }

    /// <summary> Subscribe to the events of the associated list if it is observable. </summary>
    private void Subscribe()
    {
        EnsureCount(CacheItems, _source.Count);
        if (_source is ObservableList<TSourceItem> observable)
            observable.OnChange += OnChange;
    }

    private void OnChange(in ObservableList<TSourceItem>.ChangeArguments args)
    {
        switch (args.Type)
        {
            case ListChangeType.Clear:       CacheItems.Clear(); break;
            case ListChangeType.Add:         CacheItems.Add(null); break;
            case ListChangeType.AddRange:    CacheItems.AddRange(Enumerable.Repeat<TCacheItem?>(null, args.Count)); break;
            case ListChangeType.Insert:      CacheItems.Insert(args.Index, null); break;
            case ListChangeType.InsertRange: CacheItems.InsertRange(args.Index, Enumerable.Repeat<TCacheItem?>(null, args.Count)); break;
            case ListChangeType.Remove:      CacheItems.RemoveAt(args.Index); break;
            case ListChangeType.RemoveRange: CacheItems.RemoveRange(args.Index, args.Count); break;
            case ListChangeType.Update:      CacheItems[args.Index] = null; break;
        }

        Dirty = true;
        ++Revision;
    }

    /// <summary> Unsubscribe from the events of the associated list if it is observable. </summary>
    private void Unsubscribe()
    {
        if (_source is ObservableList<TSourceItem> observable)
            observable.OnChange -= OnChange;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Unsubscribe();
        GC.SuppressFinalize(this);
    }

    ~CacheListAdapter()
        => Unsubscribe();

    /// <inheritdoc/>
    public sealed override IEnumerator<TCacheItem> GetEnumerator()
    {
        var count = _source.Count;
        EnsureCount(CacheItems, count);
        for (var i = 0; i < count; ++i)
            yield return CacheItems[i] ??= _converter(_source[i]);
    }

    /// <inheritdoc/>
    public sealed override int Count
        => _source.Count;

    /// <inheritdoc/>
    public sealed override TCacheItem this[int index]
    {
        get
        {
            if (index >= _source.Count)
                throw new IndexOutOfRangeException();

            EnsureCount(CacheItems, _source.Count);
            return CacheItems[index] ??= _converter(_source[index]);
        }
    }

    /// <summary> Ensure that the actual size, not just the capacity of the list is large enough. </summary>
    private static int EnsureCount<T>(List<T> list, int count)
    {
        if (list.Count >= count)
            return 0;

        var toAdd = count - list.Count;
        for (var i = 0; i < toAdd; i++)
            list.Add(default!);
        return toAdd;
    }
}
