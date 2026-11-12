using ImSharp;
using Lumina.Excel.Sheets;
using Luna.Generators;

namespace Penumbra.GameData.Enums;

/// <summary> A full equipment type representing any type of equipment a character can wear. </summary>
[NamedEnum]
public enum FullEquipType : byte
{
    [Name(Omit: true)]
    Unknown,

    [Name("头部")]
    Head,

    [Name("身体")]
    Body,

    [Name("手臂")]
    Hands,

    [Name("腿部")]
    Legs,

    [Name("脚部")]
    Feet,

    [Name("耳部")]
    Ears,

    [Name("颈部")]
    Neck,

    [Name("腕部")]
    Wrists,

    [Name("戒指")]
    Finger,

    [Name("格斗武器")]
    Fists, // PGL, MNK

    [Name("格斗武器（副手）")]
    FistsOff,

    [Name("单手剑")]
    Sword, // GLA, PLD Main

    [Name("大斧")]
    Axe, // MRD, WAR

    [Name("弓")]
    Bow, // ARC, BRD

    [Name("箭袋")]
    BowOff,

    [Name("长枪")]
    Lance, // LNC, DRG,

    [Name("双手杖")]
    StaffBlm, // THM, BLM,

    [Name("单手杖")]
    Wand, // CNJ, WHM Main

    [Name("魔导书")]
    Book, // ACN, SMN, SCH

    [Name("双剑")]
    Daggers, // ROG, NIN

    [Name("双剑（副手）")]
    DaggersOff,

    [Name("双手剑")]
    Broadsword, // DRK,

    [Name("火枪")]
    Gun, // MCH,

    [Name("以太变换器")]
    GunOff,

    [Name("天球仪")]
    Orrery, // AST,

    [Name("卡套")]
    OrreryOff,

    [Name("武士刀")]
    Katana, // SAM

    [Name("刀鞘")]
    KatanaOff,

    [Name("刺剑")]
    Rapier, // RDM

    [Name("触媒")]
    RapierOff,

    [Name("青魔杖")]
    Cane, // BLU

    [Name("枪刃")]
    Gunblade, // GNB,

    [Name("投掷武器")]
    Glaives, // DNC,

    [Name("投掷武器（副手）")]
    GlaivesOff,

    [Name("镰刀")]
    Scythe, // RPR,

    [Name("贤具")]
    Nouliths, // SGE

    [Name("盾")]
    Shield, // GLA, PLD, THM, BLM, CNJ, WHM Off

    [Name("刻木工具")]
    Saw, // CRP

    [Name("锻铁工具")]
    CrossPeinHammer, // BSM

    [Name("铸甲工具")]
    RaisingHammer, // ARM

    [Name("雕金工具")]
    LapidaryHammer, // GSM

    [Name("制革工具")]
    Knife, // LTW

    [Name("裁衣工具")]
    Needle, // WVR

    [Name("炼金工具")]
    Alembic, // ALC

    [Name("烹调工具")]
    Frypan, // CUL

    [Name("采矿工具")]
    Pickaxe, // MIN

    [Name("园艺工具")]
    Hatchet, // BTN

    [Name("捕鱼用具")]
    FishingRod, // FSH

    [Name("羊角锤")]
    ClawHammer, // CRP Off

    [Name("锉刀")]
    File, // BSM Off

    [Name("手钳")]
    Pliers, // ARM Off

    [Name("砂轮机")]
    GrindingWheel, // GSM Off

    [Name("平斩")]
    Awl, // LTW Off

    [Name("纺车")]
    SpinningWheel, // WVR Off

    [Name("研钵")]
    Mortar, // ALC Off

    [Name("厨刀")]
    CulinaryKnife, // CUL Off

    [Name("碎石锤")]
    Sledgehammer, // MIN Off

    [Name("园艺镰刀")]
    GardenScythe, // BTN Off

    [Name("渔叉")]
    Gig, // FSH Off

    [Name("笔刷")]
    Brush, // PCT

    [Name("调色盘")]
    Palette, // PCT Off

    [Name("双剑")]
    Twinfangs, // VPR

    [Name("双剑（副手）")]
    TwinfangsOff, // VPR Off

    [Name("单手斧")]
    Handaxe, // BST

    [Name("眼镜")]
    Glasses,

    [Name("主手武器")]
    UnknownMainhand,

