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
            .AddSingleton<IGame, Core.Game>()
            .AddSingleton<IWindow>(Window.Create(WindowOptions.Default))
            .AddSingleton<GL>(provider => provider.GetRequiredService<IWindow>().CreateOpenGL())
            .AddSingleton<SceneManager>()
            .AddSingleton<ISceneManager>(provider => provider.GetRequiredService<SceneManager>())
            .AddSingleton<ISceneProvider>(provider => provider.GetRequiredService<SceneManager>())
            .AddSingleton<ISceneDrawer, SceneDrawer>()
            .AddSingleton<Func<ISceneDrawer>>(provider => provider.GetRequiredService<ISceneDrawer>)
            .AddTransient<QuadScene>()
            .AddTransient<Func<QuadScene>>(provider => provider.GetRequiredService<QuadScene>)
            .AddTransient<Quad>()
            .AddTransient<ShaderProgram>();
    }
}