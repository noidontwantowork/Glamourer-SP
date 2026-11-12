namespace ImSharp;

public static partial class Im
{
    /// <summary> A reference to an ImGui state storage struct. </summary>
    /// /// <param name="pointer"> The native pointer to the storage. </param>
    public readonly unsafe ref struct StateStorage(Native.Storage* pointer)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Native.Storage* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static implicit operator StateStorage(Native.Storage* pointer)
            => new(pointer);


        /// <summary> Obtain the number of stored data points. </summary>
        public int Count
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get { return Pointer->Data.Count; }
        }

        /// <summary> Get the state associated with an ID as an integer. </summary>
        /// <param name="id"> The ID to query. </param>
        /// <param name="defaultValue"> The default value to insert if this ID does not have an associated state yet. </param>
        /// <returns> The associated state. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public int GetInt(ImGuiId id, int defaultValue = 0)
            => Native.Storage.GetInt(Pointer, id, defaultValue);

        /// <summary> Get the state associated with an ID as a boolean. </summary>
        /// <inheritdoc cref="GetInt"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public bool GetBool(ImGuiId id, bool defaultValue = false)
            => Native.Storage.GetBool(Pointer, id, defaultValue);

        /// <summary> Get the state associated with an ID as a float. </summary>
        /// <inheritdoc cref="GetInt"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public float GetFloat(ImGuiId id, float defaultValue = 0f)
            => Native.Storage.GetFloat(Pointer, id, defaultValue);

        /// <summary> Get the state associated with an ID as a pointer. </summary>
        /// <inheritdoc cref="GetInt"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public nint GetPointer(ImGuiId id, nint defaultValue = 0)
            => Native.Storage.GetPointer(Pointer, id, defaultValue);

        /// <summary> Get a reference to the state associated with an ID as an integer. </summary>
        /// <inheritdoc cref="GetInt"/>
        /// <returns> A reference to the associated state. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ref int GetIntReference(ImGuiId id, int defaultValue = 0)
            => ref *Native.Storage.GetIntRef(Pointer, id, defaultValue);

        /// <summary> Get a reference to the state associated with an ID as a boolean. </summary>
        /// <inheritdoc cref="GetIntReference"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ref bool GetBoolReference(ImGuiId id, bool defaultValue = false)
            => ref *(bool*)Native.Storage.GetBoolRef(Pointer, id, defaultValue);

        /// <summary> Get a reference to the state associated with an ID as a float. </summary>
        /// <inheritdoc cref="GetIntReference"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ref float GetFloatReference(ImGuiId id, float defaultValue = 0f)
            => ref *Native.Storage.GetFloatRef(Pointer, id, defaultValue);

        /// <summary> Get a reference to the state associated with an ID as a pointer. </summary>
        /// <inheritdoc cref="GetIntReference"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ref nint GetPointerReference(ImGuiId id, nint defaultValue = 0)
            => ref *Native.Storage.GetPointerRef(Pointer, id, defaultValue);

        /// <summary> Set the state for an ID. </summary>
        /// <param name="id"> The ID to set the state for. </param>
        /// <param name="value"> The state to set as an Integer. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void SetInt(ImGuiId id, int value)
            => Native.Storage.SetInt(Pointer, id, value);

        /// <summary> Set the state for an ID. </summary>
        /// <param name="id"> The ID to set the state for. </param>
        /// <param name="value"> The state to set as a boolean. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void SetBool(ImGuiId id, bool value)
            => Native.Storage.SetBool(Pointer, id, value);

        /// <summary> Set the state for an ID. </summary>
        /// <param name="id"> The ID to set the state for. </param>
        /// <param name="value"> The state to set as a float. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void SetFloat(ImGuiId id, float value)
            => Native.Storage.SetFloat(Pointer, id, value);

        /// <summary> Set the state for an ID. </summary>
        /// <param name="id"> The ID to set the state for. </param>
        /// <param name="value"> The state to set as a pointer. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void SetPointer(ImGuiId id, nint value)
            => Native.Storage.SetPointer(Pointer, id, value);

        /// <summary> Clear all current state. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Clear()
            => Native.Storage.Clear(Pointer);

        /// <summary> Set all current state flags to an integer value. </summary>
        /// <param name="value"> The value to set everything to. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void SetAllInt(int value)
            => Native.Storage.SetAllInt(Pointer, value);
    }
}