    [Name("副手武器")]
    UnknownOffhand,

    [Name("双手咒杖")]
    StaffWhm, // CNJ, WHM

    [Name("单手咒杖")]
    Scepter, // THM, BLM, Main
}

public static partial class FullEquipTypeExtensions
{
    /// <summary> Obtain the FullEquipType of an item. </summary>
    internal static FullEquipType ToEquipType(this Item item)
    {
        var slot   = (EquipSlot)item.EquipSlotCategory.RowId;
        var weapon = (WeaponCategory)item.ItemUICategory.RowId;
        return slot.ToEquipType(weapon);
    }

    /// <summary> Return whether a FullEquipType is not fully known. </summary>
    public static bool IsUnknown(this FullEquipType type)
        => type is FullEquipType.Unknown or FullEquipType.UnknownMainhand or FullEquipType.UnknownOffhand;

    /// <summary> Return whether a FullEquipType is a primary weapon type. </summary>
    public static bool IsWeapon(this FullEquipType type)
        => type switch
        {
            FullEquipType.Fists           => true,
            FullEquipType.Sword           => true,
            FullEquipType.Axe             => true,
            FullEquipType.Bow             => true,
            FullEquipType.Lance           => true,
            FullEquipType.StaffBlm        => true,
            FullEquipType.StaffWhm        => true,
            FullEquipType.Wand            => true,
            FullEquipType.Book            => true,
            FullEquipType.Daggers         => true,
            FullEquipType.Broadsword      => true,
            FullEquipType.Gun             => true,
            FullEquipType.Orrery          => true,
            FullEquipType.Katana          => true,
            FullEquipType.Rapier          => true,
            FullEquipType.Cane            => true,
            FullEquipType.Scepter         => true,
            FullEquipType.Gunblade        => true,
            FullEquipType.Glaives         => true,
            FullEquipType.Scythe          => true,
            FullEquipType.Nouliths        => true,
            FullEquipType.Shield          => true,
            FullEquipType.Brush           => true,
            FullEquipType.Twinfangs       => true,
            FullEquipType.Handaxe         => true,
            FullEquipType.UnknownMainhand => true,
            _                             => false,
        };

    /// <summary> Return whether two weapon types are compatible. </summary>
    public static bool IsCompatible(this FullEquipType type, FullEquipType other)
        => type switch
        {
            FullEquipType.Scepter or FullEquipType.Wand or FullEquipType.StaffWhm or FullEquipType.StaffBlm => other is FullEquipType.Scepter
                or FullEquipType.Wand or FullEquipType.StaffBlm or FullEquipType.StaffWhm,
            FullEquipType.Sword or FullEquipType.Handaxe => other is FullEquipType.Sword or FullEquipType.Handaxe,
            _                                            => type == other,
        };

    /// <summary> Return whether an offhand weapon type is compatible with the current mainhand state. </summary>
    /// <param name="type"> The new offhand item's type. </param>
    /// <param name="gameMainhand"> The game's mainhand type. </param>
    /// <param name="actualMainhand"> The current actual mainhand type. </param>
    /// <param name="gameOffhand"> The game's offhand type. </param>
    /// <returns> True whether the new item can be applied to the offhand. </returns>
    public static bool IsOffhandCompatible(this FullEquipType type, FullEquipType gameMainhand, FullEquipType actualMainhand,
        FullEquipType gameOffhand)
    {
        // If the mainhand type is the default one, just check against the default offhand.
        if (gameMainhand == actualMainhand)
            return type.IsCompatible(gameOffhand);

        // Otherwise check against the expected offhand.
        var validOffhand = actualMainhand.ValidOffhand();
        return type.IsCompatible(validOffhand);
    }

    /// <summary> Get all compatible types for a weapon type, excluding itself. </summary>
    public static IReadOnlyList<FullEquipType> CompatibleTypes(this FullEquipType type)
        => type switch
        {
            FullEquipType.Scepter  => [FullEquipType.Wand, FullEquipType.StaffWhm, FullEquipType.StaffBlm],
            FullEquipType.Wand     => [FullEquipType.Scepter, FullEquipType.StaffWhm, FullEquipType.StaffBlm],
            FullEquipType.StaffWhm => [FullEquipType.Scepter, FullEquipType.Wand, FullEquipType.StaffBlm],
            FullEquipType.StaffBlm => [FullEquipType.Scepter, FullEquipType.Wand, FullEquipType.StaffWhm],
            FullEquipType.Sword    => [FullEquipType.Handaxe],
            FullEquipType.Handaxe  => [FullEquipType.Sword],
            _                      => [],
        };

