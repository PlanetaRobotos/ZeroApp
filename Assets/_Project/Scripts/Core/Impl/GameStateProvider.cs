namespace _Project.Scripts.Core
{
    public interface IGameStateProvider
    {
        void StartGame();
        void EndGame();
        void ResetGame();
    }
}