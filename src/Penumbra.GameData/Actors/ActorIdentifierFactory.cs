using System.Collections.Frozen;
using Dalamud.Plugin.Services;
using ImSharp;
using Penumbra.GameData.Data;
using Penumbra.GameData.DataContainers.Bases;
using Penumbra.GameData.Enums;
using Penumbra.GameData.Interop;
using Penumbra.GameData.Structs;
using Penumbra.String;
using ObjectKind = Dalamud.Game.ClientState.Objects.Enums.ObjectKind;

namespace Penumbra.GameData.Actors;

/// <summary> Creation of ActorIdentifiers. </summary>
public class ActorIdentifierFactory(ObjectManager objects, IFramework framework, NameDicts data, CutsceneResolver toParentIdx)
{
    /// <summary> Expose the _toParentIdx function for convenience. </summary>
    /// <returns> The parent index for a cutscene object or -1 if no parent exists. </returns>
    public short ToCutsceneParent(ushort index)
        => toParentIdx.Invoke(index);

    /// <summary> Used in construction from user strings. </summary>
    public class IdentifierParseError(string reason) : Exception(reason);

    /// <summary> Create an ImGui Tooltip for user strings. </summary>
    public static void WriteUserStringTooltip(bool withIndex)
    {
        using var tt   = Im.Tooltip.Begin();
        using var font = Im.Font.PushMono();
        Im.Text("标识符字符串的有效格式为："u8);

        const uint typeColor    = 0xFF40FF40;
        const uint nameColor    = 0xFF00D0D0;
        const uint keyColor     = 0xFFFFD060;
        const uint npcTypeColor = 0xFFFF40FF;
        const uint npcNameColor = 0xFF4040FF;
        const uint indexColor   = 0xFFA0A0A0;

        Im.Bullet();
        Im.Line.Same();
        ImEx.TextMultiColored("P"u8, typeColor)
            .Then(" | "u8,                        keyColor)
            .Then("[玩家名称]@<服务器名称>"u8, nameColor)
            .End();

        Im.Bullet();
        Im.Line.Same();
        ImEx.TextMultiColored("R"u8, typeColor)
            .Then(" | "u8,             keyColor)
            .Then("[雇员名称]"u8, nameColor)
            .End();

        Im.Bullet();
        Im.Line.Same();
        var text = ImEx.TextMultiColored("N"u8, typeColor)
            .Then(" | "u8,        keyColor)
            .Then("[NPC类型]"u8, npcTypeColor)
            .Then(" : "u8,        keyColor)
            .Then("[NPC名称]"u8, npcNameColor);

        if (withIndex)
            text.Then("@"u8, keyColor)
                .Then("<Object Index>"u8, indexColor)
                .End();
        else
            text.End();

        Im.Bullet();
        Im.Line.Same();
        ImEx.TextMultiColored("所有的 [] 或 <> 这样的括号只是占位符不需要输入，所有的"u8)
            .Then("亮蓝色符号"u8, keyColor)
            .Then("必须使用。"u8)
            .End();

        Im.Bullet();
        Im.Line.Same();
        ImEx.TextMultiColored("O"u8, typeColor)
            .Then(" | "u8,                        keyColor)
            .Then("[NPC类型]"u8,                 npcTypeColor)
            .Then(" : "u8,                        keyColor)
            .Then("[NPC名称]"u8,                 npcNameColor)
            .Then(" | "u8,                        keyColor)
            .Then("[玩家名称]@<服务器名称>"u8, nameColor)
            .End();

        Im.Line.New();
        Im.Bullet();
        Im.Line.Same();
        ImEx.TextMultiColored("[P]"u8, typeColor)
            .Then("玩家, "u8)
            .Then("[R]"u8, typeColor)
            .Then("雇员, "u8)
            .Then("[N]"u8, typeColor)
            .Then("NPC或 "u8)
            .Then("[O]"u8, typeColor)
            .Then("从属的描述标识符类型。"u8)
            .End();

        Im.Line.New();
        Im.Bullet();
        Im.Line.Same();
        ImEx.TextMultiColored("[玩家名称]"u8, nameColor)
            .Then("和"u8)
            .Then("[雇员名称]"u8, nameColor)
            .Then("必须符合命名规则。"u8)
            .End();

        Im.Bullet();
        Im.Line.Same();
        ImEx.TextMultiColored("<服务器名称>"u8, nameColor)
            .Then("是可填项（如果不提供则代表"u8)
            .Then("任意服务器"u8, nameColor)
            .Then("），填写则必须是有效服务器名称。"u8)
            .End();

        Im.Bullet();
        Im.Line.Same();
        ImEx.TextMultiColored("[NPC类型]"u8, npcTypeColor)
            .Then("可以是"u8)
            .Then("[M]"u8, npcTypeColor)
            .Then("坐骑, "u8)
            .Then("[C]"u8, npcTypeColor)
            .Then("宠物, "u8)
            .Then("[A]"u8, npcTypeColor)
            .Then("时尚佩饰, "u8)
            .Then("[E]"u8, npcTypeColor)
            .Then("事件NPC或"u8)
            .Then("[B]"u8, npcTypeColor)
            .Then("战斗NPC。"u8)
            .End();

        Im.Bullet();
        Im.Line.Same();
        ImEx.TextMultiColored("[NPC名称]"u8, npcNameColor)
            .Then("必须是符合所选类型且有效已知的名称"u8)
            .End();

        if (withIndex)
        {
            Im.Bullet();
            Im.Line.Same();
            ImEx.TextMultiColored("<Object Index>"u8, indexColor)
                .Then(" is optional and must be a non-negative index into the object table."u8)
                .End();
        }
    }

