namespace ImSharp;

public static partial class Im
{
    /// <summary> A reference to the generally global context used by ImGui. </summary>
    /// <param name="pointer"> The native pointer to the context. </param>
    public readonly unsafe ref struct ImGuiContext(Native.Internal.Context* pointer)
    {
        /// <summary> A reference to the currently used global context object. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ImGuiContext Get()
            => Native.Methods.Context.GetCurrentContext();

        /// <summary> The address of the native object. </summary>
        public readonly Native.Internal.Context* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator ImGuiContext(Native.Internal.Context* pointer)
            => new(pointer);

        /// <summary> Whether this context is initialized and not null. </summary>
        /// <remarks> This is the only property that checks for null. </remarks>
        public bool Initialized
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer is not null && Pointer->Initialized;
        }

        /// <summary> Whether the font atlas used by this context is owned by it. </summary>
        /// <remarks> See also <seealso cref="Im.InputOutput.Fonts"/>. </remarks>
        public bool FontAtlasOwnedByContext
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->FontAtlasOwnedByContext;
        }

        /// <summary> Get the input-output data of this context. </summary>
        public InputOutput Io
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => &Pointer->Io;
        }

        /// <summary> Get the style data of this context. </summary>
        public ImGuiStyle ImGuiStyle
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => &Pointer->Style;
        }

        /// <summary> Get the current font of this context. </summary>
        /// <remarks> This is a shortcut that accesses the last font on the font stack, or <seealso cref="Native.Io.FontDefault"/> if the stack is empty. </remarks>
        public Font Font
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->Font;
        }

        /// <summary> Get the size of the current font of this context. </summary>
        public float FontSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->FontSize;
        }

        /// <summary> Get the current frame count. </summary>
        public int FrameCount
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->FrameCount;
        }

        /// <summary> Get the ID of the currently active input text widget. </summary>
        public ImGuiId InputTextId
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->InputTextState.Id;
        }

        /// <summary> Get the ID of the currently active widget. </summary>
        public ImGuiId ActiveId
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ActiveId;
        }

        /// <summary> Get the ID of the currently active widget. </summary>
        public ImGuiId ActiveIdPreviousFrame
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ActiveIdPreviousFrame;
        }

        /// <summary> Get the ID of the currently hovered widget. </summary>
        public ImGuiId HoveredId
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->HoveredId;
        }

        /// <summary> The current count of disabled pushes, see <seealso cref="DisabledDisposable"/>. </summary>
        public short DisabledStackSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->DisabledStackSize;
        }

        /// <summary> The current count of style pushes, see <seealso cref="StyleDisposable"/>. </summary>
        public int StyleStackSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->StyleStack.Count;
        }

        /// <summary> The current count of color pushes, see <seealso cref="ColorDisposable"/>. </summary>
        public int ColorStackSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ColorStack.Count;
        }

        /// <summary> The current style pushes, see <seealso cref="StyleDisposable"/>. </summary>
        public IReadOnlyList<Im.Native.Internal.StyleMod> StyleStack
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->StyleStack;
        }

        /// <summary> The current color pushes, see <seealso cref="ColorDisposable"/>. </summary>
        public IReadOnlyList<Im.Native.Internal.ColorMod> ColorStack
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ColorStack;
        }

        /// <summary> Whether a drag and drop action is currently active. </summary>
        public bool DragDropActive
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->DragDropActive;
        }

        /// <summary> Create a new context with an existing font atlas. </summary>
        /// <param name="fontAtlas"> The font atlas to re-use. </param>
        /// <returns> The created context. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ImGuiContext Create(FontAtlas fontAtlas)
            => Native.Methods.Context.CreateContext(fontAtlas.Pointer);

        /// <summary> Create a new context that creates its own font atlas. </summary>
        /// <returns> The created context. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ImGuiContext Create()
            => Native.Methods.Context.CreateContext(null);

        /// <summary> Destroy this context. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Destroy()
            => Native.Methods.Context.DestroyContext(Pointer);

        /// <summary> Destroy the context currently in use. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void DestroyCurrent()
            => Native.Methods.Context.DestroyContext(null);

        /// <summary> Set the current context for this binary. </summary>
        /// <param name="context"> The context to use. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetCurrent(ImGuiContext context)
            => Native.Methods.Context.SetCurrentContext(context.Pointer);
    }
}
