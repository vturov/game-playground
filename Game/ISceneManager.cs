namespace Game
{
    internal interface ISceneManager
    {
        void Initialize();

        public event Action<IScene> SceneChanged;
    }
}