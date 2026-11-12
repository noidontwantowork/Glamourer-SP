namespace ImSharp;

/// <summary> Typeless filter combo base class. </summary>
public abstract class FilterComboBase()
{
    /// <summary> Create a filter combo with a specific config. </summary>
    protected FilterComboBase(in ConfigData config)
        : this()
    {
        AllowMouseWheel            = config.MouseWheelType;
        ComputeWidth               = config.ComputeWidth;
        MaximumItems               = config.MaximumItems;
        DirtyCacheOnClosingPopup   = config.DirtyCacheOnClose;
        ClearFilterOnSelection     = config.ClearFilterOnSelection;
        ClearFilterOnCacheDisposal = config.ClearFilterOnCacheDisposal;
        PreviewAlignment           = config.PreviewAlignment;
        Flags                      = config.Flags;
    }

    /// <summary> Configuration for the filter combo. </summary>
    protected readonly record struct ConfigData(
        MouseWheelType MouseWheelType = MouseWheelType.Control,
        bool ComputeWidth = false,
        int MaximumItems = 12,
        bool DirtyCacheOnClose = false,
        bool ClearFilterOnSelection = false,
        bool ClearFilterOnCacheDisposal = true,
        Vector2 PreviewAlignment = default,
        ComboFlags Flags = ComboFlags.None)
    {
        public static readonly ConfigData Default = new(MouseWheelType.Control);
    }

    /// <summary> Whether to allow mouse-wheel scrolling while hovering the unexpanded combo, potentially only with specific key modifiers held. </summary>
    public MouseWheelType AllowMouseWheel { get; init; } = MouseWheelType.Control;

    /// <summary> Additional flags used to draw the combo. </summary>
    public ComboFlags Flags { get; set; } = ComboFlags.None;

    /// <summary> The alignment of the text inside the preview button. </summary>
    public Vector2 PreviewAlignment { get; set; }

    /// <summary> The desired lifespan for the cache for this combo. </summary>
    public TimeSpan CacheLifetime { get; init; } = TimeSpan.FromSeconds(10);

    /// <summary> Whether the cache should always be set dirty when the popup is closed. </summary>
    public bool DirtyCacheOnClosingPopup { get; set; } = false;

    /// <summary> The maximum number of items to display in the expanded combo list. </summary>
    public int MaximumItems { get; init; } = 12;

    /// <summary> Whether the width of the combo popup depends on the displayed items and should be computed. </summary>
    /// <remarks> If this is false, the preview width is used for the popup window too. </remarks>
    public bool ComputeWidth { get; init; }

    /// <summary> Whether the filter should be cleared whenever the selection is updated. </summary>
    public bool ClearFilterOnSelection { get; set; }

    /// <summary> Whether the filter should be cleared whenever the combo cache is disposed. </summary>
    public bool ClearFilterOnCacheDisposal { get; set; } = true;

    /// <summary> The ID used for the cache. </summary>
    protected ImGuiId CurrentId;

    /// <summary> Obtain the item height used to draw a single cache item. Should include spacing. </summary>
    protected internal abstract float ItemHeight { get; }

    /// <summary> Function invoked before drawing the expanded combo list. </summary>
    protected internal virtual void PreDrawList()
    { }

    /// <summary> Function invoked after drawing the expanded combo list. </summary>
    protected internal virtual void PostDrawList()
    { }

    /// <summary> Function invoked before drawing the combo preview. </summary>
    protected virtual void PreDrawCombo(float width)
    { }

    /// <summary> Function invoked after drawing the combo preview, before beginning the popup window (if it is open at all). </summary>
    protected virtual void PostDrawCombo(float width)
    { }

    /// <summary> Function invoked before drawing the filter inside the expanded combo list. </summary>
    protected virtual void PreDrawFilter()
    { }

    /// <summary> Function invoked after drawing the filter inside the expanded combo list. </summary>
    protected virtual void PostDrawFilter()
    { }

    /// <summary> Function invoked when the user presses Enter while the combo popup is open and focused. </summary>
    protected internal virtual void EnterPressed()
    { }