    /// <summary> Convert a string into a list of ActorIdentifiers. </summary>
    /// <param name="userString"> The input string. </param>
    /// <param name="allowIndex"> Whether the conversion allows and respects game object indices. </param>
    /// <returns> All matching actor identifiers. </returns>
    /// <exception cref="IdentifierParseError"> Any failure to convert throws an exception with the failure reason. </exception>
    /// <remarks>
    /// Valid formats are:
    /// <list type="bullet">
    ///     <item><code>p|[Player Name]@{World Name}</code> players of name from world, world is optional</item>
    ///     <item><code>r|[Retainer Name]</code> retainers</item>
    ///     <item><code>n|[NPC Type]:[NPC Name]@{Object Index}</code> NPCs of type MCAEB and a given name, at an optional index which may be disallowed.</item>
    ///     <item><code>o|[NPC Type]:[NPC Name]|[Player Name]@{World Name}</code> Owned NPCs.</item>
    /// </list>
    /// </remarks>
    public ActorIdentifier[] FromUserString(string userString, bool allowIndex)
    {
        if (userString.Length == 0)
            throw new IdentifierParseError("标识符字符串为空。");

        var split = userString.Split('|', 3, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (split.Length < 2)
            throw new IdentifierParseError($"The identifier string {userString} does not contain a type and a value.");

        switch (split[0].ToLowerInvariant())
        {
            case "p":
            case "player":
            {
                var (playerName, worldId) = ParsePlayer(split[1]);
                return [CreateIndividualUnchecked(IdentifierType.Player, playerName, worldId.Id, 0, 0)];
            }
            case "r":
            case "retainer":
            {
                if (!VerifyRetainerName(split[1]))
                    throw new IdentifierParseError($"{split[1]} 不是有效的玩家名称。");
                if (!ByteString.FromString(split[1], out var playerName))
                    throw new IdentifierParseError($"雇员字符串 {split[1]} 包含无效符号。");

                return [CreateIndividualUnchecked(IdentifierType.Retainer, playerName, 0, 0, 0)];
            }
            case "n":
            case "npc":
            {
                var (kind, objectIds, worldId) = ParseNpc(split[1], allowIndex);
                return objectIds.Select(i => CreateIndividualUnchecked(IdentifierType.Npc, ByteString.Empty, worldId.Id, kind, i)).ToArray();
            }
            case "o":
            case "owned":
            {
                if (split.Length < 3)
                    throw new IdentifierParseError(
                        "Owned NPCs need a NPC and a player, separated by '|', but only one was provided.");

                var (kind, objectIds, _)  = ParseNpc(split[1], allowIndex);
                var (playerName, worldId) = ParsePlayer(split[2]);
                return objectIds.Select(i => CreateIndividualUnchecked(IdentifierType.Owned, playerName, worldId.Id, kind, i)).ToArray();
            }
            default:
                throw new IdentifierParseError(
                    $"{split[0]} is not a valid identifier type. Valid types are [P]layer, [R]etainer, [N]PC, or [O]wned");
        }
    }

    /// <summary> Compute an ActorIdentifier from a GameObject. </summary>
    /// <param name="actor"> The game object. Can be null. </param>
    /// <param name="owner"> A returned owner if the game object has an owner. </param>
    /// <param name="allowPlayerNpc"> Allow to treat npcs as players in certain cases for Anamnesis compatibility.</param>
    /// <param name="check"> Check obtained data for validity. </param>
    /// <param name="withoutIndex"> Skip the game object index for unowned NPCs. </param>
    /// <returns> An actor identifier for that object. </returns>
    public unsafe ActorIdentifier FromObject(Actor actor, out Actor owner, bool allowPlayerNpc, bool check, bool withoutIndex)
    {
        owner = Actor.Null;
        if (!actor.Valid)
            return ActorIdentifier.Invalid;

        actor = HandleCutscene(actor);
        var idx = actor.Index;
        if (idx.Index is >= (ushort)ScreenActor.CharacterScreen and <= (ushort)ScreenActor.Card8)
            return CreateIndividualUnchecked(IdentifierType.Special, ByteString.Empty, idx.Index, ObjectKind.None, uint.MaxValue);

        var kind = (ObjectKind)actor.AsObject->ObjectKind;
        return kind switch
        {
            ObjectKind.Pc        => CreatePlayerFromObject(actor, check),
            ObjectKind.BattleNpc => CreateBNpcFromObject(actor, out owner, check, allowPlayerNpc, withoutIndex),
            ObjectKind.EventNpc  => CreateENpcFromObject(actor, check, withoutIndex),
            ObjectKind.Mount     => CreateCompanionFromObject(actor, out owner, kind, check),
            ObjectKind.Companion => CreateCompanionFromObject(actor, out owner, kind, check),
            ObjectKind.Ornament  => CreateCompanionFromObject(actor, out owner, kind, check),
            ObjectKind.Retainer  => CreateRetainerFromObject(actor, check),
            _                    => CreateUnkFromObject(actor, withoutIndex),
        };
    }

    /// <inheritdoc cref="FromObject(Actor,out Actor,bool,bool,bool)"/>
    public ActorIdentifier FromObject(Dalamud.Game.ClientState.Objects.Types.IGameObject? actor,
        out Actor owner, bool allowPlayerNpc, bool check, bool withoutIndex)
        => FromObject(actor?.Address ?? nint.Zero, out owner, allowPlayerNpc, check, withoutIndex);

    /// <inheritdoc cref="FromObject(Actor,out Actor,bool,bool,bool)"/>
    public ActorIdentifier FromObject(Dalamud.Game.ClientState.Objects.Types.IGameObject? actor, bool allowPlayerNpc, bool check,
        bool withoutIndex)
        => FromObject(actor, out _, allowPlayerNpc, check, withoutIndex);

    /// <summary> Create an individual from existing data, which is checked for correctness. </summary>
    /// <param name="type"> The type of actor. </param>
    /// <param name="name"> The name of the actor for players, retainers and owned objects. </param>
    /// <param name="value"> WorldId, ScreenActor, ObjectIndex or RetainerType. </param>
    /// <param name="kind"> The object kind for NPCs. </param>
    /// <param name="dataId"> The data id for NPCs. </param>
    /// <returns> An identifier for that data. </returns>
    public ActorIdentifier CreateIndividual(IdentifierType type, ByteString name, ushort value, ObjectKind kind, NpcId dataId)
        => type switch
        {
            IdentifierType.Player    => CreatePlayer(name, value),
            IdentifierType.Retainer  => CreateRetainer(name, (ActorIdentifier.RetainerType)value),
            IdentifierType.Owned     => CreateOwned(name, value, kind, dataId),
            IdentifierType.Special   => CreateSpecial((ScreenActor)value),
            IdentifierType.Npc       => CreateNpc(kind, dataId, value),
            IdentifierType.UnkObject => CreateIndividualUnchecked(IdentifierType.UnkObject, name, value, ObjectKind.None, 0),
            _                        => ActorIdentifier.Invalid,
        };

    /// <inheritdoc cref="CreateIndividual"/>
    /// <summary> Create an individual from existing data, which is left unchecked. </summary>
    /// <remarks> Only use this if you are sure the data is valid. </remarks>
    public ActorIdentifier CreateIndividualUnchecked(IdentifierType type, ByteString name, ushort value, ObjectKind kind, NpcId dataId)
        => new(type, kind, (ObjectIndex)value, dataId, name);

    /// <summary> Create a player from name and home world. Input is checked for correctness. </summary>
    public ActorIdentifier CreatePlayer(ByteString name, WorldId homeWorld)
    {
        if (!VerifyWorld(homeWorld) || !VerifyPlayerName(name.Span))
            return ActorIdentifier.Invalid;

        return new ActorIdentifier(IdentifierType.Player, ObjectKind.Pc, homeWorld, 0, name);
    }

    /// <summary> Create a retainer from name and retainer type. Input is checked for correctness. </summary>
    public ActorIdentifier CreateRetainer(ByteString name, ActorIdentifier.RetainerType type)
    {
        if (!VerifyRetainerName(name.Span))
            return ActorIdentifier.Invalid;

        return new ActorIdentifier(IdentifierType.Retainer, ObjectKind.Retainer, type, 0, name);
    }

    /// <summary> Create a special actor identifier from special input. Input is checked for correctness. </summary>
    public ActorIdentifier CreateSpecial(ScreenActor actor)
    {
        if (!VerifySpecial(actor))
            return ActorIdentifier.Invalid;

        return new ActorIdentifier(IdentifierType.Special, ObjectKind.Pc, (ObjectIndex)(uint)actor, 0, ByteString.Empty);
    }

    /// <summary> Create an NPC from kind and id. Input is checked for correctness. </summary>
    public ActorIdentifier CreateNpc(ObjectKind kind, NpcId data)
        => CreateNpc(kind, data, ObjectIndex.AnyIndex);

    /// <summary> Create an NPC from kind, id and index. Input is checked for correctness. </summary>
    public ActorIdentifier CreateNpc(ObjectKind kind, NpcId data, ObjectIndex index)
    {
        if (!VerifyIndex(index) || !VerifyNpcData(kind, data))
            return ActorIdentifier.Invalid;

        return new ActorIdentifier(IdentifierType.Npc, kind, index, data, ByteString.Empty);
    }

    /// <summary> Create an owned NPC from kind and id and the owners name and home world. Input is checked for correctness. </summary>
    public ActorIdentifier CreateOwned(ByteString ownerName, WorldId homeWorld, ObjectKind kind, NpcId dataId)
    {
        if (!VerifyWorld(homeWorld) || !VerifyPlayerName(ownerName.Span) || !VerifyOwnedData(kind, dataId))
            return ActorIdentifier.Invalid;

        return new ActorIdentifier(IdentifierType.Owned, kind, homeWorld, dataId, ownerName);
    }

    #region Verification

    /// <summary> Checks SE naming rules. </summary>
    public static bool VerifyPlayerName(ReadOnlySpan<byte> name)
    {
        if (name.Length == 0) return false;
        return FFXIVClientStructs.FFXIV.Client.UI.UIGlobals.IsValidPlayerCharacterName(name);
    }

    /// <summary> Checks SE naming rules. </summary>
    public static bool VerifyPlayerName(ReadOnlySpan<char> name)
    {
        if (name.Length == 0) return false;
        return FFXIVClientStructs.FFXIV.Client.UI.UIGlobals.IsValidPlayerCharacterName(name.ToString());
    }

    /// <summary> Checks SE naming rules. </summary>
    public static bool VerifyRetainerName(ReadOnlySpan<byte> name)
    {
        if (name.Length == 0) return false;
        return FFXIVClientStructs.FFXIV.Client.UI.UIGlobals.IsValidPlayerCharacterName(name);
    }

    /// <summary> Checks SE naming rules. </summary>
    public static bool VerifyRetainerName(ReadOnlySpan<char> name)
    {
        if (name.Length == 0) return false;
        return FFXIVClientStructs.FFXIV.Client.UI.UIGlobals.IsValidPlayerCharacterName(name.ToString());
    }

    /// <summary> Checks if the world is a valid public world or ushort.MaxValue (any world). </summary>
    public bool VerifyWorld(WorldId worldId)
        => worldId == WorldId.AnyWorld || data.Worlds.ContainsKey(worldId.Id);

    /// <summary> Verify that the enum value is a specific actor and return the name if it is. </summary>
    public static bool VerifySpecial(ScreenActor actor)
        => actor is >= ScreenActor.CharacterScreen and <= ScreenActor.Card8;

    /// <summary> Verify that the object index is a valid index for an NPC. </summary>
    public bool VerifyIndex(ObjectIndex index)
    {
        if (index == ObjectIndex.AnyIndex)
            return true;
        if (index < ObjectIndex.GPosePlayer)
            return (index.Index & 1) == 0;
        if (index > ObjectIndex.Card8)
            return index.Index < objects.TotalCount;

        return index < ObjectIndex.CharacterScreen;
    }

    /// <summary> Verify that the object kind is a valid owned object, and the corresponding data ID. </summary>
    public bool VerifyOwnedData(ObjectKind kind, NpcId dataId)
    {
        return kind switch
        {
            ObjectKind.Mount     => data.Mounts.ContainsKey(dataId.Id),
            ObjectKind.Companion => data.Companions.ContainsKey(dataId.Id),
            ObjectKind.Ornament  => data.Ornaments.ContainsKey(dataId.Id),
            ObjectKind.BattleNpc => data.BNpcs.ContainsKey(dataId.Id),
            _                    => false,
        };
    }

    /// <summary> Verify that the data ID is valid for the object kind. </summary>
    public bool VerifyNpcData(ObjectKind kind, NpcId dataId)
        => kind switch
        {
            ObjectKind.Mount     => data.Mounts.ContainsKey(dataId.Id),
            ObjectKind.Companion => data.Companions.ContainsKey(dataId.Id),
            ObjectKind.Ornament  => data.Ornaments.ContainsKey(dataId.Id),
            ObjectKind.BattleNpc => data.BNpcs.ContainsKey(dataId.Id),
            ObjectKind.EventNpc  => data.ENpcs.ContainsKey(dataId.Id),
            _                    => false,
        };

    #endregion

    #region FromObjectHelpers

    /// <summary> Create a player from the game object.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private ActorIdentifier CreatePlayerFromObject(Actor actor, bool check)
    {
        var name      = actor.Utf8Name;
        var homeWorld = actor.HomeWorld;
        return check
            ? CreatePlayer(name, homeWorld)
            : CreateIndividualUnchecked(IdentifierType.Player, name, homeWorld, ObjectKind.None, uint.MaxValue);
    }

    /// <summary> Create a battle npc from the game object.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private unsafe ActorIdentifier CreateBNpcFromObject(Actor actor, out Actor owner, bool check, bool allowPlayerNpc,
        bool withoutIndex)
    {
        var ownerId = actor.AsObject->OwnerId;
        // 952 -> 780 is a special case for chocobos because they have NameId == 0 otherwise.
        var nameId = actor.AsObject->BaseId == 952 ? 780 : actor.AsCharacter->NameId;
        if (ownerId != 0xE0000000)
        {
            owner = HandleCutscene(objects.ById(ownerId));
            if (!owner.Valid)
                return ActorIdentifier.Invalid;

            var name      = owner.Utf8Name;
            var homeWorld = owner.HomeWorld;
            return check
                ? CreateOwned(name, homeWorld, ObjectKind.BattleNpc, nameId)
                : CreateIndividualUnchecked(IdentifierType.Owned, name, homeWorld, ObjectKind.BattleNpc, nameId);
        }

        owner = Actor.Null;
        // Hack to support Anamnesis changing ObjectKind for NPC faces.
        if (nameId == 0 && allowPlayerNpc)
        {
            var name = actor.Utf8Name;
            if (!name.IsEmpty)
            {
                var homeWorld = actor.HomeWorld;
                return check
                    ? CreatePlayer(name, homeWorld)
                    : CreateIndividualUnchecked(IdentifierType.Player, name, homeWorld, ObjectKind.None, uint.MaxValue);
            }
        }

        var index = withoutIndex ? ObjectIndex.AnyIndex : actor.Index;
        return check
            ? CreateNpc(ObjectKind.BattleNpc, nameId, index)
            : CreateIndividualUnchecked(IdentifierType.Npc, ByteString.Empty, index.Index, ObjectKind.BattleNpc, nameId);
    }

