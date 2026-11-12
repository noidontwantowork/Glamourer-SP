namespace ImSharp.ImNodes;

using ImSharp;

public static partial class Internal
{
    public readonly record struct ColorStackData(Rgba32 Color, ImNodesColor Type) : ITrivialTypeInformation<ColorStackData>;

    public readonly record struct StyleStackData(ImNodesStyleDouble Type, Vector2 Value) : ITrivialTypeInformation<StyleStackData>
    {
        public StyleStackData(ImNodesStyleSingle type, float value)
            : this((ImNodesStyleDouble)type, new Vector2(value, float.NaN))
        { }
    }
}
