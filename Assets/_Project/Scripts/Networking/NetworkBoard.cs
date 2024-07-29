using System;
using _Project.Core;
using _Project.Core.Boards;
using _Project.Models;
using _Project.Models.Boards;
using _Project.Windows.BoardWidget.Factories;
using Fusion;
using Logging;
using UnityEngine;

namespace _Project.Networking
{
    public class NetworkBoard : NetworkBehaviour, IBoard
    {
        [Inject] private IBoardFactory _boardFactory;
        private BoardWidgetData _boardWidgetData;
        private IGameplayMediator _gameplayMediator;
        [Inject] private IGameRules _gameRules;
        [Inject] private IGameTracker _gameTracker;

        private int _gridSize;

        [Inject] private ICustomLogger _logger;
        [Inject] private IPlayerProfileProvider _playerProvider;

        private NetworkBool NetworkIsInteract { get; set; }

        [Networked]
        [OnChangedRender(nameof(GridChanged))]
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

        public void MakeMove(BoardCell cell)
        {
            MakeMoveRpc(cell);
        }

        public void SetInteract(bool isInteractable)
        {
            SetInteractRpc(isInteractable);
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
            get => NetworkIsInteract;
            set
            {
                _logger.Log($"IsInteractive changed to {value}");

                _boardWidgetData.OnInteractiveChanged?.Invoke(value);
                _boardWidgetData.OnPlayerTurn?.Invoke(value);

                NetworkIsInteract = value;
            }
        }

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        private void MakeMoveRpc(BoardCell cell)
        {
            Debug.Log($"MakeMoveRpc called {cell} - {_playerProvider}");
            NetworkCell = cell;
        }

        public void Initialize(BoardWidgetData boardWidgetData)
        {
            throw new NotImplementedException();
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

        private void SetInteractiveBoth(bool isInteractable)
        {
            IsInteractive = isInteractable;
            _gameplayMediator.GetOtherPlayer.Board.SetInteract(isInteractable);
        }
    }
}