namespace ImSharp;

/// <summary> Ref Tuple static functions. </summary>
public static class RefTuple
{
    /// <inheritdoc cref="Create{T1,T2,T3,T4,T5,T6,T7,T8,T9}(T1,T2,T3,T4,T5,T6,T7,T8,T9)"/>
    public static RefTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        where T1 : allows ref struct
        where T2 : allows ref struct
        => new(item1, item2);

    /// <inheritdoc cref="Create{T1,T2,T3,T4,T5,T6,T7,T8,T9}(T1,T2,T3,T4,T5,T6,T7,T8,T9)"/>
    public static RefTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        where T1 : allows ref struct
        where T2 : allows ref struct
        where T3 : allows ref struct
        => new(item1, item2, item3);

    /// <inheritdoc cref="Create{T1,T2,T3,T4,T5,T6,T7,T8,T9}(T1,T2,T3,T4,T5,T6,T7,T8,T9)"/>
    public static RefTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        where T1 : allows ref struct
        where T2 : allows ref struct
        where T3 : allows ref struct
        where T4 : allows ref struct
        => new(item1, item2, item3, item4);

    /// <inheritdoc cref="Create{T1,T2,T3,T4,T5,T6,T7,T8,T9}(T1,T2,T3,T4,T5,T6,T7,T8,T9)"/>
    public static RefTuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        where T1 : allows ref struct
        where T2 : allows ref struct
        where T3 : allows ref struct
        where T4 : allows ref struct
        where T5 : allows ref struct
        => new(item1, item2, item3, item4, item5);

    /// <inheritdoc cref="Create{T1,T2,T3,T4,T5,T6,T7,T8,T9}(T1,T2,T3,T4,T5,T6,T7,T8,T9)"/>
    public static RefTuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        where T1 : allows ref struct
        where T2 : allows ref struct
        where T3 : allows ref struct
        where T4 : allows ref struct
        where T5 : allows ref struct
        where T6 : allows ref struct
        => new(item1, item2, item3, item4, item5, item6);

    /// <inheritdoc cref="Create{T1,T2,T3,T4,T5,T6,T7,T8,T9}(T1,T2,T3,T4,T5,T6,T7,T8,T9)"/>
    public static RefTuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        where T1 : allows ref struct
        where T2 : allows ref struct
        where T3 : allows ref struct
        where T4 : allows ref struct
        where T5 : allows ref struct
        where T6 : allows ref struct
        where T7 : allows ref struct
        => new(item1, item2, item3, item4, item5, item6, item7);

    /// <inheritdoc cref="Create{T1,T2,T3,T4,T5,T6,T7,T8,T9}(T1,T2,T3,T4,T5,T6,T7,T8,T9)"/>
    public static RefTuple<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
        where T1 : allows ref struct
        where T2 : allows ref struct
        where T3 : allows ref struct
        where T4 : allows ref struct
        where T5 : allows ref struct
        where T6 : allows ref struct
        where T7 : allows ref struct
        where T8 : allows ref struct
        => new(item1, item2, item3, item4, item5, item6, item7, item8);

    /// <inheritdoc cref="RefTuple{T1,T2,T3,T4,T5,T6,T7,T8,T9}"/>
    public static RefTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
        where T1 : allows ref struct
        where T2 : allows ref struct
        where T3 : allows ref struct
        where T4 : allows ref struct
        where T5 : allows ref struct
        where T6 : allows ref struct
        where T7 : allows ref struct
        where T8 : allows ref struct
        where T9 : allows ref struct
        => new(item1, item2, item3, item4, item5, item6, item7, item8, item9);
}

/// <inheritdoc cref="RefTuple{T1,T2,T3,T4,T5,T6,T7,T8,T9}"/>
public readonly ref struct RefTuple<T1, T2>(T1 item1, T2 item2)
    where T1 : allows ref struct
    where T2 : allows ref struct
{
    /// <summary> The first object of the tuple. </summary>
    public readonly T1 Item1 = item1;

    /// <summary> The second object of the tuple. </summary>
    public readonly T2 Item2 = item2;

    /// <summary> Deconstruct the tuple into its components. </summary>
    public void Deconstruct(out T1 item1, out T2 item2)
    {
        item1 = Item1;
        item2 = Item2;
    }
}

