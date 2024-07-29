using System.Threading;
using _Project.Core.Tasks;
using _Project.GameConstants;
using _Project.Scripts.Infrastructure;
using _Project.Scripts.Infrastructure.Tasks;
using _Project.Windows.Loading;
using Constellation.SceneManagement;
using GameTasks;
using GameTasks.Core;
using Services.States;
using WindowsSystem.Core.Managers;

namespace _Project.GameStates
{
    public class LoadApplicationState : IState
    {
        [Inject] private readonly IScenesManager _scenesManager;
        [Inject] private readonly ApplicationStateMachine _stateMachine;
        [Inject] private readonly TasksLoader _tasksLoader;
        [Inject] private readonly WindowsController _windowsController;

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
                new LoadAndOpenHUDWindowAsyncTask(),
                new SelectModeWindowAsyncTask(),
                new AuthAsyncTask(),
                new MakeActionTaskAsync(
                    () => _scenesManager.LoadScene((byte)SceneLibraryConstants.MAIN_MENU, _cts.Token))
            };
            return tasks;
        }
    }
}