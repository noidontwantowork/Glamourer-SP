namespace ImSharp;

public static partial class Im
{
    /// <summary> A read-only reference to style data for ImGui. </summary>
    /// <param name="pointer"> The native pointer to the style. </param>
    public readonly unsafe ref struct ImGuiStyle(Native.ImGuiStyle* pointer)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Native.ImGuiStyle* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator ImGuiStyle(Native.ImGuiStyle* pointer)
            => new(pointer);

        /// <summary> Obtain a read-only reference to the current style container. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ImGuiStyle Get()
            => new(Native.Methods.Main.GetStyle());

        /// <summary> Obtain a writeable reference to this style.</summary>
        /// <remarks> Generally avoid writing to the style and use Push/Pull methods instead. </remarks>
        public ImGuiStyleWritable AsWritable()
            => new(Pointer);

        /// <summary> The global alpha value applies to everything in ImGui. </summary>
        public float Alpha
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->Alpha;
        }

        /// <summary> An additional alpha multiplier applied by <seealso cref="Disabled()"/> blocks. </summary>
        /// <remarks> This multiplies over the current normal alpha, it does not replace it. </remarks>
        public float DisabledAlpha
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->DisabledAlpha;
        }

        /// <summary> The padding to the sides of a window. </summary>
        public Vector2 WindowPadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WindowPadding;
        }

        /// <summary> The radius of the rounding of window corners in pixels. </summary>
        public float WindowRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WindowRounding;
        }

        /// <summary> The thickness of the border around windows. </summary>
        /// <remarks> Usually should be either 0 or 1. </remarks>
        public float WindowBorderThickness
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WindowBorderSize;
        }

        /// <summary> The global minimum size for windows. </summary>
        public Vector2 MinimumWindowSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WindowMinSize;
        }

        /// <summary> The alignment for the title bar text. Defaults to (0, 0.5). </summary>
        public Vector2 WindowTitleAlignment
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WindowTitleAlign;
        }

        /// <summary> The side of the collapsing and docking buttons in the title bar. Defaults to left. </summary>
        /// <remarks> Only <seealso cref="Direction.None"/>, <seealso cref="Direction.Left"/> and <seealso cref="Direction.Right"/> are supported. </remarks>
        public Direction WindowMenuButtonPosition
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WindowMenuButtonPosition;
        }

        /// <summary> The radius of the rounding of child window corners. </summary>
        public float ChildRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ChildRounding;
        }

        /// <summary> The thickness of the border around child windows. </summary>
        /// <remarks> Usually should be either 0 or 1. </remarks>
        public float ChildBorderThickness
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ChildBorderSize;
        }

        /// <summary> The radius of the rounding of popup window corners. </summary>
        public float PopupRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->PopupRounding;
        }

        /// <summary> The thickness of the border around popup windows. </summary>
        /// <remarks> Usually should be either 0 or 1. </remarks>
        public float PopupBorderThickness
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->PopupBorderSize;
        }

        /// <summary> The padding used within framed rectangles. </summary>
        public Vector2 FramePadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->FramePadding;
        }

        /// <summary> The radius of the rounding of frame corners. </summary>
        public float FrameRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->FrameRounding;
        }

        /// <summary> The thickness of the border around frames. </summary>
        /// <remarks> Usually should be either 0 or 1. </remarks>
        public float FrameBorderThickness
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->FrameBorderSize;
        }

        /// <summary> The horizontal spacing between items on the same line and the vertical spacing between lines. </summary>
        public Vector2 ItemSpacing
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ItemSpacing;
        }

        /// <summary> The horizontal and vertical spacing between elements of a composed item, e.g. between inputs and their labels. </summary>
        public Vector2 ItemInnerSpacing
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ItemInnerSpacing;
        }

        /// <summary> The padding used within a table cell. </summary>
        /// <remarks> Horizontal padding is locked for an entire table, but vertical padding may be altered between rows. </remarks>
        public Vector2 CellPadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->CellPadding;
        }

        /// <summary> Expansion of the reactive bounding box for touch-based systems. </summary>
        /// <remarks> Since items are not sorted, overlap will resolve to the first item, so do not create too much overlap. </remarks>
        public Vector2 TouchExtraPadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->TouchExtraPadding;
        }

        /// <summary> The horizontal indentation when pushing indents, e.g. when entering tree nodes. </summary>
        /// <remarks> Generally set to FontSize + 2 * <seealso cref="FramePadding"/>.X. </remarks>
        public float IndentSpacing
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->IndentSpacing;
        }

        /// <summary> The minimum horizontal spacing between two columns. </summary>
        /// <remarks> Should be > <seealso cref="FramePadding"/>.X + 1. </remarks>
        public float ColumnsMinimumSpacing
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ColumnsMinSpacing;
        }

        /// <summary> Width of the vertical and height of the horizontal scroll bar. </summary>
        public float ScrollbarSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ScrollbarSize;
        }

        /// <summary> The radius of the grab corners for scroll bars. </summary>
        public float ScrollbarRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ScrollbarRounding;
        }

        /// <summary> The minimum width or height of the grab box for sliders and scroll bars. </summary>
        public float MinimumGrabSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->GrabMinSize;
        }

        /// <summary> The radius of the grab corners for scroll bars. </summary>
        public float GrabRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->GrabRounding;
        }

        /// <summary> The size of the dead zone around zero on logarithmic sliders in pixels. </summary>
        public float LogSliderDeadzone
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->LogSliderDeadzone;
        }

        /// <summary> The radius of the upper corners of tab items and buttons. </summary>
        public float TabRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->TabRounding;
        }

        /// <summary> The thickness of the border around tabs items and buttons. </summary>
        public float TabBorderThickness
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->TabBorderSize;
        }

        /// <summary> The minimum width of a tab item for the close button to appear when hovered. </summary>
        public float TabMinimumWidthForCloseButton
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->TabMinWidthForCloseButton;
        }

        /// <summary> The side of the color button in <seealso cref="Im.Color.Editor(Utf8LabelHandler,ref Vector4,ColorEditorFlags)"/> widgets. </summary>
        /// <remarks> Defaults to <seealso cref="Direction.Right"/>. Only Left and Right are supported. </remarks>
        public Direction ColorButtonPosition
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ColorButtonPosition;
        }

        /// <summary> The alignment of text within a button. </summary>
        /// <remarks> Defaults to (0.5, 0.5). </remarks>
        public Vector2 ButtonTextAlignment
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ButtonTextAlign;
        }

        /// <summary> The alignment of text within a selectable. </summary>
        /// <remarks> Defaults to (0, 0). </remarks>
        public Vector2 SelectableTextAlignment
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->SelectableTextAlign;
        }

        /// <summary> The amount kept visible when moving a window near the edges of a screen. </summary>
        public Vector2 DisplayWindowPadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->DisplayWindowPadding;
        }

        /// <summary> The amount where no contents are displayed. </summary>
        public Vector2 DisplaySafeAreaPadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->DisplaySafeAreaPadding;
        }

        /// <summary> The scale for the software-rendered mouse cursor. </summary>
        public float MouseCursorScale
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->MouseCursorScale;
        }

        /// <summary> Enable anti-aliasing of lines and borders. </summary>
        /// <remarks> Enabled by default. Disable if you lack performance. </remarks>
        public bool AntiAliasedLines
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->AntiAliasedLines;
        }

        /// <summary> Enable anti-aliasing of lines and borders using textures where possible. </summary>
        /// <remarks> Enabled by default. Disable if you lack performance. </remarks>
        public bool AntiAliasedLinesUseTex
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->AntiAliasedLinesUseTex;
        }

        /// <summary> Enable anti-aliasing edges around filled shapes. </summary>
        /// <remarks> Enabled by default. Disable if you lack performance. </remarks>
        public bool AntiAliasedFill
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->AntiAliasedFill;
        }

        /// <summary> The tesselation tolerance when using Bézier curves. </summary>
        /// <remarks> Increase to reduce quality and increase performance.</remarks>
        public ref float CurveTessellationTolerance
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => ref Pointer->CurveTessellationTol;
        }

        /// <summary> The maximum error allowed when drawing rounded objects in pixels. </summary>
        /// <remarks> Increase to reduce quality and increase performance. </remarks>
        public float CircleTessellationMaximumError
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->CircleTessellationMaxError;
        }

        /// <summary> Obtain style colors. </summary>
        /// <param name="color"> The color. </param>
        public Vector4 this[ImGuiColor color]
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->Colors[color];
        }

        /// <summary> Obtain a single-value style variable by type. </summary>
        /// <param name="imStyle"> The style variable. </param>
        /// <returns> The value. </returns>
        /// <exception cref="ArgumentOutOfRangeException" />
        public float this[ImStyleSingle imStyle]
        {
            [MethodImpl(ImSharpConfiguration.Opt)]
            get => imStyle switch
            {
                ImStyleSingle.Alpha                 => Alpha,
                ImStyleSingle.DisabledAlpha         => DisabledAlpha,
                ImStyleSingle.WindowRounding        => WindowRounding,
                ImStyleSingle.WindowBorderThickness => WindowBorderThickness,
                ImStyleSingle.ChildRounding         => ChildRounding,
                ImStyleSingle.ChildBorderThickness  => ChildBorderThickness,
                ImStyleSingle.PopupRounding         => PopupRounding,
                ImStyleSingle.PopupBorderThickness  => PopupBorderThickness,
                ImStyleSingle.FrameRounding         => FrameRounding,
                ImStyleSingle.FrameBorderThickness  => FrameBorderThickness,
                ImStyleSingle.IndentSpacing         => IndentSpacing,
                ImStyleSingle.ScrollbarSize         => ScrollbarSize,
                ImStyleSingle.ScrollbarRounding     => ScrollbarRounding,
                ImStyleSingle.MinimumGrabSize       => MinimumGrabSize,
                ImStyleSingle.GrabRounding          => GrabRounding,
                ImStyleSingle.TabRounding           => TabRounding,
                _                                   => throw new ArgumentOutOfRangeException(nameof(imStyle), imStyle, null),
            };
        }

        /// <summary> Obtain a double-value style variable by type. </summary>
        /// <param name="imStyle"> The style variable. </param>
        /// <returns> The value. </returns>
        /// <exception cref="ArgumentOutOfRangeException" />
        public Vector2 this[ImStyleDouble imStyle]
        {
            [MethodImpl(ImSharpConfiguration.Opt)]
            get => imStyle switch
            {
                ImStyleDouble.WindowPadding        => WindowPadding,
                ImStyleDouble.MinimumWindowSize    => MinimumWindowSize,
                ImStyleDouble.WindowTitleAlignment => WindowTitleAlignment,
                ImStyleDouble.FramePadding         => FramePadding,
                ImStyleDouble.ItemSpacing          => ItemSpacing,
                ImStyleDouble.ItemInnerSpacing     => ItemInnerSpacing,
                ImStyleDouble.CellPadding          => CellPadding,
                ImStyleDouble.ButtonTextAlign      => ButtonTextAlignment,
                ImStyleDouble.SelectableTextAlign  => SelectableTextAlignment,
                _                                  => throw new ArgumentOutOfRangeException(nameof(imStyle), imStyle, null),
            };
        }

        /// <summary> Create a new, empty <see cref="StyleDisposable"/> to push colors to. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Empty()
            => new();

        /// <inheritdoc cref="StyleDisposable.Push(ImStyleSingle,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Push(ImStyleSingle type, float value, bool condition)
            => new StyleDisposable().Push(type, value, condition);

        /// <inheritdoc cref="StyleDisposable.Push(ImStyleSingle,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Push(ImStyleDouble type, Vector2 value, bool condition)
            => new StyleDisposable().Push(type, value, condition);

        /// <inheritdoc cref="StyleDisposable.Push(ImStyleSingle,float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Push(ImStyleSingle type, float value)
            => new StyleDisposable().Push(type, value);

        /// <inheritdoc cref="StyleDisposable.Push(ImStyleSingle,float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Push(ImStyleDouble type, Vector2 value)
            => new StyleDisposable().Push(type, value);

        /// <inheritdoc cref="ColorStyleDisposable.PushDefault()"/>
        public ColorStyleDisposable PushDefault()
            => new ColorStyleDisposable().PushDefault();

        /// <inheritdoc cref="StyleDisposable.PushDefault(ImStyleSingle)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushDefault(ImStyleSingle type)
            => new StyleDisposable().PushDefault(type);

        /// <inheritdoc cref="StyleDisposable.PushDefault(ImStyleDouble)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushDefault(ImStyleDouble type)
            => new StyleDisposable().PushDefault(type);

        /// <inheritdoc cref="StyleDisposable.PushX(ImStyleDouble,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushX(ImStyleDouble type, float value, bool condition)
            => new StyleDisposable().PushX(type, value, condition);

        /// <inheritdoc cref="StyleDisposable.PushY(ImStyleDouble,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushY(ImStyleDouble type, float value, bool condition)
            => new StyleDisposable().PushY(type, value, condition);

        /// <inheritdoc cref="StyleDisposable.PushX(ImStyleDouble,float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushX(ImStyleDouble type, float value)
            => new StyleDisposable().PushX(type, value);

        /// <inheritdoc cref="StyleDisposable.PushY(ImStyleDouble,float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable PushY(ImStyleDouble type, float value)
            => new StyleDisposable().PushY(type, value);

        /// <inheritdoc cref="ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.ColorParameter,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorStyleDisposable PushBorder(ImStyleBorder borderType, ColorParameter color, float thickness, bool condition)
            => new ColorStyleDisposable().Push(borderType, color, thickness, condition);

        /// <inheritdoc cref="ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.ColorParameter,float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorStyleDisposable PushBorder(ImStyleBorder borderType, ColorParameter color, float thickness)
            => new ColorStyleDisposable().Push(borderType, color, thickness);

        /// <inheritdoc cref="ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.ColorParameter)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorStyleDisposable PushBorder(ImStyleBorder borderType, ColorParameter color)
            => new ColorStyleDisposable().Push(borderType, color);

        /// <inheritdoc cref="ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.Rgba32,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorStyleDisposable PushBorder(ImStyleBorder borderType, Rgba32 color, float thickness, bool condition)
            => new ColorStyleDisposable().Push(borderType, color, thickness, condition);

        /// <inheritdoc cref="ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.Rgba32,float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorStyleDisposable PushBorder(ImStyleBorder borderType, Rgba32 color, float thickness)
            => new ColorStyleDisposable().Push(borderType, color, thickness);

        /// <inheritdoc cref="ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorStyleDisposable PushBorder(ImStyleBorder borderType, Rgba32 color)
            => new ColorStyleDisposable().Push(borderType, color);

        /// <inheritdoc cref="ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.Rgba32,float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorStyleDisposable PushBorder(ImStyleBorder borderType, Vector4 color, float thickness, bool condition)
            => new ColorStyleDisposable().Push(borderType, color, thickness, condition);

        /// <inheritdoc cref="ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.Rgba32,float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorStyleDisposable PushBorder(ImStyleBorder borderType, Vector4 color, float thickness)
            => new ColorStyleDisposable().Push(borderType, color, thickness);

        /// <inheritdoc cref="ColorStyleDisposable.Push(ImSharp.ImStyleBorder,ImSharp.Rgba32)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public ColorStyleDisposable PushBorder(ImStyleBorder borderType, Vector4 color)
            => new ColorStyleDisposable().Push(borderType, color);


        /// <summary> Get the current text line height. </summary>
        public float TextHeight
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Layout.GetTextLineHeight();
        }

        /// <summary> Get the current <seealso cref="TextHeight"/> + <seealso cref="ImGuiStyle.ItemSpacing"/>.Y. </summary>
        public float TextHeightWithSpacing
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Layout.GetTextLineHeightWithSpacing();
        }

        /// <summary> Get the current frame height (which is <seealso cref="TextHeight"/> + 2 <seealso cref="ImGuiStyle.FramePadding"/>.Y). </summary>
        public float FrameHeight
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Layout.GetFrameHeight();
        }

        /// <summary> Get the current <seealso cref="FrameHeight"/> + <seealso cref="ImGuiStyle.ItemSpacing"/>.Y. </summary>
        public float FrameHeightWithSpacing
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Layout.GetFrameHeightWithSpacing();
        }

        /// <summary> Get the global scale applied to almost anything. </summary>
        public float GlobalScale
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Io.GlobalScale;
        }

        /// <summary> Get the horizontal distance preceding a label when using a <seealso cref="Tree.Node"/> or <seealso cref="Bullet"/>. </summary>
        /// <remarks> This is equal to <seealso cref="Font.CurrentSize"/> + 2 * <seealso cref="ImGuiStyle.FramePadding"/>.X for regular tree nodes. </remarks>
        public float TreeNodeToLabelSpacing
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Tree.GetTreeNodeToLabelSpacing();
        }
    }
}
