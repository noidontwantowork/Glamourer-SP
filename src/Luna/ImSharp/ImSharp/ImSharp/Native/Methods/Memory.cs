namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class Memory
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetAllocatorFunctions")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void SetAllocatorFunctions(delegate*<ulong, void*, void*> allocFunc,
                    delegate*<void*, void*, void> freeFunc,
                    void* userData);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetAllocatorFunctions")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void GetAllocatorFunctions(delegate*<ulong, void*, void*>* allocFunc,
                    delegate*<void*, void*, void>* freeFunc,
                    void** userData);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igMemAlloc")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void* MemAlloc(ulong size);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igMemFree")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void MemFree(void* ptr);
            }
        }
    }
}
