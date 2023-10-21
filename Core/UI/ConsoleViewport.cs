using ImGuiNET;
using Renderer.Interfaces;
using Renderer.Renderers.OpenGl;

namespace Renderer.UI;

public class ConsoleViewport : IViewport
{
    private bool close;
    private ImGuiManager manager; 

    public bool ToRender() => !close;

    public ConsoleViewport(ImGuiManager manager)
    {
        this.manager = manager;
    }

    public void Show() => close = false;
    public void Hide() => close = true;

    public void Close()
    {
        close = true;
        Dispose();
    }

    public void Render(float delta)
    {
        ImGui.Begin("Console", ref close, ImGuiWindowFlags.MenuBar);
        if (ImGui.BeginMenuBar())
        {
            if (ImGui.BeginMenu("File"))
            {
                if (ImGui.MenuItem("Clear", "Ctrl+R"))
                {
                    Console.WriteLine("Reset console");
                }
                if (ImGui.MenuItem("Close", "Ctrl+Q"))
                {
                    Close();
                }
                ImGui.EndMenu();
            }
            ImGui.EndMenuBar();
        }

        ImGui.Text("Hello World!");
        ImGui.End();
    }

    public void Dispose()
    {
        manager.ClearViewport(this);
    }
}