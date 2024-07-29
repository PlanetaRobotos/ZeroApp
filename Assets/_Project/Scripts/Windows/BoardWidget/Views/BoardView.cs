using System;
using _Project.Models;
using _Project.Windows.BoardWidget.Factories;
using Logging;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Windows.BoardWidget.Views
{
    public class BoardView : MonoBehaviour, IDisposable
    {
        [SerializeField] private Transform cellsParent;
        [SerializeField] private Transform linesParent;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        [Inject] private IBoardFactory _boardFactory;
        private BoardWindow _boardWindow;

        private CellView[,] _cells;
        private ICustomLogger _logger;

        public void Dispose()
        {
            foreach (CellView cell in _cells) cell.Unsubscribe();
        }

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
                SymbolType symbolType = grid[i, j];
                bool isSpriteFound = _boardFactory.GetSpriteByType(symbolType);
                if (isSpriteFound)
                    _cells[i, j].SetSymbolSprite(_boardFactory.GetSpriteByType(symbolType), symbolType);
                else _logger.LogError("Sprite not found for symbol type: " + symbolType);
            });
        }

        public void UpdateCell(SymbolType symbol, int row, int column)
        {
            _cells[row, column].SetSymbolSprite(_boardFactory.GetSpriteByType(symbol), symbol);
        }

        private void IterateCells(Action<int, int> action)
        {
            for (var i = 0; i < _cells.GetLength(0); i++)
            for (var j = 0; j < _cells.GetLength(1); j++)
                action(i, j);
        }
    }
}