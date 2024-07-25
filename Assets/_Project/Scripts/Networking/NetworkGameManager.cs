using _Project.Scripts.Core;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.GameConstants;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.Scripts.Windows.HUD;
using Fusion;
using UnityEngine;
using WindowsSystem.Core.Managers;

public class NetworkGameManager : NetworkBehaviour
{
    public NetworkPrefabRef boardPrefab;
    private Board board;
    
    [Inject] private IPlayerProvider _playerProvider;
    [Inject] private WindowsController _windowsController;
    [Inject] private BoardData _boardData;
    
    // [Networked(OnChanged = nameof(OnBoardChanged))]
    // public NetworkedList<Move> Moves { get; } = new NetworkedList<Move>();

    /*void Start()
    {
        if (Runner.IsSharedModeMasterClient)
        {
            board = Runner.Spawn(boardPrefab, Vector3.zero, Quaternion.identity, Runner.LocalPlayer).GetComponent<Board>();
        }
    }*/

    // [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    // public void RpcMakeMove(int x, int y, SymbolType symbol)
    // {
    //     MakeMove(x, y, symbol);
    // }
    //
    // private static void OnBoardChanged(Changed<NetworkGameManager> changed)
    // {
    //     changed.Behaviour.UpdateBoard();
    // }
    //
    // private void UpdateBoard()
    // {
    //     // Update the board UI with the latest moves
    //     foreach (var move in Moves)
    //     {
    //         board.MakeMove(move.X, move.Y, move.PlayerSymbol);
    //     }
    // }
    public void StartGame()
    {
        Debug.Log($"Game started");
        
        _windowsController.OpenWindowAsync<BoardWindow>(WindowsConstants.BOARD_WINDOW, _boardData, immediate: true);
    }
    
    public void EndGame()
    {
        Debug.Log($"Game ended");
    }


    public void TryMakeMove(int row, int column)
    {
        // if (HasStateAuthority)
        // {
        //     board.PlaceSymbol(row, column, _playerProvider.Player.Symbol);
        // }
        // else
        // {
        //     Debug.Log($"Player does not have state authority to make a move");
        // }

        Debug.Log($"Player {_playerProvider.Player.Name} with {_playerProvider.Player.Symbol} made a move at {row}, {column}");
    }
}