using Game.Contracts;
using Game.Objects.Components;
using Silk.NET.Input;

namespace Game.Handlers;

internal sealed class InputComponentsHandler : IObjectComponentCreationHandler<KeyboardInput>
{
    private readonly IInputContext inputContext;
    private readonly IObjectComponentManager componentManager;

    private bool subscribedToEvents;

    public InputComponentsHandler(IInputContext inputContext, IObjectComponentManager componentManager)
    {
        this.inputContext = inputContext;
        this.componentManager = componentManager;
    }

    public void Handle(IObject owner, KeyboardInput component)
    {
        if (subscribedToEvents)
            return;

        inputContext.Keyboards.Single().KeyDown += OnKeyDown;
        inputContext.Keyboards.Single().KeyUp += OnKeyUp;
        inputContext.Keyboards.Single().KeyChar += OnKeyChar;

        subscribedToEvents = true;
    }

    private void OnKeyDown(IKeyboard _, Key key, int code)
    {
        foreach (var component in componentManager.Components.OfType<KeyboardInput>())
        {
            component.OnKeyDown?.Invoke(key, code);
        }
    }

    private void OnKeyUp(IKeyboard _, Key key, int code)
    {
        foreach (var component in componentManager.Components.OfType<KeyboardInput>())
        {
            component.OnKeyUp?.Invoke(key, code);
        }
    }

    private void OnKeyChar(IKeyboard _, char character)
    {
        foreach (var component in componentManager.Components.OfType<KeyboardInput>())
        {
            component.OnKeyChar?.Invoke(character);
        }
    }
}