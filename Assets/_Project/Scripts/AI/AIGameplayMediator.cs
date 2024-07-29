using System;
using System.Linq;
using System.Threading;
using _Project.Scripts.Core;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Data;
using _Project.Scripts.GameConstants;
using Constellation.SceneManagement;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public class AIGameplayMediator : IGameplayMediator
    {
        [Inject] private IScenesManager ScenesManager { get; }
        [Inject] private IPlayerProfileProvider PlayerProvider { get; }

        public NetworkPlayerHandler GetOtherPlayer => null;

        public IPlayerHandler _currentPlayer;
        
        private CancellationTokenSource _cts;
        private IGameplayMediator _gameplayMediatorImplementation;

        public async UniTask StartGame()
        {
            var board = new AIBoard();

            _cts = new CancellationTokenSource();
            await ScenesManager.LoadScene((byte)SceneLibraryConstants.GAMEPLAY, _cts.Token);

            _currentPlayer = new AIPlayerHandler();
            _currentPlayer.Board = board;
            _currentPlayer.GameplayMediator = this;

            await _currentPlayer.StartGame(SymbolType.Cross, new NetworkBool(true));
        }

        public void ExitSession()
        {
            ExitSessionAsync().Forget();
        }

        private async UniTask ExitSessionAsync()
        {
            _cts.Cancel();
            await _currentPlayer.EndGame(_cts.Token);
        }

        public void TryMakeMove(int x, int y)
        {
            if (_currentPlayer.Board.IsInteractive)
            {
                Debug.Log($"Player is interactive");

                _currentPlayer.Board.MakeMove(new BoardCell
                {
                    Row = x,
                    Column = y,
                    Symbol = PlayerProvider.Symbol
                });
            }
            else
            {
                Debug.Log($"Player is not interactive");
            }
        }
    }
}