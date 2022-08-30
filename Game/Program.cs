using Game.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Game;

internal class Program
{
    private static void Main(string[] args)
    {
        using var host = Host
            .CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(x => x.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "content")))
            .ConfigureServices((_, serviceCollection) => serviceCollection.AddApplicationCore())
            .Build();

        host.Run();
    }
}