namespace ImSharp;

public partial class Im
{
    /// <summary> A clipper utility that cleans up after itself. </summary>
    public unsafe struct ListClipper : IEnumerable<int>, IDisposable
    {
        /// <summary> Create a list clipper from a native pointer. </summary>
        internal ListClipper(Native.ListClipper* pointer)
            => Pointer = pointer;

        /// <summary> The address of the native object. </summary>
        public Native.ListClipper* Pointer { get; private set; }

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static implicit operator ListClipper(Native.ListClipper* pointer)
            => new(pointer);

        /// <summary> The index of the first displayed element. </summary>
        public readonly ref int DisplayStart
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => ref Pointer->DisplayStart;
        }

        /// <summary> The index of the last displayed element. </summary>
        public readonly ref int DisplayEnd
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => ref Pointer->DisplayEnd;
        }

        /// <summary> The number of displayed items. </summary>
        public readonly ref int ItemsCount
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => ref Pointer->ItemsCount;
        }

        public readonly int ItemsInCurrentStep
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get
            {
                if (Pointer->TempData is null)
                    return 0;

                var step = Pointer->TempData->StepNo - 1;
                return Pointer->TempData->Ranges[step].Count;
            }
        }

        /// <summary> The total height of the items. </summary>
        public readonly ref float ItemsHeight
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => ref Pointer->ItemsHeight;
        }

        /// <summary> The height offset of the skipped height. </summary>
        public readonly ref float StartPosY
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => ref Pointer->StartPosY;
        }

        /// <summary> The current step in the process of iterating the list clipper. </summary>
        public int CurrentStep
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->TempData is null ? 0 : Pointer->TempData->StepNo;
        }

        /// <summary> Get whether the current step is a navigation or focus step instead of the visible item step. </summary>
        public bool IsInFocusStep
        {
            get
            {
                var data = Pointer->TempData;
                if (data is null)
                    return false;
                if (data->Ranges.Count <= 1)
                    return false;

                if (data->Ranges[data->StepNo - 1].Count > 1)
                    return false;

                return true;
            }
        }

        /// <summary> Create a new ListClipper. </summary>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public ListClipper()
            : this(Native.ListClipper.Constructor())
        { }

        /// <summary> Create a new ListClipper and begin it with the given number of items and uniform height. </summary>
        /// <param name="itemsCount"> The number of items of uniform height that are in the list. </param>
        /// <param name="itemsHeight"> The uniform height of each item in the list. </param>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public ListClipper(int itemsCount, float itemsHeight)
            : this(Native.ListClipper.Constructor())
            => Native.ListClipper.Begin(Pointer, itemsCount, itemsHeight);

        /// <summary> Execute a step in the clipper. </summary>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public readonly bool Step()
            => Native.ListClipper.Step(Pointer);

        /// <summary> Force the current display range using a pair of indices. </summary>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public readonly void ForceDisplayRangeByIndices(int itemMin, int itemMax)
            => Native.ListClipper.ForceDisplayRangeByIndices(Pointer, itemMin, itemMax);

        /// <summary> Dispose of the list clipper. </summary>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public void Dispose()
        {
            if (Pointer is null)
                return;

            Native.ListClipper.End(Pointer);
            Native.ListClipper.Destructor(Pointer);
            Pointer = null;
        }

        /// <summary> Iterate over all indices to be drawn with the current ListClipper settings. </summary>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public readonly IEnumerator<int> GetEnumerator()
        {
            while (Step())
            {
                for (var i = DisplayStart; i < DisplayEnd; ++i)
                    yield return i;
            }
        }

        /// <summary> Iterate over all items in the list to be drawn with the current ListClipper settings. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public readonly IEnumerable<T> Iterate<T>(IReadOnlyList<T> list)
        {
            while (Step())
            {
                for (var i = DisplayStart; i < DisplayEnd; ++i)
                    yield return list[i];
            }
        }

        /// <inheritdoc cref="Iterate{T}(IReadOnlyList{T})"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(50)]
        public readonly IEnumerable<T> Iterate<T>(IList<T> list)
        {
            while (Step())
            {
                for (var i = DisplayStart; i < DisplayEnd; ++i)
                    yield return list[i];
            }
        }

        /// <summary> Iterate over all items in the enumerable to be drawn with the current ListClipper settings. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(0)]
        public readonly IEnumerable<T> Iterate<T>(IEnumerable<T> list)
        {
            // Shortcut for random access.
            switch (list)
            {
                case IReadOnlyList<T> l:
                {
                    foreach (var i in Iterate(l))
                        yield return i;

                    break;
                }
                case IList<T> l2:
                {
                    foreach (var i in Iterate(l2))
                        yield return i;

                    break;
                }
            }


            using var enumerator = list.GetEnumerator();
            while (Step())
            {
                var currentIndex = 0;
                var skips        = DisplayStart - currentIndex;
                if (skips < 0)
                    continue;

                for (var i = 0; i < skips; ++i)
                {
                    if (!enumerator.MoveNext())
                        yield break;
                }

                currentIndex += skips;
                var takes = DisplayEnd - currentIndex;
                if (takes <= 0)
                    continue;

                for (var i = 0; i < takes; ++i)
                {
                    if (!enumerator.MoveNext())
                        yield break;

                    ++currentIndex;
                    yield return enumerator.Current;
                }
            }
        }

        [MethodImpl(ImSharpConfiguration.Inl)]
        readonly IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary> Draw the list of items clipped. </summary>
        /// <typeparam name="T"> The type of item. </typeparam>
        /// <param name="items"> The list of items. </param>
        /// <param name="draw"> The draw function taking the item. </param>
        /// <param name="itemHeight"> The height of each item. </param>
        public static void Draw<T>(IReadOnlyList<T> items, Action<T> draw, float itemHeight)
        {
            using var clipper = new ListClipper(items.Count, itemHeight);
            foreach (var item in clipper.Iterate(items))
                draw(item);
        }

        /// <summary> Draw the list of items clipped. </summary>
        /// <typeparam name="T"> The type of item. </typeparam>
        /// <param name="items"> The list of items. </param>
        /// <param name="draw"> The draw function taking the item and its global index. </param>
        /// <param name="itemHeight"> The height of each item. </param>
        public static void Draw<T>(IReadOnlyList<T> items, Action<T, int> draw, float itemHeight)
        {
            using var clipper = new ListClipper(items.Count, itemHeight);
            foreach (var globalIndex in clipper)
                draw(items[globalIndex], globalIndex);
        }

        /// <summary> Draw a set of items clipped. </summary>
        /// <typeparam name="T"> The type of item. </typeparam>
        /// <param name="items"> The items. </param>
        /// <param name="draw"> The draw function taking the item. </param>
        /// <param name="count"> The number of items. </param>
        /// <param name="itemHeight"> The height of each item. </param>
        public static void Draw<T>(IEnumerable<T> items, Action<T> draw, int count, float itemHeight)
        {
            using var clipper = new ListClipper(count, itemHeight);
            foreach (var item in clipper.Iterate(items))
                draw(item);
        }

        /// <summary> Draw a list of items in groups of specified size and clipped per row. </summary>
        /// <typeparam name="T"> The type of item. </typeparam>
        /// <param name="items"> The random-access list of items. </param>
        /// <param name="draw"> The function to invoke for each item in a row. </param>
        /// <param name="groupSize"> The number of items per row. </param>
        /// <param name="rowHeight"> The height of a row. </param>
        /// <param name="itemSpacing"> The vertical spacing between the drawn items. </param>
        public static void DrawGrouped<T>(IReadOnlyList<T> items, Action<T> draw, int groupSize, float rowHeight, float itemSpacing)
        {
            if (groupSize <= 1)
            {
                Draw(items, draw, rowHeight);
                return;
            }

            var numRows = (items.Count + groupSize - 1) / groupSize;
            using var clip = new ListClipper(numRows, rowHeight);
            foreach (var index in clip)
            {
                var scaledIndex = index * groupSize;
                var end = Math.Min(scaledIndex + groupSize - 1, items.Count - 1);
                for (var i = scaledIndex; i < end; ++i)
                {
                    draw(items[i]);
                    Line.Same(0, itemSpacing);
                }

                draw(items[end]);
            }
        }

        /// <summary> Draw a set of items in groups of specified size and clipped per row. </summary>
        /// <typeparam name="T"> The type of item. </typeparam>
        /// <param name="items"> The items. </param>
        /// <param name="draw"> The function to invoke for each item in a row. </param>
        /// <param name="count"> The total number of items. </param>
        /// <param name="groupSize"> The number of items per row. </param>
        /// <param name="rowHeight"> The height of a row. </param>
        /// <param name="itemSpacing"> The vertical spacing between the drawn items. </param>
        public static void DrawGrouped<T>(IEnumerable<T> items, Action<T> draw, int count, int groupSize, float rowHeight, float itemSpacing)
        {
            if (items is IReadOnlyList<T> list)
            {
                DrawGrouped(list, draw, groupSize, rowHeight, itemSpacing);
                return;
            }

            var numRows = (count + groupSize - 1) / groupSize;
            using var clip = new ListClipper(numRows, rowHeight);
            using var enumerator = items.GetEnumerator();
            var counter = 0;
            foreach (var index in clip)
            {
                var scaledIndex = index * groupSize;
                while (enumerator.MoveNext())
                {
                    if (counter++ < scaledIndex)
                        continue;

                    if (counter == scaledIndex + groupSize - 1)
                    {
                        draw(enumerator.Current);
                        break;
                    }

                    draw(enumerator.Current);
                    Line.Same(0, itemSpacing);
                }
            }
        }

        /// <summary> Draw a list of items in groups of specified size and clipped per row. </summary>
        /// <typeparam name="T"> The type of item. </typeparam>
        /// <param name="items"> The random-access list of items. </param>
        /// <param name="draw"> The function to invoke for each item in a row and its global index. </param>
        /// <param name="groupSize"> The number of items per row. </param>
        /// <param name="rowHeight"> The height of a row. </param>
        /// <param name="itemSpacing"> The vertical spacing between the drawn items. </param>
        public static void DrawGrouped<T>(IReadOnlyList<T> items, Action<T, int> draw, int groupSize, float rowHeight, float itemSpacing)
        {
            if (groupSize <= 1)
            {
                Draw(items, draw, rowHeight);
                return;
            }

            var numRows = (items.Count + groupSize - 1) / groupSize;
            using var clip = new ListClipper(numRows, rowHeight);
            foreach (var index in clip)
            {
                var scaledIndex = index * groupSize;
                var end = Math.Min(scaledIndex + groupSize - 1, items.Count - 1);
                for (var i = scaledIndex; i < end; ++i)
                {
                    draw(items[i], i);
                    Line.Same(0, itemSpacing);
                }

                draw(items[end], end);
            }
        }

        /// <summary> Draw a set of items in groups of specified size and clipped per row. </summary>
        /// <typeparam name="T"> The type of item. </typeparam>
        /// <param name="items"> The items. </param>
        /// <param name="draw"> The function to invoke for each item in a row and its global index. </param>
        /// <param name="count"> The total number of items. </param>
        /// <param name="groupSize"> The number of items per row. </param>
        /// <param name="rowHeight"> The height of a row. </param>
        /// <param name="itemSpacing"> The vertical spacing between the drawn items. </param>
        public static void DrawGrouped<T>(IEnumerable<T> items, Action<T, int> draw, int count, int groupSize, float rowHeight, float itemSpacing)
        {
            if (items is IReadOnlyList<T> list)
            {
                DrawGrouped(list, draw, groupSize, rowHeight, itemSpacing);
                return;
            }

            var numRows = (count + groupSize - 1) / groupSize;
            using var clip = new ListClipper(numRows, rowHeight);
            using var enumerator = items.GetEnumerator();
            var counter = 0;
            foreach (var index in clip)
            {
                var scaledIndex = index * groupSize;
                while (enumerator.MoveNext())
                {
                    if (counter++ < scaledIndex)
                        continue;

                    if (counter == scaledIndex + groupSize - 1)
                    {
                        draw(enumerator.Current, counter);
                        break;
                    }

                    draw(enumerator.Current, counter);
                    Line.Same(0, itemSpacing);
                }
            }
        }
    }
}
