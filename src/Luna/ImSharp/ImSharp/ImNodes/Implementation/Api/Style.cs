namespace ImSharp.ImNodes;

public static partial class ImNodes
{
    public static unsafe partial class Api
    {
        [MethodImpl(ImSharpConfiguration.Inl)]
        public static Internal.Io* GetIo()
            => &Context->Io;

        [MethodImpl(ImSharpConfiguration.Inl)]
        public static Internal.Style* GetStyle()
            => &Context->Style;

        public static void StyleColorsDark(Internal.Style* style)
        {
            style->Colors[(int)ImNodesColor.NodeBackground]                = new Rgba32(30,  50,  50);
            style->Colors[(int)ImNodesColor.NodeBackgroundHovered]         = new Rgba32(75,  75,  75);
            style->Colors[(int)ImNodesColor.NodeBackgroundSelected]        = new Rgba32(75,  75,  75);
            style->Colors[(int)ImNodesColor.NodeOutline]                   = new Rgba32(100, 100, 100);
            style->Colors[(int)ImNodesColor.TitleBar]                      = new Rgba32(41,  74,  122);
            style->Colors[(int)ImNodesColor.TitleBarHovered]               = new Rgba32(66,  150, 250);
            style->Colors[(int)ImNodesColor.TitleBarSelected]              = new Rgba32(66,  150, 250);
            style->Colors[(int)ImNodesColor.Link]                          = new Rgba32(61,  133, 224, 200);
            style->Colors[(int)ImNodesColor.LinkHovered]                   = new Rgba32(66,  150, 250);
            style->Colors[(int)ImNodesColor.LinkSelected]                  = new Rgba32(66,  150, 250);
            style->Colors[(int)ImNodesColor.Pin]                           = new Rgba32(53,  150, 250, 180);
            style->Colors[(int)ImNodesColor.PinHovered]                    = new Rgba32(53,  150, 250);
            style->Colors[(int)ImNodesColor.BoxSelector]                   = new Rgba32(61,  133, 224, 30);
            style->Colors[(int)ImNodesColor.BoxSelectorOutline]            = new Rgba32(61,  133, 224, 150);
            style->Colors[(int)ImNodesColor.GridBackground]                = new Rgba32(40,  40,  50,  200);
            style->Colors[(int)ImNodesColor.GridLine]                      = new Rgba32(200, 200, 200, 40);
            style->Colors[(int)ImNodesColor.GridLinePrimary]               = new Rgba32(240, 240, 240, 60);
            style->Colors[(int)ImNodesColor.MiniMapBackground]             = new Rgba32(25,  25,  25,  150);
            style->Colors[(int)ImNodesColor.MiniMapBackgroundHovered]      = new Rgba32(25,  25,  25,  200);
            style->Colors[(int)ImNodesColor.MiniMapOutline]                = new Rgba32(150, 150, 150, 100);
            style->Colors[(int)ImNodesColor.MiniMapOutlineHovered]         = new Rgba32(150, 150, 150, 200);
            style->Colors[(int)ImNodesColor.MiniMapNodeBackground]         = new Rgba32(200, 200, 200, 100);
            style->Colors[(int)ImNodesColor.MiniMapNodeBackgroundHovered]  = new Rgba32(200, 200, 200);
            style->Colors[(int)ImNodesColor.MiniMapNodeBackgroundSelected] = new Rgba32(200, 200, 200);
            style->Colors[(int)ImNodesColor.MiniMapNodeOutline]            = new Rgba32(200, 200, 200, 100);
            style->Colors[(int)ImNodesColor.MiniMapLink]                   = new Rgba32(61,  133, 224, 200);
            style->Colors[(int)ImNodesColor.MiniMapLinkSelected]           = new Rgba32(66,  150, 250);
            style->Colors[(int)ImNodesColor.MiniMapCanvas]                 = new Rgba32(200, 200, 200, 25);
            style->Colors[(int)ImNodesColor.MiniMapCanvasOutline]          = new Rgba32(200, 200, 200, 200);
        }

