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

    public IPlayerProvider PlayerProvider => _playerProvider;

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void StartGameRpc()
    {
        Debug.Log($"Game started");

        board.Initialize();

        _windowsController.OpenWindowAsync<BoardWindow>(WindowsConstants.BOARD_WINDOW, _boardData, true);
    }

    public async UniTask EndGame(CancellationToken cancellationToken)
    {
        Debug.Log($"Game ended");

        _windowsController.CloseWindow(WindowsConstants.BOARD_WINDOW);

        await _scenesManager.LoadScene((byte)SceneLibraryConstants.MAIN_MENU, cancellationToken);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void SetPlayerRpc(PlayerProfile playerProfile)
    {
        Debug.Log($"Player changed {playerProfile}");

        _playerProvider.SetPlayer(playerProfile);
        gameObject.name = playerProfile.Name.Value;
    }
    
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void SetInteractRpc(NetworkBool isInteractable)
    {
        Debug.Log($"Board changed isInteractable {isInteractable}");
        
        board.IsInteractive = isInteractable;
    }
}