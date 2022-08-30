using Game.Contracts;
using Game.Objects.Components;

namespace Game.Objects.old;

internal sealed class Quad : IObject
{
    public Quad(IObjectComponentManager factory)
    {
        Components = new List<IObjectComponent>
        {
            factory.Create<Transformation>(this),
            factory.Create<GeometryShape>(this)
        };

        var geometry = Components.OfType<GeometryShape>().Single();
        geometry.Type = GeometryType.Quad;
    }

    public IReadOnlyCollection<IObjectComponent> Components { get; }
}