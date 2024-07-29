using UnityEngine;

namespace _Project.Configs.Boards
{
    [CreateAssetMenu(fileName = "BoardConfig", menuName = "Gameplay/BoardConfig")]
    public class BoardConfig : ScriptableObject
    {
        [field: SerializeField] public int GridSize { get; private set; } = 3;
    }
}