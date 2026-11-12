namespace ImSharp;

/// <summary> Miscellaneous utility functions. </summary>
public static class ImUtility
{
    /// <summary> Apply the delta of a mousewheel scroll to an index considering scroll direction and total item count. </summary>
    /// <param name="delta"> The mousewheel delta converted to an integer. </param>
    /// <param name="index"> The initial index to manipulate. </param>
    /// <param name="count"> The total count of items. </param>
    /// <returns> The modified index. </returns>
    /// <remarks>
    ///   Scrolling downwards increases the index, upwards decreases the index.
    ///   If the index is negative (invalid), it is treated as no starting point so scrolling up yields <c>count - 1</c> and down yields <c>0</c> if the delta is 1 or -1.
    /// </remarks>
    public static int ApplyMouseWheelDelta(int delta, int index, int count)
    {
        switch (count)
        {
            case 0: return -1;
            case 1: return 0;
            default:
                delta = -delta % count;
                return delta switch
                {
                    < 0 when index < 0 => count + delta,
                    < 0                => (index + count + delta) % count,
                    > 0 when index < 0 => delta - 1,
                    _                  => (index + delta) % count,
                };
        }
    }
}
