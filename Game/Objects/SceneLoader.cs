using Game.Contracts;
using Microsoft.Extensions.Configuration;

namespace Game.Objects;

internal class SceneLoader : IObject
{
    public SceneLoader(IConfiguration configuration, IObjectManager objectManager)
    {
        objectManager.RemoveAll();

        var scene = configuration["scene"];
        if (scene == "quad")
        {
        }

        objectManager.Create<EscapeHandler>();
    }
}