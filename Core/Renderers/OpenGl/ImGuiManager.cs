using ImGuiNET;
using Renderer.Enums;
using Renderer.Interfaces;
using Renderer.UI;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace Renderer.Renderers.OpenGl;

public class ImGuiManager
{
    public ImGuiState State { get; private set; } = ImGuiState.NotLoaded;
    
    private ImGuiController controller = null!;
    private IInputContext inputContext = null!;

    private readonly List<IViewport> viewports = new();

    public void Load(IRenderer renderer)
    {
        inputContext = renderer.GetWindow()!.CreateInput();
        controller = new ImGuiController((GL)renderer.GetApi()!, renderer.GetWindow()!, inputContext);
        
        State = ImGuiState.Loaded;
    }

    public void StartRenderTick(float delta)
    {
        // Throw an exception if the manager is not loaded yet
        IsLoaded(true);
        
        controller.Update(delta);
    }

    public void RenderTick(float delta)
    {
        if (IsBusy(true)) return;
        
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("View"))
            {
                if (ImGui.MenuItem("Console"))
                {
                    AddViewport<ConsoleViewport>();
                }
                if (ImGui.MenuItem("Stats"))
                {
                    AddViewport<StatsViewport>();
                }
            }
        }
        
        foreach (IViewport viewport in viewports.Where(x => x.ToRender()).ToArray())
        {
            viewport.Render(delta);
        }
    }
    
    public void EndRenderTick(float delta)
    {
        // Throw an exception if the manager is not loaded yet
        IsBusy(true);
        
        controller.Render();
    }

    public void Dispose()
    {
        if (!IsLoaded()) return;

        controller.Dispose();
        inputContext.Dispose();

        State = ImGuiState.Disposed;
    }

    public IViewport AddViewport<T>() where T : IViewport
    {
        var viewport = (IViewport)Activator.CreateInstance(typeof(T), this)!;
        viewports.Add(viewport);
        return viewport;
    }
    public void ClearViewport(IViewport viewport)
    {
        State = ImGuiState.Busy;
        viewports.Remove(viewport);
        State = ImGuiState.Loaded;
    }

    private bool IsLoaded(bool throwException = false)
    {
        bool isLoaded = State == ImGuiState.Loaded;
        if (!isLoaded && throwException) throw new InvalidOperationException($"{nameof(ImGuiController)} not loaded");
        return isLoaded;
    }

    private bool IsBusy(bool throwException = false)
    {
        IsLoaded(throwException);
        return State == ImGuiState.Busy;
    }
}