    /// <summary> Return whether a FullEquipType is a primary or secondary tool. </summary>
    public static bool IsTool(this FullEquipType type)
        => type switch
        {
            FullEquipType.Saw             => true,
            FullEquipType.CrossPeinHammer => true,
            FullEquipType.RaisingHammer   => true,
            FullEquipType.LapidaryHammer  => true,
            FullEquipType.Knife           => true,
            FullEquipType.Needle          => true,
            FullEquipType.Alembic         => true,
            FullEquipType.Frypan          => true,
            FullEquipType.Pickaxe         => true,
            FullEquipType.Hatchet         => true,
            FullEquipType.FishingRod      => true,
            FullEquipType.ClawHammer      => true,
            FullEquipType.File            => true,
            FullEquipType.Pliers          => true,
            FullEquipType.GrindingWheel   => true,
            FullEquipType.Awl             => true,
            FullEquipType.SpinningWheel   => true,
            FullEquipType.Mortar          => true,
            FullEquipType.CulinaryKnife   => true,
            FullEquipType.Sledgehammer    => true,
            FullEquipType.GardenScythe    => true,
            FullEquipType.Gig             => true,
            _                             => false,
        };

    /// <summary> Return whether a FullEquipType is a piece of primary equipment. </summary>
    public static bool IsEquipment(this FullEquipType type)
        => type switch
        {
            FullEquipType.Head  => true,
            FullEquipType.Body  => true,
            FullEquipType.Hands => true,
            FullEquipType.Legs  => true,
            FullEquipType.Feet  => true,
            _                   => false,
        };

    /// <summary> Return whether a FullEquipType is a piece of secondary equipment. </summary>
    public static bool IsAccessory(this FullEquipType type)
        => type switch
        {
            FullEquipType.Ears   => true,
            FullEquipType.Neck   => true,
            FullEquipType.Wrists => true,
            FullEquipType.Finger => true,
            _                    => false,
        };

    /// <summary> Return whether a FullEquipType is a bonus slot. </summary>
    public static bool IsBonus(this FullEquipType type)
        => type switch
        {
            FullEquipType.Glasses => true,
            _                     => false,
        };

    public static BonusItemFlag ToBonus(this FullEquipType type)
        => type switch
        {
            FullEquipType.Head    => EquipSlot.Head.ToBonusSlot(),
            FullEquipType.Glasses => BonusItemFlag.Glasses,
            _                     => BonusItemFlag.Unknown,
        };

