namespace Game.Core;

internal sealed class QuadScene : IScene
{
    private readonly List<IGameObject> objects;

    public QuadScene(Quad quad)
    {
        objects = new List<IGameObject> { quad };
    }

    public IEnumerable<IGameObject> Objects => objects;
}