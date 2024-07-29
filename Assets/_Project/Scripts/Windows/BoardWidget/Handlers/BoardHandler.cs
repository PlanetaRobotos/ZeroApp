using _Project.Scripts.Infrastructure;
using _Project.Windows.BoardWidget.Views;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Windows.BoardWidget.Handlers
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
            GameObject boardCell = await _boardCellRef.LoadAssetAsyncOnce<GameObject>();
            if (boardCell == null)
            {
                Debug.LogError("BoardCell is not loaded");
                return null;
            }

            return boardCell.GetComponent<CellView>();
        }

        public async UniTask<GameObject> LoadLineView()
        {
            GameObject line = await _lineRef.LoadAssetAsyncOnce<GameObject>();
            if (line == null)
            {
                Debug.LogError("Line is not loaded");
                return null;
            }

            return line;
        }
    }
}