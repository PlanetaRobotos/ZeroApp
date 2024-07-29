using System.Threading;
using _Project.Scripts.Core;
using _Project.Scripts.GameConstants;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.HUD;
using Cysharp.Threading.Tasks;
using WindowsSystem.Core.Managers;

namespace _Project.Scripts.Windows.BoardWidget.Tasks
{
    public class LoadBoardWindowAsyncTask: AsyncTask
    {
        [Inject] private BaseUIMediator<BoardWindow> _boardWindowMediator;
        [Inject] private WindowsController _windowsController;
        
        private readonly BoardWidgetData _boardWidgetData;
        private readonly CancellationToken _cancellationToken;

        public LoadBoardWindowAsyncTask(BoardWidgetData boardWidgetData, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _boardWidgetData = boardWidgetData;
        }

        protected override async UniTask DoAsync()
        {
            var boardWindow = await _windowsController.OpenWindowAsync<BoardWindow>(
                WindowsConstants.BOARD_WINDOW, _boardWidgetData, true);
            await _boardWindowMediator.InitializeMediator(boardWindow, _cancellationToken);
            await _boardWindowMediator.RunMediator(_cancellationToken);
        }
    }
}