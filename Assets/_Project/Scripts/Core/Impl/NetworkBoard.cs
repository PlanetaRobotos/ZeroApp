using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Data;
using _Project.Scripts.Models;
using _Project.Scripts.Windows.HUD;
using Fusion;
using Logging;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class NetworkBoard : NetworkBehaviour, IBoard
    {
        private BoardWidgetData _boardWidgetData;

        [Inject] private ICustomLogger _logger;
        [Inject] private IGameRules _gameRules;
        [Inject] private IBoardFactory _boardFactory;
        [Inject] private IPlayerProfileProvider _playerProvider;
        [Inject] private IGameTracker _gameTracker;

        private int _gridSize;
        private IGameplayMediator _gameplayMediator;

        private NetworkBool NetworkIsInteract { get; set; }

        [Networked, OnChangedRender(nameof(GridChanged))]
        private BoardCell NetworkCell { get; set; }

        public SymbolType[,] Grid { get; private set; }

        public void Initialize(BoardWidgetData boardWidgetData, IGameplayMediator gameplayMediator)
        {
            _gameplayMediator = gameplayMediator;
            _boardWidgetData = boardWidgetData;
            Grid = _boardFactory.CreateGrid();
            _boardWidgetData.OnBoardChanged?.Invoke(Grid);
            Reset();
        }

        public void MakeMove(BoardCell cell) =>
            MakeMoveRpc(cell);

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        private void MakeMoveRpc(BoardCell cell)
        {
            Debug.Log($"MakeMoveRpc called {cell} - {_playerProvider}");
            NetworkCell = cell;
        }

        public void SetInteract(bool isInteractable) => 
            SetInteractRpc(isInteractable);

        public void Initialize(BoardWidgetData boardWidgetData)
        {
            throw new System.NotImplementedException();
        }

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        private void SetInteractRpc(NetworkBool isInteractable)
        {
            Debug.Log($"Board changed isInteractable {isInteractable}");
            IsInteractive = isInteractable;
        }

        private void GridChanged()
        {
            if (HasStateAuthority)
            {
                _logger.Log($"Grid changed from server {Grid}");
                PlaceSymbol(NetworkCell.Row, NetworkCell.Column, NetworkCell.Symbol, out _);
            }
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
                SetInteractiveBoth(false);
                result = ResultType.Win;
            }

            if (_gameRules.CheckDraw(this))
            {
                Debug.Log("Draw");
                _boardWidgetData.OnDraw?.Invoke();
                SetInteractiveBoth(false);
                result = ResultType.Draw;
            }

            _boardWidgetData.OnBoardChanged?.Invoke(Grid);

            if (symbol == _playerProvider.Symbol)
            {
                IsInteractive = false;
                _gameplayMediator.GetOtherPlayer.Board.SetInteract(true);
            }
        }

        private void SetInteractiveBoth(bool isInteractable)
        {
            IsInteractive = isInteractable;
            _gameplayMediator.GetOtherPlayer.Board.SetInteract(isInteractable);
        }

        public bool IsCellEmpty(int row, int column) =>
            Grid.IsCellEmpty(row, column);

        public bool IsFull() => Grid.IsFull();

        public void Reset() => this.ResetGrid();

        public bool IsInteractive
        {
            get => NetworkIsInteract;
            set
            {
                _logger.Log($"IsInteractive changed to {value}");

                _boardWidgetData.OnInteractiveChanged?.Invoke(value);
                _boardWidgetData.OnPlayerTurn?.Invoke(value);

                NetworkIsInteract = value;
            }
        }
    }
}