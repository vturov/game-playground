namespace Game.Contracts.Objects;

internal interface IGameObjectManager
{
    ICollection<IGameObject> Objects { get; }
}