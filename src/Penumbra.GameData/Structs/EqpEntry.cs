using System.Collections.Frozen;
using Penumbra.GameData.Enums;

namespace Penumbra.GameData.Structs;

/// <summary> Equipment Parameter entries contain information on how a piece of equipment influences other parts of the character for a given primary ID. </summary>
[Flags]
public enum EqpEntry : ulong
{
    BodyEnabled              = 0x00_01ul,
    BodyHideWaist            = 0x00_02ul,
    BodyHideThighs           = 0x00_04ul,
    BodyHideGlovesS          = 0x00_08ul,
    BodyHideGloveCuffs       = 0x00_10ul,
    BodyHideGlovesM          = 0x00_20ul,
    BodyHideGlovesL          = 0x00_40ul,
    BodyHideGorget           = 0x00_80ul,
    BodyShowLeg              = 0x01_00ul,
    BodyShowHand             = 0x02_00ul,
    BodyShowHead             = 0x04_00ul,
    BodyShowNecklace         = 0x08_00ul,
    BodyShowBracelet         = 0x10_00ul,
    BodyShowTail             = 0x20_00ul,
    BodyDisableBreastPhysics = 0x40_00ul,
    BodyUsesEvpTable         = 0x80_00ul,
    BodyMask                 = 0xFF_FFul,

    LegsEnabled      = 0x01ul << 16,
    LegsHideKneePads = 0x02ul << 16,
    LegsHideBootsS   = 0x04ul << 16,
    LegsHideBootsM   = 0x08ul << 16,
    _20              = 0x10ul << 16,
    LegsShowFoot     = 0x20ul << 16,
    LegsShowTail     = 0x40ul << 16,
    _23              = 0x80ul << 16,
    LegsMask         = 0xFFul << 16,

    HandsEnabled     = 0x01ul << 24,
    HandsHideElbow   = 0x02ul << 24,
    HandsHideForearm = 0x04ul << 24,
    _27              = 0x08ul << 24,
    HandShowBracelet = 0x10ul << 24,
    HandShowRingL    = 0x20ul << 24,
    HandShowRingR    = 0x40ul << 24,
    _31              = 0x80ul << 24,
    HandsMask        = 0xFFul << 24,

    FeetEnabled   = 0x01ul << 32,
    FeetHideKnee  = 0x02ul << 32,
    FeetHideCalf  = 0x04ul << 32,
    FeetHideAnkle = 0x08ul << 32,
    _36           = 0x10ul << 32,
    _37           = 0x20ul << 32,
    _38           = 0x40ul << 32,
    _39           = 0x80ul << 32,
    FeetMask      = 0xFFul << 32,

    HeadEnabled                    = 0x00_00_01ul << 40,
    HeadHideScalp                  = 0x00_00_02ul << 40,
    HeadHideHair                   = 0x00_00_04ul << 40,
    HeadShowHairOverride           = 0x00_00_08ul << 40,
    HeadHideNeck                   = 0x00_00_10ul << 40,
    HeadShowNecklace               = 0x00_00_20ul << 40,
    HeadShowEarringsHyurRoe        = 0x00_00_40ul << 40,
    HeadShowEarringsLalaElezen     = 0x00_00_80ul << 40,
    HeadShowEarringsMiqoHrothViera = 0x00_01_00ul << 40,
    HeadShowEarringsAura           = 0x00_02_00ul << 40,
    HeadShowEarHuman               = 0x00_04_00ul << 40,
    HeadShowEarMiqote              = 0x00_08_00ul << 40,
    HeadShowEarAuRa                = 0x00_10_00ul << 40,
    HeadShowEarViera               = 0x00_20_00ul << 40,
    HeadDisableBangsPhysics         = 0x00_40_00ul << 40,
    HeadDisableHairPhysics         = 0x00_80_00ul << 40,
    HeadShowHrothgarHat            = 0x01_00_00ul << 40,
    HeadShowVieraHat               = 0x02_00_00ul << 40,
    HeadUsesEvpTable               = 0x04_00_00ul << 40,
    _59                            = 0x08_00_00ul << 40,
    _60                            = 0x10_00_00ul << 40,
    _61                            = 0x20_00_00ul << 40,
    _62                            = 0x40_00_00ul << 40,
    _63                            = 0x80_00_00ul << 40,
    HeadMask                       = 0xFF_FF_FFul << 40,
}

public static class Eqp
{
    /// <summary> The default EqpEntry for otherwise unspecified gear sets. </summary>
    /// <remarks> cf. Client::Graphics::Scene::CharacterUtility.GetSlotEqpFlags </remarks>
    public const EqpEntry DefaultEntry = (EqpEntry)0x3fe00070603f00;

