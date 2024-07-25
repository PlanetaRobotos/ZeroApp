using System;
using System.Collections.Generic;
using _Project.Scripts.Entities.Abstract;
using _Project.Scripts.Entities.Configs;
using _Project.Scripts.Infrastructure.Inputs.Abstract;
using _Project.Scripts.Infrastructure.Mechanics;
using _Project.Scripts.Infrastructure.Services.ApplicationObservers.Runtime;
using UnityEngine;

namespace _Project.Scripts.Entities.Impl
{
    public class PlayerEntity : BaseEntity, ITriggable
    {
        [SerializeField] private SimpleObserver _simpleObserver;
        [SerializeField] private Transform _followParent;
        [SerializeField] private PlayerEntityConfig _playerEntityConfig;

        private List<FollowPoint> _followPositions;

        public PlayerEntityConfig Config => _playerEntityConfig;

        public Transform FollowParent => _followParent;
        private Vector2 _pointerPosition;

        [Inject] private IInputObserver _inputObserver;

        private IUpdater _updater;
        private float _rotationSpeed;
        private float _moveSpeed;

        private void OnEnable()
        {
            _inputObserver.OnClick += MoveToPosition;
        }

        private void OnDisable()
        {
            _inputObserver.OnClick -= MoveToPosition;
        }

        public void Initialize(IUpdater updater, float rotationSpeed, float moveSpeed)
        {
            _rotationSpeed = rotationSpeed;
            _moveSpeed = moveSpeed;
            _updater = updater;
            _simpleObserver.Setup(this);
            
            _pointerPosition = transform.position;

            _updater.Subscribe(OnUpdate, 0.01f);
        }

        private void OnUpdate(float _)
        {
            if (Vector2.Distance(transform.position, _pointerPosition) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, _pointerPosition, _moveSpeed * Time.deltaTime);
                var velocity = _pointerPosition - (Vector2) transform.position;
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

                // Create the target rotation
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

                // Smoothly rotate the player towards the target rotation
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }

        public void TriggerEnter(Collider2D other)
        {
        }

        public void TriggerExit(Collider2D other)
        {
        }

        private void MoveToPosition(Vector2 position)
        {
            _pointerPosition = position;
        }

        private void OnDestroy()
        {
            _updater.Unsubscribe(OnUpdate);
        }

        public void SetFollowPositions(List<Vector2> gridPositions)
        {
            for (var i = 0; i < _followPositions.Count; i++)
            {
                Transform followPosition = _followPositions[i].trans;
                followPosition.localPosition = gridPositions[i];
            }
        }

        public FollowPoint GetAvailableFollowPosition()
        {
            foreach (var followPoint in _followPositions)
            {
                if (followPoint.IsAvailable)
                {
                    followPoint.IsAvailable = false;
                    return followPoint;
                }
            }

            return null;
        }

        public void SetAvailablePoint(int index, bool isAvailable)
        {
            FollowPoint followPosition = _followPositions[index];
            if (followPosition != null) 
                _followPositions[index].IsAvailable = isAvailable;
        }

        public void SpawnFollowPoints(int amount)
        {
            _followPositions = new List<FollowPoint>();
            for (int i = 0; i < amount; i++)
            {
                var followPoint = new GameObject($"Follow Point {i}");
                followPoint.transform.SetParent(_followParent);
                var followPointMono = followPoint.AddComponent<FollowPointMono>();
                followPointMono.SetIndex(i);
                _followPositions.Add(new FollowPoint
                {
                    trans = followPoint.transform,
                    IsAvailable = true,
                    Index = i
                });
            }
        }

        [Serializable]
        public class FollowPoint
        {
            public Transform trans;

            public bool IsAvailable { get; set; }
            public int Index { get; set; }
        }
        
        public class FollowPointMono : MonoBehaviour
        {
            public int index;
            
            public void SetIndex(int index)
            {
                this.index = index;
            }
        }
    }
}