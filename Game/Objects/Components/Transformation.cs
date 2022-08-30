using System.Numerics;
using Game.Contracts;

namespace Game.Objects.Components;

internal sealed class Transformation : IObjectComponent
{
    public Vector3 Position { get; set; }
}