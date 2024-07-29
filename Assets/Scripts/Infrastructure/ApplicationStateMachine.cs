using _Project.GameStates;
using ServiceLocator.Core;
using Services.States;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class ApplicationStateMachine : StateMachineMonoBehaviour
    {
        [SerializeField] private MonoServicesRegistrator _monoServicesRegistrator;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _monoServicesRegistrator.Register();
        }

        private void Start()
        {
            AddState(new InitializeApplicationState());
            AddState(new LoadApplicationState());
            AddState(new GameplayState());
        
            Enter<InitializeApplicationState>();
        }
    }
}