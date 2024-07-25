using _Project.Scripts.Entities.AI.FSM;
using _Project.Scripts.Entities.AI.Movement;
using _Project.Scripts.Entities.Configs;
using _Project.Scripts.Entities.Models;
using _Project.Scripts.Infrastructure.Mechanics;
using UnityEngine;

namespace _Project.Scripts.Entities.Abstract
{
    public abstract class AIBase : BaseEntity, ITriggable
    {
        [SerializeField] EntityType _entityType;
        [SerializeField] private SimpleObserver _observer;

        protected FollowBehaviour _followBehaviour;
        public Collider2D SpawnArea { get; private set; }

        public Vector3 SpawnPoint { get; protected set; }
        public Transform Target { get; set; }
        // public Vector2 TargetPoint { get; set; }
        public AIStateMachine StateMachine => GetComponent<AIStateMachine>();
        public EntityType EntityType => _entityType;
        public EntitiesConfig EntitiesConfig { get; protected set; }

        public bool UseHealthUI { get; protected set; }

        public abstract IAIMovement Movement { get; protected set; }

        public virtual int Cost { get; protected set; } = 1;

        public virtual void Initialize(FollowBehaviour followBehaviour, EntitiesProvider entitiesProvider,
            Collider2D spawnArea, EntitiesConfig entitiesConfig)
        {
            SpawnArea = spawnArea;
            EntitiesConfig = entitiesConfig;
            _followBehaviour = followBehaviour;
            _observer.Setup(this);
        }

        public abstract void InitializeStateMachine();

        public bool HaveTarget(out float distance)
        {
            distance = 0f;
            if (Target != null)
            {
                distance = Vector3.Distance(Target.position, transform.position);
                return true;
            }

            return false;
        }

        public bool HaveTarget(out Vector3 normalizedDirection)
        {
            normalizedDirection = Vector3.zero;
            if (Target != null)
            {
                normalizedDirection = (Target.position - transform.position).normalized;
                return true;
            }

            return false;
        }

        public bool HaveTarget(out float distance, out Vector3 normalizedDirection)
        {
            distance = 0f;
            normalizedDirection = Vector3.zero;
            if (Target != null)
            {
                var position = transform.position;
                var position1 = Target.position;
                distance = Vector3.Distance(position1, position);
                normalizedDirection = (position1 - position).normalized;
                return true;
            }

            return false;
        }

        public abstract bool IsSeeHero();
        public abstract bool IsUseInteraction();
        public abstract void TriggerEnter(Collider2D other);
        public abstract void TriggerExit(Collider2D other);
        public void SetPosition(Vector3 position) => transform.position = position;
        public void SetActiveTrigger(bool active) => _observer.SetActive(active);

        public abstract void SetData(string savePath);
    }
}