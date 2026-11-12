namespace ImSharp;

public static partial class Im
{
    /// <summary> A writable reference to style data for ImGui. </summary>
    /// <param name="pointer"> The native pointer to the style. </param>
    public readonly unsafe ref struct ImGuiStyleWritable(Native.ImGuiStyle* pointer)
    {
        /// <inheritdoc cref="ImGuiStyle.Pointer"/>
        public readonly Native.ImGuiStyle* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static explicit operator ImGuiStyleWritable(Native.ImGuiStyle* pointer)
            => new(pointer);

        /// <summary> Obtain a writeable reference to the current style container. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ImGuiStyle Get()
            => new(Native.Methods.Main.GetStyle());

        /// <inheritdoc cref="ImGuiStyle.Alpha"/>
        public float Alpha
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->Alpha;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->Alpha = value;
        }

        /// <inheritdoc cref="ImGuiStyle.DisabledAlpha"/>
        public float DisabledAlpha
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->DisabledAlpha;

            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->DisabledAlpha = value;
        }

        /// <inheritdoc cref="ImGuiStyle.WindowPadding"/>
        public Vector2 WindowPadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WindowPadding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->WindowPadding = value;
        }

        /// <inheritdoc cref="ImGuiStyle.WindowRounding"/>
        public float WindowRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WindowRounding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->WindowRounding = value;
        }

        /// <inheritdoc cref="ImGuiStyle.Alpha"/>
        public ref float WindowBorderThickness
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => ref Pointer->WindowBorderSize;
        }

        /// <inheritdoc cref="ImGuiStyle.MinimumWindowSize"/>
        public Vector2 MinimumWindowSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WindowMinSize;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->WindowMinSize = value;
        }

        /// <inheritdoc cref="ImGuiStyle.WindowTitleAlignment"/>
        public Vector2 WindowTitleAlignment
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WindowTitleAlign;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->WindowTitleAlign = value;
        }

        /// <inheritdoc cref="ImGuiStyle.WindowMenuButtonPosition"/>
        public Direction WindowMenuButtonPosition
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->WindowMenuButtonPosition;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->WindowMenuButtonPosition = value;
        }

        /// <inheritdoc cref="ImGuiStyle.ChildRounding"/>
        public float ChildRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ChildRounding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->ChildRounding = value;
        }

        /// <inheritdoc cref="ImGuiStyle.ChildBorderThickness"/>
        public float ChildBorderThickness
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ChildBorderSize;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->ChildBorderSize = value;
        }

        /// <inheritdoc cref="ImGuiStyle.PopupRounding"/>
        public float PopupRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->PopupRounding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->PopupRounding = value;
        }

        /// <inheritdoc cref="ImGuiStyle.Alpha"/>
        public ref float PopupBorderThickness
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => ref Pointer->PopupBorderSize;
        }

        /// <inheritdoc cref="ImGuiStyle.FramePadding"/>
        public Vector2 FramePadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->FramePadding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->FramePadding = value;
        }

        /// <inheritdoc cref="ImGuiStyle.FrameRounding"/>
        public float FrameRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->FrameRounding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->FrameRounding = value;
        }

        /// <inheritdoc cref="ImGuiStyle.FrameBorderThickness"/>
        public float FrameBorderThickness
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->FrameBorderSize;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->FrameBorderSize = value;
        }

        /// <inheritdoc cref="ImGuiStyle.ItemSpacing"/>
        public Vector2 ItemSpacing
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ItemSpacing;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->ItemSpacing = value;
        }

        /// <inheritdoc cref="ImGuiStyle.ItemInnerSpacing"/>
        public Vector2 ItemInnerSpacing
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ItemInnerSpacing;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->ItemInnerSpacing = value;
        }

        /// <inheritdoc cref="ImGuiStyle.CellPadding"/>
        public Vector2 CellPadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->CellPadding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->CellPadding = value;
        }

        /// <inheritdoc cref="ImGuiStyle.TouchExtraPadding"/>
        public Vector2 TouchExtraPadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->TouchExtraPadding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->TouchExtraPadding = value;
        }

        /// <inheritdoc cref="ImGuiStyle.IndentSpacing"/>
        public float IndentSpacing
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->IndentSpacing;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->IndentSpacing = value;
        }

        /// <inheritdoc cref="ImGuiStyle.ColumnsMinimumSpacing"/>
        public float ColumnsMinimumSpacing
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ColumnsMinSpacing;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->ColumnsMinSpacing = value;
        }

        /// <inheritdoc cref="ImGuiStyle.ScrollbarSize"/>
        public float ScrollbarSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ScrollbarSize;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->ScrollbarSize = value;
        }

        /// <inheritdoc cref="ImGuiStyle.ScrollbarRounding"/>
        public float ScrollbarRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ScrollbarRounding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->ScrollbarRounding = value;
        }

        /// <inheritdoc cref="ImGuiStyle.MinimumGrabSize"/>
        public float MinimumGrabSize
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->GrabMinSize;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->GrabMinSize = value;
        }

        /// <inheritdoc cref="ImGuiStyle.GrabRounding"/>
        public float GrabRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->GrabRounding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->GrabRounding = value;
        }

        /// <inheritdoc cref="ImGuiStyle.LogSliderDeadzone"/>
        public float LogSliderDeadzone
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->LogSliderDeadzone;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->LogSliderDeadzone = value;
        }

        /// <inheritdoc cref="ImGuiStyle.TabRounding"/>
        public float TabRounding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->TabRounding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->TabRounding = value;
        }

        /// <inheritdoc cref="ImGuiStyle.TabBorderThickness"/>
        public float TabBorderThickness
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->TabBorderSize;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->TabBorderSize = value;
        }

        /// <inheritdoc cref="ImGuiStyle.TabMinimumWidthForCloseButton"/>
        public float TabMinimumWidthForCloseButton
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->TabMinWidthForCloseButton;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->TabMinWidthForCloseButton = value;
        }

        /// <inheritdoc cref="ImGuiStyle.ColorButtonPosition"/>
        public Direction ColorButtonPosition
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ColorButtonPosition;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->ColorButtonPosition = value;
        }

        /// <inheritdoc cref="ImGuiStyle.ButtonTextAlignment"/>
        public Vector2 ButtonTextAlignment
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->ButtonTextAlign;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->ButtonTextAlign = value;
        }

        /// <inheritdoc cref="ImGuiStyle.SelectableTextAlignment"/>
        public Vector2 SelectableTextAlignment
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->SelectableTextAlign;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->SelectableTextAlign = value;
        }

        /// <inheritdoc cref="ImGuiStyle.DisplayWindowPadding"/>
        public Vector2 DisplayWindowPadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->DisplayWindowPadding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->DisplayWindowPadding = value;
        }

        /// <inheritdoc cref="ImGuiStyle.DisplaySafeAreaPadding"/>
        public Vector2 DisplaySafeAreaPadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->DisplaySafeAreaPadding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->DisplaySafeAreaPadding = value;
        }

        /// <inheritdoc cref="ImGuiStyle.MouseCursorScale"/>
        public float MouseCursorScale
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->MouseCursorScale;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->MouseCursorScale = value;
        }

        /// <inheritdoc cref="ImGuiStyle.AntiAliasedLines"/>
        public bool AntiAliasedLines
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->AntiAliasedLines;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->AntiAliasedLines = value;
        }

        /// <inheritdoc cref="ImGuiStyle.AntiAliasedLinesUseTex"/>
        public bool AntiAliasedLinesUseTex
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->AntiAliasedLinesUseTex;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->AntiAliasedLinesUseTex = value;
        }

        /// <inheritdoc cref="ImGuiStyle.AntiAliasedFill"/>
        public bool AntiAliasedFill
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->AntiAliasedFill;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->AntiAliasedFill = value;
        }

        /// <inheritdoc cref="ImGuiStyle.CurveTessellationTolerance"/>
        public float CurveTessellationTolerance
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->CurveTessellationTol;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->CurveTessellationTol = value;
        }

        /// <inheritdoc cref="ImGuiStyle.CircleTessellationMaximumError"/>
        public float CircleTessellationMaximumError
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->CircleTessellationMaxError;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->CircleTessellationMaxError = value;
        }

        /// <inheritdoc cref="ImGuiStyle.this[ImGuiColor]"/>
        public Vector4 this[ImGuiColor color]
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->Colors[color];
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->Colors[color] = value;
        }

        /// <inheritdoc cref="ImGuiStyle.this[ImStyleSingle]"/>
        public float this[ImStyleSingle style]
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => ((ImGuiStyle)Pointer)[style];
            [MethodImpl(ImSharpConfiguration.Opt)]
            set
            {
                switch (style)
                {
                    case ImStyleSingle.Alpha:                 Alpha                 = value; break;
                    case ImStyleSingle.DisabledAlpha:         DisabledAlpha         = value; break;
                    case ImStyleSingle.WindowRounding:        WindowRounding        = value; break;
                    case ImStyleSingle.WindowBorderThickness: WindowBorderThickness = value; break;
                    case ImStyleSingle.ChildRounding:         ChildRounding         = value; break;
                    case ImStyleSingle.ChildBorderThickness:  ChildBorderThickness  = value; break;
                    case ImStyleSingle.PopupRounding:         PopupRounding         = value; break;
                    case ImStyleSingle.PopupBorderThickness:  PopupBorderThickness  = value; break;
                    case ImStyleSingle.FrameRounding:         FrameRounding         = value; break;
                    case ImStyleSingle.FrameBorderThickness:  FrameBorderThickness  = value; break;
                    case ImStyleSingle.IndentSpacing:         IndentSpacing         = value; break;
                    case ImStyleSingle.ScrollbarSize:         ScrollbarSize         = value; break;
                    case ImStyleSingle.ScrollbarRounding:     ScrollbarRounding     = value; break;
                    case ImStyleSingle.MinimumGrabSize:       MinimumGrabSize       = value; break;
                    case ImStyleSingle.GrabRounding:          GrabRounding          = value; break;
                    case ImStyleSingle.TabRounding:           TabRounding           = value; break;
                    default:                                  throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
        }

        /// <inheritdoc cref="ImGuiStyle.this[ImStyleDouble]"/>
        public Vector2 this[ImStyleDouble imStyle]
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                return imStyle switch
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
            [MethodImpl(ImSharpConfiguration.Opt)]
            set
            {
                switch (imStyle)
                {
                    case ImStyleDouble.WindowPadding:        WindowPadding           = value; break;
                    case ImStyleDouble.MinimumWindowSize:    MinimumWindowSize       = value; break;
                    case ImStyleDouble.WindowTitleAlignment: WindowTitleAlignment    = value; break;
                    case ImStyleDouble.FramePadding:         FramePadding            = value; break;
                    case ImStyleDouble.ItemSpacing:          ItemSpacing             = value; break;
                    case ImStyleDouble.ItemInnerSpacing:     ItemInnerSpacing        = value; break;
                    case ImStyleDouble.CellPadding:          CellPadding             = value; break;
                    case ImStyleDouble.ButtonTextAlign:      ButtonTextAlignment     = value; break;
                    case ImStyleDouble.SelectableTextAlign:  SelectableTextAlignment = value; break;
                    default:                                 throw new ArgumentOutOfRangeException(nameof(imStyle), imStyle, null);
                }
            }
        }

        /// <summary> Scale all style sizes by a given factor. </summary>
        /// <param name="scaleFactor"> The factor. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void ScaleAllSizes(float scaleFactor)
            => Native.ImGuiStyle.ScaleAllSizes(Pointer, scaleFactor);


        /// <summary> Apply the default style. Dark colors. </summary>
        public void SetDark()
            => Native.Methods.Style.StyleColorsDark(Pointer);

        /// <summary> Apply the old default style. </summary>
        public void SetClassic()
            => Native.Methods.Style.StyleColorsClassic(Pointer);

        /// <summary> Apply a light style. Best used with borders and a thicker font. </summary>
        public void SetLight()
            => Native.Methods.Style.StyleColorsLight(Pointer);
    }
}
