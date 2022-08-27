using Game.Contracts;
using Silk.NET.Input;

namespace Game.Game;

internal sealed class EscapeHandler : IGameComponent, IInitializable
{
    private readonly IInputContext inputContext;
    private readonly IGame game;

    public EscapeHandler(IInputContext inputContext, IGame game)
    {
        this.inputContext = inputContext;
        this.game = game;
    }

    public void Initialize()
    {
        inputContext.Keyboards.First().KeyUp += OnKeyUp;
    }

    private void OnKeyUp(IKeyboard _, Key key, int code)
    {
        if (key == Key.Escape)
            game.Shutdown();
    }
}