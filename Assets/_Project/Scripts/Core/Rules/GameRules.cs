using _Project.Models;

namespace _Project.Core.Rules
{
    public class GameRules : IGameRules
    {
        private SymbolType[,] _grid;
        private int _gridSize;

        public bool CheckWin(SymbolType[,] grid, SymbolType symbol)
        {
            _grid = grid;
            _gridSize = grid.GetLength(0);

            // Check rows and columns
            for (var i = 0; i < _gridSize; i++)
                if (CheckRow(i, symbol) || CheckColumn(i, symbol))
                    return true;

            // Check diagonals
            if (CheckMainDiagonal(symbol) || CheckAntiDiagonal(symbol))
                return true;


            return false;
        }

        public bool CheckDraw(IBoard board)
        {
            return board.IsFull() && !CheckWin(board.Grid, SymbolType.Circle) &&
                   !CheckWin(board.Grid, SymbolType.Cross);
        }

        private bool CheckRow(int row, SymbolType symbol)
        {
            for (var col = 0; col < _gridSize; col++)
                if (_grid[row, col] != symbol)
                    return false;
            return true;
        }

        private bool CheckColumn(int col, SymbolType symbol)
        {
            for (var row = 0; row < _gridSize; row++)
                if (_grid[row, col] != symbol)
                    return false;
            return true;
        }

        private bool CheckMainDiagonal(SymbolType symbol)
        {
            for (var i = 0; i < _gridSize; i++)
                if (_grid[i, i] != symbol)
                    return false;
            return true;
        }

        private bool CheckAntiDiagonal(SymbolType symbol)
        {
            for (var i = 0; i < _gridSize; i++)
                if (_grid[i, _gridSize - 1 - i] != symbol)
                    return false;
            return true;
        }
    }
}