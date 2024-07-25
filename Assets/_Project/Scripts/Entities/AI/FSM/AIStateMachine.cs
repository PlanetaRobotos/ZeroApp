using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Entities.AI.States;
using UnityEngine;

namespace _Project.Scripts.Entities.AI.FSM
{
    public class AIStateMachine : MonoBehaviour
    {
        private Dictionary<Type, BaseAIState> _availableStates;

        private BaseAIState CurrentAIState { get; set; }
        public event Action<BaseAIState> OnStateChanged = s => { };

        public void SetStates(Dictionary<Type, BaseAIState> states) =>
            _availableStates = states;

        private void Update()
        {
            // Debug.Log($"{gameObject.name} = {CurrentAIState}");
            TickAction(CurrentAIState?.Tick());
        }

        private void FixedUpdate() => TickAction(CurrentAIState?.FixedTick());

        private void TickAction(Type func)
        {
            if (CurrentAIState == null)
            {
                CurrentAIState = _availableStates.Values.First();
                CurrentAIState.EnterState(null);
            }

            var nextState = func;

            if (nextState != null && nextState != CurrentAIState?.GetType())
                ChangeState(nextState);
        }
        
        public void ChangeState(Type nextState, params object[] args)
        {
            CurrentAIState = _availableStates[nextState];
            Debug.Log($"[AIState - {gameObject.name}] Change state to {CurrentAIState}");
            OnStateChanged(CurrentAIState);
            CurrentAIState.EnterState(args);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            CurrentAIState.TriggerEnter(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            CurrentAIState.TriggerExit(other);
        }
    }
}