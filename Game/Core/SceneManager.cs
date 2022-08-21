namespace Game.Core;

internal sealed class SceneManager : ISceneManager, ISceneProvider
{
    private readonly Func<QuadScene> createQuadScene;

    public SceneManager(Func<QuadScene> createQuadScene)
    {
        this.createQuadScene = createQuadScene;
    }

    public IScene? Scene { get; private set; }

    public void Initialize()
    {
        Scene = createQuadScene();
    }
}