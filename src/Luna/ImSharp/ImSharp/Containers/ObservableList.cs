namespace ImSharp.Containers;

/// <summary> Types of changes made to a list. </summary>
public enum ListChangeType : byte
{
    /// <summary> The list was cleared. </summary>
    /// <remarks> Other arguments are invalid. </remarks>
    Clear,

    /// <summary> A single element was appended to the list.  </summary>
    /// <remarks> <see cref="ObservableList{T}.ChangeArguments.NewValue"/>, <see cref="ObservableList{T}.ChangeArguments.Index"/>, and <see cref="ObservableList{T}.ChangeArguments.Count"/> are set. </remarks>
    Add,

    /// <summary> Multiple elements were appended to the list.  </summary>
    /// <remarks> <see cref="ObservableList{T}.ChangeArguments.Index"/> and <see cref="ObservableList{T}.ChangeArguments.Count"/> are set. </remarks>
    AddRange,

    /// <summary> A single element was inserted into the list.  </summary>
    /// <remarks> <see cref="ObservableList{T}.ChangeArguments.NewValue"/>, <see cref="ObservableList{T}.ChangeArguments.Index"/>, and <see cref="ObservableList{T}.ChangeArguments.Count"/> are set. </remarks>
    Insert,

    /// <summary> Multiple elements were inserted into the list.  </summary>
    /// <remarks> <see cref="ObservableList{T}.ChangeArguments.Index"/> and <see cref="ObservableList{T}.ChangeArguments.Count"/> are set. </remarks>
    InsertRange,

    /// <summary> A single element was removed from the list.  </summary>
    /// <remarks> <see cref="ObservableList{T}.ChangeArguments.OldValue"/>, <see cref="ObservableList{T}.ChangeArguments.Index"/>, and <see cref="ObservableList{T}.ChangeArguments.Count"/> are set. </remarks>
    Remove,

    /// <summary> Multiple elements were removed from the list.  </summary>
    /// <remarks> <see cref="ObservableList{T}.ChangeArguments.Index"/> and <see cref="ObservableList{T}.ChangeArguments.Count"/> are set. </remarks>
    RemoveRange,

    /// <summary> A single element in the list was replaced by another element.  </summary>
    /// <remarks> <see cref="ObservableList{T}.ChangeArguments.NewValue"/>, <see cref="ObservableList{T}.ChangeArguments.OldValue"/>, <see cref="ObservableList{T}.ChangeArguments.Index"/>, and <see cref="ObservableList{T}.ChangeArguments.Count"/> are set. </remarks>
    Update,
}

/// <summary> A list that invokes events when changes occur. </summary>
/// <typeparam name="T"> The type of item. </typeparam>
public interface IObservableList<T> : IReadOnlyList<T>
{
    public event ObservableList<T>.ChangeDelegate OnChange;
}

/// <summary> A list that invokes events when changes occur. </summary>
/// <typeparam name="T"> The type of item. </typeparam>
public class ObservableList<T> : List<T>, IObservableList<T>
{
    /// <summary> Arguments provided for the event when the list changes. </summary>
    public record struct ChangeArguments()
    {
        /// <summary> The value removed or updated from. </summary>
        public T? OldValue = default;

        /// <summary> The value added, inserted or updated to. </summary>
        public T? NewValue = default;

        /// <summary> The index of the first changed element. </summary>
        public int Index = -1;

        /// <summary> The number of changed elements. </summary>
        public int Count = 0;

        /// <summary> The type of change. </summary>
        public ListChangeType Type = ListChangeType.Clear;
    }

    /// <summary> Invoked on any change of the list. </summary>
    public event ChangeDelegate? OnChange;

    /// <inheritdoc cref="List{T}.Clear"/>
    public new void Clear()
    {
        base.Clear();
        OnChange?.Invoke(new ChangeArguments { Type = ListChangeType.Clear });
    }

    /// <inheritdoc cref="List{T}.Add"/>
    public new void Add(T item)
    {
        var count = Count;
        base.Add(item);
        OnChange?.Invoke(new ChangeArguments
        {
            Type     = ListChangeType.Add,
            Index    = count,
            Count    = 1,
            NewValue = item,
        });
    }

    /// <inheritdoc cref="List{T}.Insert"/>
    public new void Insert(int index, T item)
    {
        base.Insert(index, item);
        OnChange?.Invoke(new ChangeArguments
        {
            Type     = ListChangeType.Insert,
            Index    = index,
            Count    = 1,
            NewValue = item,
        });
    }

    /// <inheritdoc cref="List{T}.Remove"/>
    public new bool Remove(T item)
    {
        var idx = IndexOf(item);
        if (idx < 0)
            return false;

        base.RemoveAt(idx);
        OnChange?.Invoke(new ChangeArguments
        {
            Type     = ListChangeType.Remove,
            Index    = idx,
            Count    = 1,
            OldValue = item,
        });
        return true;
    }

    /// <inheritdoc cref="List{T}.RemoveAt"/>
    public new void RemoveAt(int idx)
    {
        if (idx >= Count)
            return;

        var item = this[idx];
        base.RemoveAt(idx);
        OnChange?.Invoke(new ChangeArguments
        {
            Type     = ListChangeType.Remove,
            Index    = idx,
            Count    = 1,
            OldValue = item,
        });
    }

    /// <inheritdoc cref="List{T}.RemoveRange"/>
    public new void RemoveRange(int index, int count)
    {
        base.RemoveRange(index, count);
        OnChange?.Invoke(new ChangeArguments
        {
            Type  = ListChangeType.RemoveRange,
            Index = index,
            Count = count,
        });
    }

    /// <inheritdoc cref="List{T}.AddRange"/>
    public new void AddRange(IEnumerable<T> collection)
    {
        var startIdx = Count;
        base.AddRange(collection);
        OnChange?.Invoke(new ChangeArguments
        {
            Type  = ListChangeType.Add,
            Index = startIdx,
            Count = Count - startIdx,
        });
    }

    /// <inheritdoc cref="List{T}.InsertRange"/>
    public new void InsertRange(int index, IEnumerable<T> collection)
    {
        var startCount = Count;
        base.InsertRange(index, collection);
        OnChange?.Invoke(new ChangeArguments
        {
            Type  = ListChangeType.InsertRange,
            Index = index,
            Count = Count - startCount,
        });
    }

    /// <inheritdoc cref="List{T}.this"/>
    public new T this[int index]
    {
        get => base[index];
        set
        {
            var old = base[index];
            base[index] = value;
            OnChange?.Invoke(new ChangeArguments
            {
                Type     = ListChangeType.Update,
                Index    = index,
                Count    = 1,
                OldValue = old,
                NewValue = value,
            });
        }
    }

    /// <summary> The delegate invoked on changes. </summary>
    public delegate void ChangeDelegate(in ChangeArguments args);
}
