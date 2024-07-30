using System.Threading;
using _Project.GameConstants;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.UI.Mediators;
using _Project.Windows.AuthWidget.Models;
using _Project.Windows.AuthWidget.Views;
using Cysharp.Threading.Tasks;
using WindowsSystem.Core.Managers;

namespace _Project.Core.Tasks
{
    public class AuthAsyncTask : AsyncTask
    {
        [Inject] private readonly IAuthProvider _authProvider;
        [Inject] private readonly WindowsController _windowsController;
        [Inject] private BaseUIMediator<AuthWindow> _authWindowMediator;
        [Inject] private IGameTracker _gameTracker;
        [Inject] private IPlayerProfileProvider _playerProvider;

        protected override async UniTask DoAsync()
        {
            var result = await _authProvider.TryAutoAuth();
            if (!result)
            {
                AuthWindow authWindow = await _windowsController.OpenWindowAsync<AuthWindow>(
                    WindowsConstants.AUTH_WINDOW,
                    new AuthWindowData(), true);
                await _authWindowMediator.InitializeMediator(authWindow, CancellationToken.None);
                await _authWindowMediator.RunMediator(CancellationToken.None);
            }
        }
    }
}