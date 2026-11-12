namespace ImSharp;

public static partial class Im
{
    public static class Id
    {
        /// <summary> Create a new, empty <see cref="IdDisposable"/> </summary>
        public static IdDisposable Empty()
            => new();

        /// <inheritdoc cref="IdDisposable.Push(ImGuiId)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(100)]
        public static IdDisposable Push(ImGuiId id)
            => new IdDisposable().Push(id);

        /// <inheritdoc cref="IdDisposable.Push(nint)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        [OverloadResolutionPriority(50)]
        public static IdDisposable Push(nint id)
            => new IdDisposable().Push(id);

        /// <inheritdoc cref="IdDisposable.Push(Utf8LabelHandler)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static IdDisposable Push(Utf8LabelHandler id)
            => new IdDisposable().Push(ref id);

        /// <inheritdoc cref="IdDisposable.Push(Utf8LabelHandler)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static IdDisposable Push<T>(ref Utf8StringHandler<T> id) where T : IStringHandlerBuffer
            => new IdDisposable().Push(ref id);

        /// <summary> Calculate an ID based on the current ID stack and the given ID string. </summary>
        /// <param name="id"> The ID string as text. Does not have to be null-terminated. </param>
        /// <returns> The calculated ID. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe ImGuiId Get(Utf8LabelHandler id)
            => Native.Methods.IdStack.GetId(id.Start(out var end), end);

        /// <inheritdoc cref="Get(Utf8LabelHandler)"/>
        /// <typeparam name="T"> The buffer type. </typeparam>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe ImGuiId Get<T>(ref Utf8StringHandler<T> id) where T : IStringHandlerBuffer
            => Native.Methods.IdStack.GetId(id.Start(out var end), end);

        /// <summary> Calculate an ID based on the current ID stack and the given pointer. </summary>
        /// <param name="pointer"> The pointer to be used as an ID. This does not access the content of the pointer. </param>
        /// <returns> The calculated ID. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ImGuiId Get(nint pointer)
            => Native.Methods.IdStack.GetId(pointer);

        /// <summary> Calculate an ID with a given seed value instead of the current ID value. Supports '###' resetting to seed value inside the string. </summary>
        /// <typeparam name="T"> The buffer type. </typeparam>
        /// <param name="id"> The appended ID as text. Does not have to be null-terminated. </param>
        /// <param name="seed"> The initial seed value for the hash. </param>
        /// <returns> The combined hash of the seed and the new ID. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe ImGuiId Calculate<T>(ref Utf8StringHandler<T> id, ImGuiId seed) where T : IStringHandlerBuffer
        {
            var start = id.Start(out var end);
            return Native.Methods.Internal.ImHashStr(start, (ulong)(end - start), seed.Id);
        }

        /// <summary> Calculate an ID with a given seed value instead of the current ID value. Does not support '###', use <see cref="Calculate{T}"/> instead if required. </summary>
        /// <typeparam name="T"> The buffer type. </typeparam>
        /// <param name="id"> The appended ID as text. Does not have to be null-terminated. </param>
        /// <param name="seed"> The initial seed value for the hash. </param>
        /// <returns> The combined hash of the seed and the new ID. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe ImGuiId CalculateWithoutReset<T>(ref Utf8StringHandler<T> id, ImGuiId seed) where T : IStringHandlerBuffer
        {
            var start = id.Start(out var end);
            return Native.Methods.Internal.ImHashData(start, (ulong)(end - start), seed.Id);
        }

        /// <inheritdoc cref="Calculate{T}"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe ImGuiId Calculate(Utf8TextHandler id, ImGuiId seed)
            => Calculate(ref id, seed);

        /// <inheritdoc cref="CalculateWithoutReset{T}"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe ImGuiId CalculateWithoutReset(Utf8TextHandler id, ImGuiId seed)
            => CalculateWithoutReset(ref id, seed);

        /// <summary> Get the last ID from the ID stack of the current window. </summary>
        public static unsafe ImGuiId Current
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Context.Pointer->CurrentWindow->IdStack[^1];
        }

        /// <summary> Get the ID of the currently active widget. </summary>
        public static ImGuiId Active
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Context.ActiveId;
        }

        /// <summary> Check whether an ID represents the currently active widget. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsActive(ImGuiId id)
            => Context.ActiveId == id;

        /// <summary> Check whether an ID represents the widget that was active in the previous frame. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool WasActive(ImGuiId id)
            => Context.ActiveIdPreviousFrame == id;

        /// <summary> Check whether an ID represents the last drawn widget. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool IsCurrent(ImGuiId id)
            => Current == id;

        /// <summary> Clear the currently active widget. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void ClearActive()
            => Native.Methods.Internal.ClearActiveId();

        /// <summary> Set the currently active widget for the current window. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe void SetActive(ImGuiId id)
            => Native.Methods.Internal.SetActiveID(id, Window.Current.Pointer);
    }
}