    /// <summary> Return the actual equipment slot a FullEquipType will be equipped to. </summary>
    public static EquipSlot ToSlot(this FullEquipType type)
        => type switch
        {
            FullEquipType.Head            => EquipSlot.Head,
            FullEquipType.Body            => EquipSlot.Body,
            FullEquipType.Hands           => EquipSlot.Hands,
            FullEquipType.Legs            => EquipSlot.Legs,
            FullEquipType.Feet            => EquipSlot.Feet,
            FullEquipType.Ears            => EquipSlot.Ears,
            FullEquipType.Neck            => EquipSlot.Neck,
            FullEquipType.Wrists          => EquipSlot.Wrists,
            FullEquipType.Finger          => EquipSlot.RFinger,
            FullEquipType.Fists           => EquipSlot.MainHand,
            FullEquipType.FistsOff        => EquipSlot.OffHand,
            FullEquipType.Sword           => EquipSlot.MainHand,
            FullEquipType.Axe             => EquipSlot.MainHand,
            FullEquipType.Bow             => EquipSlot.MainHand,
            FullEquipType.BowOff          => EquipSlot.OffHand,
            FullEquipType.Lance           => EquipSlot.MainHand,
            FullEquipType.StaffBlm        => EquipSlot.MainHand,
            FullEquipType.StaffWhm        => EquipSlot.MainHand,
            FullEquipType.Wand            => EquipSlot.MainHand,
            FullEquipType.Book            => EquipSlot.MainHand,
            FullEquipType.Daggers         => EquipSlot.MainHand,
            FullEquipType.DaggersOff      => EquipSlot.OffHand,
            FullEquipType.Broadsword      => EquipSlot.MainHand,
            FullEquipType.Gun             => EquipSlot.MainHand,
            FullEquipType.GunOff          => EquipSlot.OffHand,
            FullEquipType.Orrery          => EquipSlot.MainHand,
            FullEquipType.OrreryOff       => EquipSlot.OffHand,
            FullEquipType.Katana          => EquipSlot.MainHand,
            FullEquipType.KatanaOff       => EquipSlot.OffHand,
            FullEquipType.Rapier          => EquipSlot.MainHand,
            FullEquipType.RapierOff       => EquipSlot.OffHand,
            FullEquipType.Cane            => EquipSlot.MainHand,
            FullEquipType.Scepter         => EquipSlot.MainHand,
            FullEquipType.Gunblade        => EquipSlot.MainHand,
            FullEquipType.Glaives         => EquipSlot.MainHand,
            FullEquipType.GlaivesOff      => EquipSlot.OffHand,
            FullEquipType.Scythe          => EquipSlot.MainHand,
            FullEquipType.Nouliths        => EquipSlot.MainHand,
            FullEquipType.Shield          => EquipSlot.OffHand,
            FullEquipType.Saw             => EquipSlot.MainHand,
            FullEquipType.CrossPeinHammer => EquipSlot.MainHand,
            FullEquipType.RaisingHammer   => EquipSlot.MainHand,
            FullEquipType.LapidaryHammer  => EquipSlot.MainHand,
            FullEquipType.Knife           => EquipSlot.MainHand,
            FullEquipType.Needle          => EquipSlot.MainHand,
            FullEquipType.Alembic         => EquipSlot.MainHand,
            FullEquipType.Frypan          => EquipSlot.MainHand,
            FullEquipType.Pickaxe         => EquipSlot.MainHand,
            FullEquipType.Hatchet         => EquipSlot.MainHand,
            FullEquipType.FishingRod      => EquipSlot.MainHand,
            FullEquipType.ClawHammer      => EquipSlot.OffHand,
            FullEquipType.File            => EquipSlot.OffHand,
            FullEquipType.Pliers          => EquipSlot.OffHand,
            FullEquipType.GrindingWheel   => EquipSlot.OffHand,
            FullEquipType.Awl             => EquipSlot.OffHand,
            FullEquipType.SpinningWheel   => EquipSlot.OffHand,
            FullEquipType.Mortar          => EquipSlot.OffHand,
            FullEquipType.CulinaryKnife   => EquipSlot.OffHand,
            FullEquipType.Sledgehammer    => EquipSlot.OffHand,
            FullEquipType.GardenScythe    => EquipSlot.OffHand,
            FullEquipType.Gig             => EquipSlot.OffHand,
            FullEquipType.Twinfangs       => EquipSlot.MainHand,
            FullEquipType.TwinfangsOff    => EquipSlot.OffHand,
            FullEquipType.Brush           => EquipSlot.MainHand,
            FullEquipType.Palette         => EquipSlot.OffHand,
            FullEquipType.Handaxe         => EquipSlot.MainHand,
            FullEquipType.Glasses         => BonusItemFlag.Glasses.ToEquipSlot(),
            FullEquipType.UnknownMainhand => EquipSlot.MainHand,
            FullEquipType.UnknownOffhand  => EquipSlot.OffHand,
            _                             => EquipSlot.Unknown,
        };

