using Game.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Objects;

internal sealed class ObjectComponentManager : IObjectComponentManager
{
    private readonly IServiceProvider serviceProvider;
    private readonly IObjectComponentFactory objectComponentFactory;
    private readonly List<IObjectComponent> components;

    public ObjectComponentManager(IServiceProvider serviceProvider, IObjectComponentFactory objectComponentFactory)
    {
        this.serviceProvider = serviceProvider;
        this.objectComponentFactory = objectComponentFactory;

        components = new List<IObjectComponent>();
    }

    public IEnumerable<IObjectComponent> Components => components;

    public T Create<T>(IObject owner) where T : class, IObjectComponent, new()
    {
        var createdComponent = objectComponentFactory.Create<T>();
        var handlers = serviceProvider.GetServices<IObjectComponentCreationHandler<T>>();
        foreach (var creationHandler in handlers)
        {
            creationHandler.Handle(owner, createdComponent);
        }

        components.Add(createdComponent);
        return createdComponent;
    }
}