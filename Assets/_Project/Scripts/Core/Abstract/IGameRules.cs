using _Project.Models;

namespace _Project.Core
{
    public interface IGameRules
    {
        bool CheckWin(SymbolType[,] grid, SymbolType symbol);
        bool CheckDraw(IBoard board);
    }
}