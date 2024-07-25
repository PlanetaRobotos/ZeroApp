using _Project.Scripts.Core;
using _Project.Scripts.GameConstants;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.Scripts.Windows.HUD;
using Cysharp.Threading.Tasks;
using WindowsSystem.Core.Managers;

namespace _Project.Scripts.Infrastructure.States
{
    public class LoadAndOpenBoardWidgetTask : AsyncTask
    {
        [Inject] private readonly WindowsController _windowsController;
        [Inject] private BoardData _boardData;

        protected override async UniTask DoAsync()
        {
            await _windowsController.OpenWindowAsync<BoardWindow>(WindowsConstants.BOARD_WINDOW, _boardData, immediate: true);

        }
    }
}