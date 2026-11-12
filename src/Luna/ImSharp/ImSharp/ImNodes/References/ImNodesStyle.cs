namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    /// <summary> A read-only reference to style data for ImNodes. </summary>
    /// <param name="pointer"> The native pointer to the style. </param>
    public readonly unsafe ref struct ImNodesStyle(Internal.Style* pointer)
    {
        /// <summary> The address of the native object. </summary>
        public readonly Internal.Style* Pointer = pointer;

        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static implicit operator ImNodesStyle(Internal.Style* pointer)
            => new(pointer);

        /// <summary> Obtain a read-only reference to the current ImNodes style container. </summary>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static ImNodesStyle Get()
            => Api.GetStyle();

        /// <summary> Obtain a writeable reference to this style.</summary>
        /// <remarks> Generally avoid writing to the style and use Push/Pull methods instead. </remarks>
        [MethodImpl(ImSharpConfiguration.Inl)]
        public ImNodesStyleWritable AsWritable()
            => new(Pointer);

        /// <inheritdoc cref="ImNodesStyleFlags"/>
        public ImNodesStyleFlags Flags
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->Flags;
        }

        /// <inheritdoc cref="ImNodesStyleDouble.NodePadding"/>
        public Vector2 NodePadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->NodePadding;
        }

        /// <inheritdoc cref="ImNodesStyleDouble.MiniMapPadding"/>
        public Vector2 MiniMapPadding
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->MiniMapPadding;
        }

        /// <inheritdoc cref="ImNodesStyleDouble.MiniMapOffset"/>
        public Vector2 MiniMapOffset
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Pointer->MiniMapOffset;
        }

        /// <inheritdoc cref="ImNodesStyleSingle.PinOffset"/>
        public float GridSpacing
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->GridSpacing;
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

        /// <summary> Obtain a single-value ImNodes style variable by type. </summary>
        /// <param name="style"> The style variable. </param>
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
        }

        /// <summary> Obtain a double-value ImNodes style variable by type. </summary>
        /// <param name="style"> The style variable. </param>
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
        }

        /// <summary> Obtain ImNodes style colors. </summary>
        /// <param name="color"> The color. </param>
        public Rgba32 this[ImNodesColor color]
        {
            [MethodImpl(ImSharpConfiguration.Inl)]
            get => Pointer->Colors[(int)color];
            [MethodImpl(ImSharpConfiguration.Inl)]
            private set => Pointer->Colors[(int)color] = value;
        }

        /// <summary> Create a new, empty <see cref="StyleDisposable"/> to push colors to. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public StyleDisposable Empty()
            => new();

        /// <inheritdoc cref="StyleDisposable.Push(ImNodesStyleSingle,float)"/>
        public StyleDisposable Push(ImNodesStyleSingle type, float value)
            => new StyleDisposable().Push(type, value);

        /// <inheritdoc cref="StyleDisposable.Push(ImNodesStyleSingle,float,bool)"/>
        public StyleDisposable Push(ImNodesStyleSingle type, float value, bool condition)
            => new StyleDisposable().Push(type, value, condition);

        /// <inheritdoc cref="StyleDisposable.Push(ImNodesStyleDouble,Vector2)"/>
        public StyleDisposable Push(ImNodesStyleDouble type, Vector2 value)
            => new StyleDisposable().Push(type, value);

        /// <inheritdoc cref="StyleDisposable.Push(ImNodesStyleDouble,Vector2,bool)"/>
        public StyleDisposable Push(ImNodesStyleDouble type, Vector2 value, bool condition)
            => new StyleDisposable().Push(type, value, condition);

        /// <inheritdoc cref="StyleDisposable.PushX(ImNodesStyleDouble,float)"/>
        public StyleDisposable PushX(ImNodesStyleDouble type, float value)
            => new StyleDisposable().PushX(type, value);

        /// <inheritdoc cref="StyleDisposable.PushX(ImNodesStyleDouble,float,bool)"/>
        public StyleDisposable PushX(ImNodesStyleDouble type, float value, bool condition)
            => new StyleDisposable().PushX(type, value, condition);

        /// <inheritdoc cref="StyleDisposable.PushY(ImNodesStyleDouble,float)"/>
        public StyleDisposable PushY(ImNodesStyleDouble type, float value)
            => new StyleDisposable().PushX(type, value);

        /// <inheritdoc cref="StyleDisposable.PushY(ImNodesStyleDouble,float,bool)"/>
        public StyleDisposable PushY(ImNodesStyleDouble type, float value, bool condition)
            => new StyleDisposable().PushX(type, value, condition);

        /// <summary> Pop a number of ImNodes style variables. </summary>
        /// <param name="num"> The number of style variables to pop. The number is not checked against the style stack. </param>
        /// <remarks> Avoid using this function, and styles across scopes, as much as possible. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void PopUnsafe(int num = 1)
            => Api.PopStyle(num);
    }
}
