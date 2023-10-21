using Renderer.Interfaces;
using Renderer.Renderers.OpenGl;

namespace Renderer;

public class ApplicationBuilder
{
    private readonly IRenderer renderer = new OpenGlRenderer();

    public Application Build()
    {
        var app = new Application(renderer);

        return app;
    }
}