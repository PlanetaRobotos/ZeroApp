using System.Threading;
using _Project.Core;
using _Project.Core.Tasks;
using _Project.GameConstants;
using _Project.Models;
using _Project.Models.Boards;
using _Project.UI.Mediators;
using _Project.Windows.BoardWidget.Factories;
using _Project.Windows.BoardWidget.Task;
using _Project.Windows.BoardWidget.Views;
using Constellation.SceneManagement;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using WindowsSystem.Core.Managers;

namespace _Project.AI
{
    public class AIPlayerHandler : IPlayerHandler
    {
        [Inject] private readonly IScenesManager _scenesManager;
        [Inject] private IBoardFactory _boardFactory;
        [Inject] private BaseUIMediator<BoardWindow> _boardWindowMediator;
        [Inject] private IPlayerProfileProvider _playerProvider;
        [Inject] private WindowsController _windowsController;

        public IBoard Board { get; set; }
        public IGameplayMediator GameplayMediator { get; set; }

        public async UniTask StartGame(SymbolType symbol, NetworkBool isInteractable)
        {
            Debug.Log($"Game started {symbol} - {isInteractable}");

            BoardWidgetData boardWidgetData = new BoardWidgetData
            {
                BoardSize = _boardFactory.GridSize,
                GameplayMediator = GameplayMediator
            };

            Board.Initialize(boardWidgetData, GameplayMediator);

            await new LoadBoardWindowAsyncTask(boardWidgetData, CancellationToken.None).Do();

            _playerProvider.Symbol = symbol;
            Board.SetInteract(isInteractable);

            Debug.Log("Board window opened");
        }

        public async UniTask EndGame(CancellationToken cancellationToken)
        {
            Debug.Log("Game ended");

            if (_windowsController.GetWindowById<BoardWindow>(WindowsConstants.BOARD_WINDOW))
            {
                _windowsController.CloseWindow(WindowsConstants.BOARD_WINDOW);
                _boardWindowMediator.Dispose();
            }
            else
            {
                Debug.LogWarning("Board window not found");
            }

            await _scenesManager.LoadScene((byte)SceneLibraryConstants.MAIN_MENU, cancellationToken);

            await new SelectModeWindowAsyncTask().Do();
        }
    }
}