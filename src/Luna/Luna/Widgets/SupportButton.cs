using Dalamud.Interface.ImGuiNotification;

namespace Luna;

/// <summary> Auxiliary functions to draw some of the support buttons. </summary>
public static class SupportButton
{
    private const string InternationalDiscordAddress = "https://discord.gg/kVva7DHV4r";
    private const string ChineseDiscordAddress       = "https://discord.gg/QvrVye3";
    private const string GuideAddress                = "https://reniguide.carrd.co/";
    private const string XivModArchiveAddress        = "https://www.xivmodarchive.com/";
    private const string HeliosphereAddress          = "https://heliosphere.app/";

    private static readonly ImEx.SplitButtonData InternationalDiscordData = new(new StringU8("国际服"u8))
    {
        Active     = 0xFF5B5EFFu,
        Background = 0xFFDA8972u,
        Hovered    = ColorParameter.Default,
        Tooltip =
            new StringU8(
                $"访问国际服开发者支持 Discord 服务器 {InternationalDiscordAddress}。\n请注意这里不对国服作支持，有问题推荐先去国服MOD频道咨询。"),
    };

    private static readonly ImEx.SplitButtonData ChineseDiscordData = new(new StringU8("国服"u8))
    {
        Active     = 0xFF492C00u,
        Background = 0xFF6B7280u,
        Hovered    = ColorParameter.Default,
        Tooltip =
            new StringU8($"访问国服模组社区 Discord 服务器 {ChineseDiscordAddress}。"),
    };

    private static readonly ImEx.SplitButtonData GuideData = new(new StringU8("新手指南"u8))
    {
        Active     = 0xFF5B5EFFu,
        Background = 0xFFCC648Du,
        Hovered    = ColorParameter.Default,
        Tooltip =
            new StringU8(
                $"访问 {GuideAddress}。\n由 Serenity 制作的基础教程，包含大部分 Penumbra 功能。\n不是官方指南，但通常不会过时。"),
    };

    private static readonly ImEx.SplitButtonData RestartTutorialData = new(new StringU8("重启教程"u8))
    {
        Active     = 0xFF492C00u,
        Background = 0xFF6B7280u,
        Hovered    = ColorParameter.Default,
        Tooltip    = new StringU8("从零开始重启 Penumbra 教程进度。"u8),
    };

    private static readonly ImEx.SplitButtonData XivModArchiveData = new(new StringU8("XMA"u8))
    {
        Active     = 0xFF5B5EFFu,
        Background = 0xFFDA8972u,
        Hovered    = ColorParameter.Default,
        Tooltip    = new StringU8($"访问 XIV Mod Archive 模组站点 {XivModArchiveAddress}。"),
    };

    private static readonly ImEx.SplitButtonData HeliosphereData = new(new StringU8("HSphere"u8))
    {
        Active     = 0xFF23AEE2u,
        Background = 0xFF6B7280u,
        Hovered    = ColorParameter.Default,
        Tooltip    = new StringU8($"访问 Heliosphere 模组站点 {HeliosphereAddress}。\n支持通过 Heliosphere 插件一键安装模组。"),
    };

    /// <summary> Draw a button to open the official Penumbra/Glamourer discord server. </summary>
    public static void Discord(MessageService message, float width)
    {
        const string address = "https://discord.gg/kVva7DHV4r";
        using var    color   = ImGuiColor.Button.Push(LunaStyle.DiscordColor);

        Link(message, "加入 Discord 服务器以获取支持"u8, address, width, $"访问 {address}");
    }

    /// <summary> Draw the button that opens the ReniGuide. </summary>
    public static void ReniGuide(MessageService message, float width)
    {
        using var color = Im.Color.Push(ImGuiColor.Button, LunaStyle.ReniColorButton)
            .Push(ImGuiColor.ButtonHovered, LunaStyle.ReniColorHovered)
            .Push(ImGuiColor.ButtonActive,  LunaStyle.ReniColorActive);

        Link(message, "新手指南"u8, "https://reniguide.carrd.co/", width,
            "访问 https://reniguide.carrd.co/\n由 Serenity 制作的包含 Penumbra 大部分功能的图片和文字教程（英文）。\n"u8
          + "非官方指南，但通常不会过时。"u8);
    }

    /// <summary> Draw a button that opens an address in the browser. </summary>
    public static void Link(MessageService message, Utf8LabelHandler text, string address, float width, Utf8TextHandler tooltip)
    {
        if (Im.Button(text, new Vector2(width, 0)))
            try
            {
                var process = new ProcessStartInfo(address)
                {
                    UseShellExecute = true,
                };
                Process.Start(process);
            }
            catch
            {
                message.NotificationMessage($"Could not open the link to {address} in external browser", NotificationType.Error);
            }

        Im.Tooltip.OnHover(ref tooltip);
    }

