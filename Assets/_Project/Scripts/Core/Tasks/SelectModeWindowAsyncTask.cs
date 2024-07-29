using System.Threading;
using _Project.GameConstants;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.UI.Mediators;
using _Project.Windows.SelectModeWidget.Views;
using Cysharp.Threading.Tasks;
using WindowsSystem.Core.Managers;

namespace _Project.Core.Tasks
{
    public class SelectModeWindowAsyncTask : AsyncTask
    {
        [Inject] private readonly IAuthProvider _authProvider;
        [Inject] private readonly WindowsController _windowsController;
        [Inject] private IGameTracker _gameTracker;
        [Inject] private IPlayerProfileProvider _playerProvider;
        [Inject] private BaseUIMediator<SelectModeWindow> _selectModeWindowMediator;

        protected override async UniTask DoAsync()
        {
            SelectModeWindow window = await _windowsController.OpenWindowAsync<SelectModeWindow>(
                WindowsConstants.SELECT_MODE_WINDOW,
                null, true);
            await _selectModeWindowMediator.InitializeMediator(window, CancellationToken.None);
            await _selectModeWindowMediator.RunMediator(CancellationToken.None);
        }
    }
}