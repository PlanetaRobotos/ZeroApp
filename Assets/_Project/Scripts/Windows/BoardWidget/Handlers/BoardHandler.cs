using _Project.Scripts.Data;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using _Project.Scripts.Infrastructure;
using _Project.Scripts.Windows.HUD;
using UnityEngine;

namespace _Project.Scripts.Windows.BoardWidget.Handlers
{
    public class BoardHandler
    {
        private readonly AssetReference _boardCellRef;
        private readonly AssetReference _lineRef;

        public BoardHandler(AssetReference boardCellRef, AssetReference lineRef)
        {
            _boardCellRef = boardCellRef;
            _lineRef = lineRef;
        }

        public async UniTask<CellView> LoadCellView()
        {
            var boardCell = await _boardCellRef.LoadAssetAsyncOnce<GameObject>();
            if (boardCell == null)
            {
                Debug.LogError("BoardCell is not loaded");
                return null;
            }

            return boardCell.GetComponent<CellView>();
        }
        
        public async UniTask<GameObject> LoadLineView()
        {
            var line = await _lineRef.LoadAssetAsyncOnce<GameObject>();
            if (line == null)
            {
                Debug.LogError("Line is not loaded");
                return null;
            }

            return line;
        }
    }
}