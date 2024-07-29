namespace _Project.Core.Providers
{
    public interface IGameStateProvider
    {
        void StartGame();
        void EndGame();
        void ResetGame();
    }
}