    /// <summary> Convert an EquipSlot and a weapon category to a FullEquipType. </summary>
    /// <param name="slot"> The slot to convert. </param>
    /// <param name="category"> The weapon category to use if the slot is mainhand or offhand. </param>
    /// <param name="mainhand"> Whether to use the mainhand or offhand type of weapon. </param>
    public static FullEquipType ToEquipType(this EquipSlot slot, WeaponCategory category = WeaponCategory.Unknown, bool mainhand = true)
        => slot switch
        {
            EquipSlot.Head              => FullEquipType.Head,
            EquipSlot.Body              => FullEquipType.Body,
            EquipSlot.Hands             => FullEquipType.Hands,
            EquipSlot.Legs              => FullEquipType.Legs,
            EquipSlot.Feet              => FullEquipType.Feet,
            EquipSlot.Ears              => FullEquipType.Ears,
            EquipSlot.Neck              => FullEquipType.Neck,
            EquipSlot.Wrists            => FullEquipType.Wrists,
            EquipSlot.RFinger           => FullEquipType.Finger,
            EquipSlot.LFinger           => FullEquipType.Finger,
            EquipSlot.HeadBody          => FullEquipType.Body,
            EquipSlot.BodyHandsLegsFeet => FullEquipType.Body,
            EquipSlot.LegsFeet          => FullEquipType.Legs,
            EquipSlot.FullBody          => FullEquipType.Body,
            EquipSlot.BodyHands         => FullEquipType.Body,
            EquipSlot.BodyLegsFeet      => FullEquipType.Body,
            EquipSlot.ChestHands        => FullEquipType.Body,
            EquipSlot.ChestLegs         => FullEquipType.Body,
            EquipSlot.MainHand          => category.ToEquipType(mainhand),
            EquipSlot.OffHand           => category.ToEquipType(mainhand),
            EquipSlot.BothHand          => category.ToEquipType(mainhand),
            _                           => FullEquipType.Unknown,
        };

    public static FullEquipType ToEquipType(this BonusItemFlag bonusSlot)
        => bonusSlot switch
        {
            BonusItemFlag.Glasses => FullEquipType.Glasses,
            BonusItemFlag.UnkSlot => FullEquipType.Unknown,
            _                     => FullEquipType.Unknown,
        };

    /// <summary> Convert a weapon category to a FullEquipType. </summary>
    /// <param name="category"> The category to convert. </param>
    /// <param name="mainhand"> Whether to use the mainhand or offhand type of weapon. </param>
    public static FullEquipType ToEquipType(this WeaponCategory category, bool mainhand = true)
        => category switch
        {
            WeaponCategory.Pugilist when mainhand    => FullEquipType.Fists,
            WeaponCategory.Pugilist                  => FullEquipType.FistsOff,
            WeaponCategory.Gladiator                 => FullEquipType.Sword,
            WeaponCategory.Marauder                  => FullEquipType.Axe,
            WeaponCategory.Archer when mainhand      => FullEquipType.Bow,
            WeaponCategory.Archer                    => FullEquipType.BowOff,
            WeaponCategory.Lancer                    => FullEquipType.Lance,
            WeaponCategory.Thaumaturge1              => FullEquipType.Scepter,
            WeaponCategory.Thaumaturge2              => FullEquipType.StaffBlm,
            WeaponCategory.Conjurer1                 => FullEquipType.Wand,
            WeaponCategory.Conjurer2                 => FullEquipType.StaffWhm,
            WeaponCategory.Arcanist                  => FullEquipType.Book,
            WeaponCategory.Shield                    => FullEquipType.Shield,
            WeaponCategory.CarpenterMain             => FullEquipType.Saw,
            WeaponCategory.CarpenterOff              => FullEquipType.ClawHammer,
            WeaponCategory.BlacksmithMain            => FullEquipType.CrossPeinHammer,
            WeaponCategory.BlacksmithOff             => FullEquipType.File,
            WeaponCategory.ArmorerMain               => FullEquipType.RaisingHammer,
            WeaponCategory.ArmorerOff                => FullEquipType.Pliers,
            WeaponCategory.GoldsmithMain             => FullEquipType.LapidaryHammer,
            WeaponCategory.GoldsmithOff              => FullEquipType.GrindingWheel,
            WeaponCategory.LeatherworkerMain         => FullEquipType.Knife,
            WeaponCategory.LeatherworkerOff          => FullEquipType.Awl,
            WeaponCategory.WeaverMain                => FullEquipType.Needle,
            WeaponCategory.WeaverOff                 => FullEquipType.SpinningWheel,
            WeaponCategory.AlchemistMain             => FullEquipType.Alembic,
            WeaponCategory.AlchemistOff              => FullEquipType.Mortar,
            WeaponCategory.CulinarianMain            => FullEquipType.Frypan,
            WeaponCategory.CulinarianOff             => FullEquipType.CulinaryKnife,
            WeaponCategory.MinerMain                 => FullEquipType.Pickaxe,
            WeaponCategory.MinerOff                  => FullEquipType.Sledgehammer,
            WeaponCategory.BotanistMain              => FullEquipType.Hatchet,
            WeaponCategory.BotanistOff               => FullEquipType.GardenScythe,
            WeaponCategory.FisherMain                => FullEquipType.FishingRod,
            WeaponCategory.FisherOff                 => FullEquipType.Gig,
            WeaponCategory.Rogue when mainhand       => FullEquipType.Daggers,
            WeaponCategory.Rogue                     => FullEquipType.DaggersOff,
            WeaponCategory.DarkKnight                => FullEquipType.Broadsword,
            WeaponCategory.Machinist when mainhand   => FullEquipType.Gun,
            WeaponCategory.Machinist                 => FullEquipType.GunOff,
            WeaponCategory.Astrologian when mainhand => FullEquipType.Orrery,
            WeaponCategory.Astrologian               => FullEquipType.OrreryOff,
            WeaponCategory.Samurai when mainhand     => FullEquipType.Katana,
            WeaponCategory.Samurai                   => FullEquipType.KatanaOff,
            WeaponCategory.RedMage when mainhand     => FullEquipType.Rapier,
            WeaponCategory.RedMage                   => FullEquipType.RapierOff,
            WeaponCategory.Scholar                   => FullEquipType.Book,
            WeaponCategory.BlueMage                  => FullEquipType.Cane,
            WeaponCategory.Gunbreaker                => FullEquipType.Gunblade,
            WeaponCategory.Dancer when mainhand      => FullEquipType.Glaives,
            WeaponCategory.Dancer                    => FullEquipType.GlaivesOff,
            WeaponCategory.Reaper                    => FullEquipType.Scythe,
            WeaponCategory.Sage                      => FullEquipType.Nouliths,
            WeaponCategory.Viper when mainhand       => FullEquipType.Twinfangs,
            WeaponCategory.Viper                     => FullEquipType.TwinfangsOff,
            WeaponCategory.Pictomancer when mainhand => FullEquipType.Brush,
            WeaponCategory.Pictomancer               => FullEquipType.Palette,
            WeaponCategory.Beastmaster               => FullEquipType.Handaxe,
            _ when mainhand                          => FullEquipType.UnknownMainhand,
            _                                        => FullEquipType.UnknownOffhand,
        };

