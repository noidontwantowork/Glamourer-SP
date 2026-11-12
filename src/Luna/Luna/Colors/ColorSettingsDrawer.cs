using Dalamud.Interface.ImGuiNotification;

namespace Luna;

/// <summary> Draw a fully fledged settings panel for a custom color dictionary. </summary>
public static class ColorSettingsDrawer
{
    /// <summary> Draw the settings panel including buttons to import, export and reset. </summary>
    /// <typeparam name="TColorId"> The color ID type. </typeparam>
    /// <typeparam name="TColorData"> The associated color data. </typeparam>
    /// <param name="messages"> A messager for failures when importing from clipboard. </param>
    /// <param name="dict"> The color dictionary. </param>
    /// <param name="cache"> The cache with current colors. </param>
    /// <returns> True if the color dictionary was changed in this frame, in which case its own event will also be triggered. </returns>
    public static bool Draw<TColorId, TColorData>(MessageService messages, ColorDictionary<TColorId, TColorData> dict,
        ColorCache<TColorId, TColorData> cache)
        where TColorId : unmanaged, Enum
        where TColorData : IColorData<TColorId>
    {
        if (Im.Button("复制到剪贴板"u8))
            Im.Clipboard.Set(dict.Sharable(true));

        Im.Line.Same();
        var ret = DrawImportButtons(messages, dict);
        Im.Line.Same();
        if (ImEx.Button("全部重置为默认值"u8, default, "重置所有颜色值为默认值。"u8,
                !LunaStyle.Modifier.Destructive))
            ret |= dict.ResetToDefault();
        LunaStyle.Modifier.Destructive.TooltipLineBreak("reset"u8);
        LunaStyle.DrawSeparator();
        return ret | DrawSettings(dict, cache);
    }

    private static bool DrawSettings<TColorId, TColorData>(ColorDictionary<TColorId, TColorData> dict,
        ColorCache<TColorId, TColorData> cache)
        where TColorId : unmanaged, Enum
        where TColorData : IColorData<TColorId>
    {
        var drawCache = CacheManager.Instance.GetOrCreateCache(Im.Id.Current, () => new Cache<TColorId, TColorData>(dict));
        var ret       = false;
        foreach (var (index, (category, colors)) in drawCache.Sections.Index())
        {
            using var id   = Im.Id.Push(index);
            using var tree = Im.Tree.Node(category.IsEmpty ? "通用"u8 : category, TreeNodeFlags.DefaultOpen);
            if (!tree)
                continue;

            using var clip = new Im.ListClipper(colors.Count, Im.Style.FrameHeightWithSpacing);
            foreach (var (colorId, colorData) in clip.Iterate(colors))
                ret |= ColorPicker(drawCache, colorId, colorData, dict, cache);
            if(index != drawCache.Sections.Count)
                LunaStyle.DrawSeparator();
        }

        return ret;
    }

    private static unsafe bool ColorPicker<TColorId, TColorData>(Cache<TColorId, TColorData> drawCache, TColorId id,
        in ColorData<TColorId> data, ColorDictionary<TColorId, TColorData> dict, ColorCache<TColorId, TColorData> cache)
        where TColorId : unmanaged, Enum
        where TColorData : IColorData<TColorId>
    {
        var       ret   = false;
        using var _     = Im.Id.Push(*(int*)&id);
        using var group = Im.Group();

        // Draw the regular color picker with no label.
        var currentActualColor = cache[id, true];
        var setValue           = dict[id];
        if (Im.Color.Editor("##P"u8, ref currentActualColor, ColorEditorFlags.AlphaPreviewHalf | ColorEditorFlags.NoInputs))
        {
            dict[id] = new ColorDataUnion(currentActualColor);
            ret      = true;
        }

        Im.Line.SameInner();

        if (drawCache.Draw(id, cache[id], setValue, out var newValue))
        {
            dict[id] = newValue;
            ret      = true;
        }

        // Draw a button to return to default.
        Im.Line.SameInner();
        if (ImEx.Button("默认"u8, Vector2.Zero, StringU8.Empty, setValue.IsDefault))
        {
            dict.Remove(id);
            ret = true;
        }

        if (Im.Item.Hovered(HoveredFlags.AllowWhenDisabled))
            DrawTooltip(cache, data);

        // Draw the actual label as well as a potential tooltip.
        Im.Line.SameInner();
        Im.Text(data.Label);
        Im.Tooltip.OnHover(data.Description);
        if (setValue.Type is not ColorDataUnion.TypeEnum.Const and not ColorDataUnion.TypeEnum.Default)
        {
            Im.Line.SameInner();
            Im.TextDisabled($"(自定义引用为 {setValue.ToStringU8<TColorId>(TColorData.Parent)})");
        }
        else if (setValue.Type is ColorDataUnion.TypeEnum.Default && data.Default.Type is not ColorDataUnion.TypeEnum.Const)
        {
            Im.Line.SameInner();
            Im.TextDisabled($"(默认引用为 {data.Default.ToStringU8<TColorId>(TColorData.Parent)})");
        }

        return ret;
    }

