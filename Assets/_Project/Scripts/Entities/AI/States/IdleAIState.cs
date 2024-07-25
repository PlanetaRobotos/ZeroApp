using System;
using _Project.Scripts.Entities.Abstract;
using UnityEngine;

namespace _Project.Scripts.Entities.AI.States
{
    public class IdleAIState : BaseAIState
    {
        private readonly AIBase _ai;
        private readonly float _startPatrolDelay;
        
        private int _patrolTimer;

        public IdleAIState(AIBase ai, float startPatrolDelay) : base(ai.gameObject)
        {
            _ai = ai;
            _startPatrolDelay = startPatrolDelay;
        }

        public override Type FixedTick()
        {
            if (_ai.HaveTarget(out Vector3 _))
                return typeof(ChaseTargetAIState);
            
            if (_patrolTimer >= _startPatrolDelay)
            {
                _patrolTimer = 0;
                return typeof(CirclePatrolAIState);
            }
            
            _patrolTimer++;
            
            return null;
        }
    }
}