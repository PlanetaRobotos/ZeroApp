using System.Threading;
using _Project.Scripts.Core;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.GameConstants;
using _Project.Scripts.Infrastructure.States;
using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.BoardWidget.Tasks;
using _Project.Scripts.Windows.HUD;
using Constellation.SceneManagement;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using WindowsSystem.Core.Managers;

public class NetworkPlayerHandler : NetworkBehaviour, IPlayerHandler
{
    [SerializeField] private NetworkBoard board;

    public IBoard Board
    {
        get => board;
        set => board = (NetworkBoard) value;
    }

    public IGameplayMediator GameplayMediator { get; set; }

    [Inject] private WindowsController _windowsController;
    [Inject] private readonly IScenesManager _scenesManager;
    [Inject] private IPlayerProfileProvider _playerProvider;
    [Inject] private BaseUIMediator<BoardWindow> _boardWindowMediator;
    [Inject] private IBoardFactory _boardFactory;

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void StartGameRpc(SymbolType symbol, NetworkBool isInteractable)
    {
        StartGameAsync(symbol, isInteractable).Forget();
    }

    public UniTask StartGame(SymbolType symbol, NetworkBool isInteractable)
    {
        StartGameRpc(symbol, isInteractable);
        return UniTask.CompletedTask;
    }

    private async UniTaskVoid StartGameAsync(SymbolType symbol, NetworkBool isInteractable)
    {
        Debug.Log($"Game started {symbol} - {isInteractable}");

        var boardWidgetData = new BoardWidgetData
        {
            BoardSize = _boardFactory.GridSize,
            GameplayMediator = GameplayMediator
        };

        board.Initialize(boardWidgetData, GameplayMediator);
        
        await new LoadBoardWindowAsyncTask(boardWidgetData, CancellationToken.None).Do();
        
        _playerProvider.Symbol = symbol;
        board.IsInteractive = isInteractable;
        
        Debug.Log($"Board window opened");
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

        await new SelectModeWindowAsyncTask().Do();
    }
}