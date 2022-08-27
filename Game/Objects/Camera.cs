using Game.Contracts.Objects;
using Silk.NET.Input;
using System.Numerics;

namespace Game.Core;

internal sealed class Camera : IGameObject
{
    private readonly IInputContext input;

    public Camera(IInputContext input)
    {
        this.input = input;
        input.Keyboards.First().KeyDown += OnKeyDown;

        Position = new Vector3(0, 0, 10);
        Target = Vector3.Zero;
        RecalculateUp();
    }

    public Vector3 Position { get; private set; }

    public Vector3 Target { get; private set; }

    public Vector3 Up { get; private set; }

    private void OnKeyDown(IKeyboard keyboard, Key key, int arg3)
    {
        var addition = Vector3.Zero;
        
        if (key == Key.W)
            addition.Z = -1;
        else if (key == Key.S)
            addition.Z = 1;

        if (key == Key.A)
            addition.X = -1;
        else if (key == Key.D)
            addition.X = 1;

        Position += addition;
        Target += addition;
        RecalculateUp();
    }

    private void RecalculateUp()
    {
        var direction = Target - Position;
        direction = Vector3.Normalize(direction);
        Up = Vector3.Cross(direction, Vector3.UnitX);
    }
}