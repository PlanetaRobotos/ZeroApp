using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

public class PhotonManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkPrefabRef gameManagerPrefab;

    private NetworkRunner _runner;
    private NetworkGameManager _networkManager;

    public async UniTask StartGame()
    {
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;
        _runner.AddCallbacks(this);

        var startGameArgs = new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = "TicTacToeRoom",
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        };

        await _runner.StartGame(startGameArgs);

        if (_runner.IsSharedModeMasterClient)
        {
            _networkManager = _runner.Spawn(gameManagerPrefab, Vector3.zero, Quaternion.identity, _runner.LocalPlayer)
                .GetComponent<NetworkGameManager>();
            
            Locator.Register(_networkManager);

            Debug.Log($"{_networkManager} spawned as master client");
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (_networkManager == null)
        {
            _networkManager = Locator.GetService<NetworkGameManager>();
        }

        Debug.Log($"Player {player} joined");
        // Check if we have 2 players connected
        if (runner.ActivePlayers.Count() == 2)
        {
            _networkManager.StartGame();
        }
    }

    // Implement other INetworkRunnerCallbacks methods as needed
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player {player} left");
        // Handle player leaving, possibly end the game
        _networkManager.EndGame();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
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