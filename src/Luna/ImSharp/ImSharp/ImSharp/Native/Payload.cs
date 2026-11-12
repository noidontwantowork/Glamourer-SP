namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe partial struct Payload
        {
            public void*         Data;
            public int           DataSize;
            public ImGuiId       SourceId;
            public ImGuiId       SourceParentId;
            public int           DataFrameCount;
            public DataTypeArray DataType;
            public ImBool        Preview;
            public ImBool        Delivery;

            [InlineArray(32 + 1)]
            public struct DataTypeArray
            {
                private byte _element;
            }

            [LibraryImport(Im.Version.CImGuiLibrary, EntryPoint = "ImGuiPayload_IsDataType")]
            [MethodImpl(ImSharpConfiguration.Inl)]
            public static partial ImBool IsDataType(Payload* self, byte* type);
        }
    }
}
