using Silk.NET.Windowing;

namespace Game
{
    internal sealed class Game : IGame
    {
        private readonly IWindow window;
        private readonly ISceneManager sceneManager;
        private readonly Func<ISceneDrawer> createSceneDrawer;

        public Game(IWindow window, ISceneManager sceneManager, Func<ISceneDrawer> createSceneDrawer)
        {
            this.window = window;
            this.sceneManager = sceneManager;
            this.createSceneDrawer = createSceneDrawer;

            window.Closing += OnWindowClosing;
            window.Load += OnWindowLoaded;
        }

        public event Action? Exited;

        public void Start()
        {
            Task.Run(() => window.Run());
        }

        public void Shutdown()
        {
            if (window.IsClosing)
                return;

            window.Close();
        }

        private void OnWindowClosing()
        {
            window.Closing -= OnWindowClosing;

            Exited?.Invoke();
        }

        private void OnWindowLoaded()
        {
            window.Load -= OnWindowLoaded;

            createSceneDrawer();
            sceneManager.Initialize();
        }
    }
}