    /// <summary> Get the number of bytes used per slot, and the offset of those bytes in the flag. </summary>
    public static (int, int) BytesAndOffset(EquipSlot slot)
        => slot switch
        {
            EquipSlot.Body  => (2, 0),
            EquipSlot.Legs  => (1, 2),
            EquipSlot.Hands => (1, 3),
            EquipSlot.Feet  => (1, 4),
            EquipSlot.Head  => (3, 5),
            _               => (0, 0),
        };

    /// <summary> Get the number of bytes used per slot, and the offset of those bytes in the flag. </summary>
    public static (int, EqpEntry) OffsetAndMask(EquipSlot slot)
        => slot switch
        {
            EquipSlot.Body  => (0 * 8, EqpEntry.BodyMask),
            EquipSlot.Legs  => (2 * 8, EqpEntry.LegsMask),
            EquipSlot.Hands => (3 * 8, EqpEntry.HandsMask),
            EquipSlot.Feet  => (4 * 8, EqpEntry.FeetMask),
            EquipSlot.Head  => (5 * 8, EqpEntry.HeadMask),
            _               => (0, 0),
        };

    /// <summary> Read an array of bytes and convert them to an EqpEntry given a slot. </summary>
    public static EqpEntry FromSlotAndBytes(EquipSlot slot, byte[] value)
    {
        EqpEntry ret = 0;
        var (bytes, offset) = BytesAndOffset(slot);
        if (bytes != value.Length)
            throw new ArgumentException();

        for (var i = 0; i < bytes; ++i)
            ret |= (EqpEntry)((ulong)value[i] << ((offset + i) * 8));

        return ret;
    }

    /// <summary> Get the mask for a given slot. </summary>
    public static EqpEntry Mask(EquipSlot slot)
        => OffsetAndMask(slot).Item2;

    /// <summary> Convert an entry flag to the slot it affects. </summary>
    public static EquipSlot ToEquipSlot(this EqpEntry entry)
    {
        return entry switch
        {
            EqpEntry.BodyEnabled              => EquipSlot.Body,
            EqpEntry.BodyHideWaist            => EquipSlot.Body,
            EqpEntry.BodyHideThighs           => EquipSlot.Body,
            EqpEntry.BodyHideGlovesS          => EquipSlot.Body,
            EqpEntry.BodyHideGloveCuffs       => EquipSlot.Body,
            EqpEntry.BodyHideGlovesM          => EquipSlot.Body,
            EqpEntry.BodyHideGlovesL          => EquipSlot.Body,
            EqpEntry.BodyHideGorget           => EquipSlot.Body,
            EqpEntry.BodyShowLeg              => EquipSlot.Body,
            EqpEntry.BodyShowHand             => EquipSlot.Body,
            EqpEntry.BodyShowHead             => EquipSlot.Body,
            EqpEntry.BodyShowNecklace         => EquipSlot.Body,
            EqpEntry.BodyShowBracelet         => EquipSlot.Body,
            EqpEntry.BodyShowTail             => EquipSlot.Body,
            EqpEntry.BodyDisableBreastPhysics => EquipSlot.Body,
            EqpEntry.BodyUsesEvpTable         => EquipSlot.Body,

            EqpEntry.LegsEnabled      => EquipSlot.Legs,
            EqpEntry.LegsHideKneePads => EquipSlot.Legs,
            EqpEntry.LegsHideBootsS   => EquipSlot.Legs,
            EqpEntry.LegsHideBootsM   => EquipSlot.Legs,
            EqpEntry._20              => EquipSlot.Legs,
            EqpEntry.LegsShowFoot     => EquipSlot.Legs,
            EqpEntry.LegsShowTail     => EquipSlot.Legs,
            EqpEntry._23              => EquipSlot.Legs,

            EqpEntry.HandsEnabled     => EquipSlot.Hands,
            EqpEntry.HandsHideElbow   => EquipSlot.Hands,
            EqpEntry.HandsHideForearm => EquipSlot.Hands,
            EqpEntry._27              => EquipSlot.Hands,
            EqpEntry.HandShowBracelet => EquipSlot.Hands,
            EqpEntry.HandShowRingL    => EquipSlot.Hands,
            EqpEntry.HandShowRingR    => EquipSlot.Hands,
            EqpEntry._31              => EquipSlot.Hands,

            EqpEntry.FeetEnabled   => EquipSlot.Feet,
            EqpEntry.FeetHideKnee  => EquipSlot.Feet,
            EqpEntry.FeetHideCalf  => EquipSlot.Feet,
            EqpEntry.FeetHideAnkle => EquipSlot.Feet,
            EqpEntry._36           => EquipSlot.Feet,
            EqpEntry._37           => EquipSlot.Feet,
            EqpEntry._38           => EquipSlot.Feet,
            EqpEntry._39           => EquipSlot.Feet,

            EqpEntry.HeadEnabled                    => EquipSlot.Head,
            EqpEntry.HeadHideScalp                  => EquipSlot.Head,
            EqpEntry.HeadHideHair                   => EquipSlot.Head,
            EqpEntry.HeadShowHairOverride           => EquipSlot.Head,
            EqpEntry.HeadHideNeck                   => EquipSlot.Head,
            EqpEntry.HeadShowNecklace               => EquipSlot.Head,
            EqpEntry.HeadShowEarringsHyurRoe        => EquipSlot.Head,
            EqpEntry.HeadShowEarringsLalaElezen     => EquipSlot.Head,
            EqpEntry.HeadShowEarringsMiqoHrothViera => EquipSlot.Head,
            EqpEntry.HeadShowEarringsAura           => EquipSlot.Head,
            EqpEntry.HeadShowEarHuman               => EquipSlot.Head,
            EqpEntry.HeadShowEarMiqote              => EquipSlot.Head,
            EqpEntry.HeadShowEarAuRa                => EquipSlot.Head,
            EqpEntry.HeadShowEarViera               => EquipSlot.Head,
            EqpEntry.HeadDisableBangsPhysics         => EquipSlot.Head,
            EqpEntry.HeadDisableHairPhysics         => EquipSlot.Head,
            EqpEntry.HeadShowHrothgarHat            => EquipSlot.Head,
            EqpEntry.HeadShowVieraHat               => EquipSlot.Head,
            EqpEntry.HeadUsesEvpTable               => EquipSlot.Head,

            // currently unused
            EqpEntry._59 => EquipSlot.Unknown,
            EqpEntry._60 => EquipSlot.Unknown,
            EqpEntry._61 => EquipSlot.Unknown,
            EqpEntry._62 => EquipSlot.Unknown,
            EqpEntry._63 => EquipSlot.Unknown,

            _ => EquipSlot.Unknown,
        };
    }