/// <inheritdoc cref="RefTuple{T1,T2,T3,T4,T5,T6,T7,T8,T9}"/>
public readonly ref struct RefTuple<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
{
    /// <summary> The first object of the tuple. </summary>
    public readonly T1 Item1 = item1;

    /// <summary> The second object of the tuple. </summary>
    public readonly T2 Item2 = item2;

    /// <summary> The second object of the tuple. </summary>
    public readonly T3 Item3 = item3;

    /// <summary> Deconstruct the tuple into its components. </summary>
    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
    }
}

/// <inheritdoc cref="RefTuple{T1,T2,T3,T4,T5,T6,T7,T8,T9}"/>
public readonly ref struct RefTuple<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
    where T4 : allows ref struct
{
    /// <summary> The first object of the tuple. </summary>
    public readonly T1 Item1 = item1;

    /// <summary> The second object of the tuple. </summary>
    public readonly T2 Item2 = item2;

    /// <summary> The third object of the tuple. </summary>
    public readonly T3 Item3 = item3;

    /// <summary> The fourth object of the tuple. </summary>
    public readonly T4 Item4 = item4;

    /// <summary> Deconstruct the tuple into its components. </summary>
    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
    }
}

/// <inheritdoc cref="RefTuple{T1,T2,T3,T4,T5,T6,T7,T8,T9}"/>
public readonly ref struct RefTuple<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
    where T4 : allows ref struct
    where T5 : allows ref struct
{
    /// <summary> The first object of the tuple. </summary>
    public readonly T1 Item1 = item1;

    /// <summary> The second object of the tuple. </summary>
    public readonly T2 Item2 = item2;

    /// <summary> The third object of the tuple. </summary>
    public readonly T3 Item3 = item3;

    /// <summary> The fourth object of the tuple. </summary>
    public readonly T4 Item4 = item4;

    /// <summary> The fifth object of the tuple. </summary>
    public readonly T5 Item5 = item5;

    /// <summary> Deconstruct the tuple into its components. </summary>
    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        item5 = Item5;
    }
}

/// <inheritdoc cref="RefTuple{T1,T2,T3,T4,T5,T6,T7,T8,T9}"/>
public readonly ref struct RefTuple<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
    where T4 : allows ref struct
    where T5 : allows ref struct
    where T6 : allows ref struct
{
    /// <summary> The first object of the tuple. </summary>
    public readonly T1 Item1 = item1;

    /// <summary> The second object of the tuple. </summary>
    public readonly T2 Item2 = item2;

    /// <summary> The third object of the tuple. </summary>
    public readonly T3 Item3 = item3;

    /// <summary> The fourth object of the tuple. </summary>
    public readonly T4 Item4 = item4;

    /// <summary> The fifth object of the tuple. </summary>
    public readonly T5 Item5 = item5;

    /// <summary> The sixth object of the tuple. </summary>
    public readonly T6 Item6 = item6;

    /// <summary> Deconstruct the tuple into its components. </summary>
    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        item5 = Item5;
        item6 = Item6;
    }
}

/// <inheritdoc cref="RefTuple{T1,T2,T3,T4,T5,T6,T7,T8,T9}"/>
public readonly ref struct RefTuple<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
    where T4 : allows ref struct
    where T5 : allows ref struct
    where T6 : allows ref struct
    where T7 : allows ref struct
{
    /// <summary> The first object of the tuple. </summary>
    public readonly T1 Item1 = item1;

    /// <summary> The second object of the tuple. </summary>
    public readonly T2 Item2 = item2;

    /// <summary> The third object of the tuple. </summary>
    public readonly T3 Item3 = item3;

    /// <summary> The fourth object of the tuple. </summary>
    public readonly T4 Item4 = item4;

    /// <summary> The fifth object of the tuple. </summary>
    public readonly T5 Item5 = item5;

    /// <summary> The sixth object of the tuple. </summary>
    public readonly T6 Item6 = item6;

    /// <summary> The seventh object of the tuple. </summary>
    public readonly T7 Item7 = item7;

    /// <summary> Deconstruct the tuple into its components. </summary>
    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        item5 = Item5;
        item6 = Item6;
        item7 = Item7;
    }
}

