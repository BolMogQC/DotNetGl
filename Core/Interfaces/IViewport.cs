using Renderer.Renderers.OpenGl;

namespace Renderer.Interfaces;

public interface IViewport
{
    public bool ToRender();
    public void Show();
    public void Hide();
    public void Close();
    public void Render(float delta);
    public void Dispose();
}