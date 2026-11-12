namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper around ImGui groups. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ref struct GroupDisposable : IDisposable
    {
        /// <summary> Whether the group is still open. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a group and end it on leaving scope. </summary>
        /// <returns> A disposable object. Use with using. </returns>
        /// <remarks> Groups can be used to group multiple items together and treat them as a single item. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal GroupDisposable(bool _)
        {
            Alive = true;
            Native.Methods.Layout.BeginGroup();
        }

        /// <summary> End the group on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            Native.Methods.Layout.EndGroup();
            Alive = false;
        }

        /// <summary> End a Group without using an IDisposable. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void EndUnsafe()
            => Native.Methods.Layout.EndGroup();
    }
}