    /// <summary> Function invoked when the combo popup is closed either through selection or through pressing Enter. </summary>
    protected internal virtual void OnPopupClosed()
    { }
}

/// <summary> A base class for a combo supporting filtering, cached drawing and clipping. </summary>
/// <typeparam name="TCacheItem"> The type of the cache items to draw. </typeparam>
public abstract class FilterComboBase<TCacheItem> : FilterComboBase
{
    public FilterComboBase()
    { }

    public FilterComboBase(IFilter<TCacheItem> filter)
    {
        Filter = filter;
    }

    protected FilterComboBase(IFilter<TCacheItem> filter, in ConfigData config)
        : base(config)
    {
        Filter = filter;
    }

    /// <summary> The filter used. It is drawn at the top of the expanded combo unless it is a <see cref="NopFilter{TCacheItem}"/>, in which case it is ignored. </summary>
    public IFilter<TCacheItem> Filter { get; init; } = NopFilter<TCacheItem>.Instance;

    /// <summary> Obtain the list of all available cache items without filtering. </summary>
    protected internal abstract IEnumerable<TCacheItem> GetItems();


    /// <summary> Draw a single cache item, generally as a selectable. </summary>
    /// <param name="item"> The item to draw. </param>
    /// <param name="globalIndex"> The global index of the item. </param>
    /// <param name="selected"> Whether the item should be highlighted as selected </param>
    /// <returns> True if the item was selected. </returns>
    protected internal abstract bool DrawItem(in TCacheItem item, int globalIndex, bool selected);

    /// <summary> Check whether an item should be highlighted as the current selection. </summary>
    /// <param name="item"> The item to query. </param>
    /// <param name="globalIndex"> The global index of the item. </param>
    /// <returns> True if the item is currently selected. </returns>
    /// <remarks> Also used to compute the index of the currently selected item on appearing. </remarks>
    protected internal abstract bool IsSelected(TCacheItem item, int globalIndex);

    private FilterComboBaseCache<TCacheItem> CreateCacheInternal()
    {
        var ret = CreateCache();
        ret.KeepAliveDuration = CacheLifetime;
        return ret;
    }

    /// <summary> Create the cache used to draw the expanded combo list. </summary>
    protected virtual FilterComboBaseCache<TCacheItem> CreateCache()
        => new(this);

    /// <summary> Draw a combo with a given label, the given preview text and an optional tooltip. </summary>
    /// <param name="label"> The label of the combo. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="preview"> The preview text displayed in the combo. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="tooltip"> The tooltip shown when hovering the combo box. Omitted if this is empty. </param>
    /// <param name="previewWidth"> The width of the preview box for the combo. </param>
    /// <param name="ret"> If true is returned, a newly selected item. </param>
    /// <returns> True if a new item is selected by any means, false otherwise. </returns>
    public virtual bool Draw(Utf8LabelHandler label, Utf8HintHandler preview, Utf8TextHandler tooltip, float previewWidth,
        [NotNullWhen(true)] out TCacheItem? ret)
    {
        // Push the ID and save it for this frame.
        using var id = Im.Id.Push(ref label);
        CurrentId = Im.Id.Current;

        // Draw the combo and additional control handling.
        var exit = DrawCombo(ref label, ref preview, ref tooltip, previewWidth, out ret!);
        if (DrawMouseWheelHandling(out var ret2))
        {
            ret  = ret2;
            exit = true;
        }

        if (exit)
            return true;

        ret = default;
        return false;
    }

    /// <summary> Attach a combo behavior to the last drawn item, without drawing the combo preview box. </summary>
    /// <param name="label"> The label of the combo. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="popupWidth"> The width of the combo popup. </param>
    /// <param name="ret"> If true is returned, a newly selected item. </param>
    /// <returns> True if a new item is selected by any means, false otherwise. </returns>
    public virtual bool DrawBehavior(Utf8LabelHandler label, float popupWidth, [NotNullWhen(true)] out TCacheItem? ret)
    {
        // Push the ID and save it for this frame.
        using var id = Im.Id.Push(ref label);
        CurrentId = Im.Id.Current;

        ImEx.SplitLabel(ref label, out _, out var idSeed);
        var popupId     = Im.Id.Calculate("##ComboPopup"u8, idSeed);
        var returnValue = HandleComboPopup(popupWidth, popupId, Im.Item.Bounds, out _, out ret!);
        if (DrawMouseWheelHandling(out var ret2))
        {
            ret         = ret2;
            returnValue = true;
        }

        if (returnValue)
            return true;

        ret = default;
        return false;
    }

