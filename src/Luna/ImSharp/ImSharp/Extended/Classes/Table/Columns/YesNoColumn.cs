namespace ImSharp.Table;

/// <summary> A column that can display a checkmark or a cross and filter for both options. </summary>
/// <typeparam name="TCacheItem"> The type of the cached transformation of the items to display. </typeparam>
public abstract class YesNoColumn<TCacheItem> : TriStateFlagColumn<YesNoFlag, TCacheItem>
{
    /// <summary> The color to use for the checkmark representing true. </summary>
    protected virtual Rgba32 YesColor
        => ImGuiColor.CheckMark.Get();

    /// <summary> The color to use for the x representing false. </summary>
    protected virtual Rgba32 NoColor
        => ImGuiColor.CheckMark.Get();

    /// <summary> The label to display for the tri-state checkbox filter. </summary>
    public StringU8 FilterLabel { get; init; } = new("启用"u8);

    /// <summary> Create a new YesNoColumn. </summary>
    protected YesNoColumn()
        => Filter = new YesNoFilter(this)
        {
            AllFlags = YesNoFlag.Yes | YesNoFlag.No,
        };

    /// <summary> The width can always be given by twice the frame height. </summary>
    public override float ComputeWidth(IEnumerable<TCacheItem> allItems)
        => 2 * Im.Style.FrameHeight;

    /// <inheritdoc/>
    public override int Compare(in TCacheItem lhs, int lhsGlobalIndex, in TCacheItem rhs, int rhsGlobalIndex)
        => GetValue(lhs, lhsGlobalIndex, 0).CompareTo(GetValue(rhs, rhsGlobalIndex, 0));

    /// <summary> Draw a centered checkmark or an x and the tooltip. </summary>
    public override void DrawColumn(in TCacheItem item, int globalIndex)
    {
        var iconSize = Im.Style.TextHeight;
        var center   = (Im.ContentRegion.Available.X - iconSize) / 2;
        if (center > 0)
            Im.Cursor.X += center;
        Im.Cursor.Y += Im.Style.FramePadding.Y;
        if (GetValue(item, globalIndex, 0))
            Im.Render.Checkmark(Im.Window.DrawList, Im.Cursor.ScreenPosition, YesColor, iconSize);
        else
            Im.Render.Cross(Im.Window.DrawList, Im.Cursor.ScreenPosition, NoColor, iconSize);
        Im.Dummy(iconSize, iconSize);
        if (Im.Item.Hovered(HoveredFlags.AllowWhenDisabled))
            DrawTooltip(item, globalIndex);
    }

    /// <inheritdoc/>
    /// <remarks> Should not be called. </remarks>
    protected override IReadOnlyList<(YesNoFlag On, YesNoFlag Off, StringU8 Name)> TriEnumData
        => [(YesNoFlag.Yes, YesNoFlag.No, FilterLabel)];

    protected override StringU8 DisplayString(in TCacheItem item, int globalIndex)
        => StringU8.Empty;

    protected class YesNoFilter : YesNoFilter<TCacheItem>
    {
        private readonly YesNoColumn<TCacheItem> _parent;

        public YesNoFilter(YesNoColumn<TCacheItem> parent)
        {
            _parent     = parent;
            FilterLabel = parent.FilterLabel;
        }

        // <inheritdoc/>
        public override bool GetValue(in TCacheItem item, int globalIndex, int triEnumIndex)
            => _parent.GetValue(item, globalIndex, triEnumIndex);
    }
}
