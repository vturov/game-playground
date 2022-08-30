using Game.Contracts;
using Microsoft.Extensions.ObjectPool;

namespace Game.Objects;

internal sealed class ObjectComponentFactory : IObjectComponentFactory
{
    private readonly Dictionary<Type, object> objectPools;

    public ObjectComponentFactory()
    {
        objectPools = new Dictionary<Type, object>();
    }

    public T Create<T>() where T : class, IObjectComponent, new()
    {
        if (objectPools.ContainsKey(typeof(T)) is false)
            objectPools.Add(typeof(T), new DefaultObjectPool<T>(new DefaultPooledObjectPolicy<T>()));

        var objectPool = objectPools[typeof(T)] as ObjectPool<T>;
        var createdComponent = objectPool!.Get();

        return createdComponent;
    }
}