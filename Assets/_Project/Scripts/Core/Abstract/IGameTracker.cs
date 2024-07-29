namespace _Project.Scripts.Core.Abstract
{
    public interface IGameTracker
    {
        void RecordWin();
        void ResetWinsAmount();
        void InitializeWins();
    }
}