    /// <summary> Create an event npc from the game object.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private unsafe ActorIdentifier CreateENpcFromObject(Actor actor, bool check, bool withoutIndex)
    {
        var dataId = actor.AsObject->BaseId;
        // Special case for squadron that is also in the game functions, cf. E8 ?? ?? ?? ?? 89 87 ?? ?? ?? ?? 4C 89 BF
        if (dataId == 0xF845D)
            dataId = actor.AsObject->GetNameId();
        if (MannequinIds.Contains(dataId))
        {
            var retainerName = new ByteString(actor.AsObject->Name);
            var actualName   = framework.IsInFrameworkUpdateThread ? new ByteString(actor.AsObject->GetName()) : ByteString.Empty;
            if (!actualName.Equals(retainerName))
            {
                var ident = check
                    ? CreateRetainer(retainerName, ActorIdentifier.RetainerType.Mannequin)
                    : CreateIndividualUnchecked(IdentifierType.Retainer, retainerName, (ushort)ActorIdentifier.RetainerType.Mannequin,
                        ObjectKind.EventNpc,                             dataId);
                if (ident.IsValid)
                    return ident;
            }
        }

        var index = withoutIndex ? ObjectIndex.AnyIndex : actor.Index;
        return check
            ? CreateNpc(ObjectKind.EventNpc, dataId, index)
            : CreateIndividualUnchecked(IdentifierType.Npc, ByteString.Empty, index.Index, ObjectKind.EventNpc, dataId);
    }

