namespace _Project.Scripts.Core.Abstract
{
    public interface IGameRules
    {
        bool CheckWin(SymbolType[,] grid, SymbolType symbol);
        bool CheckDraw(IBoard board);
    }
}