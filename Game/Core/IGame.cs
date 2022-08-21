namespace Game.Core;

internal interface IGame
{
    event Action Exited;

    void Start();
    void Shutdown();
}