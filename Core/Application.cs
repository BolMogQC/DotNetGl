using Renderer.Interfaces;

namespace Renderer;

public class Application
{
    private readonly IRenderer renderer;

    public Application(IRenderer renderer)
    {
        this.renderer = renderer;
    }

    public void Run()
    {
        renderer.Initialize();
        renderer.Show();
    }

    public void Dispose()
    {
        renderer.Dispose();
    }
}