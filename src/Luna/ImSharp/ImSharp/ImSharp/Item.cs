using ImSharp.Internal;

namespace ImSharp;

public static partial class Im
{
    /// <summary> A wrapper class for item-related queries and actions. </summary>
    public static class Item
    {
        /// <inheritdoc cref="ItemWidthDisposable.Push(float,bool)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ItemWidthDisposable PushWidth(float width, bool condition)
            => new ItemWidthDisposable().Push(width, condition);

        /// <inheritdoc cref="ItemWidthDisposable.Push(float)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static ItemWidthDisposable PushWidth(float width)
            => new ItemWidthDisposable().Push(width);

        /// <summary> Set the width of the next common item+label widget. </summary>
        /// <param name="width"> If positive, the desired width in pixels. If negative, align to the right side by that many pixels. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetNextWidth(float width)
            => Native.Methods.Stacks.SetNextItemWidth(width);

        /// <summary> Set the width of the next common item+label widget to the maximum available content region. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetNextWidthFull()
            => Native.Methods.Stacks.SetNextItemWidth(ContentRegion.Available.X);

        /// <summary> Set the width of the next common item+label widget but scale the supplied width by <see cref="Im.ImGuiStyle.GlobalScale"/>. </summary>
        /// <param name="width"> If positive, the desired width in pixels. If negative, align to the right side by that many pixels. </param>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetNextWidthScaled(float width)
            => Native.Methods.Stacks.SetNextItemWidth(width * Style.GlobalScale);

        /// <summary> Calculate the default width for the next item given the pushed settings and current cursor position. </summary>
        /// <returns> The width in pixels. </returns>
        /// <remarks> Not necessarily the width of the last item drawn. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static float CalculateWidth()
            => Native.Methods.Stacks.CalcItemWidth();

        /// <summary> Get whether the last drawn item has been edited in this frame. </summary>
        public static bool Edited
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Items.IsItemEdited();
        }

