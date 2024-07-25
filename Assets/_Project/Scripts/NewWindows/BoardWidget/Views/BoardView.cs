using System;
using _Project.Scripts.Core;
using _Project.Scripts.Core.Abstract;
using Logging;
using UnityEngine;
using UnityEngine.UI;
using CellView = _Project.Scripts.Windows.HUD.CellView;

namespace _Project.Scripts.Windows.HUD
{
    public class BoardView : MonoBehaviour, IDisposable
    {
        public CellView buttonPrefab;
        
        [SerializeField] private Transform cellsParent;
        [SerializeField] private Transform linesParent;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        private CellView[,] _cells;
        private ICustomLogger _logger;
        
        // [Inject] private IMover _mover;
        [Inject] private NetworkGameManager _networkManager;
        [Inject] private IPlayerProvider _playerProvider;
        [Inject] private BoardFactory _boardFactory;

        public void Construct(ICustomLogger logger)
        {
            _logger = logger;
        }

        public void Initialize(int colsAmount, int rowsAmount)
        {
            _cells = new CellView[colsAmount, rowsAmount];
            CreateBoard(colsAmount, rowsAmount);
            _boardFactory.GenerateLines(colsAmount, rowsAmount, _gridLayoutGroup, linesParent, 2);
        }

        private void CreateBoard(int colsAmount, int rowsAmount)
        {
            for (int i = 0; i < colsAmount; i++)
            {
                for (int j = 0; j < rowsAmount; j++)
                {
                    CellView cell = _boardFactory.CreateCell(buttonPrefab, cellsParent, SymbolType.None);
                    _cells[i, j] = cell;
                    int x = i, y = j;
                    _cells[i, j].Subscribe(() => OnButtonClicked(x, y));
                }
            }
        }

        private void OnButtonClicked(int x, int y)
        {
            // Notify the controller about the button click
            _networkManager.TryMakeMove(x, y);
        }

        public void UpdateBoard(SymbolType[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var symbolType = grid[i, j];
                    bool isSpriteFound = _boardFactory.GetSpriteByType(symbolType);
                    if (isSpriteFound)
                    {
                        _cells[i, j].SetSymbolSprite(_boardFactory.GetSpriteByType(symbolType), symbolType);
                    }
                    else _logger.LogError("Sprite not found for symbol type: " + symbolType);
                }
            }
        }
        
        public void UpdateCell(SymbolType symbol, int row, int column)
        {
            _cells[row, column].SetSymbolSprite(_boardFactory.GetSpriteByType(symbol), symbol);
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