    /// <summary> Create a companion npc from the game object.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private ActorIdentifier CreateCompanionFromObject(Actor actor, out Actor owner, ObjectKind kind, bool check)
    {
        owner = HandleCutscene(objects.CompanionParent(actor));
        if (!owner.Valid)
            return ActorIdentifier.Invalid;

        var dataId    = GetCompanionId(actor, owner);
        var name      = owner.Utf8Name;
        var homeWorld = owner.HomeWorld;
        if (check)
            return CreateOwned(name, homeWorld, kind, dataId);

        if (name.Length > 0)
            return CreateIndividualUnchecked(IdentifierType.Owned, name, homeWorld, kind, dataId);

        return CreateNpc(kind, dataId, actor.Index);
    }

    /// <summary> Create a retainer from the game object.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private ActorIdentifier CreateRetainerFromObject(Actor actor, bool check)
    {
        var name = actor.Utf8Name;
        return check
            ? CreateRetainer(name, ActorIdentifier.RetainerType.Bell)
            : CreateIndividualUnchecked(IdentifierType.Retainer, name, (ushort)ActorIdentifier.RetainerType.Bell, ObjectKind.None,
                uint.MaxValue);
    }

    /// <summary> Create an unknown object. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private ActorIdentifier CreateUnkFromObject(Actor actor, bool withoutIndex)
    {
        var name  = actor.Utf8Name;
        var index = withoutIndex ? ObjectIndex.AnyIndex : actor.Index;
        return CreateIndividualUnchecked(IdentifierType.UnkObject, name, index.Index, ObjectKind.None, 0);
    }

