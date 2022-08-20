using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace Game
{
    internal class Application : IDisposable
    {
        private IWindow? window;

        public void Run(string[] cmdLineArgs)
        {
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(1024, 768);

            window = Window.Create(options);
            window.Run();
        }

        public void Dispose()
        {
            window?.Dispose();
        }
    }
}