    /// <summary> Obtain the correct offhand FullEquipType for a Mainhand FullEquipType, excluding tools. </summary>
    public static FullEquipType ValidOffhand(this FullEquipType type)
        => type switch
        {
            FullEquipType.Fists     => FullEquipType.FistsOff,
            FullEquipType.Sword     => FullEquipType.Shield,
            FullEquipType.Wand      => FullEquipType.Shield,
            FullEquipType.Scepter   => FullEquipType.Shield,
            FullEquipType.Handaxe   => FullEquipType.Shield,
            FullEquipType.Daggers   => FullEquipType.DaggersOff,
            FullEquipType.Gun       => FullEquipType.GunOff,
            FullEquipType.Orrery    => FullEquipType.OrreryOff,
            FullEquipType.Rapier    => FullEquipType.RapierOff,
            FullEquipType.Glaives   => FullEquipType.GlaivesOff,
            FullEquipType.Bow       => FullEquipType.BowOff,
            FullEquipType.Katana    => FullEquipType.KatanaOff,
            FullEquipType.Twinfangs => FullEquipType.TwinfangsOff,
            FullEquipType.Brush     => FullEquipType.Palette,
            _                       => FullEquipType.Unknown,
        };

    /// <summary> Obtain the correct offhand FullEquipType for a Mainhand FullEquipType, including tools. </summary>
    public static FullEquipType Offhand(this FullEquipType type)
        => type switch
        {
            FullEquipType.Fists           => FullEquipType.FistsOff,
            FullEquipType.Sword           => FullEquipType.Shield,
            FullEquipType.Wand            => FullEquipType.Shield,
            FullEquipType.Scepter         => FullEquipType.Shield,
            FullEquipType.Handaxe         => FullEquipType.Shield,
            FullEquipType.Daggers         => FullEquipType.DaggersOff,
            FullEquipType.Gun             => FullEquipType.GunOff,
            FullEquipType.Orrery          => FullEquipType.OrreryOff,
            FullEquipType.Rapier          => FullEquipType.RapierOff,
            FullEquipType.Glaives         => FullEquipType.GlaivesOff,
            FullEquipType.Bow             => FullEquipType.BowOff,
            FullEquipType.Katana          => FullEquipType.KatanaOff,
            FullEquipType.Saw             => FullEquipType.ClawHammer,
            FullEquipType.CrossPeinHammer => FullEquipType.File,
            FullEquipType.RaisingHammer   => FullEquipType.Pliers,
            FullEquipType.LapidaryHammer  => FullEquipType.GrindingWheel,
            FullEquipType.Knife           => FullEquipType.Awl,
            FullEquipType.Needle          => FullEquipType.SpinningWheel,
            FullEquipType.Alembic         => FullEquipType.Mortar,
            FullEquipType.Frypan          => FullEquipType.CulinaryKnife,
            FullEquipType.Pickaxe         => FullEquipType.Sledgehammer,
            FullEquipType.Hatchet         => FullEquipType.GardenScythe,
            FullEquipType.FishingRod      => FullEquipType.Gig,
            FullEquipType.Twinfangs       => FullEquipType.TwinfangsOff,
            FullEquipType.Brush           => FullEquipType.Palette,
            _                             => FullEquipType.Unknown,
        };

