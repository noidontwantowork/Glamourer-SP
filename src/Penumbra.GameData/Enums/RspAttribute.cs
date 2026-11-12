using Luna.Generators;

namespace Penumbra.GameData.Enums;

/// <summary> All available racial scaling parameters. </summary>
[NamedEnum]
public enum RspAttribute : byte
{
    [Name("男性身体最小尺寸")]
    MaleMinSize,

    [Name("男性身体最大尺寸")]
    MaleMaxSize,

    [Name("男性尾巴最小长度")]
    MaleMinTail,

    [Name("男性尾巴最大长度")]
    MaleMaxTail,

    [Name("女性身体最小尺寸")]
    FemaleMinSize,

    [Name("女性身体最大尺寸")]
    FemaleMaxSize,

    [Name("女性尾巴最小长度")]
    FemaleMinTail,

    [Name("女性尾巴最大长度")]
    FemaleMaxTail,

    [Name("胸围最小X轴")]
    BustMinX,

    [Name("胸围最小Y轴")]
    BustMinY,

    [Name("胸围最小Z轴")]
    BustMinZ,

    [Name("胸围最大X轴")]
    BustMaxX,

    [Name("胸围最大Y轴")]
    BustMaxY,

    [Name("胸围最大Z轴")]
    BustMaxZ,

    NumAttributes,
}

public static partial class RspAttributeExtensions
{
    /// <summary> For which gender a certain racial scaling parameter is available. </summary>
    public static Gender ToGender(this RspAttribute attribute)
        => attribute switch
        {
            RspAttribute.MaleMinSize   => Gender.Male,
            RspAttribute.MaleMaxSize   => Gender.Male,
            RspAttribute.MaleMinTail   => Gender.Male,
            RspAttribute.MaleMaxTail   => Gender.Male,
            RspAttribute.FemaleMinSize => Gender.Female,
            RspAttribute.FemaleMaxSize => Gender.Female,
            RspAttribute.FemaleMinTail => Gender.Female,
            RspAttribute.FemaleMaxTail => Gender.Female,
            RspAttribute.BustMinX      => Gender.Female,
            RspAttribute.BustMinY      => Gender.Female,
            RspAttribute.BustMinZ      => Gender.Female,
            RspAttribute.BustMaxX      => Gender.Female,
            RspAttribute.BustMaxY      => Gender.Female,
            RspAttribute.BustMaxZ      => Gender.Female,
            _                          => Gender.Unknown,
        };
}