/// <inheritdoc cref="RefTuple{T1,T2,T3,T4,T5,T6,T7,T8,T9}"/>
public readonly ref struct RefTuple<T1, T2, T3, T4, T5, T6, T7, T8>(
    T1 item1,
    T2 item2,
    T3 item3,
    T4 item4,
    T5 item5,
    T6 item6,
    T7 item7,
    T8 item8)
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
    where T4 : allows ref struct
    where T5 : allows ref struct
    where T6 : allows ref struct
    where T7 : allows ref struct
    where T8 : allows ref struct
{
    /// <summary> The first object of the tuple. </summary>
    public readonly T1 Item1 = item1;

    /// <summary> The second object of the tuple. </summary>
    public readonly T2 Item2 = item2;

    /// <summary> The third object of the tuple. </summary>
    public readonly T3 Item3 = item3;

    /// <summary> The fourth object of the tuple. </summary>
    public readonly T4 Item4 = item4;

    /// <summary> The fifth object of the tuple. </summary>
    public readonly T5 Item5 = item5;

    /// <summary> The sixth object of the tuple. </summary>
    public readonly T6 Item6 = item6;

    /// <summary> The seventh object of the tuple. </summary>
    public readonly T7 Item7 = item7;

    /// <summary> The eighth object of the tuple. </summary>
    public readonly T8 Item8 = item8;

    /// <summary> Deconstruct the tuple into its components. </summary>
    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        item5 = Item5;
        item6 = Item6;
        item7 = Item7;
        item8 = Item8;
    }
}

/// <summary> A tuple that allows ref struct elements. </summary>
/// <typeparam name="T1"> The type of the first element. </typeparam>
/// <typeparam name="T2"> The type of the second element. </typeparam>
/// <typeparam name="T3"> The type of the third element. </typeparam>
/// <typeparam name="T4"> The type of the fourth element. </typeparam>
/// <typeparam name="T5"> The type of the fifth element. </typeparam>
/// <typeparam name="T6"> The type of the sixth element. </typeparam>
/// <typeparam name="T7"> The type of the seventh element. </typeparam>
/// <typeparam name="T8"> The type of the eighth element. </typeparam>
/// <typeparam name="T9"> The type of the ninth element. </typeparam>
/// <param name="item1"> The first object. </param>
/// <param name="item2"> The second object. </param>
/// <param name="item3"> The third object. </param>
/// <param name="item4"> The fourth object. </param>
/// <param name="item5"> The fifth object. </param>
/// <param name="item6"> The sixth object. </param>
/// <param name="item7"> The seventh object. </param>
/// <param name="item8"> The eighth object. </param>
/// <param name="item9"> The ninth object. </param>
public readonly ref struct RefTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    T1 item1,
    T2 item2,
    T3 item3,
    T4 item4,
    T5 item5,
    T6 item6,
    T7 item7,
    T8 item8,
    T9 item9)
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
    where T4 : allows ref struct
    where T5 : allows ref struct
    where T6 : allows ref struct
    where T7 : allows ref struct
    where T8 : allows ref struct
    where T9 : allows ref struct
{
    /// <summary> The first object of the tuple. </summary>
    public readonly T1 Item1 = item1;

    /// <summary> The second object of the tuple. </summary>
    public readonly T2 Item2 = item2;

    /// <summary> The third object of the tuple. </summary>
    public readonly T3 Item3 = item3;

    /// <summary> The fourth object of the tuple. </summary>
    public readonly T4 Item4 = item4;

    /// <summary> The fifth object of the tuple. </summary>
    public readonly T5 Item5 = item5;

    /// <summary> The sixth object of the tuple. </summary>
    public readonly T6 Item6 = item6;

    /// <summary> The seventh object of the tuple. </summary>
    public readonly T7 Item7 = item7;

    /// <summary> The eighth object of the tuple. </summary>
    public readonly T8 Item8 = item8;

    /// <summary> The ninth object of the tuple. </summary>
    public readonly T9 Item9 = item9;

    /// <summary> Deconstruct the tuple into its components. </summary>
    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8,
        out T9 item9)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        item5 = Item5;
        item6 = Item6;
        item7 = Item7;
        item8 = Item8;
        item9 = Item9;
    }
}
