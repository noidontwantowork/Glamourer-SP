namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    /// <summary> A writable reference to style data for ImNodes. </summary>
    /// <param name="pointer"> The native pointer to the style. </param>
    public readonly unsafe ref struct ImNodesStyleWritable(Internal.Style* pointer)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Internal.Style* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator ImNodesStyleWritable(Internal.Style* pointer)
            => new(pointer);

        /// <summary> Obtain a writeable reference to the current ImNodes style container. </summary>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static ImNodesStyleWritable Get()
            => Api.GetStyle();

        /// <inheritdoc cref="ImNodesStyleFlags"/>
        public ImNodesStyleFlags Flags
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->Flags;
            [MethodImpl(ImSharpConfiguration.Inl)]
            set => Pointer->Flags = value;
        }

        /// <inheritdoc cref="ImNodesStyleDouble.NodePadding"/>
        public Vector2 NodePadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->NodePadding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->NodePadding = value;
        }

        /// <inheritdoc cref="ImNodesStyleDouble.MiniMapPadding"/>
        public Vector2 MiniMapPadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->MiniMapPadding;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->MiniMapPadding = value;
        }

        /// <inheritdoc cref="ImNodesStyleDouble.MiniMapOffset"/>
        public Vector2 MiniMapOffset
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->MiniMapOffset;
            [MethodImpl(ImSharpConfiguration.OptInl)]
            set => Pointer->MiniMapOffset = value;
        }

        /// <inheritdoc cref="ImNodesStyleSingle.PinOffset"/>
        public float GridSpacing
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->GridSpacing;
            [MethodImpl(ImSharpConfiguration.Inl)]
            set => Pointer->GridSpacing = value;
        }

        /// <inheritdoc cref="ImNodesStyleSingle.PinOffset"/>
        public float NodeCornerRounding
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->NodeCornerRounding;
        }

        /// <inheritdoc cref="ImNodesStyleSingle.PinOffset"/>
        public float NodeBorderThickness
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->NodeBorderThickness;
        }

        /// <inheritdoc cref="ImNodesStyleSingle.PinOffset"/>
        public float LinkThickness
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->LinkThickness;
        }

        /// <inheritdoc cref="ImNodesStyleSingle.PinOffset"/>
        public float LinkLineSegmentsPerLength
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->LinkLineSegmentsPerLength;
        }

        /// <inheritdoc cref="ImNodesStyleSingle.PinOffset"/>
        public float LinkHoverDistance
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->LinkHoverDistance;
        }

        /// <inheritdoc cref="ImNodesStyleSingle.PinOffset"/>
        public float PinCircleRadius
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->PinCircleRadius;
        }

        /// <inheritdoc cref="ImNodesStyleSingle.PinOffset"/>
        public float PinQuadSideLength
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->PinQuadSideLength;
        }

        /// <inheritdoc cref="ImNodesStyleSingle.PinOffset"/>
        public float PinTriangleSideLength
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->PinTriangleSideLength;
        }

        /// <inheritdoc cref="ImNodesStyleSingle.PinLineThickness"/>
        public float PinLineThickness
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->PinLineThickness;
        }

        /// <inheritdoc cref="ImNodesStyleSingle.PinOffset"/>
        public float PinOffset
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->PinOffset;
        }

        /// <inheritdoc cref="ImNodesStyleSingle.PinOffset"/>
        public float PinHoverRadius
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->PinHoverRadius;
        }

        /// <summary> Get or set a single-value ImNodes style variable by type. </summary>
        /// <param name="style"> The style variable to get or set. </param>
        /// <returns> The value. </returns>
        /// <exception cref="ArgumentOutOfRangeException" />
        public float this[ImNodesStyleSingle style]
        {
            [MethodImpl(ImSharpConfiguration.Opt)]
            get => style switch
            {
                ImNodesStyleSingle.GridSpacing               => Pointer->GridSpacing,
                ImNodesStyleSingle.NodeCornerRounding        => Pointer->NodeCornerRounding,
                ImNodesStyleSingle.NodeBorderThickness       => Pointer->NodeBorderThickness,
                ImNodesStyleSingle.LinkThickness             => Pointer->LinkThickness,
                ImNodesStyleSingle.LinkLineSegmentsPerLength => Pointer->LinkLineSegmentsPerLength,
                ImNodesStyleSingle.LinkHoverDistance         => Pointer->LinkHoverDistance,
                ImNodesStyleSingle.PinCircleRadius           => Pointer->PinCircleRadius,
                ImNodesStyleSingle.PinQuadSideLength         => Pointer->PinQuadSideLength,
                ImNodesStyleSingle.PinTriangleSideLength     => Pointer->PinTriangleSideLength,
                ImNodesStyleSingle.PinLineThickness          => Pointer->PinLineThickness,
                ImNodesStyleSingle.PinHoverRadius            => Pointer->PinHoverRadius,
                ImNodesStyleSingle.PinOffset                 => Pointer->PinOffset,
                _                                            => throw new ArgumentOutOfRangeException(nameof(style), style, null),
            };
            [MethodImpl(ImSharpConfiguration.Opt)]
            set
            {
                switch (style)
                {
                    case ImNodesStyleSingle.GridSpacing:               Pointer->GridSpacing               = value; break;
                    case ImNodesStyleSingle.NodeCornerRounding:        Pointer->NodeCornerRounding        = value; break;
                    case ImNodesStyleSingle.NodeBorderThickness:       Pointer->NodeBorderThickness       = value; break;
                    case ImNodesStyleSingle.LinkThickness:             Pointer->LinkThickness             = value; break;
                    case ImNodesStyleSingle.LinkLineSegmentsPerLength: Pointer->LinkLineSegmentsPerLength = value; break;
                    case ImNodesStyleSingle.LinkHoverDistance:         Pointer->LinkHoverDistance         = value; break;
                    case ImNodesStyleSingle.PinCircleRadius:           Pointer->PinCircleRadius           = value; break;
                    case ImNodesStyleSingle.PinQuadSideLength:         Pointer->PinQuadSideLength         = value; break;
                    case ImNodesStyleSingle.PinTriangleSideLength:     Pointer->PinTriangleSideLength     = value; break;
                    case ImNodesStyleSingle.PinLineThickness:          Pointer->PinLineThickness          = value; break;
                    case ImNodesStyleSingle.PinHoverRadius:            Pointer->PinHoverRadius            = value; break;
                    case ImNodesStyleSingle.PinOffset:                 Pointer->PinOffset                 = value; break;
                    default:                                           throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
        }

        /// <summary> Get or set a double-value ImNodes style variable by type. </summary>
        /// <param name="style"> The style variable to get or set. </param>
        /// <returns> The value. </returns>
        /// <exception cref="ArgumentOutOfRangeException" />
        public Vector2 this[ImNodesStyleDouble style]
        {
            [MethodImpl(ImSharpConfiguration.Opt)]
            get => style switch
            {
                ImNodesStyleDouble.NodePadding    => Pointer->NodePadding,
                ImNodesStyleDouble.MiniMapPadding => Pointer->MiniMapPadding,
                ImNodesStyleDouble.MiniMapOffset  => Pointer->MiniMapOffset,
                _                                 => throw new ArgumentOutOfRangeException(nameof(style), style, null),
            };
            set
            {
                switch (style)
                {
                    case ImNodesStyleDouble.NodePadding:    Pointer->NodePadding    = value; break;
                    case ImNodesStyleDouble.MiniMapPadding: Pointer->MiniMapPadding = value; break;
                    case ImNodesStyleDouble.MiniMapOffset:  Pointer->MiniMapOffset  = value; break;
                    default:                                throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
        }

        /// <summary> Get or set ImNodes style colors. </summary>
        /// <param name="color"> The color to get or set. </param>
        public Rgba32 this[ImNodesColor color]
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->Colors[(int)color];
            [MethodImpl(ImSharpConfiguration.Inl)]
            set => Pointer->Colors[(int)color] = value;
        }

        /// <inheritdoc cref="Im.ImGuiStyleWritable.SetDark"/>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public void SetDark()
            => Api.StyleColorsDark(Pointer);

        /// <inheritdoc cref="Im.ImGuiStyleWritable.SetClassic"/>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public void SetClassic()
            => Api.StyleColorsClassic(Pointer);

        /// <inheritdoc cref="Im.ImGuiStyleWritable.SetLight"/>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public void SetLight()
            => Api.StyleColorsLight(Pointer);
    }
}
