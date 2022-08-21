namespace Game
{
    internal sealed class SceneManager : ISceneManager
    {
        public event Action<IScene>? SceneChanged;

        public void Initialize()
        {
        }
    }
}