    /// <summary> Get human-readable names for the known flags. </summary>
    public static string ToLocalName(this EqpEntry entry)
        => entry switch
        {
            EqpEntry.BodyEnabled              => "启用",
            EqpEntry.BodyHideWaist            => "隐藏腰部",
            EqpEntry.BodyHideThighs           => "隐藏护腿",
            EqpEntry.BodyHideGlovesS          => "隐藏短手套",
            EqpEntry.BodyHideGloveCuffs       => "隐藏手套袖口",
            EqpEntry.BodyHideGlovesM          => "隐藏中手套",
            EqpEntry.BodyHideGlovesL          => "隐藏长手套",
            EqpEntry.BodyHideGorget           => "隐藏护喉甲",
            EqpEntry.BodyShowLeg              => "显示腿",
            EqpEntry.BodyShowHand             => "显示手",
            EqpEntry.BodyShowHead             => "显示头",
            EqpEntry.BodyShowNecklace         => "显示项链",
            EqpEntry.BodyShowBracelet         => "显示手镯",
            EqpEntry.BodyShowTail             => "显示尾巴",
            EqpEntry.BodyDisableBreastPhysics => "禁用乳房物理",
            EqpEntry.BodyUsesEvpTable         => "使用EVP表",

            EqpEntry.LegsEnabled      => "启用",
            EqpEntry.LegsHideKneePads => "隐藏护膝",
            EqpEntry.LegsHideBootsS   => "隐藏短靴",
            EqpEntry.LegsHideBootsM   => "隐藏半靴",
            EqpEntry._20              => "未知 Bit20",
            EqpEntry.LegsShowFoot     => "显示脚",
            EqpEntry.LegsShowTail     => "显示尾巴",
            EqpEntry._23              => "未知 Bit23",

            EqpEntry.HandsEnabled     => "启用",
            EqpEntry.HandsHideElbow   => "隐藏肘部",
            EqpEntry.HandsHideForearm => "隐藏前臂",
            EqpEntry._27              => "未知 Bit27",
            EqpEntry.HandShowBracelet => "显示手镯",
            EqpEntry.HandShowRingL    => "显示左手戒指",
            EqpEntry.HandShowRingR    => "显示右手戒指",
            EqpEntry._31              => "未知 Bit31",

            EqpEntry.FeetEnabled   => "启用",
            EqpEntry.FeetHideKnee  => "隐藏膝盖",
            EqpEntry.FeetHideCalf  => "隐藏小腿",
            EqpEntry.FeetHideAnkle => "隐藏脚踝",
            EqpEntry._36           => "未知 Bit36",
            EqpEntry._37           => "未知 Bit37",
            EqpEntry._38           => "未知 Bit38",
            EqpEntry._39           => "未知 Bit39",

            EqpEntry.HeadEnabled                    => "启用",
            EqpEntry.HeadHideScalp                  => "隐藏头皮",
            EqpEntry.HeadHideHair                   => "隐藏头发",
            EqpEntry.HeadShowHairOverride           => "显示头发覆盖",
            EqpEntry.HeadHideNeck                   => "隐藏颈部",
            EqpEntry.HeadShowNecklace               => "显示颈饰",
            EqpEntry.HeadShowEarringsHyurRoe        => "显示耳饰（人族，鲁加族）",
            EqpEntry.HeadShowEarringsLalaElezen     => "显示耳饰（精灵族，拉拉菲尔族）",
            EqpEntry.HeadShowEarringsMiqoHrothViera => "显示耳饰（猫魅族，硌狮族，维埃拉族）",
            EqpEntry.HeadShowEarringsAura           => "显示耳饰（敖龙族）",
            EqpEntry.HeadShowEarHuman               => "显示耳朵（人类）",
            EqpEntry.HeadShowEarMiqote              => "显示耳朵（猫魅族）",
            EqpEntry.HeadShowEarAuRa                => "显示耳朵（敖龙族）",
            EqpEntry.HeadShowEarViera               => "显示耳朵（维埃拉族）",
            EqpEntry.HeadDisableBangsPhysics        => "禁用头发物理（刘海）",
            EqpEntry.HeadDisableHairPhysics         => "禁用头发物理（两侧、后部）",
            EqpEntry.HeadShowHrothgarHat            => "显示硌狮族帽子",
            EqpEntry.HeadShowVieraHat               => "显示维埃拉族帽子",
            EqpEntry.HeadUsesEvpTable               => "使用 EVP 表",

            EqpEntry._59 => "未知 Bit59",
            EqpEntry._60 => "未知 Bit60",
            EqpEntry._61 => "未知 Bit61",
            EqpEntry._62 => "未知 Bit62",
            EqpEntry._63 => "未知 Bit63",

            _ => throw new InvalidEnumArgumentException(),
        };

