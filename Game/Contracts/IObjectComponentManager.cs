namespace Game.Contracts;

internal interface IObjectComponentManager
{
    IEnumerable<IObjectComponent> Components { get; }

    T Create<T>(IObject owner) where T : class, IObjectComponent, new();
}