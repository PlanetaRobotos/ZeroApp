using System;
using _Project.Scripts.Entities.Abstract;
using UnityEngine;

namespace _Project.Scripts.Entities.AI.States
{
    public class ChaseTargetAIState : BaseAIState
    {
        private readonly AIBase _ai;
        private readonly FollowBehaviour _followBehaviour;

        public ChaseTargetAIState(AIBase ai, FollowBehaviour followBehaviour) : base(ai.gameObject)
        {
            _followBehaviour = followBehaviour;
            _ai = ai;
        }

        public override void EnterState(object[] objects)
        {
            _followBehaviour.AddToGroup(_ai);
            _followBehaviour.UpdateAnimalPositions();
        }

        public override Type FixedTick()
        {
            if (_ai.HaveTarget(out Vector3 _))
            {
                var targetPosition = _ai.Target.position;
                _ai.Movement.Rotate(targetPosition);
                _ai.Movement.CalculateDirection(targetPosition);
                _ai.Movement.Move(_ai.Target.position);
            }
            else
            {
                return typeof(IdleAIState);
            }

            return null;
        }
    }
}