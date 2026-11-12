# Glamourer SP v1.7.1.4 变更说明

基线：`Glamourer 1.7.1.4`（国服 Glamourer-CN 版源码）。本版本在此基础上恢复并扩展“坐骑 / 宠物 / 召唤物”支持，并把插件对外名称改为 `Glamourer SP`。

## 目录结构

```
Glamourer SP\
├─ Glamourer.json …            ← 可直接使用的插件本体（Release 构建产物）
├─ *.dll / *.xml / *.deps.json
└─ Source\                     ← 完整可编译源码（含 Luna / Penumbra.* / Glamourer.Api 子模块）
   ├─ Glamourer\
   ├─ Glamourer.Api\
   ├─ Luna\
   ├─ Penumbra.Api\
   ├─ Penumbra.GameData\
   ├─ Penumbra.String\
   └─ Glamourer.slnx
```

> `Source` 目录名必须保持纯 ASCII：`Penumbra.GameData` 的 FlatSharp 代码生成器无法处理含中文的路径（会报 `expect path to exist`）。

## 改动清单

### ① 对坐骑 / 宠物 / 召唤物使用“应用到目标”

| 文件 | 改动 |
| --- | --- |
| `Glamourer\Services\DesignApplier.cs` | `CanApplyTo(ActorIdentifier, ActorData)` 中删除“目标必须是人形”的限制，改为只要求目标是可绘制角色（`IsCharacter`）。坐骑（Mount）、宠物（Companion）、召唤物都是 Character，因此 `应用到目标` 不再被拒绝；本身不是 Character 的对象仍返回 `TargetUnavailable`，避免访问无效 Character 指针。同时移除不再使用的 `HumanModelList` 依赖。 |
| `Glamourer\State\InternalStateEditor.cs` | `ChangeModelId` 中删除 1.7.1.4 新增的 `if (!oldIsHuman) return false;`，恢复“非人形状态可以转为人形”的能力——这是把人形设计/状态应用到坐骑上的必要前提。 |
| `Glamourer\Gui\Tabs\ActorTab\ActorPanel.cs` | 怪物/非人形面板恢复“变成人类”按钮（`StateManager.TurnHuman`），用于把坐骑、宠物等直接转成可自由定制外观与装备的人形角色。 |

流程：设计页选中设计 → 目标选为坐骑/宠物/召唤物 → 点“应用到目标”；或角色页选中该对象 → 点“变成人类”。应用后人形外观由设计决定，恢复/重绘走原有 Penumbra 重绘链路。

### ② 自动执行集可以把坐骑 / 宠物 / 召唤物作为目标

| 文件 | 改动 |
| --- | --- |
| `Glamourer\Gui\Tabs\AutomationTab\SetPanel.cs` | “添加当前目标 / 分配给当前目标”的有效性判定由 `data.Valid && Objects[0].IsHuman(humans)` 改为 `data.Valid && Objects[0].IsCharacter`，两个按键（Ctrl 为“添加当前目标”，否则为“分配给当前目标”）对坐骑/宠物/召唤物都可用。 |
| `Glamourer\Automation\AutoDesignManager.cs` | `CreateNpcs`（`GetGroup` 内部）对 `Mount / Companion / Ornament` 直接返回标识符本身：这类对象的数据 ID 唯一且必属某个玩家，原先按 BNpc/ENpc 名称词典反查会得到空组，导致 `IdentifierValid` 失败、按键虽可点但设置无效。 |
| `Glamourer\Automation\AutoDesignApplier.cs` | 1) `Reduce` 把 `!_humans.IsHuman(ModelCharaId)` 早退改为 `!actor.IsCharacter` 早退，自动执行得以作用于非人形角色。<br>2) 非人形状态合并设计时改用 `CustomizeArray.Default` 作为基准外貌（与 `StateEditor.ApplyDesign` 的做法一致），避免把非人形 customize 数据当成种族/性别去查表。<br>3) 新增 `TryGetAnyWorld`：`ApplyNew` 中坐骑/宠物/佩饰不再回退到“无主 NPC 标识”，只按“所属玩家 + 数据 ID（+ 世界）”匹配。<br>4) `GetPlayerSet` 的 `Owned` 分支同样禁止对 `Mount / Companion / Ornament` 回退到 `CreateNpc`，确保执行集只对**选定玩家**的那一只生效，不会波及他人同种坐骑/宠物。 |

### ③ 名称与描述

| 位置 | 内容 |
| --- | --- |
| `Glamourer\Gui\MainWindow.cs` | 主窗口标题：`Glamourer SP v{Version}`（匿名模式为 `Glamourer SP v{Version}（匿名模式）`，版本缺失时为 `Glamourer SP`）。 |
| `Glamourer\Glamourer.cs` | `IPlugin.Name` → `Glamourer SP`；加载日志同步。 |
| `Glamourer\Glamourer.json` | `Name` = `Glamourer SP`；`Punchline` = `改变和保存玩家外观。`；`Description` 为四行中文说明（含 SP 支持说明）；`AssemblyVersion` = `1.7.1.4`。 |
| `Glamourer\Glamourer.csproj` | `AssemblyVersion` / `FileVersion` = `1.7.1.4`，`Product` = `Glamourer SP`（标题里的版本号由此而来）。 |

`InternalName` 保持 `Glamourer`，配置目录、IPC 名称与原有数据兼容。

### 编译依赖补齐

| 文件 | 改动 |
| --- | --- |
| `Luna\Luna\Json\Utf8JsonWriterExtensions.cs` | 补充 `WriteFloatSecurely<T>`（1.7.1.4 的 `MaterialValueManager` 使用该方法写材质浮点，非有限值写成 `"Infinity"` / `"NaN"` / `"-Infinity"` 字符串），并让 `WriteIfNotNaN` 委托给它，与本版本所针对的上游 Luna 行为一致。 |

## 构建

```powershell
$env:DALAMUD_HOME = "$env:APPDATA\XIVLauncherCN\addon\Hooks\dev"
& "tools\dotnet10\dotnet.exe" build `
    "Dalamud Plugins\Glamourer\Glamourer SP\Source\Glamourer\Glamourer.csproj" -c Release
```

产物位于 `Source\Glamourer\bin\Release\`，已复制到本目录根（插件本体）。`Glamourer.csproj` 中 `Use_DalamudPackager=false`，因此不生成 zip。

## 已知限制

- 设计本身仍需是人形设计/状态（`ModelId == 0`）。`ChangeModelId` 对非 0 模型 ID 的拒绝是 1.7.1.4 上游行为，本次未改动——SP 的语义是“把坐骑/宠物/召唤物变成设计里的人形外观”，与 SP 1.7.1.0 一致。
- 时尚佩饰（Ornament）不是 Character，仍不作为应用/自动执行目标。
- 与原版 Glamourer 的 `InternalName` 相同，两者不能同时启用。