    private static void DrawTooltip<TColorId, TColorData>(ColorCache<TColorId, TColorData> cache, ColorData<TColorId> colorData)
        where TColorId : unmanaged, Enum
        where TColorData : IColorData<TColorId>
    {
        using var tt = Im.Tooltip.Begin();
        Vector4   current;
        switch (colorData.Default.Type)
        {
            case ColorDataUnion.TypeEnum.Const:
                current = colorData.Default.ConstantValue.ToVector();
                ImEx.TextFrameAligned($"重置此颜色为 {colorData.Default.ConstantValue}。");
                break;
            case ColorDataUnion.TypeEnum.Self:
                var parent      = TColorData.Data(colorData.Default.SelfValue<TColorId>());
                var parentValue = cache[colorData.Default.SelfValue<TColorId>()];
                current = cache[colorData.Default.SelfValue<TColorId>(), true];
                ImEx.TextFrameAligned(
                    $"重置此颜色为 {TColorData.Parent} 颜色 <{parent.Label}> (当前为 {parentValue})。");
                break;
            case ColorDataUnion.TypeEnum.ImGui:
                current = cache[colorData.Default.ImGuiValue, true];
                ImEx.TextFrameAligned(
                    $"重置此颜色为 ImGui 颜色 <{Im.Color.GetNameOwned(colorData.Default.ImGuiValue)}> (当前为 {cache[colorData.Default.ImGuiValue]})。");
                break;
            case ColorDataUnion.TypeEnum.Dalamud:
                current = cache[colorData.Default.DalamudValue, true];
                ImEx.TextFrameAligned(
                    $"重置此颜色为 Dalamud 颜色 <{colorData.Default.DalamudValue.StringU8}> (当前为 {cache[colorData.Default.DalamudValue]})。");
                break;
            default: throw new Exception("Unknown Color Type");
        }

        Im.Line.SameInner();
        Im.Color.Editor(StringU8.Empty, ref current, ColorEditorFlags.AlphaPreviewHalf | ColorEditorFlags.NoInputs);
    }

    private static bool DrawImportButtons<TColorId, TColorData>(MessageService messages, ColorDictionary<TColorId, TColorData> dict)
        where TColorId : unmanaged, Enum
        where TColorData : IColorData<TColorId>
    {
        var ignoreDefaults = ImEx.Button("从剪贴板导入 (忽略默认值)"u8,
            default,
            "尝试从剪贴板导入导出的颜色值，若导入项为默认值，则不覆盖已修改的数值。"u8,
            !LunaStyle.Modifier.Misclick);
        LunaStyle.Modifier.Misclick.TooltipLineBreak("import"u8);

        Im.Line.Same();
        var applyDefaults = ImEx.Button("从剪贴板导入 (覆盖所有)"u8, default,
            "尝试从剪贴板导入颜色值，覆盖所有已设置的值。"u8, !LunaStyle.Modifier.Misclick);
        LunaStyle.Modifier.Misclick.TooltipLineBreak("import"u8);

        if (!ignoreDefaults && !applyDefaults)
            return false;

        try
        {
            if (ColorDictionary<TColorId, TColorData>.FromSharable(Im.Clipboard.Get(), true) is { } parsedDict)
                return dict.Apply(parsedDict, applyDefaults);

            throw new Exception("无法从剪贴板解析颜色字典。");
        }
        catch (Exception ex)
        {
            messages.NotificationMessage(ex, "导入颜色字典失败", NotificationType.Error, false);
        }

        return false;
    }

