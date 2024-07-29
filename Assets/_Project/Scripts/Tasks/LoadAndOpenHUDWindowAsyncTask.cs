using System.Threading;
using _Project.Scripts.GameConstants;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.HUD;
using Cysharp.Threading.Tasks;
using WindowsSystem.Core.Managers;

namespace _Project.Scripts.Tasks
{
    public class LoadAndOpenHUDWindowAsyncTask: AsyncTask
    {
        [Inject] private readonly WindowsController _windowsController;
        [Inject] private BaseUIMediator<HUDWindow> _hudWindowMediator;

        protected override async UniTask DoAsync()
        {
            var hudWindow = await _windowsController.OpenWindowAsync<HUDWindow>(WindowsConstants.HUD_WINDOW,
                null, true);
            await _hudWindowMediator.InitializeMediator(hudWindow, CancellationToken.None);
            await _hudWindowMediator.RunMediator(CancellationToken.None);
        }
    }
}