    /// <summary> Draw the combo itself. </summary>
    /// <param name="label"> The label of the combo. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="preview"> The preview text displayed in the combo. If this is a UTF8 string, it HAS to be null-terminated. </param>
    /// <param name="tooltip"> The tooltip shown when hovering the combo box. Omitted if this is empty. </param>
    /// <param name="previewWidth"> The width of the preview box for the combo. </param>
    /// <param name="ret"> If true is returned, a newly selected item. </param>
    /// <returns> True if a new item is selected by any means, false otherwise. </returns>
    protected virtual bool DrawCombo(ref Utf8LabelHandler label, ref Utf8HintHandler preview, ref Utf8TextHandler tooltip, float previewWidth,
        [NotNullWhen(true)] out TCacheItem? ret)
    {
        var       currentItemSpacing = Im.Style.ItemSpacing.Y;
        using var style              = Im.Style.PushDefault(ImStyleDouble.ItemSpacing);
        var       offset             = currentItemSpacing - Im.Style.ItemSpacing.Y;
        // Draw the combo itself.
        PreDrawCombo(previewWidth);
        Im.Item.SetNextWidth(previewWidth);
        var flags = Flags.CheckAny(ComboFlags.HeightMask) ? Flags : Flags | ComboFlags.HeightLarge;
        Im.Combo.DrawPreview(label, preview, out var id, out var boundingBox, flags, PreviewAlignment);
        if (offset is not 0)
            Im.Cursor.Y += offset;
        PostDrawCombo(previewWidth);

        // Draw the tooltip if not empty.
        if (tooltip.GetSpan(out var tooltipSpan) && !tooltipSpan.IsEmpty)
        {
            using var enabled = Im.Enabled();
            Im.Tooltip.OnHover(tooltipSpan, true);
        }

        var returnValue = HandleComboPopup(previewWidth, id, boundingBox, out var exit, out ret);
        if (exit)
            return returnValue;

        if (DirtyCacheOnClosingPopup)
            CacheManager.Instance.SetDirty(CurrentId);

        ret = default;
        return false;
    }

    /// <summary> Draws the popup if it is open. </summary>
    /// <param name="width"> The width of the combo popup. </param>
    /// <param name="id"> The ID of the combo popup. </param>
    /// <param name="boundingBox"> The screen-space bounds of the preview box. </param>
    /// <param name="exit"> True if <see cref="DrawCombo"/> shall exit immediately after this function returns. </param>
    /// <param name="ret"> If true is returned, a newly selected item. </param>
    /// <returns> True if a new item is selected by any means, false otherwise. </returns>
    protected virtual bool HandleComboPopup(float width, ImGuiId id, Rectangle boundingBox, out bool exit, [NotNullWhen(true)] out TCacheItem? ret)
    {
        // If the combo is expanded, draw the filter and list.
        if (Im.Popup.IsOpen(id))
        {
            SetPopupWindowSize(width);
            using var style = Im.Style.PushX(ImStyleDouble.FramePadding, 0).Push(ImStyleDouble.WindowPadding, Vector2.Zero)
                .Push(ImStyleSingle.PopupBorderThickness, Im.Style.GlobalScale);
            using var popup = Im.Combo.DrawPopup(id, boundingBox, Flags | ComboFlags.HeightLarge);
            exit = true;
            return DrawComboPopup(out ret);
        }

        ret  = default;
        exit = false;
        return false;
    }