    /// <summary> Get all flags for a given slot. </summary>
    private static IReadOnlyList<EqpEntry> GetEntriesForSlot(EquipSlot slot)
    {
        return ((EqpEntry[])Enum.GetValues(typeof(EqpEntry)))
            .Where(e => e.ToEquipSlot() == slot)
            .ToArray();
    }

    /// <summary> A list of flags affecting the body. </summary>
    public static readonly IReadOnlyList<EqpEntry> EqpAttributesBody = GetEntriesForSlot(EquipSlot.Body);

    /// <summary> A list of flags affecting the legs. </summary>
    public static readonly IReadOnlyList<EqpEntry> EqpAttributesLegs = GetEntriesForSlot(EquipSlot.Legs);

    /// <summary> A list of flags affecting the hands. </summary>
    public static readonly IReadOnlyList<EqpEntry> EqpAttributesHands = GetEntriesForSlot(EquipSlot.Hands);

    /// <summary> A list of flags affecting the feet. </summary>
    public static readonly IReadOnlyList<EqpEntry> EqpAttributesFeet = GetEntriesForSlot(EquipSlot.Feet);

    /// <summary> A list of flags affecting the head. </summary>
    public static readonly IReadOnlyList<EqpEntry> EqpAttributesHead = GetEntriesForSlot(EquipSlot.Head);

    /// <summary> A dictionary mapping slots to a list of their affected flags. </summary>
    public static readonly IReadOnlyDictionary<EquipSlot, IReadOnlyList<EqpEntry>> EqpAttributes =
        FrozenDictionary.ToFrozenDictionary<EquipSlot, IReadOnlyList<EqpEntry>>(
        [
            new KeyValuePair<EquipSlot, IReadOnlyList<EqpEntry>>(EquipSlot.Body,  EqpAttributesBody),
            new KeyValuePair<EquipSlot, IReadOnlyList<EqpEntry>>(EquipSlot.Legs,  EqpAttributesLegs),
            new KeyValuePair<EquipSlot, IReadOnlyList<EqpEntry>>(EquipSlot.Hands, EqpAttributesHands),
            new KeyValuePair<EquipSlot, IReadOnlyList<EqpEntry>>(EquipSlot.Feet,  EqpAttributesFeet),
            new KeyValuePair<EquipSlot, IReadOnlyList<EqpEntry>>(EquipSlot.Head,  EqpAttributesHead),
        ]);
}
