using _Project.Core;
using _Project.Core.Boards;
using _Project.Models;
using _Project.Models.Boards;
using _Project.Windows.BoardWidget.Factories;
using Cysharp.Threading.Tasks;
using Galaxy4Games.Utils;
using Logging;
using UnityEngine;

namespace _Project.AI
{
    public class AIBoard : IBoard
    {
        private readonly MinMaxValue MinMaxAISecDelay = new(1, 2.5f);
        [Inject] private IBoardFactory _boardFactory;

        private BoardWidgetData _boardWidgetData;
        [Inject] private IGameRules _gameRules;
        [Inject] private IGameTracker _gameTracker;

        private int _gridSize;
        private bool _isInteract;

        [Inject] private ICustomLogger _logger;
        [Inject] private IPlayerProfileProvider _playerProvider;

        public SymbolType[,] Grid { get; private set; }

        public void Initialize(BoardWidgetData boardWidgetData, IGameplayMediator gameplayMediator)
        {
            _boardWidgetData = boardWidgetData;
            Grid = _boardFactory.CreateGrid();
            _boardWidgetData.OnBoardChanged?.Invoke(Grid);
            Reset();
        }

        public void MakeMove(BoardCell cell)
        {
            PlaceSymbol(cell.Row, cell.Column, cell.Symbol, out ResultType result);

            if (result is ResultType.None)
                FakeAIMove().Forget();
        }

        public void SetInteract(bool isInteractable)
        {
            IsInteractive = isInteractable;
        }

        public void PlaceSymbol(int row, int column, SymbolType symbol, out ResultType result)
        {
            result = ResultType.None;

            if (!IsCellEmpty(row, column))
            {
                Debug.Log($"Cell {row} - {column} is not empty");
                return;
            }

            Grid[row, column] = symbol;

            if (_gameRules.CheckWin(Grid, symbol))
            {
                Debug.Log($"Player {symbol} wins");

                _gameTracker.RecordWin();

                _boardWidgetData.OnPlayerWin?.Invoke(symbol);
                IsInteractive = false;
                result = ResultType.Win;
            }

            if (_gameRules.CheckDraw(this))
            {
                Debug.Log("Draw");
                _boardWidgetData.OnDraw?.Invoke();
                IsInteractive = false;
                result = ResultType.Draw;
            }

            _boardWidgetData.OnBoardChanged?.Invoke(Grid);

            if (symbol == _playerProvider.Symbol) IsInteractive = false;
        }

        public bool IsCellEmpty(int row, int column)
        {
            return Grid.IsCellEmpty(row, column);
        }

        public bool IsFull()
        {
            return Grid.IsFull();
        }

        public void Reset()
        {
            this.ResetGrid();
        }

        public bool IsInteractive
        {
            get => _isInteract;
            set
            {
                _logger.Log($"IsInteractive changed to {value}");

                _boardWidgetData.OnInteractiveChanged?.Invoke(value);
                _boardWidgetData.OnPlayerTurn?.Invoke(value);

                _isInteract = value;
            }
        }

        private async UniTask FakeAIMove()
        {
            await UniTask.Delay((int)Random.Range(MinMaxAISecDelay.min, MinMaxAISecDelay.max) * 1000);

            bool isEmptyCellFound = Grid.GetRandomEmptyCell(out Vector2Int cell);
            int row = cell.x;
            int column = cell.y;
            SymbolType symbol = _playerProvider.Symbol == SymbolType.Cross ? SymbolType.Circle : SymbolType.Cross;

            if (isEmptyCellFound)
            {
                if (!IsCellEmpty(row, column))
                {
                    Debug.Log($"Cell {row} - {column} is not empty");
                    return;
                }

                Grid[row, column] = symbol;

                if (_gameRules.CheckWin(Grid, symbol))
                {
                    Debug.Log($"Player {symbol} wins");

                    _boardWidgetData.OnPlayerWin?.Invoke(symbol);
                    IsInteractive = false;
                }

                if (_gameRules.CheckDraw(this))
                {
                    Debug.Log("Draw");
                    _boardWidgetData.OnDraw?.Invoke();
                    IsInteractive = false;
                }

                _boardWidgetData.OnBoardChanged?.Invoke(Grid);

                IsInteractive = true;
            }
            else
            {
                _logger.LogError("No empty cell found");
            }
        }
    }
}