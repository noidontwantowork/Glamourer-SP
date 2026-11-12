namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around re-enabling the state. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class EnabledDisposable : IDisposable
    {
        /// <summary> The stored number of ended disposables as a workaround. </summary>
        public int Count { get; private set; }

        /// <summary> Enforce an enabled state by popping the global number of disabled states. </summary>
        /// <param name="condition"> Whether to force the enabled state. </param>
        /// <returns> A disposable object that will push the prior number of enabled states after leaving scope. Use with using. </returns>
        /// <remarks> This is a workaround for the problem that you can not force the state to be enabled without knowing the disabled stack's size. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public EnabledDisposable(bool condition)
        {
            if (!condition)
                return;

            Count = DisabledDisposable.GlobalCount;
            var count = Count;
            while (count-- > 0)
                Native.Methods.Disabled.EndDisabled();
        }

        /// <inheritdoc cref="EnabledDisposable(bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public EnabledDisposable()
        {
            Count = DisabledDisposable.GlobalCount;
            var count = Count;
            while (count-- > 0)
                Native.Methods.Disabled.EndDisabled();
        }

        /// <summary> Return to the prior disabled state. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            while (Count-- > 0)
                Native.Methods.Disabled.BeginDisabled(true);
        }
    }
}
