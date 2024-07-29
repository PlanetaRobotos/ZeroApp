using System.Threading;
using _Project.Core;
using _Project.GameConstants;
using _Project.Models;
using _Project.Models.Boards;
using _Project.Networking;
using Constellation.SceneManagement;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace _Project.AI
{
    public class AIGameplayMediator : IGameplayMediator
    {
        private CancellationTokenSource _cts;

        public IPlayerHandler _currentPlayer;
        private IGameplayMediator _gameplayMediatorImplementation;
        [Inject] private IScenesManager ScenesManager { get; }
        [Inject] private IPlayerProfileProvider PlayerProvider { get; }

        public NetworkPlayerHandler GetOtherPlayer => null;

        public async UniTask StartGame()
        {
            AIBoard board = new AIBoard();

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

        public void TryMakeMove(int x, int y)
        {
            if (_currentPlayer.Board.IsInteractive)
            {
                Debug.Log("Player is interactive");

                _currentPlayer.Board.MakeMove(new BoardCell
                {
                    Row = x,
                    Column = y,
                    Symbol = PlayerProvider.Symbol
                });
            }
            else
            {
                Debug.Log("Player is not interactive");
            }
        }

        private async UniTask ExitSessionAsync()
        {
            _cts.Cancel();
            await _currentPlayer.EndGame(_cts.Token);
        }
    }
}