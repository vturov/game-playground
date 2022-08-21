namespace Game.Core;

internal interface IScene
{
    IEnumerable<IGameObject> Objects { get; }
}