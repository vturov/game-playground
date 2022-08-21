using Microsoft.Extensions.Hosting;

namespace Game
{
    internal sealed class ApplicationHost : IHostedService
    {
        public ApplicationHost(IGame game, IHostApplicationLifetime lifetime)
        {
            game.Exited += lifetime.StopApplication;

            lifetime.ApplicationStarted.Register(game.Start);
            lifetime.ApplicationStopping.Register(game.Shutdown);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
