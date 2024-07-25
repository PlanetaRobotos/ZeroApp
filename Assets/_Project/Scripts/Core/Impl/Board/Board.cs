using System;
using _Project.Scripts.Core.Abstract;
using Fusion;

namespace _Project.Scripts.Core
{
    public class Board : NetworkBehaviour, IBoard
    {
        [Inject] private BoardData _boardData;

        private SymbolType[,] Grid { get; }

        public Board()
        {
            Grid = new SymbolType[3, 3];
            _boardData.OnBoardChanged?.Invoke(Grid);
            Reset();
        }

        public bool PlaceSymbol(int row, int column, SymbolType symbol)
        {
            if (IsCellEmpty(row, column))
            {
                Grid[row, column] = symbol;
                _boardData.OnBoardCellChanged?.Invoke(symbol, row, column);
                return true;
            }

            return false;
        }

        public bool IsCellEmpty(int row, int column)
        {
            return Grid[row, column] == SymbolType.None;
        }

        public bool IsFull()
        {
            foreach (var cell in Grid)
            {
                if (cell == SymbolType.None)
                    return false;
            }

            return true;
        }

        public void Reset()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Grid[i, j] = SymbolType.None;
                }
            }
        }
    }
}