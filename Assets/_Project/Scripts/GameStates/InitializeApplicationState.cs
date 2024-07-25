using GameTasks;
using GameTasks.Core;
using Services.States;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.States
{
    public class InitializeApplicationState : IState
    {
        [Inject] private ApplicationStateMachine _stateMachine { get; }
        [Inject] private TasksLoader _tasksLoader { get; }
        
        public void Enter()
        {
            Application.runInBackground = true;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            SetGameFrameRate();
            _tasksLoader.DoTasks(GetTasks()).OnDone(_ => _stateMachine.Enter<LoadApplicationState>());
        }

        public void Exit()
        {
        }

        private ITask[] GetTasks()
        {
            ITask[] tasks = {
                new WaitCachingReadyTask(),
            };

            return tasks;
        }

        private void SetGameFrameRate()
        {
#if UNITY_ANDROID || UNITY_IOS
            Application.targetFrameRate = 60; //TODO определять для слабых девайсов другой фреймрейт
#else
        Application.targetFrameRate = 300;
#endif
        }
    }
}