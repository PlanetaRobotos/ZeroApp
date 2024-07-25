using System;
using System.Collections.Generic;
using _Project.Scripts.Entities.Configs;
using UnityEngine;

namespace _Project.Scripts.Entities.Formations
{
    [Serializable]
    public class LineFormation : IFormation
    {
        [field: SerializeField] public float Spacing { get; private set; } = 2.2f;
        
        [Inject] private EntitiesConfig EntitiesConfig { get; }

        public List<Vector2> CalculatePositions(Vector2 center, List<Transform> data)
        {
            List<Vector2> positions = new();

            for (int i = 0; i < EntitiesConfig.MaxFollowAmount; i++)
            {
                float offsetX = i * Spacing;
                Vector2 position = new(-offsetX, 0);
                positions.Add(position);
            }

            return positions;
        }
    }
}