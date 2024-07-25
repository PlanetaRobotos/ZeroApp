using System;
using _Project.Scripts.Entities.Abstract;
using _Project.Scripts.Entities.AI.Movement;
using UnityEngine;

namespace _Project.Scripts.Entities.AI.States
{
    public class DeathAIState : BaseAIState
    {
        private readonly AIBase _ai;
        private readonly float _deadDuration;

        private float _deathTimer;
        private AIMovement _movement;
        private readonly Collider2D[] _colliders;

        public DeathAIState(AIBase ai, Collider2D[] colliders, float deadDuration) : base(ai.gameObject)
        {
            _colliders = colliders;
            _deadDuration = deadDuration;
            _ai = ai;

            Initialize();
        }

        private void Initialize()
        {
        }

        public override void EnterState(object[] objects)
        {
            _ai.Target = null;
        }

        public override Type FixedTick()
        {
            _deathTimer += Time.fixedDeltaTime;
            if (_deathTimer >= _deadDuration)
            {
                UnityEngine.Object.Destroy(_ai.gameObject);
            }

            return null;
        }
    }
}