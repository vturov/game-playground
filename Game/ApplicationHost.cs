using Microsoft.Extensions.Hosting;
using Silk.NET.Windowing;

namespace Game
{
    internal sealed class ApplicationHost : IHostedService, IDisposable
    {
        private readonly IWindow window;

        public ApplicationHost(IWindow window)
        {
            this.window = window;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            window.Run();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            window.Dispose();
        }
    }
}
