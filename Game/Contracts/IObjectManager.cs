namespace Game.Contracts;

internal interface IObjectManager
{
    IObject Create<T>() where T : IObject;
    void RemoveAll();
}