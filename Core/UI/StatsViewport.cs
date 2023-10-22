using ImGuiNET;
using Renderer.Interfaces;
using Renderer.Interpreters;
using Renderer.Renderers.OpenGl;

namespace Renderer.UI;

public class StatsViewport : IViewport
{
    private bool close;
    private float updateRate = .1f;
    private float updateBuf = 0f;
    private int fpsBuff;
    
    private readonly ImGuiManager manager;
    private readonly IInterpreter interpreter;
    
    public StatsViewport(ImGuiManager manager)
    {
        this.manager = manager;
        this.interpreter = LoadViewport();
    }

    public bool ToRender() => !close;
    public IInterpreter LoadViewport()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"UI/{nameof(StatsViewport)}.xml");
        IInterpreter interpreter = InterpreterFactory.Create(filePath);
        
        interpreter.Parse();
        return interpreter;
    }

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