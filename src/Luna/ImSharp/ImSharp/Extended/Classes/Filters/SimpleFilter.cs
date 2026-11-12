namespace ImSharp;

/// <summary> Available simple filters for <see cref="SimpleCacheItem{T}"/>. </summary>
public enum SimpleFilterType : byte
{
    /// <summary> No filter at all. </summary>
    None = 0,

    /// <summary> A basic text filter that checks for containment. </summary>
    Text = 1,

    /// <summary> A part-wise text filter that checks whether all whitespace-separated tokens are contained. </summary>
    Partwise = 2,

    /// <summary> A regex text filter that allows for text or regex filtering. </summary>
    Regex = 3,
}

/// <summary> Extension methods for <see cref="SimpleFilterType"/>. </summary>
public static class SimpleFilterTypeExtensions
{
    /// <summary> Create a new filter of the corresponding type. </summary>
    /// <typeparam name="T"> The base type of the items. </typeparam>
    /// <param name="type"> The chosen filter type. </param>
    /// <returns> A filter of the corresponding type. </returns>
    public static IFilter<SimpleCacheItem<T>> ToFilter<T>(this SimpleFilterType type)
        => type switch
        {
            SimpleFilterType.Text     => new SimpleTextFilter<T>(),
            SimpleFilterType.Partwise => new SimplePartwiseFilter<T>(),
            SimpleFilterType.Regex    => new SimpleRegexFilter<T>(),
            _                         => NopFilter<SimpleCacheItem<T>>.Instance,
        };
}
