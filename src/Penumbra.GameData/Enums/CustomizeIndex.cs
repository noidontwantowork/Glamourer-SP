using ImSharp;
using Luna.Generators;

namespace Penumbra.GameData.Enums;

/// <summary> All options that can be configured for humans via the customize array. </summary>
[NamedEnum]
public enum CustomizeIndex : byte
{
    [Name("种族")]
    Race,

    [Name("性别")]
    Gender,

    [Name("身型")]
    BodyType,

    [Name("身高")]
    Height,

    [Name("部族")]
    Clan,

    [Name("脸型")]
    Face,

    [Name("发型")]
    Hairstyle,

    [Name("启用挑染")]
    Highlights,

    [Name("肤色")]
    SkinColor,

    [Name("右眼")]
    EyeColorRight,

    [Name("发色")]
    HairColor,

    [Name("挑染颜色")]
    HighlightsColor,

    [Name("黑痣与伤痕等 1")]
    FacialFeature1,

    [Name("黑痣与伤痕等 2")]
    FacialFeature2,

    [Name("黑痣与伤痕等 3")]
    FacialFeature3,

    [Name("黑痣与伤痕等 4")]
    FacialFeature4,

    [Name("黑痣与伤痕等 5")]
    FacialFeature5,

    [Name("黑痣与伤痕等 6")]
    FacialFeature6,

    [Name("黑痣与伤痕等 7")]
    FacialFeature7,

    [Name("遗产纹身")]
    LegacyTattoo,

    [Name("纹身颜色")]
    TattooColor,

    [Name("眉形")]
    Eyebrows,

    [Name("左眼")]
    EyeColorLeft,

    [Name("较小瞳孔")]
    EyeShape,

    [Name("较小眼瞳")]
    SmallIris,

    [Name("鼻型")]
    Nose,

    [Name("脸部轮廓")]
    Jaw,

    [Name("嘴型")]
    Mouth,

    [Name("启用唇色")]
    Lipstick,

    [Name("唇色")]
    LipColor,

    [Name("肌肉")]
    MuscleMass,

    [Name("尾巴形状")]
    TailShape,

    [Name("胸围")]
    BustSize,

    [Name("面妆")]
    FacePaint,

    [Name("反转面妆")]
    FacePaintReversed,

    [Name("面妆颜色")]
    FacePaintColor,
}

public static class CustomizationExtensions
{
    /// <summary> The total number of options. </summary>
    public const int NumIndices = (int)CustomizeIndex.FacePaintColor + 1;

    /// <summary> A list of all options that are not race or body type. </summary>
    public static readonly CustomizeIndex[] All = CustomizeIndex.Values
        .Where(v => v is not CustomizeIndex.Race and not CustomizeIndex.BodyType).ToArray();

    /// <summary> A set of all options that are not race, gender, clan or body type. </summary>
    public static readonly CustomizeIndex[] AllBasic = All
        .Where(v => v is not CustomizeIndex.Gender and not CustomizeIndex.Clan).ToArray();

    /// <summary> A set of all options that are not race, gender, clan, face, or body type. </summary>
    public static readonly CustomizeIndex[] AllBasicWithoutFace = AllBasic
        .Where(v => v is not CustomizeIndex.Face).ToArray();

    /// <summary> Get the index of the customization option in the customize array, and a mask for its value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (int ByteIdx, byte Mask) ToByteAndMask(this CustomizeIndex index)
        => index switch
        {
            CustomizeIndex.Race              => (0, 0xFF),
            CustomizeIndex.Gender            => (1, 0xFF),
            CustomizeIndex.BodyType          => (2, 0xFF),
            CustomizeIndex.Height            => (3, 0xFF),
            CustomizeIndex.Clan              => (4, 0xFF),
            CustomizeIndex.Face              => (5, 0xFF),
            CustomizeIndex.Hairstyle         => (6, 0xFF),
            CustomizeIndex.Highlights        => (7, 0x80),
            CustomizeIndex.SkinColor         => (8, 0xFF),
            CustomizeIndex.EyeColorRight     => (9, 0xFF),
            CustomizeIndex.HairColor         => (10, 0xFF),
            CustomizeIndex.HighlightsColor   => (11, 0xFF),
            CustomizeIndex.FacialFeature1    => (12, 0x01),
            CustomizeIndex.FacialFeature2    => (12, 0x02),
            CustomizeIndex.FacialFeature3    => (12, 0x04),
            CustomizeIndex.FacialFeature4    => (12, 0x08),
            CustomizeIndex.FacialFeature5    => (12, 0x10),
            CustomizeIndex.FacialFeature6    => (12, 0x20),
            CustomizeIndex.FacialFeature7    => (12, 0x40),
            CustomizeIndex.LegacyTattoo      => (12, 0x80),
            CustomizeIndex.TattooColor       => (13, 0xFF),
            CustomizeIndex.Eyebrows          => (14, 0xFF),
            CustomizeIndex.EyeColorLeft      => (15, 0xFF),
            CustomizeIndex.EyeShape          => (16, 0x7F),
            CustomizeIndex.SmallIris         => (16, 0x80),
            CustomizeIndex.Nose              => (17, 0xFF),
            CustomizeIndex.Jaw               => (18, 0xFF),
            CustomizeIndex.Mouth             => (19, 0x7F),
            CustomizeIndex.Lipstick          => (19, 0x80),
            CustomizeIndex.LipColor          => (20, 0xFF),
            CustomizeIndex.MuscleMass        => (21, 0xFF),
            CustomizeIndex.TailShape         => (22, 0xFF),
            CustomizeIndex.BustSize          => (23, 0xFF),
            CustomizeIndex.FacePaint         => (24, 0x7F),
            CustomizeIndex.FacePaintReversed => (24, 0x80),
            CustomizeIndex.FacePaintColor    => (25, 0xFF),
            _                                => (0, 0x00),
        };
}
