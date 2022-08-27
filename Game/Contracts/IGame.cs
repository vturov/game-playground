namespace Game.Contracts;

internal interface IGame
{
    event Action Exited;

    void Start();
    void Shutdown();
}