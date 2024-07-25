using System.Threading;
using _Project.Scripts.Configs;
using _Project.Scripts.Entities;
using _Project.Scripts.GameConstants;
using _Project.Scripts.Infrastructure.Tasks;
using _Project.Scripts.Windows.HUD;
using _Project.Scripts.Windows.Loading;
using Constellation.SceneManagement;
using GameTasks;
using GameTasks.Core;
using Services.States;
using WindowsSystem.Core.Managers;

namespace _Project.Scripts.Infrastructure.States
{
    public class LoadApplicationState : IState
    {
        [Inject] private readonly ApplicationStateMachine _stateMachine;
        [Inject] private readonly IScenesManager _scenesManager;
        [Inject] private readonly TasksLoader _tasksLoader;
        [Inject] private readonly WindowsController _windowsController;
        [Inject] private readonly ConfigsController _configsController;
        [Inject] private readonly FollowBehaviour _followBehaviour;

        [Inject] private readonly PhotonManager _photonManager;

        private CancellationTokenSource _cts;

        public void Enter()
        {
            _cts = new CancellationTokenSource();

            _tasksLoader.DoTasks(GetTasks()).OnDone(_ => _stateMachine.Enter<GameplayState>());
        }

        public void Exit()
        {
            if (_tasksLoader.enabled)
                _tasksLoader.AbortTasks();
        }

        private ITask[] GetTasks()
        {
            ITask[] tasks =
            {
                new OpenWindowTask<LoadingWindow>(_windowsController, WindowsConstants.LOADING_WINDOW, true),
                new OpenWindowTask<HUDWindow>(_windowsController, WindowsConstants.HUD_WINDOW, true),
                new OpenWindowTask<SelectModeWindow>(_windowsController, WindowsConstants.SELECT_MODE_WINDOW, true),
                // new MakeActionTaskAsync(
                // () => _scenesManager.LoadScene((byte)SceneLibraryConstants.GAMEPLAY, _cts.Token)),

                // new LoadEntitiesTaskAsync(),
                // new MakeActionTask<FollowBehaviour>(_followBehaviour, x => x.Initialize()),

                // new MakeActionTaskAsync(
                    // () => { return _photonManager.StartGame(); })

                // new LoadAndOpenBoardWidgetTask(),
            };
            return tasks;
        }
    }
}