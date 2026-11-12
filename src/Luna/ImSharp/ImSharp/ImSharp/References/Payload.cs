namespace ImSharp;

public static partial class Im
{
    /// <summary> A reference to a drag and drop payload storage managed by ImGui. </summary>
    /// <param name="native"> The native pointer to the payload. </param>
    public readonly unsafe ref struct Payload(Native.Payload* native)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Native.Payload* Address = native;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator Payload(Native.Payload* pointer)
            => new(pointer);

        /// <summary> Whether the payload exists. </summary>
        public bool Valid
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Address is not null;
        }

        /// <summary> Try to get a copy of the unmanaged data stored by ImGui. </summary>
        /// <typeparam name="T"> The type of the data to try and get. </typeparam>
        /// <param name="data"> The copied data output on success. </param>
        /// <returns> True if the data stored in ImGui has the correct size and is set. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool TryGetData<T>(out T data) where T : unmanaged
        {
            if (Address is null || Address->DataSize != sizeof(T) || Address->Data is null)
            {
                data = default;
                return false;
            }

            data = *(T*)Address->Data;
            return true;
        }

        /// <summary> Check the 
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool CheckType(Utf8LabelHandler type)
            => Native.Payload.IsDataType(Address, type.Start());

        /// <summary> Get a reference to the user-defined type of the payload as null-terminated UTF8-string. </summary>
        public ReadOnlySpan<byte> Type
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => NullTerminationHelpers.GetSpan((byte*)&Address->DataType);
        }

        /// <summary> Get whether the payload reference is for previewing through <seealso cref="Im.DragDrop.PeekPayload"/> or <seealso cref="Im.DragDrop.TryPeekPayload"/>. </summary>
        public bool Preview
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Address->Preview;
        }

        /// <summary> Get whether the payload reference is a delivery through <seealso cref="Im.DragDrop.AcceptPayload"/> or <seealso cref="Im.DragDrop.TryAcceptPayload"/>. </summary>
        public bool Delivery
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Address->Delivery;
        }
    }
}
