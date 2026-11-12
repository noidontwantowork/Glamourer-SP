using Luna.Generators;

namespace Penumbra.GameData.Enums;

/// <summary> Types of game objects or identities. </summary>
[NamedEnum]
public enum ObjectType : byte
{
    Unknown,

    [Name("视觉效果")]
    Vfx,

    [Name("亚人")]
    DemiHuman,

    [Name("配饰")]
    Accessory,

    [Name("小物件")]
    World,

    [Name("装修物品")]
    Housing,

    [Name("怪物")]
    Monster,

    [Name("图标")]
    Icon,

    [Name("加载界面")]
    LoadingScreen,

    [Name("地图")]
    Map,

    [Name("UI元素")]
    Interface,

    [Name("装备")]
    Equipment,

    [Name("角色")]
    Character,

    [Name("武器")]
    Weapon,

    [Name("字体")]
    Font,
}

public static partial class ObjectTypeExtensions
{
    /// <summary> A list of valid object types for IMC files. </summary>
    public static readonly IReadOnlyList<ObjectType> ValidImcTypes =
    [
        ObjectType.Equipment,
        ObjectType.Accessory,
        ObjectType.DemiHuman,
        ObjectType.Monster,
        ObjectType.Weapon,
    ];
}
