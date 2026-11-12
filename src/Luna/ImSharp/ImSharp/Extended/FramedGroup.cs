namespace ImSharp;

public static partial class ImEx
{
    /// <inheritdoc cref="FramedGroupDisposable(ref Utf8LabelHandler,Vector2,ref Utf8TextHandler,ColorParameter,ColorParameter,ReadOnlySpan{byte},Im.Font,ReadOnlySpan{byte},Im.Font)"/>
    public static FramedGroupDisposable FramedGroup<TIcon>(Utf8LabelHandler label, TIcon headerPreIcon, TIcon tooltipIcon,
        Utf8TextHandler tooltip = default, ColorParameter headerColor = default,
        ColorParameter borderColor = default, Vector2 minimumSize = default) where TIcon : IIconStandIn
        => new(ref label, minimumSize, ref tooltip, borderColor, headerColor, headerPreIcon.Span, TIcon.Font,
            tooltipIcon.Span, TIcon.Font);

    /// <inheritdoc cref="FramedGroupDisposable(ref Utf8LabelHandler,Vector2,ref Utf8TextHandler,ColorParameter,ColorParameter,ReadOnlySpan{byte},Im.Font,ReadOnlySpan{byte},Im.Font)"/>
    public static FramedGroupDisposable FramedGroup<TIcon>(Utf8LabelHandler label, TIcon tooltipIcon, Utf8TextHandler tooltip = default,
        ColorParameter headerColor = default, ColorParameter borderColor = default, Vector2 minimumSize = default) where TIcon : IIconStandIn
        => new(ref label, minimumSize, ref tooltip, borderColor, headerColor, StringU8.Empty, default, tooltipIcon.Span, TIcon.Font);

    /// <inheritdoc cref="FramedGroupDisposable(ref Utf8LabelHandler,Vector2,ref Utf8TextHandler,ColorParameter,ColorParameter,ReadOnlySpan{byte},Im.Font,ReadOnlySpan{byte},Im.Font)"/>
    public static FramedGroupDisposable FramedGroup(Utf8LabelHandler label, Utf8TextHandler tooltip = default,
        ColorParameter headerColor = default, ColorParameter borderColor = default, Vector2 minimumSize = default)
        => new(ref label, minimumSize, ref tooltip, borderColor, headerColor, StringU8.Empty, default, StringU8.Empty, default);

    /// <summary> A wrapper around a framed group. </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ref struct FramedGroupDisposable : IDisposable
    {
        /// <summary> A stack of label data for all groups. </summary>
        private static readonly Stack<(Vector2, Vector2)> LabelStack = [];

        /// <summary> The color for the frame. Used in End. </summary>
        public readonly Rgba32 BorderColor;

        /// <summary> The minimum width returned by the framed group. </summary>
        public readonly float MinimumWidth;

        /// <summary> Whether the group is still open. </summary>
        public bool Alive { get; private set; }

        /// <summary> Begin a framed group and end it upon leaving scope. </summary>
        /// <param name="label"> The label for the ID and displayed on the top left inside the frame borders as text. Does not have to be null-terminated. </param>
        /// <param name="minimumSize"> A minimum size for the group. Currently, only the width is used. </param>
        /// <param name="tooltip"> A tooltip to display when hovering the tooltip icon to the right of the label. </param>
        /// <param name="borderColor"> The color of the frame border. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Border"/> is used. </param>
        /// <param name="headerColor"> The color of the label. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </param>
        /// <param name="headerPreIcon"> An optional icon to display to the left of the label. </param>
        /// <param name="headerPreFont"> The icon font for the header icon. </param>
        /// <param name="tooltipIcon"> The icon to show if there is a tooltip set, displayed to the right of the label so that hovering yields the tooltip. Required if tooltips are used. </param>
        /// <param name="tooltipFont"> The icon font for the tooltip icon. </param>
        /// <returns> A disposable object. Use with using. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        internal FramedGroupDisposable(scoped ref Utf8LabelHandler label, Vector2 minimumSize, scoped ref Utf8TextHandler tooltip,
            ColorParameter borderColor,
            ColorParameter headerColor, ReadOnlySpan<byte> headerPreIcon, Im.Font headerPreFont, ReadOnlySpan<byte> tooltipIcon,
            Im.Font tooltipFont)
        {
            Alive       = true;
            BorderColor = borderColor.CheckDefault(ImGuiColor.Border);
            MinimumWidth = BeginFramedGroupInternal(ref label, minimumSize, ref tooltip, headerColor, headerPreIcon, headerPreFont, tooltipIcon,
                tooltipFont);
        }

        /// <summary> End the group on leaving scope. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public void Dispose()
        {
            if (!Alive)
                return;

            Alive = false;
            EndFramedGroup(BorderColor);
        }

