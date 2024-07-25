using UnityEngine;

namespace _Project.Scripts.Entities.Configs
{
    [CreateAssetMenu(fileName = "AnimalEntity", menuName = "Entities/AnimalEntity")]
    public class AnimalEntityConfig : ScriptableObject
    {
        [field: SerializeField] public float FollowSpeed { get; private set; }
        [field: SerializeField] public float DeadDelay { get; private set; }
        [field: SerializeField] public float InteractDistance { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField, Header("Patrol")] public float PatrolRadius { get; private set; }
        [field: SerializeField] public float StartPatrolDelay { get; private set; }
        [field: SerializeField] public float PatrolSpeed { get; private set; }
        [field: SerializeField] public float FindNextPointDelay { get; private set; }
    }
}