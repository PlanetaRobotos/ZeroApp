using UnityEngine;

namespace _Project.Scripts.Entities.AI.Movement
{
    public interface IAIMovement
    {
        void Rotate(Vector3 targetPosition);
        void CalculateDirection(Vector3 targetPosition);
        void Move(Vector2 target, float speed = default);
        bool IsNearby(Vector2 position, float distance = default);
    }

    public class AIMovement : IAIMovement
    {
        private readonly float _followSpeed;
        private readonly float _interactDistance;
        private readonly Transform _trans;

        public AIMovement(float followSpeed, float interactDistance, Transform trans)
        {
            _followSpeed = followSpeed;
            _interactDistance = interactDistance;
            _trans = trans;
        }

        public void Rotate(Vector3 targetPosition)
        {
            // Rotate towards targetPosition
        }

        public void CalculateDirection(Vector3 targetPosition)
        {
            // Calculate direction towards targetPosition
        }

        public void Move(Vector2 target, float speed)
        {
            float step = (speed == 0 ? _followSpeed : speed) * Time.deltaTime;
            _trans.position = Vector2.MoveTowards(_trans.position, target, step);
        }

        public bool IsNearby(Vector2 position, float distance = default)
        {
            if (distance == default)
                distance = _interactDistance;

            var currentDistance = Vector3.Distance(_trans.position, position);

            return currentDistance <= distance;
        }
    }
}