using Game.Contracts;
using Game.Core;
using Game.Game;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Game;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        return services
            .AddGameCore()
            .AddGameComponents();
    }

    private static IServiceCollection AddGameCore(this IServiceCollection services)
    {
        return services
            .AddSingleton<IHostedService, ApplicationHost>()
            .AddSingleton<IGame, Game.Game>()
            .AddSingleton(Window.Create(WindowOptions.Default))
            .AddSingleton(provider => provider.GetRequiredService<IWindow>().CreateInput())
            .AddSingleton(provider => provider.GetRequiredService<IWindow>().CreateOpenGL());
    }

    private static IServiceCollection AddGameComponents(this IServiceCollection services)
    {
        return services
            .AddSingleton<IGameComponent, SceneDrawer>()
            .AddSingleton<IGameComponent, EscapeHandler>();
    }
}