    #endregion

    /// <summary> Obtain the current companion ID for an object by its actor and owner. </summary>
    private static unsafe NpcId GetCompanionId(Actor actor,
        Actor owner)
    {
        return (ObjectKind)actor.AsObject->ObjectKind switch
        {
            ObjectKind.Mount     => owner.AsCharacter->Mount.MountId,
            ObjectKind.Ornament  => owner.AsCharacter->OrnamentData.OrnamentId,
            ObjectKind.Companion => actor.AsObject->BaseId,
            _                    => actor.AsObject->BaseId,
        };
    }

    /// <summary> Handle owned cutscene actors. </summary>
    private Actor HandleCutscene(Actor main)
    {
        if (!main.Valid)
            return Actor.Null;

        if (main.Index.Index is < (ushort)ScreenActor.CutsceneStart or >= (ushort)ScreenActor.CutsceneEnd)
            return main;

        var parentIdx = toParentIdx.Invoke(main.Index.Index);
        var parent    = objects[parentIdx];
        return parent.Valid ? parent : main;
    }

    /// <summary> The existing IDs that correspond to mannequins. </summary>
    private static readonly FrozenSet<ENpcId> MannequinIds = FrozenSet.ToFrozenSet<ENpcId>(
    [
        1026228u,
        1026229u,
        1026986u,
        1026987u,
        1026988u,
        1026989u,
        1032291u,
        1032292u,
        1032293u,
        1032294u,
        1033046u,
        1033047u,
        1033658u,
        1033659u,
        1007137u,
    ]);

