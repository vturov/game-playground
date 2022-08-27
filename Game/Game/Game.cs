using Game.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Silk.NET.Windowing;

namespace Game.Game;

internal sealed class Game : IGame
{
    private readonly IWindow window;
    private readonly IServiceProvider serviceProvider;

    public Game(IWindow window, IServiceProvider serviceProvider)
    {
        this.window = window;
        this.serviceProvider = serviceProvider;

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

    private void OnWindowLoaded()
    {
        window.Load -= OnWindowLoaded;

        var components = serviceProvider.GetServices<IGameComponent>();
        foreach (var component in components.OfType<IInitializable>())
            component.Initialize();
    }

    private void OnWindowClosing()
    {
        window.Closing -= OnWindowClosing;

        Exited?.Invoke();
    }
}