    /// <summary> Whether an Offhand FullEquipType allows putting Nothing into the Offhand slot. </summary>
    public static bool AllowsNothing(this FullEquipType type)
        => type switch
        {
            FullEquipType.Head      => true,
            FullEquipType.Body      => true,
            FullEquipType.Hands     => true,
            FullEquipType.Legs      => true,
            FullEquipType.Feet      => true,
            FullEquipType.Ears      => true,
            FullEquipType.Neck      => true,
            FullEquipType.Wrists    => true,
            FullEquipType.Finger    => true,
            FullEquipType.BowOff    => true,
            FullEquipType.GunOff    => true,
            FullEquipType.OrreryOff => true,
            FullEquipType.KatanaOff => true,
            FullEquipType.RapierOff => true,
            FullEquipType.Shield    => true,
            FullEquipType.Palette   => true,
            FullEquipType.Glasses   => true,
            _                       => false,
        };

    /// <summary> The human-readable suffix for inferred offhand types. </summary>
    internal static string OffhandTypeSuffix(this FullEquipType type)
        => type switch
        {
            FullEquipType.FistsOff     => "（副手）",
            FullEquipType.DaggersOff   => "（副手）",
            FullEquipType.GunOff       => "（以太变换器）",
            FullEquipType.OrreryOff    => "（卡套）",
            FullEquipType.RapierOff    => "（触媒）",
            FullEquipType.GlaivesOff   => "（副手）",
            FullEquipType.BowOff       => "（箭袋）",
            FullEquipType.KatanaOff    => "（刀鞘）",
            FullEquipType.TwinfangsOff => "（副手）",
            FullEquipType.Palette      => "（调色盘）",
            _                          => string.Empty,
        };

    /// <summary> Whether a FullEquipType is an inferred offhand type. </summary>
    public static bool IsOffhandType(this FullEquipType type)
        => type.OffhandTypeSuffix().Length > 0;

    /// <summary> A list of all weapon types. </summary>
    public static readonly IReadOnlyList<FullEquipType> WeaponTypes
        = FullEquipType.Values.Where(v => v.IsWeapon()).Except([FullEquipType.UnknownMainhand])
            .ToArray();

    /// <summary> A list of all tool types, including offhands. </summary>
    public static readonly IReadOnlyList<FullEquipType> ToolTypes
        = FullEquipType.Values.Where(v => v.IsTool()).ToArray();

    /// <summary> A list of all equipment types. </summary>
    public static readonly IReadOnlyList<FullEquipType> EquipmentTypes
        = FullEquipType.Values.Where(v => v.IsEquipment()).ToArray();

    /// <summary> A list of all accessory types. </summary>
    public static readonly IReadOnlyList<FullEquipType> AccessoryTypes
        = FullEquipType.Values.Where(v => v.IsAccessory()).ToArray();

    /// <summary> A list of all inferred offhand types. </summary>
    public static readonly IReadOnlyList<FullEquipType> OffhandTypes
        = FullEquipType.Values.Where(IsOffhandType).ToArray();

    /// <summary> A list of all inferred offhand types. </summary>
    public static readonly IReadOnlyList<FullEquipType> BonusTypes
        = FullEquipType.Values.Where(IsBonus).ToArray();
}
