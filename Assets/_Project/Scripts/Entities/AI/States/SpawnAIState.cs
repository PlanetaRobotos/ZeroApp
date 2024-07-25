using System;
using _Project.Scripts.Entities.Abstract;
using UnityEngine;

namespace _Project.Scripts.Entities.AI.States
{
    public class SpawnAIState : BaseAIState
    {
        private readonly AIBase _ai;
        private readonly GameObject _pointer;
        private readonly Collider2D[] _colliders;

        public SpawnAIState(AIBase ai, Collider2D[] colliders, GameObject pointer) 
            : base(ai.gameObject)
        {
            _colliders = colliders;
            _pointer = pointer;
            _ai = ai;
        }

        public override void EnterState(object[] objects)
        {
            if (_pointer != null)
                _pointer.SetActive(true);

            // foreach (var col in _colliders) 
                // if(col.isTrigger) col.enabled = true;
            
            _ai.transform.position = _ai.SpawnPoint;
        }

        public override Type Tick() => typeof(IdleAIState);
    }
}