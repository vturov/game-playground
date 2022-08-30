using Game.Contracts;
using Game.Objects.Components;
using Silk.NET.Input;

namespace Game.Objects;

internal sealed class EscapeHandler : IObject
{
    private readonly IGame game;

    public EscapeHandler(IGame game, IObjectComponentManager componentManager)
    {
        this.game = game;

        var input = componentManager.Create<KeyboardInput>(this);
        input.OnKeyDown = OnKeyDown;
    }

    private void OnKeyDown(Key key, int code)
    {
        if (key == Key.Escape)
            game.Shutdown();
    }
}