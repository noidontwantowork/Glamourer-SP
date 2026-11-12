namespace ImSharp;

public static partial class Im
{
    public static partial class Native
    {
        public unsafe struct PlatformIo
        {
            public delegate*<Viewport*, void>                      CreateWindow;
            public delegate*<Viewport*, void>                      DestroyWindow;
            public delegate*<Viewport*, void>                      ShowWindow;
            public delegate*<Viewport*, ImVec2, void>              SetWindowPos;
            public delegate*<Viewport*, ImVec2>                    GetWindowPos;
            public delegate*<Viewport*, ImVec2, void>              SetWindowSize;
            public delegate*<Viewport*, ImVec2>                    GetWindowSize;
            public delegate*<Viewport*, void>                      SetWindowFocus;
            public delegate*<Viewport*, bool>                      GetWindowFocus;
            public delegate*<Viewport*, bool>                      GetWindowMinimized;
            public delegate*<Viewport*, byte*, void>               SetWindowTitle;
            public delegate*<Viewport*, float, void>               SetWindowAlpha;
            public delegate*<Viewport*, void>                      UpdateWindow;
            public delegate*<Viewport*, void*, void>               RenderWindow;
            public delegate*<Viewport*, void*, void>               SwapBuffers;
            public delegate*<Viewport*, float>                     GetWindowDpiScale;
            public delegate*<Viewport*, void>                      OnChangedViewport;
            public delegate*<Viewport*, ulong, void*, ulong*, int> CreateVkSurface;
            public delegate*<Viewport*, void>                      RendererCreateWindow;
            public delegate*<Viewport*, void>                      RendererDestroyWindow;
            public delegate*<Viewport*, ImVec2, void>              RendererSetWindowSize;
            public delegate*<Viewport*, void*, void>               RendererRenderWindow;
            public delegate*<Viewport*, void*, void>               RendererSwapBuffers;
            public ImVector<ImGuiPlatformMonitor>                  Monitors;
            public ImVector<Pointer<Viewport>>                     Viewports;
        }
    }
}