    /// <summary> Parse a user string for player identifier data. </summary>
    private (ByteString, WorldId) ParsePlayer(string player)
    {
        var parts = player.Split('@', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (!VerifyPlayerName(parts[0]))
            throw new IdentifierParseError($"{parts[0]} is not a valid player name.");
        if (!ByteString.FromString(parts[0], out var p))
            throw new IdentifierParseError($"The player string {parts[0]} contains invalid symbols.");

        var world = parts.Length == 2
            ? data.ToWorldId(parts[1])
            : WorldId.AnyWorld;

        if (!VerifyWorld(world))
            throw new IdentifierParseError($"{parts[1]} is not a valid world name.");

        return (p, world);
    }

    /// <summary> Parse a user string for npc identifier data. </summary>
    private (ObjectKind, NpcId[], WorldId) ParseNpc(string npc, bool allowIndex)
    {
        var indexString = allowIndex ? "@<Index>" : string.Empty;
        var split2      = npc.Split(':', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (split2.Length != 2)
            throw new IdentifierParseError($"NPCs need to be specified by '[Object Type]:[NPC Name]{indexString}'.");

        var split3 = allowIndex
            ? split2[1].Split('@', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            : [split2[1]];

        return split2[0].ToLowerInvariant() switch
        {
            "m" or "mount" => FindDataId(split3[0], data.Mounts, out var id)
                ? (ObjectKind.Mount, mountId: id, GetIndex())
                : throw new IdentifierParseError($"Could not identify a Mount named {split2[1]}."),
            "c" or "companion" or "minion" or "mini" => FindDataId(split3[0], data.Companions, out var id)
                ? (ObjectKind.Companion, cId: id, GetIndex())
                : throw new IdentifierParseError($"Could not identify a Minion named {split2[1]}."),
            "a" or "o" or "accessory" or "ornament" => FindDataId(split3[0], data.Ornaments, out var id)
                ? (ObjectKind.Ornament, id, GetIndex())
                : throw new IdentifierParseError($"Could not identify an Accessory named {split2[1]}."),
            "e" or "enpc" or "eventnpc" or "event npc" => FindDataId(split3[0], data.ENpcs, out var id)
                ? (ObjectKind.EventNpc, id, GetIndex())
                : throw new IdentifierParseError($"Could not identify an Event NPC named {split2[1]}."),
            "b" or "bnpc" or "battlenpc" or "battle npc" => FindDataId(split3[0], data.BNpcs, out var id)
                ? (ObjectKind.BattleNpc, id, GetIndex())
                : throw new IdentifierParseError($"Could not identify a Battle NPC named {split2[1]}."),
            _ => throw new IdentifierParseError($"The argument {split2[0]} is not a valid NPC Type."),
        };

        WorldId GetIndex()
        {
            var idx = WorldId.AnyWorld;
            if (split3.Length != 2)
                return idx;

            if (ushort.TryParse(split3[1], out var intIdx) && intIdx < objects.TotalCount)
                idx = intIdx;
            else
                throw new IdentifierParseError($"Could not parse index {split3[1]} to valid Index.");

            return idx;
        }

        static bool FindDataId(string name, NameDictionary data, out NpcId[] dataIds)
        {
            dataIds = data.Where(kvp => kvp.Value.Equals(name, StringComparison.OrdinalIgnoreCase)).Select(kvp => kvp.Key).ToArray();
            return dataIds.Length > 0;
        }
    }
}
