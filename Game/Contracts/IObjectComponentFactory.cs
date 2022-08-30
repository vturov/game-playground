namespace Game.Contracts;

internal interface IObjectComponentFactory
{
    T Create<T>() where T : class, IObjectComponent, new();
}