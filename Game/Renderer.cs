using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Game
{
    internal sealed class Renderer : IRenderer
    {
        private IWindow? window;
        private GL? context;

        private readonly Scene scene;

        public Renderer(Scene scene)
        {
            this.scene = scene;
        }

        public void Initialize(IWindow window)
        {
            if (this.window is not null)
                throw new InvalidOperationException("Double initialization detected");

            this.window = window;
            context = window.CreateOpenGL();
            scene.Initialize(context);

            window.Render += OnWindowRender;
            window.Closing += OnWindowClosing;
        }

        private void OnWindowRender(double delta)
        {
            scene.Draw(context!);
        }

        private void OnWindowClosing()
        {
            window!.Render -= OnWindowRender;
            window.Closing -= OnWindowClosing;
        }
    }
}
