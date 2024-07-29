using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Project.Core;
using _Project.GameConstants;
using _Project.Models;
using _Project.Models.Boards;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.Windows.SearchOpponentWidget.Views;
using Constellation.SceneManagement;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using WindowsSystem.Core.Managers;
using Object = UnityEngine.Object;

namespace _Project.Networking
{
    public class PhotonGameplayMediator : INetworkRunnerCallbacks, IGameplayMediator
    {
        private const int WaitingOpponentDelay = 25;

        private readonly NetworkPlayerHandler _networkPlayerPrefab;
        private CancellationTokenSource _cts;
        private IPlayerHandler _currentPlayer;

        private NetworkPlayerHandler[] _players = new NetworkPlayerHandler[2];

        private NetworkRunner _runner;

        public PhotonGameplayMediator(NetworkPlayerHandler networkPlayerPrefab)
        {
            _networkPlayerPrefab = networkPlayerPrefab;
        }

        [Inject] private WindowsController WindowsController { get; }
        [Inject] private IScenesManager ScenesManager { get; }
        [Inject] private IPlayerProfileProvider PlayerProvider { get; }

        public NetworkPlayerHandler GetOtherPlayer =>
            _players.First(x => x != _currentPlayer);

        public async UniTask StartGame()
        {
            _cts = new CancellationTokenSource();
            _runner = new GameObject("Network Runner").AddComponent<NetworkRunner>();
            NetworkSceneManagerDefault sceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
            Object.DontDestroyOnLoad(_runner);
            _runner.ProvideInput = true;
            _runner.AddCallbacks(this);

            await ScenesManager.LoadScene((byte)SceneLibraryConstants.GAMEPLAY, _cts.Token);

            StartGameArgs startGameArgs = new StartGameArgs
            {
                GameMode = GameMode.Shared,
                SessionName = "TicTacToeRoom",
                SceneManager = sceneManager
            };

            SearchOpponentWindow searchOpponentWindow = await WindowsController.OpenWindowAsync<SearchOpponentWindow>(
                WindowsConstants.SEARCH_WINDOW, null, true);

            searchOpponentWindow.SetWaitingView("Loading game...");
            Debug.Log("[Multiplayer] Loading game...");

            await _runner.StartGame(startGameArgs);

            _currentPlayer = _runner.Spawn(_networkPlayerPrefab, Vector3.zero, Quaternion.identity)
                .GetComponent<NetworkPlayerHandler>();

            _currentPlayer.GameplayMediator = this;

            searchOpponentWindow.SetWaitingView("Waiting for opponent...");
            Debug.Log("[Multiplayer] Waiting for opponent...");

            float searchOpponentTimer = 0;
            while (searchOpponentTimer < WaitingOpponentDelay &&
                   Object.FindObjectsOfType<NetworkPlayerHandler>().Length < 2)
            {
                var delay = 0.2f;

                await UniTask.Delay(TimeSpan.FromSeconds(delay));
                searchOpponentTimer += delay;
            }

            TryCloseSearchOpponentWindow();

            NetworkPlayerHandler[] players = Object.FindObjectsOfType<NetworkPlayerHandler>();
            if (players.Length == 2)
            {
                _players = players;

                if (_runner.IsSharedModeMasterClient)
                    await BothPlayersAreJoinedAsync();
            }
            else
            {
                Debug.LogError($"Players amount is {players.Length}");

                await _runner.Shutdown();
            }
        }

        public void ExitSession()
        {
            if (_runner != null) _runner.Shutdown();
        }

        public void TryMakeMove(int x, int y)
        {
            if (_currentPlayer.Board.IsInteractive)
            {
                Debug.Log($"Player {PlayerProvider} is interactive");

                foreach (NetworkPlayerHandler network in _players)
                    network.Board.MakeMove(new BoardCell
                    {
                        Row = x,
                        Column = y,
                        Symbol = PlayerProvider.Symbol
                    });
            }
            else
            {
                Debug.Log($"Player {PlayerProvider} is not interactive");
            }
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log($"Player {player} joined");
        }

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log($"Player {player} left OnPlayerLeft");
            PlayerLeftAsync().Forget();
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            Debug.Log("Player left OnShutdown");
            PlayerLeftAsync().Forget();
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
            byte[] token)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key,
            ArraySegment<byte> data)
        {
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }

        private async UniTask BothPlayersAreJoinedAsync()
        {
            Debug.Log($"Assigned symbols: Player {_players[0]} = X, Player {_players[1]} = O");

            _players[0].StartGame(SymbolType.Cross, true);
            _players[1].StartGame(SymbolType.Circle, false);
        }

        public void TryCloseSearchOpponentWindow()
        {
            if (WindowsController.GetWindowById<SearchOpponentWindow>(WindowsConstants.SEARCH_WINDOW))
                WindowsController.CloseWindow(WindowsConstants.SEARCH_WINDOW);
            else
                Debug.LogWarning("SearchOpponentWindow not found");
        }

        private async UniTaskVoid PlayerLeftAsync()
        {
            await _currentPlayer.EndGame(_cts.Token);

            if (_runner != null)
            {
                Object.Destroy(_runner.gameObject);
                _runner = null;
            }
        }
    }
}