using System.Threading;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Core.Auth;
using _Project.Scripts.GameConstants;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.HUD;
using Cysharp.Threading.Tasks;
using WindowsSystem.Core.Managers;

namespace _Project.Scripts.Infrastructure.States
{
    public class SelectModeWindowAsyncTask : AsyncTask
    {
        [Inject] private readonly WindowsController _windowsController;
        [Inject] private readonly IAuthProvider _authProvider;
        [Inject] private BaseUIMediator<SelectModeWindow> _selectModeWindowMediator;
        [Inject] private IPlayerProfileProvider _playerProvider;
        [Inject] private IGameTracker _gameTracker;

        protected override async UniTask DoAsync()
        {
            var window = await _windowsController.OpenWindowAsync<SelectModeWindow>(WindowsConstants.SELECT_MODE_WINDOW,
                null, true);
            await _selectModeWindowMediator.InitializeMediator(window, CancellationToken.None);
            await _selectModeWindowMediator.RunMediator(CancellationToken.None);
        }
    }
}