        public static void StyleColorsClassic(Internal.Style* style)
        {
            style->Colors[(int)ImNodesColor.NodeBackground]                = new Rgba32(50,  50,  50);
            style->Colors[(int)ImNodesColor.NodeBackgroundHovered]         = new Rgba32(75,  75,  75);
            style->Colors[(int)ImNodesColor.NodeBackgroundSelected]        = new Rgba32(75,  75,  75);
            style->Colors[(int)ImNodesColor.NodeOutline]                   = new Rgba32(100, 100, 100);
            style->Colors[(int)ImNodesColor.TitleBar]                      = new Rgba32(69,  69,  138);
            style->Colors[(int)ImNodesColor.TitleBarHovered]               = new Rgba32(82,  82,  161);
            style->Colors[(int)ImNodesColor.TitleBarSelected]              = new Rgba32(82,  82,  161);
            style->Colors[(int)ImNodesColor.Link]                          = new Rgba32(255, 255, 255, 100);
            style->Colors[(int)ImNodesColor.LinkHovered]                   = new Rgba32(105, 99,  204, 153);
            style->Colors[(int)ImNodesColor.LinkSelected]                  = new Rgba32(105, 99,  204, 153);
            style->Colors[(int)ImNodesColor.Pin]                           = new Rgba32(89,  102, 156, 170);
            style->Colors[(int)ImNodesColor.PinHovered]                    = new Rgba32(102, 122, 179, 200);
            style->Colors[(int)ImNodesColor.BoxSelector]                   = new Rgba32(82,  82,  161, 100);
            style->Colors[(int)ImNodesColor.BoxSelectorOutline]            = new Rgba32(82,  82,  161);
            style->Colors[(int)ImNodesColor.GridBackground]                = new Rgba32(40,  40,  50,  200);
            style->Colors[(int)ImNodesColor.GridLine]                      = new Rgba32(200, 200, 200, 040);
            style->Colors[(int)ImNodesColor.GridLinePrimary]               = new Rgba32(240, 240, 240, 060);
            style->Colors[(int)ImNodesColor.MiniMapBackground]             = new Rgba32(25,  25,  25,  100);
            style->Colors[(int)ImNodesColor.MiniMapBackgroundHovered]      = new Rgba32(25,  25,  25,  200);
            style->Colors[(int)ImNodesColor.MiniMapOutline]                = new Rgba32(150, 150, 150, 100);
            style->Colors[(int)ImNodesColor.MiniMapOutlineHovered]         = new Rgba32(150, 150, 150, 200);
            style->Colors[(int)ImNodesColor.MiniMapNodeBackground]         = new Rgba32(200, 200, 200, 100);
            style->Colors[(int)ImNodesColor.MiniMapNodeBackgroundHovered]  = new Rgba32(200, 200, 240);
            style->Colors[(int)ImNodesColor.MiniMapNodeBackgroundSelected] = new Rgba32(200, 200, 240);
            style->Colors[(int)ImNodesColor.MiniMapNodeOutline]            = new Rgba32(200, 200, 200, 100);
            style->Colors[(int)ImNodesColor.MiniMapLink]                   = new Rgba32(255, 255, 255, 100);
            style->Colors[(int)ImNodesColor.MiniMapLinkSelected]           = new Rgba32(105, 99,  204, 153);
            style->Colors[(int)ImNodesColor.MiniMapCanvas]                 = new Rgba32(200, 200, 200, 025);
            style->Colors[(int)ImNodesColor.MiniMapCanvasOutline]          = new Rgba32(200, 200, 200, 200);
        }

        public static void StyleColorsLight(Internal.Style* style)
        {
            style->Colors[(int)ImNodesColor.NodeBackground]                = new Rgba32(240, 240, 240);
            style->Colors[(int)ImNodesColor.NodeBackgroundHovered]         = new Rgba32(240, 240, 240);
            style->Colors[(int)ImNodesColor.NodeBackgroundSelected]        = new Rgba32(240, 240, 240);
            style->Colors[(int)ImNodesColor.NodeOutline]                   = new Rgba32(100, 100, 100);
            style->Colors[(int)ImNodesColor.TitleBar]                      = new Rgba32(248, 248, 248);
            style->Colors[(int)ImNodesColor.TitleBarHovered]               = new Rgba32(209, 209, 209);
            style->Colors[(int)ImNodesColor.TitleBarSelected]              = new Rgba32(209, 209, 209);
            style->Colors[(int)ImNodesColor.Link]                          = new Rgba32(66,  150, 250, 100);
            style->Colors[(int)ImNodesColor.LinkHovered]                   = new Rgba32(66,  150, 250, 242);
            style->Colors[(int)ImNodesColor.LinkSelected]                  = new Rgba32(66,  150, 250, 242);
            style->Colors[(int)ImNodesColor.Pin]                           = new Rgba32(66,  150, 250, 160);
            style->Colors[(int)ImNodesColor.PinHovered]                    = new Rgba32(66,  150, 250);
            style->Colors[(int)ImNodesColor.BoxSelector]                   = new Rgba32(90,  170, 250, 30);
            style->Colors[(int)ImNodesColor.BoxSelectorOutline]            = new Rgba32(90,  170, 250, 150);
            style->Colors[(int)ImNodesColor.GridBackground]                = new Rgba32(225, 225, 225);
            style->Colors[(int)ImNodesColor.GridLine]                      = new Rgba32(180, 180, 180, 100);
            style->Colors[(int)ImNodesColor.GridLinePrimary]               = new Rgba32(120, 120, 120, 100);
            style->Colors[(int)ImNodesColor.MiniMapBackground]             = new Rgba32(25,  25,  25,  100);
            style->Colors[(int)ImNodesColor.MiniMapBackgroundHovered]      = new Rgba32(25,  25,  25,  200);
            style->Colors[(int)ImNodesColor.MiniMapOutline]                = new Rgba32(150, 150, 150, 100);
            style->Colors[(int)ImNodesColor.MiniMapOutlineHovered]         = new Rgba32(150, 150, 150, 200);
            style->Colors[(int)ImNodesColor.MiniMapNodeBackground]         = new Rgba32(200, 200, 200, 100);
            style->Colors[(int)ImNodesColor.MiniMapNodeBackgroundHovered]  = new Rgba32(200, 200, 240);
            style->Colors[(int)ImNodesColor.MiniMapNodeBackgroundSelected] = new Rgba32(200, 200, 240);
            style->Colors[(int)ImNodesColor.MiniMapNodeOutline]            = new Rgba32(200, 200, 200, 100);
            style->Colors[(int)ImNodesColor.MiniMapLink]                   = new Rgba32(66,  150, 250, 100);
            style->Colors[(int)ImNodesColor.MiniMapLinkSelected]           = new Rgba32(66,  150, 250, 242);
            style->Colors[(int)ImNodesColor.MiniMapCanvas]                 = new Rgba32(200, 200, 200, 25);
            style->Colors[(int)ImNodesColor.MiniMapCanvasOutline]          = new Rgba32(200, 200, 200, 200);
        }
    }
}
