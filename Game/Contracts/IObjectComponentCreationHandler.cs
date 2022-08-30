namespace Game.Contracts;

internal interface IObjectComponentCreationHandler<T> where T : IObjectComponent
{
    void Handle(IObject owner, T component);
}