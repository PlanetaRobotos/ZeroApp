﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Project.Scripts.Core;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Data;
using _Project.Scripts.GameConstants;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.Scripts.Windows.SearchOpponentWidget.Views;
using Constellation.SceneManagement;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using WindowsSystem.Core.Managers;

public class PhotonManager : MonoBehaviour, INetworkRunnerCallbacks
{
    private const int WaitingOpponentDelay = 5;

    public NetworkPrefabRef gameManagerPrefab;

    public NetworkGameManager[] Players = new NetworkGameManager[2];
    private NetworkGameManager _currentPlayer;

    private NetworkRunner _runner;
    private CancellationTokenSource _cts;

    [Inject] private WindowsController WindowsController { get; }
    [Inject] private BoardData _boardData;
    [Inject] private IScenesManager ScenesManager { get; }
    [Inject] private IPlayerProvider PlayerProvider { get; }

    public NetworkGameManager GetOtherPlayer =>
        Players.First(x => x != _currentPlayer);

    public async UniTask StartGame()
    {
        _cts = new CancellationTokenSource();
        _runner = new GameObject("Network Runner").AddComponent<NetworkRunner>();
        var sceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        _runner.transform.SetParent(gameObject.transform);
        _runner.ProvideInput = true;
        _runner.AddCallbacks(this);

        await ScenesManager.LoadScene((byte)SceneLibraryConstants.GAMEPLAY, _cts.Token);

        var startGameArgs = new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = "TicTacToeRoom",
            SceneManager = sceneManager
        };

        var searchOpponentWindow =
            await WindowsController.OpenWindowAsync<SearchOpponentWindow>(WindowsConstants.SEARCH_WINDOW, _boardData,
                true);

        searchOpponentWindow.SetWaitingView("Loading game...");
        Debug.Log($"[Multiplayer] Loading game...");

        await _runner.StartGame(startGameArgs);

        _currentPlayer = _runner.Spawn(gameManagerPrefab, Vector3.zero, Quaternion.identity, _runner.LocalPlayer)
            .GetComponent<NetworkGameManager>();

        searchOpponentWindow.SetWaitingView("Waiting for opponent...");
        Debug.Log($"[Multiplayer] Waiting for opponent...");

        float searchOpponentTimer = 0;
        while (searchOpponentTimer < WaitingOpponentDelay && FindObjectsOfType<NetworkGameManager>().Length < 2)
        {
            var delay = 0.2f;

            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            searchOpponentTimer += delay;
        }

        TryCloseSearchOpponentWindow();

        var players = FindObjectsOfType<NetworkGameManager>();
        if (players.Length == 2)
        {
            Players = players;

            if (_runner.IsSharedModeMasterClient)
                await BothPlayersAreJoinedAsync();
        }
        else
        {
            Debug.LogError($"Players amount is {players.Length}");

            await _runner.Shutdown();
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player {player} joined");
    }

    private async UniTask BothPlayersAreJoinedAsync()
    {
        Debug.Log($"Assigned symbols: Player {Players[0]} = X, Player {Players[1]} = O");

        Players[0].StartGameRpc(SymbolType.Cross, isInteractable: true);
        Players[1].StartGameRpc(SymbolType.Circle, isInteractable: false);
    }

    public void ExitSession()
    {
        if (_runner != null)
        {
            _runner.Shutdown();
        }
    }

    public void TryMakeMove(int x, int y)
    {
        if (_currentPlayer.Board.IsInteractive)
        {
            Debug.Log($"Player {PlayerProvider.Player} is interactive");

            foreach (var network in Players)
            {
                network.Board.MakeMoveRpc(new BoardCell
                {
                    Row = x,
                    Column = y,
                    Symbol = PlayerProvider.Player.Symbol
                });
            }
        }
        else
        {
            Debug.Log($"Player {PlayerProvider.Player} is not interactive");
        }
    }

    private void TryCloseSearchOpponentWindow()
    {
        if (WindowsController.GetWindowById<SearchOpponentWindow>(WindowsConstants.SEARCH_WINDOW))
            WindowsController.CloseWindow(WindowsConstants.SEARCH_WINDOW);
        else
            Debug.LogWarning("SearchOpponentWindow not found");
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
        Debug.Log($"Player left OnShutdown");
        PlayerLeftAsync().Forget();
    }

    private async UniTaskVoid PlayerLeftAsync()
    {
        await _currentPlayer.EndGame(_cts.Token);

        if (_runner != null)
        {
            Destroy(_runner.gameObject);
            _runner = null;
        }
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

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
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

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
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
}