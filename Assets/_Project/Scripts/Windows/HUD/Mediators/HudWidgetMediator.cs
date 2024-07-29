using System.Threading;
using _Project.Core;
using _Project.Core.Tasks;
using _Project.UI.Mediators;
using _Project.Windows.HUD.Views;
using Cysharp.Threading.Tasks;

namespace _Project.Windows.HUD.Mediators
{
    public class HudWidgetMediator : BaseUIMediator<HUDWindow>
    {
        [Inject] private IAuthProvider _authProvider;
        [Inject] private IPlayerProfileProvider PlayerProvider { get; }

        protected override UniTask InitializeMediator(CancellationToken cancellationToken)
        {
            View.SignOutButton.onClick.AddListener(OnSignOut);

            return UniTask.CompletedTask;
        }

        public override UniTask RunMediator(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        private void OnSignOut()
        {
            _authProvider.SignOut();
            PlayerProvider.Reset();

            new AuthAsyncTask().Do();
        }

        protected override UniTask DisposeMediator()
        {
            View.SignOutButton.onClick.RemoveListener(OnSignOut);
            return UniTask.CompletedTask;
        }
    }
}