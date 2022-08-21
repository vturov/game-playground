namespace Game.Core;

internal interface ISceneManager
{
    void Initialize();

    public event Action<IScene> SceneChanged;
}