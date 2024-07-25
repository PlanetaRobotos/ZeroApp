using GameTasks.Core;
using Services.States;

namespace _Project.Scripts.Infrastructure.States
{
    public class GameplayState : IState
    {
        [Inject] private readonly ApplicationStateMachine _stateMachine;
        [Inject] private readonly TasksLoader _tasksLoader;

        public void Enter()
        {
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
            };

            return tasks;
        }

        private void OnTasksProgress(float progress)
        {
            // LoadingWindow.progress = progress;
        }
    }
}