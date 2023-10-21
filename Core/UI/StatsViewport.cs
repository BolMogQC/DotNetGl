using ImGuiNET;
using Renderer.Interfaces;
using Renderer.Renderers.OpenGl;

namespace Renderer.UI;

public class StatsViewport : IViewport
{
    private bool close;
    private float updateRate = .1f;
    private float updateBuf = 0f;
    private int fpsBuff;
    
    private readonly ImGuiManager manager;
    
    public StatsViewport(ImGuiManager manager)
    {
        this.manager = manager;
    }

    public bool ToRender() => !close;
    public void Show() => close = false;
    public void Hide() => close = true;
    public void Close()
    {
        close = true;
    }

    public void Render(float delta)
    {
        if (updateBuf >= updateRate)
        {
            updateBuf = 0;
            fpsBuff = (int)(60 / delta);
        }
        updateBuf += delta;
        
        ImGui.Begin("Stats");
        
        ImGui.Text($"FPS: {fpsBuff}");
        ImGui.End();
    }

    public void Dispose()
    {
        manager.ClearViewport(this);
    }
}