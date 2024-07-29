using System.Collections.Generic;
using _Project.Scripts.Core.Abstract;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public static class BoardOperationsExtensions
    {
        public static bool IsFull(this SymbolType[,] grid)
        {
            foreach (var cell in grid)
                if (cell == SymbolType.None)
                    return false;

            return true;
        }

        public static bool GetRandomEmptyCell(this SymbolType[,] grid, out Vector2Int cell)
        {
            cell = new Vector2Int(-1, -1);
            List<Vector2Int> emptyCells = new();

            for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
                if (grid[i, j] == SymbolType.None)
                    emptyCells.Add(new Vector2Int(i, j));

            if (emptyCells.Count == 0)
                return false;

            int randomIndex = Random.Range(0, emptyCells.Count);
            cell = emptyCells[randomIndex];
            return true;
        }

        public static void ResetGrid(this IBoard board)
        {
            for (int i = 0; i < board.Grid.GetLength(0); i++)
            for (int j = 0; j < board.Grid.GetLength(1); j++)
                board.Grid[i, j] = SymbolType.None;
        }

        public static bool IsCellEmpty(this SymbolType[,] grid, int row, int column) =>
            grid[row, column] == SymbolType.None;
    }
}