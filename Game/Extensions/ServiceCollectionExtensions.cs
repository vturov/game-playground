using Game.Contracts;
using Game.Handlers;
using Game.Objects;
using Game.Objects.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Game.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        return services
            .AddGameCore()
            .AddGameObjectManagement()
            .AddGameObjects();
    }

    private static IServiceCollection AddGameCore(this IServiceCollection services)
    {
        return services
            .AddSingleton<IHostedService, ApplicationHost>()
            .AddSingleton<IGame, Game>()
            .AddSingleton(Window.Create(WindowOptions.Default))
            .AddSingleton(provider => provider.GetRequiredService<IWindow>().CreateInput())
            .AddSingleton(provider => provider.GetRequiredService<IWindow>().CreateOpenGL());
    }
    private static IServiceCollection AddGameObjectManagement(this IServiceCollection services)
    {
        return services
            .AddSingleton<IObjectManager, ObjectManager>()
            .AddSingleton<IObjectComponentManager, ObjectComponentManager>()
            .AddSingleton<IObjectComponentFactory, ObjectComponentFactory>()
            .AddSingleton<IObjectComponentCreationHandler<KeyboardInput>, InputComponentsHandler>();
    }

    private static IServiceCollection AddGameObjects(this IServiceCollection services)
    {
        return services
            .AddTransient<EscapeHandler>()
            .AddTransient<SceneLoader>();
    }
}