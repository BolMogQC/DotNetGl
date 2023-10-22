using ImGuiNET;
using Renderer.Interfaces;
using Renderer.Interpreters;
using Renderer.Renderers.OpenGl;

namespace Renderer.UI;

public class ConsoleViewport : IViewport
{
    private bool close;
    private readonly ImGuiManager manager;
    private readonly IInterpreter interpreter;

    public bool ToRender() => !close;
    public IInterpreter LoadViewport()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"UI/{nameof(ConsoleViewport)}.xml");
        IInterpreter interpreter = InterpreterFactory.Create(filePath);
        
        interpreter.Parse();
        return interpreter;
    }

    public ConsoleViewport(ImGuiManager manager)
    {
        this.manager = manager;
        this.interpreter = LoadViewport();
    }

    public void Show() => close = false;
    public void Hide() => close = true;

    public void Close()
    {
        close = true;
        Dispose();
    }

    public async void Render(float delta)
    {
        interpreter.Render();
    }

    public void Dispose()
    {
        manager.ClearViewport(this);
    }
}