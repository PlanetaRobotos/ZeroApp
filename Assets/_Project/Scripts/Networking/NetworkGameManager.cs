using System.Threading;
using _Project.Scripts.Core;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.GameConstants;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.HUD;
using Constellation.SceneManagement;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using WindowsSystem.Core.Managers;

public class NetworkGameManager : NetworkBehaviour
{
    [SerializeField] private Board board;

    public IBoard Board => board;

    [Inject] private WindowsController _windowsController;
    [Inject] private readonly IScenesManager _scenesManager;
    [Inject] private IPlayerProfileProvider _playerProvider;
    [Inject] private BaseUIMediator<BoardWindow> _boardWindowMediator;
    [Inject] private IBoardFactory _boardFactory;

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void StartGameRpc(SymbolType symbol, NetworkBool isInteractable)
    {
        StartGameAsync(symbol, isInteractable).Forget();
    }

    private async UniTaskVoid StartGameAsync(SymbolType symbol, NetworkBool isInteractable)
    {
        Debug.Log($"Game started {symbol} - {isInteractable}");

        var boardWidgetData = new BoardWidgetData
        {
            BoardSize = _boardFactory.GridSize,
        };

        board.Initialize(boardWidgetData);

        var boardWindow = await _windowsController.OpenWindowAsync<BoardWindow>(
            WindowsConstants.BOARD_WINDOW, boardWidgetData, true);
        await _boardWindowMediator.InitializeMediator(boardWindow, CancellationToken.None);
        await _boardWindowMediator.RunMediator(CancellationToken.None);
        
        _playerProvider.Symbol = symbol;
        board.IsInteractive = isInteractable;
        
        Debug.Log($"Board window opened {boardWindow}");
    }

    public async UniTask EndGame(CancellationToken cancellationToken)
    {
        Debug.Log($"Game ended");

        if (_windowsController.GetWindowById<BoardWindow>(WindowsConstants.BOARD_WINDOW))
        {
            _windowsController.CloseWindow(WindowsConstants.BOARD_WINDOW);
            _boardWindowMediator.Dispose();
        }
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