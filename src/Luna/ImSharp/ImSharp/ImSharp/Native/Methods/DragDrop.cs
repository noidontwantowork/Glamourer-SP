namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public static partial class Methods
        {
            public static unsafe partial class DragDrop
            {
                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginDragDropSource")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginDragDropSource(DragDropSourceFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igSetDragDropPayload")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool SetDragDropPayload(byte* type, void* data, ulong size, Condition condition);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndDragDropSource")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndDragDropSource();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igBeginDragDropTarget")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial ImBool BeginDragDropTarget();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igAcceptDragDropPayload")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Payload* AcceptDragDropPayload(byte* type, DragDropTargetFlags flags);

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igEndDragDropTarget")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial void EndDragDropTarget();

                [LibraryImport(Version.CImGuiLibrary, EntryPoint = "igGetDragDropPayload")]
                [MethodImpl(ImSharpConfiguration.Inl)]
                public static partial Payload* GetDragDropPayload();
            }
        }
    }
}
