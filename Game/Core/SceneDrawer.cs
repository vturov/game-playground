using Silk.NET.Windowing;

namespace Game.Core;

internal sealed class SceneDrawer : ISceneDrawer, IDisposable
{
    private readonly IWindow window;
    private readonly ISceneProvider sceneProvider;

    public SceneDrawer(IWindow window, ISceneProvider sceneProvider)
    {
        this.window = window;
        this.sceneProvider = sceneProvider;

        window.Render += OnWindowRendered;
    }

    private void OnWindowRendered(double delta)
    {
        var scene = sceneProvider.Scene;
        if (scene is null)
            return;

        foreach (var drawableObject in scene.Objects.OfType<IDrawableGameObject>())
        {
            drawableObject.Draw(delta);
        }
    }

    public void Dispose()
    {
        window.Render -= OnWindowRendered;
    }
}