    private sealed class Cache<TColorId, TColorData>(ColorDictionary<TColorId, TColorData> dictionary) : BasicCache
        where TColorId : unmanaged, Enum
        where TColorData : IColorData<TColorId>
    {
        private readonly ColorTypeCombo<TColorId, TColorData>   _type        = new(dictionary);
        private readonly ImGuiColorCombo                        _imGui       = new();
        private readonly ImNodesColorCombo                      _imNodes     = new();
        private readonly DalamudColorCombo                      _dalamud     = new();
        private readonly LunaColorCombo                         _luna        = new();
        private readonly CustomColorCombo<TColorId, TColorData> _customCombo = new(dictionary);

        public bool Draw(TColorId id, Rgba32 currentColor, ColorDataUnion input, out ColorDataUnion output)
        {
            var totalWidth = 320 * Im.Style.GlobalScale;
            var (typeWidth, comboWidth) = input.Type is ColorDataUnion.TypeEnum.Default or ColorDataUnion.TypeEnum.Const
                ? (totalWidth, 0)
                : (100 * Im.Style.GlobalScale, totalWidth - 100 * Im.Style.GlobalScale - Im.Style.ItemInnerSpacing.X);
            var ret = _type.Draw(id, input.Type, currentColor, out output, typeWidth);
            Im.Line.SameInner();
            switch (input.Type)
            {
                case ColorDataUnion.TypeEnum.Self:
                    if (_customCombo.Draw(id, input.SelfValue<TColorId>(), out var newColor, comboWidth))
                    {
                        output = ColorDataUnion.FromSelf(newColor);
                        ret    = true;
                    }

                    break;
                case ColorDataUnion.TypeEnum.ImGui:
                    if (_imGui.Draw("##imgui"u8, input.ImGuiValue, "选择一个 ImGui 颜色引用。"u8, comboWidth, out var newImGui))
                    {
                        output = new ColorDataUnion(newImGui);
                        ret    = true;
                    }

                    break;
                case ColorDataUnion.TypeEnum.ImNodes:
                    if (_imNodes.Draw("##imNodes"u8, input.ImNodesValue, "选择一个 ImNodes 颜色引用。"u8, comboWidth,
                            out var newImNodes))
                    {
                        output = new ColorDataUnion(newImNodes);
                        ret    = true;
                    }

                    break;
                case ColorDataUnion.TypeEnum.Dalamud:
                    if (_dalamud.Draw("##dalamud"u8, input.DalamudValue, "选择一个 Dalamud 颜色引用。"u8, comboWidth,
                            out var newDalamud))
                    {
                        output = new ColorDataUnion(newDalamud);
                        ret    = true;
                    }

                    break;
                case ColorDataUnion.TypeEnum.Luna:
                    if (_luna.Draw("##luna"u8, input.LunaValue, "选择一个 Luna 颜色引用。"u8, comboWidth, out var newLuna))
                    {
                        output = new ColorDataUnion(newLuna);
                        ret    = true;
                    }

                    break;
            }

            return ret;
        }

        public readonly IReadOnlyList<(StringU8 Section, IReadOnlyList<(TColorId Id, ColorData<TColorId> Data)>)> Sections
            = EnumExtensions.get_Values<TColorId>()
                .Select(id => (id, TColorData.Data(id)))
                .GroupBy(p => p.Item2.Section)
                .Select(g => (g.Key, (IReadOnlyList<(TColorId Id, ColorData<TColorId> Data)>)g.Select(d => (d.id, d.Item2)).ToArray()))
                .ToArray();

        public override void Update()
        { }
    }
}
