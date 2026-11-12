namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    /// <summary> Wrapper class for methods related to attributes. </summary>
    public static class Attribute
    {
        /// <summary> Get whether any attribute is currently active. </summary>
        public static unsafe bool AnyActive
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Api.IsAnyAttributeActive(null);
        }

        /// <summary> Get whether the last drawn attribute is currently active. </summary>
        public static bool LastAttributeActive
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Api.IsAttributeActive();
        }

        /// <summary> Get whether any attribute pin is currently hovered. </summary>
        /// <remarks> Use after disposing the <seealso cref="NodeEditor"/>. </remarks>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static unsafe bool AnyPinHovered()
        {
            AttributeId id = 0;
            return Api.IsPinHovered(&id);
        }

        /// <inheritdoc cref="AttributeFlagDisposable.Push(AttributeFlags)"/>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static AttributeFlagDisposable PushFlag(AttributeFlags flag)
            => new AttributeFlagDisposable().Push(flag);

        /// <inheritdoc cref="AttributeFlagDisposable.Push(AttributeFlags,bool)"/>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static AttributeFlagDisposable PushFlag(AttributeFlags flag, bool condition)
            => new AttributeFlagDisposable().Push(flag, condition);

        /// <summary> Pop a number of ImNodes attribute flags. </summary>
        /// <param name="num"> The number of attribute flags to pop. The number is not checked against the ImNodes attribute flag stack. </param>
        /// <remarks> Avoid using this function, and attribute flags across scopes, as much as possible. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void PopFlagUnsafe(int num = 1)
        {
            while (num-- > 0)
                Api.PopAttributeFlag();
        }
    }
}
