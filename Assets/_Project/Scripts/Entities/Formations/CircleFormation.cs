using System;
using System.Collections.Generic;
using _Project.Scripts.Entities.Configs;
using UnityEngine;

namespace _Project.Scripts.Entities.Formations
{
    [Serializable]
    public class CircleFormation : IFormation
    {
        [field: SerializeField] public float SpacingAngle { get; private set; } = 150;
        [field: SerializeField] public float Radius { get; private set; } = 4f;
        
        [Inject] private EntitiesConfig EntitiesConfig { get; }

        public List<Vector2> CalculatePositions(Vector2 center, List<Transform> data)
        {
            List<Vector2> semiCirclePositions = new();

            float angleStep = SpacingAngle / (EntitiesConfig.MaxFollowAmount - 1);

            for (int i = 0; i < EntitiesConfig.MaxFollowAmount; i++)
            {
                float angle = Mathf.Deg2Rad * (angleStep * i - SpacingAngle / 2);
                float x = -Radius * Mathf.Cos(angle);
                float y = -Radius * Mathf.Sin(angle);
                semiCirclePositions.Add(new Vector2(x, y));
            }

            return semiCirclePositions;
        }
    }
}