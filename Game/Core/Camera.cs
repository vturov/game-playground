using System.Numerics;

namespace Game.Core;

internal sealed class Camera : IGameObject
{
    public Camera()
    {
        Position = new Vector3(0, 10, 10);
        Target = Vector3.Zero;

        var direction = Target - Position;
        direction = Vector3.Normalize(direction);
        Up = Vector3.Cross(direction, Vector3.UnitX);
    }

    public Vector3 Position { get; private set; }
    public Vector3 Target { get; private set; }
    public Vector3 Up { get; private set; }
}