        /// <summary> Get whether the last drawn item has been activated in this frame. </summary>
        public static bool Activated
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Items.IsItemActivated();
        }

        /// <summary> Returns whether the last drawn item was made inactive this frame. </summary>
        /// <returns> True if the item was deactivated. </returns>
        public static bool Deactivated
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Items.IsItemDeactivated();
        }

        /// <summary> Get whether the last drawn item was made inactive this frame, but only if it made a value change while it was active. </summary>
        public static bool DeactivatedAfterEdit
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Items.IsItemDeactivatedAfterEdit();
        }

        /// <summary> Draw a widget behaving like a button but without visuals on top of the last drawn item. </summary>
        /// <param name="id"> The id as text. If this is a UTF8 string, it HAS to be null-terminated. </param>
        /// <param name="flags"> Additional flags to control the button's behaviour. </param>
        /// <returns> True if the button has been clicked in this frame. </returns>
        public static bool InvisibleButton(Utf8LabelHandler id, ButtonFlags flags = ButtonFlags.None)
            => Im.InvisibleButton(id, Bounds, flags);

        /// <summary> Get whether the last drawn item has been clicked in this frame. </summary>
        /// <param name="button"> The button for which to check. </param>
        /// <returns> True if the item has been clicked with <paramref name="button"/>. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Clicked(MouseButton button = MouseButton.Left)
            => Native.Methods.Items.IsItemClicked(button);

        /// <summary> Get whether the last drawn item has been right-clicked in this frame. </summary>
        /// <returns> True if the item has been right-clicked. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool RightClicked()
            => Native.Methods.Items.IsItemClicked(MouseButton.Right);

        /// <summary> Get whether the last drawn item has been middle-clicked in this frame. </summary>
        /// <returns> True if the item has been middle-clicked. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool MiddleClicked()
            => Native.Methods.Items.IsItemClicked(MouseButton.Middle);

        /// <summary> Get whether the last drawn item is currently being hovered by the cursor. </summary>
        /// <param name="flags"> Additional flags to control the behaviour, see <seealso cref="HoveredFlags"/>. </param>
        /// <returns> True if the item is hovered according to the flags. </returns>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static bool Hovered(HoveredFlags flags = HoveredFlags.None)
            => Native.Methods.Items.IsItemHovered(flags);

        /// <summary> Get whether the last drawn item is currently active. </summary>
        public static bool Active
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Items.IsItemActive();
        }

        /// <summary> Get whether the last drawn item is currently focused. </summary>
        public static bool Focused
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Items.IsAnyItemFocused();
        }

        /// <summary> Get whether the last drawn item is currently visible. </summary>
        public static bool Visible
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Items.IsItemVisible();
        }

        /// <summary> Get whether any item is currently hovered. </summary>
        public static bool AnyHovered
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Items.IsAnyItemHovered();
        }

        /// <summary> Get whether any item is currently active. </summary>
        public static bool AnyActive
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Items.IsAnyItemActive();
        }

        /// <summary> Return whether any item is currently focused. </summary>
        /// <returns> True if any item is focused. </returns>
        public static bool AnyFocused
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get => Native.Methods.Items.IsAnyItemFocused();
        }

        /// <summary> Get the upper-left corner of the bounding rectangle of the last item in screen coordinates. </summary>
        public static unsafe Vector2 UpperLeftCorner
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImVec2 ret;
                Native.Methods.Items.GetItemRectMin(&ret);
                return ret;
            }
        }

        /// <summary> Get the lower-right corner of the bounding rectangle of the last item in screen coordinates. </summary>
        public static unsafe Vector2 LowerRightCorner
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImVec2 ret;
                Native.Methods.Items.GetItemRectMax(&ret);
                return ret;
            }
        }

        /// <summary> Get the bounding rectangle of the last item in screen coordinates. </summary>
        public static unsafe Rectangle Bounds
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImVec2 min, max;
                Native.Methods.Items.GetItemRectMin(&min);
                Native.Methods.Items.GetItemRectMax(&max);
                return new Rectangle(min, max);
            }
        }

        /// <summary> Get the size of the last item in pixels. </summary>
        public static unsafe Vector2 Size
        {
            [MethodImpl(ImSharpConfiguration.OptInl)]
            get
            {
                ImVec2 ret;
                Native.Methods.Items.GetItemRectSize(&ret);
                return ret;
            }
        }

        /// <summary> Allow the LAST item to be overlapped by a subsequent item. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void AllowOverlap()
            => Native.Methods.Items.SetItemAllowOverlap();

        /// <summary> Declare a new bounding box for an item for clipping and interaction. </summary>
        /// <param name="boundingBox"> The bounding box size, which can be different from the one passed to <seealso cref="SetSize"/>, which uses the minimum size requirement while this uses the actual size. </param>
        /// <param name="id"> The ID for the new item. </param>
        /// <param name="navigationBoundingBox"> The bounding box for keyboard/gamepad navigation. </param>
        /// <param name="flags"> Additional flags for the item. </param>
        /// <remarks> Only use this when creating custom widgets. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool Add(in Rectangle boundingBox, ImGuiId id, in Rectangle navigationBoundingBox,
            ItemFlags flags = ItemFlags.None)
        {
            ImRect rect = navigationBoundingBox;
            return Native.Methods.Internal.ItemAdd(boundingBox, id, &rect, flags);
        }

        /// <inheritdoc cref="Add(in Rectangle,ImGuiId,in Rectangle,ItemFlags)"/>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe bool Add(in Rectangle boundingBox, ImGuiId id, ItemFlags flags = ItemFlags.None)
        {
            ImRect rect = boundingBox;
            return Native.Methods.Internal.ItemAdd(boundingBox, id, &rect, flags);
        }

        /// <summary> Set the size of a new item. </summary>
        /// <param name="size"> The size of the bounding box for the current item, assumed to start at the current cursor position. </param>
        /// <param name="textBaseLineY"> The base offset for text for the current item line. </param>
        /// <remarks> Only use this when creating custom widgets. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetSize(Vector2 size, float textBaseLineY = -1f)
            => Native.Methods.Internal.ItemSize(size, textBaseLineY);

        /// <summary> Calculate the optimal size for a new item. </summary>
        /// <param name="minimum"> The minimum size required in pixels. </param>
        /// <param name="defaultSize"> The default size for the item in pixels. </param>
        /// <returns> The calculated size. </returns>
        /// <remarks> Only use this when creating custom widgets. </remarks>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static unsafe Vector2 CalculateSize(Vector2 minimum, Vector2 defaultSize)
        {
            ImVec2 ret;
            Native.Methods.Internal.CalcItemSize(&ret, minimum, defaultSize.X, defaultSize.Y);
            return ret;
        }

        /// <summary> Tell that the last item is listening and owning the mousewheel for this frame. </summary>
        [MethodImpl(ImSharpConfiguration.OptInl)]
        public static void SetUsingMouseWheel()
            => Native.Methods.Internal.SetItemUsingMouseWheel();
    }
}
