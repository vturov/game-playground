using Game.Contracts;
using Game.Objects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Reflection;

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
        services
           .AddSingleton<IObjectManager, ObjectManager>()
           .AddSingleton<IObjectComponentManager, ObjectComponentManager>()
           .AddSingleton<IObjectComponentFactory, ObjectComponentFactory>();

        var handlerBaseType = typeof(IObjectComponentCreationHandler<>);
        var typeToRegister = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(handlerType => handlerType.IsAbstract is false)
            .Select(handlerType =>
                new
                {
                    HandlerType = handlerType,
                    ImplementedInterfaces =
                        handlerType
                            .GetInterfaces()
                            .Where(baseType => baseType.IsGenericType &&
                                               baseType.GetGenericTypeDefinition() == handlerBaseType)
                });

        foreach (var type in typeToRegister)
        {
            services.AddSingleton(type.HandlerType);

            foreach (var implementedInterface in type.ImplementedInterfaces)
                services.AddSingleton(implementedInterface, provider => provider.GetRequiredService(type.HandlerType));
        }

        return services;
    }

    private static IServiceCollection AddGameObjects(this IServiceCollection services)
    {
        var objectTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(x => !x.IsAbstract && x.IsAssignableTo(typeof(IObject)));

        foreach (var type in objectTypes)
            services.AddTransient(type);

        return services;
    }
}