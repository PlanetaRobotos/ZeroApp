using System;
using _Project.Scripts.Core;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Data;
using Logging;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Windows.HUD
{
    public class BoardView : MonoBehaviour, IDisposable
    {
        [SerializeField] private Transform cellsParent;
        [SerializeField] private Transform linesParent;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        private CellView[,] _cells;
        private ICustomLogger _logger;
        private BoardWindow _boardWindow;

        [Inject] private IBoardFactory _boardFactory;

        public void Construct(ICustomLogger logger)
        {
            _logger = logger;
        }

        public void Initialize(int boardSize)
        {
            _cells = new CellView[boardSize, boardSize];
        }

        public void GenerateLines(GameObject linePrefab)
        {
            _boardFactory.GenerateLines(_gridLayoutGroup, linesParent, linePrefab);
        }

        public void CreateBoard(CellView buttonPrefab, Action<int, int> onCellClicked)
        {
            IterateCells((i, j) =>
            {
                CellView cell = _boardFactory.CreateCell(buttonPrefab, cellsParent, SymbolType.None);
                _cells[i, j] = cell;
                int x = i, y = j;
                _cells[i, j].Subscribe(() => onCellClicked?.Invoke(x, y));
            });
        }

        public void UpdateBoard(SymbolType[,] grid)
        {
            IterateCells((i, j) =>
            {
                var symbolType = grid[i, j];
                bool isSpriteFound = _boardFactory.GetSpriteByType(symbolType);
                if (isSpriteFound)
                {
                    _cells[i, j].SetSymbolSprite(_boardFactory.GetSpriteByType(symbolType), symbolType);
                }
                else _logger.LogError("Sprite not found for symbol type: " + symbolType);
            });
        }

        public void UpdateCell(SymbolType symbol, int row, int column)
        {
            _cells[row, column].SetSymbolSprite(_boardFactory.GetSpriteByType(symbol), symbol);
        }

        private void IterateCells(Action<int, int> action)
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            for (int j = 0; j < _cells.GetLength(1); j++)
                action(i, j);
        }

        public void Dispose()
        {
            foreach (var cell in _cells)
            {
                cell.Unsubscribe();
            }
        }
    }
}