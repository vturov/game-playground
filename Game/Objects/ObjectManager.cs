using Game.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Objects;

internal sealed class ObjectManager : IObjectManager
{
    private readonly IServiceProvider serviceProvider;
    private readonly List<IObject> createdObjects;

    public ObjectManager(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;

        createdObjects = new List<IObject>();
    }

    public IObject Create<T>() where T : IObject
    {
        var obj = serviceProvider.GetRequiredService<T>();
        createdObjects.Add(obj);
        return obj;
    }

    public void RemoveAll()
    {
        createdObjects.Clear();
    }
}