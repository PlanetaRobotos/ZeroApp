using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Data;
using _Project.Scripts.Windows.HUD;
using Fusion;
using Logging;
using QFSW.QC;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class Board : NetworkBehaviour, IBoard
    {
        [Inject] private BoardData _boardData;
        [Inject] private ICustomLogger _logger;
        [Inject] private IGameRules _gameRules;
        [Inject] private BoardFactory _boardFactory;
        [Inject] private PhotonManager _photonManager;

        private int _gridSize;

        public NetworkBool NetworkIsInteract { get; private set; }

        public SymbolType[,] Grid { get; private set; }

        [Networked, OnChangedRender(nameof(GridChanged))]
        public BoardCell NetworkCell { get; set; }

        public void Initialize()
        {
            Grid = _boardFactory.CreateGrid();
            _boardData.OnBoardChanged?.Invoke(Grid);
            Reset();
        }

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void MakeMoveRpc(BoardCell cell)
        {
            Debug.Log($"MakeMoveRpc called {cell}");

            NetworkCell = cell;
        }

        private void GridChanged()
        {
            if (HasStateAuthority && IsInteractive)
            {
                PlaceSymbol(NetworkCell.Row, NetworkCell.Column, NetworkCell.Symbol);
            }
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
                _boardData.OnPlayerWin?.Invoke(symbol);
                IsInteractive = false;
            }

            if (_gameRules.CheckDraw(this))
            {
                Debug.Log("Draw");
                _boardData.OnDraw?.Invoke();
                IsInteractive = false;
            }

            _boardData.OnBoardChanged?.Invoke(Grid);
            IsInteractive = false;
            _photonManager.GetOtherPlayer.Board.IsInteractive = true;
        }

        [Command("show-player")]
        public void ShowPlayer()
        {
            // Debug.Log($"Current player: {currentPlayer.PlayerId}");
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
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    Grid[i, j] = SymbolType.None;
                }
            }
        }

        public bool IsInteractive
        {
            get => NetworkIsInteract;
            set
            {
                _boardData.OnInteractiveChanged?.Invoke(value);
                NetworkIsInteract = value;
            }
        }

        /*public void SetInteractive(bool isActive)
        {
            _boardData.OnInteractiveChanged?.Invoke(isActive);
        }*/
    }
}