namespace ImSharp.temp;

public static class UniformListClipper
{
    public static void Draw<TIn>(IReadOnlyList<TIn> list, Action<TIn, int> drawAction, float height = -1)
    {
        var clipper = new Clipper<TIn>(drawAction, height);
        clipper.Draw(list);
    }

    public static void DrawText<TIn>(IReadOnlyList<TIn> list, Action<TIn, int> drawAction)
        => Draw(list, drawAction, Im.Style.TextHeightWithSpacing);

    public static void DrawFrame<TIn>(IReadOnlyList<TIn> list, Action<TIn, int> drawAction)
        => Draw(list, drawAction, Im.Style.FrameHeightWithSpacing);

    public static void Draw<TIn>(IReadOnlyCollection<TIn> list, Action<TIn, int> drawAction, float height = -1)
    {
        var clipper = new Clipper<TIn>(drawAction, height);
        clipper.Draw(list);
    }

    public static void DrawText<TIn>(IReadOnlyCollection<TIn> list, Action<TIn, int> drawAction)
        => Draw(list, drawAction, Im.Style.TextHeightWithSpacing);

    public static void DrawFrame<TIn>(IReadOnlyCollection<TIn> list, Action<TIn, int> drawAction)
        => Draw(list, drawAction, Im.Style.FrameHeightWithSpacing);

    public static void Draw<TIn>(IEnumerable<TIn> list, Action<TIn, int> drawAction, float height = -1, int count = int.MaxValue)
    {
        var clipper = new Clipper<TIn>(drawAction, height);
        clipper.Draw(list, count);
    }

    public static void DrawText<TIn>(IEnumerable<TIn> list, Action<TIn, int> drawAction, int count = int.MaxValue)
        => Draw(list, drawAction, Im.Style.TextHeightWithSpacing);

    public static void DrawFrame<TIn>(IEnumerable<TIn> list, Action<TIn, int> drawAction, int count = int.MaxValue)
        => Draw(list, drawAction, Im.Style.FrameHeightWithSpacing);

    private sealed class Clipper<T>(Action<T, int> drawAction, float height) : Base<T>(height)
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        protected override void DrawLine(in T item, int globalIdx)
            => drawAction(item, globalIdx);
    }

    public abstract class Base<T>(float height = -1)
    {
        protected readonly float Height = height;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        protected abstract void DrawLine(in T item, int globalIdx);

        public virtual unsafe void Draw(in IReadOnlyList<T> list)
        {
            if (list.Count == 0)
                return;

            var clipper = Im.Native.ListClipper.Constructor();
            try
            {
                Im.Native.ListClipper.Begin(clipper, list.Count, Height);
                while (Im.Native.ListClipper.Step(clipper))
                {
                    for (var i = clipper->DisplayStart; i < clipper->DisplayEnd; ++i)
                        DrawLine(list[i], i);
                }
            }
            finally
            {
                Im.Native.ListClipper.End(clipper);
                Im.Native.ListClipper.Destructor(clipper);
            }
        }

        public void Draw(IReadOnlyCollection<T> items)
            => Draw(items, items.Count);

        public virtual unsafe void Draw(IEnumerable<T> items, int count = int.MaxValue)
        {
            if (count == 0)
                return;

            var clipper = Im.Native.ListClipper.Constructor();
            try
            {
                Im.Native.ListClipper.Begin(clipper, count, Height);
                using var enumerator     = items.GetEnumerator();
                var       globalIdx      = -1;
                var       lastDisplayEnd = 0;
                while (Im.Native.ListClipper.Step(clipper))
                {
                    if (clipper->DisplayStart < lastDisplayEnd)
                        throw new Exception(
                            $"Clipping exception with no random access enumeration: A clipper step decreased the display start to before the last end ({clipper->DisplayStart} < {lastDisplayEnd}).");

                    for (var i = lastDisplayEnd; i < clipper->DisplayStart; ++i)
                    {
                        if (!enumerator.MoveNext())
                            return;

                        ++globalIdx;
                    }

                    for (var i = clipper->DisplayStart; i < clipper->DisplayEnd; ++i)
                    {
                        if (!enumerator.MoveNext())
                            return;

                        DrawLine(enumerator.Current, ++globalIdx);
                    }

                    lastDisplayEnd = clipper->DisplayEnd;
                }
            }
            finally
            {
                Im.Native.ListClipper.End(clipper);
                Im.Native.ListClipper.Destructor(clipper);
            }
        }
    }
}

