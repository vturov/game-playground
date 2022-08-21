using Game.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Game;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        return services
            .AddSingleton<IHostedService, ApplicationHost>()
            .AddSingleton<IGame, Game>()
            .AddSingleton(Window.Create(WindowOptions.Default))
            .AddSingleton(provider => provider.GetRequiredService<IWindow>().CreateOpenGL())
            .AddSingleton<ISceneManager, SceneManager>()
            .AddSingleton<ISceneDrawer, SceneDrawer>()
            .AddSingleton<Func<ISceneDrawer>>(provider => provider.GetRequiredService<ISceneDrawer>);
    }
}