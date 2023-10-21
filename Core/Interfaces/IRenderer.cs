using Silk.NET.Core.Native;
using Silk.NET.Windowing;

namespace Renderer.Interfaces;

public interface IRenderer
{
    public IWindow? GetWindow();
    public NativeAPI? GetApi();
    
    public void Initialize();
    public void Show();
    public void Dispose();
}