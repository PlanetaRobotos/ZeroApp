using System.Threading;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Core.Auth;
using _Project.Scripts.Infrastructure.States;
using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.HUD;
using Cysharp.Threading.Tasks;
using GameTasks.Core;

namespace _Project.Scripts.Windows.BoardWidget
{
    public class HudWidgetMediator : BaseUIMediator<HUDWindow>
    {
        [Inject] private IAuthProvider _authProvider;
        [Inject] private readonly TasksLoader _tasksLoader;
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

            _tasksLoader.DoTasks(new ITask[] { new AuthAsyncTask() });
        }

        protected override UniTask DisposeMediator()
        {
            View.SignOutButton.onClick.RemoveListener(OnSignOut);
            return UniTask.CompletedTask;
        }
    }
}