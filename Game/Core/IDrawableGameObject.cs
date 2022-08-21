namespace Game.Core;

internal interface IDrawableGameObject : IGameObject
{
    void Draw(double delta);
}