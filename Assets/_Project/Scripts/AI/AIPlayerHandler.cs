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

namespace _Project.Scripts.AI
{
    public class AIPlayerHandler : IPlayerHandler
    {
        [Inject] private WindowsController _windowsController;
        [Inject] private readonly IScenesManager _scenesManager;
        [Inject] private IPlayerProfileProvider _playerProvider;
        [Inject] private BaseUIMediator<BoardWindow> _boardWindowMediator;
        [Inject] private IBoardFactory _boardFactory;

        public IBoard Board { get; set; }
        public IGameplayMediator GameplayMediator { get; set; }

        public async UniTask StartGame(SymbolType symbol, NetworkBool isInteractable)
        {
            Debug.Log($"Game started {symbol} - {isInteractable}");

            var boardWidgetData = new BoardWidgetData
            {
                BoardSize = _boardFactory.GridSize,
                GameplayMediator = GameplayMediator 
            };

            Board.Initialize(boardWidgetData, GameplayMediator);
            
            await new LoadBoardWindowAsyncTask(boardWidgetData, CancellationToken.None).Do();

            _playerProvider.Symbol = symbol;
            Board.SetInteract(isInteractable);

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
}