    /// <summary> Draw a split button for international and Chinese Discord servers. </summary>
    public static void DiscordSplit(MessageService message, Vector2 size)
    {
        var splitButtonDiscord = ImEx.SplitButton(6, InternationalDiscordData, ChineseDiscordData, size,
            InternationalDiscordData.Background.Color!.Value.Mix(ChineseDiscordData.Background.Color!.Value));
        var (address, name) = splitButtonDiscord switch
        {
            ImEx.SplitButtonHalf.UpperLeft  => (InternationalDiscordAddress, "国际服 Discord"),
            ImEx.SplitButtonHalf.LowerRight => (ChineseDiscordAddress, "国服 Discord"),
            _                               => (string.Empty, string.Empty),
        };
        if (address.Length is 0)
            return;

        try
        {
            var process = new ProcessStartInfo(address)
            {
                UseShellExecute = true,
            };
            Process.Start(process);
        }
        catch
        {
            message.NotificationMessage($"Could not open {name} link at {address} in external browser", NotificationType.Error);
        }
    }

    /// <summary> Draw a split button for guide and tutorial reset. </summary>
    public static void GuideTutorial(MessageService message, Vector2 size, Action resetTutorialAction)
    {
        var splitButtonGuide = ImEx.SplitButton(7, GuideData, RestartTutorialData, size,
            GuideData.Background.Color!.Value.Mix(RestartTutorialData.Background.Color!.Value));
        switch (splitButtonGuide)
        {
            case ImEx.SplitButtonHalf.UpperLeft:
                try
                {
                    var process = new ProcessStartInfo(GuideAddress)
                    {
                        UseShellExecute = true,
                    };
                    Process.Start(process);
                }
                catch
                {
                    message.NotificationMessage($"Could not open guide link at {GuideAddress} in external browser", NotificationType.Error);
                }

                break;
            case ImEx.SplitButtonHalf.LowerRight:
                resetTutorialAction();
                break;
        }
    }

    /// <summary> Draw a split button for mod sites. </summary>
    public static void ModSites(MessageService message, Vector2 size)
    {
        var splitButtonModSites = ImEx.SplitButton(8, XivModArchiveData, HeliosphereData, size,
            XivModArchiveData.Background.Color!.Value.Mix(HeliosphereData.Background.Color!.Value));
        var (address, name) = splitButtonModSites switch
        {
            ImEx.SplitButtonHalf.UpperLeft  => (XivModArchiveAddress, "XIV Mod Archive"),
            ImEx.SplitButtonHalf.LowerRight => (HeliosphereAddress, "Heliosphere"),
            _                               => (string.Empty, string.Empty),
        };
        if (address.Length is 0)
            return;

        try
        {
            var process = new ProcessStartInfo(address)
            {
                UseShellExecute = true,
            };
            Process.Start(process);
        }
        catch
        {
            message.NotificationMessage($"Could not open {name} link at {address} in external browser", NotificationType.Error);
        }
    }

    private const string KofiAddress    = "https://ko-fi.com/ottermandias";
    private const string PatreonAddress = "https://www.patreon.com/Ottermandias";

    private const string Happiness =
        "所有捐赠都是完全自愿的，不会获得任何优先待遇或特殊福利，只是让Otter开心。";

    private static readonly ImEx.SplitButtonData KoFiData = new(new StringU8("Ko-Fi"u8))
    {
        Active     = 0xFF5B5EFFu,
        Background = 0xFFFFC313u,
        Text       = 0xFF302010u,
        Hovered    = ColorParameter.Default,
        Tooltip    = new StringU8($"在浏览器中打开 Ottermandias 的 Ko-Fi 页面 {KofiAddress}。\n\n{Happiness}"),
    };

    private static readonly ImEx.SplitButtonData PatreonData = new(new StringU8("Patreon"u8))
    {
        Active     = 0xFF492C00u,
        Hovered    = ColorParameter.Default,
        Background = 0xFF5467F7u,
        Text       = 0xFF302010u,
        Tooltip    = new StringU8($"在浏览器中打开 Ottermandias 的 Patreon 页面 {PatreonAddress}。\n\n{Happiness}"),
    };

    /// <summary> Draw a split button to link to Ottermandias' Ko-Fi and Patreon. </summary>
    public static void KoFiPatreon(MessageService message, Vector2 size)
    {
        var splitButtonPatreon = ImEx.SplitButton(5, KoFiData, PatreonData, size,
            KoFiData.Background.Color!.Value.Mix(PatreonData.Background.Color!.Value));
        var (address, name) = splitButtonPatreon switch
        {
            ImEx.SplitButtonHalf.UpperLeft  => (KofiAddress, "Ko-Fi"),
            ImEx.SplitButtonHalf.LowerRight => (PatreonAddress, "Patreon"),
            _                               => (string.Empty, string.Empty),
        };
        if (address.Length is 0)
            return;

        try
        {
            var process = new ProcessStartInfo(address)
            {
                UseShellExecute = true,
            };
            Process.Start(process);
        }
        catch
        {
            message.NotificationMessage($"Could not open {name} link at {address} in external browser", NotificationType.Error);
        }
    }
}