    /// <summary> Sets up the size for the popup based on the current required size if a cache exists, and the height of the items and filter. </summary>
    /// <param name="previewWidth"> The width of the preview widget which is used as the minimum of the combo popup width. </param>
    protected virtual void SetPopupWindowSize(float previewWidth)
    {
        var width = previewWidth;
        // A filter adds a frame height to the window.
        var additionalHeight = Filter.IsVisible ? Im.Style.FrameHeight : 0;
        var height           = MaximumItems * ItemHeight + additionalHeight;

        // If we have an active cache, we have information about the actual width and height needed.
        if (CacheManager.Instance.TryGetCache(CurrentId, out FilterComboBaseCache<TCacheItem>? cache))
        {
            if (ComputeWidth)
                width = Math.Max(cache.ComboWidth, width);

            if (cache.Count <= MaximumItems)
                height = Math.Max(1, cache.Count) * ItemHeight + additionalHeight + Im.Style.ItemSpacing.Y / 2;
        }

        // Set the popup size to fixed values to avoid scroll bars and unnecessary padding.
        // This gets automatically removed if the combo popup is not drawn.
        Im.Window.SetNextSize(new Vector2(width, height));
    }

    /// <summary> Draw the expanded combo popup. </summary>
    /// <param name="ret"> If true is returned, a newly selected item. </param>
    /// <returns> True if a new item is selected by any means, false otherwise. </returns>
    protected virtual bool DrawComboPopup([NotNullWhen(true)] out TCacheItem? ret)
    {
        using var style = Im.Style.PushDefault(ImStyleDouble.FramePadding);
        var       cache = CacheManager.Instance.GetOrCreateCache(CurrentId, CreateCacheInternal);
        // If the filter is changed, set it dirty for the next frame.
        if (DrawFilter(Im.Window.Width, cache))
            cache.Dirty |= IManagedCache.DirtyFlags.Custom;

        // Draw the list.
        if (cache.DrawList(out var globalIndex))
        {
            PostDrawList();
            ret = cache.AllItems[globalIndex]!;
            // Clear the filter on selection change given the setting.
            if (ClearFilterOnSelection)
            {
                Filter.Clear();
                cache.Dirty |= IManagedCache.DirtyFlags.Custom;
            }

            return true;
        }

        ret = default;
        return false;
    }

    /// <summary> Draw the filter on top of the item list. </summary>
    /// <returns> True if the filter changed. </returns>
    protected virtual bool DrawFilter(float width, FilterComboBaseCache<TCacheItem> cache)
    {
        if (!Filter.IsVisible)
            return false;

        Im.Cursor.Position = Vector2.Zero;
        PreDrawFilter();
        if (Im.Window.Appearing)
            Im.Keyboard.SetFocusHere();

        var ret = Filter.DrawFilter("筛选..."u8, new Vector2(width, Im.Style.FrameHeight));
        // Remove the spacing after the filter.
        Im.Cursor.Y -= Im.Style.ItemSpacing.Y;

        PostDrawFilter();
        return ret;
    }

    /// <summary> Handle mouse-wheel interaction when hovering the combo. </summary>
    /// <param name="ret"></param>
    /// <returns></returns>
    protected virtual bool DrawMouseWheelHandling([NotNullWhen(true)] out TCacheItem? ret)
    {
        // Check hovering and mouse-wheel activity.
        if (Im.Item.Hovered() && AllowMouseWheel.CheckMouseWheel())
        {
            // Set the item to consume the mouse wheel.
            Im.Item.SetUsingMouseWheel();
            // Use the mouse wheel delta to select a new item. This may require creating a new cache.
            var delta = (int)Im.Io.MouseWheel;
            if (delta is not 0)
            {
                var cache = CacheManager.Instance.GetOrCreateCache(CurrentId, CreateCacheInternal);
                if (cache.HandleMouseWheel(delta, out var newIndex))
                {
                    ret = cache.AllItems[newIndex]!;

                    // Clear the filter on selection change given the setting.
                    if (ClearFilterOnSelection)
                    {
                        Filter.Clear();
                        cache.Dirty |= IManagedCache.DirtyFlags.Custom;
                    }

                    return true;
                }
            }
        }

        ret = default;
        return false;
    }
}
