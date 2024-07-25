using UnityEngine;

namespace _Project.Configs.Boards
{
    [CreateAssetMenu(fileName = "BoardConfig", menuName = "Gameplay/BoardConfig")]
    public class BoardConfig : ScriptableObject
    {
        [field: SerializeField] public int RowsAmount { get; private set; } = 3;
        [field: SerializeField] public int ColumnsAmount { get; private set; } = 3;
    }
}