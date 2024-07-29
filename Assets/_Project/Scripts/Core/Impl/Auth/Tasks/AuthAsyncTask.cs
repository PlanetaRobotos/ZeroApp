using System.Threading;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Core.Auth;
using _Project.Scripts.GameConstants;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.AuthWidget;
using Cysharp.Threading.Tasks;
using WindowsSystem.Core.Managers;

namespace _Project.Scripts.Infrastructure.States
{
    public class AuthAsyncTask : AsyncTask
    {
        [Inject] private readonly WindowsController _windowsController;
        [Inject] private readonly IAuthProvider _authProvider;
        [Inject] private BaseUIMediator<AuthWindow> _authWindowMediator;
        [Inject] private IPlayerProfileProvider _playerProvider;
        [Inject] private IGameTracker _gameTracker;

        protected override async UniTask DoAsync()
        {
            if (!_authProvider.TryAutoAuth(out string email, out string password, out string username))
            {
                var authWindow = await _windowsController.OpenWindowAsync<AuthWindow>(WindowsConstants.AUTH_WINDOW,
                    new AuthWindowData(), true);
                await _authWindowMediator.InitializeMediator(authWindow, CancellationToken.None);
                await _authWindowMediator.RunMediator(CancellationToken.None);
            }
        }
    }
}