using System;
using _Project.Scripts.Entities.AI.AIEntities;
using _Project.Scripts.Entities.Spawners;
using UnityEngine;

namespace _Project.Scripts.Entities.AI.States
{
    public class CirclePatrolAIState : BaseAIState
    {
        private readonly SimpleAI _ai;
        private Vector3 _point;
        private float _timer;
        private readonly float _findNextPointDelay;

        public CirclePatrolAIState(SimpleAI ai, float findNextPointDelay)
            : base(ai.gameObject)
        {
            _findNextPointDelay = findNextPointDelay;
            _ai = ai;
        }

        public override void EnterState(object[] objects)
        {
            SetNextPoint();
        }

        public override Type FixedTick()
        {
            if (_ai.HaveTarget(out Vector3 _))
                return typeof(ChaseTargetAIState);
            
            TimerActionLooped(ref _timer, _findNextPointDelay, action: () =>
            {
                SetNextPoint();
                _ai.NavMeshAgent.SetDestination(_point);
            });

            Debug.DrawLine(_ai.transform.position, _point, Color.red);

            return null;
        }

        private static void TimerActionLooped(ref float currentValue, float maxValue, Action action)
        {
            if (currentValue >= maxValue)
            {
                action?.Invoke();
                currentValue = 0f;
            }

            currentValue += Time.fixedDeltaTime;
        }

        private void SetNextPoint()
        {
            _point = _ai.SpawnArea.TryGetRandomPosition(_ai.EntitiesConfig.CheckSpawnRadius,
                _ai.EntitiesConfig.SpawnLayerMask);
        }
    }
}