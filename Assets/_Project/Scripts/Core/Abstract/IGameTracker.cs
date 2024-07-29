namespace _Project.Core
{
    public interface IGameTracker
    {
        void RecordWin();
        void ResetWinsAmount();
        void InitializeWins();
    }
}