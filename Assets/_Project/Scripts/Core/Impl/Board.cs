using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Data;
using _Project.Scripts.Windows.HUD;
using Fusion;
using Logging;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class Board : NetworkBehaviour, IBoard
    {
        private BoardWidgetData _boardWidgetData;
        
        [Inject] private ICustomLogger _logger;
        [Inject] private IGameRules _gameRules;
        [Inject] private IBoardFactory _boardFactory;
        [Inject] private PhotonManager _photonManager;
        [Inject] private IPlayerProfileProvider _playerProvider;
        [Inject] private IGameTracker _gameTracker;

        private int _gridSize;

        private NetworkBool NetworkIsInteract { get; set; }

        [Networked, OnChangedRender(nameof(GridChanged))]
        private BoardCell NetworkCell { get; set; }

        public SymbolType[,] Grid { get; private set; }

        public void Initialize(BoardWidgetData boardWidgetData)
        {
            _boardWidgetData = boardWidgetData;
            
            Grid = _boardFactory.CreateGrid();
            _boardWidgetData.OnBoardChanged?.Invoke(Grid);
            Reset();
        }

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void MakeMoveRpc(BoardCell cell)
        {
            Debug.Log($"MakeMoveRpc called {cell} - {_playerProvider}");
            NetworkCell = cell;
        }

        private void GridChanged()
        {
            if (HasStateAuthority) 
                PlaceSymbol(NetworkCell.Row, NetworkCell.Column, NetworkCell.Symbol);
        }

        public void PlaceSymbol(int row, int column, SymbolType symbol)
        {
            _logger.Log($"Grid changed from server {Grid}");

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
            }

            if (_gameRules.CheckDraw(this))
            {
                Debug.Log("Draw");
                _boardWidgetData.OnDraw?.Invoke();
                SetInteractiveBoth(false);
            }

            _boardWidgetData.OnBoardChanged?.Invoke(Grid);

            if (symbol == _playerProvider.Symbol)
            {
                IsInteractive = false;
                _photonManager.GetOtherPlayer.SetInteractRpc(true);
            }
        }

        private void SetInteractiveBoth(bool isInteractable)
        {
            IsInteractive = isInteractable;
            _photonManager.GetOtherPlayer.SetInteractRpc(isInteractable);
        }

        public bool IsCellEmpty(int row, int column)
        {
            return Grid[row, column] == SymbolType.None;
        }

        public bool IsFull()
        {
            foreach (var cell in Grid)
                if (cell == SymbolType.None)
                    return false;

            return true;
        }

        public void Reset()
        {
            for (int i = 0; i < _gridSize; i++)
            for (int j = 0; j < _gridSize; j++)
                Grid[i, j] = SymbolType.None;
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
    }
}