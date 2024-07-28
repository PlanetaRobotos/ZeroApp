using System.Threading;
using _Project.Configs.Boards;
using _Project.Scripts.Core;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.GameConstants;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.Scripts.Windows.HUD;
using Constellation.SceneManagement;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using WindowsSystem.Core.Managers;

public class NetworkGameManager : NetworkBehaviour
{
    [SerializeField] private Board board;

    public Board Board => board;

    [Inject] private WindowsController _windowsController;
    [Inject] private BoardData _boardData;
    [Inject] private readonly IScenesManager _scenesManager;
    [Inject] private IPlayerProvider _playerProvider;
    [Inject] private BoardConfig _boardConfig;

    // public IPlayerProvider PlayerProvider => _playerProvider;

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void StartGameRpc(SymbolType symbol, NetworkBool isInteractable)
    {
        StartGameAsync(symbol, isInteractable).Forget();
    }

    private async UniTaskVoid StartGameAsync(SymbolType symbol, NetworkBool isInteractable)
    {
        Debug.Log($"Game started {symbol} - {isInteractable}");

        board.Initialize();

        await _windowsController.OpenWindowAsync<BoardWindow>(WindowsConstants.BOARD_WINDOW, _boardData, true);

        SetPlayerSymbol(symbol);
        board.IsInteractive = isInteractable;
    }

    private void SetPlayerSymbol(SymbolType symbol)
    {
        var playerProfile = new PlayerProfile
        {
            Name = $"Player {symbol}",
            Symbol = symbol
        };

        Debug.Log($"Player changed {playerProfile}");

        _playerProvider.SetPlayer(playerProfile);
        gameObject.name = playerProfile.Name.Value;
    }

    public async UniTask EndGame(CancellationToken cancellationToken)
    {
        Debug.Log($"Game ended");

        if (_windowsController.GetWindowById<BoardWindow>(WindowsConstants.BOARD_WINDOW))
            _windowsController.CloseWindow(WindowsConstants.BOARD_WINDOW);
        else
            Debug.LogWarning("Board window not found");

        await _scenesManager.LoadScene((byte)SceneLibraryConstants.MAIN_MENU, cancellationToken);
        
        _windowsController.OpenImmediate<SelectModeWindow>(WindowsConstants.SELECT_MODE_WINDOW);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void SetInteractRpc(NetworkBool isInteractable)
    {
        Debug.Log($"Board changed isInteractable {isInteractable}");

        board.IsInteractive = isInteractable;
    }
}