public static class VariableListClipper
{
    public static void Draw<TIn>(IEnumerable<TIn> list, Action<TIn, int> drawAction, Func<TIn, int, float> measure)
    {
        var clipper = new Clipper<TIn>(drawAction, measure);
        clipper.Draw(list);
    }

    private sealed class Clipper<T>(Action<T, int> drawAction, Func<T, int, float> measure) : Base<T>
    {
        protected override void DrawLine(in T item, int globalIdx)
            => drawAction(item, globalIdx);

        protected override float GetHeight(in T item, int globalIdx)
            => measure(item, globalIdx);
    }

    public abstract class Base<T>
    {
        [MethodImpl(ImSharpConfiguration.OptInl)]
        protected abstract void DrawLine(in T item, int globalIdx);

        [MethodImpl(ImSharpConfiguration.OptInl)]
        protected abstract float GetHeight(in T item, int globalIdx);

        public virtual void Draw(IEnumerable<T> items)
        {
            // TODO Maybe subtract cursor position
            var       scrollY          = Im.Scroll.Y;
            var       end              = scrollY + Im.ContentRegion.Available.Y;
            var       cumulativeHeight = 0f;
            var       endSum           = 0f;
            using var enumerator       = items.GetEnumerator();
            var       globalIdx        = -1;
            while (enumerator.MoveNext())
            {
                ++globalIdx;

                if (cumulativeHeight < scrollY)
                {
                    var currentHeight = GetHeight(enumerator.Current, globalIdx);
                    cumulativeHeight += currentHeight;
                    if (cumulativeHeight >= scrollY)
                    {
                        Im.Dummy(0, cumulativeHeight - currentHeight);
                        DrawLine(enumerator.Current, globalIdx);
                    }
                }
                else if (cumulativeHeight >= end)
                {
                    endSum += GetHeight(enumerator.Current, globalIdx);
                }
                else
                {
                    DrawLine(enumerator.Current, globalIdx);
                }
            }

            if (cumulativeHeight < scrollY)
                Im.Dummy(0, scrollY);
            else if (endSum > 0)
                Im.Dummy(0, endSum);
        }
    }
}

public class ListClipperTester
{
    private readonly List<(string, float)> _items = [];

    public ListClipperTester(int count)
    {
        AddItems(count);
    }

    private void AddItems(int count)
    {
        count += _items.Count;
        _items.EnsureCapacity(count);
        var rng = new Random();
        for (var i = _items.Count; i < count; ++i)
        {
            var height = (rng.NextSingle() + 1) * Im.Style.TextHeight;
            var text   = $"Text {i + 1}";
            _items.Add((text, height));
        }
    }

    public void Draw()
    {
        if (Im.Button("Clear Items"u8))
            _items.Clear();

        Im.Line.Same();
        if (Im.Button("Double Items"u8))
            AddItems(_items.Count);

        Im.Line.Same();
        if (Im.Button("Add 100"u8))
            AddItems(100);

        Im.Line.Same();
        if (Im.Button("Remove 100"u8))
            _items.RemoveRange(_items.Count - 100, 100);

        using (var child = Im.Child.Begin(1, new Vector2(200, -1), true))
        {
            if (child)
                UniformListClipper.DrawText(_items, (p, idx) => Im.Text($"{p.Item1} {idx}"));
        }

        Im.Line.Same();
        using (var child = Im.Child.Begin(2, new Vector2(200, -1), true))
        {
            if (child)
                new VaryingClipper().Draw(_items);
        }
    }

    private sealed class VaryingClipper : VariableListClipper.Base<(string, float)>
    {
        protected override void DrawLine(in (string, float) item, int idx)
        {
            Im.Selectable($"{item.Item1} {idx}", size: new Vector2(200, item.Item2));
        }

        protected override float GetHeight(in (string, float) item, int idx)
            => item.Item2 + Im.Style.ItemSpacing.Y;
    }
}
