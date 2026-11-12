namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper class for scrolling-related queries or actions in the current window. </summary>
    public static class Scroll
    {
        /// <summary> Get or set the current horizontal scroll position in the current window. </summary>
        public static float X
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Native.Methods.Scrolling.GetScrollX();
            [MethodImpl(ImSharpConfiguration.Inl)]
            set => Native.Methods.Scrolling.SetScrollX(value);
        }

        /// <summary> Get or set the current vertical scroll position in the current window. </summary>
        public static float Y
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Native.Methods.Scrolling.GetScrollY();
            [MethodImpl(ImSharpConfiguration.Inl)]
            set => Native.Methods.Scrolling.SetScrollY(value);
        }

        /// <summary> Get the current maximum horizontal scroll position in the current window. </summary>
        public static float MaximumX
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Native.Methods.Scrolling.GetScrollMaxX();
        }

        /// <summary> Get the current maximum vertical scroll position in the current window. </summary>
        public static float MaximumY
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Native.Methods.Scrolling.GetScrollMaxY();
        }

        /// <summary> Adjust the current horizontal scroll position to make the current cursor position visible in the current window. </summary>
        /// <param name="centerRatio"> The ratio where the current cursor position should appear, 0 is to the left, 1 is to the right. </param>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SetHereX(float centerRatio = 0.5f)
            => Native.Methods.Scrolling.SetScrollHereX(centerRatio);

        /// <summary> Adjust the current vertical scroll position to make the current cursor position visible in the current window. </summary>
        /// <param name="centerRatio"> The ratio where the current cursor position should appear, 0 is at the top, 1 is at the bottom. </param>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SetHereY(float centerRatio = 0.5f)
            => Native.Methods.Scrolling.SetScrollHereY(centerRatio);

        /// <summary> Adjust the current horizontal scroll position to make the given cursor position visible in the current window. </summary>
        /// <param name="localX"> The given local cursor position. </param>
        /// <param name="centerRatio"> The ratio where the position at <seealso cref="localX"/> should appear, 0 is to the left, 1 is to the right. </param>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SetFromPositionX(float localX, float centerRatio = 0.5f)
            => Native.Methods.Scrolling.SetScrollFromPosX(localX, centerRatio);

        /// <summary> Adjust the current horizontal scroll position to make the given cursor position visible in the current window. </summary>
        /// <param name="localY"> The given local cursor position. </param>
        /// <param name="centerRatio"> The ratio where the position at <seealso cref="localY"/> should appear, 0 is at the top, 1 is at the bottom. </param>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static void SetFromPositionY(float localY, float centerRatio = 0.5f)
            => Native.Methods.Scrolling.SetScrollFromPosY(localY, centerRatio);
    }
}