        private static unsafe float BeginFramedGroupInternal(ref Utf8LabelHandler label, Vector2 minimumSize, ref Utf8TextHandler tooltip,
            ColorParameter headerColor, ReadOnlySpan<byte> symbol, Im.Font symbolFont, ReadOnlySpan<byte> tooltipIcon, Im.Font tooltipIconFont)
        {
            var itemSpacing     = Im.Style.ItemSpacing;
            var frameHeight     = Im.Style.FrameHeight;
            var halfFrameHeight = new Vector2(frameHeight / 2, 0);
            var startPoint      = Im.Cursor.ScreenPosition.X + halfFrameHeight.X;

            Im.Group();

            var style = ImStyleDouble.FramePadding.Push(Vector2.Zero)
                .Push(ImStyleDouble.ItemSpacing, Vector2.Zero);

            Im.Group(); // Second group

            var effectiveSize = minimumSize;
            if (effectiveSize.X < 0)
                effectiveSize.X += Im.ContentRegion.Available.X;

            // Ensure width.
            Im.Dummy(effectiveSize.X);
            // Ensure left half boundary width/distance.
            Im.Dummy(halfFrameHeight);

            Im.Line.Same();
            Im.Group(); // Third group.
            // Ensure right half of boundary width/distance
            Im.Dummy(halfFrameHeight);

            // Label block
            Im.Line.Same();
            using (_ = Im.Group())
            {
                using var color = ImGuiColor.Text.Push(headerColor);
                if (symbol.Length > 0 && symbolFont.Pointer is not null)
                {
                    using var font = symbolFont.Push();
                    Im.Text(symbol);
                    Im.Line.SameInner();
                }


                Im.Text(ref label);

                if (tooltip.GetSpan(out var span) && span.Length > 0)
                {
                    Im.Line.SameInner();

                    if (tooltipIcon.Length > 0 && tooltipIconFont.Pointer is not null)
                    {
                        using var font = tooltipIconFont.Push();
                        Im.TextDisabled(tooltipIcon);
                    }

                    Im.Tooltip.OnHover(ref tooltip);
                }
            }

            var labelMin = Im.Item.UpperLeftCorner;
            var labelMax = Im.Item.LowerRightCorner;
            Im.Line.Same();
            // Ensure height and distance to label.
            Im.Dummy(new Vector2(0, frameHeight + itemSpacing.Y));

            Im.Group(); // Fourth Group.

            style.Dispose();
            var itemWidth = Im.Item.CalculateWidth();
            Im.Item.PushWidth(Math.Max(0f, itemWidth - frameHeight));

            LabelStack.Push((labelMin, labelMax));
            return Math.Max(effectiveSize.X, labelMax.X - startPoint);
        }

        private static void EndFramedGroup(Rgba32 borderColor)
        {
            var itemSpacing     = Im.Style.ItemSpacing;
            var frameHeight     = Im.Style.FrameHeight;
            var halfFrameHeight = new Vector2(frameHeight / 2, 0);
            var (currentLabelMin, currentLabelMax) = LabelStack.Pop();

            Im.ItemWidthDisposable.PopUnsafe();
            var style = ImStyleDouble.FramePadding.Push(Vector2.Zero)
                .Push(ImStyleDouble.ItemSpacing, Vector2.Zero);

            Im.GroupDisposable.EndUnsafe(); // Close fourth group
            Im.GroupDisposable.EndUnsafe(); // Close third group

            Im.Line.Same();
            // Ensure right distance.
            Im.Dummy(halfFrameHeight);
            // Ensure bottom distance
            Im.Dummy(new Vector2(0, frameHeight / 2 - itemSpacing.Y));
            Im.GroupDisposable.EndUnsafe(); // Close second group

            var itemMin   = Im.Item.UpperLeftCorner;
            var itemMax   = Im.Item.LowerRightCorner;
            var halfFrame = new Vector2(frameHeight / 8, frameHeight / 2);
            var frameMin  = itemMin + halfFrame;
            var frameMax  = itemMax - halfFrame with { Y = 0 };
            currentLabelMin.X -= itemSpacing.X;
            currentLabelMax.X += itemSpacing.X;
            var thickness = 2 * Im.Style.ChildBorderThickness;

            // Left
            DrawClippedRect(new Vector2(-float.MaxValue, -float.MaxValue), currentLabelMin with { Y = float.MaxValue }, frameMin,
                frameMax,                                                  borderColor,                                 thickness);
            // Right
            DrawClippedRect(currentLabelMax with { Y = -float.MaxValue }, new Vector2(float.MaxValue, float.MaxValue), frameMin,
                frameMax,                                                 borderColor,                                 thickness);
            // Top
            DrawClippedRect(currentLabelMin with { Y = -float.MaxValue }, new Vector2(currentLabelMax.X, currentLabelMin.Y), frameMin,
                frameMax,                                                 borderColor,                                       thickness);
            // Bottom
            DrawClippedRect(new Vector2(currentLabelMin.X, currentLabelMax.Y), currentLabelMax with { Y = float.MaxValue }, frameMin,
                frameMax,                                                      borderColor,                                 thickness);

            style.Dispose();
            // This seems wrong?
            // ImGui.SetWindowSize( new Vector2( ImGui.GetWindowSize().X + frameHeight, ImGui.GetWindowSize().Y ) );
            Im.Dummy(Vector2.Zero);

            Im.GroupDisposable.EndUnsafe(); // Close first group
        }

        private static void DrawClippedRect(Vector2 clipMin, Vector2 clipMax, Vector2 drawMin, Vector2 drawMax, Rgba32 color, float thickness)
        {
            var rect = Im.Drawing.PushClipRect(clipMin, clipMax, true);
            Im.Window.DrawList.Shape.Rectangle(drawMin, drawMax, color, Im.Style.FrameRounding, ImDrawFlagsRectangle.RoundCornersAll,
                thickness);
            rect.Dispose();
        }
    }
}
