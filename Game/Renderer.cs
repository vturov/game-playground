using System.Drawing;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Game
{
    internal sealed class Renderer : IRenderer
    {
        private IWindow? window;
        private GL? context;

        public void Initialize(IWindow window)
        {
            if (this.window is not null)
                throw new InvalidOperationException("Double initialization detected");

            this.window = window;
            this.context = window.CreateOpenGL();

            window.Render += OnWindowRender;
            window.Closing += OnWindowClosing;
        }

        private void OnWindowRender(double delta)
        {
            context.ClearColor(Color.CadetBlue);
            context.Clear(ClearBufferMask.ColorBufferBit);
        }

        private void OnWindowClosing()
        {
            window.Render -= OnWindowRender;
            window.Closing -= OnWindowClosing;
        }
    }
}
