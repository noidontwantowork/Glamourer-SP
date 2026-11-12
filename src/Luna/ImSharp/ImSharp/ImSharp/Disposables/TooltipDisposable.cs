namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui tooltips. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ref struct TooltipDisposable : IDisposable
    {
        /// <summary> Whether the tooltip is still open. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a tooltip and end it on leaving scope. </summary>
        /// <returns> A disposable object. Use with using. </returns>
        /// <remarks> Anything drawn while a tooltip is active will be drawn in a little popup window on your cursor. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal TooltipDisposable(bool _)
        {
            Alive = true;
            Native.Methods.Tooltip.BeginTooltip();
        }

        /// <summary> End the tooltip on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            Native.Methods.Tooltip.EndTooltip();
            Alive = false;
        }

        /// <summary> End a Tooltip without using an IDisposable. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.Tooltip.EndTooltip();
    }
}
