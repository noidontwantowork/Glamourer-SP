namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    /// <summary> A wrapper around ImNodes attributes or pins. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ref struct AttributeDisposable : IDisposable
    {
        /// <summary> The unique ID of the attribute. </summary>
        public readonly AttributeId Id;

        /// <summary> The <see cref="Internal.AttributeType"/>> of the attribute. </summary>
        public Internal.AttributeType Type;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator AttributeId(AttributeDisposable attribute)
            => attribute.Id;

        /// <summary> Begin a new attribute inside the current node. </summary>
        /// <param name="id"> The desired unique ID of the new input attribute. Can be any integer except for <seealso cref="AttributeId.Invalid"/>. </param>
        /// <param name="shape"> The shape of the pin rendered next to the attribute. Links are created between pins. </param>
        /// <param name="type"> The type of the attribute. </param>
        /// <returns> A disposable object that ends the input attribute on disposal. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal AttributeDisposable(AttributeId id, PinShape shape, Internal.AttributeType type)
        {
            Type = type;
            Id   = id;

            switch (type)
            {
                case Internal.AttributeType.Input:  Api.BeginInputAttribute(id, shape); break;
                case Internal.AttributeType.Output: Api.BeginOutputAttribute(id, shape); break;
                case Internal.AttributeType.Static: Api.BeginStaticAttribute(id); break;
            }
        }

        /// <summary> End the attribute on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            switch (Type)
            {
                case Internal.AttributeType.Input:  Api.EndInputAttribute(); break;
                case Internal.AttributeType.Output: Api.EndOutputAttribute(); break;
                case Internal.AttributeType.Static: Api.EndStaticAttribute(); break;
            }

            Type = Internal.AttributeType.None;
        }
    }
}
