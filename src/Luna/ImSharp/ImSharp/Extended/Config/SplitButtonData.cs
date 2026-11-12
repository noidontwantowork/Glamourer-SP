namespace ImSharp;

public static partial class ImEx
{
    /// <summary> Data to describe a side of the split button. </summary>
    /// <param name="label"> The label drawn into the corresponding button half. </param>
    public readonly struct SplitButtonData(StringU8 label)
    {
        /// <summary> The label drawn into the corresponding button half. </summary>
        public readonly StringU8 Label = label;

        /// <summary> The optional tooltip shown when hovering over the corresponding half of the button. </summary>
        public StringU8 Tooltip { get; init; } = StringU8.Empty;

        /// <summary> The background color of the corresponding button half when neither active nor hovered. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Button"/> is used. </summary>
        public ColorParameter Background { get; init; } = ColorParameter.Default;

        /// <summary> The background color of the corresponding button half when active. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.ButtonActive"/> is used. </summary>
        public ColorParameter Active { get; init; } = ColorParameter.Default;

        /// <summary> The background color of the corresponding button half when hovered. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.ButtonHovered"/> is used. </summary>
        public ColorParameter Hovered { get; init; } = ColorParameter.Default;

        /// <summary> The outer border color of the corresponding button half when active. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Border"/> is used. </summary>
        public ColorParameter Border { get; init; } = ColorParameter.Default;

        /// <summary> The color for the label in the corresponding button half. If this is <see cref="ColorParameter.Default"/>, <see cref="ImGuiColor.Text"/> is used. </summary>
        public ColorParameter Text { get; init; } = ColorParameter.Default;

        /// <summary> Get the correct color depending on hovering and active state. </summary>
        public Rgba32 GetColor(bool isHovered, bool isHeld)
            => isHovered
                ? isHeld
                    ? Active.CheckDefault(ImGuiColor.ButtonActive)
                    : Hovered.CheckDefault(ImGuiColor.ButtonHovered)
                : Background.CheckDefault(ImGuiColor.Button);
    }
}
