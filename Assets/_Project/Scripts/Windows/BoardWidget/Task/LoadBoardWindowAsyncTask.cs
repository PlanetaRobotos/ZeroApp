using System.Threading;
using _Project.GameConstants;
using _Project.Models.Boards;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.UI.Mediators;
using _Project.Windows.BoardWidget.Views;
using Cysharp.Threading.Tasks;
using WindowsSystem.Core.Managers;

namespace _Project.Windows.BoardWidget.Task
{
    public class LoadBoardWindowAsyncTask : AsyncTask
    {
        private readonly BoardWidgetData _boardWidgetData;
        private readonly CancellationToken _cancellationToken;
        [Inject] private BaseUIMediator<BoardWindow> _boardWindowMediator;
        [Inject] private WindowsController _windowsController;

        public LoadBoardWindowAsyncTask(BoardWidgetData boardWidgetData, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _boardWidgetData = boardWidgetData;
        }

        protected override async UniTask DoAsync()
        {
            BoardWindow boardWindow = await _windowsController.OpenWindowAsync<BoardWindow>(
                WindowsConstants.BOARD_WINDOW, _boardWidgetData, true);
            await _boardWindowMediator.InitializeMediator(boardWindow, _cancellationToken);
            await _boardWindowMediator.RunMediator(_cancellationToken);
        }
    }
}