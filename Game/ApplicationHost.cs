using Microsoft.Extensions.Hosting;
using Silk.NET.Windowing;

namespace Game
{
    internal sealed class ApplicationHost : IHostedService, IDisposable
    {
        private readonly IWindow window;
        private readonly IRenderer renderer;

        public ApplicationHost(IWindow window, IRenderer renderer)
        {
            this.window = window;
            this.renderer = renderer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            window.Load += OnWindowLoaded;
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

        private void OnWindowLoaded()
        {
            window.Load -= OnWindowLoaded;
            renderer.Initialize(window);
        }
    }
}
