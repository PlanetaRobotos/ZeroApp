using System;
using UnityEngine;

namespace _Project.Scripts.Entities.AI.States
{
    public abstract class BaseAIState
    {
        private readonly GameObject _gameObject;

        protected BaseAIState(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public virtual void EnterState(object[] objects)
        {
        }

        public virtual Type Tick() => null;

        public virtual Type FixedTick() => null;

        public virtual Type TriggerEnter(Collider2D other) => null;

        public virtual Type TriggerExit(Collider2D other) => null;
    }
}