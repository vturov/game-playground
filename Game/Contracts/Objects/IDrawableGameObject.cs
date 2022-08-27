namespace Game.Contracts.Objects;

internal interface IDrawableGameObject : IGameObject
{
    void Draw(double delta);
}