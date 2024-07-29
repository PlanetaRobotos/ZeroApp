using System.Threading;
using _Project.GameConstants;
using _Project.Scripts.Infrastructure.Extensions.AsyncExtensions;
using _Project.UI.Mediators;
using _Project.Windows.HUD.Views;
using Cysharp.Threading.Tasks;
using WindowsSystem.Core.Managers;

namespace _Project.Core.Tasks
{
    public class LoadAndOpenHUDWindowAsyncTask : AsyncTask
    {
        [Inject] private readonly WindowsController _windowsController;
        [Inject] private BaseUIMediator<HUDWindow> _hudWindowMediator;

        protected override async UniTask DoAsync()
        {
            HUDWindow hudWindow = await _windowsController.OpenWindowAsync<HUDWindow>(WindowsConstants.HUD_WINDOW,
                null, true);
            await _hudWindowMediator.InitializeMediator(hudWindow, CancellationToken.None);
            await _hudWindowMediator.RunMediator(CancellationToken.None);
        }
    }
}