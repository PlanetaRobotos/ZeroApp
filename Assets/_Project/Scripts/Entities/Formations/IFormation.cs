using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Entities.Formations
{
    public interface IFormation
    {
        List<Vector2> CalculatePositions(Vector2 center, List<Transform> data);
    }
}