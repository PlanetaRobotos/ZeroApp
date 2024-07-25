namespace _Project.Scripts.Core.Abstract
{
    public interface IGameRules
    {
        bool CheckWin(IBoard board, PlayerProfile player);
        bool CheckDraw(IBoard board);
    }
}