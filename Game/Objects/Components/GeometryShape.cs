using Game.Contracts;

namespace Game.Objects.Components;

internal sealed class GeometryShape : IObjectComponent
{
    public GeometryType Type { get; set; }
}