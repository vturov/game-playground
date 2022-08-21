using Microsoft.Extensions.Hosting;

namespace Game;

internal class Program
{
    private static void Main(string[] args)
    {
        using var host = Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((_, services) => services.AddApplicationCore())
            .Build();

        host.Run();
    }
}