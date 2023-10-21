using System.Drawing;
using Renderer.Interfaces;
using Silk.NET.Core.Native;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Renderer.Renderers.OpenGl;

public class OpenGlRenderer : IRenderer
{
    private IWindow? window;
    private GL? api;
    private ImGuiManager? imGuiManager;

    public void Initialize()
    {
        window = Window.Create(WindowOptions.Default);
        
        window.Load += OnLoad;
        window.FramebufferResize += OnFramebufferResize;
        window.Render += OnRender;
        window.Closing += OnClosing;
    }

    public void Show()
    {
        window!.Run();
    }

    public void Dispose()
    {
        window!.Dispose();
    }

    public IWindow? GetWindow() => window;
    public NativeAPI? GetApi() => api;

#region Private Methods

    private void OnLoad()
    {
        api = window.CreateOpenGL();
        
        imGuiManager = new ImGuiManager();
        imGuiManager!.Load(this);
    }
    
    private void OnFramebufferResize(Vector2D<int> s)
    {
        api!.Viewport(s);
    }
    
    private void OnRender(double delta)
    {
        imGuiManager!.StartRenderTick((float)delta);

        api!.ClearColor(Color.DarkGray);
        api!.Clear((uint)ClearBufferMask.ColorBufferBit);
        
        imGuiManager.RenderTick((float)delta);

        imGuiManager.EndRenderTick((float)delta);
    }

    private void OnClosing()
    {
        imGuiManager!.Dispose();
        api!.Dispose();
    }
    
#endregion
    
}