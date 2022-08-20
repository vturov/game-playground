using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silk.NET.Windowing;

namespace Game
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationCore(this IServiceCollection services)
        {
            return services
                .AddSingleton<IHostedService, ApplicationHost>()
                .AddSingleton(Window.Create(WindowOptions.Default))
                .AddSingleton<IRenderer, Renderer>()
                .AddScoped<Scene>();
        }
    }
}
