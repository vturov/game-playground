using Silk.NET.OpenGL;

namespace Game
{
    internal sealed class SceneDrawer : ISceneDrawer
    {
        private GL context;

        public SceneDrawer(GL context)
        {
            this.context = context;
        }
    }
}